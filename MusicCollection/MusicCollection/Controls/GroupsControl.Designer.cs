namespace MusicCollection.Controls
{
    partial class GroupsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupsControl));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colgroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colupdateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colgroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.seectStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.insertStripButton = new System.Windows.Forms.ToolStripButton();
            this.updateToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(596, 382);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridControl1_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 35;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colgroupName,
            this.colupdateTime,
            this.colgroupId});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colgroupName
            // 
            this.colgroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.colgroupName.AppearanceHeader.Options.UseFont = true;
            this.colgroupName.AppearanceHeader.Options.UseTextOptions = true;
            this.colgroupName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colgroupName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colgroupName.Caption = "Имя группы";
            this.colgroupName.FieldName = "GroupName";
            this.colgroupName.Name = "colgroupName";
            this.colgroupName.Visible = true;
            this.colgroupName.VisibleIndex = 1;
            this.colgroupName.Width = 354;
            // 
            // colupdateTime
            // 
            this.colupdateTime.AppearanceCell.Options.UseTextOptions = true;
            this.colupdateTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colupdateTime.AppearanceHeader.Options.UseTextOptions = true;
            this.colupdateTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colupdateTime.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colupdateTime.Caption = "Время изменения";
            this.colupdateTime.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            this.colupdateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colupdateTime.FieldName = "UpdateTime";
            this.colupdateTime.Name = "colupdateTime";
            this.colupdateTime.Width = 120;
            // 
            // colgroupId
            // 
            this.colgroupId.AppearanceHeader.Options.UseTextOptions = true;
            this.colgroupId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colgroupId.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colgroupId.Caption = "ID Группы";
            this.colgroupId.FieldName = "GroupId";
            this.colgroupId.Name = "colgroupId";
            this.colgroupId.Width = 104;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seectStripButton,
            this.toolStripSeparator1,
            this.insertStripButton,
            this.updateToolStripButton,
            this.deleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(596, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // seectStripButton
            // 
            this.seectStripButton.Image = ((System.Drawing.Image)(resources.GetObject("seectStripButton.Image")));
            this.seectStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.seectStripButton.Name = "seectStripButton";
            this.seectStripButton.Size = new System.Drawing.Size(65, 35);
            this.seectStripButton.Text = "Обновить";
            this.seectStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.seectStripButton.Click += new System.EventHandler(this.SelectClick);
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
            this.insertStripButton.Click += new System.EventHandler(this.InsertClick);
            // 
            // updateToolStripButton
            // 
            this.updateToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("updateToolStripButton.Image")));
            this.updateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateToolStripButton.Name = "updateToolStripButton";
            this.updateToolStripButton.Size = new System.Drawing.Size(65, 35);
            this.updateToolStripButton.Text = "Изменить";
            this.updateToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.updateToolStripButton.Click += new System.EventHandler(this.UpdateClick);
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
            // GroupsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "GroupsControl";
            this.Size = new System.Drawing.Size(596, 420);
            this.Enter += new System.EventHandler(this.GroupsControl_Enter);
            this.Leave += new System.EventHandler(this.GroupsControl_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colgroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colgroupName;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton insertStripButton;
        private System.Windows.Forms.ToolStripButton updateToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripButton seectStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraGrid.Columns.GridColumn colupdateTime;
    }
}
