namespace OCV
{
    partial class FrmUpload
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_UPmes = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.labnum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvTrayInfo = new System.Windows.Forms.DataGridView();
            this.DEVICE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OPERATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.END_DATE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.END_DATE_TIME_STR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.btnDateOK = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoOCV3 = new System.Windows.Forms.RadioButton();
            this.rdoOCV2 = new System.Windows.Forms.RadioButton();
            this.rdoOCV1 = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkTrayCode = new System.Windows.Forms.CheckBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TexTrayCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_UPmes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrayInfo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_UPmes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(986, 615);
            this.tabControl1.TabIndex = 159;
            // 
            // tabPage_UPmes
            // 
            this.tabPage_UPmes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_UPmes.Controls.Add(this.label5);
            this.tabPage_UPmes.Controls.Add(this.labnum);
            this.tabPage_UPmes.Controls.Add(this.groupBox1);
            this.tabPage_UPmes.Controls.Add(this.btn_Upload);
            this.tabPage_UPmes.Controls.Add(this.btnDateOK);
            this.tabPage_UPmes.Controls.Add(this.groupBox4);
            this.tabPage_UPmes.Controls.Add(this.button1);
            this.tabPage_UPmes.Controls.Add(this.txtMes);
            this.tabPage_UPmes.Location = new System.Drawing.Point(4, 28);
            this.tabPage_UPmes.Name = "tabPage_UPmes";
            this.tabPage_UPmes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_UPmes.Size = new System.Drawing.Size(978, 583);
            this.tabPage_UPmes.TabIndex = 5;
            this.tabPage_UPmes.Text = "上传MES信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 19);
            this.label5.TabIndex = 187;
            this.label5.Text = "数量";
            // 
            // labnum
            // 
            this.labnum.BackColor = System.Drawing.Color.Lime;
            this.labnum.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labnum.Location = new System.Drawing.Point(547, 3);
            this.labnum.Name = "labnum";
            this.labnum.Size = new System.Drawing.Size(64, 19);
            this.labnum.TabIndex = 190;
            this.labnum.Text = "0";
            this.labnum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvTrayInfo);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(8, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 292);
            this.groupBox1.TabIndex = 184;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据选择(点击行）";
            // 
            // dgvTrayInfo
            // 
            this.dgvTrayInfo.AllowUserToAddRows = false;
            this.dgvTrayInfo.AllowUserToDeleteRows = false;
            this.dgvTrayInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTrayInfo.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvTrayInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvTrayInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvTrayInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrayInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEVICE_CODE,
            this.OPERATION,
            this.END_DATE_TIME,
            this.END_DATE_TIME_STR});
            this.dgvTrayInfo.Location = new System.Drawing.Point(8, 22);
            this.dgvTrayInfo.MultiSelect = false;
            this.dgvTrayInfo.Name = "dgvTrayInfo";
            this.dgvTrayInfo.ReadOnly = true;
            this.dgvTrayInfo.RowTemplate.Height = 23;
            this.dgvTrayInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrayInfo.Size = new System.Drawing.Size(593, 247);
            this.dgvTrayInfo.TabIndex = 0;
            this.dgvTrayInfo.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTrayInfo_CellMouseClick);
            // 
            // DEVICE_CODE
            // 
            this.DEVICE_CODE.DataPropertyName = "DEVICE_CODE";
            this.DEVICE_CODE.HeaderText = "托盘号";
            this.DEVICE_CODE.Name = "DEVICE_CODE";
            this.DEVICE_CODE.ReadOnly = true;
            // 
            // OPERATION
            // 
            this.OPERATION.DataPropertyName = "OPERATION";
            this.OPERATION.HeaderText = "OCV类型";
            this.OPERATION.Name = "OPERATION";
            this.OPERATION.ReadOnly = true;
            // 
            // END_DATE_TIME
            // 
            this.END_DATE_TIME.DataPropertyName = "END_DATE_TIME";
            this.END_DATE_TIME.HeaderText = "日期";
            this.END_DATE_TIME.Name = "END_DATE_TIME";
            this.END_DATE_TIME.ReadOnly = true;
            // 
            // END_DATE_TIME_STR
            // 
            this.END_DATE_TIME_STR.DataPropertyName = "END_DATE_TIME_STR";
            this.END_DATE_TIME_STR.HeaderText = "时间";
            this.END_DATE_TIME_STR.Name = "END_DATE_TIME_STR";
            this.END_DATE_TIME_STR.ReadOnly = true;
            // 
            // btn_Upload
            // 
            this.btn_Upload.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Upload.Location = new System.Drawing.Point(819, 255);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(135, 34);
            this.btn_Upload.TabIndex = 182;
            this.btn_Upload.Text = "上传";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btnDateOK
            // 
            this.btnDateOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnDateOK.Location = new System.Drawing.Point(656, 252);
            this.btnDateOK.Name = "btnDateOK";
            this.btnDateOK.Size = new System.Drawing.Size(137, 37);
            this.btnDateOK.TabIndex = 183;
            this.btnDateOK.Text = "查询";
            this.btnDateOK.UseVisualStyleBackColor = true;
            this.btnDateOK.Click += new System.EventHandler(this.btnDateOK_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(646, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 218);
            this.groupBox4.TabIndex = 181;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数选择";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.rdoOCV3);
            this.groupBox3.Controls.Add(this.rdoOCV2);
            this.groupBox3.Controls.Add(this.rdoOCV1);
            this.groupBox3.Location = new System.Drawing.Point(10, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 45);
            this.groupBox3.TabIndex = 178;
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "OCV类型:";
            // 
            // rdoOCV3
            // 
            this.rdoOCV3.AutoSize = true;
            this.rdoOCV3.Location = new System.Drawing.Point(234, 15);
            this.rdoOCV3.Name = "rdoOCV3";
            this.rdoOCV3.Size = new System.Drawing.Size(59, 21);
            this.rdoOCV3.TabIndex = 2;
            this.rdoOCV3.Text = "OCV3";
            this.rdoOCV3.UseVisualStyleBackColor = true;
            this.rdoOCV3.CheckedChanged += new System.EventHandler(this.rdoOCV3_CheckedChanged);
            // 
            // rdoOCV2
            // 
            this.rdoOCV2.AutoSize = true;
            this.rdoOCV2.Location = new System.Drawing.Point(163, 15);
            this.rdoOCV2.Name = "rdoOCV2";
            this.rdoOCV2.Size = new System.Drawing.Size(59, 21);
            this.rdoOCV2.TabIndex = 1;
            this.rdoOCV2.Text = "OCV2";
            this.rdoOCV2.UseVisualStyleBackColor = true;
            this.rdoOCV2.CheckedChanged += new System.EventHandler(this.rdoOCV2_CheckedChanged);
            // 
            // rdoOCV1
            // 
            this.rdoOCV1.AutoSize = true;
            this.rdoOCV1.Checked = true;
            this.rdoOCV1.Location = new System.Drawing.Point(84, 15);
            this.rdoOCV1.Name = "rdoOCV1";
            this.rdoOCV1.Size = new System.Drawing.Size(59, 21);
            this.rdoOCV1.TabIndex = 0;
            this.rdoOCV1.TabStop = true;
            this.rdoOCV1.Text = "OCV1";
            this.rdoOCV1.UseVisualStyleBackColor = true;
            this.rdoOCV1.CheckedChanged += new System.EventHandler(this.rdoOCV1_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.chkTrayCode);
            this.groupBox5.Controls.Add(this.chkDate);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.TexTrayCode);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(10, 71);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(299, 134);
            this.groupBox5.TabIndex = 181;
            this.groupBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(249, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 21);
            this.label6.TabIndex = 188;
            // 
            // chkTrayCode
            // 
            this.chkTrayCode.AutoSize = true;
            this.chkTrayCode.Location = new System.Drawing.Point(165, 19);
            this.chkTrayCode.Name = "chkTrayCode";
            this.chkTrayCode.Size = new System.Drawing.Size(75, 21);
            this.chkTrayCode.TabIndex = 186;
            this.chkTrayCode.Text = "托盘条码";
            this.chkTrayCode.UseVisualStyleBackColor = true;
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new System.Drawing.Point(84, 19);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(75, 21);
            this.chkDate.TabIndex = 185;
            this.chkDate.Text = "生产日期";
            this.chkDate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 184;
            this.label4.Text = "查询方式:";
            // 
            // TexTrayCode
            // 
            this.TexTrayCode.Location = new System.Drawing.Point(78, 100);
            this.TexTrayCode.Name = "TexTrayCode";
            this.TexTrayCode.Size = new System.Drawing.Size(152, 23);
            this.TexTrayCode.TabIndex = 181;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 180;
            this.label3.Text = "托盘条码:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy年MM月dd号";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(77, 54);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(154, 23);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "生产日期:";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button1.Location = new System.Drawing.Point(646, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 30);
            this.button1.TabIndex = 61;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMes
            // 
            this.txtMes.Location = new System.Drawing.Point(8, 336);
            this.txtMes.Multiline = true;
            this.txtMes.Name = "txtMes";
            this.txtMes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMes.Size = new System.Drawing.Size(607, 220);
            this.txtMes.TabIndex = 58;
            this.txtMes.Tag = "";
            // 
            // FrmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(986, 615);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMonitor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "数据上传";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMonitor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_UPmes.ResumeLayout(false);
            this.tabPage_UPmes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrayInfo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_UPmes;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTrayInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEVICE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OPERATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn END_DATE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn END_DATE_TIME_STR;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btnDateOK;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoOCV3;
        private System.Windows.Forms.RadioButton rdoOCV2;
        private System.Windows.Forms.RadioButton rdoOCV1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkTrayCode;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TexTrayCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labnum;
    }
}

