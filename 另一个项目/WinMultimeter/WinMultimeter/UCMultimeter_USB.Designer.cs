namespace WinMultimeter
{
    partial class UCMultimeter_USB
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
            this.gbMain = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.SuspendLayout();
            // 
            // gbMain
            // 
            this.gbMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.gbMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(853, 509);
            // 
            // 
            // 
            this.gbMain.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gbMain.Style.BackColorGradientAngle = 90;
            this.gbMain.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gbMain.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbMain.Style.BorderBottomWidth = 1;
            this.gbMain.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gbMain.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbMain.Style.BorderLeftWidth = 1;
            this.gbMain.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbMain.Style.BorderRightWidth = 1;
            this.gbMain.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbMain.Style.BorderTopWidth = 1;
            this.gbMain.Style.CornerDiameter = 4;
            this.gbMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gbMain.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gbMain.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gbMain.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gbMain.TabIndex = 1;
            // 
            // UCMultimeter_USB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMain);
            this.Name = "UCMultimeter_USB";
            this.Size = new System.Drawing.Size(853, 509);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gbMain;
    }
}
