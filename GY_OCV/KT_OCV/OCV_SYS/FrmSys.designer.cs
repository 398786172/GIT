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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSys));
            this.tim_UI = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvTest = new System.Windows.Forms.DataGridView();
            this.Col_Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_OCV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_CaseOCV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_CaseOCV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_ACIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_TEMP_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_TEMP_N = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sscMain = new CCWin.SkinControl.SkinSplitContainer();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.txtInfoA = new System.Windows.Forms.TextBox();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPLCError = new DevComponents.DotNetBar.TabItem(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtTraycodeA = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.gbManual = new System.Windows.Forms.GroupBox();
            this.chbPShellVol = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbNShellVol = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbACIR = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chbVol = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnManualStart = new CCWin.SkinControl.SkinButton();
            this.rbOCV3 = new System.Windows.Forms.RadioButton();
            this.rbOCV2 = new System.Windows.Forms.RadioButton();
            this.lblHaveGoods = new ClsDeviceComm.Controls.UserLantern();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.skinButton3 = new CCWin.SkinControl.SkinButton();
            this.txtPlc_ResetStepNO = new System.Windows.Forms.TextBox();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.skinButton2 = new CCWin.SkinControl.SkinButton();
            this.lblBhCVAllow = new ClsDeviceComm.Controls.UserLantern();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.Btn_A_stop = new CCWin.SkinControl.SkinButton();
            this.Btn_A_RUN = new CCWin.SkinControl.SkinButton();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblBhOCVReq = new ClsDeviceComm.Controls.UserLantern();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lblFrOCVAllow = new ClsDeviceComm.Controls.UserLantern();
            this.lblFrCVRequest = new ClsDeviceComm.Controls.UserLantern();
            this.lblAuto = new ClsDeviceComm.Controls.UserLantern();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.lblManual = new ClsDeviceComm.Controls.UserLantern();
            this.labelX47 = new DevComponents.DotNetBar.LabelX();
            this.labelX45 = new DevComponents.DotNetBar.LabelX();
            this.txtPlc_AutoStepNO = new System.Windows.Forms.TextBox();
            this.labelX36 = new DevComponents.DotNetBar.LabelX();
            this.btnRunMode = new ClsDeviceComm.Controls.UserSwitch();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssMachineId = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTestCH = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssOperatorId = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTestType = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssRunMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IOToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.工程设置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.手动测试ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.手动校准toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.通道异常统计toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.数据补传ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.密码修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.校准设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.型号设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动输入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTest)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sscMain)).BeginInit();
            this.sscMain.Panel1.SuspendLayout();
            this.sscMain.Panel2.SuspendLayout();
            this.sscMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.gbManual.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tim_UI
            // 
            this.tim_UI.Enabled = true;
            this.tim_UI.Tick += new System.EventHandler(this.tim_UI_Tick);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.splitContainer1);
            this.panel6.Controls.Add(this.statusStrip1);
            this.panel6.Controls.Add(this.panel2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(4, 28);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1378, 700);
            this.panel6.TabIndex = 172;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvTest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1378, 639);
            this.splitContainer1.SplitterDistance = 945;
            this.splitContainer1.TabIndex = 170;
            // 
            // dgvTest
            // 
            this.dgvTest.AllowUserToAddRows = false;
            this.dgvTest.AllowUserToResizeColumns = false;
            this.dgvTest.AllowUserToResizeRows = false;
            this.dgvTest.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTest.ColumnHeadersHeight = 25;
            this.dgvTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Num,
            this.SFC,
            this.Col_OCV,
            this.Col_CaseOCV2,
            this.Col_CaseOCV,
            this.Col_ACIR,
            this.Col_TEMP_P,
            this.Col_TEMP_N,
            this.Col_CODE,
            this.Col_Des});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTest.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTest.Location = new System.Drawing.Point(0, 0);
            this.dgvTest.Name = "dgvTest";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTest.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvTest.RowHeadersVisible = false;
            this.dgvTest.RowHeadersWidth = 30;
            this.dgvTest.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.dgvTest.RowTemplate.Height = 17;
            this.dgvTest.RowTemplate.ReadOnly = true;
            this.dgvTest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvTest.Size = new System.Drawing.Size(945, 639);
            this.dgvTest.TabIndex = 197;
            this.dgvTest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTest_CellContentClick);
            // 
            // Col_Num
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Col_Num.DefaultCellStyle = dataGridViewCellStyle6;
            this.Col_Num.HeaderText = "N0.";
            this.Col_Num.Name = "Col_Num";
            this.Col_Num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_Num.Width = 35;
            // 
            // SFC
            // 
            this.SFC.HeaderText = "电池条码";
            this.SFC.Name = "SFC";
            this.SFC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SFC.Width = 190;
            // 
            // Col_OCV
            // 
            this.Col_OCV.HeaderText = "电压(mV)";
            this.Col_OCV.Name = "Col_OCV";
            this.Col_OCV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_CaseOCV2
            // 
            this.Col_CaseOCV2.HeaderText = "壳体\\正极电压(mV)";
            this.Col_CaseOCV2.Name = "Col_CaseOCV2";
            this.Col_CaseOCV2.Width = 120;
            // 
            // Col_CaseOCV
            // 
            this.Col_CaseOCV.HeaderText = "壳体\\负极电压(mV)";
            this.Col_CaseOCV.Name = "Col_CaseOCV";
            this.Col_CaseOCV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_CaseOCV.Width = 120;
            // 
            // Col_ACIR
            // 
            this.Col_ACIR.HeaderText = "ACIR(mΩ)";
            this.Col_ACIR.Name = "Col_ACIR";
            this.Col_ACIR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_TEMP_P
            // 
            this.Col_TEMP_P.HeaderText = "温度+(℃)";
            this.Col_TEMP_P.Name = "Col_TEMP_P";
            this.Col_TEMP_P.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_TEMP_P.Width = 80;
            // 
            // Col_TEMP_N
            // 
            this.Col_TEMP_N.HeaderText = "温度-(℃)";
            this.Col_TEMP_N.Name = "Col_TEMP_N";
            this.Col_TEMP_N.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_TEMP_N.Width = 80;
            // 
            // Col_CODE
            // 
            this.Col_CODE.HeaderText = "NG代码";
            this.Col_CODE.Name = "Col_CODE";
            this.Col_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_CODE.Width = 80;
            // 
            // Col_Des
            // 
            this.Col_Des.HeaderText = "NG描述";
            this.Col_Des.Name = "Col_Des";
            this.Col_Des.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_Des.Width = 260;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sscMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 639);
            this.panel1.TabIndex = 196;
            // 
            // sscMain
            // 
            this.sscMain.CollapsePanel = CCWin.SkinControl.CollapsePanel.Panel2;
            this.sscMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.sscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sscMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sscMain.LineBack = System.Drawing.Color.Black;
            this.sscMain.LineBack2 = System.Drawing.Color.LightCyan;
            this.sscMain.Location = new System.Drawing.Point(0, 0);
            this.sscMain.Name = "sscMain";
            this.sscMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sscMain.Panel1
            // 
            this.sscMain.Panel1.Controls.Add(this.tabControl1);
            this.sscMain.Panel1.Controls.Add(this.panel4);
            // 
            // sscMain.Panel2
            // 
            this.sscMain.Panel2.Controls.Add(this.groupPanel1);
            this.sscMain.Panel2MinSize = 0;
            this.sscMain.Size = new System.Drawing.Size(427, 637);
            this.sscMain.SplitterDistance = 268;
            this.sscMain.SplitterWidth = 10;
            this.sscMain.TabIndex = 200;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Location = new System.Drawing.Point(4, 38);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(420, 227);
            this.tabControl1.TabIndex = 182;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabPLCError);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.txtInfoA);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(420, 201);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // txtInfoA
            // 
            this.txtInfoA.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtInfoA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfoA.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfoA.Location = new System.Drawing.Point(1, 1);
            this.txtInfoA.Multiline = true;
            this.txtInfoA.Name = "txtInfoA";
            this.txtInfoA.ReadOnly = true;
            this.txtInfoA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfoA.Size = new System.Drawing.Size(418, 199);
            this.txtInfoA.TabIndex = 181;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "运行信息";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.listBox1);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(420, 201);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.SystemColors.ButtonFace;
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.SystemColors.ButtonFace;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = -90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabPLCError;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.ForeColor = System.Drawing.Color.Red;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(1, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(418, 199);
            this.listBox1.TabIndex = 0;
            // 
            // tabPLCError
            // 
            this.tabPLCError.AttachedControl = this.tabControlPanel2;
            this.tabPLCError.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPLCError.BackColor2 = System.Drawing.SystemColors.ButtonFace;
            this.tabPLCError.Name = "tabPLCError";
            this.tabPLCError.Text = "PLC报警信息";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtTraycodeA);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(427, 32);
            this.panel4.TabIndex = 181;
            // 
            // txtTraycodeA
            // 
            this.txtTraycodeA.BackColor = System.Drawing.Color.Transparent;
            this.txtTraycodeA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTraycodeA.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTraycodeA.Location = new System.Drawing.Point(97, 0);
            this.txtTraycodeA.Name = "txtTraycodeA";
            this.txtTraycodeA.Size = new System.Drawing.Size(330, 32);
            this.txtTraycodeA.TabIndex = 178;
            this.txtTraycodeA.Text = "label1";
            this.txtTraycodeA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 32);
            this.label2.TabIndex = 195;
            this.label2.Text = "托盘条码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.groupPanel2);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(427, 359);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 2;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 259;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.label3);
            this.groupPanel2.Controls.Add(this.label1);
            this.groupPanel2.Controls.Add(this.txtPeriod);
            this.groupPanel2.Controls.Add(this.gbManual);
            this.groupPanel2.Controls.Add(this.lblHaveGoods);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.Controls.Add(this.skinButton3);
            this.groupPanel2.Controls.Add(this.txtPlc_ResetStepNO);
            this.groupPanel2.Controls.Add(this.labelX5);
            this.groupPanel2.Controls.Add(this.skinButton2);
            this.groupPanel2.Controls.Add(this.lblBhCVAllow);
            this.groupPanel2.Controls.Add(this.skinButton1);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.Btn_A_stop);
            this.groupPanel2.Controls.Add(this.Btn_A_RUN);
            this.groupPanel2.Controls.Add(this.labelX4);
            this.groupPanel2.Controls.Add(this.lblBhOCVReq);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Controls.Add(this.lblFrOCVAllow);
            this.groupPanel2.Controls.Add(this.lblFrCVRequest);
            this.groupPanel2.Controls.Add(this.lblAuto);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.lblManual);
            this.groupPanel2.Controls.Add(this.labelX47);
            this.groupPanel2.Controls.Add(this.labelX45);
            this.groupPanel2.Controls.Add(this.txtPlc_AutoStepNO);
            this.groupPanel2.Controls.Add(this.labelX36);
            this.groupPanel2.Controls.Add(this.btnRunMode);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(0, 0);
            this.groupPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.ShowFocusRectangle = true;
            this.groupPanel2.Size = new System.Drawing.Size(421, 353);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor = System.Drawing.Color.Lavender;
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel2.TabIndex = 257;
            this.groupPanel2.Text = "机械控制";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 17);
            this.label3.TabIndex = 268;
            this.label3.Text = "秒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 267;
            this.label1.Text = "生产周期";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(87, 187);
            this.txtPeriod.Margin = new System.Windows.Forms.Padding(2);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.ReadOnly = true;
            this.txtPeriod.Size = new System.Drawing.Size(78, 23);
            this.txtPeriod.TabIndex = 266;
            this.txtPeriod.Text = "0";
            // 
            // gbManual
            // 
            this.gbManual.Controls.Add(this.chbPShellVol);
            this.gbManual.Controls.Add(this.chbNShellVol);
            this.gbManual.Controls.Add(this.chbACIR);
            this.gbManual.Controls.Add(this.chbVol);
            this.gbManual.Controls.Add(this.btnManualStart);
            this.gbManual.Controls.Add(this.rbOCV3);
            this.gbManual.Controls.Add(this.rbOCV2);
            this.gbManual.Location = new System.Drawing.Point(4, 224);
            this.gbManual.Name = "gbManual";
            this.gbManual.Size = new System.Drawing.Size(429, 78);
            this.gbManual.TabIndex = 265;
            this.gbManual.TabStop = false;
            this.gbManual.Text = "手动测试";
            // 
            // chbPShellVol
            // 
            this.chbPShellVol.Location = new System.Drawing.Point(116, 49);
            this.chbPShellVol.Name = "chbPShellVol";
            this.chbPShellVol.Size = new System.Drawing.Size(91, 23);
            this.chbPShellVol.TabIndex = 274;
            this.chbPShellVol.Text = "壳体对正极";
            // 
            // chbNShellVol
            // 
            this.chbNShellVol.Location = new System.Drawing.Point(11, 49);
            this.chbNShellVol.Name = "chbNShellVol";
            this.chbNShellVol.Size = new System.Drawing.Size(91, 23);
            this.chbNShellVol.TabIndex = 273;
            this.chbNShellVol.Text = "壳体对负极";
            // 
            // chbACIR
            // 
            this.chbACIR.Location = new System.Drawing.Point(116, 22);
            this.chbACIR.Name = "chbACIR";
            this.chbACIR.Size = new System.Drawing.Size(91, 23);
            this.chbACIR.TabIndex = 272;
            this.chbACIR.Text = "ACIR";
            // 
            // chbVol
            // 
            this.chbVol.Location = new System.Drawing.Point(11, 22);
            this.chbVol.Name = "chbVol";
            this.chbVol.Size = new System.Drawing.Size(91, 23);
            this.chbVol.TabIndex = 271;
            this.chbVol.Text = "电压";
            // 
            // btnManualStart
            // 
            this.btnManualStart.BackColor = System.Drawing.Color.Transparent;
            this.btnManualStart.BaseColor = System.Drawing.Color.LawnGreen;
            this.btnManualStart.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.btnManualStart.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnManualStart.DownBack = null;
            this.btnManualStart.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManualStart.GlowColor = System.Drawing.Color.Silver;
            this.btnManualStart.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.btnManualStart.Location = new System.Drawing.Point(317, 28);
            this.btnManualStart.MouseBack = null;
            this.btnManualStart.MouseBaseColor = System.Drawing.Color.Teal;
            this.btnManualStart.Name = "btnManualStart";
            this.btnManualStart.NormlBack = null;
            this.btnManualStart.Size = new System.Drawing.Size(101, 44);
            this.btnManualStart.TabIndex = 270;
            this.btnManualStart.Text = "启动测试";
            this.btnManualStart.UseVisualStyleBackColor = false;
            this.btnManualStart.Click += new System.EventHandler(this.btnManualStart_Click);
            // 
            // rbOCV3
            // 
            this.rbOCV3.AutoSize = true;
            this.rbOCV3.BackColor = System.Drawing.Color.Transparent;
            this.rbOCV3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOCV3.Location = new System.Drawing.Point(217, 45);
            this.rbOCV3.Name = "rbOCV3";
            this.rbOCV3.Size = new System.Drawing.Size(83, 21);
            this.rbOCV3.TabIndex = 269;
            this.rbOCV3.TabStop = true;
            this.rbOCV3.Text = "OCV3测试";
            this.rbOCV3.UseVisualStyleBackColor = false;
            this.rbOCV3.CheckedChanged += new System.EventHandler(this.rbOCV3_CheckedChanged);
            // 
            // rbOCV2
            // 
            this.rbOCV2.AutoSize = true;
            this.rbOCV2.BackColor = System.Drawing.Color.Transparent;
            this.rbOCV2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOCV2.Location = new System.Drawing.Point(217, 18);
            this.rbOCV2.Name = "rbOCV2";
            this.rbOCV2.Size = new System.Drawing.Size(83, 21);
            this.rbOCV2.TabIndex = 268;
            this.rbOCV2.TabStop = true;
            this.rbOCV2.Text = "OCV2测试";
            this.rbOCV2.UseVisualStyleBackColor = false;
            this.rbOCV2.CheckedChanged += new System.EventHandler(this.rbOCV2_CheckedChanged);
            // 
            // lblHaveGoods
            // 
            this.lblHaveGoods.AutoSize = true;
            this.lblHaveGoods.BackColor = System.Drawing.Color.Transparent;
            this.lblHaveGoods.LanternBackground = System.Drawing.Color.Gray;
            this.lblHaveGoods.Location = new System.Drawing.Point(22, 26);
            this.lblHaveGoods.Margin = new System.Windows.Forms.Padding(101, 776, 101, 776);
            this.lblHaveGoods.Name = "lblHaveGoods";
            this.lblHaveGoods.Size = new System.Drawing.Size(27, 27);
            this.lblHaveGoods.TabIndex = 264;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(19, 2);
            this.labelX6.Margin = new System.Windows.Forms.Padding(2);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(38, 22);
            this.labelX6.TabIndex = 263;
            this.labelX6.Text = "有料";
            // 
            // skinButton3
            // 
            this.skinButton3.BackColor = System.Drawing.Color.Transparent;
            this.skinButton3.BaseColor = System.Drawing.Color.Chocolate;
            this.skinButton3.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.skinButton3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton3.DownBack = null;
            this.skinButton3.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton3.GlowColor = System.Drawing.Color.Silver;
            this.skinButton3.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.skinButton3.Location = new System.Drawing.Point(111, 132);
            this.skinButton3.MouseBack = null;
            this.skinButton3.MouseBaseColor = System.Drawing.Color.Teal;
            this.skinButton3.Name = "skinButton3";
            this.skinButton3.NormlBack = null;
            this.skinButton3.Size = new System.Drawing.Size(81, 36);
            this.skinButton3.TabIndex = 262;
            this.skinButton3.Text = "初始化";
            this.skinButton3.UseVisualStyleBackColor = false;
            this.skinButton3.Click += new System.EventHandler(this.skinButton3_Click);
            // 
            // txtPlc_ResetStepNO
            // 
            this.txtPlc_ResetStepNO.Location = new System.Drawing.Point(22, 146);
            this.txtPlc_ResetStepNO.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlc_ResetStepNO.Name = "txtPlc_ResetStepNO";
            this.txtPlc_ResetStepNO.ReadOnly = true;
            this.txtPlc_ResetStepNO.Size = new System.Drawing.Size(46, 23);
            this.txtPlc_ResetStepNO.TabIndex = 261;
            this.txtPlc_ResetStepNO.TextChanged += new System.EventHandler(this.txtPlc_ResetStepNO_TextChanged);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(14, 121);
            this.labelX5.Margin = new System.Windows.Forms.Padding(2);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(89, 18);
            this.labelX5.TabIndex = 260;
            this.labelX5.Text = "初始化工步";
            // 
            // skinButton2
            // 
            this.skinButton2.BackColor = System.Drawing.Color.Transparent;
            this.skinButton2.BaseColor = System.Drawing.Color.Chocolate;
            this.skinButton2.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.skinButton2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton2.DownBack = null;
            this.skinButton2.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton2.GlowColor = System.Drawing.Color.Silver;
            this.skinButton2.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.skinButton2.Location = new System.Drawing.Point(310, 75);
            this.skinButton2.MouseBack = null;
            this.skinButton2.MouseBaseColor = System.Drawing.Color.Teal;
            this.skinButton2.Name = "skinButton2";
            this.skinButton2.NormlBack = null;
            this.skinButton2.Size = new System.Drawing.Size(81, 36);
            this.skinButton2.TabIndex = 181;
            this.skinButton2.Text = "整体复位";
            this.skinButton2.UseVisualStyleBackColor = false;
            this.skinButton2.Click += new System.EventHandler(this.skinButton2_Click);
            // 
            // lblBhCVAllow
            // 
            this.lblBhCVAllow.AutoSize = true;
            this.lblBhCVAllow.BackColor = System.Drawing.Color.Transparent;
            this.lblBhCVAllow.LanternBackground = System.Drawing.Color.Gray;
            this.lblBhCVAllow.Location = new System.Drawing.Point(262, 26);
            this.lblBhCVAllow.Margin = new System.Windows.Forms.Padding(142, 1653, 142, 1653);
            this.lblBhCVAllow.Name = "lblBhCVAllow";
            this.lblBhCVAllow.Size = new System.Drawing.Size(27, 27);
            this.lblBhCVAllow.TabIndex = 259;
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.Goldenrod;
            this.skinButton1.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.GlowColor = System.Drawing.Color.Silver;
            this.skinButton1.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.skinButton1.Location = new System.Drawing.Point(210, 75);
            this.skinButton1.MouseBack = null;
            this.skinButton1.MouseBaseColor = System.Drawing.Color.Teal;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(81, 36);
            this.skinButton1.TabIndex = 180;
            this.skinButton1.Text = "清除报警";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(383, 3);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(36, 21);
            this.labelX1.TabIndex = 160;
            this.labelX1.Text = "自动";
            // 
            // Btn_A_stop
            // 
            this.Btn_A_stop.BackColor = System.Drawing.Color.Transparent;
            this.Btn_A_stop.BaseColor = System.Drawing.Color.MediumSlateBlue;
            this.Btn_A_stop.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.Btn_A_stop.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.Btn_A_stop.DownBack = null;
            this.Btn_A_stop.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_A_stop.GlowColor = System.Drawing.Color.Silver;
            this.Btn_A_stop.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.Btn_A_stop.Location = new System.Drawing.Point(310, 132);
            this.Btn_A_stop.MouseBack = null;
            this.Btn_A_stop.MouseBaseColor = System.Drawing.Color.Teal;
            this.Btn_A_stop.Name = "Btn_A_stop";
            this.Btn_A_stop.NormlBack = null;
            this.Btn_A_stop.Size = new System.Drawing.Size(81, 36);
            this.Btn_A_stop.TabIndex = 178;
            this.Btn_A_stop.Text = "停止";
            this.Btn_A_stop.UseVisualStyleBackColor = false;
            this.Btn_A_stop.Click += new System.EventHandler(this.Btn_A_stop_Click);
            // 
            // Btn_A_RUN
            // 
            this.Btn_A_RUN.BackColor = System.Drawing.Color.Transparent;
            this.Btn_A_RUN.BaseColor = System.Drawing.Color.Green;
            this.Btn_A_RUN.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.Btn_A_RUN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.Btn_A_RUN.DownBack = null;
            this.Btn_A_RUN.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_A_RUN.GlowColor = System.Drawing.Color.Silver;
            this.Btn_A_RUN.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.Btn_A_RUN.Location = new System.Drawing.Point(208, 132);
            this.Btn_A_RUN.MouseBack = null;
            this.Btn_A_RUN.MouseBaseColor = System.Drawing.Color.Teal;
            this.Btn_A_RUN.Name = "Btn_A_RUN";
            this.Btn_A_RUN.NormlBack = null;
            this.Btn_A_RUN.Size = new System.Drawing.Size(81, 36);
            this.Btn_A_RUN.TabIndex = 179;
            this.Btn_A_RUN.Text = "启动";
            this.Btn_A_RUN.UseVisualStyleBackColor = false;
            this.Btn_A_RUN.Click += new System.EventHandler(this.Btn_A_RUN_Click);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(251, 3);
            this.labelX4.Margin = new System.Windows.Forms.Padding(2);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(64, 18);
            this.labelX4.TabIndex = 258;
            this.labelX4.Text = "允许下料";
            // 
            // lblBhOCVReq
            // 
            this.lblBhOCVReq.AutoSize = true;
            this.lblBhOCVReq.BackColor = System.Drawing.Color.Transparent;
            this.lblBhOCVReq.LanternBackground = System.Drawing.Color.Gray;
            this.lblBhOCVReq.Location = new System.Drawing.Point(199, 26);
            this.lblBhOCVReq.Margin = new System.Windows.Forms.Padding(122, 1167, 122, 1167);
            this.lblBhOCVReq.Name = "lblBhOCVReq";
            this.lblBhOCVReq.Size = new System.Drawing.Size(27, 27);
            this.lblBhOCVReq.TabIndex = 257;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(322, 3);
            this.labelX2.Margin = new System.Windows.Forms.Padding(2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(36, 21);
            this.labelX2.TabIndex = 161;
            this.labelX2.Text = "手动";
            // 
            // lblFrOCVAllow
            // 
            this.lblFrOCVAllow.AutoSize = true;
            this.lblFrOCVAllow.BackColor = System.Drawing.Color.Transparent;
            this.lblFrOCVAllow.LanternBackground = System.Drawing.Color.Gray;
            this.lblFrOCVAllow.Location = new System.Drawing.Point(138, 26);
            this.lblFrOCVAllow.Margin = new System.Windows.Forms.Padding(105, 824, 105, 824);
            this.lblFrOCVAllow.Name = "lblFrOCVAllow";
            this.lblFrOCVAllow.Size = new System.Drawing.Size(27, 27);
            this.lblFrOCVAllow.TabIndex = 257;
            // 
            // lblFrCVRequest
            // 
            this.lblFrCVRequest.AutoSize = true;
            this.lblFrCVRequest.BackColor = System.Drawing.Color.Transparent;
            this.lblFrCVRequest.LanternBackground = System.Drawing.Color.Gray;
            this.lblFrCVRequest.Location = new System.Drawing.Point(76, 26);
            this.lblFrCVRequest.Margin = new System.Windows.Forms.Padding(90, 582, 90, 582);
            this.lblFrCVRequest.Name = "lblFrCVRequest";
            this.lblFrCVRequest.Size = new System.Drawing.Size(27, 27);
            this.lblFrCVRequest.TabIndex = 255;
            // 
            // lblAuto
            // 
            this.lblAuto.AutoSize = true;
            this.lblAuto.BackColor = System.Drawing.Color.Transparent;
            this.lblAuto.LanternBackground = System.Drawing.Color.Gray;
            this.lblAuto.Location = new System.Drawing.Point(383, 26);
            this.lblAuto.Margin = new System.Windows.Forms.Padding(77, 411, 77, 411);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(27, 27);
            this.lblAuto.TabIndex = 254;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(187, 3);
            this.labelX3.Margin = new System.Windows.Forms.Padding(2);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(70, 18);
            this.labelX3.TabIndex = 26;
            this.labelX3.Text = "请求下料";
            // 
            // lblManual
            // 
            this.lblManual.BackColor = System.Drawing.Color.Transparent;
            this.lblManual.LanternBackground = System.Drawing.Color.Gray;
            this.lblManual.Location = new System.Drawing.Point(322, 26);
            this.lblManual.Margin = new System.Windows.Forms.Padding(66, 290, 66, 290);
            this.lblManual.Name = "lblManual";
            this.lblManual.Size = new System.Drawing.Size(27, 27);
            this.lblManual.TabIndex = 253;
            // 
            // labelX47
            // 
            this.labelX47.BackColor = System.Drawing.Color.Transparent;
            this.labelX47.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX47.Location = new System.Drawing.Point(124, 3);
            this.labelX47.Margin = new System.Windows.Forms.Padding(2);
            this.labelX47.Name = "labelX47";
            this.labelX47.Size = new System.Drawing.Size(70, 18);
            this.labelX47.TabIndex = 26;
            this.labelX47.Text = "允许入料";
            // 
            // labelX45
            // 
            this.labelX45.BackColor = System.Drawing.Color.Transparent;
            this.labelX45.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX45.Location = new System.Drawing.Point(64, 3);
            this.labelX45.Margin = new System.Windows.Forms.Padding(2);
            this.labelX45.Name = "labelX45";
            this.labelX45.Size = new System.Drawing.Size(61, 18);
            this.labelX45.TabIndex = 24;
            this.labelX45.Text = "请求入料";
            // 
            // txtPlc_AutoStepNO
            // 
            this.txtPlc_AutoStepNO.Location = new System.Drawing.Point(22, 90);
            this.txtPlc_AutoStepNO.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlc_AutoStepNO.Name = "txtPlc_AutoStepNO";
            this.txtPlc_AutoStepNO.ReadOnly = true;
            this.txtPlc_AutoStepNO.Size = new System.Drawing.Size(46, 23);
            this.txtPlc_AutoStepNO.TabIndex = 40;
            // 
            // labelX36
            // 
            this.labelX36.BackColor = System.Drawing.Color.Transparent;
            this.labelX36.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX36.Location = new System.Drawing.Point(19, 65);
            this.labelX36.Margin = new System.Windows.Forms.Padding(2);
            this.labelX36.Name = "labelX36";
            this.labelX36.Size = new System.Drawing.Size(84, 19);
            this.labelX36.TabIndex = 23;
            this.labelX36.Text = "自动工步";
            // 
            // btnRunMode
            // 
            this.btnRunMode.BackColor = System.Drawing.Color.Transparent;
            this.btnRunMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRunMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunMode.Location = new System.Drawing.Point(108, 52);
            this.btnRunMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRunMode.Name = "btnRunMode";
            this.btnRunMode.Size = new System.Drawing.Size(86, 88);
            this.btnRunMode.SwitchBackground = System.Drawing.Color.SteelBlue;
            this.btnRunMode.SwitchForeground = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btnRunMode.SwitchStatusDescription = new string[] {
        "手动",
        "自动"};
            this.btnRunMode.TabIndex = 57;
            this.btnRunMode.OnSwitchChanged += new System.Action<object, bool>(this.btnRunMode_OnSwitchChanged);
            this.btnRunMode.Click += new System.EventHandler(this.btnRunMode_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Lavender;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.tssMachineId,
            this.toolStripStatusLabel2,
            this.tssTestCH,
            this.toolStripStatusLabel1,
            this.tssOperatorId,
            this.toolStripStatusLabel5,
            this.tssTestType,
            this.toolStripStatusLabel4,
            this.tssRunMode});
            this.statusStrip1.Location = new System.Drawing.Point(0, 671);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1378, 29);
            this.statusStrip1.TabIndex = 208;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(68, 24);
            this.toolStripStatusLabel3.Text = "设备编号：";
            // 
            // tssMachineId
            // 
            this.tssMachineId.AutoSize = false;
            this.tssMachineId.BackColor = System.Drawing.Color.Lime;
            this.tssMachineId.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssMachineId.Name = "tssMachineId";
            this.tssMachineId.Size = new System.Drawing.Size(150, 24);
            this.tssMachineId.Text = "CQ-BYD-01-OCV2-01";
            this.tssMachineId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(72, 24);
            this.toolStripStatusLabel2.Text = "    通道数：";
            // 
            // tssTestCH
            // 
            this.tssTestCH.AutoSize = false;
            this.tssTestCH.BackColor = System.Drawing.Color.Lime;
            this.tssTestCH.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tssTestCH.Name = "tssTestCH";
            this.tssTestCH.Size = new System.Drawing.Size(60, 24);
            this.tssTestCH.Text = "16通道";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 24);
            this.toolStripStatusLabel1.Text = "    测试工序：";
            // 
            // tssOperatorId
            // 
            this.tssOperatorId.AutoSize = false;
            this.tssOperatorId.BackColor = System.Drawing.Color.Lime;
            this.tssOperatorId.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssOperatorId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tssOperatorId.Name = "tssOperatorId";
            this.tssOperatorId.Size = new System.Drawing.Size(100, 24);
            this.tssOperatorId.Text = "OCV_No";
            this.tssOperatorId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(84, 24);
            this.toolStripStatusLabel5.Text = "    测试项目：";
            // 
            // tssTestType
            // 
            this.tssTestType.BackColor = System.Drawing.Color.Lime;
            this.tssTestType.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssTestType.Name = "tssTestType";
            this.tssTestType.Size = new System.Drawing.Size(187, 24);
            this.tssTestType.Text = "电压+负极壳压+正极壳压+ACIR";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(80, 24);
            this.toolStripStatusLabel4.Text = "   运行模式：";
            this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssRunMode
            // 
            this.tssRunMode.BackColor = System.Drawing.Color.Lime;
            this.tssRunMode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssRunMode.Name = "tssRunMode";
            this.tssRunMode.Size = new System.Drawing.Size(60, 24);
            this.tssRunMode.Text = "单机测试";
            this.tssRunMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1378, 32);
            this.panel2.TabIndex = 207;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.toolStripMenuItem2});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1378, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.BackColor = System.Drawing.Color.SteelBlue;
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem,
            this.IOToolStripMenuItem1,
            this.工程设置ToolStripMenuItem1,
            this.手动测试ToolStripMenuItem1,
            this.手动校准toolStripMenuItem1,
            this.通道异常统计toolStripMenuItem1,
            this.数据补传ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.密码修改ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.校准设置ToolStripMenuItem,
            this.型号设置ToolStripMenuItem,
            this.手动输入ToolStripMenuItem});
            this.设置ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.设置ToolStripMenuItem.Image = global::OCV.Properties.Resources.tools;
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(74, 28);
            this.设置ToolStripMenuItem.Text = "工具";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Image = global::OCV.Properties.Resources.options;
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // IOToolStripMenuItem1
            // 
            this.IOToolStripMenuItem1.Image = global::OCV.Properties.Resources.ControlsDesign;
            this.IOToolStripMenuItem1.Name = "IOToolStripMenuItem1";
            this.IOToolStripMenuItem1.Size = new System.Drawing.Size(176, 26);
            this.IOToolStripMenuItem1.Text = "IO监控";
            this.IOToolStripMenuItem1.Click += new System.EventHandler(this.IOToolStripMenuItem1_Click);
            // 
            // 工程设置ToolStripMenuItem1
            // 
            this.工程设置ToolStripMenuItem1.Image = global::OCV.Properties.Resources.design;
            this.工程设置ToolStripMenuItem1.Name = "工程设置ToolStripMenuItem1";
            this.工程设置ToolStripMenuItem1.Size = new System.Drawing.Size(176, 26);
            this.工程设置ToolStripMenuItem1.Text = "工程设置";
            this.工程设置ToolStripMenuItem1.Visible = false;
            this.工程设置ToolStripMenuItem1.Click += new System.EventHandler(this.工程设置ToolStripMenuItem1_Click);
            // 
            // 手动测试ToolStripMenuItem1
            // 
            this.手动测试ToolStripMenuItem1.Image = global::OCV.Properties.Resources.Organizer_16x161;
            this.手动测试ToolStripMenuItem1.Name = "手动测试ToolStripMenuItem1";
            this.手动测试ToolStripMenuItem1.Size = new System.Drawing.Size(176, 26);
            this.手动测试ToolStripMenuItem1.Text = "手动测试";
            this.手动测试ToolStripMenuItem1.Click += new System.EventHandler(this.手动测试ToolStripMenuItem1_Click);
            // 
            // 手动校准toolStripMenuItem1
            // 
            this.手动校准toolStripMenuItem1.Image = global::OCV.Properties.Resources.EditWorkflowTask;
            this.手动校准toolStripMenuItem1.Name = "手动校准toolStripMenuItem1";
            this.手动校准toolStripMenuItem1.Size = new System.Drawing.Size(176, 26);
            this.手动校准toolStripMenuItem1.Text = "手动校准";
            this.手动校准toolStripMenuItem1.Click += new System.EventHandler(this.手动校准toolStripMenuItem1_Click);
            // 
            // 通道异常统计toolStripMenuItem1
            // 
            this.通道异常统计toolStripMenuItem1.Image = global::OCV.Properties.Resources.Ribbon_Save_16x16;
            this.通道异常统计toolStripMenuItem1.Name = "通道异常统计toolStripMenuItem1";
            this.通道异常统计toolStripMenuItem1.Size = new System.Drawing.Size(176, 26);
            this.通道异常统计toolStripMenuItem1.Text = "通道异常统计";
            this.通道异常统计toolStripMenuItem1.Click += new System.EventHandler(this.通道异常统计toolStripMenuItem1_Click);
            // 
            // 数据补传ToolStripMenuItem
            // 
            this.数据补传ToolStripMenuItem.Image = global::OCV.Properties.Resources.Ribbon_Close_32x321;
            this.数据补传ToolStripMenuItem.Name = "数据补传ToolStripMenuItem";
            this.数据补传ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.数据补传ToolStripMenuItem.Text = "数据补传";
            this.数据补传ToolStripMenuItem.Click += new System.EventHandler(this.数据补传ToolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::OCV.Properties.Resources.Ribbon_AlignRight_16x16;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(176, 26);
            this.toolStripMenuItem3.Text = "日志查看";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // 密码修改ToolStripMenuItem
            // 
            this.密码修改ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("密码修改ToolStripMenuItem.Image")));
            this.密码修改ToolStripMenuItem.Name = "密码修改ToolStripMenuItem";
            this.密码修改ToolStripMenuItem.ShowShortcutKeys = false;
            this.密码修改ToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.密码修改ToolStripMenuItem.Text = "权限管理";
            this.密码修改ToolStripMenuItem.Click += new System.EventHandler(this.密码修改ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Image = global::OCV.Properties.Resources.help_2;
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click_1);
            // 
            // 校准设置ToolStripMenuItem
            // 
            this.校准设置ToolStripMenuItem.Name = "校准设置ToolStripMenuItem";
            this.校准设置ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.校准设置ToolStripMenuItem.Text = "校准设置";
            this.校准设置ToolStripMenuItem.Visible = false;
            // 
            // 型号设置ToolStripMenuItem
            // 
            this.型号设置ToolStripMenuItem.Name = "型号设置ToolStripMenuItem";
            this.型号设置ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.型号设置ToolStripMenuItem.Text = "型号设置";
            this.型号设置ToolStripMenuItem.Visible = false;
            this.型号设置ToolStripMenuItem.Click += new System.EventHandler(this.型号设置ToolStripMenuItem_Click);
            // 
            // 手动输入ToolStripMenuItem
            // 
            this.手动输入ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("手动输入ToolStripMenuItem.Image")));
            this.手动输入ToolStripMenuItem.Name = "手动输入ToolStripMenuItem";
            this.手动输入ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.手动输入ToolStripMenuItem.Text = "手动输入";
            this.手动输入ToolStripMenuItem.Click += new System.EventHandler(this.手动输入ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStripMenuItem2.Image = global::OCV.Properties.Resources.tips;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(131, 28);
            this.toolStripMenuItem2.Text = "NG代码说明";
            this.toolStripMenuItem2.Visible = false;
            // 
            // FrmSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CaptionBackColorBottom = System.Drawing.Color.Black;
            this.CaptionBackColorTop = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1386, 732);
            this.ControlBoxActive = System.Drawing.Color.SteelBlue;
            this.ControlBoxDeactive = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.panel6);
            this.EffectBack = System.Drawing.Color.Transparent;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MdiBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "FrmSys";
            this.Shadow = false;
            this.ShadowColor = System.Drawing.SystemColors.ButtonFace;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OCV测试系统  Copyright © Kinte(广州擎天实业有限公司) ";
            this.TitleColor = System.Drawing.Color.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSys_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSys_FormClosed);
            this.Load += new System.EventHandler(this.FrmSys_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTest)).EndInit();
            this.panel1.ResumeLayout(false);
            this.sscMain.Panel1.ResumeLayout(false);
            this.sscMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sscMain)).EndInit();
            this.sscMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.tabControlPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.gbManual.ResumeLayout(false);
            this.gbManual.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Timer tim_UI;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IOToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 工程设置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 手动测试ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 手动校准toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 通道异常统计toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 数据补传ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 密码修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 校准设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 型号设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tssMachineId;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tssTestCH;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel tssOperatorId;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tssTestType;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tssRunMode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.DataGridView dgvTest;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinSplitContainer sscMain;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label txtTraycodeA;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private CCWin.SkinControl.SkinButton skinButton2;
        private ClsDeviceComm.Controls.UserLantern lblBhCVAllow;
        private CCWin.SkinControl.SkinButton skinButton1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private CCWin.SkinControl.SkinButton Btn_A_stop;
        private CCWin.SkinControl.SkinButton Btn_A_RUN;
        private DevComponents.DotNetBar.LabelX labelX4;
        private ClsDeviceComm.Controls.UserLantern lblBhOCVReq;
        private DevComponents.DotNetBar.LabelX labelX2;
        private ClsDeviceComm.Controls.UserLantern lblFrOCVAllow;
        private ClsDeviceComm.Controls.UserLantern lblFrCVRequest;
        private ClsDeviceComm.Controls.UserLantern lblAuto;
        private DevComponents.DotNetBar.LabelX labelX3;
        private ClsDeviceComm.Controls.UserLantern lblManual;
        private DevComponents.DotNetBar.LabelX labelX47;
        private DevComponents.DotNetBar.LabelX labelX45;
        private System.Windows.Forms.TextBox txtPlc_AutoStepNO;
        private DevComponents.DotNetBar.LabelX labelX36;
        private ClsDeviceComm.Controls.UserSwitch btnRunMode;
        private System.Windows.Forms.TextBox txtPlc_ResetStepNO;
        private DevComponents.DotNetBar.LabelX labelX5;
        private CCWin.SkinControl.SkinButton skinButton3;
        private ClsDeviceComm.Controls.UserLantern lblHaveGoods;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        public System.Windows.Forms.TextBox txtInfoA;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabPLCError;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox gbManual;
        private CCWin.SkinControl.SkinButton btnManualStart;
        private System.Windows.Forms.RadioButton rbOCV3;
        private System.Windows.Forms.RadioButton rbOCV2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbVol;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbACIR;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbNShellVol;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbPShellVol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn SFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_OCV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_CaseOCV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_CaseOCV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_ACIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_TEMP_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_TEMP_N;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Des;
        private System.Windows.Forms.ToolStripMenuItem 手动输入ToolStripMenuItem;
    }
}