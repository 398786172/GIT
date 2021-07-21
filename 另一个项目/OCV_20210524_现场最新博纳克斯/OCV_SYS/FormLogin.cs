using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace OCV
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        public bool IsLogin = false;

        string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SettingFile", "UserConfig.json");
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                string jsonData = File.ReadAllText(savePath);
                var lstUser = JsonConvert.DeserializeObject<List<UserInfo>>(jsonData);
                var user = lstUser.Find(a => a.UserCode == txtUserName.Text && a.PassWord == txtPassword.Text);
                if (user == null)
                {
                    MessageBox.Show("登录失败!");
                }
                else
                {
                    ClsGlobal.UserInfo = user;
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    MessageBox.Show("请输入用户名!");
                    return;
                }
                UserInfo userInfo = new UserInfo() { UserCode = txtUserName.Text, UserType = UserType.Operator, PassWord = "0" };
                ClsGlobal.UserInfo = userInfo;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.Enabled = true;
            }
            else
            {
                txtPassword.Text = "";
                txtPassword.Enabled = false;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            if (ClsSysSetting.SysSetting.LoginWithPassWord)
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
            }
        }
    }
}
