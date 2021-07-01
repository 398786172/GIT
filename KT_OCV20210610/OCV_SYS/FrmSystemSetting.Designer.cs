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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemSetting));
            this.rdoSerialCom = new System.Windows.Forms.RadioButton();
            this.rdoUSBCom = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtUSBAddr = new System.Windows.Forms.TextBox();
            this.cmbCOM_DMT = new System.Windows.Forms.ComboBox();
            this.grpCellStyle = new System.Windows.Forms.GroupBox();
            this.grpDynaCellStyle = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCellStyle3_Code = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCellStyle2_Code = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCellStyle1_Code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodeStart = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpFixCellStyle = new System.Windows.Forms.GroupBox();
            this.rdoCellStyle3 = new System.Windows.Forms.RadioButton();
            this.rdoCellStyle2 = new System.Windows.Forms.RadioButton();
            this.rdoCellStyle1 = new System.Windows.Forms.RadioButton();
            this.rdoDynaCellStyle = new System.Windows.Forms.RadioButton();
            this.rdoFixCellStyle = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoTray40CH = new System.Windows.Forms.RadioButton();
            this.rdoTray88CH = new System.Windows.Forms.RadioButton();
            this.rdoTray80CH = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoPNVoltandShell = new System.Windows.Forms.RadioButton();
            this.rdoPNVolt = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbOCVNum = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCellCodeLen = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTrayCodeLen = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbDeviceNo = new System.Windows.Forms.ComboBox();
            this.chkNoTemp = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDelayTime = new System.Windows.Forms.TextBox();
            this.chkDelayTest = new System.Windows.Forms.CheckBox();
            this.cmbCOM_Tmp = new System.Windows.Forms.ComboBox();
            this.cmbCOM_RT = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cmbCOM_SW = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab万用表 = new System.Windows.Forms.TabPage();
            this.tab内阻仪 = new System.Windows.Forms.TabPage();
            this.tab切换系统 = new System.Windows.Forms.TabPage();
            this.tab温度测量 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbTemp = new System.Windows.Forms.ComboBox();
            this.tab位移传感器 = new System.Windows.Forms.TabPage();
            this.cmbCOM_Loca = new System.Windows.Forms.ComboBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.tab条码 = new System.Windows.Forms.TabPage();
            this.tabConn = new System.Windows.Forms.TabPage();
            this.btnModify = new System.Windows.Forms.Button();
            this.tab岗位条码 = new System.Windows.Forms.TabPage();
            this.texResrce = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grpCellStyle.SuspendLayout();
            this.grpDynaCellStyle.SuspendLayout();
            this.grpFixCellStyle.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab万用表.SuspendLayout();
            this.tab内阻仪.SuspendLayout();
            this.tab切换系统.SuspendLayout();
            this.tab温度测量.SuspendLayout();
            this.tab位移传感器.SuspendLayout();
            this.tab条码.SuspendLayout();
            this.tabConn.SuspendLayout();
            this.tab岗位条码.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoSerialCom
            // 
            this.rdoSerialCom.AutoSize = true;
            this.rdoSerialCom.Checked = true;
            this.rdoSerialCom.Location = new System.Drawing.Point(26, 22);
            this.rdoSerialCom.Name = "rdoSerialCom";
            this.rdoSerialCom.Size = new System.Drawing.Size(55, 24);
            this.rdoSerialCom.TabIndex = 0;
            this.rdoSerialCom.TabStop = true;
            this.rdoSerialCom.Text = "串口";
            this.rdoSerialCom.UseVisualStyleBackColor = true;
            this.rdoSerialCom.CheckedChanged += new System.EventHandler(this.rdoSerialCom_CheckedChanged);
            // 
            // rdoUSBCom
            // 
            this.rdoUSBCom.AutoSize = true;
            this.rdoUSBCom.Location = new System.Drawing.Point(26, 62);
            this.rdoUSBCom.Name = "rdoUSBCom";
            this.rdoUSBCom.Size = new System.Drawing.Size(54, 24);
            this.rdoUSBCom.TabIndex = 1;
            this.rdoUSBCom.TabStop = true;
            this.rdoUSBCom.Text = "USB";
            this.rdoUSBCom.UseVisualStyleBackColor = true;
            this.rdoUSBCom.CheckedChanged += new System.EventHandler(this.rdoUSBCom_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSave.Location = new System.Drawing.Point(595, 352);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(114, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存所有参数";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtUSBAddr
            // 
            this.txtUSBAddr.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtUSBAddr.Location = new System.Drawing.Point(104, 63);
            this.txtUSBAddr.Name = "txtUSBAddr";
            this.txtUSBAddr.Size = new System.Drawing.Size(125, 23);
            this.txtUSBAddr.TabIndex = 3;
            // 
            // cmbCOM_DMT
            // 
            this.cmbCOM_DMT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_DMT.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbCOM_DMT.FormattingEnabled = true;
            this.cmbCOM_DMT.Location = new System.Drawing.Point(105, 22);
            this.cmbCOM_DMT.Name = "cmbCOM_DMT";
            this.cmbCOM_DMT.Size = new System.Drawing.Size(80, 25);
            this.cmbCOM_DMT.TabIndex = 2;
            // 
            // grpCellStyle
            // 
            this.grpCellStyle.Controls.Add(this.grpDynaCellStyle);
            this.grpCellStyle.Controls.Add(this.grpFixCellStyle);
            this.grpCellStyle.Controls.Add(this.rdoDynaCellStyle);
            this.grpCellStyle.Controls.Add(this.rdoFixCellStyle);
            this.grpCellStyle.Enabled = false;
            this.grpCellStyle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpCellStyle.Location = new System.Drawing.Point(27, 486);
            this.grpCellStyle.Name = "grpCellStyle";
            this.grpCellStyle.Size = new System.Drawing.Size(694, 152);
            this.grpCellStyle.TabIndex = 174;
            this.grpCellStyle.TabStop = false;
            this.grpCellStyle.Text = "电池品种";
            this.grpCellStyle.Visible = false;
            this.grpCellStyle.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // grpDynaCellStyle
            // 
            this.grpDynaCellStyle.Controls.Add(this.label8);
            this.grpDynaCellStyle.Controls.Add(this.txtCellStyle3_Code);
            this.grpDynaCellStyle.Controls.Add(this.label7);
            this.grpDynaCellStyle.Controls.Add(this.txtCellStyle2_Code);
            this.grpDynaCellStyle.Controls.Add(this.label6);
            this.grpDynaCellStyle.Controls.Add(this.txtCellStyle1_Code);
            this.grpDynaCellStyle.Controls.Add(this.label2);
            this.grpDynaCellStyle.Controls.Add(this.txtCodeStart);
            this.grpDynaCellStyle.Controls.Add(this.label1);
            this.grpDynaCellStyle.Location = new System.Drawing.Point(106, 78);
            this.grpDynaCellStyle.Name = "grpDynaCellStyle";
            this.grpDynaCellStyle.Size = new System.Drawing.Size(573, 55);
            this.grpDynaCellStyle.TabIndex = 7;
            this.grpDynaCellStyle.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(432, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "小电池2型->";
            // 
            // txtCellStyle3_Code
            // 
            this.txtCellStyle3_Code.Location = new System.Drawing.Point(521, 19);
            this.txtCellStyle3_Code.Name = "txtCellStyle3_Code";
            this.txtCellStyle3_Code.Size = new System.Drawing.Size(30, 25);
            this.txtCellStyle3_Code.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "小电池1型->";
            // 
            // txtCellStyle2_Code
            // 
            this.txtCellStyle2_Code.Location = new System.Drawing.Point(396, 19);
            this.txtCellStyle2_Code.Name = "txtCellStyle2_Code";
            this.txtCellStyle2_Code.Size = new System.Drawing.Size(30, 25);
            this.txtCellStyle2_Code.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "大电池->";
            // 
            // txtCellStyle1_Code
            // 
            this.txtCellStyle1_Code.Location = new System.Drawing.Point(272, 19);
            this.txtCellStyle1_Code.Name = "txtCellStyle1_Code";
            this.txtCellStyle1_Code.Size = new System.Drawing.Size(30, 25);
            this.txtCellStyle1_Code.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "位字符:";
            // 
            // txtCodeStart
            // 
            this.txtCodeStart.Location = new System.Drawing.Point(99, 19);
            this.txtCodeStart.Name = "txtCodeStart";
            this.txtCodeStart.Size = new System.Drawing.Size(35, 25);
            this.txtCodeStart.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "托盘条码第";
            // 
            // grpFixCellStyle
            // 
            this.grpFixCellStyle.Controls.Add(this.rdoCellStyle3);
            this.grpFixCellStyle.Controls.Add(this.rdoCellStyle2);
            this.grpFixCellStyle.Controls.Add(this.rdoCellStyle1);
            this.grpFixCellStyle.Location = new System.Drawing.Point(106, 21);
            this.grpFixCellStyle.Name = "grpFixCellStyle";
            this.grpFixCellStyle.Size = new System.Drawing.Size(573, 48);
            this.grpFixCellStyle.TabIndex = 6;
            this.grpFixCellStyle.TabStop = false;
            // 
            // rdoCellStyle3
            // 
            this.rdoCellStyle3.AutoSize = true;
            this.rdoCellStyle3.Location = new System.Drawing.Point(252, 17);
            this.rdoCellStyle3.Name = "rdoCellStyle3";
            this.rdoCellStyle3.Size = new System.Drawing.Size(91, 24);
            this.rdoCellStyle3.TabIndex = 2;
            this.rdoCellStyle3.TabStop = true;
            this.rdoCellStyle3.Text = "小电池2型";
            this.rdoCellStyle3.UseVisualStyleBackColor = true;
            // 
            // rdoCellStyle2
            // 
            this.rdoCellStyle2.AutoSize = true;
            this.rdoCellStyle2.Location = new System.Drawing.Point(136, 17);
            this.rdoCellStyle2.Name = "rdoCellStyle2";
            this.rdoCellStyle2.Size = new System.Drawing.Size(91, 24);
            this.rdoCellStyle2.TabIndex = 1;
            this.rdoCellStyle2.TabStop = true;
            this.rdoCellStyle2.Text = "小电池1型";
            this.rdoCellStyle2.UseVisualStyleBackColor = true;
            // 
            // rdoCellStyle1
            // 
            this.rdoCellStyle1.AutoSize = true;
            this.rdoCellStyle1.Location = new System.Drawing.Point(28, 17);
            this.rdoCellStyle1.Name = "rdoCellStyle1";
            this.rdoCellStyle1.Size = new System.Drawing.Size(69, 24);
            this.rdoCellStyle1.TabIndex = 0;
            this.rdoCellStyle1.TabStop = true;
            this.rdoCellStyle1.Text = "大电池";
            this.rdoCellStyle1.UseVisualStyleBackColor = true;
            // 
            // rdoDynaCellStyle
            // 
            this.rdoDynaCellStyle.AutoSize = true;
            this.rdoDynaCellStyle.Location = new System.Drawing.Point(20, 97);
            this.rdoDynaCellStyle.Name = "rdoDynaCellStyle";
            this.rdoDynaCellStyle.Size = new System.Drawing.Size(83, 24);
            this.rdoDynaCellStyle.TabIndex = 5;
            this.rdoDynaCellStyle.TabStop = true;
            this.rdoDynaCellStyle.Text = "根据条码";
            this.rdoDynaCellStyle.UseVisualStyleBackColor = true;
            this.rdoDynaCellStyle.CheckedChanged += new System.EventHandler(this.rdoDynaCellStyle_CheckedChanged);
            // 
            // rdoFixCellStyle
            // 
            this.rdoFixCellStyle.AutoSize = true;
            this.rdoFixCellStyle.Location = new System.Drawing.Point(20, 38);
            this.rdoFixCellStyle.Name = "rdoFixCellStyle";
            this.rdoFixCellStyle.Size = new System.Drawing.Size(55, 24);
            this.rdoFixCellStyle.TabIndex = 4;
            this.rdoFixCellStyle.TabStop = true;
            this.rdoFixCellStyle.Text = "固定";
            this.rdoFixCellStyle.UseVisualStyleBackColor = true;
            this.rdoFixCellStyle.CheckedChanged += new System.EventHandler(this.rdoFixCellStyle_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoTray40CH);
            this.groupBox2.Controls.Add(this.rdoTray88CH);
            this.groupBox2.Controls.Add(this.rdoTray80CH);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox2.Location = new System.Drawing.Point(289, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 111);
            this.groupBox2.TabIndex = 175;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "托盘类型";
            // 
            // rdoTray40CH
            // 
            this.rdoTray40CH.AutoSize = true;
            this.rdoTray40CH.Location = new System.Drawing.Point(32, 54);
            this.rdoTray40CH.Name = "rdoTray40CH";
            this.rdoTray40CH.Size = new System.Drawing.Size(71, 24);
            this.rdoTray40CH.TabIndex = 2;
            this.rdoTray40CH.TabStop = true;
            this.rdoTray40CH.Text = "40通道";
            this.rdoTray40CH.UseVisualStyleBackColor = true;
            // 
            // rdoTray88CH
            // 
            this.rdoTray88CH.AutoSize = true;
            this.rdoTray88CH.Enabled = false;
            this.rdoTray88CH.Location = new System.Drawing.Point(32, 80);
            this.rdoTray88CH.Name = "rdoTray88CH";
            this.rdoTray88CH.Size = new System.Drawing.Size(71, 24);
            this.rdoTray88CH.TabIndex = 1;
            this.rdoTray88CH.TabStop = true;
            this.rdoTray88CH.Text = "88通道";
            this.rdoTray88CH.UseVisualStyleBackColor = true;
            this.rdoTray88CH.Visible = false;
            // 
            // rdoTray80CH
            // 
            this.rdoTray80CH.AutoSize = true;
            this.rdoTray80CH.Checked = true;
            this.rdoTray80CH.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.rdoTray80CH.Location = new System.Drawing.Point(32, 28);
            this.rdoTray80CH.Name = "rdoTray80CH";
            this.rdoTray80CH.Size = new System.Drawing.Size(71, 24);
            this.rdoTray80CH.TabIndex = 0;
            this.rdoTray80CH.TabStop = true;
            this.rdoTray80CH.Text = "80通道";
            this.rdoTray80CH.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoPNVoltandShell);
            this.groupBox3.Controls.Add(this.rdoPNVolt);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox3.Location = new System.Drawing.Point(481, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 83);
            this.groupBox3.TabIndex = 176;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "设备测量类型";
            // 
            // rdoPNVoltandShell
            // 
            this.rdoPNVoltandShell.AutoSize = true;
            this.rdoPNVoltandShell.Enabled = false;
            this.rdoPNVoltandShell.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.rdoPNVoltandShell.Location = new System.Drawing.Point(114, 30);
            this.rdoPNVoltandShell.Name = "rdoPNVoltandShell";
            this.rdoPNVoltandShell.Size = new System.Drawing.Size(107, 24);
            this.rdoPNVoltandShell.TabIndex = 1;
            this.rdoPNVoltandShell.TabStop = true;
            this.rdoPNVoltandShell.Text = "正负极+壳体";
            this.rdoPNVoltandShell.UseVisualStyleBackColor = true;
            this.rdoPNVoltandShell.Visible = false;
            // 
            // rdoPNVolt
            // 
            this.rdoPNVolt.AutoSize = true;
            this.rdoPNVolt.Checked = true;
            this.rdoPNVolt.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.rdoPNVolt.Location = new System.Drawing.Point(22, 30);
            this.rdoPNVolt.Name = "rdoPNVolt";
            this.rdoPNVolt.Size = new System.Drawing.Size(69, 24);
            this.rdoPNVolt.TabIndex = 0;
            this.rdoPNVolt.TabStop = true;
            this.rdoPNVolt.Text = "正负极";
            this.rdoPNVolt.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbOCVNum);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox4.Location = new System.Drawing.Point(23, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(110, 74);
            this.groupBox4.TabIndex = 177;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "OCV类型";
            // 
            // cmbOCVNum
            // 
            this.cmbOCVNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOCVNum.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbOCVNum.FormattingEnabled = true;
            this.cmbOCVNum.Location = new System.Drawing.Point(20, 30);
            this.cmbOCVNum.Name = "cmbOCVNum";
            this.cmbOCVNum.Size = new System.Drawing.Size(70, 25);
            this.cmbOCVNum.TabIndex = 179;
            this.cmbOCVNum.SelectedIndexChanged += new System.EventHandler(this.cmbOCVNum_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 177;
            this.label4.Text = "电池条码长度";
            // 
            // cmbCellCodeLen
            // 
            this.cmbCellCodeLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCellCodeLen.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbCellCodeLen.FormattingEnabled = true;
            this.cmbCellCodeLen.Location = new System.Drawing.Point(151, 61);
            this.cmbCellCodeLen.Name = "cmbCellCodeLen";
            this.cmbCellCodeLen.Size = new System.Drawing.Size(61, 25);
            this.cmbCellCodeLen.TabIndex = 176;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 175;
            this.label3.Text = "托盘条码长度";
            // 
            // cmbTrayCodeLen
            // 
            this.cmbTrayCodeLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrayCodeLen.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbTrayCodeLen.FormattingEnabled = true;
            this.cmbTrayCodeLen.Location = new System.Drawing.Point(151, 20);
            this.cmbTrayCodeLen.Name = "cmbTrayCodeLen";
            this.cmbTrayCodeLen.Size = new System.Drawing.Size(61, 25);
            this.cmbTrayCodeLen.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbDeviceNo);
            this.groupBox6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox6.Location = new System.Drawing.Point(157, 21);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(110, 74);
            this.groupBox6.TabIndex = 181;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "设备序号";
            // 
            // cmbDeviceNo
            // 
            this.cmbDeviceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceNo.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbDeviceNo.FormattingEnabled = true;
            this.cmbDeviceNo.Location = new System.Drawing.Point(19, 30);
            this.cmbDeviceNo.Name = "cmbDeviceNo";
            this.cmbDeviceNo.Size = new System.Drawing.Size(70, 25);
            this.cmbDeviceNo.TabIndex = 180;
            this.cmbDeviceNo.Visible = false;
            // 
            // chkNoTemp
            // 
            this.chkNoTemp.AutoSize = true;
            this.chkNoTemp.Location = new System.Drawing.Point(209, 20);
            this.chkNoTemp.Name = "chkNoTemp";
            this.chkNoTemp.Size = new System.Drawing.Size(84, 24);
            this.chkNoTemp.TabIndex = 187;
            this.chkNoTemp.Text = "不测温度";
            this.chkNoTemp.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(382, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 20);
            this.label5.TabIndex = 186;
            this.label5.Text = "秒";
            // 
            // txtDelayTime
            // 
            this.txtDelayTime.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtDelayTime.Location = new System.Drawing.Point(323, 57);
            this.txtDelayTime.Name = "txtDelayTime";
            this.txtDelayTime.Size = new System.Drawing.Size(53, 25);
            this.txtDelayTime.TabIndex = 185;
            // 
            // chkDelayTest
            // 
            this.chkDelayTest.AutoSize = true;
            this.chkDelayTest.Checked = true;
            this.chkDelayTest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelayTest.Location = new System.Drawing.Point(209, 56);
            this.chkDelayTest.Name = "chkDelayTest";
            this.chkDelayTest.Size = new System.Drawing.Size(101, 24);
            this.chkDelayTest.TabIndex = 184;
            this.chkDelayTest.Text = "延迟测试: >";
            this.chkDelayTest.UseVisualStyleBackColor = true;
            // 
            // cmbCOM_Tmp
            // 
            this.cmbCOM_Tmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_Tmp.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbCOM_Tmp.FormattingEnabled = true;
            this.cmbCOM_Tmp.Location = new System.Drawing.Point(104, 22);
            this.cmbCOM_Tmp.Name = "cmbCOM_Tmp";
            this.cmbCOM_Tmp.Size = new System.Drawing.Size(80, 25);
            this.cmbCOM_Tmp.TabIndex = 1;
            // 
            // cmbCOM_RT
            // 
            this.cmbCOM_RT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_RT.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbCOM_RT.FormattingEnabled = true;
            this.cmbCOM_RT.Location = new System.Drawing.Point(104, 22);
            this.cmbCOM_RT.Name = "cmbCOM_RT";
            this.cmbCOM_RT.Size = new System.Drawing.Size(80, 25);
            this.cmbCOM_RT.TabIndex = 4;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(26, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(55, 24);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "串口";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // cmbCOM_SW
            // 
            this.cmbCOM_SW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_SW.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbCOM_SW.FormattingEnabled = true;
            this.cmbCOM_SW.Location = new System.Drawing.Point(104, 22);
            this.cmbCOM_SW.Name = "cmbCOM_SW";
            this.cmbCOM_SW.Size = new System.Drawing.Size(80, 25);
            this.cmbCOM_SW.TabIndex = 4;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(26, 22);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(55, 24);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "串口";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(26, 22);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(55, 24);
            this.radioButton3.TabIndex = 188;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "串口";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab万用表);
            this.tabControl1.Controls.Add(this.tab内阻仪);
            this.tabControl1.Controls.Add(this.tab切换系统);
            this.tabControl1.Controls.Add(this.tab温度测量);
            this.tabControl1.Controls.Add(this.tab位移传感器);
            this.tabControl1.Controls.Add(this.tab条码);
            this.tabControl1.Controls.Add(this.tabConn);
            this.tabControl1.Controls.Add(this.tab岗位条码);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabControl1.Location = new System.Drawing.Point(23, 138);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(702, 204);
            this.tabControl1.TabIndex = 185;
            // 
            // tab万用表
            // 
            this.tab万用表.BackColor = System.Drawing.SystemColors.Control;
            this.tab万用表.Controls.Add(this.txtUSBAddr);
            this.tab万用表.Controls.Add(this.rdoSerialCom);
            this.tab万用表.Controls.Add(this.cmbCOM_DMT);
            this.tab万用表.Controls.Add(this.rdoUSBCom);
            this.tab万用表.Location = new System.Drawing.Point(4, 28);
            this.tab万用表.Name = "tab万用表";
            this.tab万用表.Padding = new System.Windows.Forms.Padding(3);
            this.tab万用表.Size = new System.Drawing.Size(694, 172);
            this.tab万用表.TabIndex = 0;
            this.tab万用表.Text = "万用表";
            // 
            // tab内阻仪
            // 
            this.tab内阻仪.BackColor = System.Drawing.SystemColors.Control;
            this.tab内阻仪.Controls.Add(this.cmbCOM_RT);
            this.tab内阻仪.Controls.Add(this.radioButton1);
            this.tab内阻仪.Location = new System.Drawing.Point(4, 28);
            this.tab内阻仪.Name = "tab内阻仪";
            this.tab内阻仪.Padding = new System.Windows.Forms.Padding(3);
            this.tab内阻仪.Size = new System.Drawing.Size(694, 172);
            this.tab内阻仪.TabIndex = 1;
            this.tab内阻仪.Text = "内阻仪";
            // 
            // tab切换系统
            // 
            this.tab切换系统.BackColor = System.Drawing.SystemColors.Control;
            this.tab切换系统.Controls.Add(this.cmbCOM_SW);
            this.tab切换系统.Controls.Add(this.radioButton2);
            this.tab切换系统.Location = new System.Drawing.Point(4, 28);
            this.tab切换系统.Name = "tab切换系统";
            this.tab切换系统.Size = new System.Drawing.Size(694, 172);
            this.tab切换系统.TabIndex = 2;
            this.tab切换系统.Text = "切换系统";
            // 
            // tab温度测量
            // 
            this.tab温度测量.BackColor = System.Drawing.SystemColors.Control;
            this.tab温度测量.Controls.Add(this.label12);
            this.tab温度测量.Controls.Add(this.cmbTemp);
            this.tab温度测量.Controls.Add(this.radioButton3);
            this.tab温度测量.Controls.Add(this.chkNoTemp);
            this.tab温度测量.Controls.Add(this.cmbCOM_Tmp);
            this.tab温度测量.Controls.Add(this.label5);
            this.tab温度测量.Controls.Add(this.chkDelayTest);
            this.tab温度测量.Controls.Add(this.txtDelayTime);
            this.tab温度测量.Location = new System.Drawing.Point(4, 28);
            this.tab温度测量.Name = "tab温度测量";
            this.tab温度测量.Size = new System.Drawing.Size(694, 172);
            this.tab温度测量.TabIndex = 3;
            this.tab温度测量.Text = "温度";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(217, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 20);
            this.label12.TabIndex = 190;
            this.label12.Text = "温度通道选择";
            // 
            // cmbTemp
            // 
            this.cmbTemp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemp.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbTemp.FormattingEnabled = true;
            this.cmbTemp.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cmbTemp.Location = new System.Drawing.Point(323, 99);
            this.cmbTemp.Name = "cmbTemp";
            this.cmbTemp.Size = new System.Drawing.Size(70, 25);
            this.cmbTemp.TabIndex = 189;
            // 
            // tab位移传感器
            // 
            this.tab位移传感器.Controls.Add(this.cmbCOM_Loca);
            this.tab位移传感器.Controls.Add(this.radioButton4);
            this.tab位移传感器.Location = new System.Drawing.Point(4, 28);
            this.tab位移传感器.Name = "tab位移传感器";
            this.tab位移传感器.Size = new System.Drawing.Size(694, 172);
            this.tab位移传感器.TabIndex = 9;
            this.tab位移传感器.Text = "位移传感器";
            this.tab位移传感器.UseVisualStyleBackColor = true;
            // 
            // cmbCOM_Loca
            // 
            this.cmbCOM_Loca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM_Loca.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbCOM_Loca.FormattingEnabled = true;
            this.cmbCOM_Loca.Location = new System.Drawing.Point(109, 23);
            this.cmbCOM_Loca.Name = "cmbCOM_Loca";
            this.cmbCOM_Loca.Size = new System.Drawing.Size(80, 25);
            this.cmbCOM_Loca.TabIndex = 6;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(31, 23);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(55, 24);
            this.radioButton4.TabIndex = 5;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "串口";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // tab条码
            // 
            this.tab条码.BackColor = System.Drawing.SystemColors.Control;
            this.tab条码.Controls.Add(this.label4);
            this.tab条码.Controls.Add(this.label3);
            this.tab条码.Controls.Add(this.cmbCellCodeLen);
            this.tab条码.Controls.Add(this.cmbTrayCodeLen);
            this.tab条码.Location = new System.Drawing.Point(4, 28);
            this.tab条码.Name = "tab条码";
            this.tab条码.Size = new System.Drawing.Size(694, 172);
            this.tab条码.TabIndex = 4;
            this.tab条码.Text = "条码";
            // 
            // tabConn
            // 
            this.tabConn.BackColor = System.Drawing.SystemColors.Control;
            this.tabConn.Controls.Add(this.btnModify);
            this.tabConn.Location = new System.Drawing.Point(4, 28);
            this.tabConn.Name = "tabConn";
            this.tabConn.Size = new System.Drawing.Size(694, 172);
            this.tabConn.TabIndex = 5;
            this.tabConn.Text = "联网";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(41, 29);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(83, 30);
            this.btnModify.TabIndex = 183;
            this.btnModify.Text = "更改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Visible = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // tab岗位条码
            // 
            this.tab岗位条码.Controls.Add(this.texResrce);
            this.tab岗位条码.Controls.Add(this.label9);
            this.tab岗位条码.Location = new System.Drawing.Point(4, 28);
            this.tab岗位条码.Name = "tab岗位条码";
            this.tab岗位条码.Padding = new System.Windows.Forms.Padding(3);
            this.tab岗位条码.Size = new System.Drawing.Size(694, 172);
            this.tab岗位条码.TabIndex = 7;
            this.tab岗位条码.Text = "岗位条码";
            this.tab岗位条码.UseVisualStyleBackColor = true;
            // 
            // texResrce
            // 
            this.texResrce.AcceptsTab = true;
            this.texResrce.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.texResrce.Location = new System.Drawing.Point(26, 48);
            this.texResrce.Multiline = true;
            this.texResrce.Name = "texResrce";
            this.texResrce.Size = new System.Drawing.Size(161, 28);
            this.texResrce.TabIndex = 188;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label9.Location = new System.Drawing.Point(22, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 187;
            this.label9.Text = "岗位条码";
            // 
            // FrmSystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 401);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpCellStyle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemSetting";
            this.ShowIcon = false;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.FrmSystemSetting_Load);
            this.grpCellStyle.ResumeLayout(false);
            this.grpCellStyle.PerformLayout();
            this.grpDynaCellStyle.ResumeLayout(false);
            this.grpDynaCellStyle.PerformLayout();
            this.grpFixCellStyle.ResumeLayout(false);
            this.grpFixCellStyle.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab万用表.ResumeLayout(false);
            this.tab万用表.PerformLayout();
            this.tab内阻仪.ResumeLayout(false);
            this.tab内阻仪.PerformLayout();
            this.tab切换系统.ResumeLayout(false);
            this.tab切换系统.PerformLayout();
            this.tab温度测量.ResumeLayout(false);
            this.tab温度测量.PerformLayout();
            this.tab位移传感器.ResumeLayout(false);
            this.tab位移传感器.PerformLayout();
            this.tab条码.ResumeLayout(false);
            this.tab条码.PerformLayout();
            this.tabConn.ResumeLayout(false);
            this.tab岗位条码.ResumeLayout(false);
            this.tab岗位条码.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoSerialCom;
        private System.Windows.Forms.RadioButton rdoUSBCom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpCellStyle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoTray80CH;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoPNVoltandShell;
        private System.Windows.Forms.RadioButton rdoPNVolt;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbCOM_DMT;
        private System.Windows.Forms.ComboBox cmbOCVNum;
        private System.Windows.Forms.TextBox txtUSBAddr;
        private System.Windows.Forms.RadioButton rdoTray40CH;
        private System.Windows.Forms.RadioButton rdoTray88CH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCellCodeLen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTrayCodeLen;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbDeviceNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDelayTime;
        private System.Windows.Forms.CheckBox chkDelayTest;
        private System.Windows.Forms.ComboBox cmbCOM_Tmp;
        private System.Windows.Forms.CheckBox chkNoTemp;
        private System.Windows.Forms.ComboBox cmbCOM_RT;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ComboBox cmbCOM_SW;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab万用表;
        private System.Windows.Forms.TabPage tab内阻仪;
        private System.Windows.Forms.TabPage tab切换系统;
        private System.Windows.Forms.TabPage tab温度测量;
        private System.Windows.Forms.TabPage tab条码;
        private System.Windows.Forms.RadioButton rdoDynaCellStyle;
        private System.Windows.Forms.RadioButton rdoFixCellStyle;
        private System.Windows.Forms.GroupBox grpDynaCellStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCellStyle1_Code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodeStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpFixCellStyle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCellStyle3_Code;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCellStyle2_Code;
        private System.Windows.Forms.RadioButton rdoCellStyle3;
        private System.Windows.Forms.RadioButton rdoCellStyle2;
        private System.Windows.Forms.RadioButton rdoCellStyle1;
        private System.Windows.Forms.TabPage tabConn;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox texResrce;
        private System.Windows.Forms.TabPage tab岗位条码;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbTemp;
        private System.Windows.Forms.TabPage tab位移传感器;
        private System.Windows.Forms.ComboBox cmbCOM_Loca;
        private System.Windows.Forms.RadioButton radioButton4;
    }
}