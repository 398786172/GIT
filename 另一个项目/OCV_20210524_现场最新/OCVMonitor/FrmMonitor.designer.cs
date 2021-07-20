namespace OCV
{
    partial class FrmMonitor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tim_UI_M = new System.Windows.Forms.Timer(this.components);
            this.tim_run_M = new System.Windows.Forms.Timer(this.components);
            this.tabPage_Daily = new System.Windows.Forms.TabPage();
            this.btnShowDaily = new System.Windows.Forms.Button();
            this.tabPage_PLC = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAutoMode = new System.Windows.Forms.Label();
            this.lblManualMode = new System.Windows.Forms.Label();
            this.chkEnableManMode = new System.Windows.Forms.CheckBox();
            this.grpAdvan = new System.Windows.Forms.GroupBox();
            this.btnAdvanceSet = new System.Windows.Forms.Button();
            this.grpTrans = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.lblTrayInPlace = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpCylPos = new System.Windows.Forms.GroupBox();
            this.btnPosUp = new System.Windows.Forms.Button();
            this.btnPosDown = new System.Windows.Forms.Button();
            this.lblPosCylDown = new System.Windows.Forms.Label();
            this.lblPosCylUp = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage_Msg = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClearInfoText = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClearAlarmInfo = new System.Windows.Forms.Button();
            this.txtAlarmInfo = new System.Windows.Forms.TextBox();
            this.lbl_Alarm = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTrayCodeScan = new System.Windows.Forms.TextBox();
            this.btnClearAlarm = new System.Windows.Forms.Button();
            this.lbl_Stop = new System.Windows.Forms.Label();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.lbl_Run = new System.Windows.Forms.Label();
            this.lbl_Pause = new System.Windows.Forms.Label();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_TrayView = new System.Windows.Forms.TabPage();
            this.tabPage_Temp = new System.Windows.Forms.TabPage();
            this.tim_ClearMsn = new System.Windows.Forms.Timer(this.components);
            this.tim_err = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.chkFCT = new System.Windows.Forms.CheckBox();
            this.tabPage_Daily.SuspendLayout();
            this.tabPage_PLC.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpAdvan.SuspendLayout();
            this.grpTrans.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpCylPos.SuspendLayout();
            this.tabPage_Msg.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tim_UI_M
            // 
            this.tim_UI_M.Interval = 200;
            this.tim_UI_M.Tick += new System.EventHandler(this.tim_UI_M_Tick);
            // 
            // tim_run_M
            // 
            this.tim_run_M.Interval = 50;
            this.tim_run_M.Tick += new System.EventHandler(this.tim_run_M_Tick);
            // 
            // tabPage_Daily
            // 
            this.tabPage_Daily.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Daily.Controls.Add(this.btnShowDaily);
            this.tabPage_Daily.Location = new System.Drawing.Point(4, 30);
            this.tabPage_Daily.Name = "tabPage_Daily";
            this.tabPage_Daily.Size = new System.Drawing.Size(894, 591);
            this.tabPage_Daily.TabIndex = 2;
            this.tabPage_Daily.Text = "日志";
            // 
            // btnShowDaily
            // 
            this.btnShowDaily.Location = new System.Drawing.Point(49, 49);
            this.btnShowDaily.Name = "btnShowDaily";
            this.btnShowDaily.Size = new System.Drawing.Size(149, 38);
            this.btnShowDaily.TabIndex = 0;
            this.btnShowDaily.Text = "打开日志";
            this.btnShowDaily.UseVisualStyleBackColor = true;
            this.btnShowDaily.Click += new System.EventHandler(this.btnShowDaily_Click);
            // 
            // tabPage_PLC
            // 
            this.tabPage_PLC.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_PLC.Controls.Add(this.tabControl2);
            this.tabPage_PLC.Location = new System.Drawing.Point(4, 36);
            this.tabPage_PLC.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage_PLC.Name = "tabPage_PLC";
            this.tabPage_PLC.Size = new System.Drawing.Size(1149, 780);
            this.tabPage_PLC.TabIndex = 3;
            this.tabPage_PLC.Text = "设备调试";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1149, 780);
            this.tabControl2.TabIndex = 192;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lblAutoMode);
            this.tabPage1.Controls.Add(this.lblManualMode);
            this.tabPage1.Controls.Add(this.chkEnableManMode);
            this.tabPage1.Controls.Add(this.grpAdvan);
            this.tabPage1.Controls.Add(this.grpTrans);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.grpCylPos);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1141, 754);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "控制";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(571, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(515, 45);
            this.label2.TabIndex = 184;
            this.label2.Text = "信号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAutoMode
            // 
            this.lblAutoMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAutoMode.Location = new System.Drawing.Point(326, 19);
            this.lblAutoMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoMode.Name = "lblAutoMode";
            this.lblAutoMode.Size = new System.Drawing.Size(76, 30);
            this.lblAutoMode.TabIndex = 213;
            this.lblAutoMode.Text = "自动";
            // 
            // lblManualMode
            // 
            this.lblManualMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblManualMode.Location = new System.Drawing.Point(231, 19);
            this.lblManualMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblManualMode.Name = "lblManualMode";
            this.lblManualMode.Size = new System.Drawing.Size(76, 30);
            this.lblManualMode.TabIndex = 212;
            this.lblManualMode.Text = "手动";
            // 
            // chkEnableManMode
            // 
            this.chkEnableManMode.Location = new System.Drawing.Point(13, 19);
            this.chkEnableManMode.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnableManMode.Name = "chkEnableManMode";
            this.chkEnableManMode.Size = new System.Drawing.Size(139, 30);
            this.chkEnableManMode.TabIndex = 214;
            // 
            // grpAdvan
            // 
            this.grpAdvan.Controls.Add(this.btnAdvanceSet);
            this.grpAdvan.Location = new System.Drawing.Point(571, 359);
            this.grpAdvan.Margin = new System.Windows.Forms.Padding(4);
            this.grpAdvan.Name = "grpAdvan";
            this.grpAdvan.Padding = new System.Windows.Forms.Padding(4);
            this.grpAdvan.Size = new System.Drawing.Size(515, 109);
            this.grpAdvan.TabIndex = 196;
            this.grpAdvan.TabStop = false;
            // 
            // btnAdvanceSet
            // 
            this.btnAdvanceSet.Location = new System.Drawing.Point(0, 0);
            this.btnAdvanceSet.Name = "btnAdvanceSet";
            this.btnAdvanceSet.Size = new System.Drawing.Size(75, 23);
            this.btnAdvanceSet.TabIndex = 0;
            // 
            // grpTrans
            // 
            this.grpTrans.BackColor = System.Drawing.SystemColors.Control;
            this.grpTrans.Controls.Add(this.groupBox2);
            this.grpTrans.Controls.Add(this.groupBox1);
            this.grpTrans.Controls.Add(this.groupBox3);
            this.grpTrans.Font = new System.Drawing.Font("宋体", 12F);
            this.grpTrans.Location = new System.Drawing.Point(571, 99);
            this.grpTrans.Margin = new System.Windows.Forms.Padding(4);
            this.grpTrans.Name = "grpTrans";
            this.grpTrans.Padding = new System.Windows.Forms.Padding(4);
            this.grpTrans.Size = new System.Drawing.Size(515, 220);
            this.grpTrans.TabIndex = 194;
            this.grpTrans.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(270, 121);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(220, 61);
            this.groupBox2.TabIndex = 198;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(24, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 21);
            this.label5.TabIndex = 161;
            this.label5.Text = "21700选型";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(157, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 24);
            this.label7.TabIndex = 195;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(23, 121);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(220, 61);
            this.groupBox1.TabIndex = 197;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(24, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 21);
            this.label3.TabIndex = 161;
            this.label3.Text = "18650选型";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(157, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 24);
            this.label4.TabIndex = 195;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.lblTrayInPlace);
            this.groupBox3.Location = new System.Drawing.Point(23, 34);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(220, 61);
            this.groupBox3.TabIndex = 196;
            this.groupBox3.TabStop = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label24.Location = new System.Drawing.Point(24, 21);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(74, 21);
            this.label24.TabIndex = 161;
            this.label24.Text = "托盘有无";
            // 
            // lblTrayInPlace
            // 
            this.lblTrayInPlace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTrayInPlace.Location = new System.Drawing.Point(157, 22);
            this.lblTrayInPlace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrayInPlace.Name = "lblTrayInPlace";
            this.lblTrayInPlace.Size = new System.Drawing.Size(36, 24);
            this.lblTrayInPlace.TabIndex = 195;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(17, 67);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(515, 45);
            this.label6.TabIndex = 186;
            this.label6.Text = "针床";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpCylPos
            // 
            this.grpCylPos.Controls.Add(this.btnPosUp);
            this.grpCylPos.Controls.Add(this.btnPosDown);
            this.grpCylPos.Controls.Add(this.lblPosCylDown);
            this.grpCylPos.Controls.Add(this.lblPosCylUp);
            this.grpCylPos.Location = new System.Drawing.Point(17, 97);
            this.grpCylPos.Margin = new System.Windows.Forms.Padding(4);
            this.grpCylPos.Name = "grpCylPos";
            this.grpCylPos.Padding = new System.Windows.Forms.Padding(4);
            this.grpCylPos.Size = new System.Drawing.Size(515, 222);
            this.grpCylPos.TabIndex = 183;
            this.grpCylPos.TabStop = false;
            // 
            // btnPosUp
            // 
            this.btnPosUp.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPosUp.Location = new System.Drawing.Point(47, 101);
            this.btnPosUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnPosUp.Name = "btnPosUp";
            this.btnPosUp.Size = new System.Drawing.Size(185, 50);
            this.btnPosUp.TabIndex = 201;
            this.btnPosUp.Text = "打开";
            this.btnPosUp.UseVisualStyleBackColor = true;
            // 
            // btnPosDown
            // 
            this.btnPosDown.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPosDown.Location = new System.Drawing.Point(283, 101);
            this.btnPosDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnPosDown.Name = "btnPosDown";
            this.btnPosDown.Size = new System.Drawing.Size(181, 50);
            this.btnPosDown.TabIndex = 202;
            this.btnPosDown.Text = "压合";
            this.btnPosDown.UseVisualStyleBackColor = true;
            // 
            // lblPosCylDown
            // 
            this.lblPosCylDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPosCylDown.Location = new System.Drawing.Point(283, 63);
            this.lblPosCylDown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosCylDown.Name = "lblPosCylDown";
            this.lblPosCylDown.Size = new System.Drawing.Size(181, 21);
            this.lblPosCylDown.TabIndex = 200;
            // 
            // lblPosCylUp
            // 
            this.lblPosCylUp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPosCylUp.Location = new System.Drawing.Point(47, 63);
            this.lblPosCylUp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosCylUp.Name = "lblPosCylUp";
            this.lblPosCylUp.Size = new System.Drawing.Size(185, 21);
            this.lblPosCylUp.TabIndex = 199;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1141, 754);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "信息";
            // 
            // tabPage_Msg
            // 
            this.tabPage_Msg.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Msg.Controls.Add(this.panel2);
            this.tabPage_Msg.Controls.Add(this.panel3);
            this.tabPage_Msg.Controls.Add(this.panel1);
            this.tabPage_Msg.Location = new System.Drawing.Point(4, 30);
            this.tabPage_Msg.Name = "tabPage_Msg";
            this.tabPage_Msg.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Msg.Size = new System.Drawing.Size(894, 591);
            this.tabPage_Msg.TabIndex = 0;
            this.tabPage_Msg.Text = " 实时/报警信息 ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.Controls.Add(this.btnClearInfoText);
            this.panel2.Controls.Add(this.txtInfo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 318);
            this.panel2.TabIndex = 160;
            this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
            // 
            // btnClearInfoText
            // 
            this.btnClearInfoText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearInfoText.Location = new System.Drawing.Point(711, 0);
            this.btnClearInfoText.Name = "btnClearInfoText";
            this.btnClearInfoText.Size = new System.Drawing.Size(125, 30);
            this.btnClearInfoText.TabIndex = 1;
            this.btnClearInfoText.Text = "清空信息";
            this.btnClearInfoText.UseVisualStyleBackColor = true;
            this.btnClearInfoText.Click += new System.EventHandler(this.btnClearInfoText_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfo.Location = new System.Drawing.Point(38, 0);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(850, 318);
            this.txtInfo.TabIndex = 129;
            this.txtInfo.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 318);
            this.label1.TabIndex = 127;
            this.label1.Text = "实时信息";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel3.Controls.Add(this.btnClearAlarmInfo);
            this.panel3.Controls.Add(this.txtAlarmInfo);
            this.panel3.Controls.Add(this.lbl_Alarm);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 321);
            this.panel3.Margin = new System.Windows.Forms.Padding(10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(888, 149);
            this.panel3.TabIndex = 159;
            this.panel3.Resize += new System.EventHandler(this.panel3_Resize);
            // 
            // btnClearAlarmInfo
            // 
            this.btnClearAlarmInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearAlarmInfo.Location = new System.Drawing.Point(711, 1);
            this.btnClearAlarmInfo.Name = "btnClearAlarmInfo";
            this.btnClearAlarmInfo.Size = new System.Drawing.Size(125, 30);
            this.btnClearAlarmInfo.TabIndex = 128;
            this.btnClearAlarmInfo.Text = "清空信息";
            this.btnClearAlarmInfo.UseVisualStyleBackColor = true;
            this.btnClearAlarmInfo.Click += new System.EventHandler(this.btnClearAlarmInfo_Click);
            // 
            // txtAlarmInfo
            // 
            this.txtAlarmInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.txtAlarmInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlarmInfo.Location = new System.Drawing.Point(38, 0);
            this.txtAlarmInfo.Margin = new System.Windows.Forms.Padding(10);
            this.txtAlarmInfo.Multiline = true;
            this.txtAlarmInfo.Name = "txtAlarmInfo";
            this.txtAlarmInfo.ReadOnly = true;
            this.txtAlarmInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAlarmInfo.Size = new System.Drawing.Size(850, 149);
            this.txtAlarmInfo.TabIndex = 127;
            // 
            // lbl_Alarm
            // 
            this.lbl_Alarm.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbl_Alarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Alarm.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Alarm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Alarm.Location = new System.Drawing.Point(0, 0);
            this.lbl_Alarm.Name = "lbl_Alarm";
            this.lbl_Alarm.Size = new System.Drawing.Size(38, 149);
            this.lbl_Alarm.TabIndex = 126;
            this.lbl_Alarm.Text = "报警信息";
            this.lbl_Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkFCT);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnClearAlarm);
            this.panel1.Controls.Add(this.lbl_Stop);
            this.panel1.Controls.Add(this.btn_Start);
            this.panel1.Controls.Add(this.btn_Stop);
            this.panel1.Controls.Add(this.lbl_Run);
            this.panel1.Controls.Add(this.lbl_Pause);
            this.panel1.Controls.Add(this.btn_Pause);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 470);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 118);
            this.panel1.TabIndex = 157;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.txtTrayCodeScan);
            this.panel4.Location = new System.Drawing.Point(587, 33);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(287, 75);
            this.panel4.TabIndex = 184;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(177, 44);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 20);
            this.label9.TabIndex = 183;
            this.label9.Text = "(按回车键确定)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkBox1.Location = new System.Drawing.Point(46, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 25);
            this.checkBox1.TabIndex = 184;
            this.checkBox1.Text = "手动输入托盘码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.Location = new System.Drawing.Point(12, 11);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 181;
            this.label8.Text = "托盘条码";
            // 
            // txtTrayCodeScan
            // 
            this.txtTrayCodeScan.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTrayCodeScan.Location = new System.Drawing.Point(86, 9);
            this.txtTrayCodeScan.Margin = new System.Windows.Forms.Padding(2);
            this.txtTrayCodeScan.Multiline = true;
            this.txtTrayCodeScan.Name = "txtTrayCodeScan";
            this.txtTrayCodeScan.Size = new System.Drawing.Size(188, 28);
            this.txtTrayCodeScan.TabIndex = 182;
            // 
            // btnClearAlarm
            // 
            this.btnClearAlarm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearAlarm.Location = new System.Drawing.Point(452, 58);
            this.btnClearAlarm.Name = "btnClearAlarm";
            this.btnClearAlarm.Size = new System.Drawing.Size(107, 44);
            this.btnClearAlarm.TabIndex = 180;
            this.btnClearAlarm.Text = "清报警";
            this.btnClearAlarm.UseVisualStyleBackColor = true;
            this.btnClearAlarm.Click += new System.EventHandler(this.btnClearAlarm_Click);
            // 
            // lbl_Stop
            // 
            this.lbl_Stop.BackColor = System.Drawing.Color.White;
            this.lbl_Stop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Stop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Stop.Location = new System.Drawing.Point(316, 27);
            this.lbl_Stop.Name = "lbl_Stop";
            this.lbl_Stop.Size = new System.Drawing.Size(107, 18);
            this.lbl_Stop.TabIndex = 177;
            this.lbl_Stop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Start
            // 
            this.btn_Start.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Start.Location = new System.Drawing.Point(34, 58);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(107, 44);
            this.btn_Start.TabIndex = 175;
            this.btn_Start.Text = "启动";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Stop.Location = new System.Drawing.Point(316, 58);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(107, 44);
            this.btn_Stop.TabIndex = 176;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // lbl_Run
            // 
            this.lbl_Run.BackColor = System.Drawing.Color.White;
            this.lbl_Run.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Run.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Run.Location = new System.Drawing.Point(34, 27);
            this.lbl_Run.Name = "lbl_Run";
            this.lbl_Run.Size = new System.Drawing.Size(107, 18);
            this.lbl_Run.TabIndex = 124;
            this.lbl_Run.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Pause
            // 
            this.lbl_Pause.BackColor = System.Drawing.Color.White;
            this.lbl_Pause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Pause.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Pause.Location = new System.Drawing.Point(175, 27);
            this.lbl_Pause.Name = "lbl_Pause";
            this.lbl_Pause.Size = new System.Drawing.Size(107, 18);
            this.lbl_Pause.TabIndex = 125;
            this.lbl_Pause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Pause.Location = new System.Drawing.Point(175, 58);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(107, 44);
            this.btn_Pause.TabIndex = 174;
            this.btn_Pause.Text = "暂停";
            this.btn_Pause.UseVisualStyleBackColor = true;
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage_Msg);
            this.tabControl1.Controls.Add(this.tabPage_TrayView);
            this.tabControl1.Controls.Add(this.tabPage_Temp);
            this.tabControl1.Controls.Add(this.tabPage_Daily);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(902, 625);
            this.tabControl1.TabIndex = 159;
            // 
            // tabPage_TrayView
            // 
            this.tabPage_TrayView.Location = new System.Drawing.Point(4, 30);
            this.tabPage_TrayView.Name = "tabPage_TrayView";
            this.tabPage_TrayView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_TrayView.Size = new System.Drawing.Size(894, 591);
            this.tabPage_TrayView.TabIndex = 4;
            this.tabPage_TrayView.Text = "托盘显示";
            this.tabPage_TrayView.UseVisualStyleBackColor = true;
            // 
            // tabPage_Temp
            // 
            this.tabPage_Temp.Location = new System.Drawing.Point(4, 30);
            this.tabPage_Temp.Name = "tabPage_Temp";
            this.tabPage_Temp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Temp.Size = new System.Drawing.Size(894, 591);
            this.tabPage_Temp.TabIndex = 3;
            this.tabPage_Temp.Text = "温度监控";
            this.tabPage_Temp.UseVisualStyleBackColor = true;
            // 
            // tim_ClearMsn
            // 
            this.tim_ClearMsn.Tick += new System.EventHandler(this.tim_ClearMsn_Tick);
            // 
            // tim_err
            // 
            this.tim_err.Tick += new System.EventHandler(this.tim_err_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F);
            this.label10.Location = new System.Drawing.Point(11, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 160;
            this.label10.Text = "label10";
            // 
            // chkFCT
            // 
            this.chkFCT.AutoSize = true;
            this.chkFCT.Location = new System.Drawing.Point(590, 4);
            this.chkFCT.Name = "chkFCT";
            this.chkFCT.Size = new System.Drawing.Size(257, 25);
            this.chkFCT.TabIndex = 185;
            this.chkFCT.Text = "是否FCT电池(仅FCT电池时勾选)";
            this.chkFCT.UseVisualStyleBackColor = true;
            this.chkFCT.CheckedChanged += new System.EventHandler(this.chkFCT_CheckedChanged);
            // 
            // FrmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(902, 656);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMonitor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "实时监控";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage_Daily.ResumeLayout(false);
            this.tabPage_PLC.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grpAdvan.ResumeLayout(false);
            this.grpTrans.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpCylPos.ResumeLayout(false);
            this.tabPage_Msg.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tim_UI_M;
        private System.Windows.Forms.Timer tim_run_M;
        private System.Windows.Forms.TabPage tabPage_Daily;
        private System.Windows.Forms.Button btnShowDaily;
        private System.Windows.Forms.TabPage tabPage_PLC;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkEnableManMode;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpCylPos;
        private System.Windows.Forms.Label lblPosCylDown;
        private System.Windows.Forms.Label lblPosCylUp;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage_Msg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClearInfoText;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClearAlarmInfo;
        private System.Windows.Forms.TextBox txtAlarmInfo;
        private System.Windows.Forms.Label lbl_Alarm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label lbl_Run;
        private System.Windows.Forms.Label lbl_Pause;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Timer tim_ClearMsn;
        private System.Windows.Forms.Label lblAutoMode;
        private System.Windows.Forms.Label lblManualMode;
        private System.Windows.Forms.Timer tim_err;
        private System.Windows.Forms.GroupBox grpAdvan;
        private System.Windows.Forms.Button btnAdvanceSet;
        private System.Windows.Forms.GroupBox grpTrans;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTrayInPlace;
        private System.Windows.Forms.Button btnPosUp;
        private System.Windows.Forms.Button btnPosDown;
        private System.Windows.Forms.Button btnClearAlarm;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTrayCodeScan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabPage_Temp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage_TrayView;
        private System.Windows.Forms.CheckBox chkFCT;
    }
}

