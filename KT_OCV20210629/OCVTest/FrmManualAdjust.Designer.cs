namespace OCV
{
    partial class FrmManualAdjust
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
            this.tim_UI = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_TEMPAdjust = new System.Windows.Forms.TabPage();
            this.groupBoxTEMP = new System.Windows.Forms.GroupBox();
            this.panelTEMP = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnClrAllTempAdjust = new System.Windows.Forms.Button();
            this.btnSetAllTempAdjust = new System.Windows.Forms.Button();
            this.lblNote_TEMP = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtTempBase_adjust = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSampleTemp2 = new System.Windows.Forms.Button();
            this.lblBaseAdjust2 = new System.Windows.Forms.Label();
            this.btnSampleTemp = new System.Windows.Forms.Button();
            this.lblBaseAdjust1 = new System.Windows.Forms.Label();
            this.tab_VoltZero = new System.Windows.Forms.TabPage();
            this.grpBoxVoltAZero = new System.Windows.Forms.GroupBox();
            this.panelVoltZero = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSaveVoltZeroResult = new System.Windows.Forms.Button();
            this.btnVoltZeroAllStop = new System.Windows.Forms.Button();
            this.btnVoltZeroAllStart = new System.Windows.Forms.Button();
            this.lblNote_VoltZero = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblVoltZeroVal2 = new System.Windows.Forms.Label();
            this.lblVoltZeroVal1 = new System.Windows.Forms.Label();
            this.tab_IRAdjust_Meterage = new System.Windows.Forms.TabPage();
            this.tabContrIR = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelIRAdjust = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnIRAdjustSampleAllStart = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.txtIRBase_Adjust = new System.Windows.Forms.TextBox();
            this.btnIRAdjustAllStart = new System.Windows.Forms.Button();
            this.btnIRAdjustAllValClr = new System.Windows.Forms.Button();
            this.lblNote_IRAdjust = new System.Windows.Forms.Label();
            this.btnIRAdjustAllStop = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblIRTestVal2 = new System.Windows.Forms.Label();
            this.lblIRTestVal1 = new System.Windows.Forms.Label();
            this.lblIRBaseVal2 = new System.Windows.Forms.Label();
            this.lblIRBaseVal1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelIRMetering = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnIRMeteringSave = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.txtIRMeterErrRange = new System.Windows.Forms.TextBox();
            this.btnIRMeteringAllStop = new System.Windows.Forms.Button();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.txtIRSet_Metering = new System.Windows.Forms.TextBox();
            this.btnIRMeteringAllStart = new System.Windows.Forms.Button();
            this.lblNote_IRMetering = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblIRMeterVal2 = new System.Windows.Forms.Label();
            this.lblIRMeterVal1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tab_TEMPAdjust.SuspendLayout();
            this.groupBoxTEMP.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tab_VoltZero.SuspendLayout();
            this.grpBoxVoltAZero.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tab_IRAdjust_Meterage.SuspendLayout();
            this.tabContrIR.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tim_UI
            // 
            this.tim_UI.Enabled = true;
            this.tim_UI.Tick += new System.EventHandler(this.tim_UI_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_TEMPAdjust);
            this.tabControl1.Controls.Add(this.tab_VoltZero);
            this.tabControl1.Controls.Add(this.tab_IRAdjust_Meterage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1028, 599);
            this.tabControl1.TabIndex = 120;
            // 
            // tab_TEMPAdjust
            // 
            this.tab_TEMPAdjust.BackColor = System.Drawing.SystemColors.Control;
            this.tab_TEMPAdjust.Controls.Add(this.groupBoxTEMP);
            this.tab_TEMPAdjust.Location = new System.Drawing.Point(4, 30);
            this.tab_TEMPAdjust.Name = "tab_TEMPAdjust";
            this.tab_TEMPAdjust.Size = new System.Drawing.Size(1020, 565);
            this.tab_TEMPAdjust.TabIndex = 5;
            this.tab_TEMPAdjust.Text = "手动温度校准";
            // 
            // groupBoxTEMP
            // 
            this.groupBoxTEMP.Controls.Add(this.panelTEMP);
            this.groupBoxTEMP.Controls.Add(this.panel8);
            this.groupBoxTEMP.Controls.Add(this.panel1);
            this.groupBoxTEMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTEMP.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTEMP.Name = "groupBoxTEMP";
            this.groupBoxTEMP.Size = new System.Drawing.Size(1020, 565);
            this.groupBoxTEMP.TabIndex = 44;
            this.groupBoxTEMP.TabStop = false;
            this.groupBoxTEMP.Text = "温度校准";
            // 
            // panelTEMP
            // 
            this.panelTEMP.AutoScroll = true;
            this.panelTEMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTEMP.Location = new System.Drawing.Point(3, 65);
            this.panelTEMP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelTEMP.Name = "panelTEMP";
            this.panelTEMP.Size = new System.Drawing.Size(817, 497);
            this.panelTEMP.TabIndex = 56;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Control;
            this.panel8.Controls.Add(this.btnClrAllTempAdjust);
            this.panel8.Controls.Add(this.btnSetAllTempAdjust);
            this.panel8.Controls.Add(this.lblNote_TEMP);
            this.panel8.Controls.Add(this.groupBox14);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(820, 65);
            this.panel8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(197, 497);
            this.panel8.TabIndex = 55;
            // 
            // btnClrAllTempAdjust
            // 
            this.btnClrAllTempAdjust.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnClrAllTempAdjust.Location = new System.Drawing.Point(42, 154);
            this.btnClrAllTempAdjust.Name = "btnClrAllTempAdjust";
            this.btnClrAllTempAdjust.Size = new System.Drawing.Size(122, 38);
            this.btnClrAllTempAdjust.TabIndex = 0;
            this.btnClrAllTempAdjust.Text = "清0";
            this.btnClrAllTempAdjust.UseVisualStyleBackColor = true;
            this.btnClrAllTempAdjust.Click += new System.EventHandler(this.btnClrAllTempAdjust_Click);
            // 
            // btnSetAllTempAdjust
            // 
            this.btnSetAllTempAdjust.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSetAllTempAdjust.Location = new System.Drawing.Point(42, 94);
            this.btnSetAllTempAdjust.Name = "btnSetAllTempAdjust";
            this.btnSetAllTempAdjust.Size = new System.Drawing.Size(122, 38);
            this.btnSetAllTempAdjust.TabIndex = 1;
            this.btnSetAllTempAdjust.Text = "所有通道校准";
            this.btnSetAllTempAdjust.UseVisualStyleBackColor = true;
            this.btnSetAllTempAdjust.Click += new System.EventHandler(this.btnSetAllTempAdjust_Click);
            // 
            // lblNote_TEMP
            // 
            this.lblNote_TEMP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNote_TEMP.Location = new System.Drawing.Point(15, 302);
            this.lblNote_TEMP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNote_TEMP.Name = "lblNote_TEMP";
            this.lblNote_TEMP.Size = new System.Drawing.Size(162, 129);
            this.lblNote_TEMP.TabIndex = 4;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtTempBase_adjust);
            this.groupBox14.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox14.Location = new System.Drawing.Point(42, 16);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(135, 62);
            this.groupBox14.TabIndex = 3;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "温度基准值(°C)";
            // 
            // txtTempBase_adjust
            // 
            this.txtTempBase_adjust.Location = new System.Drawing.Point(19, 27);
            this.txtTempBase_adjust.Name = "txtTempBase_adjust";
            this.txtTempBase_adjust.Size = new System.Drawing.Size(76, 25);
            this.txtTempBase_adjust.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSampleTemp2);
            this.panel1.Controls.Add(this.lblBaseAdjust2);
            this.panel1.Controls.Add(this.btnSampleTemp);
            this.panel1.Controls.Add(this.lblBaseAdjust1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 40);
            this.panel1.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Chartreuse;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(588, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "负极温度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Chartreuse;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "正极温度";
            // 
            // btnSampleTemp2
            // 
            this.btnSampleTemp2.AutoSize = true;
            this.btnSampleTemp2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSampleTemp2.Location = new System.Drawing.Point(461, 5);
            this.btnSampleTemp2.Name = "btnSampleTemp2";
            this.btnSampleTemp2.Size = new System.Drawing.Size(54, 33);
            this.btnSampleTemp2.TabIndex = 3;
            this.btnSampleTemp2.Text = "采样";
            this.btnSampleTemp2.UseVisualStyleBackColor = true;
            this.btnSampleTemp2.Click += new System.EventHandler(this.btnSampleTemp_Click);
            // 
            // lblBaseAdjust2
            // 
            this.lblBaseAdjust2.AutoSize = true;
            this.lblBaseAdjust2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblBaseAdjust2.Location = new System.Drawing.Point(520, 12);
            this.lblBaseAdjust2.Name = "lblBaseAdjust2";
            this.lblBaseAdjust2.Size = new System.Drawing.Size(51, 20);
            this.lblBaseAdjust2.TabIndex = 2;
            this.lblBaseAdjust2.Text = "基准值";
            // 
            // btnSampleTemp
            // 
            this.btnSampleTemp.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSampleTemp.Location = new System.Drawing.Point(94, 6);
            this.btnSampleTemp.Name = "btnSampleTemp";
            this.btnSampleTemp.Size = new System.Drawing.Size(53, 28);
            this.btnSampleTemp.TabIndex = 0;
            this.btnSampleTemp.Text = "采样";
            this.btnSampleTemp.UseVisualStyleBackColor = true;
            this.btnSampleTemp.Click += new System.EventHandler(this.btnSampleTemp_Click);
            // 
            // lblBaseAdjust1
            // 
            this.lblBaseAdjust1.AutoSize = true;
            this.lblBaseAdjust1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblBaseAdjust1.Location = new System.Drawing.Point(153, 13);
            this.lblBaseAdjust1.Name = "lblBaseAdjust1";
            this.lblBaseAdjust1.Size = new System.Drawing.Size(51, 20);
            this.lblBaseAdjust1.TabIndex = 1;
            this.lblBaseAdjust1.Text = "基准值";
            // 
            // tab_VoltZero
            // 
            this.tab_VoltZero.BackColor = System.Drawing.SystemColors.Control;
            this.tab_VoltZero.Controls.Add(this.grpBoxVoltAZero);
            this.tab_VoltZero.Location = new System.Drawing.Point(4, 30);
            this.tab_VoltZero.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tab_VoltZero.Name = "tab_VoltZero";
            this.tab_VoltZero.Size = new System.Drawing.Size(1020, 565);
            this.tab_VoltZero.TabIndex = 6;
            this.tab_VoltZero.Text = "手动电压清零";
            // 
            // grpBoxVoltAZero
            // 
            this.grpBoxVoltAZero.Controls.Add(this.panelVoltZero);
            this.grpBoxVoltAZero.Controls.Add(this.panel4);
            this.grpBoxVoltAZero.Controls.Add(this.panel3);
            this.grpBoxVoltAZero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxVoltAZero.Location = new System.Drawing.Point(0, 0);
            this.grpBoxVoltAZero.Name = "grpBoxVoltAZero";
            this.grpBoxVoltAZero.Size = new System.Drawing.Size(1020, 565);
            this.grpBoxVoltAZero.TabIndex = 52;
            this.grpBoxVoltAZero.TabStop = false;
            this.grpBoxVoltAZero.Text = "电压清零";
            // 
            // panelVoltZero
            // 
            this.panelVoltZero.AutoScroll = true;
            this.panelVoltZero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVoltZero.Location = new System.Drawing.Point(3, 63);
            this.panelVoltZero.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelVoltZero.Name = "panelVoltZero";
            this.panelVoltZero.Size = new System.Drawing.Size(847, 499);
            this.panelVoltZero.TabIndex = 57;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.btnSaveVoltZeroResult);
            this.panel4.Controls.Add(this.btnVoltZeroAllStop);
            this.panel4.Controls.Add(this.btnVoltZeroAllStart);
            this.panel4.Controls.Add(this.lblNote_VoltZero);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(850, 63);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(167, 499);
            this.panel4.TabIndex = 56;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label13.Location = new System.Drawing.Point(25, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 65);
            this.label13.TabIndex = 8;
            this.label13.Text = "请使用清零(校准)工装进行操作";
            // 
            // btnSaveVoltZeroResult
            // 
            this.btnSaveVoltZeroResult.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSaveVoltZeroResult.Location = new System.Drawing.Point(15, 199);
            this.btnSaveVoltZeroResult.Name = "btnSaveVoltZeroResult";
            this.btnSaveVoltZeroResult.Size = new System.Drawing.Size(135, 35);
            this.btnSaveVoltZeroResult.TabIndex = 7;
            this.btnSaveVoltZeroResult.Text = "保存清零结果";
            this.btnSaveVoltZeroResult.UseVisualStyleBackColor = true;
            this.btnSaveVoltZeroResult.Click += new System.EventHandler(this.btnSaveVoltZeroResult_Click);
            // 
            // btnVoltZeroAllStop
            // 
            this.btnVoltZeroAllStop.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnVoltZeroAllStop.Location = new System.Drawing.Point(16, 148);
            this.btnVoltZeroAllStop.Name = "btnVoltZeroAllStop";
            this.btnVoltZeroAllStop.Size = new System.Drawing.Size(135, 35);
            this.btnVoltZeroAllStop.TabIndex = 4;
            this.btnVoltZeroAllStop.Text = "停止";
            this.btnVoltZeroAllStop.UseVisualStyleBackColor = true;
            this.btnVoltZeroAllStop.Click += new System.EventHandler(this.btnVoltZeroAllStop_Click);
            // 
            // btnVoltZeroAllStart
            // 
            this.btnVoltZeroAllStart.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnVoltZeroAllStart.Location = new System.Drawing.Point(16, 97);
            this.btnVoltZeroAllStart.Name = "btnVoltZeroAllStart";
            this.btnVoltZeroAllStart.Size = new System.Drawing.Size(135, 35);
            this.btnVoltZeroAllStart.TabIndex = 1;
            this.btnVoltZeroAllStart.Text = "所有通道电压清零";
            this.btnVoltZeroAllStart.UseVisualStyleBackColor = true;
            this.btnVoltZeroAllStart.Click += new System.EventHandler(this.btnVoltZeroAllStart_Click);
            // 
            // lblNote_VoltZero
            // 
            this.lblNote_VoltZero.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNote_VoltZero.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblNote_VoltZero.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNote_VoltZero.Location = new System.Drawing.Point(15, 276);
            this.lblNote_VoltZero.Name = "lblNote_VoltZero";
            this.lblNote_VoltZero.Size = new System.Drawing.Size(136, 162);
            this.lblNote_VoltZero.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblVoltZeroVal2);
            this.panel3.Controls.Add(this.lblVoltZeroVal1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1014, 38);
            this.panel3.TabIndex = 53;
            // 
            // lblVoltZeroVal2
            // 
            this.lblVoltZeroVal2.AutoSize = true;
            this.lblVoltZeroVal2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblVoltZeroVal2.Location = new System.Drawing.Point(440, 11);
            this.lblVoltZeroVal2.Name = "lblVoltZeroVal2";
            this.lblVoltZeroVal2.Size = new System.Drawing.Size(83, 20);
            this.lblVoltZeroVal2.TabIndex = 6;
            this.lblVoltZeroVal2.Text = "测试值(mV)";
            // 
            // lblVoltZeroVal1
            // 
            this.lblVoltZeroVal1.AutoSize = true;
            this.lblVoltZeroVal1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblVoltZeroVal1.Location = new System.Drawing.Point(118, 12);
            this.lblVoltZeroVal1.Name = "lblVoltZeroVal1";
            this.lblVoltZeroVal1.Size = new System.Drawing.Size(83, 20);
            this.lblVoltZeroVal1.TabIndex = 5;
            this.lblVoltZeroVal1.Text = "测试值(mV)";
            // 
            // tab_IRAdjust_Meterage
            // 
            this.tab_IRAdjust_Meterage.BackColor = System.Drawing.SystemColors.Control;
            this.tab_IRAdjust_Meterage.Controls.Add(this.tabContrIR);
            this.tab_IRAdjust_Meterage.Location = new System.Drawing.Point(4, 30);
            this.tab_IRAdjust_Meterage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tab_IRAdjust_Meterage.Name = "tab_IRAdjust_Meterage";
            this.tab_IRAdjust_Meterage.Size = new System.Drawing.Size(1020, 565);
            this.tab_IRAdjust_Meterage.TabIndex = 7;
            this.tab_IRAdjust_Meterage.Text = "手动内阻校准/计量";
            // 
            // tabContrIR
            // 
            this.tabContrIR.Controls.Add(this.tabPage1);
            this.tabContrIR.Controls.Add(this.tabPage2);
            this.tabContrIR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContrIR.Location = new System.Drawing.Point(0, 0);
            this.tabContrIR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabContrIR.Name = "tabContrIR";
            this.tabContrIR.SelectedIndex = 0;
            this.tabContrIR.Size = new System.Drawing.Size(1020, 565);
            this.tabContrIR.TabIndex = 54;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panelIRAdjust);
            this.tabPage1.Controls.Add(this.panel7);
            this.tabPage1.Controls.Add(this.panel6);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(1012, 531);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "内阻校准";
            // 
            // panelIRAdjust
            // 
            this.panelIRAdjust.AutoScroll = true;
            this.panelIRAdjust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIRAdjust.Location = new System.Drawing.Point(2, 36);
            this.panelIRAdjust.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelIRAdjust.Name = "panelIRAdjust";
            this.panelIRAdjust.Size = new System.Drawing.Size(835, 493);
            this.panelIRAdjust.TabIndex = 61;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Control;
            this.panel7.Controls.Add(this.btnIRAdjustSampleAllStart);
            this.panel7.Controls.Add(this.groupBox16);
            this.panel7.Controls.Add(this.btnIRAdjustAllStart);
            this.panel7.Controls.Add(this.btnIRAdjustAllValClr);
            this.panel7.Controls.Add(this.lblNote_IRAdjust);
            this.panel7.Controls.Add(this.btnIRAdjustAllStop);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(837, 36);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(173, 493);
            this.panel7.TabIndex = 60;
            // 
            // btnIRAdjustSampleAllStart
            // 
            this.btnIRAdjustSampleAllStart.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRAdjustSampleAllStart.Location = new System.Drawing.Point(17, 85);
            this.btnIRAdjustSampleAllStart.Name = "btnIRAdjustSampleAllStart";
            this.btnIRAdjustSampleAllStart.Size = new System.Drawing.Size(139, 37);
            this.btnIRAdjustSampleAllStart.TabIndex = 60;
            this.btnIRAdjustSampleAllStart.Text = "所有通道测试";
            this.btnIRAdjustSampleAllStart.UseVisualStyleBackColor = true;
            this.btnIRAdjustSampleAllStart.Click += new System.EventHandler(this.btnIRAdjustSampleAllStart_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.txtIRBase_Adjust);
            this.groupBox16.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox16.Location = new System.Drawing.Point(22, 16);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(134, 62);
            this.groupBox16.TabIndex = 59;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "内阻基准值(mΩ)";
            // 
            // txtIRBase_Adjust
            // 
            this.txtIRBase_Adjust.Location = new System.Drawing.Point(34, 26);
            this.txtIRBase_Adjust.Name = "txtIRBase_Adjust";
            this.txtIRBase_Adjust.Size = new System.Drawing.Size(66, 25);
            this.txtIRBase_Adjust.TabIndex = 2;
            this.txtIRBase_Adjust.Text = "0";
            // 
            // btnIRAdjustAllStart
            // 
            this.btnIRAdjustAllStart.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRAdjustAllStart.Location = new System.Drawing.Point(17, 135);
            this.btnIRAdjustAllStart.Name = "btnIRAdjustAllStart";
            this.btnIRAdjustAllStart.Size = new System.Drawing.Size(139, 37);
            this.btnIRAdjustAllStart.TabIndex = 55;
            this.btnIRAdjustAllStart.Text = "所有通道校准";
            this.btnIRAdjustAllStart.UseVisualStyleBackColor = true;
            this.btnIRAdjustAllStart.Click += new System.EventHandler(this.btnIRAdjustAllStart_Click);
            // 
            // btnIRAdjustAllValClr
            // 
            this.btnIRAdjustAllValClr.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRAdjustAllValClr.Location = new System.Drawing.Point(17, 240);
            this.btnIRAdjustAllValClr.Name = "btnIRAdjustAllValClr";
            this.btnIRAdjustAllValClr.Size = new System.Drawing.Size(139, 37);
            this.btnIRAdjustAllValClr.TabIndex = 57;
            this.btnIRAdjustAllValClr.Text = " 全部校准值清0";
            this.btnIRAdjustAllValClr.UseVisualStyleBackColor = true;
            this.btnIRAdjustAllValClr.Click += new System.EventHandler(this.btnIRAdjustAllValClr_Click);
            // 
            // lblNote_IRAdjust
            // 
            this.lblNote_IRAdjust.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNote_IRAdjust.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNote_IRAdjust.Location = new System.Drawing.Point(20, 292);
            this.lblNote_IRAdjust.Name = "lblNote_IRAdjust";
            this.lblNote_IRAdjust.Size = new System.Drawing.Size(136, 144);
            this.lblNote_IRAdjust.TabIndex = 58;
            // 
            // btnIRAdjustAllStop
            // 
            this.btnIRAdjustAllStop.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRAdjustAllStop.Location = new System.Drawing.Point(17, 188);
            this.btnIRAdjustAllStop.Name = "btnIRAdjustAllStop";
            this.btnIRAdjustAllStop.Size = new System.Drawing.Size(139, 37);
            this.btnIRAdjustAllStop.TabIndex = 56;
            this.btnIRAdjustAllStop.Text = "停止";
            this.btnIRAdjustAllStop.UseVisualStyleBackColor = true;
            this.btnIRAdjustAllStop.Click += new System.EventHandler(this.btnIRAdjustAllStop_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblIRTestVal2);
            this.panel6.Controls.Add(this.lblIRTestVal1);
            this.panel6.Controls.Add(this.lblIRBaseVal2);
            this.panel6.Controls.Add(this.lblIRBaseVal1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1008, 34);
            this.panel6.TabIndex = 54;
            // 
            // lblIRTestVal2
            // 
            this.lblIRTestVal2.AutoSize = true;
            this.lblIRTestVal2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblIRTestVal2.Location = new System.Drawing.Point(405, 8);
            this.lblIRTestVal2.Name = "lblIRTestVal2";
            this.lblIRTestVal2.Size = new System.Drawing.Size(51, 20);
            this.lblIRTestVal2.TabIndex = 5;
            this.lblIRTestVal2.Text = "测试值";
            // 
            // lblIRTestVal1
            // 
            this.lblIRTestVal1.AutoSize = true;
            this.lblIRTestVal1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblIRTestVal1.Location = new System.Drawing.Point(86, 8);
            this.lblIRTestVal1.Name = "lblIRTestVal1";
            this.lblIRTestVal1.Size = new System.Drawing.Size(51, 20);
            this.lblIRTestVal1.TabIndex = 4;
            this.lblIRTestVal1.Text = "测试值";
            // 
            // lblIRBaseVal2
            // 
            this.lblIRBaseVal2.AutoSize = true;
            this.lblIRBaseVal2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblIRBaseVal2.Location = new System.Drawing.Point(450, 8);
            this.lblIRBaseVal2.Name = "lblIRBaseVal2";
            this.lblIRBaseVal2.Size = new System.Drawing.Size(51, 20);
            this.lblIRBaseVal2.TabIndex = 3;
            this.lblIRBaseVal2.Text = "基准值";
            // 
            // lblIRBaseVal1
            // 
            this.lblIRBaseVal1.AutoSize = true;
            this.lblIRBaseVal1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblIRBaseVal1.Location = new System.Drawing.Point(131, 8);
            this.lblIRBaseVal1.Name = "lblIRBaseVal1";
            this.lblIRBaseVal1.Size = new System.Drawing.Size(51, 20);
            this.lblIRBaseVal1.TabIndex = 2;
            this.lblIRBaseVal1.Text = "基准值";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.panelIRMetering);
            this.tabPage2.Controls.Add(this.panel9);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(1090, 534);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "内阻计量";
            // 
            // panelIRMetering
            // 
            this.panelIRMetering.AutoScroll = true;
            this.panelIRMetering.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIRMetering.Location = new System.Drawing.Point(2, 35);
            this.panelIRMetering.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelIRMetering.Name = "panelIRMetering";
            this.panelIRMetering.Size = new System.Drawing.Size(916, 497);
            this.panelIRMetering.TabIndex = 57;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnIRMeteringSave);
            this.panel9.Controls.Add(this.groupBox13);
            this.panel9.Controls.Add(this.btnIRMeteringAllStop);
            this.panel9.Controls.Add(this.groupBox21);
            this.panel9.Controls.Add(this.btnIRMeteringAllStart);
            this.panel9.Controls.Add(this.lblNote_IRMetering);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(918, 35);
            this.panel9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(170, 497);
            this.panel9.TabIndex = 55;
            // 
            // btnIRMeteringSave
            // 
            this.btnIRMeteringSave.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRMeteringSave.Location = new System.Drawing.Point(20, 243);
            this.btnIRMeteringSave.Name = "btnIRMeteringSave";
            this.btnIRMeteringSave.Size = new System.Drawing.Size(136, 35);
            this.btnIRMeteringSave.TabIndex = 6;
            this.btnIRMeteringSave.Text = "保存计量结果";
            this.btnIRMeteringSave.UseVisualStyleBackColor = true;
            this.btnIRMeteringSave.Click += new System.EventHandler(this.btnIRMeteringSave_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.txtIRMeterErrRange);
            this.groupBox13.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox13.Location = new System.Drawing.Point(18, 89);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(142, 62);
            this.groupBox13.TabIndex = 5;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "误差范围(mΩ)";
            // 
            // txtIRMeterErrRange
            // 
            this.txtIRMeterErrRange.Location = new System.Drawing.Point(26, 27);
            this.txtIRMeterErrRange.Name = "txtIRMeterErrRange";
            this.txtIRMeterErrRange.Size = new System.Drawing.Size(96, 25);
            this.txtIRMeterErrRange.TabIndex = 2;
            // 
            // btnIRMeteringAllStop
            // 
            this.btnIRMeteringAllStop.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRMeteringAllStop.Location = new System.Drawing.Point(20, 202);
            this.btnIRMeteringAllStop.Name = "btnIRMeteringAllStop";
            this.btnIRMeteringAllStop.Size = new System.Drawing.Size(136, 35);
            this.btnIRMeteringAllStop.TabIndex = 4;
            this.btnIRMeteringAllStop.Text = "停止";
            this.btnIRMeteringAllStop.UseVisualStyleBackColor = true;
            this.btnIRMeteringAllStop.Click += new System.EventHandler(this.btnIRMeteringAllStop_Click);
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.txtIRSet_Metering);
            this.groupBox21.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox21.Location = new System.Drawing.Point(18, 13);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(142, 62);
            this.groupBox21.TabIndex = 3;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "计量工装阻值(mΩ)";
            // 
            // txtIRSet_Metering
            // 
            this.txtIRSet_Metering.Location = new System.Drawing.Point(26, 27);
            this.txtIRSet_Metering.Name = "txtIRSet_Metering";
            this.txtIRSet_Metering.Size = new System.Drawing.Size(96, 25);
            this.txtIRSet_Metering.TabIndex = 2;
            // 
            // btnIRMeteringAllStart
            // 
            this.btnIRMeteringAllStart.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnIRMeteringAllStart.Location = new System.Drawing.Point(20, 158);
            this.btnIRMeteringAllStart.Name = "btnIRMeteringAllStart";
            this.btnIRMeteringAllStart.Size = new System.Drawing.Size(136, 35);
            this.btnIRMeteringAllStart.TabIndex = 1;
            this.btnIRMeteringAllStart.Text = "所有通道计量";
            this.btnIRMeteringAllStart.UseVisualStyleBackColor = true;
            this.btnIRMeteringAllStart.Click += new System.EventHandler(this.btnIRMeteringAllStart_Click);
            // 
            // lblNote_IRMetering
            // 
            this.lblNote_IRMetering.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNote_IRMetering.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblNote_IRMetering.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNote_IRMetering.Location = new System.Drawing.Point(19, 295);
            this.lblNote_IRMetering.Name = "lblNote_IRMetering";
            this.lblNote_IRMetering.Size = new System.Drawing.Size(136, 136);
            this.lblNote_IRMetering.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblIRMeterVal2);
            this.panel5.Controls.Add(this.lblIRMeterVal1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1086, 33);
            this.panel5.TabIndex = 53;
            // 
            // lblIRMeterVal2
            // 
            this.lblIRMeterVal2.AutoSize = true;
            this.lblIRMeterVal2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblIRMeterVal2.Location = new System.Drawing.Point(455, 7);
            this.lblIRMeterVal2.Name = "lblIRMeterVal2";
            this.lblIRMeterVal2.Size = new System.Drawing.Size(51, 20);
            this.lblIRMeterVal2.TabIndex = 6;
            this.lblIRMeterVal2.Text = "测试值";
            // 
            // lblIRMeterVal1
            // 
            this.lblIRMeterVal1.AutoSize = true;
            this.lblIRMeterVal1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblIRMeterVal1.Location = new System.Drawing.Point(136, 6);
            this.lblIRMeterVal1.Name = "lblIRMeterVal1";
            this.lblIRMeterVal1.Size = new System.Drawing.Size(51, 20);
            this.lblIRMeterVal1.TabIndex = 5;
            this.lblIRMeterVal1.Text = "测试值";
            // 
            // FrmManualAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1028, 599);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualAdjust";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动校准";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmManualTestOCV_FormClosed);
            this.Load += new System.EventHandler(this.FrmOCV_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_TEMPAdjust.ResumeLayout(false);
            this.groupBoxTEMP.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tab_VoltZero.ResumeLayout(false);
            this.grpBoxVoltAZero.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tab_IRAdjust_Meterage.ResumeLayout(false);
            this.tabContrIR.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tim_UI;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_TEMPAdjust;
        private System.Windows.Forms.GroupBox groupBoxTEMP;
        private System.Windows.Forms.Panel panelTEMP;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnClrAllTempAdjust;
        private System.Windows.Forms.Button btnSetAllTempAdjust;
        private System.Windows.Forms.Label lblNote_TEMP;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtTempBase_adjust;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSampleTemp2;
        private System.Windows.Forms.Label lblBaseAdjust2;
        private System.Windows.Forms.Button btnSampleTemp;
        private System.Windows.Forms.Label lblBaseAdjust1;
        private System.Windows.Forms.TabPage tab_VoltZero;
        private System.Windows.Forms.GroupBox grpBoxVoltAZero;
        public System.Windows.Forms.Panel panelVoltZero;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSaveVoltZeroResult;
        private System.Windows.Forms.Button btnVoltZeroAllStop;
        public System.Windows.Forms.Button btnVoltZeroAllStart;
        private System.Windows.Forms.Label lblNote_VoltZero;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblVoltZeroVal2;
        private System.Windows.Forms.Label lblVoltZeroVal1;
        private System.Windows.Forms.TabPage tab_IRAdjust_Meterage;
        private System.Windows.Forms.TabControl tabContrIR;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Panel panelIRAdjust;
        private System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Button btnIRAdjustSampleAllStart;
        private System.Windows.Forms.GroupBox groupBox16;
        public System.Windows.Forms.TextBox txtIRBase_Adjust;
        public System.Windows.Forms.Button btnIRAdjustAllStart;
        public System.Windows.Forms.Button btnIRAdjustAllValClr;
        public System.Windows.Forms.Label lblNote_IRAdjust;
        private System.Windows.Forms.Button btnIRAdjustAllStop;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblIRTestVal2;
        private System.Windows.Forms.Label lblIRTestVal1;
        private System.Windows.Forms.Label lblIRBaseVal2;
        private System.Windows.Forms.Label lblIRBaseVal1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.Panel panelIRMetering;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnIRMeteringSave;
        private System.Windows.Forms.GroupBox groupBox13;
        public System.Windows.Forms.TextBox txtIRMeterErrRange;
        private System.Windows.Forms.Button btnIRMeteringAllStop;
        private System.Windows.Forms.GroupBox groupBox21;
        public System.Windows.Forms.TextBox txtIRSet_Metering;
        public System.Windows.Forms.Button btnIRMeteringAllStart;
        public System.Windows.Forms.Label lblNote_IRMetering;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblIRMeterVal2;
        private System.Windows.Forms.Label lblIRMeterVal1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

