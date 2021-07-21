namespace OCV.OCVTest
{
    partial class FrmProcessManager
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtProcessName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStayTimeHour = new System.Windows.Forms.TextBox();
            this.txtStayMin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWarningV = new System.Windows.Forms.TextBox();
            this.txtMaxNGV = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMinNGV = new System.Windows.Forms.TextBox();
            this.tlpV = new System.Windows.Forms.TableLayoutPanel();
            this.tlpIR = new System.Windows.Forms.TableLayoutPanel();
            this.txtMaxNGIR = new System.Windows.Forms.TextBox();
            this.txtMinNGIR = new System.Windows.Forms.TextBox();
            this.chkOCV1 = new System.Windows.Forms.CheckBox();
            this.chkOCV2 = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsCurrent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSetCurrent = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpV.SuspendLayout();
            this.tlpIR.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel1, 2);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.12903F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.87097F));
            this.tableLayoutPanel1.Controls.Add(this.txtProcessName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtStayTimeHour, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtStayMin, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 101);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtProcessName
            // 
            this.txtProcessName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcessName.Location = new System.Drawing.Point(114, 6);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.Size = new System.Drawing.Size(193, 21);
            this.txtProcessName.TabIndex = 1;
            this.txtProcessName.Validating += new System.ComponentModel.CancelEventHandler(this.txtProcessName_Validating);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "静置时间上限";
            // 
            // txtStayTimeHour
            // 
            this.txtStayTimeHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStayTimeHour.Location = new System.Drawing.Point(114, 39);
            this.txtStayTimeHour.Name = "txtStayTimeHour";
            this.txtStayTimeHour.Size = new System.Drawing.Size(193, 21);
            this.txtStayTimeHour.TabIndex = 14;
            this.txtStayTimeHour.Validating += new System.ComponentModel.CancelEventHandler(this.txtStayTimeHour_Validating);
            // 
            // txtStayMin
            // 
            this.txtStayMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStayMin.Location = new System.Drawing.Point(114, 73);
            this.txtStayMin.Name = "txtStayMin";
            this.txtStayMin.Size = new System.Drawing.Size(193, 21);
            this.txtStayMin.TabIndex = 11;
            this.txtStayMin.Validating += new System.ComponentModel.CancelEventHandler(this.txtStayMin_Validating);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "静置时间下限";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "工程名称";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "电压上限(mV)";
            // 
            // txtWarningV
            // 
            this.txtWarningV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWarningV.Location = new System.Drawing.Point(113, 4);
            this.txtWarningV.Name = "txtWarningV";
            this.txtWarningV.Size = new System.Drawing.Size(194, 21);
            this.txtWarningV.TabIndex = 2;
            this.txtWarningV.Validating += new System.ComponentModel.CancelEventHandler(this.txtWarningV_Validating);
            // 
            // txtMaxNGV
            // 
            this.txtMaxNGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxNGV.Location = new System.Drawing.Point(113, 33);
            this.txtMaxNGV.Name = "txtMaxNGV";
            this.txtMaxNGV.Size = new System.Drawing.Size(194, 21);
            this.txtMaxNGV.TabIndex = 3;
            this.txtMaxNGV.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaxNGV_Validating);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "最高电压(mV)";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "内阻下限(mΩ)";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "电压下限(mV)";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "内阻上限(mΩ)";
            // 
            // txtMinNGV
            // 
            this.txtMinNGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinNGV.Location = new System.Drawing.Point(113, 62);
            this.txtMinNGV.Name = "txtMinNGV";
            this.txtMinNGV.Size = new System.Drawing.Size(194, 21);
            this.txtMinNGV.TabIndex = 13;
            this.txtMinNGV.Validating += new System.ComponentModel.CancelEventHandler(this.txtMinNGV_Validating);
            // 
            // tlpV
            // 
            this.tlpV.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tlpV, 2);
            this.tlpV.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.80645F));
            this.tlpV.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.19355F));
            this.tlpV.Controls.Add(this.label2, 0, 0);
            this.tlpV.Controls.Add(this.txtWarningV, 1, 0);
            this.tlpV.Controls.Add(this.label1, 0, 1);
            this.tlpV.Controls.Add(this.txtMaxNGV, 1, 1);
            this.tlpV.Controls.Add(this.label4, 0, 2);
            this.tlpV.Controls.Add(this.txtMinNGV, 1, 2);
            this.tlpV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpV.Location = new System.Drawing.Point(3, 148);
            this.tlpV.Name = "tlpV";
            this.tlpV.RowCount = 3;
            this.tlpV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpV.Size = new System.Drawing.Size(310, 87);
            this.tlpV.TabIndex = 1;
            // 
            // tlpIR
            // 
            this.tlpIR.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tlpIR, 2);
            this.tlpIR.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.48387F));
            this.tlpIR.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.51613F));
            this.tlpIR.Controls.Add(this.label5, 0, 0);
            this.tlpIR.Controls.Add(this.txtMaxNGIR, 1, 0);
            this.tlpIR.Controls.Add(this.txtMinNGIR, 1, 1);
            this.tlpIR.Controls.Add(this.label3, 0, 1);
            this.tlpIR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpIR.Location = new System.Drawing.Point(3, 241);
            this.tlpIR.Name = "tlpIR";
            this.tlpIR.RowCount = 2;
            this.tlpIR.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpIR.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpIR.Size = new System.Drawing.Size(310, 66);
            this.tlpIR.TabIndex = 14;
            // 
            // txtMaxNGIR
            // 
            this.txtMaxNGIR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxNGIR.Location = new System.Drawing.Point(113, 6);
            this.txtMaxNGIR.Name = "txtMaxNGIR";
            this.txtMaxNGIR.Size = new System.Drawing.Size(194, 21);
            this.txtMaxNGIR.TabIndex = 2;
            this.txtMaxNGIR.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaxNGIR_Validating);
            // 
            // txtMinNGIR
            // 
            this.txtMinNGIR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinNGIR.Location = new System.Drawing.Point(113, 39);
            this.txtMinNGIR.Name = "txtMinNGIR";
            this.txtMinNGIR.Size = new System.Drawing.Size(194, 21);
            this.txtMinNGIR.TabIndex = 3;
            this.txtMinNGIR.Validating += new System.ComponentModel.CancelEventHandler(this.txtMinNGIR_Validating);
            // 
            // chkOCV1
            // 
            this.chkOCV1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkOCV1.AutoSize = true;
            this.chkOCV1.Location = new System.Drawing.Point(55, 8);
            this.chkOCV1.Name = "chkOCV1";
            this.chkOCV1.Size = new System.Drawing.Size(48, 16);
            this.chkOCV1.TabIndex = 14;
            this.chkOCV1.Text = "OCV1";
            this.chkOCV1.UseVisualStyleBackColor = true;
            this.chkOCV1.CheckedChanged += new System.EventHandler(this.chkOCV1_CheckedChanged);
            // 
            // chkOCV2
            // 
            this.chkOCV2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkOCV2.AutoSize = true;
            this.chkOCV2.Location = new System.Drawing.Point(210, 8);
            this.chkOCV2.Name = "chkOCV2";
            this.chkOCV2.Size = new System.Drawing.Size(48, 16);
            this.chkOCV2.TabIndex = 15;
            this.chkOCV2.Text = "OCV2";
            this.chkOCV2.UseVisualStyleBackColor = true;
            this.chkOCV2.CheckedChanged += new System.EventHandler(this.chkOCV2_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.Location = new System.Drawing.Point(30, 313);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.Location = new System.Drawing.Point(187, 313);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.96774F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.03226F));
            this.tableLayoutPanel4.Controls.Add(this.chkOCV2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkOCV1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(310, 32);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // dgvView
            // 
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToDeleteRows = false;
            this.dgvView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProcessName,
            this.colIsCurrent});
            this.dgvView.Location = new System.Drawing.Point(6, 3);
            this.dgvView.Name = "dgvView";
            this.dgvView.ReadOnly = true;
            this.dgvView.RowHeadersWidth = 4;
            this.dgvView.RowTemplate.Height = 23;
            this.dgvView.Size = new System.Drawing.Size(307, 387);
            this.dgvView.TabIndex = 16;
            this.dgvView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvView_CellClick);
            // 
            // colProcessName
            // 
            this.colProcessName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProcessName.DataPropertyName = "ProcessName";
            this.colProcessName.HeaderText = "工程名";
            this.colProcessName.Name = "colProcessName";
            this.colProcessName.ReadOnly = true;
            // 
            // colIsCurrent
            // 
            this.colIsCurrent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colIsCurrent.DataPropertyName = "IsCurrent";
            this.colIsCurrent.FillWeight = 50F;
            this.colIsCurrent.HeaderText = "当前工程";
            this.colIsCurrent.Name = "colIsCurrent";
            this.colIsCurrent.ReadOnly = true;
            this.colIsCurrent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colIsCurrent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colIsCurrent.Visible = false;
            // 
            // btnSetCurrent
            // 
            this.btnSetCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetCurrent.Location = new System.Drawing.Point(6, 396);
            this.btnSetCurrent.Name = "btnSetCurrent";
            this.btnSetCurrent.Size = new System.Drawing.Size(307, 23);
            this.btnSetCurrent.TabIndex = 17;
            this.btnSetCurrent.Text = "设定为当前工程";
            this.btnSetCurrent.UseVisualStyleBackColor = true;
            this.btnSetCurrent.Visible = false;
            this.btnSetCurrent.Click += new System.EventHandler(this.btnSetCurrent_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvView);
            this.splitContainer1.Panel1.Controls.Add(this.btnSetCurrent);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(636, 422);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.63291F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.36709F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.tlpIR, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tlpV, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(316, 422);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // FrmProcessManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 446);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmProcessManager";
            this.Text = "FrmProcessManager";
            this.Load += new System.EventHandler(this.FrmProcessManager_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpV.ResumeLayout(false);
            this.tlpV.PerformLayout();
            this.tlpIR.ResumeLayout(false);
            this.tlpIR.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStayTimeHour;
        private System.Windows.Forms.TextBox txtStayMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWarningV;
        private System.Windows.Forms.TextBox txtMaxNGV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMinNGV;
        private System.Windows.Forms.TableLayoutPanel tlpV;
        private System.Windows.Forms.CheckBox chkOCV1;
        private System.Windows.Forms.TableLayoutPanel tlpIR;
        private System.Windows.Forms.CheckBox chkOCV2;
        private System.Windows.Forms.TextBox txtMaxNGIR;
        private System.Windows.Forms.TextBox txtMinNGIR;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.Button btnSetCurrent;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsCurrent;
    }
}