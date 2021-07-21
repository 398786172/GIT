namespace OCV
{
    partial class FrmRunMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRunMode));
            this.rdoRunMode1 = new System.Windows.Forms.RadioButton();
            this.rdoRunMode3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoRunMode4 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoRunMode2 = new System.Windows.Forms.RadioButton();
            this.btnSaveRunMode = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoRunMode1
            // 
            this.rdoRunMode1.AutoSize = true;
            this.rdoRunMode1.Checked = true;
            this.rdoRunMode1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoRunMode1.Location = new System.Drawing.Point(41, 50);
            this.rdoRunMode1.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRunMode1.Name = "rdoRunMode1";
            this.rdoRunMode1.Size = new System.Drawing.Size(156, 31);
            this.rdoRunMode1.TabIndex = 174;
            this.rdoRunMode1.TabStop = true;
            this.rdoRunMode1.Text = "联网OCV测试";
            this.rdoRunMode1.UseVisualStyleBackColor = true;
            // 
            // rdoRunMode3
            // 
            this.rdoRunMode3.AutoSize = true;
            this.rdoRunMode3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoRunMode3.Location = new System.Drawing.Point(41, 136);
            this.rdoRunMode3.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRunMode3.Name = "rdoRunMode3";
            this.rdoRunMode3.Size = new System.Drawing.Size(156, 31);
            this.rdoRunMode3.TabIndex = 173;
            this.rdoRunMode3.Text = "单机OCV测试";
            this.rdoRunMode3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(221, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 29);
            this.label1.TabIndex = 176;
            this.label1.Text = "( 正常OCV测试)";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(221, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(632, 64);
            this.label3.TabIndex = 178;
            this.label3.Text = "( 进行OCV测试,不获取托盘电池信息, 不上传数据, 保存本地数据)";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rdoRunMode4);
            this.groupBox1.Controls.Add(this.rdoRunMode1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rdoRunMode3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoRunMode2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(55, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(933, 321);
            this.groupBox1.TabIndex = 179;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(221, 230);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(679, 64);
            this.label4.TabIndex = 180;
            this.label4.Text = "( 进行内阻校准.  运行前请先在“校准设置”中输入托盘的真实内阻数据）";
            // 
            // rdoRunMode4
            // 
            this.rdoRunMode4.AutoSize = true;
            this.rdoRunMode4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoRunMode4.Location = new System.Drawing.Point(41, 227);
            this.rdoRunMode4.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRunMode4.Name = "rdoRunMode4";
            this.rdoRunMode4.Size = new System.Drawing.Size(153, 31);
            this.rdoRunMode4.TabIndex = 179;
            this.rdoRunMode4.Text = "内阻校准测试";
            this.rdoRunMode4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(500, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 29);
            this.label2.TabIndex = 177;
            this.label2.Text = "( 不进行OCV测试, 直接排出托盘 )";
            this.label2.Visible = false;
            // 
            // rdoRunMode2
            // 
            this.rdoRunMode2.AutoSize = true;
            this.rdoRunMode2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoRunMode2.Location = new System.Drawing.Point(320, 17);
            this.rdoRunMode2.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRunMode2.Name = "rdoRunMode2";
            this.rdoRunMode2.Size = new System.Drawing.Size(153, 31);
            this.rdoRunMode2.TabIndex = 175;
            this.rdoRunMode2.Text = "托盘直接排出";
            this.rdoRunMode2.UseVisualStyleBackColor = true;
            this.rdoRunMode2.Visible = false;
            // 
            // btnSaveRunMode
            // 
            this.btnSaveRunMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveRunMode.Location = new System.Drawing.Point(789, 374);
            this.btnSaveRunMode.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveRunMode.Name = "btnSaveRunMode";
            this.btnSaveRunMode.Size = new System.Drawing.Size(149, 49);
            this.btnSaveRunMode.TabIndex = 180;
            this.btnSaveRunMode.Text = "设置";
            this.btnSaveRunMode.UseVisualStyleBackColor = true;
            this.btnSaveRunMode.Click += new System.EventHandler(this.btnSaveRunMode_Click);
            // 
            // FrmRunMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1055, 454);
            this.Controls.Add(this.btnSaveRunMode);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRunMode";
            this.Text = "运行模式";
            this.Load += new System.EventHandler(this.FrmRunMode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoRunMode1;
        private System.Windows.Forms.RadioButton rdoRunMode3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveRunMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoRunMode4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoRunMode2;
    }
}