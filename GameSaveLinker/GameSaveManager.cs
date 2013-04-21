using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using Monitor.Core.Utilities;

namespace GameSaveLinker
{
	public class GameSaveManager
	{
		public GameList Games {
			get; private set;
		}

		public GameSaveManager()
		{
		}

		public int LoadGames(Boolean includeMissing = false)
		{
			this.Games = new GameList();

			string xmlFile = "Games.xml";
			string xmlPath = Path.Combine(Application.StartupPath, xmlFile);
			if (!File.Exists(xmlPath))
			{
				xmlPath = Path.Combine(SpecialFolder.GetSavedGames(), xmlFile);
				if (!File.Exists(xmlPath))
				{
					throw new Exception(string.Format("{0} not found", xmlFile));
				}
			}

			return this.Games.LoadGames(xmlPath, includeMissing);
		}

		public void CheckAll(Boolean value = true)
		{
			foreach (Game game in this.Games)
			{
				game.Checked = value;
			}
		}

		protected Boolean ShowPreviewDialog(ActionList actions)
		{
			if (!this.Games.HasChecked())
			{
				MessageBox.Show("No games checked");
				return false;
			}

			if (actions.Count == 0)
			{
				MessageBox.Show("Nothing to do");
				return false;
			}

			Preview preview = new Preview(this, actions);
			preview.ShowDialog();
			if (preview.DialogResult == DialogResult.OK)
			{
				return true;
			}
			return false;
		}

		public Boolean HandleShowLinks(Boolean show = true)
		{
			ActionList actions = new ActionList();

			foreach (Game game in this.Games)
			{
				if (!game.Checked)
				{
					continue;
				}

				if (game.State != Game.SaveState.FullLink)
				{
					continue;
				}

				List<String> paths = this.GetPathsToHide(game.OriginalPath);
				for (int i = 0, t = paths.Count; i < t; i++)
				{
					String realPath = GamePlaceholder.ReplacePlaceholders(paths[i]);
					if (show)
					{
						if (DirectoryEx.IsHidden(realPath))
						{
							actions.AddAction("show", game, paths[i]);
						}
					}
					else
					{
						if (!DirectoryEx.IsHidden(realPath))
						{
							actions.AddAction("hide", game, paths[i]);
						}
					}
				}
			}

			if (actions.Count > 0)
			{
				// Sort the actions by number of slashes (descending) so we hide sub folders first. This should prevent
				// the parent folders from failing to hide.
				actions.Sort(delegate(Action a1, Action a2)
				{
					return a2.Path.Split(Path.DirectorySeparatorChar).Length - a1.Path.Split(Path.DirectorySeparatorChar).Length;
				});
			}

			return this.ShowPreviewDialog(actions);
		}

		public Boolean HandleHideLinks()
		{
			return this.HandleShowLinks(false);
		}

		public Boolean HandleMoveToStorage(Boolean create = true)
		{
			ActionList actions = new ActionList();

			foreach (Game game in this.Games)
			{
				if (game.Checked != true)
				{
					continue;
				}

				if (game.State == Game.SaveState.Missing || game.State == Game.SaveState.Conflict)
				{
					Trace.WriteLine(String.Format("{0}.HandleCreateLinks({1}) Skipping game due to save state {2}", this.GetType(), create, game.ToString()));
					continue;
				}

				if (create)
				{
					if (game.State == Game.SaveState.FullLink)
					{
						Trace.WriteLine(String.Format("{0}.HandleCreateLinks({1}) Skipping game due to save state {2}", this.GetType(), create, game.ToString()));
						continue;
					}

					if (Directory.Exists(game.OriginalPathFull))
					{
						actions.AddAction("move-storage", game);
					}

					actions.AddAction("create-link", game);
				}
				else
				{
					if (game.State == Game.SaveState.NoLink)
					{
						Trace.WriteLine(String.Format("{0}.HandleCreateLinks({1}) Skipping game due to save state {2}", this.GetType(), create, game.ToString()));
						continue;
					}

					List<String> paths = this.GetPathsToHide(game.OriginalPath);

					// Start at 1 since the actual save path does not need to be unhidden
					for (int i = 1, t = paths.Count; i < t; i++)
					{
						if (DirectoryEx.IsHidden(GamePlaceholder.ReplacePlaceholders(paths[i])))
						{
							actions.AddAction("show", game, paths[i]);
						}
					}

					if (game.State == Game.SaveState.FullLink)
					{
						actions.AddAction("delete-link", game);
					}

					actions.AddAction("move-original", game);
				}
			}

			return this.ShowPreviewDialog(actions);
		}

