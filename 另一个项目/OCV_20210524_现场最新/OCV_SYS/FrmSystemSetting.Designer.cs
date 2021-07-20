namespace OCV
{
    partial class FrmSystemSetting
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
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUSBAddr = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoTray256CH = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbDeviceNo = new System.Windows.Forms.ComboBox();
            this.cmbCOM_RT = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoIRSpeedMid = new System.Windows.Forms.RadioButton();
            this.rdoIRSpeedLow = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCOM_SW = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSave.Location = new System.Drawing.Point(243, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUSBAddr);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(12, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 63);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "万用表";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 176;
            this.label5.Text = "USB通讯名";
            // 
            // txtUSBAddr
            // 
            this.txtUSBAddr.Location = new System.Drawing.Point(95, 28);
            this.txtUSBAddr.Name = "txtUSBAddr";
            this.txtUSBAddr.Size = new System.Drawing.Size(104, 26);
            this.txtUSBAddr.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoTray256CH);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(12, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 53);
            this.groupBox2.TabIndex = 175;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "托盘类型";
            // 
            // rdoTray256CH
            // 
            this.rdoTray256CH.AutoSize = true;
            this.rdoTray256CH.Checked = true;
            this.rdoTray256CH.Location = new System.Drawing.Point(21, 31);
            this.rdoTray256CH.Name = "rdoTray256CH";
            this.rdoTray256CH.Size = new System.Drawing.Size(82, 20);
            this.rdoTray256CH.TabIndex = 0;
            this.rdoTray256CH.TabStop = true;
            this.rdoTray256CH.Text = "256通道";
            this.rdoTray256CH.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbDeviceNo);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox6.Location = new System.Drawing.Point(76, 359);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(155, 70);
            this.groupBox6.TabIndex = 180;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "设备序号";
            // 
            // cmbDeviceNo
            // 
            this.cmbDeviceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceNo.FormattingEnabled = true;
            this.cmbDeviceNo.Location = new System.Drawing.Point(22, 31);
            this.cmbDeviceNo.Name = "cmbDeviceNo";
            this.cmbDeviceNo.Size = new System.Drawing.Size(70, 24);
            this.cmbDeviceNo.TabIndex = 180;
            // 
            // cmbCOM_RT
            // 
            this.cmbCOM_RT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_RT.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbCOM_RT.FormattingEnabled = true;
            this.cmbCOM_RT.Location = new System.Drawing.Point(81, 18);
            this.cmbCOM_RT.Name = "cmbCOM_RT";
            this.cmbCOM_RT.Size = new System.Drawing.Size(80, 27);
            this.cmbCOM_RT.TabIndex = 182;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.rdoIRSpeedMid);
            this.groupBox8.Controls.Add(this.rdoIRSpeedLow);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.cmbCOM_RT);
            this.groupBox8.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox8.Location = new System.Drawing.Point(247, 65);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(193, 75);
            this.groupBox8.TabIndex = 183;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "内阻仪";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 189;
            this.label1.Text = "测速:";
            // 
            // rdoIRSpeedMid
            // 
            this.rdoIRSpeedMid.AutoSize = true;
            this.rdoIRSpeedMid.Location = new System.Drawing.Point(128, 52);
            this.rdoIRSpeedMid.Margin = new System.Windows.Forms.Padding(2);
            this.rdoIRSpeedMid.Name = "rdoIRSpeedMid";
            this.rdoIRSpeedMid.Size = new System.Drawing.Size(58, 20);
            this.rdoIRSpeedMid.TabIndex = 188;
            this.rdoIRSpeedMid.TabStop = true;
            this.rdoIRSpeedMid.Text = "中速";
            this.rdoIRSpeedMid.UseVisualStyleBackColor = true;
            // 
            // rdoIRSpeedLow
            // 
            this.rdoIRSpeedLow.AutoSize = true;
            this.rdoIRSpeedLow.Location = new System.Drawing.Point(68, 51);
            this.rdoIRSpeedLow.Margin = new System.Windows.Forms.Padding(2);
            this.rdoIRSpeedLow.Name = "rdoIRSpeedLow";
            this.rdoIRSpeedLow.Size = new System.Drawing.Size(58, 20);
            this.rdoIRSpeedLow.TabIndex = 183;
            this.rdoIRSpeedLow.TabStop = true;
            this.rdoIRSpeedLow.Text = "慢速";
            this.rdoIRSpeedLow.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 176;
            this.label6.Text = "串口号";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label7);
            this.groupBox10.Controls.Add(this.cmbCOM_SW);
            this.groupBox10.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox10.Location = new System.Drawing.Point(446, 70);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(193, 69);
            this.groupBox10.TabIndex = 187;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "通道切换系统";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 176;
            this.label7.Text = "串口号";
            // 
            // cmbCOM_SW
            // 
            this.cmbCOM_SW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_SW.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbCOM_SW.FormattingEnabled = true;
            this.cmbCOM_SW.Location = new System.Drawing.Point(81, 26);
            this.cmbCOM_SW.Name = "cmbCOM_SW";
            this.cmbCOM_SW.Size = new System.Drawing.Size(80, 27);
            this.cmbCOM_SW.TabIndex = 182;
            // 
            // FrmSystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 196);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemSetting";
            this.ShowIcon = false;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemSetting_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSystemSetting_FormClosed);
            this.Load += new System.EventHandler(this.FrmSystemSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoTray256CH;
        private System.Windows.Forms.TextBox txtUSBAddr;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbDeviceNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCOM_RT;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCOM_SW;
        private System.Windows.Forms.RadioButton rdoIRSpeedMid;
        private System.Windows.Forms.RadioButton rdoIRSpeedLow;
        private System.Windows.Forms.Label label1;
    }
}