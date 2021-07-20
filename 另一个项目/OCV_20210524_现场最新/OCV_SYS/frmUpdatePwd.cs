using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OCV
{
    public partial class frmUpdatePwd : Form
    {
        int mType; //密码类型：普通-》1 高级-》2

        public frmUpdatePwd()
        {
            InitializeComponent();
        }

        public frmUpdatePwd(int type)
        {
            InitializeComponent();

            mType = type;
        }

        private void frmUpdatePwd_Load(object sender, EventArgs e)
        {
            if (mType == 1)
            {
                this.Text = "修改密码(普通)";
            }
            else if (mType == 2)
            {
                this.Text = "修改密码(高级)";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("当前不允许修改密码");
            //return;

            if (mType == 1 && this.txtoldpwd.Text.Trim() != ClsGlobal.UserPwd ||
                mType == 2 && this.txtoldpwd.Text.Trim() != ClsGlobal.AdvUserPwd )
            {
                MessageBox.Show("输入旧密码出错！");
                return;
            }


            if (this.txtnewpwdqueren.Text.Trim() != this.txtnewpwd.Text.Trim())
            {
                MessageBox.Show("两次新密码不一致,请重试！");
                return;
            }

            this.txtnewpwd.Text.Trim();

            if (mType == 1)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "PASSWARD", "UserPwd", this.txtnewpwd.Text.Trim());
                ClsGlobal.UserPwd = this.txtnewpwd.Text.Trim();
            }
            else if (mType == 2)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "PASSWARD", "AdvUserPwd", this.txtnewpwd.Text.Trim());
                ClsGlobal.AdvUserPwd = this.txtnewpwd.Text.Trim();
            }
            MessageBox.Show("密码修改成功！");             

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
