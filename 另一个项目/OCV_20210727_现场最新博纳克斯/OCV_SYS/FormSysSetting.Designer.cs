namespace OCV
{
    partial class FormSysSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtScanCOM = new System.Windows.Forms.TextBox();
            this.txtEndDataPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.palSourceSys = new System.Windows.Forms.Panel();
            this.chkNgCheck = new System.Windows.Forms.CheckBox();
            this.txtPPV = new System.Windows.Forms.TextBox();
            this.chkPP = new System.Windows.Forms.CheckBox();
            this.chkPassWordLogin = new System.Windows.Forms.CheckBox();
            this.btnProbeRecover = new System.Windows.Forms.Button();
            this.chkCheckFiFO = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNGCheckCount = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDeviceCode = new System.Windows.Forms.TextBox();
            this.txtBatCodeSavePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "扫描枪COM口";
            // 
            // txtScanCOM
            // 
            this.txtScanCOM.Location = new System.Drawing.Point(121, 91);
            this.txtScanCOM.Name = "txtScanCOM";
            this.txtScanCOM.Size = new System.Drawing.Size(138, 21);
            this.txtScanCOM.TabIndex = 2;
            // 
            // txtEndDataPath
            // 
            this.txtEndDataPath.Location = new System.Drawing.Point(120, 36);
            this.txtEndDataPath.Name = "txtEndDataPath";
            this.txtEndDataPath.Size = new System.Drawing.Size(139, 21);
            this.txtEndDataPath.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "结果数据保存路径";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1122, 590);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1120, 342);
            this.panel2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(1122, 242);
            this.splitContainer3.SplitterDistance = 212;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.palSourceSys);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chkNgCheck);
            this.splitContainer2.Panel2.Controls.Add(this.txtPPV);
            this.splitContainer2.Panel2.Controls.Add(this.chkPP);
            this.splitContainer2.Panel2.Controls.Add(this.chkPassWordLogin);
            this.splitContainer2.Panel2.Controls.Add(this.btnProbeRecover);
            this.splitContainer2.Panel2.Controls.Add(this.chkCheckFiFO);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.txtNGCheckCount);
            this.splitContainer2.Panel2.Controls.Add(this.btnSave);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.txtScanCOM);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.txtDeviceCode);
            this.splitContainer2.Panel2.Controls.Add(this.txtBatCodeSavePath);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.txtEndDataPath);
            this.splitContainer2.Size = new System.Drawing.Size(1122, 212);
            this.splitContainer2.SplitterDistance = 669;
            this.splitContainer2.TabIndex = 0;
            // 
            // palSourceSys
            // 
            this.palSourceSys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palSourceSys.Location = new System.Drawing.Point(0, 0);
            this.palSourceSys.Name = "palSourceSys";
            this.palSourceSys.Size = new System.Drawing.Size(667, 210);
            this.palSourceSys.TabIndex = 0;
            // 
            // chkNgCheck
            // 
            this.chkNgCheck.AutoSize = true;
            this.chkNgCheck.Location = new System.Drawing.Point(11, 123);
            this.chkNgCheck.Name = "chkNgCheck";
            this.chkNgCheck.Size = new System.Drawing.Size(102, 16);
            this.chkNgCheck.TabIndex = 18;
            this.chkNgCheck.Text = "开启复测,次数";
            this.chkNgCheck.UseVisualStyleBackColor = true;
            // 
            // txtPPV
            // 
            this.txtPPV.Location = new System.Drawing.Point(121, 147);
            this.txtPPV.Name = "txtPPV";
            this.txtPPV.Size = new System.Drawing.Size(138, 21);
            this.txtPPV.TabIndex = 17;
            this.txtPPV.Validating += new System.ComponentModel.CancelEventHandler(this.txtPPV_Validating);
            // 
            // chkPP
            // 
            this.chkPP.AutoSize = true;
            this.chkPP.Location = new System.Drawing.Point(11, 149);
            this.chkPP.Name = "chkPP";
            this.chkPP.Size = new System.Drawing.Size(114, 16);
            this.chkPP.TabIndex = 16;
            this.chkPP.Text = "开启良率预警(%)";
            this.chkPP.UseVisualStyleBackColor = true;
            // 
            // chkPassWordLogin
            // 
            this.chkPassWordLogin.AutoSize = true;
            this.chkPassWordLogin.Location = new System.Drawing.Point(275, 10);
            this.chkPassWordLogin.Name = "chkPassWordLogin";
            this.chkPassWordLogin.Size = new System.Drawing.Size(96, 16);
            this.chkPassWordLogin.TabIndex = 15;
            this.chkPassWordLogin.Text = "必须密码登录";
            this.chkPassWordLogin.UseVisualStyleBackColor = true;
            // 
            // btnProbeRecover
            // 
            this.btnProbeRecover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProbeRecover.Location = new System.Drawing.Point(226, 173);
            this.btnProbeRecover.Name = "btnProbeRecover";
            this.btnProbeRecover.Size = new System.Drawing.Size(196, 34);
            this.btnProbeRecover.TabIndex = 14;
            this.btnProbeRecover.Text = "探针寿命维护";
            this.btnProbeRecover.UseVisualStyleBackColor = true;
            this.btnProbeRecover.Click += new System.EventHandler(this.btnProbeRecover_Click);
            // 
            // chkCheckFiFO
            // 
            this.chkCheckFiFO.AutoSize = true;
            this.chkCheckFiFO.Location = new System.Drawing.Point(275, 68);
            this.chkCheckFiFO.Name = "chkCheckFiFO";
            this.chkCheckFiFO.Size = new System.Drawing.Size(132, 16);
            this.chkCheckFiFO.TabIndex = 13;
            this.chkCheckFiFO.Text = "是否FIFO进出站校验";
            this.chkCheckFiFO.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "NG复测次数";
            // 
            // txtNGCheckCount
            // 
            this.txtNGCheckCount.Location = new System.Drawing.Point(121, 119);
            this.txtNGCheckCount.Name = "txtNGCheckCount";
            this.txtNGCheckCount.Size = new System.Drawing.Size(138, 21);
            this.txtNGCheckCount.TabIndex = 12;
            this.txtNGCheckCount.TextChanged += new System.EventHandler(this.txtNGCheckCount_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(24, 173);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(196, 34);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(54, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "设备编码";
            // 
            // txtDeviceCode
            // 
            this.txtDeviceCode.Location = new System.Drawing.Point(120, 8);
            this.txtDeviceCode.Name = "txtDeviceCode";
            this.txtDeviceCode.Size = new System.Drawing.Size(139, 21);
            this.txtDeviceCode.TabIndex = 10;
            // 
            // txtBatCodeSavePath
            // 
            this.txtBatCodeSavePath.Location = new System.Drawing.Point(120, 63);
            this.txtBatCodeSavePath.Name = "txtBatCodeSavePath";
            this.txtBatCodeSavePath.Size = new System.Drawing.Size(139, 21);
            this.txtBatCodeSavePath.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "电池码文件路径";
            // 
            // FormSysSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 590);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSysSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSysSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormSysSetting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScanCOM;
        private System.Windows.Forms.TextBox txtEndDataPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDeviceCode;
        private System.Windows.Forms.TextBox txtBatCodeSavePath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel palSourceSys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNGCheckCount;
        private System.Windows.Forms.CheckBox chkCheckFiFO;
        private System.Windows.Forms.Button btnProbeRecover;
        private System.Windows.Forms.TextBox txtPPV;
        private System.Windows.Forms.CheckBox chkPP;
        private System.Windows.Forms.CheckBox chkPassWordLogin;
        private System.Windows.Forms.CheckBox chkNgCheck;
    }
}