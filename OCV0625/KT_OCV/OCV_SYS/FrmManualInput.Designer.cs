namespace OCV.OCV_SYS
{
    partial class FrmManualInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManualInput));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpTrayCellBind = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTrayCodeShow = new System.Windows.Forms.DataGridView();
            this.dgvCellCodeBind = new System.Windows.Forms.DataGridView();
            this.gbTrayCodeIn = new System.Windows.Forms.GroupBox();
            this.btDelete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBindTime = new System.Windows.Forms.TextBox();
            this.tbFocus3 = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btExport = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTrayCodeIn = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tpTrayCellBind.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrayCodeShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCellCodeBind)).BeginInit();
            this.gbTrayCodeIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpTrayCellBind);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1192, 659);
            this.tabControl1.TabIndex = 1;
            // 
            // tpTrayCellBind
            // 
            this.tpTrayCellBind.Controls.Add(this.tableLayoutPanel2);
            this.tpTrayCellBind.Location = new System.Drawing.Point(4, 29);
            this.tpTrayCellBind.Margin = new System.Windows.Forms.Padding(4);
            this.tpTrayCellBind.Name = "tpTrayCellBind";
            this.tpTrayCellBind.Padding = new System.Windows.Forms.Padding(4);
            this.tpTrayCellBind.Size = new System.Drawing.Size(1184, 626);
            this.tpTrayCellBind.TabIndex = 2;
            this.tpTrayCellBind.Text = "托盘-电芯绑定";
            this.tpTrayCellBind.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dgvTrayCodeShow, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dgvCellCodeBind, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.gbTrayCodeIn, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1173, 668);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // dgvTrayCodeShow
            // 
            this.dgvTrayCodeShow.AllowUserToAddRows = false;
            this.dgvTrayCodeShow.AllowUserToResizeRows = false;
            this.dgvTrayCodeShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrayCodeShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrayCodeShow.Location = new System.Drawing.Point(590, 92);
            this.dgvTrayCodeShow.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTrayCodeShow.MultiSelect = false;
            this.dgvTrayCodeShow.Name = "dgvTrayCodeShow";
            this.dgvTrayCodeShow.RowHeadersWidth = 51;
            this.dgvTrayCodeShow.RowTemplate.Height = 23;
            this.dgvTrayCodeShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTrayCodeShow.Size = new System.Drawing.Size(579, 572);
            this.dgvTrayCodeShow.TabIndex = 4;
            this.dgvTrayCodeShow.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrayCodeShow_CellDoubleClick);
            // 
            // dgvCellCodeBind
            // 
            this.dgvCellCodeBind.AllowUserToAddRows = false;
            this.dgvCellCodeBind.AllowUserToResizeRows = false;
            this.dgvCellCodeBind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCellCodeBind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCellCodeBind.Location = new System.Drawing.Point(4, 92);
            this.dgvCellCodeBind.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCellCodeBind.MultiSelect = false;
            this.dgvCellCodeBind.Name = "dgvCellCodeBind";
            this.dgvCellCodeBind.RowHeadersWidth = 51;
            this.dgvCellCodeBind.RowTemplate.Height = 23;
            this.dgvCellCodeBind.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCellCodeBind.Size = new System.Drawing.Size(578, 572);
            this.dgvCellCodeBind.TabIndex = 2;
            this.dgvCellCodeBind.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCellCodeBind_CellValueChanged);
            this.dgvCellCodeBind.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCellCodeBind_EditingControlShowing);
            // 
            // gbTrayCodeIn
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.gbTrayCodeIn, 2);
            this.gbTrayCodeIn.Controls.Add(this.btDelete);
            this.gbTrayCodeIn.Controls.Add(this.label4);
            this.gbTrayCodeIn.Controls.Add(this.tbBindTime);
            this.gbTrayCodeIn.Controls.Add(this.tbFocus3);
            this.gbTrayCodeIn.Controls.Add(this.btSave);
            this.gbTrayCodeIn.Controls.Add(this.btClear);
            this.gbTrayCodeIn.Controls.Add(this.btExport);
            this.gbTrayCodeIn.Controls.Add(this.btImport);
            this.gbTrayCodeIn.Controls.Add(this.label3);
            this.gbTrayCodeIn.Controls.Add(this.tbTrayCodeIn);
            this.gbTrayCodeIn.Location = new System.Drawing.Point(4, 4);
            this.gbTrayCodeIn.Margin = new System.Windows.Forms.Padding(4);
            this.gbTrayCodeIn.Name = "gbTrayCodeIn";
            this.gbTrayCodeIn.Padding = new System.Windows.Forms.Padding(4);
            this.gbTrayCodeIn.Size = new System.Drawing.Size(1165, 80);
            this.gbTrayCodeIn.TabIndex = 3;
            this.gbTrayCodeIn.TabStop = false;
            // 
            // btDelete
            // 
            this.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDelete.Location = new System.Drawing.Point(824, 26);
            this.btDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(105, 30);
            this.btDelete.TabIndex = 20;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "绑定时间";
            // 
            // tbBindTime
            // 
            this.tbBindTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBindTime.Location = new System.Drawing.Point(369, 29);
            this.tbBindTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbBindTime.Name = "tbBindTime";
            this.tbBindTime.ReadOnly = true;
            this.tbBindTime.Size = new System.Drawing.Size(198, 27);
            this.tbBindTime.TabIndex = 18;
            // 
            // tbFocus3
            // 
            this.tbFocus3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFocus3.Location = new System.Drawing.Point(1183, 30);
            this.tbFocus3.Margin = new System.Windows.Forms.Padding(4);
            this.tbFocus3.Name = "tbFocus3";
            this.tbFocus3.Size = new System.Drawing.Size(186, 27);
            this.tbFocus3.TabIndex = 17;
            // 
            // btSave
            // 
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.Location = new System.Drawing.Point(597, 26);
            this.btSave.Margin = new System.Windows.Forms.Padding(4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(105, 30);
            this.btSave.TabIndex = 16;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClear
            // 
            this.btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClear.Location = new System.Drawing.Point(711, 26);
            this.btClear.Margin = new System.Windows.Forms.Padding(4);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(105, 30);
            this.btClear.TabIndex = 15;
            this.btClear.Text = "清空";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btExport
            // 
            this.btExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExport.Location = new System.Drawing.Point(1051, 26);
            this.btExport.Margin = new System.Windows.Forms.Padding(4);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(105, 30);
            this.btExport.TabIndex = 14;
            this.btExport.Text = "导出";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Visible = false;
            // 
            // btImport
            // 
            this.btImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btImport.Location = new System.Drawing.Point(937, 26);
            this.btImport.Margin = new System.Windows.Forms.Padding(4);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(105, 30);
            this.btImport.TabIndex = 13;
            this.btImport.Text = "导入";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "托盘号";
            // 
            // tbTrayCodeIn
            // 
            this.tbTrayCodeIn.Location = new System.Drawing.Point(105, 29);
            this.tbTrayCodeIn.Margin = new System.Windows.Forms.Padding(4);
            this.tbTrayCodeIn.Name = "tbTrayCodeIn";
            this.tbTrayCodeIn.Size = new System.Drawing.Size(159, 27);
            this.tbTrayCodeIn.TabIndex = 7;
            this.tbTrayCodeIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTrayCodeIn_KeyPress);
            // 
            // FrmManualInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1192, 659);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmManualInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动输入";
            this.Load += new System.EventHandler(this.FrmManualInput_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpTrayCellBind.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrayCodeShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCellCodeBind)).EndInit();
            this.gbTrayCodeIn.ResumeLayout(false);
            this.gbTrayCodeIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpTrayCellBind;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvTrayCodeShow;
        private System.Windows.Forms.DataGridView dgvCellCodeBind;
        private System.Windows.Forms.GroupBox gbTrayCodeIn;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBindTime;
        private System.Windows.Forms.TextBox tbFocus3;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTrayCodeIn;
    }
}