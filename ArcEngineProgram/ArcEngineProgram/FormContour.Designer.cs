namespace ArcEngineProgram
{
    partial class FormContour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormContour));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.原始测量文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.建立TINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成等高线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Map = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.axLicenseControl2 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.计算测区范围ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.Map.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.建立TINToolStripMenuItem,
            this.生成等高线ToolStripMenuItem,
            this.计算测区范围ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(983, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.原始测量文件ToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // 原始测量文件ToolStripMenuItem
            // 
            this.原始测量文件ToolStripMenuItem.Name = "原始测量文件ToolStripMenuItem";
            this.原始测量文件ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.原始测量文件ToolStripMenuItem.Text = "原始测量文件";
            this.原始测量文件ToolStripMenuItem.Click += new System.EventHandler(this.原始测量文件ToolStripMenuItem_Click);
            // 
            // 建立TINToolStripMenuItem
            // 
            this.建立TINToolStripMenuItem.Name = "建立TINToolStripMenuItem";
            this.建立TINToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.建立TINToolStripMenuItem.Text = "建立TIN";
            this.建立TINToolStripMenuItem.Click += new System.EventHandler(this.建立TINToolStripMenuItem_Click);
            // 
            // 生成等高线ToolStripMenuItem
            // 
            this.生成等高线ToolStripMenuItem.Name = "生成等高线ToolStripMenuItem";
            this.生成等高线ToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.生成等高线ToolStripMenuItem.Text = "生成等高线";
            this.生成等高线ToolStripMenuItem.Click += new System.EventHandler(this.生成等高线ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 571);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(983, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 19);
            this.toolStripProgressBar1.Step = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Map
            // 
            this.Map.Controls.Add(this.tabPage1);
            this.Map.Controls.Add(this.tabPage2);
            this.Map.Controls.Add(this.tabPage3);
            this.Map.Location = new System.Drawing.Point(297, 56);
            this.Map.Name = "Map";
            this.Map.SelectedIndex = 0;
            this.Map.Size = new System.Drawing.Size(686, 512);
            this.Map.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Map.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axLicenseControl1);
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(678, 483);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Map";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(85, 296);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(678, 483);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Property Table";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(672, 477);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.axLicenseControl2);
            this.tabPage3.Controls.Add(this.axSceneControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(678, 483);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TIN";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // axLicenseControl2
            // 
            this.axLicenseControl2.Enabled = true;
            this.axLicenseControl2.Location = new System.Drawing.Point(249, 213);
            this.axLicenseControl2.Name = "axLicenseControl2";
            this.axLicenseControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl2.OcxState")));
            this.axLicenseControl2.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl2.TabIndex = 1;
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSceneControl1.Location = new System.Drawing.Point(3, 3);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(672, 477);
            this.axSceneControl1.TabIndex = 0;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(672, 477);
            this.axMapControl1.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(0, 56);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(291, 512);
            this.axTOCControl1.TabIndex = 5;
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl2.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
            this.axToolbarControl2.Size = new System.Drawing.Size(983, 28);
            this.axToolbarControl2.TabIndex = 3;
            // 
            // 计算测区范围ToolStripMenuItem
            // 
            this.计算测区范围ToolStripMenuItem.Name = "计算测区范围ToolStripMenuItem";
            this.计算测区范围ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.计算测区范围ToolStripMenuItem.Text = "计算测区范围";
            this.计算测区范围ToolStripMenuItem.Click += new System.EventHandler(this.计算测区范围ToolStripMenuItem_Click);
            // 
            // FormContour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 596);
            this.Controls.Add(this.Map);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axToolbarControl2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormContour";
            this.Text = " ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Map.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 原始测量文件ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem 建立TINToolStripMenuItem;
        private System.Windows.Forms.TabControl Map;
        private System.Windows.Forms.TabPage tabPage1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl2;
        private System.Windows.Forms.ToolStripMenuItem 生成等高线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算测区范围ToolStripMenuItem;

    }
}