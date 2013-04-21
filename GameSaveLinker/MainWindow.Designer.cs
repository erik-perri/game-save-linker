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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.buttonCreateLinks = new System.Windows.Forms.Button();
			this.buttonRemoveLinks = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.contextGames = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.checkSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uncheckSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonHideLinks = new System.Windows.Forms.Button();
			this.buttonShowLinks = new System.Windows.Forms.Button();
			this.imageListGames = new System.Windows.Forms.ImageList(this.components);
			this.viewGames = new BrightIdeasSoftware.ObjectListView();
			this.columnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.columnSaves = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.columnLink = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.contextGames.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewGames)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonCreateLinks
			// 
			this.buttonCreateLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCreateLinks.Location = new System.Drawing.Point(12, 439);
			this.buttonCreateLinks.Name = "buttonCreateLinks";
			this.buttonCreateLinks.Size = new System.Drawing.Size(95, 23);
			this.buttonCreateLinks.TabIndex = 0;
			this.buttonCreateLinks.Text = "Move to &Storage";
			this.buttonCreateLinks.UseVisualStyleBackColor = true;
			this.buttonCreateLinks.Click += new System.EventHandler(this.buttonCreateLinks_Click);
			// 
			// buttonRemoveLinks
			// 
			this.buttonRemoveLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemoveLinks.Location = new System.Drawing.Point(116, 439);
			this.buttonRemoveLinks.Name = "buttonRemoveLinks";
			this.buttonRemoveLinks.Size = new System.Drawing.Size(95, 23);
			this.buttonRemoveLinks.TabIndex = 1;
			this.buttonRemoveLinks.Text = "Move to &Original";
			this.buttonRemoveLinks.UseVisualStyleBackColor = true;
			this.buttonRemoveLinks.Click += new System.EventHandler(this.buttonRemoveLinks_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExit.Location = new System.Drawing.Point(412, 439);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(85, 23);
			this.buttonExit.TabIndex = 2;
			this.buttonExit.Text = "Exit";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// contextGames
			// 
			this.contextGames.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkSelectedToolStripMenuItem,
            this.uncheckSelectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.checkAllToolStripMenuItem,
            this.checkNoneToolStripMenuItem});
			this.contextGames.Name = "contextGames";
			this.contextGames.Size = new System.Drawing.Size(168, 98);
			this.contextGames.Opening += new System.ComponentModel.CancelEventHandler(this.contextGames_Opening);
			// 
			// checkSelectedToolStripMenuItem
			// 
			this.checkSelectedToolStripMenuItem.Name = "checkSelectedToolStripMenuItem";
			this.checkSelectedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.checkSelectedToolStripMenuItem.Text = "&Check Selected";
			this.checkSelectedToolStripMenuItem.Click += new System.EventHandler(this.checkSelectedToolStripMenuItem_Click);
			// 
			// uncheckSelectedToolStripMenuItem
			// 
			this.uncheckSelectedToolStripMenuItem.Name = "uncheckSelectedToolStripMenuItem";
			this.uncheckSelectedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.uncheckSelectedToolStripMenuItem.Text = "&Uncheck Selected";
			this.uncheckSelectedToolStripMenuItem.Click += new System.EventHandler(this.uncheckSelectedToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
			// 
			// checkAllToolStripMenuItem
			// 
			this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
			this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.checkAllToolStripMenuItem.Text = "Check &All";
			this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
			// 
			// checkNoneToolStripMenuItem
			// 
			this.checkNoneToolStripMenuItem.Name = "checkNoneToolStripMenuItem";
			this.checkNoneToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.checkNoneToolStripMenuItem.Text = "Uncheck A&ll";
			this.checkNoneToolStripMenuItem.Click += new System.EventHandler(this.checkNoneToolStripMenuItem_Click);
			// 
			// buttonHideLinks
			// 
			this.buttonHideLinks.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonHideLinks.Location = new System.Drawing.Point(231, 439);
			this.buttonHideLinks.Name = "buttonHideLinks";
			this.buttonHideLinks.Size = new System.Drawing.Size(75, 23);
			this.buttonHideLinks.TabIndex = 7;
			this.buttonHideLinks.Text = "Hide links";
			this.buttonHideLinks.UseVisualStyleBackColor = true;
			this.buttonHideLinks.Click += new System.EventHandler(this.buttonHideLinks_Click);
			// 
			// buttonShowLinks
			// 
			this.buttonShowLinks.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonShowLinks.Location = new System.Drawing.Point(315, 439);
			this.buttonShowLinks.Name = "buttonShowLinks";
			this.buttonShowLinks.Size = new System.Drawing.Size(75, 23);
			this.buttonShowLinks.TabIndex = 8;
			this.buttonShowLinks.Text = "Show links";
			this.buttonShowLinks.UseVisualStyleBackColor = true;
			this.buttonShowLinks.Click += new System.EventHandler(this.buttonShowLinks_Click);
			// 
			// imageListGames
			// 
			this.imageListGames.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGames.ImageStream")));
			this.imageListGames.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListGames.Images.SetKeyName(0, "cross");
			this.imageListGames.Images.SetKeyName(1, "exclamation");
			this.imageListGames.Images.SetKeyName(2, "exclamation_red");
			this.imageListGames.Images.SetKeyName(3, "tick");
			// 
			// viewGames
			// 
			this.viewGames.AllColumns.Add(this.columnName);
			this.viewGames.AllColumns.Add(this.columnSaves);
			this.viewGames.AllColumns.Add(this.columnLink);
			this.viewGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.viewGames.CheckBoxes = true;
			this.viewGames.CheckedAspectName = "Checked";
			this.viewGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnSaves,
            this.columnLink});
			this.viewGames.ContextMenuStrip = this.contextGames;
			this.viewGames.Location = new System.Drawing.Point(12, 12);
			this.viewGames.Name = "viewGames";
			this.viewGames.ShowGroups = false;
			this.viewGames.ShowImagesOnSubItems = true;
			this.viewGames.Size = new System.Drawing.Size(485, 417);
			this.viewGames.SmallImageList = this.imageListGames;
			this.viewGames.TabIndex = 6;
			this.viewGames.UseCompatibleStateImageBehavior = false;
			this.viewGames.View = System.Windows.Forms.View.Details;
			// 
			// columnName
			// 
			this.columnName.AspectName = "Name";
			this.columnName.CellPadding = null;
			this.columnName.CheckBoxes = true;
			this.columnName.Text = "Name";
			this.columnName.Width = 300;
			// 
			// columnSaves
			// 
			this.columnSaves.AspectName = "SavesAspect";
			this.columnSaves.CellPadding = null;
			this.columnSaves.ImageAspectName = "SavesIconAspect";
			this.columnSaves.Text = "Saves Exist";
			this.columnSaves.Width = 80;
			// 
			// columnLink
			// 
			this.columnLink.AspectName = "LinkedAspect";
			this.columnLink.CellPadding = null;
			this.columnLink.ImageAspectName = "LinkedIconAspect";
			this.columnLink.Text = "Linked";
			this.columnLink.Width = 80;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(509, 474);
			this.Controls.Add(this.buttonShowLinks);
			this.Controls.Add(this.buttonHideLinks);
			this.Controls.Add(this.viewGames);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonRemoveLinks);
			this.Controls.Add(this.buttonCreateLinks);
			this.Name = "MainWindow";
			this.Text = "GameSaveLinker";
			this.contextGames.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.viewGames)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonCreateLinks;
		private System.Windows.Forms.Button buttonRemoveLinks;
		private System.Windows.Forms.Button buttonExit;
		private BrightIdeasSoftware.ObjectListView viewGames;
		private BrightIdeasSoftware.OLVColumn columnName;
		private BrightIdeasSoftware.OLVColumn columnSaves;
		private BrightIdeasSoftware.OLVColumn columnLink;
		private System.Windows.Forms.Button buttonHideLinks;
		private System.Windows.Forms.Button buttonShowLinks;
		private System.Windows.Forms.ContextMenuStrip contextGames;
		private System.Windows.Forms.ToolStripMenuItem checkSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uncheckSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkNoneToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ImageList imageListGames;
	}
}

