namespace MusicCollection.Controls
{
    partial class SongControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.selectStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.insertStripButton = new System.Windows.Forms.ToolStripButton();
            this.updateToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSongName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSongId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.deleteToolStripButton});
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
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(596, 382);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridControl1_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 35;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSongName,
            this.colSongId,
            this.colGroupName,
            this.colGroupId});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colSongName
            // 
            this.colSongName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.colSongName.AppearanceHeader.Options.UseFont = true;
            this.colSongName.AppearanceHeader.Options.UseTextOptions = true;
            this.colSongName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSongName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSongName.Caption = "Название песни";
            this.colSongName.FieldName = "SongName";
            this.colSongName.Name = "colSongName";
            this.colSongName.Visible = true;
            this.colSongName.VisibleIndex = 1;
            // 
            // colSongId
            // 
            this.colSongId.AppearanceHeader.Options.UseTextOptions = true;
            this.colSongId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSongId.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSongId.Caption = "ID песни";
            this.colSongId.FieldName = "SongId";
            this.colSongId.Name = "colSongId";
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            this.colGroupName.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupName.Caption = "Название группы";
            this.colGroupName.FieldName = "GroupName";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 2;
            // 
            // colGroupId
            // 
            this.colGroupId.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupId.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupId.Caption = "ID группы";
            this.colGroupId.FieldName = "GroupId";
            this.colGroupId.Name = "colGroupId";
            // 
            // SongControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SongControl";
            this.Size = new System.Drawing.Size(596, 420);
            this.Enter += new System.EventHandler(this.SongControl_Enter);
            this.Leave += new System.EventHandler(this.SongControl_Leave);
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
        private DevExpress.XtraGrid.Columns.GridColumn colSongName;
        private DevExpress.XtraGrid.Columns.GridColumn colSongId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
    }
}