		public Boolean HandleMoveToOriginal()
		{
			return this.HandleMoveToStorage(false);
		}

		public static String PreviewAction(Action action)
		{
			String moveFormat = "{0} -> {1}";
			
			switch (action.Type)
			{
				case "hide":
				case "show":
					return GamePlaceholder.GetDisplayPath(action.Path);
				case "move-storage":
					return String.Format(moveFormat, action.Game.OriginalPathDisplay, action.Game.StoragePathDisplay);
				case "move-original":
					return String.Format(moveFormat, action.Game.StoragePathDisplay, action.Game.OriginalPathDisplay);
				case "create-link":
					return String.Format(moveFormat, action.Game.StoragePathDisplay, action.Game.OriginalPathDisplay);
				case "delete-link":
					return action.Game.OriginalPathDisplay;
				default:
					return String.Empty;
			}
		}

		public Boolean RunAction(Action action)
		{
			switch (action.Type)
			{
				case "hide":
				case "show":
					return this.ShowPath(action.Game, GamePlaceholder.ReplacePlaceholders(action.Path), (action.Type == "show"));
				case "move-storage":
					return this.MoveSaves(action.Game, false);
				case "move-original":
					return this.MoveSaves(action.Game, true);
				case "create-link":
					return this.CreateLink(action.Game);
				case "delete-link":
					return this.RemoveLink(action.Game);
				default:
					throw new Exception(String.Format("Unhandled action type {0}", action));
			}

		}

		public bool MoveSaves(Game game, bool original)
		{
			if (game.State == Game.SaveState.Missing || game.State == Game.SaveState.Conflict)
			{
				return false;
			}

			if (original)
			{
				if (game.State == Game.SaveState.FullLink)
				{
					return false;
				}

				FileSystem.MoveDirectory(game.StoragePathFull, game.OriginalPathFull, UIOption.AllDialogs);
				game.UpdateSaveState();
				return (game.State == Game.SaveState.NoLink);
			}
			else
			{
				if (game.State != Game.SaveState.NoLink)
				{
					return false;
				}

				FileSystem.MoveDirectory(game.OriginalPathFull, game.StoragePathFull, UIOption.AllDialogs);
				game.UpdateSaveState();
				return (game.State == Game.SaveState.PartialLink);
			}
		}

		public bool CreateLink(Game game)
		{
			if (game.State != Game.SaveState.NoLink && game.State != Game.SaveState.PartialLink)
			{
				return false;
			}

			if (Directory.Exists(game.OriginalPathFull))
			{
				throw new Exception("Directory should not exist");
			}

			JunctionPoint.Create(game.OriginalPathFull, game.StoragePathFull, true);
			game.UpdateSaveState();
			return (game.State == Game.SaveState.FullLink);
		}

		public bool RemoveLink(Game game)
		{
			if (game.State != Game.SaveState.FullLink)
			{
				return false;
			}

			Directory.Delete(game.OriginalPathFull);
			game.UpdateSaveState();
			return (game.State == Game.SaveState.PartialLink);
		}

		public bool ShowPath(Game game, String path, bool show = true)
		{
			if (!show && game != null && path != game.OriginalPathFull)
			{
				if (DirectoryEx.HasNonHidden(path))
				{
					Trace.WriteLine(String.Format("{0}.ShowPath({1}, {2}) Skipping directory, not empty {3}", this.GetType(), path, show, path));
					return false;
				}
			}
			
			if (show)
			{
				if (!DirectoryEx.Show(path))
				{
					Trace.WriteLine(String.Format("{0}.ShowPath({1}, {2}) Failed to change attributes for path {3}", this.GetType(), path, show, path));
					return false;
				}
			}
			else
			{
				if (!DirectoryEx.Hide(path))
				{
					Trace.WriteLine(String.Format("{0}.ShowPath({1}, {2}) Failed to change attributes for path {3}", this.GetType(), path, show, path));
					return false;
				}
			}
			return true;
		}

		/**
			* Retrieves a list of the paths that need to be hidden for a game
			*/
		public List<String> GetPathsToHide(String path/*, Boolean full = true*/)
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
					paths.Add(current);
				}
			}

			// Reverse the paths so when we eventually change the hidden state the empty folder check does not trigger 
			paths.Reverse();

			return paths;
		}
	}
}
