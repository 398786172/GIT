namespace OCV.OCVTest
{
    partial class BateryViewController
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labBatCode = new System.Windows.Forms.Label();
            this.labV = new System.Windows.Forms.Label();
            this.labIR = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.98087F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.79758F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.22155F));
            this.tableLayoutPanel1.Controls.Add(this.labBatCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labV, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labIR, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 39);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labBatCode
            // 
            this.labBatCode.AutoSize = true;
            this.labBatCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labBatCode.Font = new System.Drawing.Font("宋体", 8F);
            this.labBatCode.Location = new System.Drawing.Point(4, 1);
            this.labBatCode.Name = "labBatCode";
            this.labBatCode.Size = new System.Drawing.Size(64, 37);
            this.labBatCode.TabIndex = 0;
            this.labBatCode.Text = "电池码";
            this.labBatCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labV
            // 
            this.labV.AutoSize = true;
            this.labV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labV.Font = new System.Drawing.Font("宋体", 8F);
            this.labV.Location = new System.Drawing.Point(75, 1);
            this.labV.Name = "labV";
            this.labV.Size = new System.Drawing.Size(72, 37);
            this.labV.TabIndex = 1;
            this.labV.Text = "电压";
            this.labV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labIR
            // 
            this.labIR.AutoSize = true;
            this.labIR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labIR.Font = new System.Drawing.Font("宋体", 8F);
            this.labIR.Location = new System.Drawing.Point(154, 1);
            this.labIR.Name = "labIR";
            this.labIR.Size = new System.Drawing.Size(59, 37);
            this.labIR.TabIndex = 2;
            this.labIR.Text = "内阻";
            this.labIR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BateryViewController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.Name = "BateryViewController";
            this.Size = new System.Drawing.Size(217, 39);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labIR;
        private System.Windows.Forms.Label labV;
        private System.Windows.Forms.Label labBatCode;
    }
}
