namespace OCV
{
    partial class FrmProSelection
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
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_Process = new System.Windows.Forms.ComboBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.labModel_no = new System.Windows.Forms.Label();
            this.labBatch_NO = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labProject_no = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label5.Location = new System.Drawing.Point(29, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 189;
            this.label5.Text = "电池型号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label10.Location = new System.Drawing.Point(29, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 25);
            this.label10.TabIndex = 187;
            this.label10.Text = "工艺名称";
            // 
            // cmb_Process
            // 
            this.cmb_Process.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cmb_Process.FormattingEnabled = true;
            this.cmb_Process.Location = new System.Drawing.Point(33, 222);
            this.cmb_Process.Name = "cmb_Process";
            this.cmb_Process.Size = new System.Drawing.Size(119, 32);
            this.cmb_Process.TabIndex = 186;
            // 
            // btn_Save
            // 
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Save.Location = new System.Drawing.Point(33, 275);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(115, 31);
            this.btn_Save.TabIndex = 190;
            this.btn_Save.Text = "确认选择";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // labModel_no
            // 
            this.labModel_no.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labModel_no.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.labModel_no.ForeColor = System.Drawing.Color.White;
            this.labModel_no.Location = new System.Drawing.Point(33, 40);
            this.labModel_no.Name = "labModel_no";
            this.labModel_no.Size = new System.Drawing.Size(119, 25);
            this.labModel_no.TabIndex = 192;
            this.labModel_no.Text = "0";
            this.labModel_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labBatch_NO
            // 
            this.labBatch_NO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labBatch_NO.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.labBatch_NO.ForeColor = System.Drawing.Color.White;
            this.labBatch_NO.Location = new System.Drawing.Point(33, 97);
            this.labBatch_NO.Name = "labBatch_NO";
            this.labBatch_NO.Size = new System.Drawing.Size(119, 25);
            this.labBatch_NO.TabIndex = 194;
            this.labBatch_NO.Text = "0";
            this.labBatch_NO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label3.Location = new System.Drawing.Point(29, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 193;
            this.label3.Text = "电池批号";
            // 
            // labProject_no
            // 
            this.labProject_no.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labProject_no.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.labProject_no.ForeColor = System.Drawing.Color.White;
            this.labProject_no.Location = new System.Drawing.Point(33, 153);
            this.labProject_no.Name = "labProject_no";
            this.labProject_no.Size = new System.Drawing.Size(119, 25);
            this.labProject_no.TabIndex = 196;
            this.labProject_no.Text = "0";
            this.labProject_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label6.Location = new System.Drawing.Point(29, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 25);
            this.label6.TabIndex = 195;
            this.label6.Text = "项目编号";
            // 
            // FrmProSelection
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(170, 327);
            this.ControlBox = false;
            this.Controls.Add(this.labProject_no);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labBatch_NO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labModel_no);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmb_Process);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProSelection";
            this.ShowIcon = false;
            this.Text = "工艺选择";
            this.Load += new System.EventHandler(this.FrmProSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_Process;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label labModel_no;
        private System.Windows.Forms.Label labBatch_NO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labProject_no;
        private System.Windows.Forms.Label label6;

    }
}