
using System;
using System.Windows.Forms;
using Pro.DAL;

namespace UserSetting
{
    public partial class frmUserPwd : Form
    {
        PwdType mPwdType;
        string thePwd, mOperate;
        // private SqLiteHelper sql;
        private string UserName;
        private string UserPwd;
        private string mName;
        private string mRank;
        private string mKey;
        private int UserAuthority;
        int Type = 0;
        public frmUserPwd()
        {
            InitializeComponent();
        }

        public frmUserPwd(PwdType type, string Operate, string Name, string Rank)
        {
            InitializeComponent();
            mPwdType = type;
            mOperate = Operate;
            mName = ":" + Name;
            mRank = "|" + Rank;
        }
        public frmUserPwd(PwdType type, string Operate)
        {
            InitializeComponent();
            mPwdType = type;
            mOperate = Operate;
            mName = "";
            mRank = "";
        }
        public frmUserPwd(PwdType type, string Key,string Operate)
        {
            InitializeComponent();
            mPwdType = type;
            mOperate = Operate;
            mKey = Key;
            mName = "";
            mRank = "";
        }
        private void frmUserPwd_Load(object sender, EventArgs e)
        {
            // sql = new SqLiteHelper("OCVInfo.db");

            if (mPwdType == PwdType.USER)
            {
                this.Text = "一级权限验证";
                Type = 1;
            }
            else if (mPwdType == PwdType.PROCESS)
            {
                this.Text = "二级权限验证";
                Type = 2;
            }
            else if (mPwdType == PwdType.ADVCMD)
            {
                this.Text = "三级权限验证";
                Type = 3;
            }
            else if (mPwdType == PwdType.kinte)
            {
                this.Text = "管理员权限验证";
                Type = 4;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClsGlobal.SystemPwd != this.txtPwd.Text.Trim())    //后门
                {
                    if (txtusername.Text.Trim() == "")
                    {
                        MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                    if (this.txtPwd.Text.Trim() == "")
                    {
                        MessageBox.Show("密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (!ClsGlobal.mProjectInfo.ExistsUesr(txtusername.Text.Trim().ToUpper()))
                    {
                        MessageBox.Show("此用户不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int UserAuthority = ClsGlobal.mProjectInfo.GetUesrInfo(txtusername.Text.Trim().ToUpper(), this.txtPwd.Text.Trim());
                    if (UserAuthority == 0)
                    {
                        MessageBox.Show("密码错误,请重新输入！");
                        return;
                    }
                    else if (UserAuthority < 5 && UserAuthority > 0)
                    {
                        if (Type > UserAuthority)
                        {
                            MessageBox.Show("此用户权限不足！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("此用户信息异常");
                        return;
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户信息验证异常" + ex.Message);
                return;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
    }
}
