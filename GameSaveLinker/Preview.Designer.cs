namespace GameSaveLinker
{
	partial class Preview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preview));
			this.imageListPreview = new System.Windows.Forms.ImageList(this.components);
			this.buttonContinue = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.viewActions = new BrightIdeasSoftware.ObjectListView();
			this.columnOrder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.columnType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.columnStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.columnGame = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			((System.ComponentModel.ISupportInitialize)(this.viewActions)).BeginInit();
			this.SuspendLayout();
			// 
			// imageListPreview
			// 
			this.imageListPreview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPreview.ImageStream")));
			this.imageListPreview.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListPreview.Images.SetKeyName(0, "arrow");
			this.imageListPreview.Images.SetKeyName(1, "cross");
			this.imageListPreview.Images.SetKeyName(2, "eye");
			this.imageListPreview.Images.SetKeyName(3, "eye__minus");
			this.imageListPreview.Images.SetKeyName(4, "shortcut");
			this.imageListPreview.Images.SetKeyName(5, "tick");
			// 
			// buttonContinue
			// 
			this.buttonContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonContinue.Location = new System.Drawing.Point(285, 373);
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.Size = new System.Drawing.Size(95, 23);
			this.buttonContinue.TabIndex = 1;
			this.buttonContinue.Text = "&Make Changes";
			this.buttonContinue.UseVisualStyleBackColor = true;
			this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(389, 373);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(95, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// viewActions
			// 
			this.viewActions.AllColumns.Add(this.columnOrder);
			this.viewActions.AllColumns.Add(this.columnType);
			this.viewActions.AllColumns.Add(this.columnStatus);
			this.viewActions.AllColumns.Add(this.columnGame);
			this.viewActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.viewActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnOrder,
            this.columnType,
            this.columnStatus,
            this.columnGame});
			this.viewActions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.viewActions.Location = new System.Drawing.Point(12, 12);
			this.viewActions.Name = "viewActions";
			this.viewActions.ShowImagesOnSubItems = true;
			this.viewActions.Size = new System.Drawing.Size(472, 351);
			this.viewActions.SmallImageList = this.imageListPreview;
			this.viewActions.TabIndex = 0;
			this.viewActions.UseCompatibleStateImageBehavior = false;
			this.viewActions.View = System.Windows.Forms.View.Details;
			// 
			// columnOrder
			// 
			this.columnOrder.AspectName = "OrderAspect";
			this.columnOrder.CellPadding = null;
			this.columnOrder.ImageAspectName = "";
			this.columnOrder.Text = "Order";
			this.columnOrder.Width = 0;
			// 
			// columnType
			// 
			this.columnType.AspectName = "TypeAspect";
			this.columnType.CellPadding = null;
			this.columnType.ImageAspectName = "TypeIconAspect";
			this.columnType.Sortable = false;
			this.columnType.Text = "Type";
			// 
			// columnStatus
			// 
			this.columnStatus.AspectName = "StatusAspect";
			this.columnStatus.CellPadding = null;
			this.columnStatus.ImageAspectName = "StatusIconAspect";
			this.columnStatus.Sortable = false;
			this.columnStatus.Text = "Action";
			this.columnStatus.Width = 550;
			// 
			// columnGame
			// 
			this.columnGame.AspectName = "GameAspect";
			this.columnGame.CellPadding = null;
			this.columnGame.Sortable = false;
			this.columnGame.Width = 0;
			// 
			// Preview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 408);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonContinue);
			this.Controls.Add(this.viewActions);
			this.Name = "Preview";
			this.Text = "Preview Changes";
			((System.ComponentModel.ISupportInitialize)(this.viewActions)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BrightIdeasSoftware.ObjectListView viewActions;
		private System.Windows.Forms.Button buttonContinue;
		private System.Windows.Forms.Button buttonCancel;
		private BrightIdeasSoftware.OLVColumn columnType;
		private BrightIdeasSoftware.OLVColumn columnStatus;
		private BrightIdeasSoftware.OLVColumn columnOrder;
		private System.Windows.Forms.ImageList imageListPreview;
		private BrightIdeasSoftware.OLVColumn columnGame;
	}
}