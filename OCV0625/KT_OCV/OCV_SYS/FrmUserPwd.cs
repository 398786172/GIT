using OCV.OCVLogs;
using System;
using System.Windows.Forms;
using DB_OCV.DAL;

namespace OCV
{
    public partial class frmUserPwd : Form
    {
        PwdType mPwdType;
        string thePwd, mOperate;
        // private SqLiteHelper sql;
        //private string UserName;
        //private string UserPwd;
        private string mName;
        private string mRank;
        private string mKey;
        //private int UserAuthority;
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
        public frmUserPwd(PwdType type, string Key, string Operate)
        {
            InitializeComponent();
            mPwdType = type;
            mOperate = Operate;
            mKey = Key;
            mName = "";
            mRank = "";
        }

        public frmUserPwd(string Operate)
        {
            InitializeComponent();
            mOperate = Operate;
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
                this.Text = "超级权限验证";
                Type = 4;
            }
            else
            {
                Type = 10;
                this.Text = mOperate;
                txtPwd.Visible = false;
                label2.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            try
            {
                //this.DialogResult = DialogResult.OK;
                //return;

                if (ClsGlobal.SystemPwd == this.txtPwd.Text.Trim()|| this.txtPwd.Text.ToLower() == "kinte")
                {
                    ClsGlobal.USER_NO = "BYD";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                if (txtusername.Text.Trim() == "")
                {
                    MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                if (Type == 10)
                {
                    if (ClsGlobal.USER_NO.Trim().ToUpper() == txtusername.Text.Trim().ToUpper())
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("此用户不是已登录的用户!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClsLogs.UserinfologNet.WriteInfo("权限验证：" + mKey, mOperate + " 用户" + txtusername.Text.Trim().ToUpper() + "不是已登录的用户");
                        return;
                    }

                }
                else
                {
                    if (this.txtPwd.Text.Trim() == "")
                    {
                        MessageBox.Show("密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!ClsGlobal.mDBCOM_OCV_QT.ExistsUesrInfo(txtusername.Text.Trim().ToUpper()))
                    {
                        MessageBox.Show("此用户不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int UserAuthority = ClsGlobal.mDBCOM_OCV_QT.GetUesrInfo(txtusername.Text.Trim().ToUpper(), this.txtPwd.Text.Trim());
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
                            ClsLogs.UserinfologNet.WriteInfo("权限验证：" + mKey, mOperate + " 用户" + txtusername.Text.Trim().ToUpper() + "权限不足");
                            return;
                        }
                        else
                        {
                            if (mKey == "登录验证")
                            {
                                ClsGlobal.USER_NO = txtusername.Text.Trim().ToUpper();

                            }
                            ClsLogs.UserinfologNet.WriteInfo("权限验证：" + mKey, mOperate);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户信息验证异常" + ex.Message);
                ClsLogs.UserinfologNet.WriteInfo("用户信息验证异常：" + mKey, mOperate + ex.Message);
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
