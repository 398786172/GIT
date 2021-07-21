namespace OCV
{
    partial class FormProbeRecover
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStopCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWarningCount = new System.Windows.Forms.TextBox();
            this.btnSetCount = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimes = new System.Windows.Forms.TextBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 654);
            this.splitContainer1.SplitterDistance = 587;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChannel,
            this.colTimes});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(587, 654);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // colChannel
            // 
            this.colChannel.DataPropertyName = "Channel";
            this.colChannel.HeaderText = "通道";
            this.colChannel.Name = "colChannel";
            this.colChannel.ReadOnly = true;
            // 
            // colTimes
            // 
            this.colTimes.DataPropertyName = "Times";
            this.colTimes.HeaderText = "次数";
            this.colTimes.Name = "colTimes";
            this.colTimes.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtStopCount);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtWarningCount);
            this.panel2.Controls.Add(this.btnSetCount);
            this.panel2.Location = new System.Drawing.Point(39, 222);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 117);
            this.panel2.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "停止次数";
            // 
            // txtStopCount
            // 
            this.txtStopCount.Location = new System.Drawing.Point(70, 45);
            this.txtStopCount.Name = "txtStopCount";
            this.txtStopCount.Size = new System.Drawing.Size(138, 21);
            this.txtStopCount.TabIndex = 4;
            this.txtStopCount.Validating += new System.ComponentModel.CancelEventHandler(this.txtStopCount_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "预警次数";
            // 
            // txtWarningCount
            // 
            this.txtWarningCount.Location = new System.Drawing.Point(70, 14);
            this.txtWarningCount.Name = "txtWarningCount";
            this.txtWarningCount.Size = new System.Drawing.Size(138, 21);
            this.txtWarningCount.TabIndex = 1;
            this.txtWarningCount.Validating += new System.ComponentModel.CancelEventHandler(this.txtWarningCount_Validating);
            // 
            // btnSetCount
            // 
            this.btnSetCount.Location = new System.Drawing.Point(47, 78);
            this.btnSetCount.Name = "btnSetCount";
            this.btnSetCount.Size = new System.Drawing.Size(138, 23);
            this.btnSetCount.TabIndex = 2;
            this.btnSetCount.Text = "设定";
            this.btnSetCount.UseVisualStyleBackColor = true;
            this.btnSetCount.Click += new System.EventHandler(this.btnSetCount_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "设定报警次数";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSetAll);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTimes);
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Location = new System.Drawing.Point(39, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 117);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "次数";
            // 
            // txtTimes
            // 
            this.txtTimes.Location = new System.Drawing.Point(68, 14);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(138, 21);
            this.txtTimes.TabIndex = 1;
            this.txtTimes.Validating += new System.ComponentModel.CancelEventHandler(this.txtTimes_Validating);
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(10, 80);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(91, 23);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "设定";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设定次数(应用于检修更换探针后重设次数)";
            // 
            // btnSetAll
            // 
            this.btnSetAll.Location = new System.Drawing.Point(125, 80);
            this.btnSetAll.Name = "btnSetAll";
            this.btnSetAll.Size = new System.Drawing.Size(91, 23);
            this.btnSetAll.TabIndex = 4;
            this.btnSetAll.Text = "设定全部";
            this.btnSetAll.UseVisualStyleBackColor = true;
            this.btnSetAll.Click += new System.EventHandler(this.btnSetAll_Click);
            // 
            // FormProbeRecover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 654);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormProbeRecover";
            this.Text = "FormProbeRecover";
            this.Load += new System.EventHandler(this.FormProbeRecover_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimes;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStopCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWarningCount;
        private System.Windows.Forms.Button btnSetCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetAll;
    }
}