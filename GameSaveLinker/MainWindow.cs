using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace GameSaveLinker
{
	public partial class MainWindow : Form
	{
		protected GameSaveManager manager;

		public MainWindow()
		{
			InitializeComponent();

			manager = new GameSaveManager();
			
			RefreshView(true);
		}

		protected void RefreshView(Boolean rebuild = false)
		{
			this.viewGames.BeginUpdate();
			if (rebuild)
			{
				this.manager.LoadGames();
				this.viewGames.ClearObjects();
				this.viewGames.AddObjects(this.manager.Games);
			}
			else
			{
				this.viewGames.RefreshObjects(this.manager.Games);
			}
			this.viewGames.EndUpdate();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.F5:
					RefreshView(true);
					return true;

				case Keys.Escape:
					if (buttonExit.Enabled)
					{
						Close();
						return true;
					}
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonHideLinks_Click(object sender, EventArgs e)
		{
			if (this.manager.HandleHideLinks())
			{
				this.RefreshView();
			}
		}

		private void buttonShowLinks_Click(object sender, EventArgs e)
		{
			if (this.manager.HandleShowLinks())
			{
				this.RefreshView();
			}
		}

		private void buttonCreateLinks_Click(object sender, EventArgs e)
		{
			if (this.manager.HandleMoveToStorage())
			{
				this.RefreshView();
			}
		}

		private void buttonRemoveLinks_Click(object sender, EventArgs e)
		{
			if (this.manager.HandleMoveToOriginal())
			{
				this.RefreshView();
			}
		}

		private void contextGames_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			int selected = this.viewGames.SelectedItems.Count;

			this.contextGames.Items[3].Enabled = (selected > 0);
			this.contextGames.Items[4].Enabled = (selected > 0);
		}

		private void checkSelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (BrightIdeasSoftware.OLVListItem selected in this.viewGames.SelectedItems)
			{
				((Game)selected.RowObject).Checked = true;
			}

			this.RefreshView();
		}

		private void uncheckSelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (BrightIdeasSoftware.OLVListItem selected in this.viewGames.SelectedItems)
			{
				((Game)selected.RowObject).Checked = false;
			}
			this.RefreshView();
		}

		private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.manager.CheckAll(true);
			this.RefreshView();
		}

		private void checkNoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.manager.CheckAll(false);
			this.RefreshView();
		}
	}
}
