namespace MusicCollection.Controls
{
    partial class AlbumControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.selectStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.insertStripButton = new System.Windows.Forms.ToolStripButton();
            this.updateToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.songslStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ActiveAlbumsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAlbumId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlbumName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectStripButton,
            this.toolStripSeparator1,
            this.insertStripButton,
            this.updateToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator2,
            this.songslStripButton1,
            this.toolStripSeparator3,
            this.ActiveAlbumsDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(596, 38);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // selectStripButton
            // 
            this.selectStripButton.Image = ((System.Drawing.Image)(resources.GetObject("selectStripButton.Image")));
            this.selectStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectStripButton.Name = "selectStripButton";
            this.selectStripButton.Size = new System.Drawing.Size(65, 35);
            this.selectStripButton.Text = "Обновить";
            this.selectStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.selectStripButton.Click += new System.EventHandler(this.selectStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // insertStripButton
            // 
            this.insertStripButton.Image = ((System.Drawing.Image)(resources.GetObject("insertStripButton.Image")));
            this.insertStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertStripButton.Name = "insertStripButton";
            this.insertStripButton.Size = new System.Drawing.Size(63, 35);
            this.insertStripButton.Text = "Добавить";
            this.insertStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.insertStripButton.Click += new System.EventHandler(this.insertStripButton_Click);
            // 
            // updateToolStripButton
            // 
            this.updateToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("updateToolStripButton.Image")));
            this.updateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateToolStripButton.Name = "updateToolStripButton";
            this.updateToolStripButton.Size = new System.Drawing.Size(65, 35);
            this.updateToolStripButton.Text = "Изменить";
            this.updateToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.updateToolStripButton.Click += new System.EventHandler(this.updateToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(55, 35);
            this.deleteToolStripButton.Text = "Удалить";
            this.deleteToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // songslStripButton1
            // 
            this.songslStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("songslStripButton1.Image")));
            this.songslStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.songslStripButton1.Name = "songslStripButton1";
            this.songslStripButton1.Size = new System.Drawing.Size(46, 35);
            this.songslStripButton1.Text = "Песни";
            this.songslStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.songslStripButton1.Click += new System.EventHandler(this.songslStripButton1_Click);
            // 
            // ActiveAlbumsDropDownButton
            // 
            this.ActiveAlbumsDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ActiveAlbumsDropDownButton.Image")));
            this.ActiveAlbumsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ActiveAlbumsDropDownButton.Name = "ActiveAlbumsDropDownButton";
            this.ActiveAlbumsDropDownButton.Size = new System.Drawing.Size(132, 35);
            this.ActiveAlbumsDropDownButton.Text = "Открытые Альбомы";
            this.ActiveAlbumsDropDownButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(596, 344);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridControl1_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 35;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAlbumId,
            this.colAlbumName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colAlbumId
            // 
            this.colAlbumId.Caption = "ID Альбома";
            this.colAlbumId.FieldName = "AlbumId";
            this.colAlbumId.Name = "colAlbumId";
            this.colAlbumId.Width = 50;
            // 
            // colAlbumName
            // 
            this.colAlbumName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.colAlbumName.AppearanceHeader.Options.UseFont = true;
            this.colAlbumName.AppearanceHeader.Options.UseTextOptions = true;
            this.colAlbumName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlbumName.Caption = "Название альбома";
            this.colAlbumName.FieldName = "AlbumName";
            this.colAlbumName.Name = "colAlbumName";
            this.colAlbumName.Visible = true;
            this.colAlbumName.VisibleIndex = 1;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // AlbumControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AlbumControl";
            this.Size = new System.Drawing.Size(596, 382);
            this.Enter += new System.EventHandler(this.AlbumControl_Enter);
            this.Leave += new System.EventHandler(this.AlbumControl_Leave);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton selectStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton insertStripButton;
        private System.Windows.Forms.ToolStripButton updateToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colAlbumId;
        private DevExpress.XtraGrid.Columns.GridColumn colAlbumName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton songslStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton ActiveAlbumsDropDownButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
