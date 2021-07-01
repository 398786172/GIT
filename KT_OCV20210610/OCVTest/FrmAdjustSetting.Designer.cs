namespace OCV
{
    partial class FrmAdjustSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnConfirmSetting = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetAllAdjustVal = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIR = new System.Windows.Forms.TextBox();
            this.rdoUnit_B = new System.Windows.Forms.RadioButton();
            this.rdoUnit_A = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirmSetting
            // 
            this.btnConfirmSetting.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmSetting.Location = new System.Drawing.Point(490, 441);
            this.btnConfirmSetting.Name = "btnConfirmSetting";
            this.btnConfirmSetting.Size = new System.Drawing.Size(115, 32);
            this.btnConfirmSetting.TabIndex = 3;
            this.btnConfirmSetting.Text = "确定";
            this.btnConfirmSetting.UseVisualStyleBackColor = true;
            this.btnConfirmSetting.Click += new System.EventHandler(this.btnConfirmSetting_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 40);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(351, 421);
            this.dataGridView1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox1.Location = new System.Drawing.Point(31, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 486);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请输入校准托盘的电池内阻值";
            // 
            // btnSetAllAdjustVal
            // 
            this.btnSetAllAdjustVal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetAllAdjustVal.Location = new System.Drawing.Point(29, 72);
            this.btnSetAllAdjustVal.Name = "btnSetAllAdjustVal";
            this.btnSetAllAdjustVal.Size = new System.Drawing.Size(79, 31);
            this.btnSetAllAdjustVal.TabIndex = 6;
            this.btnSetAllAdjustVal.Text = "输入";
            this.btnSetAllAdjustVal.UseVisualStyleBackColor = true;
            this.btnSetAllAdjustVal.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtIR);
            this.groupBox2.Controls.Add(this.btnSetAllAdjustVal);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox2.Location = new System.Drawing.Point(479, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 119);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "一键输入所有值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "内阻值:";
            // 
            // txtIR
            // 
            this.txtIR.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIR.Location = new System.Drawing.Point(61, 31);
            this.txtIR.Name = "txtIR";
            this.txtIR.Size = new System.Drawing.Size(57, 23);
            this.txtIR.TabIndex = 7;
            this.txtIR.Text = "0";
            // 
            // rdoUnit_B
            // 
            this.rdoUnit_B.AutoSize = true;
            this.rdoUnit_B.Location = new System.Drawing.Point(490, 67);
            this.rdoUnit_B.Name = "rdoUnit_B";
            this.rdoUnit_B.Size = new System.Drawing.Size(53, 16);
            this.rdoUnit_B.TabIndex = 55;
            this.rdoUnit_B.TabStop = true;
            this.rdoUnit_B.Text = "B单元";
            this.rdoUnit_B.UseVisualStyleBackColor = true;
            // 
            // rdoUnit_A
            // 
            this.rdoUnit_A.AutoSize = true;
            this.rdoUnit_A.Checked = true;
            this.rdoUnit_A.Location = new System.Drawing.Point(490, 27);
            this.rdoUnit_A.Name = "rdoUnit_A";
            this.rdoUnit_A.Size = new System.Drawing.Size(53, 16);
            this.rdoUnit_A.TabIndex = 54;
            this.rdoUnit_A.TabStop = true;
            this.rdoUnit_A.Text = "A单元";
            this.rdoUnit_A.UseVisualStyleBackColor = true;
            // 
            // FrmAdjustSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 526);
            this.Controls.Add(this.rdoUnit_B);
            this.Controls.Add(this.rdoUnit_A);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfirmSetting);
            this.Name = "FrmAdjustSetting";
            this.ShowIcon = false;
            this.Text = "校准设置";
            this.Load += new System.EventHandler(this.FrmAdjustSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmSetting;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetAllAdjustVal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIR;
        private System.Windows.Forms.RadioButton rdoUnit_B;
        private System.Windows.Forms.RadioButton rdoUnit_A;


    }
}