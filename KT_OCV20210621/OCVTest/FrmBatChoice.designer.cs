namespace OCV
{
    partial class FrmBatChoice
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_OK = new System.Windows.Forms.Button();
            this.btNG = new System.Windows.Forms.Button();
            this.btNGClear = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.rbSelectSingle = new System.Windows.Forms.RadioButton();
            this.rbSelectMulti = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 494);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电池选择";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 474);
            this.panel1.TabIndex = 0;
            // 
            // bt_OK
            // 
            this.bt_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_OK.Location = new System.Drawing.Point(566, 526);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(85, 28);
            this.bt_OK.TabIndex = 4;
            this.bt_OK.Text = "确定";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // btNG
            // 
            this.btNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNG.Location = new System.Drawing.Point(180, 525);
            this.btNG.Name = "btNG";
            this.btNG.Size = new System.Drawing.Size(110, 31);
            this.btNG.TabIndex = 5;
            this.btNG.Text = "通道NG累计次数";
            this.btNG.UseVisualStyleBackColor = true;
            this.btNG.Visible = false;
            this.btNG.Click += new System.EventHandler(this.btNG_Click);
            // 
            // btNGClear
            // 
            this.btNGClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNGClear.Location = new System.Drawing.Point(296, 525);
            this.btNGClear.Name = "btNGClear";
            this.btNGClear.Size = new System.Drawing.Size(110, 30);
            this.btNGClear.TabIndex = 6;
            this.btNGClear.Text = "通道NG清零";
            this.btNGClear.UseVisualStyleBackColor = true;
            this.btNGClear.Visible = false;
            this.btNGClear.Click += new System.EventHandler(this.btNGClear_Click);
            // 
            // btCancel
            // 
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Location = new System.Drawing.Point(679, 526);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(85, 28);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // rbSelectSingle
            // 
            this.rbSelectSingle.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSelectSingle.AutoSize = true;
            this.rbSelectSingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSelectSingle.Location = new System.Drawing.Point(40, 528);
            this.rbSelectSingle.Name = "rbSelectSingle";
            this.rbSelectSingle.Size = new System.Drawing.Size(41, 24);
            this.rbSelectSingle.TabIndex = 9;
            this.rbSelectSingle.TabStop = true;
            this.rbSelectSingle.Text = "单选";
            this.rbSelectSingle.UseVisualStyleBackColor = true;
            this.rbSelectSingle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rbSelectSingle_MouseUp);
            // 
            // rbSelectMulti
            // 
            this.rbSelectMulti.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSelectMulti.AutoSize = true;
            this.rbSelectMulti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbSelectMulti.Location = new System.Drawing.Point(97, 528);
            this.rbSelectMulti.Name = "rbSelectMulti";
            this.rbSelectMulti.Size = new System.Drawing.Size(41, 24);
            this.rbSelectMulti.TabIndex = 10;
            this.rbSelectMulti.TabStop = true;
            this.rbSelectMulti.Text = "多选";
            this.rbSelectMulti.UseVisualStyleBackColor = true;
            this.rbSelectMulti.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rbSelectMulti_MouseUp);
            // 
            // FrmBatChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 572);
            this.Controls.Add(this.rbSelectMulti);
            this.Controls.Add(this.rbSelectSingle);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btNGClear);
            this.Controls.Add(this.btNG);
            this.Controls.Add(this.bt_OK);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBatChoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BatChoice";
            this.Load += new System.EventHandler(this.BatChoice_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button btNG;
        private System.Windows.Forms.Button btNGClear;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbSelectSingle;
        private System.Windows.Forms.RadioButton rbSelectMulti;
    }
}