namespace UserSetting
{
    partial class UserSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSetting));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TempPara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownLmt_DATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpLmt_DATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.K = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinVoltDrop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxVoltDrop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownLmt_ACIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpLmt_ACIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpLmt_V = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatteryType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgDataView1 = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkupdateUser = new System.Windows.Forms.CheckBox();
            this.checkdeluser = new System.Windows.Forms.CheckBox();
            this.checkAdduser = new System.Windows.Forms.CheckBox();
            this.tetPwd = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataView1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TempPara
            // 
            this.TempPara.DataPropertyName = "TempPara";
            this.TempPara.HeaderText = "温度系数";
            this.TempPara.Name = "TempPara";
            this.TempPara.ReadOnly = true;
            this.TempPara.Width = 61;
            // 
            // TempBase
            // 
            this.TempBase.DataPropertyName = "TempBase";
            this.TempBase.HeaderText = "基准温度";
            this.TempBase.Name = "TempBase";
            this.TempBase.ReadOnly = true;
            this.TempBase.Width = 61;
            // 
            // DownLmt_DATA
            // 
            this.DownLmt_DATA.DataPropertyName = "DownLmt_DATA";
            this.DownLmt_DATA.FillWeight = 134.4532F;
            this.DownLmt_DATA.HeaderText = "时间下限（h）";
            this.DownLmt_DATA.Name = "DownLmt_DATA";
            this.DownLmt_DATA.ReadOnly = true;
            this.DownLmt_DATA.Width = 72;
            // 
            // UpLmt_DATA
            // 
            this.UpLmt_DATA.DataPropertyName = "UpLmt_DATA";
            this.UpLmt_DATA.FillWeight = 84.08001F;
            this.UpLmt_DATA.HeaderText = "时间上限（h）";
            this.UpLmt_DATA.Name = "UpLmt_DATA";
            this.UpLmt_DATA.ReadOnly = true;
            this.UpLmt_DATA.Width = 72;
            // 
            // K
            // 
            this.K.DataPropertyName = "K";
            this.K.HeaderText = "K值1";
            this.K.Name = "K";
            this.K.ReadOnly = true;
            this.K.Width = 51;
            // 
            // MinVoltDrop
            // 
            this.MinVoltDrop.DataPropertyName = "MinVoltDrop";
            this.MinVoltDrop.HeaderText = "压降下限（mV）";
            this.MinVoltDrop.Name = "MinVoltDrop";
            this.MinVoltDrop.ReadOnly = true;
            this.MinVoltDrop.Width = 83;
            // 
            // MaxVoltDrop
            // 
            this.MaxVoltDrop.DataPropertyName = "MaxVoltDrop";
            this.MaxVoltDrop.HeaderText = "压降上限（mV）";
            this.MaxVoltDrop.Name = "MaxVoltDrop";
            this.MaxVoltDrop.ReadOnly = true;
            this.MaxVoltDrop.Width = 83;
            // 
            // DownLmt_ACIR
            // 
            this.DownLmt_ACIR.DataPropertyName = "DownLmt_ACIR";
            this.DownLmt_ACIR.HeaderText = "内阻下限（mΩ）";
            this.DownLmt_ACIR.Name = "DownLmt_ACIR";
            this.DownLmt_ACIR.ReadOnly = true;
            this.DownLmt_ACIR.Width = 88;
            // 
            // UpLmt_ACIR
            // 
            this.UpLmt_ACIR.DataPropertyName = "UpLmt_ACIR";
            this.UpLmt_ACIR.FillWeight = 86.87363F;
            this.UpLmt_ACIR.HeaderText = "内阻上限（mΩ）";
            this.UpLmt_ACIR.Name = "UpLmt_ACIR";
            this.UpLmt_ACIR.ReadOnly = true;
            this.UpLmt_ACIR.Width = 88;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "DownLmt_V";
            this.dataGridViewTextBoxColumn19.FillWeight = 84.13306F;
            this.dataGridViewTextBoxColumn19.HeaderText = "电压下限（mV）";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 83;
            // 
            // UpLmt_V
            // 
            this.UpLmt_V.DataPropertyName = "UpLmt_V";
            this.UpLmt_V.HeaderText = "电压上限（mV）";
            this.UpLmt_V.Name = "UpLmt_V";
            this.UpLmt_V.ReadOnly = true;
            this.UpLmt_V.Width = 83;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "工艺编号";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 61;
            // 
            // BatteryType
            // 
            this.BatteryType.DataPropertyName = "BatteryType";
            this.BatteryType.FillWeight = 206.0845F;
            this.BatteryType.HeaderText = "电池型号";
            this.BatteryType.Name = "BatteryType";
            this.BatteryType.ReadOnly = true;
            this.BatteryType.Width = 61;
            // 
            // dgDataView1
            // 
            this.dgDataView1.AllowUserToAddRows = false;
            this.dgDataView1.AllowUserToDeleteRows = false;
            this.dgDataView1.AllowUserToOrderColumns = true;
            this.dgDataView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgDataView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgDataView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgDataView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDataView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatteryType,
            this.ProjectName,
            this.UpLmt_V,
            this.dataGridViewTextBoxColumn19,
            this.UpLmt_ACIR,
            this.DownLmt_ACIR,
            this.MaxVoltDrop,
            this.MinVoltDrop,
            this.K,
            this.UpLmt_DATA,
            this.DownLmt_DATA,
            this.TempBase,
            this.TempPara});
            this.dgDataView1.Location = new System.Drawing.Point(1047, 88);
            this.dgDataView1.Name = "dgDataView1";
            this.dgDataView1.ReadOnly = true;
            this.dgDataView1.RowTemplate.Height = 23;
            this.dgDataView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDataView1.Size = new System.Drawing.Size(107, 71);
            this.dgDataView1.TabIndex = 192;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox6.Controls.Add(this.groupBox10);
            this.groupBox6.Controls.Add(this.groupBox1);
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(962, 630);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.dataGridView1);
            this.groupBox10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox10.Location = new System.Drawing.Point(6, 14);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(331, 556);
            this.groupBox10.TabIndex = 37;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "所有用户";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(17, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(300, 525);
            this.dataGridView1.TabIndex = 36;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "UserName";
            this.Column1.HeaderText = "用户名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Userdisc";
            this.Column2.HeaderText = "用户权限";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkupdateUser);
            this.groupBox1.Controls.Add(this.checkdeluser);
            this.groupBox1.Controls.Add(this.checkAdduser);
            this.groupBox1.Controls.Add(this.tetPwd);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(434, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 168);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // checkupdateUser
            // 
            this.checkupdateUser.AutoSize = true;
            this.checkupdateUser.Location = new System.Drawing.Point(227, 17);
            this.checkupdateUser.Name = "checkupdateUser";
            this.checkupdateUser.Size = new System.Drawing.Size(84, 24);
            this.checkupdateUser.TabIndex = 37;
            this.checkupdateUser.Text = "修改用户";
            this.checkupdateUser.UseVisualStyleBackColor = true;
            this.checkupdateUser.CheckedChanged += new System.EventHandler(this.checkupdateUser_CheckedChanged);
            // 
            // checkdeluser
            // 
            this.checkdeluser.AutoSize = true;
            this.checkdeluser.Location = new System.Drawing.Point(137, 17);
            this.checkdeluser.Name = "checkdeluser";
            this.checkdeluser.Size = new System.Drawing.Size(84, 24);
            this.checkdeluser.TabIndex = 36;
            this.checkdeluser.Text = "删除用户";
            this.checkdeluser.UseVisualStyleBackColor = true;
            this.checkdeluser.CheckedChanged += new System.EventHandler(this.checkdeluser_CheckedChanged);
            // 
            // checkAdduser
            // 
            this.checkAdduser.AutoSize = true;
            this.checkAdduser.Location = new System.Drawing.Point(47, 17);
            this.checkAdduser.Name = "checkAdduser";
            this.checkAdduser.Size = new System.Drawing.Size(84, 24);
            this.checkAdduser.TabIndex = 35;
            this.checkAdduser.Text = "新建用户";
            this.checkAdduser.UseVisualStyleBackColor = true;
            this.checkAdduser.CheckedChanged += new System.EventHandler(this.checkAdduser_CheckedChanged);
            // 
            // tetPwd
            // 
            this.tetPwd.Location = new System.Drawing.Point(137, 76);
            this.tetPwd.Name = "tetPwd";
            this.tetPwd.Size = new System.Drawing.Size(131, 26);
            this.tetPwd.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(52, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 28;
            this.label11.Text = "用户名称:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(52, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 20);
            this.label16.TabIndex = 33;
            this.label16.Text = "用户密码:";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(137, 118);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(131, 28);
            this.button6.TabIndex = 30;
            this.button6.Text = "确定";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(137, 47);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(131, 26);
            this.txtUserName.TabIndex = 29;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.checkedListBox1);
            this.groupBox9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.Location = new System.Drawing.Point(343, 25);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(563, 133);
            this.groupBox9.TabIndex = 26;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "用户权限:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBox1.Items.AddRange(new object[] {
            "一级权限（查看工艺参数，设置OCV软件基本参数）",
            "二级权限（查看工艺参数，设置OCV软件基本参数，系统参数）",
            "三级权限（查看工艺参数，设定工艺，删除工艺，设置OCV软件基本参数，系统参数）",
            "管理员   （用于账户管理，同时具备一、二、三级权限）"});
            this.checkedListBox1.Location = new System.Drawing.Point(3, 22);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(557, 108);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // UserSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 630);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.dgDataView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "权限管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserSetting_FormClosed);
            this.Load += new System.EventHandler(this.UserSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDataView1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempPara;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownLmt_DATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpLmt_DATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn K;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinVoltDrop;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxVoltDrop;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownLmt_ACIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpLmt_ACIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpLmt_V;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatteryType;
        private System.Windows.Forms.DataGridView dgDataView1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tetPwd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox checkAdduser;
        private System.Windows.Forms.CheckBox checkdeluser;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkupdateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}