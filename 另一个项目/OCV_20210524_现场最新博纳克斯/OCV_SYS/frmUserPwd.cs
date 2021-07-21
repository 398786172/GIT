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
    public partial class frmUserPwd : Form
    {
        bool bOK = false;
        int mType;              //密码类型：普通-》1 高级-》2

        public frmUserPwd(int type)
        {
            InitializeComponent();

            mType = type;
        }

        private void frmUserPwd_Load(object sender, EventArgs e)
        {
            if (mType == 1)
            {
                this.Text = "普通权限";
                lblPwd.Text = "普通密码";
            }
            else if (mType == 2)
            {
                this.Text = "高级权限";
                lblPwd.Text = "高级密码";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string temp = this.txtuserpwd.Text.Trim();

            if (mType == 1 && temp == ClsGlobal.UserPwd ||
                mType == 2 && temp == ClsGlobal.AdvUserPwd)
            {
                bOK = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                bOK = false;
            }
 
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            bOK = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

      
    }
}
