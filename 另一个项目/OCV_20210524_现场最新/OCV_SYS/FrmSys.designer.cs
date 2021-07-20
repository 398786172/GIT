namespace OCV
{
    partial class FrmSys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSys));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Master设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.补偿设置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.运行模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoOCVTest = new System.Windows.Forms.RadioButton();
            this.rdoMonitor = new System.Windows.Forms.RadioButton();
            this.lblOCV_Num = new System.Windows.Forms.Label();
            this.lblTestType = new System.Windows.Forms.Label();
            this.lblTrayType = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbRunMode = new System.Windows.Forms.Label();
            this.lblBatteryType = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.运行模式ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem,
            this.toolStripSeparator1,
            this.Master设置ToolStripMenuItem,
            this.补偿设置ToolStripMenuItem1,
            this.toolStripSeparator2});
            this.设置ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // Master设置ToolStripMenuItem
            // 
            this.Master设置ToolStripMenuItem.Name = "Master设置ToolStripMenuItem";
            this.Master设置ToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.Master设置ToolStripMenuItem.Text = "OCV Master设置";
            this.Master设置ToolStripMenuItem.Click += new System.EventHandler(this.校准设置ToolStripMenuItem_Click);
            // 
            // 补偿设置ToolStripMenuItem1
            // 
            this.补偿设置ToolStripMenuItem1.Name = "补偿设置ToolStripMenuItem1";
            this.补偿设置ToolStripMenuItem1.Size = new System.Drawing.Size(205, 26);
            this.补偿设置ToolStripMenuItem1.Text = "内阻补偿设置";
            this.补偿设置ToolStripMenuItem1.Click += new System.EventHandler(this.校准值设置ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            // 
            // 运行模式ToolStripMenuItem
            // 
            this.运行模式ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.运行模式ToolStripMenuItem.Name = "运行模式ToolStripMenuItem";
            this.运行模式ToolStripMenuItem.Size = new System.Drawing.Size(86, 25);
            this.运行模式ToolStripMenuItem.Text = "运行模式";
            this.运行模式ToolStripMenuItem.Click += new System.EventHandler(this.运行模式ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.rdoOCVTest);
            this.panel1.Controls.Add(this.rdoMonitor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 624);
            this.panel1.TabIndex = 2;
            // 
            // rdoOCVTest
            // 
            this.rdoOCVTest.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoOCVTest.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoOCVTest.Location = new System.Drawing.Point(3, 122);
            this.rdoOCVTest.Name = "rdoOCVTest";
            this.rdoOCVTest.Size = new System.Drawing.Size(95, 46);
            this.rdoOCVTest.TabIndex = 1;
            this.rdoOCVTest.TabStop = true;
            this.rdoOCVTest.Text = "OCV测试";
            this.rdoOCVTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoOCVTest.UseVisualStyleBackColor = true;
            this.rdoOCVTest.CheckedChanged += new System.EventHandler(this.rdoOCVTest_CheckedChanged);
            // 
            // rdoMonitor
            // 
            this.rdoMonitor.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoMonitor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoMonitor.Location = new System.Drawing.Point(3, 58);
            this.rdoMonitor.Name = "rdoMonitor";
            this.rdoMonitor.Size = new System.Drawing.Size(95, 46);
            this.rdoMonitor.TabIndex = 0;
            this.rdoMonitor.TabStop = true;
            this.rdoMonitor.Text = "实时监控";
            this.rdoMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoMonitor.UseVisualStyleBackColor = true;
            this.rdoMonitor.CheckedChanged += new System.EventHandler(this.rdoMonitor_CheckedChanged);
            // 
            // lblOCV_Num
            // 
            this.lblOCV_Num.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblOCV_Num.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOCV_Num.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOCV_Num.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblOCV_Num.Location = new System.Drawing.Point(156, 2);
            this.lblOCV_Num.Name = "lblOCV_Num";
            this.lblOCV_Num.Size = new System.Drawing.Size(97, 24);
            this.lblOCV_Num.TabIndex = 162;
            this.lblOCV_Num.Text = "OCV_Num";
            this.lblOCV_Num.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestType
            // 
            this.lblTestType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblTestType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTestType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTestType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTestType.Location = new System.Drawing.Point(362, 2);
            this.lblTestType.Name = "lblTestType";
            this.lblTestType.Size = new System.Drawing.Size(97, 24);
            this.lblTestType.TabIndex = 161;
            this.lblTestType.Text = "TestType";
            this.lblTestType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrayType
            // 
            this.lblTrayType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblTrayType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTrayType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTrayType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTrayType.Location = new System.Drawing.Point(259, 2);
            this.lblTrayType.Name = "lblTrayType";
            this.lblTrayType.Size = new System.Drawing.Size(97, 24);
            this.lblTrayType.TabIndex = 160;
            this.lblTrayType.Text = "TrayType";
            this.lblTrayType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.lbRunMode);
            this.panel2.Controls.Add(this.lblOCV_Num);
            this.panel2.Controls.Add(this.lblTrayType);
            this.panel2.Controls.Add(this.lblBatteryType);
            this.panel2.Controls.Add(this.lblTestType);
            this.panel2.Location = new System.Drawing.Point(383, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(839, 26);
            this.panel2.TabIndex = 163;
            // 
            // lbRunMode
            // 
            this.lbRunMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbRunMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbRunMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRunMode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRunMode.Location = new System.Drawing.Point(24, 2);
            this.lbRunMode.Name = "lbRunMode";
            this.lbRunMode.Size = new System.Drawing.Size(126, 24);
            this.lbRunMode.TabIndex = 163;
            this.lbRunMode.Text = "RunMode";
            this.lbRunMode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBatteryType
            // 
            this.lblBatteryType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblBatteryType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBatteryType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBatteryType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblBatteryType.Location = new System.Drawing.Point(465, 2);
            this.lblBatteryType.Name = "lblBatteryType";
            this.lblBatteryType.Size = new System.Drawing.Size(97, 24);
            this.lblBatteryType.TabIndex = 161;
            this.lblBatteryType.Text = "BatType";
            this.lblBatteryType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblBatteryType.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel3.Location = new System.Drawing.Point(106, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(914, 621);
            this.panel3.TabIndex = 165;
            // 
            // FrmSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 653);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmSys";
            this.Text = "OCV测试";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSys_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSys_FormClosed);
            this.Load += new System.EventHandler(this.FrmSys_Load);
            this.Resize += new System.EventHandler(this.FrmSys_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoOCVTest;
        private System.Windows.Forms.RadioButton rdoMonitor;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Master设置ToolStripMenuItem;
        private System.Windows.Forms.Label lblOCV_Num;
        private System.Windows.Forms.Label lblTestType;
        private System.Windows.Forms.Label lblTrayType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.Label lbRunMode;
        private System.Windows.Forms.ToolStripMenuItem 补偿设置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label lblBatteryType;
        private System.Windows.Forms.Panel panel3;
    }
}