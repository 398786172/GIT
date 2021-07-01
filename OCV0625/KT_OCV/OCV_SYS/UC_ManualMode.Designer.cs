namespace OCV.OCV_SYS
{
    partial class UC_ManualMode
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
            this.components = new System.ComponentModel.Container();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbOCV2 = new System.Windows.Forms.RadioButton();
            this.rbOCV3 = new System.Windows.Forms.RadioButton();
            this.btnManualStart = new CCWin.SkinControl.SkinButton();
            this.btnTrayOut = new CCWin.SkinControl.SkinButton();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnTrayOut);
            this.groupPanel1.Controls.Add(this.btnManualStart);
            this.groupPanel1.Controls.Add(this.rbOCV3);
            this.groupPanel1.Controls.Add(this.rbOCV2);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(448, 287);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "手动控制";
            // 
            // rbOCV2
            // 
            this.rbOCV2.AutoSize = true;
            this.rbOCV2.BackColor = System.Drawing.Color.Transparent;
            this.rbOCV2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOCV2.Location = new System.Drawing.Point(46, 67);
            this.rbOCV2.Name = "rbOCV2";
            this.rbOCV2.Size = new System.Drawing.Size(83, 21);
            this.rbOCV2.TabIndex = 0;
            this.rbOCV2.TabStop = true;
            this.rbOCV2.Text = "OCV2测试";
            this.rbOCV2.UseVisualStyleBackColor = false;
            // 
            // rbOCV3
            // 
            this.rbOCV3.AutoSize = true;
            this.rbOCV3.BackColor = System.Drawing.Color.Transparent;
            this.rbOCV3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOCV3.Location = new System.Drawing.Point(46, 43);
            this.rbOCV3.Name = "rbOCV3";
            this.rbOCV3.Size = new System.Drawing.Size(83, 21);
            this.rbOCV3.TabIndex = 1;
            this.rbOCV3.TabStop = true;
            this.rbOCV3.Text = "OCV3测试";
            this.rbOCV3.UseVisualStyleBackColor = false;
            // 
            // btnManualStart
            // 
            this.btnManualStart.BackColor = System.Drawing.Color.Transparent;
            this.btnManualStart.BaseColor = System.Drawing.Color.Green;
            this.btnManualStart.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.btnManualStart.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnManualStart.DownBack = null;
            this.btnManualStart.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManualStart.GlowColor = System.Drawing.Color.Silver;
            this.btnManualStart.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.btnManualStart.Location = new System.Drawing.Point(144, 43);
            this.btnManualStart.MouseBack = null;
            this.btnManualStart.MouseBaseColor = System.Drawing.Color.Teal;
            this.btnManualStart.Name = "btnManualStart";
            this.btnManualStart.NormlBack = null;
            this.btnManualStart.Size = new System.Drawing.Size(95, 44);
            this.btnManualStart.TabIndex = 180;
            this.btnManualStart.Text = "启动测试";
            this.btnManualStart.UseVisualStyleBackColor = false;
            // 
            // btnTrayOut
            // 
            this.btnTrayOut.BackColor = System.Drawing.Color.Transparent;
            this.btnTrayOut.BaseColor = System.Drawing.Color.LightSteelBlue;
            this.btnTrayOut.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.btnTrayOut.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnTrayOut.DownBack = null;
            this.btnTrayOut.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTrayOut.GlowColor = System.Drawing.Color.Silver;
            this.btnTrayOut.InnerBorderColor = System.Drawing.Color.LightSteelBlue;
            this.btnTrayOut.Location = new System.Drawing.Point(270, 43);
            this.btnTrayOut.MouseBack = null;
            this.btnTrayOut.MouseBaseColor = System.Drawing.Color.Teal;
            this.btnTrayOut.Name = "btnTrayOut";
            this.btnTrayOut.NormlBack = null;
            this.btnTrayOut.Size = new System.Drawing.Size(95, 43);
            this.btnTrayOut.TabIndex = 181;
            this.btnTrayOut.Text = "移出托盘";
            this.btnTrayOut.UseVisualStyleBackColor = false;
            // 
            // UC_ManualMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupPanel1);
            this.Name = "UC_ManualMode";
            this.Size = new System.Drawing.Size(448, 287);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.RadioButton rbOCV3;
        private System.Windows.Forms.RadioButton rbOCV2;
        private CCWin.SkinControl.SkinButton btnManualStart;
        private CCWin.SkinControl.SkinButton btnTrayOut;
    }
}
