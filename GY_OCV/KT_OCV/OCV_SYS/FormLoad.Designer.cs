namespace OCV
{
    partial class FormLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoad));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.label4 = new CCWin.SkinControl.SkinLabel();
            this.txtUserName = new CCWin.SkinControl.SkinTextBox();
            this.tetPwd = new CCWin.SkinControl.SkinTextBox();
            this.label1 = new CCWin.SkinControl.SkinLabel();
            this.skinPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(3, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(391, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "版权所有权 Copyright © Kinte(广州擎天实业有限公司) 2020";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(174, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 28;
            this.label3.Text = "用户名称:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(174, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 21);
            this.label16.TabIndex = 33;
            this.label16.Text = "用户密码:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(273, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "登  录";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.White;
            this.skinPanel2.Controls.Add(this.label4);
            this.skinPanel2.Controls.Add(this.txtUserName);
            this.skinPanel2.Controls.Add(this.tetPwd);
            this.skinPanel2.Controls.Add(this.button1);
            this.skinPanel2.Controls.Add(this.label2);
            this.skinPanel2.Controls.Add(this.label16);
            this.skinPanel2.Controls.Add(this.label3);
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(-14, 193);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(664, 267);
            this.skinPanel2.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.BorderColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(435, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 23);
            this.label4.TabIndex = 41;
            this.label4.Text = "skinLabel1";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.Transparent;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.DownBack = null;
            this.txtUserName.Icon = null;
            this.txtUserName.IconIsButton = false;
            this.txtUserName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserName.IsPasswordChat = '\0';
            this.txtUserName.IsSystemPasswordChar = false;
            this.txtUserName.Lines = new string[0];
            this.txtUserName.Location = new System.Drawing.Point(273, 61);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserName.MaxLength = 32767;
            this.txtUserName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtUserName.MouseBack = null;
            this.txtUserName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NormlBack = null;
            this.txtUserName.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserName.ReadOnly = false;
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.Size = new System.Drawing.Size(149, 28);
            // 
            // 
            // 
            this.txtUserName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtUserName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtUserName.SkinTxt.Name = "BaseText";
            this.txtUserName.SkinTxt.Size = new System.Drawing.Size(137, 18);
            this.txtUserName.SkinTxt.TabIndex = 0;
            this.txtUserName.SkinTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserName.SkinTxt.WaterText = "";
            this.txtUserName.TabIndex = 40;
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserName.WaterText = "";
            this.txtUserName.WordWrap = true;
            // 
            // tetPwd
            // 
            this.tetPwd.BackColor = System.Drawing.Color.Transparent;
            this.tetPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tetPwd.DownBack = null;
            this.tetPwd.Icon = null;
            this.tetPwd.IconIsButton = false;
            this.tetPwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.tetPwd.IsPasswordChat = '●';
            this.tetPwd.IsSystemPasswordChar = true;
            this.tetPwd.Lines = new string[0];
            this.tetPwd.Location = new System.Drawing.Point(273, 104);
            this.tetPwd.Margin = new System.Windows.Forms.Padding(0);
            this.tetPwd.MaxLength = 32767;
            this.tetPwd.MinimumSize = new System.Drawing.Size(28, 28);
            this.tetPwd.MouseBack = null;
            this.tetPwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.tetPwd.Multiline = false;
            this.tetPwd.Name = "tetPwd";
            this.tetPwd.NormlBack = null;
            this.tetPwd.Padding = new System.Windows.Forms.Padding(5);
            this.tetPwd.ReadOnly = false;
            this.tetPwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tetPwd.Size = new System.Drawing.Size(149, 28);
            // 
            // 
            // 
            this.tetPwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tetPwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tetPwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tetPwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.tetPwd.SkinTxt.Name = "BaseText";
            this.tetPwd.SkinTxt.PasswordChar = '●';
            this.tetPwd.SkinTxt.Size = new System.Drawing.Size(137, 18);
            this.tetPwd.SkinTxt.TabIndex = 0;
            this.tetPwd.SkinTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tetPwd.SkinTxt.UseSystemPasswordChar = true;
            this.tetPwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.tetPwd.SkinTxt.WaterText = "";
            this.tetPwd.TabIndex = 39;
            this.tetPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tetPwd.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.tetPwd.WaterText = "";
            this.tetPwd.WordWrap = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(559, 79);
            this.label1.TabIndex = 41;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.BackShade = false;
            this.BorderColor = System.Drawing.Color.SteelBlue;
            this.CaptionBackColorBottom = System.Drawing.Color.Black;
            this.CaptionBackColorTop = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(640, 455);
            this.ControlBoxActive = System.Drawing.Color.SteelBlue;
            this.ControlBoxDeactive = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skinPanel2);
            this.EffectCaption = CCWin.TitleType.Title;
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.InheritTheme = true;
            this.InnerBorderColor = System.Drawing.Color.SteelBlue;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 485);
            this.MdiBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 455);
            this.Name = "FormLoad";
            this.RoundStyle = CCWin.SkinClass.RoundStyle.None;
            this.Shadow = false;
            this.ShadowColor = System.Drawing.Color.Transparent;
            this.ShadowRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ShadowWidth = 1;
            this.ShowBorder = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录   【版权所有权 Copyright © Kinte(广州擎天实业有限公司) 2020】";
            this.TitleColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.FormLoad_Load);
            this.skinPanel2.ResumeLayout(false);
            this.skinPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label16;
        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinTextBox txtUserName;
        private CCWin.SkinControl.SkinTextBox tetPwd;
        private CCWin.SkinControl.SkinLabel label1;
        private CCWin.SkinControl.SkinLabel label4;
    }
}