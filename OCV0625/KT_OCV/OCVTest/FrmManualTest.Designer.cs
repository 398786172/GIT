namespace OCV
{
    partial class FrmManualTest
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
            this.tab_MultiTest = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvManualTest = new System.Windows.Forms.DataGridView();
            this.grpbxTestManual = new System.Windows.Forms.GroupBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPostiveSVTest = new System.Windows.Forms.Button();
            this.btnStartTempTest = new System.Windows.Forms.Button();
            this.btnSaveTest = new System.Windows.Forms.Button();
            this.btnDmm_Init = new System.Windows.Forms.Button();
            this.btnIRBT4_Init = new System.Windows.Forms.Button();
            this.lblTestTime = new System.Windows.Forms.Label();
            this.btnTestMultiIR_BT4560 = new System.Windows.Forms.Button();
            this.btnTestMultiVolt_PosNeg = new System.Windows.Forms.Button();
            this.btnTestMultiVolt_ShellNeg = new System.Windows.Forms.Button();
            this.tab_ManualTest = new System.Windows.Forms.TabPage();
            this.grpBT4560 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInit_TestIRBT4 = new System.Windows.Forms.Button();
            this.btnTestIRBT4 = new System.Windows.Forms.Button();
            this.txtACIR = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInit_TestVolt = new System.Windows.Forms.Button();
            this.btnTestVolt = new System.Windows.Forms.Button();
            this.txtVolt = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAdd_V = new System.Windows.Forms.Button();
            this.btnSUB_V = new System.Windows.Forms.Button();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.btnSWCH_PosNeg = new System.Windows.Forms.Button();
            this.btnStopSW_PosNeg = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnStopSW_IRBT4 = new System.Windows.Forms.Button();
            this.btnSWCH_IRBT4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnStopSW_ShellNeg = new System.Windows.Forms.Button();
            this.btnSWCH_ShellNeg = new System.Windows.Forms.Button();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volt_PosNeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volt_ShellNeg2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volt_ShellNeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACIR_BT4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempData2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_MultiTest.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManualTest)).BeginInit();
            this.grpbxTestManual.SuspendLayout();
            this.tab_ManualTest.SuspendLayout();
            this.grpBT4560.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tim_UI
            // 
            this.tim_UI.Enabled = true;
            this.tim_UI.Tick += new System.EventHandler(this.tim_UI_Tick);
            // 
            // tab_MultiTest
            // 
            this.tab_MultiTest.BackColor = System.Drawing.SystemColors.Control;
            this.tab_MultiTest.Controls.Add(this.panel1);
            this.tab_MultiTest.Controls.Add(this.grpbxTestManual);
            this.tab_MultiTest.Location = new System.Drawing.Point(4, 34);
            this.tab_MultiTest.Margin = new System.Windows.Forms.Padding(4);
            this.tab_MultiTest.Name = "tab_MultiTest";
            this.tab_MultiTest.Padding = new System.Windows.Forms.Padding(4);
            this.tab_MultiTest.Size = new System.Drawing.Size(1464, 807);
            this.tab_MultiTest.TabIndex = 3;
            this.tab_MultiTest.Text = "多通道手动测试";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvManualTest);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1097, 800);
            this.panel1.TabIndex = 42;
            // 
            // dgvManualTest
            // 
            this.dgvManualTest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvManualTest.ColumnHeadersHeight = 30;
            this.dgvManualTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvManualTest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Volt_PosNeg,
            this.Volt_ShellNeg2,
            this.Volt_ShellNeg,
            this.ACIR_BT4,
            this.TempData,
            this.TempData2});
            this.dgvManualTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvManualTest.Location = new System.Drawing.Point(0, 0);
            this.dgvManualTest.Margin = new System.Windows.Forms.Padding(4);
            this.dgvManualTest.Name = "dgvManualTest";
            this.dgvManualTest.RowHeadersWidth = 20;
            this.dgvManualTest.RowTemplate.Height = 23;
            this.dgvManualTest.Size = new System.Drawing.Size(1097, 800);
            this.dgvManualTest.TabIndex = 43;
            // 
            // grpbxTestManual
            // 
            this.grpbxTestManual.Controls.Add(this.lblNum);
            this.grpbxTestManual.Controls.Add(this.btnCreateReport);
            this.grpbxTestManual.Controls.Add(this.btnSave);
            this.grpbxTestManual.Controls.Add(this.btnPostiveSVTest);
            this.grpbxTestManual.Controls.Add(this.btnStartTempTest);
            this.grpbxTestManual.Controls.Add(this.btnSaveTest);
            this.grpbxTestManual.Controls.Add(this.btnDmm_Init);
            this.grpbxTestManual.Controls.Add(this.btnIRBT4_Init);
            this.grpbxTestManual.Controls.Add(this.lblTestTime);
            this.grpbxTestManual.Controls.Add(this.btnTestMultiIR_BT4560);
            this.grpbxTestManual.Controls.Add(this.btnTestMultiVolt_PosNeg);
            this.grpbxTestManual.Controls.Add(this.btnTestMultiVolt_ShellNeg);
            this.grpbxTestManual.Enabled = false;
            this.grpbxTestManual.Location = new System.Drawing.Point(1101, 8);
            this.grpbxTestManual.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxTestManual.Name = "grpbxTestManual";
            this.grpbxTestManual.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxTestManual.Size = new System.Drawing.Size(328, 750);
            this.grpbxTestManual.TabIndex = 41;
            this.grpbxTestManual.TabStop = false;
            this.grpbxTestManual.Text = "测试";
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(261, 688);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(0, 25);
            this.lblNum.TabIndex = 57;
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(13, 620);
            this.btnCreateReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(243, 50);
            this.btnCreateReport.TabIndex = 56;
            this.btnCreateReport.Text = "一键生成报告";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(13, 557);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(243, 50);
            this.btnSave.TabIndex = 55;
            this.btnSave.Text = "保存数据";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPostiveSVTest
            // 
            this.btnPostiveSVTest.Location = new System.Drawing.Point(13, 204);
            this.btnPostiveSVTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnPostiveSVTest.Name = "btnPostiveSVTest";
            this.btnPostiveSVTest.Size = new System.Drawing.Size(244, 46);
            this.btnPostiveSVTest.TabIndex = 54;
            this.btnPostiveSVTest.Text = "测壳体正极电压";
            this.btnPostiveSVTest.UseVisualStyleBackColor = true;
            this.btnPostiveSVTest.Click += new System.EventHandler(this.btnPostiveSVTest_Click);
            // 
            // btnStartTempTest
            // 
            this.btnStartTempTest.Location = new System.Drawing.Point(16, 318);
            this.btnStartTempTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartTempTest.Name = "btnStartTempTest";
            this.btnStartTempTest.Size = new System.Drawing.Size(244, 50);
            this.btnStartTempTest.TabIndex = 53;
            this.btnStartTempTest.Text = "测温度";
            this.btnStartTempTest.UseVisualStyleBackColor = true;
            this.btnStartTempTest.Click += new System.EventHandler(this.btnStartTempTest_Click_1);
            // 
            // btnSaveTest
            // 
            this.btnSaveTest.Location = new System.Drawing.Point(16, 376);
            this.btnSaveTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveTest.Name = "btnSaveTest";
            this.btnSaveTest.Size = new System.Drawing.Size(243, 50);
            this.btnSaveTest.TabIndex = 52;
            this.btnSaveTest.Text = "保存数据";
            this.btnSaveTest.UseVisualStyleBackColor = true;
            this.btnSaveTest.Click += new System.EventHandler(this.btnSaveTest_Click_1);
            // 
            // btnDmm_Init
            // 
            this.btnDmm_Init.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDmm_Init.Location = new System.Drawing.Point(16, 439);
            this.btnDmm_Init.Margin = new System.Windows.Forms.Padding(4);
            this.btnDmm_Init.Name = "btnDmm_Init";
            this.btnDmm_Init.Size = new System.Drawing.Size(243, 50);
            this.btnDmm_Init.TabIndex = 51;
            this.btnDmm_Init.Text = "万用表复位";
            this.btnDmm_Init.UseVisualStyleBackColor = true;
            this.btnDmm_Init.Click += new System.EventHandler(this.btnDmm_Init_Click);
            // 
            // btnIRBT4_Init
            // 
            this.btnIRBT4_Init.Location = new System.Drawing.Point(15, 499);
            this.btnIRBT4_Init.Margin = new System.Windows.Forms.Padding(4);
            this.btnIRBT4_Init.Name = "btnIRBT4_Init";
            this.btnIRBT4_Init.Size = new System.Drawing.Size(243, 50);
            this.btnIRBT4_Init.TabIndex = 50;
            this.btnIRBT4_Init.Text = "内阻仪复位";
            this.btnIRBT4_Init.UseVisualStyleBackColor = true;
            this.btnIRBT4_Init.Click += new System.EventHandler(this.btnIRBT4_Init_Click);
            // 
            // lblTestTime
            // 
            this.lblTestTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTestTime.Location = new System.Drawing.Point(15, 29);
            this.lblTestTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestTime.Name = "lblTestTime";
            this.lblTestTime.Size = new System.Drawing.Size(245, 49);
            this.lblTestTime.TabIndex = 45;
            // 
            // btnTestMultiIR_BT4560
            // 
            this.btnTestMultiIR_BT4560.Location = new System.Drawing.Point(15, 260);
            this.btnTestMultiIR_BT4560.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestMultiIR_BT4560.Name = "btnTestMultiIR_BT4560";
            this.btnTestMultiIR_BT4560.Size = new System.Drawing.Size(244, 46);
            this.btnTestMultiIR_BT4560.TabIndex = 48;
            this.btnTestMultiIR_BT4560.Text = "测BT4560内阻";
            this.btnTestMultiIR_BT4560.UseVisualStyleBackColor = true;
            this.btnTestMultiIR_BT4560.Click += new System.EventHandler(this.btnTestMultiIR_BT4560_Click);
            // 
            // btnTestMultiVolt_PosNeg
            // 
            this.btnTestMultiVolt_PosNeg.Location = new System.Drawing.Point(16, 92);
            this.btnTestMultiVolt_PosNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestMultiVolt_PosNeg.Name = "btnTestMultiVolt_PosNeg";
            this.btnTestMultiVolt_PosNeg.Size = new System.Drawing.Size(244, 46);
            this.btnTestMultiVolt_PosNeg.TabIndex = 46;
            this.btnTestMultiVolt_PosNeg.Text = "测正负极电压";
            this.btnTestMultiVolt_PosNeg.UseVisualStyleBackColor = true;
            this.btnTestMultiVolt_PosNeg.Click += new System.EventHandler(this.btnTestMultiVolt_PosNeg_Click);
            // 
            // btnTestMultiVolt_ShellNeg
            // 
            this.btnTestMultiVolt_ShellNeg.Location = new System.Drawing.Point(16, 148);
            this.btnTestMultiVolt_ShellNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestMultiVolt_ShellNeg.Name = "btnTestMultiVolt_ShellNeg";
            this.btnTestMultiVolt_ShellNeg.Size = new System.Drawing.Size(244, 46);
            this.btnTestMultiVolt_ShellNeg.TabIndex = 47;
            this.btnTestMultiVolt_ShellNeg.Text = "测壳体负极电压";
            this.btnTestMultiVolt_ShellNeg.UseVisualStyleBackColor = true;
            this.btnTestMultiVolt_ShellNeg.Click += new System.EventHandler(this.btnTestMultiVolt_ShellNeg_Click);
            // 
            // tab_ManualTest
            // 
            this.tab_ManualTest.BackColor = System.Drawing.SystemColors.Control;
            this.tab_ManualTest.Controls.Add(this.grpBT4560);
            this.tab_ManualTest.Controls.Add(this.groupBox5);
            this.tab_ManualTest.Controls.Add(this.groupBox4);
            this.tab_ManualTest.Location = new System.Drawing.Point(4, 34);
            this.tab_ManualTest.Margin = new System.Windows.Forms.Padding(4);
            this.tab_ManualTest.Name = "tab_ManualTest";
            this.tab_ManualTest.Size = new System.Drawing.Size(1464, 807);
            this.tab_ManualTest.TabIndex = 2;
            this.tab_ManualTest.Text = "手动测试";
            // 
            // grpBT4560
            // 
            this.grpBT4560.Controls.Add(this.button3);
            this.grpBT4560.Controls.Add(this.btnAuto);
            this.grpBT4560.Controls.Add(this.listBox1);
            this.grpBT4560.Controls.Add(this.txtResult);
            this.grpBT4560.Controls.Add(this.label6);
            this.grpBT4560.Controls.Add(this.txtMin);
            this.grpBT4560.Controls.Add(this.label5);
            this.grpBT4560.Controls.Add(this.txtMax);
            this.grpBT4560.Controls.Add(this.label4);
            this.grpBT4560.Controls.Add(this.label2);
            this.grpBT4560.Controls.Add(this.btnInit_TestIRBT4);
            this.grpBT4560.Controls.Add(this.btnTestIRBT4);
            this.grpBT4560.Controls.Add(this.txtACIR);
            this.grpBT4560.Location = new System.Drawing.Point(673, 218);
            this.grpBT4560.Margin = new System.Windows.Forms.Padding(4);
            this.grpBT4560.Name = "grpBT4560";
            this.grpBT4560.Padding = new System.Windows.Forms.Padding(4);
            this.grpBT4560.Size = new System.Drawing.Size(637, 376);
            this.grpBT4560.TabIndex = 17;
            this.grpBT4560.TabStop = false;
            this.grpBT4560.Text = "读BT4560内阻仪";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(207, 114);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 48);
            this.button3.TabIndex = 22;
            this.button3.Text = "AutoStop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(43, 114);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(4);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(151, 48);
            this.btnAuto.TabIndex = 21;
            this.btnAuto.Text = "Auto Test";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Visible = false;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(333, 114);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(268, 254);
            this.listBox1.TabIndex = 20;
            this.listBox1.Visible = false;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(141, 268);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(132, 32);
            this.txtResult.TabIndex = 19;
            this.txtResult.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 274);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 25);
            this.label6.TabIndex = 18;
            this.label6.Text = "result";
            this.label6.Visible = false;
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(141, 226);
            this.txtMin.Margin = new System.Windows.Forms.Padding(4);
            this.txtMin.Name = "txtMin";
            this.txtMin.ReadOnly = true;
            this.txtMin.Size = new System.Drawing.Size(132, 32);
            this.txtMin.TabIndex = 17;
            this.txtMin.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 229);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "MinValue";
            this.label5.Visible = false;
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(141, 185);
            this.txtMax.Margin = new System.Windows.Forms.Padding(4);
            this.txtMax.Name = "txtMax";
            this.txtMax.ReadOnly = true;
            this.txtMax.Size = new System.Drawing.Size(132, 32);
            this.txtMax.TabIndex = 15;
            this.txtMax.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 188);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "MaxValue";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "mΩ";
            // 
            // btnInit_TestIRBT4
            // 
            this.btnInit_TestIRBT4.Location = new System.Drawing.Point(439, 50);
            this.btnInit_TestIRBT4.Margin = new System.Windows.Forms.Padding(4);
            this.btnInit_TestIRBT4.Name = "btnInit_TestIRBT4";
            this.btnInit_TestIRBT4.Size = new System.Drawing.Size(164, 48);
            this.btnInit_TestIRBT4.TabIndex = 8;
            this.btnInit_TestIRBT4.Text = "初始化";
            this.btnInit_TestIRBT4.UseVisualStyleBackColor = true;
            this.btnInit_TestIRBT4.Click += new System.EventHandler(this.btnInit_TestIRBT4_Click);
            // 
            // btnTestIRBT4
            // 
            this.btnTestIRBT4.Location = new System.Drawing.Point(267, 50);
            this.btnTestIRBT4.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestIRBT4.Name = "btnTestIRBT4";
            this.btnTestIRBT4.Size = new System.Drawing.Size(148, 48);
            this.btnTestIRBT4.TabIndex = 7;
            this.btnTestIRBT4.Text = "测试";
            this.btnTestIRBT4.UseVisualStyleBackColor = true;
            this.btnTestIRBT4.Click += new System.EventHandler(this.btnTestIRBT4_Click);
            // 
            // txtACIR
            // 
            this.txtACIR.Location = new System.Drawing.Point(43, 58);
            this.txtACIR.Margin = new System.Windows.Forms.Padding(4);
            this.txtACIR.Name = "txtACIR";
            this.txtACIR.Size = new System.Drawing.Size(149, 32);
            this.txtACIR.TabIndex = 6;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.btnInit_TestVolt);
            this.groupBox5.Controls.Add(this.btnTestVolt);
            this.groupBox5.Controls.Add(this.txtVolt);
            this.groupBox5.Location = new System.Drawing.Point(673, 68);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(637, 142);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "读万用表";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(201, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "mV";
            // 
            // btnInit_TestVolt
            // 
            this.btnInit_TestVolt.Location = new System.Drawing.Point(439, 50);
            this.btnInit_TestVolt.Margin = new System.Windows.Forms.Padding(4);
            this.btnInit_TestVolt.Name = "btnInit_TestVolt";
            this.btnInit_TestVolt.Size = new System.Drawing.Size(164, 48);
            this.btnInit_TestVolt.TabIndex = 8;
            this.btnInit_TestVolt.Text = "初始化";
            this.btnInit_TestVolt.UseVisualStyleBackColor = true;
            this.btnInit_TestVolt.Click += new System.EventHandler(this.btnInit_TestVolt_Click);
            // 
            // btnTestVolt
            // 
            this.btnTestVolt.Location = new System.Drawing.Point(267, 50);
            this.btnTestVolt.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestVolt.Name = "btnTestVolt";
            this.btnTestVolt.Size = new System.Drawing.Size(148, 48);
            this.btnTestVolt.TabIndex = 7;
            this.btnTestVolt.Text = "测试";
            this.btnTestVolt.UseVisualStyleBackColor = true;
            this.btnTestVolt.Click += new System.EventHandler(this.btnTestVolt_Click);
            // 
            // txtVolt
            // 
            this.txtVolt.Location = new System.Drawing.Point(43, 58);
            this.txtVolt.Margin = new System.Windows.Forms.Padding(4);
            this.txtVolt.Name = "txtVolt";
            this.txtVolt.Size = new System.Drawing.Size(149, 32);
            this.txtVolt.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.btnAdd_V);
            this.groupBox4.Controls.Add(this.btnSUB_V);
            this.groupBox4.Controls.Add(this.groupBox17);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.txtChannel);
            this.groupBox4.Location = new System.Drawing.Point(47, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(601, 708);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "通道切换";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(48, 552);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(405, 112);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "正极壳体测量 ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 38);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 52);
            this.button1.TabIndex = 6;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(55, 35);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 54);
            this.button2.TabIndex = 5;
            this.button2.Text = "切换";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAdd_V
            // 
            this.btnAdd_V.Location = new System.Drawing.Point(397, 50);
            this.btnAdd_V.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd_V.Name = "btnAdd_V";
            this.btnAdd_V.Size = new System.Drawing.Size(45, 32);
            this.btnAdd_V.TabIndex = 17;
            this.btnAdd_V.Text = "△";
            this.btnAdd_V.UseVisualStyleBackColor = true;
            this.btnAdd_V.Click += new System.EventHandler(this.btnAdd_V_Click);
            // 
            // btnSUB_V
            // 
            this.btnSUB_V.Location = new System.Drawing.Point(169, 50);
            this.btnSUB_V.Margin = new System.Windows.Forms.Padding(4);
            this.btnSUB_V.Name = "btnSUB_V";
            this.btnSUB_V.Size = new System.Drawing.Size(45, 32);
            this.btnSUB_V.TabIndex = 16;
            this.btnSUB_V.Text = "▽";
            this.btnSUB_V.UseVisualStyleBackColor = true;
            this.btnSUB_V.Click += new System.EventHandler(this.btnSUB_V_Click);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.btnSWCH_PosNeg);
            this.groupBox17.Controls.Add(this.btnStopSW_PosNeg);
            this.groupBox17.Location = new System.Drawing.Point(48, 118);
            this.groupBox17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox17.Size = new System.Drawing.Size(405, 122);
            this.groupBox17.TabIndex = 7;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "  正极对负极电压";
            // 
            // btnSWCH_PosNeg
            // 
            this.btnSWCH_PosNeg.Location = new System.Drawing.Point(55, 42);
            this.btnSWCH_PosNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnSWCH_PosNeg.Name = "btnSWCH_PosNeg";
            this.btnSWCH_PosNeg.Size = new System.Drawing.Size(123, 54);
            this.btnSWCH_PosNeg.TabIndex = 5;
            this.btnSWCH_PosNeg.Text = "切换";
            this.btnSWCH_PosNeg.UseVisualStyleBackColor = true;
            this.btnSWCH_PosNeg.Click += new System.EventHandler(this.btnSWCH_PosNeg_Click);
            // 
            // btnStopSW_PosNeg
            // 
            this.btnStopSW_PosNeg.Location = new System.Drawing.Point(227, 45);
            this.btnStopSW_PosNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopSW_PosNeg.Name = "btnStopSW_PosNeg";
            this.btnStopSW_PosNeg.Size = new System.Drawing.Size(123, 52);
            this.btnStopSW_PosNeg.TabIndex = 6;
            this.btnStopSW_PosNeg.Text = "关闭";
            this.btnStopSW_PosNeg.UseVisualStyleBackColor = true;
            this.btnStopSW_PosNeg.Click += new System.EventHandler(this.btnStopSW_PosNeg_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnStopSW_IRBT4);
            this.groupBox8.Controls.Add(this.btnSWCH_IRBT4);
            this.groupBox8.Location = new System.Drawing.Point(48, 395);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(405, 112);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "内阻测量 ";
            // 
            // btnStopSW_IRBT4
            // 
            this.btnStopSW_IRBT4.Location = new System.Drawing.Point(227, 38);
            this.btnStopSW_IRBT4.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopSW_IRBT4.Name = "btnStopSW_IRBT4";
            this.btnStopSW_IRBT4.Size = new System.Drawing.Size(123, 52);
            this.btnStopSW_IRBT4.TabIndex = 6;
            this.btnStopSW_IRBT4.Text = "关闭";
            this.btnStopSW_IRBT4.UseVisualStyleBackColor = true;
            this.btnStopSW_IRBT4.Click += new System.EventHandler(this.btnStopSW_IRBT4_Click);
            // 
            // btnSWCH_IRBT4
            // 
            this.btnSWCH_IRBT4.Location = new System.Drawing.Point(55, 35);
            this.btnSWCH_IRBT4.Margin = new System.Windows.Forms.Padding(4);
            this.btnSWCH_IRBT4.Name = "btnSWCH_IRBT4";
            this.btnSWCH_IRBT4.Size = new System.Drawing.Size(123, 54);
            this.btnSWCH_IRBT4.TabIndex = 5;
            this.btnSWCH_IRBT4.Text = "切换";
            this.btnSWCH_IRBT4.UseVisualStyleBackColor = true;
            this.btnSWCH_IRBT4.Click += new System.EventHandler(this.btnSWCH_IRBT4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "通道号：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnStopSW_ShellNeg);
            this.groupBox6.Controls.Add(this.btnSWCH_ShellNeg);
            this.groupBox6.Location = new System.Drawing.Point(48, 259);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(405, 122);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = " 壳体对负极电压";
            // 
            // btnStopSW_ShellNeg
            // 
            this.btnStopSW_ShellNeg.Location = new System.Drawing.Point(227, 48);
            this.btnStopSW_ShellNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopSW_ShellNeg.Name = "btnStopSW_ShellNeg";
            this.btnStopSW_ShellNeg.Size = new System.Drawing.Size(123, 52);
            this.btnStopSW_ShellNeg.TabIndex = 6;
            this.btnStopSW_ShellNeg.Text = "关闭";
            this.btnStopSW_ShellNeg.UseVisualStyleBackColor = true;
            this.btnStopSW_ShellNeg.Click += new System.EventHandler(this.btnStopSW_ShellNeg_Click);
            // 
            // btnSWCH_ShellNeg
            // 
            this.btnSWCH_ShellNeg.Location = new System.Drawing.Point(55, 46);
            this.btnSWCH_ShellNeg.Margin = new System.Windows.Forms.Padding(4);
            this.btnSWCH_ShellNeg.Name = "btnSWCH_ShellNeg";
            this.btnSWCH_ShellNeg.Size = new System.Drawing.Size(123, 54);
            this.btnSWCH_ShellNeg.TabIndex = 5;
            this.btnSWCH_ShellNeg.Text = "切换";
            this.btnSWCH_ShellNeg.UseVisualStyleBackColor = true;
            this.btnSWCH_ShellNeg.Click += new System.EventHandler(this.btnSWCH_ShellNeg_Click);
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(235, 50);
            this.txtChannel.Margin = new System.Windows.Forms.Padding(4);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(144, 32);
            this.txtChannel.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_ManualTest);
            this.tabControl1.Controls.Add(this.tab_MultiTest);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1472, 845);
            this.tabControl1.TabIndex = 119;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Number
            // 
            this.Number.HeaderText = "通道号";
            this.Number.Name = "Number";
            this.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Volt_PosNeg
            // 
            this.Volt_PosNeg.HeaderText = "正/负电压 (mV)";
            this.Volt_PosNeg.Name = "Volt_PosNeg";
            this.Volt_PosNeg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Volt_ShellNeg2
            // 
            this.Volt_ShellNeg2.HeaderText = "壳体/正电压(mV)";
            this.Volt_ShellNeg2.Name = "Volt_ShellNeg2";
            // 
            // Volt_ShellNeg
            // 
            this.Volt_ShellNeg.HeaderText = "壳体/负电压(mV)";
            this.Volt_ShellNeg.Name = "Volt_ShellNeg";
            this.Volt_ShellNeg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ACIR_BT4
            // 
            this.ACIR_BT4.HeaderText = "ACIR内阻(mΩ)";
            this.ACIR_BT4.Name = "ACIR_BT4";
            // 
            // TempData
            // 
            this.TempData.HeaderText = "温度+(C°)";
            this.TempData.Name = "TempData";
            this.TempData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TempData2
            // 
            this.TempData2.HeaderText = "温度-(C°)";
            this.TempData2.Name = "TempData2";
            // 
            // FrmManualTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1472, 845);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualTest";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmManualTestOCV_FormClosed);
            this.Load += new System.EventHandler(this.FrmOCV_Load);
            this.tab_MultiTest.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvManualTest)).EndInit();
            this.grpbxTestManual.ResumeLayout(false);
            this.grpbxTestManual.PerformLayout();
            this.tab_ManualTest.ResumeLayout(false);
            this.grpBT4560.ResumeLayout(false);
            this.grpBT4560.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tim_UI;
        private System.Windows.Forms.TabPage tab_MultiTest;
        private System.Windows.Forms.GroupBox grpbxTestManual;
        private System.Windows.Forms.TabPage tab_ManualTest;
        private System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.Label lblTestTime;
        private System.Windows.Forms.Button btnDmm_Init;
        private System.Windows.Forms.Button btnIRBT4_Init;
        public System.Windows.Forms.Button btnTestMultiIR_BT4560;
        public System.Windows.Forms.Button btnTestMultiVolt_PosNeg;
        public System.Windows.Forms.Button btnTestMultiVolt_ShellNeg;
        private System.Windows.Forms.Button btnStartTempTest;
        private System.Windows.Forms.Button btnSaveTest;
        private System.Windows.Forms.GroupBox grpBT4560;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInit_TestIRBT4;
        private System.Windows.Forms.Button btnTestIRBT4;
        private System.Windows.Forms.TextBox txtACIR;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInit_TestVolt;
        private System.Windows.Forms.Button btnTestVolt;
        private System.Windows.Forms.TextBox txtVolt;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAdd_V;
        private System.Windows.Forms.Button btnSUB_V;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Button btnSWCH_PosNeg;
        private System.Windows.Forms.Button btnStopSW_PosNeg;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnStopSW_IRBT4;
        private System.Windows.Forms.Button btnSWCH_IRBT4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnStopSW_ShellNeg;
        private System.Windows.Forms.Button btnSWCH_ShellNeg;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button btnPostiveSVTest;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dgvManualTest;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volt_PosNeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volt_ShellNeg2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volt_ShellNeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACIR_BT4;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempData;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempData2;
    }
}

