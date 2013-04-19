using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Monitor.Core.Utilities;

namespace GameSaveLinker
{
	public class Game
	{
		public string Linked
		{
			get { return (this.IsLinked ? "Yes" : "No"); }
		}

		public string Saves
		{
			get { return (this.HasSaves ? "Yes" : "No"); }
		}

		public bool IsLinked
		{
			get
			{
				return Directory.Exists(this.SavePath)
					&& JunctionPoint.Exists(this.SavePath)
					&& JunctionPoint.GetTarget(this.SavePath) == this.LinkPath
					;
			}
		}

		public bool HasSaves
		{
			get
			{
				return ((Directory.Exists(this.LinkPath)) || (Directory.Exists(this.SavePath) && !JunctionPoint.Exists(this.SavePath)));
			}
		}

		public string Status
		{
			get
			{
				if (Directory.Exists(this.LinkPath))
				{
					return string.Format("Saves exist in linked path \"{0}\"", this.LinkPath);
				}
				// If the link path does not exist the save path must not be a link for saves to exist
				else if (Directory.Exists(this.SavePath) && !JunctionPoint.Exists(this.SavePath))
				{
					return string.Format("Saves exist in save path \"{0}\" but are not linked to \"{1}\"", this.SavePath, this.LinkPath);
				}
				return string.Format("Saves not found in either save path \"{0}\" or linked path \"{1}\"", this.SavePath, this.LinkPath);
			}
		}

		public bool Checked
		{
			get { return this._checked; }
			set { this._checked = value; }
		}

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public string SavePath
		{
			get { return this._savePath; }
			set { this._savePath = value; }
		}

		public string LinkPath
		{
			get { return this._linkPath; }
			set { this._linkPath = value; }
		}

		public string UnparsedSavePath
		{
			get { return this._unparsedSavePath; }
			set { this._unparsedSavePath = value; }
		}

		public string UnparsedLinkPath
		{
			get { return this._unparsedLinkPath; }
			set { this._unparsedLinkPath = value; }
		}

		public bool HidePaths()
		{
			List<string> paths = this.GetPaths();
			bool allHidden = true, first = (paths.Count > 0);
			
			paths.Reverse();

			foreach (string directory in paths)
			{
				if (!first)
				{
					if (GameList.DirectoryHasNonHidden(directory))
					{
						Console.WriteLine("{0}.HidePaths: Skipping directory, not empty {1}", this.GetType().Name, directory);
						allHidden = false;
						continue;
					}
				}
				first = false;

				File.SetAttributes(directory, File.GetAttributes(directory) | FileAttributes.System | FileAttributes.Hidden);
				
				FileAttributes fileAttributes = File.GetAttributes(directory);
				if ((fileAttributes & FileAttributes.System) != FileAttributes.System || (fileAttributes & FileAttributes.Hidden) != FileAttributes.Hidden)
				{
					Console.WriteLine("{0}.HidePaths: Failed to change attributes for path {1}", this.GetType().Name, directory);
					allHidden = false;
				}
			}
			return allHidden;
		}

		public bool ShowPaths()
		{
			bool allShown = true;
			foreach (string directory in this.GetPaths())
			{
				File.SetAttributes(directory, File.GetAttributes(directory) & ~(FileAttributes.System | FileAttributes.Hidden));

				FileAttributes fileAttributes = File.GetAttributes(directory);
				if ((fileAttributes & FileAttributes.System) == FileAttributes.System || (fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
				{
					Console.WriteLine("{0}.ShowPaths: Failed to change attributes for path {1}", this.GetType().Name, directory);
					allShown = false;
				}
			}
			return allShown;
		}
		
		public bool CreateLink()
		{
			if (!this.HasSaves || this.IsLinked)
			{
				Console.WriteLine("{0}.CreateLink: Skipping, {1}", this.GetType().Name, (!this.HasSaves ? "no saves" : "already linked"));
				return false;
			}

			if (Directory.Exists(this.SavePath))
			{
				File.SetAttributes(this.SavePath, File.GetAttributes(this.SavePath) & ~(FileAttributes.ReadOnly | FileAttributes.System | FileAttributes.Hidden));
				FileSystem.MoveDirectory(this.SavePath, this.LinkPath, UIOption.AllDialogs);
			}

			JunctionPoint.Create(this.SavePath, this.LinkPath, true);

			return (this.HasSaves && this.IsLinked);
		}

		public bool RemoveLink()
		{
			if (!this.HasSaves || !this.IsLinked)
			{
				Console.WriteLine("{0}.RemoveLink: Skipping, {1}", this.GetType().Name, (!this.HasSaves ? "no saves" : "not linked"));
				return false;
			}

			File.SetAttributes(this.SavePath, File.GetAttributes(this.SavePath) & ~(FileAttributes.ReadOnly | FileAttributes.System | FileAttributes.Hidden));
			Directory.Delete(this.SavePath);
			FileSystem.MoveDirectory(this.LinkPath, this.SavePath, UIOption.AllDialogs);

			return (this.HasSaves && !this.IsLinked);

		}

		public List<string> GetPaths()
		{
			List<string> paths = new List<string>();

			string currentPath = "";
			string[] sections = this.UnparsedSavePath.Split(Path.DirectorySeparatorChar);
			foreach (string directory in sections)
			{
				if (String.IsNullOrEmpty(currentPath))
				{
					currentPath = directory;
					continue;
				}

				currentPath = Path.Combine(currentPath, directory);
				paths.Add(GamePlaceholder.ReplacePlaceholders(currentPath));
			}

			return paths;
		}

		protected bool _checked = true;
		protected string _name;
		protected string _savePath;
		protected string _linkPath;
		protected string _unparsedSavePath;
		protected string _unparsedLinkPath;
	}
}
