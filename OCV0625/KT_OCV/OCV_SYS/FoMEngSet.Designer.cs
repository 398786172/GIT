namespace OCV
{
    partial class FomEngSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FomEngSet));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtUpLMT_CAP = new System.Windows.Forms.TextBox();
            this.txtDownLMT_CAP = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMinVoltDrop = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtMaxVoltDrop = new System.Windows.Forms.TextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtUpLMT_ACIR = new System.Windows.Forms.TextBox();
            this.txtDownLMT_ACIR = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUpLMT_V = new System.Windows.Forms.TextBox();
            this.txtDownLMT_V = new System.Windows.Forms.TextBox();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.txtUpLMT_CAP);
            this.groupBox5.Controls.Add(this.txtDownLMT_CAP);
            this.groupBox5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(39, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(520, 73);
            this.groupBox5.TabIndex = 176;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "容量NG判定";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(273, 31);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(106, 21);
            this.label30.TabIndex = 160;
            this.label30.Text = "下限容量(VA)";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(19, 32);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(106, 21);
            this.label32.TabIndex = 159;
            this.label32.Text = "上限容量(VA)";
            // 
            // txtUpLMT_CAP
            // 
            this.txtUpLMT_CAP.Location = new System.Drawing.Point(166, 28);
            this.txtUpLMT_CAP.Name = "txtUpLMT_CAP";
            this.txtUpLMT_CAP.Size = new System.Drawing.Size(76, 29);
            this.txtUpLMT_CAP.TabIndex = 158;
            // 
            // txtDownLMT_CAP
            // 
            this.txtDownLMT_CAP.Location = new System.Drawing.Point(420, 28);
            this.txtDownLMT_CAP.Name = "txtDownLMT_CAP";
            this.txtDownLMT_CAP.Size = new System.Drawing.Size(77, 29);
            this.txtDownLMT_CAP.TabIndex = 157;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMinVoltDrop);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.txtMaxVoltDrop);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(39, 314);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(520, 73);
            this.groupBox4.TabIndex = 175;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "压降NG判定";
            // 
            // txtMinVoltDrop
            // 
            this.txtMinVoltDrop.Location = new System.Drawing.Point(421, 28);
            this.txtMinVoltDrop.Name = "txtMinVoltDrop";
            this.txtMinVoltDrop.Size = new System.Drawing.Size(76, 29);
            this.txtMinVoltDrop.TabIndex = 163;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(273, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(142, 21);
            this.label33.TabIndex = 162;
            this.label33.Text = "最小压降电压(mV)";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(21, 31);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(142, 21);
            this.label31.TabIndex = 161;
            this.label31.Text = "最大压降电压(mV)";
            // 
            // txtMaxVoltDrop
            // 
            this.txtMaxVoltDrop.Location = new System.Drawing.Point(166, 28);
            this.txtMaxVoltDrop.Name = "txtMaxVoltDrop";
            this.txtMaxVoltDrop.Size = new System.Drawing.Size(76, 29);
            this.txtMaxVoltDrop.TabIndex = 158;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label22);
            this.groupBox12.Controls.Add(this.label23);
            this.groupBox12.Controls.Add(this.txtUpLMT_ACIR);
            this.groupBox12.Controls.Add(this.txtDownLMT_ACIR);
            this.groupBox12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox12.Location = new System.Drawing.Point(39, 215);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(520, 73);
            this.groupBox12.TabIndex = 174;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "内阻NG判定";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(273, 31);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(112, 21);
            this.label22.TabIndex = 160;
            this.label22.Text = "下限内阻(mΩ)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(19, 32);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 21);
            this.label23.TabIndex = 159;
            this.label23.Text = "上限内阻(mΩ)";
            // 
            // txtUpLMT_ACIR
            // 
            this.txtUpLMT_ACIR.Location = new System.Drawing.Point(166, 28);
            this.txtUpLMT_ACIR.Name = "txtUpLMT_ACIR";
            this.txtUpLMT_ACIR.Size = new System.Drawing.Size(76, 29);
            this.txtUpLMT_ACIR.TabIndex = 158;
            // 
            // txtDownLMT_ACIR
            // 
            this.txtDownLMT_ACIR.Location = new System.Drawing.Point(420, 28);
            this.txtDownLMT_ACIR.Name = "txtDownLMT_ACIR";
            this.txtDownLMT_ACIR.Size = new System.Drawing.Size(77, 29);
            this.txtDownLMT_ACIR.TabIndex = 157;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(594, 352);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 35);
            this.btnSave.TabIndex = 172;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtUpLMT_V);
            this.groupBox1.Controls.Add(this.txtDownLMT_V);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(39, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 73);
            this.groupBox1.TabIndex = 173;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电压NG判定";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 21);
            this.label6.TabIndex = 160;
            this.label6.Text = "下限电压(mV)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 21);
            this.label7.TabIndex = 159;
            this.label7.Text = "上限电压(mV)";
            // 
            // txtUpLMT_V
            // 
            this.txtUpLMT_V.Location = new System.Drawing.Point(166, 28);
            this.txtUpLMT_V.Name = "txtUpLMT_V";
            this.txtUpLMT_V.Size = new System.Drawing.Size(76, 29);
            this.txtUpLMT_V.TabIndex = 158;
            // 
            // txtDownLMT_V
            // 
            this.txtDownLMT_V.Location = new System.Drawing.Point(420, 28);
            this.txtDownLMT_V.Name = "txtDownLMT_V";
            this.txtDownLMT_V.Size = new System.Drawing.Size(77, 29);
            this.txtDownLMT_V.TabIndex = 157;
            // 
            // FomEngSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(723, 415);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FomEngSet";
            this.Text = "单机测试本地工程设置";
            this.Load += new System.EventHandler(this.FomEngSet_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtUpLMT_CAP;
        private System.Windows.Forms.TextBox txtDownLMT_CAP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtMinVoltDrop;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtMaxVoltDrop;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtUpLMT_ACIR;
        private System.Windows.Forms.TextBox txtDownLMT_ACIR;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUpLMT_V;
        private System.Windows.Forms.TextBox txtDownLMT_V;
    }
}