namespace ArcEngineProgram
{
    partial class FormSurvey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSurvey));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载Shp文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();

            this.选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择测区范围ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加控制点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换控制点符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.删除控制点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标添加控制点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.选择ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.查询ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1168, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载Shp文件ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 加载Shp文件ToolStripMenuItem
            // 
            this.加载Shp文件ToolStripMenuItem.Name = "加载Shp文件ToolStripMenuItem";
            this.加载Shp文件ToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.加载Shp文件ToolStripMenuItem.Text = "添加shp文件";
            this.加载Shp文件ToolStripMenuItem.Click += new System.EventHandler(this.加载Shp文件ToolStripMenuItem_Click);
            // 

            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // axTOCControl1

            // 选择ToolStripMenuItem

            // 
            this.选择ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择测区范围ToolStripMenuItem});
            this.选择ToolStripMenuItem.Name = "选择ToolStripMenuItem";
            this.选择ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.选择ToolStripMenuItem.Text = "选择";
            // 
            // 选择测区范围ToolStripMenuItem
            // 
            this.选择测区范围ToolStripMenuItem.CheckOnClick = true;
            this.选择测区范围ToolStripMenuItem.Name = "选择测区范围ToolStripMenuItem";
            this.选择测区范围ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.选择测区范围ToolStripMenuItem.Text = "选择测区范围";
            this.选择测区范围ToolStripMenuItem.Click += new System.EventHandler(this.选择测区范围ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(244, 564);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(919, 231);
            this.dataGridView1.TabIndex = 8;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(1104, 71);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除图层ToolStripMenuItem,
            this.图层上移ToolStripMenuItem,
            this.图层下移ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 76);
            // 
            // 删除图层ToolStripMenuItem
            // 
            this.删除图层ToolStripMenuItem.Name = "删除图层ToolStripMenuItem";
            this.删除图层ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.删除图层ToolStripMenuItem.Text = "删除图层";
            this.删除图层ToolStripMenuItem.Click += new System.EventHandler(this.删除图层ToolStripMenuItem_Click);
            // 
            // 图层上移ToolStripMenuItem
            // 
            this.图层上移ToolStripMenuItem.Name = "图层上移ToolStripMenuItem";
            this.图层上移ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.图层上移ToolStripMenuItem.Text = "图层上移";
            this.图层上移ToolStripMenuItem.Click += new System.EventHandler(this.图层上移ToolStripMenuItem_Click);
            // 
            // 图层下移ToolStripMenuItem
            // 
            this.图层下移ToolStripMenuItem.Name = "图层下移ToolStripMenuItem";
            this.图层下移ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.图层下移ToolStripMenuItem.Text = "图层下移";
            this.图层下移ToolStripMenuItem.Click += new System.EventHandler(this.图层下移ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加控制点ToolStripMenuItem,
            this.坐标添加控制点ToolStripMenuItem,
            this.删除控制点ToolStripMenuItem,
            this.更换控制点符号ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 添加控制点ToolStripMenuItem
            // 
            this.添加控制点ToolStripMenuItem.CheckOnClick = true;
            this.添加控制点ToolStripMenuItem.Name = "添加控制点ToolStripMenuItem";
            this.添加控制点ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.添加控制点ToolStripMenuItem.Text = "添加控制点";
            this.添加控制点ToolStripMenuItem.Click += new System.EventHandler(this.添加控制点ToolStripMenuItem_Click);
            // 
            // 更换控制点符号ToolStripMenuItem
            // 
            this.更换控制点符号ToolStripMenuItem.Name = "更换控制点符号ToolStripMenuItem";
            this.更换控制点符号ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.更换控制点符号ToolStripMenuItem.Text = "更换控制点符号";
            this.更换控制点符号ToolStripMenuItem.Click += new System.EventHandler(this.更换控制点符号ToolStripMenuItem_Click);
            // 
            // axMapControl2
            // 
            this.axMapControl2.Location = new System.Drawing.Point(0, 563);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(238, 232);
            this.axMapControl2.TabIndex = 7;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1168, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(0, 61);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(238, 496);
            this.axTOCControl1.TabIndex = 2;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCControl1_OnMouseUp);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(244, 61);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(919, 496);
            this.axMapControl1.TabIndex = 1;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            // 
            // 删除控制点ToolStripMenuItem
            // 
            this.删除控制点ToolStripMenuItem.Name = "删除控制点ToolStripMenuItem";
            this.删除控制点ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.删除控制点ToolStripMenuItem.Text = "删除控制点";
            this.删除控制点ToolStripMenuItem.Click += new System.EventHandler(this.删除控制点ToolStripMenuItem_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.查询ToolStripMenuItem.Text = "查询";
            // 
            // 坐标添加控制点ToolStripMenuItem
            // 
            this.坐标添加控制点ToolStripMenuItem.Name = "坐标添加控制点ToolStripMenuItem";
            this.坐标添加控制点ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.坐标添加控制点ToolStripMenuItem.Text = "坐标添加控制点";
            this.坐标添加控制点ToolStripMenuItem.Click += new System.EventHandler(this.坐标添加控制点ToolStripMenuItem_Click);
            // 
            // FormSurvey
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1168, 798);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.axMapControl2);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormSurvey";
            this.Text = "FormSurvey";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载Shp文件ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择测区范围ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加控制点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换控制点符号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除控制点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 坐标添加控制点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
    }
}