using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Monitor.Core.Utilities;
using System.Diagnostics;

namespace GameSaveLinker
{
	public class Game
	{
		public String Name;
		public Boolean Checked;
		public String OriginalPath { set { this._originalPath = value; this.UpdateSaveState(); } }
		public String StoragePath { set { this._storagePath = value; this.UpdateSaveState(); } }
		public SaveState State { private set; get; }

		protected String _originalPath;
		protected String _storagePath;

		public enum SaveState
		{
			Missing,
			Conflict,
			NoLink,
			PartialLink,
			FullLink
		};

		public Game()
		{
			this.Name = String.Empty;
			this.Checked = true;
			this.State = SaveState.Missing;
			this._originalPath = String.Empty;
			this._storagePath = String.Empty;
		}

		/**
		 * Called by the view to convert the state into readable text for the Saves column
		 */
		public String SavesAspect
		{
			get
			{
				return (this.State == SaveState.Missing ? "No" : "Yes");
			}
		}

		/**
		 * Called by the view to convert the state into an icon for the Saves column
		 */
		public String SavesIconAspect
		{
			get
			{
				return (this.State == SaveState.Missing ? "cross" : "tick");
			}
		}

		/**
		 * Called by the view to convert the state into readable text for the Linked column
		 */
		public String LinkedAspect
		{
			get
			{
				switch (this.State)
				{
					case SaveState.Missing:
						return "";
					case SaveState.Conflict:
						return "Conflict";
					case SaveState.NoLink:
						return "No";
					case SaveState.PartialLink:
						return "Partial";
					case SaveState.FullLink:
						return "Yes";
					default:
						return "Unknown";
				}
			}
		}

		/**
		 * Called by the view to convert the state into an icon for the Linked column
		 */
		public String LinkedIconAspect
		{
			get
			{
				switch (this.State)
				{
					case SaveState.Missing:
						return "";
					case SaveState.Conflict:
						return "exclamation_red";
					case SaveState.NoLink:
						return "cross";
					case SaveState.PartialLink:
						return "exclamation";
					case SaveState.FullLink:
						return "tick";
					default:
						return "";
				}
			}
		}

		/**
		 * Checks whether this is a valid game or not (has name, original path, and optionally storage path)
		 */
		public bool IsValid(Boolean checkStorage = true)
		{
			if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.GetOriginalPath()))
			{
				return false;
			}

			if (checkStorage && string.IsNullOrEmpty(this.GetStoragePath()))
			{
				return false;
			}

			return true;
		}

		/**
		 * Updates the state
		 */
		public SaveState UpdateSaveState()
		{
			String originalPath = this.GetOriginalPath();
			String storagePath = this.GetStoragePath();

			this.State = SaveState.Missing;
			if (Directory.Exists(originalPath) && JunctionPoint.Exists(originalPath))
			{
				if (JunctionPoint.GetTarget(originalPath) == storagePath)
				{
					this.State = SaveState.FullLink;
				}
				else
				{
					this.State = SaveState.Conflict;
				}
			}
			else if (Directory.Exists(originalPath) && Directory.Exists(storagePath))
			{
				this.State = SaveState.Conflict;
			}
			else if (Directory.Exists(storagePath) && !Directory.Exists(originalPath))
			{
				this.State = SaveState.PartialLink;
			}
			else if (Directory.Exists(originalPath) && !Directory.Exists(storagePath))
			{
				this.State = SaveState.NoLink;
			}

			return this.State;
		}

		/**
		 * Retrieves the original path, replacing placeholders if requested
		 */
		public String GetOriginalPath(Boolean full = true)
		{
			if (full)
			{
				return GamePlaceholder.ReplacePlaceholders(this._originalPath);
			}
			return this._GetDisplayPath(this._originalPath);
		}

		/**
		 * Retrieves the storage path, replacing placeholders if requested
		 */
		public String GetStoragePath(Boolean full = true)
		{
			if (full)
			{
				return GamePlaceholder.ReplacePlaceholders(this._storagePath);
			}
			return this._GetDisplayPath(this._storagePath);
		}

		/**
		 * Retrieves a list of the paths that need to be hidden after linking is done
		 */
		public List<String> GetPathsToHide(Boolean full = true)
		{
			return this._GetPaths(this._originalPath, full);
		}

		/**
		 * Retrieves a list of the paths that need to be hidden for a game
		 */
		protected List<String> _GetPaths(String path, Boolean full = true)
		{
			List<String> paths = new List<String>();

			String current = "";
			String[] sections = path.Split(Path.DirectorySeparatorChar);
			foreach (String directory in sections)
			{
				if (String.IsNullOrEmpty(current))
				{
					current = directory;
				}
				else
				{
					current = Path.Combine(current, directory);
					paths.Add((full ? GamePlaceholder.ReplacePlaceholders(current) : this._GetDisplayPath(current)));
				}
			}

			// Reverse the paths so when we eventually change the hidden state the empty folder check does not trigger 
			paths.Reverse();

			return paths;
		}

		/**
		 * Cuts the path down to start at the parsed placeholder
		 */
		protected String _GetDisplayPath(String path)
		{
			int beforeParseCount = path.Split(Path.DirectorySeparatorChar).Length;
			path = GamePlaceholder.ReplacePlaceholders(path);
			int afterParseCount = path.Split(Path.DirectorySeparatorChar).Length;

			for (int i = 0; i < afterParseCount - beforeParseCount; i++)
			{
				path = path.Substring(path.IndexOf(Path.DirectorySeparatorChar) + 1);
			}

			return path;
		}

		public String ToString(Boolean verbose = false)
		{
			if (verbose)
			{
				Trace.Indent();
					Trace.WriteLine(String.Format("{0}", this.GetType()));
					
					Trace.Indent();
						Trace.WriteLine(String.Format("Name:        {0}", this.Name));
						Trace.WriteLine(String.Format("Checked:     {0}", this.Checked));
						Trace.WriteLine(String.Format("State:       {0}", this.State));

						Trace.WriteLine(String.Format("_originalPath: {0}", this._originalPath));
						Trace.WriteLine(String.Format("_storagePath:  {0}", this._storagePath));

						Trace.WriteLine(String.Format("GetStoragePath(true):  {0}", this.GetStoragePath(true)));
						Trace.WriteLine(String.Format("GetStoragePath(false): {0}", this.GetStoragePath(false)));
						Trace.WriteLine(String.Format("GetOriginalPath(true):  {0}", this.GetOriginalPath(true)));
						Trace.WriteLine(String.Format("GetOriginalPath(false): {0}", this.GetOriginalPath(false)));

						var originalPaths = this.GetPathsToHide(true);
						Trace.WriteLine(String.Format("GetPathsToHide(true):  {0}", originalPaths.Count));
						Trace.Indent();
							foreach (String path in originalPaths)
							{
								Trace.WriteLine(String.Format("originalPaths[]: {0}", path));
							}
						Trace.Unindent();

						var hiddenPaths = this.GetPathsToHide(false);
						Trace.WriteLine(String.Format("GetPathsToHide(false): {0}", hiddenPaths.Count));
						Trace.Indent();
							foreach (String path in hiddenPaths)
							{
								Trace.WriteLine(String.Format("hiddenPaths[]: {0}", path));
							}
						Trace.Unindent();

					Trace.Unindent();

				Trace.Unindent();
			}
			return this.Name;
		}
	}
}
