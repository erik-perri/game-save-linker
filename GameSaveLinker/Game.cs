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
		public SaveState State { private set; get; }

		public String OriginalPath
		{
			set
			{
				this._originalPath = value; 
				this.UpdateSaveState();
			}
			get { return this._originalPath; }
		}
		public String OriginalPathFull { get { return GamePlaceholder.ReplacePlaceholders(this.OriginalPath); } }
		public String OriginalPathDisplay { get { return GamePlaceholder.GetDisplayPath(this.OriginalPath); } }

		public String StoragePath
		{
			set
			{
				this._storagePath = value;
				this.UpdateSaveState();
			}
			get { return this._storagePath; }
		}
		public String StoragePathFull { get { return GamePlaceholder.ReplacePlaceholders(this.StoragePath); } }
		public String StoragePathDisplay { get { return GamePlaceholder.GetDisplayPath(this.StoragePath); } }

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
			if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.OriginalPathFull))
			{
				return false;
			}

			if (checkStorage && string.IsNullOrEmpty(this.StoragePathFull))
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
			String originalPath = this.OriginalPathFull;
			String storagePath = this.StoragePathFull;

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

						Trace.WriteLine(String.Format("StoragePath:        {0}", this.StoragePath));
						Trace.WriteLine(String.Format("StoragePathFull:    {0}", this.StoragePathFull));
						Trace.WriteLine(String.Format("StoragePathDisplay: {0}", this.StoragePathDisplay));

						Trace.WriteLine(String.Format("OriginalPath:        {0}", this.OriginalPath));
						Trace.WriteLine(String.Format("OriginalPathFull:    {0}", this.OriginalPathFull));
						Trace.WriteLine(String.Format("OriginalPathDisplay: {0}", this.OriginalPathDisplay));
					Trace.Unindent();

				Trace.Unindent();
			}
			return this.Name;
		}
	}
}
