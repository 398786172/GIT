using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserSetting
{
    public partial class FormLoad : Form
    {
        private readonly string iP;
        private readonly string database;
        private readonly string userID;
        private readonly string pWD;
        private string mConnString = "";
        public FormLoad()
        {
            InitializeComponent();
        }
        public FormLoad(string iP, string database, string userID, string pWD)
        {
            this.iP = iP;
            this.database = database;
            this.userID = userID;
            this.pWD = pWD;
            StringBuilder StrB = new StringBuilder();
            StrB.Append("Data Source=" + this.iP);
            StrB.Append(" ;Initial Catalog=" + this.database);
            StrB.Append(" ;User ID=" + this.userID);
            StrB.Append(" ;Password=" + this.pWD);
            StrB.Append(" ;Charset=" + "utf8");
            StrB.Append(" ;Pooling=" + "true");
            mConnString = StrB.ToString();
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
                    else if (UserAuthority < 3)
                    {
                        MessageBox.Show("此用户权限不足无法登录");
                        return;
                    }
                    else if (UserAuthority < 5 && UserAuthority > 0)
                    {
                        ClsGlobal.UserAuthority = UserAuthority;
                        this.Hide(); //隐藏当前窗体 
                        UserSetting frmPorSet = new UserSetting();
                        frmPorSet.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("此用户信息异常");
                        return;
                    }
                }
                else
                {
                    ClsGlobal.UserAuthority = 4;
                    this.Hide(); //隐藏当前窗体 
                    UserSetting frmPorSet = new UserSetting();
                    frmPorSet.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("用户信息验证异常" + ex.Message);
                return;
            }
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
            //ClsGlobal.Server_OCV_IP = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "Server_OCV_QT", "IP", null);
            //ClsGlobal.Server_OCV_id = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "Server_OCV_QT", "id", null);
            //ClsGlobal.Server_OCV_Pwd = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "Server_OCV_QT", "Pwd", null);
            //ClsGlobal.Server_OCV_DB = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "Server_OCV_QT", "DB", null);

            //StringBuilder mConnString = new StringBuilder();
            //mConnString.Append("Data Source=" + ClsGlobal.Server_OCV_IP);
            //mConnString.Append(" ;Initial Catalog=" + ClsGlobal.Server_OCV_DB);
            //mConnString.Append(" ;User ID=" + ClsGlobal.Server_OCV_id);
            //mConnString.Append(" ;Password=" + ClsGlobal.Server_OCV_Pwd);
            //mConnString.Append(" ;Integrated Security=no");
          
               
           // ClsGlobal.mProjectInfo =
            ClsGlobal.Userdisc.Add(1, "一级权限");
            ClsGlobal.Userdisc.Add(2, "二级权限");
            ClsGlobal.Userdisc.Add(3, "三级权限");
            ClsGlobal.Userdisc.Add(4, "管理员");
        }
    }
}
