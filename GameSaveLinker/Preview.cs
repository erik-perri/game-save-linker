using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameSaveLinker
{
	public partial class Preview : Form
	{
		protected ActionList actions;
		protected GameSaveManager manager;

		public Preview(GameSaveManager manager, ActionList actions)
		{
			InitializeComponent();

			this.manager = manager;
			this.actions = actions;

			this.StartPosition = FormStartPosition.CenterParent;
			this.CancelButton = this.buttonCancel;

			this.columnStatus.FillsFreeSpace = true;
			this.viewActions.AlwaysGroupByColumn = this.columnGame;
			this.viewActions.ClearObjects();
			this.viewActions.SetObjects(this.actions);
		}

		private void buttonContinue_Click(object sender, EventArgs e)
		{
			foreach (Action action in this.actions)
			{
				if (this.manager.RunAction(action))
				{
					this.viewActions.RemoveObject(action);
				}
				else
				{
					action.Status = Action.ActionStatus.Failure;
					this.viewActions.RefreshObject(action);
					this.viewActions.EnsureModelVisible(action);
				}

				Application.DoEvents();
			}

			if (this.viewActions.GetItemCount() == 0)
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				this.buttonContinue.Hide();
				this.buttonCancel.Text = "Done";
				this.buttonCancel.DialogResult = DialogResult.OK;
			}
		}
	}
}
