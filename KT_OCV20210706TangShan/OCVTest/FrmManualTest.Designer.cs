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
            this.txtInfoA = new System.Windows.Forms.TextBox();
            this.dgvManualTest = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volt_PosNeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volt_ShellNeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACIR_BT4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpbxTestManual = new System.Windows.Forms.GroupBox();
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
            this.tab_MultiTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManualTest)).BeginInit();
            this.grpbxTestManual.SuspendLayout();
            this.tab_ManualTest.SuspendLayout();
            this.grpBT4560.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.tab_MultiTest.Controls.Add(this.txtInfoA);
            this.tab_MultiTest.Controls.Add(this.dgvManualTest);
            this.tab_MultiTest.Controls.Add(this.grpbxTestManual);
            this.tab_MultiTest.Location = new System.Drawing.Point(4, 29);
            this.tab_MultiTest.Name = "tab_MultiTest";
            this.tab_MultiTest.Padding = new System.Windows.Forms.Padding(3);
            this.tab_MultiTest.Size = new System.Drawing.Size(1096, 709);
            this.tab_MultiTest.TabIndex = 3;
            this.tab_MultiTest.Text = "多通道手动测试";
            // 
            // txtInfoA
            // 
            this.txtInfoA.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtInfoA.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfoA.Location = new System.Drawing.Point(8, 487);
            this.txtInfoA.Multiline = true;
            this.txtInfoA.Name = "txtInfoA";
            this.txtInfoA.ReadOnly = true;
            this.txtInfoA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfoA.Size = new System.Drawing.Size(819, 209);
            this.txtInfoA.TabIndex = 179;
            // 
            // dgvManualTest
            // 
            this.dgvManualTest.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvManualTest.ColumnHeadersHeight = 30;
            this.dgvManualTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvManualTest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Volt_PosNeg,
            this.Volt_ShellNeg,
            this.ACIR_BT4,
            this.TempData,
            this.Column1});
            this.dgvManualTest.Location = new System.Drawing.Point(8, 15);
            this.dgvManualTest.Name = "dgvManualTest";
            this.dgvManualTest.RowTemplate.Height = 23;
            this.dgvManualTest.Size = new System.Drawing.Size(819, 460);
            this.dgvManualTest.TabIndex = 42;
            // 
            // Number
            // 
            this.Number.HeaderText = "通道号";
            this.Number.Name = "Number";
            this.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Number.Width = 85;
            // 
            // Volt_PosNeg
            // 
            this.Volt_PosNeg.HeaderText = "正/负电压 (mV)";
            this.Volt_PosNeg.Name = "Volt_PosNeg";
            this.Volt_PosNeg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Volt_PosNeg.Width = 150;
            // 
            // Volt_ShellNeg
            // 
            this.Volt_ShellNeg.HeaderText = "壳体/负电压(mV)";
            this.Volt_ShellNeg.Name = "Volt_ShellNeg";
            this.Volt_ShellNeg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Volt_ShellNeg.Width = 160;
            // 
            // ACIR_BT4
            // 
            this.ACIR_BT4.HeaderText = "ACIR内阻(mΩ)";
            this.ACIR_BT4.Name = "ACIR_BT4";
            this.ACIR_BT4.Width = 130;
            // 
            // TempData
            // 
            this.TempData.HeaderText = "温度+(C°)";
            this.TempData.Name = "TempData";
            this.TempData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempData.Width = 130;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "温度-(C°)";
            this.Column1.Name = "Column1";
            // 
            // grpbxTestManual
            // 
            this.grpbxTestManual.Controls.Add(this.btnStartTempTest);
            this.grpbxTestManual.Controls.Add(this.btnSaveTest);
            this.grpbxTestManual.Controls.Add(this.btnDmm_Init);
            this.grpbxTestManual.Controls.Add(this.btnIRBT4_Init);
            this.grpbxTestManual.Controls.Add(this.lblTestTime);
            this.grpbxTestManual.Controls.Add(this.btnTestMultiIR_BT4560);
            this.grpbxTestManual.Controls.Add(this.btnTestMultiVolt_PosNeg);
            this.grpbxTestManual.Controls.Add(this.btnTestMultiVolt_ShellNeg);
            this.grpbxTestManual.Enabled = false;
            this.grpbxTestManual.Location = new System.Drawing.Point(842, 6);
            this.grpbxTestManual.Name = "grpbxTestManual";
            this.grpbxTestManual.Size = new System.Drawing.Size(246, 469);
            this.grpbxTestManual.TabIndex = 41;
            this.grpbxTestManual.TabStop = false;
            this.grpbxTestManual.Text = "测试";
            // 
            // btnStartTempTest
            // 
            this.btnStartTempTest.Location = new System.Drawing.Point(31, 232);
            this.btnStartTempTest.Name = "btnStartTempTest";
            this.btnStartTempTest.Size = new System.Drawing.Size(183, 40);
            this.btnStartTempTest.TabIndex = 53;
            this.btnStartTempTest.Text = "测温度";
            this.btnStartTempTest.UseVisualStyleBackColor = true;
            this.btnStartTempTest.Visible = false;
            this.btnStartTempTest.Click += new System.EventHandler(this.btnStartTempTest_Click_1);
            // 
            // btnSaveTest
            // 
            this.btnSaveTest.Location = new System.Drawing.Point(31, 290);
            this.btnSaveTest.Name = "btnSaveTest";
            this.btnSaveTest.Size = new System.Drawing.Size(182, 40);
            this.btnSaveTest.TabIndex = 52;
            this.btnSaveTest.Text = "保存数据";
            this.btnSaveTest.UseVisualStyleBackColor = true;
            this.btnSaveTest.Click += new System.EventHandler(this.btnSaveTest_Click_1);
            // 
            // btnDmm_Init
            // 
            this.btnDmm_Init.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDmm_Init.Location = new System.Drawing.Point(31, 339);
            this.btnDmm_Init.Name = "btnDmm_Init";
            this.btnDmm_Init.Size = new System.Drawing.Size(182, 40);
            this.btnDmm_Init.TabIndex = 51;
            this.btnDmm_Init.Text = "万用表复位";
            this.btnDmm_Init.UseVisualStyleBackColor = true;
            this.btnDmm_Init.Click += new System.EventHandler(this.btnDmm_Init_Click);
            // 
            // btnIRBT4_Init
            // 
            this.btnIRBT4_Init.Location = new System.Drawing.Point(30, 385);
            this.btnIRBT4_Init.Name = "btnIRBT4_Init";
            this.btnIRBT4_Init.Size = new System.Drawing.Size(182, 40);
            this.btnIRBT4_Init.TabIndex = 50;
            this.btnIRBT4_Init.Text = "内阻仪复位";
            this.btnIRBT4_Init.UseVisualStyleBackColor = true;
            this.btnIRBT4_Init.Click += new System.EventHandler(this.btnIRBT4_Init_Click);
            // 
            // lblTestTime
            // 
            this.lblTestTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTestTime.Location = new System.Drawing.Point(30, 23);
            this.lblTestTime.Name = "lblTestTime";
            this.lblTestTime.Size = new System.Drawing.Size(184, 39);
            this.lblTestTime.TabIndex = 45;
            // 
            // btnTestMultiIR_BT4560
            // 
            this.btnTestMultiIR_BT4560.Location = new System.Drawing.Point(30, 184);
            this.btnTestMultiIR_BT4560.Name = "btnTestMultiIR_BT4560";
            this.btnTestMultiIR_BT4560.Size = new System.Drawing.Size(183, 37);
            this.btnTestMultiIR_BT4560.TabIndex = 48;
            this.btnTestMultiIR_BT4560.Text = "测内阻";
            this.btnTestMultiIR_BT4560.UseVisualStyleBackColor = true;
            this.btnTestMultiIR_BT4560.Click += new System.EventHandler(this.btnTestMultiIR_BT4560_Click);
            // 
            // btnTestMultiVolt_PosNeg
            // 
            this.btnTestMultiVolt_PosNeg.Location = new System.Drawing.Point(31, 83);
            this.btnTestMultiVolt_PosNeg.Name = "btnTestMultiVolt_PosNeg";
            this.btnTestMultiVolt_PosNeg.Size = new System.Drawing.Size(183, 37);
            this.btnTestMultiVolt_PosNeg.TabIndex = 46;
            this.btnTestMultiVolt_PosNeg.Text = "测正负极电压";
            this.btnTestMultiVolt_PosNeg.UseVisualStyleBackColor = true;
            this.btnTestMultiVolt_PosNeg.Click += new System.EventHandler(this.btnTestMultiVolt_PosNeg_Click);
            // 
            // btnTestMultiVolt_ShellNeg
            // 
            this.btnTestMultiVolt_ShellNeg.Location = new System.Drawing.Point(31, 133);
            this.btnTestMultiVolt_ShellNeg.Name = "btnTestMultiVolt_ShellNeg";
            this.btnTestMultiVolt_ShellNeg.Size = new System.Drawing.Size(183, 37);
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
            this.tab_ManualTest.Location = new System.Drawing.Point(4, 29);
            this.tab_ManualTest.Name = "tab_ManualTest";
            this.tab_ManualTest.Size = new System.Drawing.Size(1096, 709);
            this.tab_ManualTest.TabIndex = 2;
            this.tab_ManualTest.Text = "手动测试";
            // 
            // grpBT4560
            // 
            this.grpBT4560.Controls.Add(this.label2);
            this.grpBT4560.Controls.Add(this.btnInit_TestIRBT4);
            this.grpBT4560.Controls.Add(this.btnTestIRBT4);
            this.grpBT4560.Controls.Add(this.txtACIR);
            this.grpBT4560.Location = new System.Drawing.Point(505, 174);
            this.grpBT4560.Name = "grpBT4560";
            this.grpBT4560.Size = new System.Drawing.Size(478, 114);
            this.grpBT4560.TabIndex = 17;
            this.grpBT4560.TabStop = false;
            this.grpBT4560.Text = "读内阻仪";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(150, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = "mΩ";
            // 
            // btnInit_TestIRBT4
            // 
            this.btnInit_TestIRBT4.Location = new System.Drawing.Point(329, 40);
            this.btnInit_TestIRBT4.Name = "btnInit_TestIRBT4";
            this.btnInit_TestIRBT4.Size = new System.Drawing.Size(123, 38);
            this.btnInit_TestIRBT4.TabIndex = 8;
            this.btnInit_TestIRBT4.Text = "初始化";
            this.btnInit_TestIRBT4.UseVisualStyleBackColor = true;
            this.btnInit_TestIRBT4.Click += new System.EventHandler(this.btnInit_TestIRBT4_Click);
            // 
            // btnTestIRBT4
            // 
            this.btnTestIRBT4.Location = new System.Drawing.Point(200, 40);
            this.btnTestIRBT4.Name = "btnTestIRBT4";
            this.btnTestIRBT4.Size = new System.Drawing.Size(111, 38);
            this.btnTestIRBT4.TabIndex = 7;
            this.btnTestIRBT4.Text = "测试";
            this.btnTestIRBT4.UseVisualStyleBackColor = true;
            this.btnTestIRBT4.Click += new System.EventHandler(this.btnTestIRBT4_Click);
            // 
            // txtACIR
            // 
            this.txtACIR.Location = new System.Drawing.Point(32, 46);
            this.txtACIR.Name = "txtACIR";
            this.txtACIR.Size = new System.Drawing.Size(113, 27);
            this.txtACIR.TabIndex = 6;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.btnInit_TestVolt);
            this.groupBox5.Controls.Add(this.btnTestVolt);
            this.groupBox5.Controls.Add(this.txtVolt);
            this.groupBox5.Location = new System.Drawing.Point(505, 54);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(478, 114);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "读万用表";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(151, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "mV";
            // 
            // btnInit_TestVolt
            // 
            this.btnInit_TestVolt.Location = new System.Drawing.Point(329, 40);
            this.btnInit_TestVolt.Name = "btnInit_TestVolt";
            this.btnInit_TestVolt.Size = new System.Drawing.Size(123, 38);
            this.btnInit_TestVolt.TabIndex = 8;
            this.btnInit_TestVolt.Text = "初始化";
            this.btnInit_TestVolt.UseVisualStyleBackColor = true;
            this.btnInit_TestVolt.Click += new System.EventHandler(this.btnInit_TestVolt_Click);
            // 
            // btnTestVolt
            // 
            this.btnTestVolt.Location = new System.Drawing.Point(200, 40);
            this.btnTestVolt.Name = "btnTestVolt";
            this.btnTestVolt.Size = new System.Drawing.Size(111, 38);
            this.btnTestVolt.TabIndex = 7;
            this.btnTestVolt.Text = "测试";
            this.btnTestVolt.UseVisualStyleBackColor = true;
            this.btnTestVolt.Click += new System.EventHandler(this.btnTestVolt_Click);
            // 
            // txtVolt
            // 
            this.txtVolt.Location = new System.Drawing.Point(32, 46);
            this.txtVolt.Name = "txtVolt";
            this.txtVolt.Size = new System.Drawing.Size(113, 27);
            this.txtVolt.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnAdd_V);
            this.groupBox4.Controls.Add(this.btnSUB_V);
            this.groupBox4.Controls.Add(this.groupBox17);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.txtChannel);
            this.groupBox4.Location = new System.Drawing.Point(39, 44);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(451, 438);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "通道切换";
            // 
            // btnAdd_V
            // 
            this.btnAdd_V.Location = new System.Drawing.Point(298, 40);
            this.btnAdd_V.Name = "btnAdd_V";
            this.btnAdd_V.Size = new System.Drawing.Size(34, 26);
            this.btnAdd_V.TabIndex = 17;
            this.btnAdd_V.Text = "△";
            this.btnAdd_V.UseVisualStyleBackColor = true;
            this.btnAdd_V.Click += new System.EventHandler(this.btnAdd_V_Click);
            // 
            // btnSUB_V
            // 
            this.btnSUB_V.Location = new System.Drawing.Point(127, 40);
            this.btnSUB_V.Name = "btnSUB_V";
            this.btnSUB_V.Size = new System.Drawing.Size(34, 26);
            this.btnSUB_V.TabIndex = 16;
            this.btnSUB_V.Text = "▽";
            this.btnSUB_V.UseVisualStyleBackColor = true;
            this.btnSUB_V.Click += new System.EventHandler(this.btnSUB_V_Click);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.btnSWCH_PosNeg);
            this.groupBox17.Controls.Add(this.btnStopSW_PosNeg);
            this.groupBox17.Location = new System.Drawing.Point(36, 94);
            this.groupBox17.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox17.Size = new System.Drawing.Size(304, 98);
            this.groupBox17.TabIndex = 7;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "  正极对负极电压";
            // 
            // btnSWCH_PosNeg
            // 
            this.btnSWCH_PosNeg.Location = new System.Drawing.Point(41, 34);
            this.btnSWCH_PosNeg.Name = "btnSWCH_PosNeg";
            this.btnSWCH_PosNeg.Size = new System.Drawing.Size(92, 43);
            this.btnSWCH_PosNeg.TabIndex = 5;
            this.btnSWCH_PosNeg.Text = "切换";
            this.btnSWCH_PosNeg.UseVisualStyleBackColor = true;
            this.btnSWCH_PosNeg.Click += new System.EventHandler(this.btnSWCH_PosNeg_Click);
            // 
            // btnStopSW_PosNeg
            // 
            this.btnStopSW_PosNeg.Location = new System.Drawing.Point(170, 36);
            this.btnStopSW_PosNeg.Name = "btnStopSW_PosNeg";
            this.btnStopSW_PosNeg.Size = new System.Drawing.Size(92, 42);
            this.btnStopSW_PosNeg.TabIndex = 6;
            this.btnStopSW_PosNeg.Text = "关闭";
            this.btnStopSW_PosNeg.UseVisualStyleBackColor = true;
            this.btnStopSW_PosNeg.Click += new System.EventHandler(this.btnStopSW_PosNeg_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnStopSW_IRBT4);
            this.groupBox8.Controls.Add(this.btnSWCH_IRBT4);
            this.groupBox8.Location = new System.Drawing.Point(36, 316);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(304, 90);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "内阻测量 ";
            // 
            // btnStopSW_IRBT4
            // 
            this.btnStopSW_IRBT4.Location = new System.Drawing.Point(170, 30);
            this.btnStopSW_IRBT4.Name = "btnStopSW_IRBT4";
            this.btnStopSW_IRBT4.Size = new System.Drawing.Size(92, 42);
            this.btnStopSW_IRBT4.TabIndex = 6;
            this.btnStopSW_IRBT4.Text = "关闭";
            this.btnStopSW_IRBT4.UseVisualStyleBackColor = true;
            this.btnStopSW_IRBT4.Click += new System.EventHandler(this.btnStopSW_IRBT4_Click);
            // 
            // btnSWCH_IRBT4
            // 
            this.btnSWCH_IRBT4.Location = new System.Drawing.Point(41, 28);
            this.btnSWCH_IRBT4.Name = "btnSWCH_IRBT4";
            this.btnSWCH_IRBT4.Size = new System.Drawing.Size(92, 43);
            this.btnSWCH_IRBT4.TabIndex = 5;
            this.btnSWCH_IRBT4.Text = "切换";
            this.btnSWCH_IRBT4.UseVisualStyleBackColor = true;
            this.btnSWCH_IRBT4.Click += new System.EventHandler(this.btnSWCH_IRBT4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "通道号：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnStopSW_ShellNeg);
            this.groupBox6.Controls.Add(this.btnSWCH_ShellNeg);
            this.groupBox6.Location = new System.Drawing.Point(36, 207);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(304, 98);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = " 壳体对负极电压";
            // 
            // btnStopSW_ShellNeg
            // 
            this.btnStopSW_ShellNeg.Location = new System.Drawing.Point(170, 38);
            this.btnStopSW_ShellNeg.Name = "btnStopSW_ShellNeg";
            this.btnStopSW_ShellNeg.Size = new System.Drawing.Size(92, 42);
            this.btnStopSW_ShellNeg.TabIndex = 6;
            this.btnStopSW_ShellNeg.Text = "关闭";
            this.btnStopSW_ShellNeg.UseVisualStyleBackColor = true;
            this.btnStopSW_ShellNeg.Click += new System.EventHandler(this.btnStopSW_ShellNeg_Click);
            // 
            // btnSWCH_ShellNeg
            // 
            this.btnSWCH_ShellNeg.Location = new System.Drawing.Point(41, 37);
            this.btnSWCH_ShellNeg.Name = "btnSWCH_ShellNeg";
            this.btnSWCH_ShellNeg.Size = new System.Drawing.Size(92, 43);
            this.btnSWCH_ShellNeg.TabIndex = 5;
            this.btnSWCH_ShellNeg.Text = "切换";
            this.btnSWCH_ShellNeg.UseVisualStyleBackColor = true;
            this.btnSWCH_ShellNeg.Click += new System.EventHandler(this.btnSWCH_ShellNeg_Click);
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(176, 40);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(109, 27);
            this.txtChannel.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_ManualTest);
            this.tabControl1.Controls.Add(this.tab_MultiTest);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1104, 742);
            this.tabControl1.TabIndex = 119;
            // 
            // FrmManualTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1104, 742);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualTest";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmManualTestOCV_FormClosed);
            this.Load += new System.EventHandler(this.FrmOCV_Load);
            this.tab_MultiTest.ResumeLayout(false);
            this.tab_MultiTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManualTest)).EndInit();
            this.grpbxTestManual.ResumeLayout(false);
            this.tab_ManualTest.ResumeLayout(false);
            this.grpBT4560.ResumeLayout(false);
            this.grpBT4560.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        public System.Windows.Forms.DataGridView dgvManualTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volt_PosNeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volt_ShellNeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACIR_BT4;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.TextBox txtInfoA;
    }
}

