using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.IO;



namespace GameSaveLinker
{
	public partial class MainWindow : Form
	{
		protected GameList GamesList;
		protected ImageList ImagesList;

		public MainWindow()
		{
			InitializeComponent();

			this.GamesList = new GameList();
			
			this.ImagesList = new ImageList();
			this.ImagesList.Images.Add("good", GameSaveLinker.Properties.Resources.tick);
			this.ImagesList.Images.Add("bad", GameSaveLinker.Properties.Resources.cross);

			this.GamesControl.SmallImageList = this.ImagesList;

			this.ColumnLink.ImageGetter = delegate(object rowObject)
			{
				return ((Game)rowObject).IsLinked ? "good" : "bad";
			};

			this.ColumnSaves.ImageGetter = delegate(object rowObject)
			{
				return ((Game)rowObject).HasSaves ? "good" : "bad";
			};

			this.GamesControl.CellToolTipGetter = delegate(BrightIdeasSoftware.OLVColumn col, object rowObject)
			{
				// Only show the status tooltip on the link or saves columns
				if (col.DisplayIndex == this.ColumnLink.Index || col.DisplayIndex == this.ColumnSaves.Index)
				{
					return ((Game)rowObject).Status;
				}
				return String.Empty;
			};

			RefreshGamesFromXml();
		}

		protected void RefreshGamesFromXml()
		{
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

			this.GamesList.LoadGames(xmlPath);
			this.GamesControl.BeginUpdate();
			this.GamesControl.ClearObjects();
			this.GamesControl.AddObjects(this.GamesList);
			this.GamesControl.EndUpdate();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.F5:
					RefreshGamesFromXml();
					return true;

				case Keys.Escape:
					if (ExitButton.Enabled)
					{
						Close();
						return true;
					}
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void HideLinksButton_Click(object sender, EventArgs e)
		{
			this.EnableButtons(false);
			this.HandleHideLinks(true);
			this.EnableButtons(true);
		}

		private void ShowLinksButton_Click(object sender, EventArgs e)
		{
			this.EnableButtons(false);
			this.HandleHideLinks(false);
			this.EnableButtons(true);
		}

		private void CreateLinksButton_Click(object sender, EventArgs e)
		{
			this.EnableButtons(false);
			this.HandleCreateLinks(true);
			this.EnableButtons(true);
		}

		private void RemoveLinksButton_Click(object sender, EventArgs e)
		{
			this.EnableButtons(false);
			this.HandleCreateLinks(false);
			this.EnableButtons(true);
		}

		public void EnableButtons(bool enable)
		{
			HideLinksButton.Enabled = enable;
			ShowLinksButton.Enabled = enable;
			CreateLinksButton.Enabled = enable;
			RemoveLinksButton.Enabled = enable;
			ExitButton.Enabled = enable;
		}

		private void HandleHideLinks(bool hide)
		{
			Console.WriteLine("{0}.HandleHideLinks: Started {1} links", this.GetType().Name, (hide ? "hiding" : "showing"));

			int changed = 0;
			int skipped = 0;
			int failed = 0;

			foreach (Game game in this.GamesList)
			{
				if (!game.HasSaves || !game.IsLinked)
				{
					Console.WriteLine("{0}.HandleHideLinks: Skipping game {1}, {2}", this.GetType().Name, game.Name, (!game.HasSaves ? "no saves" : "not linked"));
					skipped++;
					continue;
				}

				if (hide)
				{
					if (game.HidePaths())
					{
						changed++;
					}
					else
					{
						failed++;
					}
				}
				else
				{
					if (game.ShowPaths())
					{
						changed++;
					}
					else
					{
						failed++;
					}
				}

				this.GamesControl.RefreshObject(game);
				this.GamesControl.EnsureModelVisible(game);
			}

			string directory = SpecialFolder.GetMyGames();
			if (hide)
			{
				if (GameList.DirectoryHasNonHidden(directory))
				{
					Console.WriteLine("{0}.HandleHideLinks: Not hiding {1}, not empty", this.GetType().Name, directory);
				}
				else
				{
					File.SetAttributes(directory, File.GetAttributes(directory) | FileAttributes.System | FileAttributes.Hidden);
				}
			}
			else
			{
				File.SetAttributes(directory, File.GetAttributes(directory) & ~(FileAttributes.System | FileAttributes.Hidden));
			}
				
			Console.WriteLine("{0}.HandleHideLinks: Finished {1} links, {2} changed, {3} skipped, {4} failed", this.GetType().Name, hide ? "hiding" : "showing", changed, skipped, failed);
			MessageBox.Show(String.Format("Finished {0} links, {1} changed, {2} skipped, {3} failed", hide ? "hiding" : "showing", changed, skipped, failed));
		}

		private void HandleCreateLinks(bool create)
		{
			Console.WriteLine("{0}.HandleCreateLinks: Started {1} links", this.GetType().Name, (create ? "creating" : "removing"));

			int changed = 0;
			int skipped = 0;
			int failed = 0;

			foreach (Game game in this.GamesList)
			{
				if (create)
				{
					if (!game.HasSaves || game.IsLinked)
					{
						Console.WriteLine("{0}.HandleCreateLinks: Skipping game {1}, {2}", this.GetType().Name, game.Name, (!game.HasSaves ? "no saves" : "already linked"));
						skipped++;
						continue;
					}

					if (game.CreateLink())
					{
						changed++;
					}
					else
					{
						failed++;
					}
				}
				else
				{
					if (!game.HasSaves || !game.IsLinked)
					{
						Console.WriteLine("{0}.HandleCreateLinks: Skipping game {1}, {2}", this.GetType().Name, game.Name, (!game.HasSaves ? "no saves" : "not linked"));
						skipped++;
						continue;
					}

					if (game.RemoveLink())
					{
						changed++;
					}
					else
					{
						failed++;
					}
				}

				this.GamesControl.RefreshObject(game);
				this.GamesControl.EnsureModelVisible(game);
			}

			Console.WriteLine("{0}.HandleCreateLinks: Finished {1} links, {2} changed, {3} skipped, {4} failed", this.GetType().Name, (create ? "creating" : "removing"), changed, skipped, failed);
			MessageBox.Show(String.Format("Finished {0} links, {1} changed, {2} skipped, {3} failed", (create ? "creating" : "removing"), changed, skipped, failed));
		}
	}
}
