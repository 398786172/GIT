namespace OCV
{
    partial class FrmOCVMasterSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnConfirmSetting = new System.Windows.Forms.Button();
            this.dgvMasterValInsert = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInsertTheSame = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIR = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMasterFilesImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasterValInsert)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirmSetting
            // 
            this.btnConfirmSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmSetting.Location = new System.Drawing.Point(643, 551);
            this.btnConfirmSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirmSetting.Name = "btnConfirmSetting";
            this.btnConfirmSetting.Size = new System.Drawing.Size(153, 97);
            this.btnConfirmSetting.TabIndex = 3;
            this.btnConfirmSetting.Text = "确定";
            this.btnConfirmSetting.UseVisualStyleBackColor = true;
            this.btnConfirmSetting.Click += new System.EventHandler(this.btnConfirmSetting_Click);
            // 
            // dgvMasterValInsert
            // 
            this.dgvMasterValInsert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasterValInsert.Location = new System.Drawing.Point(31, 49);
            this.dgvMasterValInsert.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMasterValInsert.Name = "dgvMasterValInsert";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMasterValInsert.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMasterValInsert.RowTemplate.Height = 23;
            this.dgvMasterValInsert.Size = new System.Drawing.Size(468, 651);
            this.dgvMasterValInsert.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvMasterValInsert);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(41, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(534, 742);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Master (校准工装)参数";
            // 
            // btnInsertTheSame
            // 
            this.btnInsertTheSame.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInsertTheSame.Location = new System.Drawing.Point(24, 85);
            this.btnInsertTheSame.Margin = new System.Windows.Forms.Padding(4);
            this.btnInsertTheSame.Name = "btnInsertTheSame";
            this.btnInsertTheSame.Size = new System.Drawing.Size(153, 42);
            this.btnInsertTheSame.TabIndex = 6;
            this.btnInsertTheSame.Text = "输入";
            this.btnInsertTheSame.UseVisualStyleBackColor = true;
            this.btnInsertTheSame.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtIR);
            this.groupBox2.Controls.Add(this.btnInsertTheSame);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(627, 104);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(195, 149);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统一输入值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(117, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "mΩ";
            // 
            // txtIR
            // 
            this.txtIR.Location = new System.Drawing.Point(34, 41);
            this.txtIR.Margin = new System.Windows.Forms.Padding(4);
            this.txtIR.Name = "txtIR";
            this.txtIR.Size = new System.Drawing.Size(75, 34);
            this.txtIR.TabIndex = 7;
            this.txtIR.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMasterFilesImport);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(627, 291);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(195, 163);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Master数据导入";
            // 
            // btnMasterFilesImport
            // 
            this.btnMasterFilesImport.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMasterFilesImport.Location = new System.Drawing.Point(23, 65);
            this.btnMasterFilesImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnMasterFilesImport.Name = "btnMasterFilesImport";
            this.btnMasterFilesImport.Size = new System.Drawing.Size(153, 42);
            this.btnMasterFilesImport.TabIndex = 6;
            this.btnMasterFilesImport.Text = "打开";
            this.btnMasterFilesImport.UseVisualStyleBackColor = true;
            this.btnMasterFilesImport.Click += new System.EventHandler(this.btnAdjustFilesImport_Click);
            // 
            // FrmOCVMasterSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 770);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfirmSetting);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOCVMasterSetting";
            this.ShowIcon = false;
            this.Text = "OCV Master设置";
            this.Load += new System.EventHandler(this.FrmAdjustSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasterValInsert)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmSetting;
        private System.Windows.Forms.DataGridView dgvMasterValInsert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInsertTheSame;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIR;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnMasterFilesImport;


    }
}