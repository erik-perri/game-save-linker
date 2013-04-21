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

				List<String> paths = game.GetPathsToHide();
				for (int i = 0, t = paths.Count; i < t; i++)
				{
					if (show)
					{
						if (DirectoryEx.IsHidden(paths[i]))
						{
							actions.AddAction("show", game, i);
						}
					}
					else
					{
						if (!DirectoryEx.IsHidden(paths[i]))
						{
							actions.AddAction("hide", game, i);
						}
					}
				}
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

					if (Directory.Exists(game.GetOriginalPath()))
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

					List<String> paths = game.GetPathsToHide();

					// Start at 1 since the actual save path does not need to be unhidden
					for (int i = 1, t = paths.Count; i < t; i++)
					{
						if (DirectoryEx.IsHidden(paths[i]))
						{
							actions.AddAction("show", game, i);
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

		public static String PreviewAction(Game game, String action, int pathId)
		{
			String moveFormat = "{0} -> {1}";

			switch (action)
			{
				case "hide":
				case "show":
					List<String> paths = game.GetPathsToHide(false);
					return paths[pathId];
				case "move-storage":
					return String.Format(moveFormat, game.GetOriginalPath(false), game.GetStoragePath(false));
				case "move-original":
					return String.Format(moveFormat, game.GetStoragePath(false), game.GetOriginalPath(false));
				case "create-link":
					return String.Format(moveFormat, game.GetStoragePath(false), game.GetOriginalPath(false));
				case "delete-link":
					return game.GetOriginalPath(false);
				default:
					return String.Empty;
			}
		}

		public Boolean RunAction(Action action)
		{
			switch (action.Type)
			{
				case "hide":
					return this.HidePath(action.Game, action.PathId);
				case "show":
					return this.ShowPath(action.Game, action.PathId);
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

				FileSystem.MoveDirectory(game.GetStoragePath(), game.GetOriginalPath(), UIOption.AllDialogs);
				game.UpdateSaveState();
				return (game.State == Game.SaveState.NoLink);
			}
			else
			{
				if (game.State != Game.SaveState.NoLink)
				{
					return false;
				}

				FileSystem.MoveDirectory(game.GetOriginalPath(), game.GetStoragePath(), UIOption.AllDialogs);
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

			if (Directory.Exists(game.GetOriginalPath()))
			{
				throw new Exception("Directory should not exist");
			}

			JunctionPoint.Create(game.GetOriginalPath(), game.GetStoragePath(), true);
			game.UpdateSaveState();
			return (game.State == Game.SaveState.FullLink);
		}

		public bool RemoveLink(Game game)
		{
			if (game.State != Game.SaveState.FullLink)
			{
				return false;
			}

			Directory.Delete(game.GetOriginalPath());
			game.UpdateSaveState();
			return (game.State == Game.SaveState.PartialLink);
		}

		public bool HidePath(Game game, int index)
		{
			return this.ShowPath(game, index, false);
		}

		public bool ShowPath(Game game, int index, bool show = true)
		{
			String path = game.GetPathsToHide()[index];
			if (!show && index > 0)
			{
				if (DirectoryEx.HasNonHidden(path))
				{
					Trace.WriteLine(String.Format("{0}.ShowPath({1}, {2}) Skipping directory, not empty {3}", game.GetType(), index, show, path));
					return false;
				}
			}

			if (show)
			{
				if (!DirectoryEx.Show(path))
				{
					Trace.WriteLine(String.Format("{0}.ShowPath({1}, {2}) Failed to change attributes for path {3}", game.GetType(), index, show, path));
					return false;
				}
			}
			else
			{
				if (!DirectoryEx.Hide(path))
				{
					Trace.WriteLine(String.Format("{0}.ShowPath({1}, {2}) Failed to change attributes for path {3}", game.GetType(), index, show, path));
					return false;
				}
			}
			return true;
		}

	}
}
