﻿namespace OCV
{
    partial class FrmManualUpload
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
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.btnDataUpLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.rdoFolder = new System.Windows.Forms.RadioButton();
            this.rdoSingleFile = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnFileSelect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMsn = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDataToKinte = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Location = new System.Drawing.Point(727, 28);
            this.btnFolderSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(117, 35);
            this.btnFolderSelect.TabIndex = 2;
            this.btnFolderSelect.Text = "浏览";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // btnDataUpLoad
            // 
            this.btnDataUpLoad.Font = new System.Drawing.Font("宋体", 11F);
            this.btnDataUpLoad.Location = new System.Drawing.Point(1090, 474);
            this.btnDataUpLoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDataUpLoad.Name = "btnDataUpLoad";
            this.btnDataUpLoad.Size = new System.Drawing.Size(66, 40);
            this.btnDataUpLoad.TabIndex = 4;
            this.btnDataUpLoad.Text = "上传数据 -> 中鼎";
            this.btnDataUpLoad.UseVisualStyleBackColor = true;
            this.btnDataUpLoad.Visible = false;
            this.btnDataUpLoad.Click += new System.EventHandler(this.btnDataUpLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFolder);
            this.groupBox1.Controls.Add(this.btnFolderSelect);
            this.groupBox1.Location = new System.Drawing.Point(229, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(877, 86);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件夹";
            // 
            // txtFolder
            // 
            this.txtFolder.Font = new System.Drawing.Font("宋体", 9F);
            this.txtFolder.Location = new System.Drawing.Point(39, 34);
            this.txtFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(665, 25);
            this.txtFolder.TabIndex = 3;
            // 
            // rdoFolder
            // 
            this.rdoFolder.AutoSize = true;
            this.rdoFolder.Location = new System.Drawing.Point(28, 62);
            this.rdoFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoFolder.Name = "rdoFolder";
            this.rdoFolder.Size = new System.Drawing.Size(144, 23);
            this.rdoFolder.TabIndex = 6;
            this.rdoFolder.TabStop = true;
            this.rdoFolder.Text = "文件夹内文件";
            this.rdoFolder.UseVisualStyleBackColor = true;
            this.rdoFolder.CheckedChanged += new System.EventHandler(this.rdoFolder_CheckedChanged);
            // 
            // rdoSingleFile
            // 
            this.rdoSingleFile.AutoSize = true;
            this.rdoSingleFile.Location = new System.Drawing.Point(28, 168);
            this.rdoSingleFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoSingleFile.Name = "rdoSingleFile";
            this.rdoSingleFile.Size = new System.Drawing.Size(106, 23);
            this.rdoSingleFile.TabIndex = 7;
            this.rdoSingleFile.TabStop = true;
            this.rdoSingleFile.Text = "单个文件";
            this.rdoSingleFile.UseVisualStyleBackColor = true;
            this.rdoSingleFile.CheckedChanged += new System.EventHandler(this.rdoSingleFile_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFile);
            this.groupBox2.Controls.Add(this.btnFileSelect);
            this.groupBox2.Location = new System.Drawing.Point(229, 134);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(877, 86);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据文件";
            // 
            // txtFile
            // 
            this.txtFile.Font = new System.Drawing.Font("宋体", 9F);
            this.txtFile.Location = new System.Drawing.Point(39, 36);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(665, 25);
            this.txtFile.TabIndex = 3;
            // 
            // btnFileSelect
            // 
            this.btnFileSelect.Location = new System.Drawing.Point(727, 29);
            this.btnFileSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFileSelect.Name = "btnFileSelect";
            this.btnFileSelect.Size = new System.Drawing.Size(117, 38);
            this.btnFileSelect.TabIndex = 2;
            this.btnFileSelect.Text = "浏览";
            this.btnFileSelect.UseVisualStyleBackColor = true;
            this.btnFileSelect.Click += new System.EventHandler(this.btnFileSelect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoFolder);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.rdoSingleFile);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox3.Location = new System.Drawing.Point(32, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1136, 242);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择";
            // 
            // txtMsn
            // 
            this.txtMsn.Location = new System.Drawing.Point(28, 39);
            this.txtMsn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMsn.Multiline = true;
            this.txtMsn.Name = "txtMsn";
            this.txtMsn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsn.Size = new System.Drawing.Size(704, 149);
            this.txtMsn.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMsn);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox4.Location = new System.Drawing.Point(32, 285);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(772, 214);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "信息";
            // 
            // btnDataToKinte
            // 
            this.btnDataToKinte.Font = new System.Drawing.Font("宋体", 11F);
            this.btnDataToKinte.Location = new System.Drawing.Point(921, 394);
            this.btnDataToKinte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDataToKinte.Name = "btnDataToKinte";
            this.btnDataToKinte.Size = new System.Drawing.Size(217, 56);
            this.btnDataToKinte.TabIndex = 12;
            this.btnDataToKinte.Text = "上传数据 -> 服务器";
            this.btnDataToKinte.UseVisualStyleBackColor = true;
            this.btnDataToKinte.Click += new System.EventHandler(this.btnDataToKinte_Click);
            // 
            // FrmManualUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 536);
            this.Controls.Add(this.btnDataToKinte);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnDataUpLoad);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualUpload";
            this.ShowIcon = false;
            this.Text = "手动上传数据";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.Button btnDataUpLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.RadioButton rdoFolder;
        private System.Windows.Forms.RadioButton rdoSingleFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnFileSelect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMsn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnDataToKinte;
    }
}

