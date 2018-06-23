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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(778, 28);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(376, 336);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 7;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.axMapControl1.Location = new System.Drawing.Point(327, 56);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(451, 450);
            this.axMapControl1.TabIndex = 6;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 56);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(321, 450);
            this.axTOCControl1.TabIndex = 5;
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl2.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
            this.axToolbarControl2.Size = new System.Drawing.Size(778, 28);
            this.axToolbarControl2.TabIndex = 3;
            // 
            // FormContour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 506);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axToolbarControl2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormContour";
            this.Text = "FormContour";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormContour_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
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
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 原始测量文件ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;

    }
}