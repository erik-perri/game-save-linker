namespace GameSaveLinker
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CreateLinksButton = new System.Windows.Forms.Button();
			this.RemoveLinksButton = new System.Windows.Forms.Button();
			this.ExitButton = new System.Windows.Forms.Button();
			this.GamesControl = new BrightIdeasSoftware.ObjectListView();
			this.ColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.ColumnSaves = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.ColumnLink = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.HideLinksButton = new System.Windows.Forms.Button();
			this.ShowLinksButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.GamesControl)).BeginInit();
			this.SuspendLayout();
			// 
			// CreateLinksButton
			// 
			this.CreateLinksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CreateLinksButton.Location = new System.Drawing.Point(12, 526);
			this.CreateLinksButton.Name = "CreateLinksButton";
			this.CreateLinksButton.Size = new System.Drawing.Size(85, 23);
			this.CreateLinksButton.TabIndex = 0;
			this.CreateLinksButton.Text = "Create links";
			this.CreateLinksButton.UseVisualStyleBackColor = true;
			this.CreateLinksButton.Click += new System.EventHandler(this.CreateLinksButton_Click);
			// 
			// RemoveLinksButton
			// 
			this.RemoveLinksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RemoveLinksButton.Location = new System.Drawing.Point(103, 526);
			this.RemoveLinksButton.Name = "RemoveLinksButton";
			this.RemoveLinksButton.Size = new System.Drawing.Size(85, 23);
			this.RemoveLinksButton.TabIndex = 1;
			this.RemoveLinksButton.Text = "Remove links";
			this.RemoveLinksButton.UseVisualStyleBackColor = true;
			this.RemoveLinksButton.Click += new System.EventHandler(this.RemoveLinksButton_Click);
			// 
			// ExitButton
			// 
			this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ExitButton.Location = new System.Drawing.Point(412, 526);
			this.ExitButton.Name = "ExitButton";
			this.ExitButton.Size = new System.Drawing.Size(85, 23);
			this.ExitButton.TabIndex = 2;
			this.ExitButton.Text = "Exit";
			this.ExitButton.UseVisualStyleBackColor = true;
			this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
			// 
			// GamesControl
			// 
			this.GamesControl.AllColumns.Add(this.ColumnName);
			this.GamesControl.AllColumns.Add(this.ColumnSaves);
			this.GamesControl.AllColumns.Add(this.ColumnLink);
			this.GamesControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GamesControl.CheckedAspectName = "Checked";
			this.GamesControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnName,
            this.ColumnSaves,
            this.ColumnLink});
			this.GamesControl.Location = new System.Drawing.Point(12, 12);
			this.GamesControl.Name = "GamesControl";
			this.GamesControl.ShowGroups = false;
			this.GamesControl.ShowImagesOnSubItems = true;
			this.GamesControl.Size = new System.Drawing.Size(485, 508);
			this.GamesControl.TabIndex = 6;
			this.GamesControl.UseCompatibleStateImageBehavior = false;
			this.GamesControl.View = System.Windows.Forms.View.Details;
			// 
			// ColumnName
			// 
			this.ColumnName.AspectName = "Name";
			this.ColumnName.CellPadding = null;
			this.ColumnName.Text = "Name";
			this.ColumnName.Width = 300;
			// 
			// ColumnSaves
			// 
			this.ColumnSaves.AspectName = "Saves";
			this.ColumnSaves.CellPadding = null;
			this.ColumnSaves.Text = "Saves Exist";
			this.ColumnSaves.Width = 80;
			// 
			// ColumnLink
			// 
			this.ColumnLink.AspectName = "Linked";
			this.ColumnLink.CellPadding = null;
			this.ColumnLink.Text = "Linked";
			this.ColumnLink.Width = 80;
			// 
			// HideLinksButton
			// 
			this.HideLinksButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.HideLinksButton.Location = new System.Drawing.Point(209, 526);
			this.HideLinksButton.Name = "HideLinksButton";
			this.HideLinksButton.Size = new System.Drawing.Size(85, 23);
			this.HideLinksButton.TabIndex = 7;
			this.HideLinksButton.Text = "Hide links";
			this.HideLinksButton.UseVisualStyleBackColor = true;
			this.HideLinksButton.Click += new System.EventHandler(this.HideLinksButton_Click);
			// 
			// ShowLinksButton
			// 
			this.ShowLinksButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ShowLinksButton.Location = new System.Drawing.Point(300, 526);
			this.ShowLinksButton.Name = "ShowLinksButton";
			this.ShowLinksButton.Size = new System.Drawing.Size(85, 23);
			this.ShowLinksButton.TabIndex = 8;
			this.ShowLinksButton.Text = "Show links";
			this.ShowLinksButton.UseVisualStyleBackColor = true;
			this.ShowLinksButton.Click += new System.EventHandler(this.ShowLinksButton_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(509, 561);
			this.Controls.Add(this.ShowLinksButton);
			this.Controls.Add(this.HideLinksButton);
			this.Controls.Add(this.GamesControl);
			this.Controls.Add(this.ExitButton);
			this.Controls.Add(this.RemoveLinksButton);
			this.Controls.Add(this.CreateLinksButton);
			this.Name = "MainWindow";
			this.Text = "GameSaveLinker";
			((System.ComponentModel.ISupportInitialize)(this.GamesControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button CreateLinksButton;
		private System.Windows.Forms.Button RemoveLinksButton;
		private System.Windows.Forms.Button ExitButton;
		private BrightIdeasSoftware.ObjectListView GamesControl;
		private BrightIdeasSoftware.OLVColumn ColumnName;
		private BrightIdeasSoftware.OLVColumn ColumnSaves;
		private BrightIdeasSoftware.OLVColumn ColumnLink;
		private System.Windows.Forms.Button HideLinksButton;
		private System.Windows.Forms.Button ShowLinksButton;
	}
}

