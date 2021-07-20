using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Pro.DAL;
using Pro.Model;
using System.Text.RegularExpressions;


namespace UserSetting
{
    public partial class UserSetting : Form
    {
        DataSet DS_mProjectInfo;
        DataSet DS_Info;
        private string mConnString = "";
        private readonly string iP;
        private readonly string database;
        private readonly string userID;
        private readonly string pWD;

        public UserSetting()
        {
            InitializeComponent();
        }
        public UserSetting(string ConnString)
        {

            mConnString = ConnString;
            //    mConnString = " Data Source = 192.168.100.110; Initial Catalog = byd_ocv; User ID = root; Password = VS2019; Charset = utf8; Pooling = true";
            ClsGlobal.mProjectInfo = new Pro.DAL.mysql.ProjectInfo(mConnString);
            ClsGlobal.Userdisc.Add(1, "一级权限");
            ClsGlobal.Userdisc.Add(2, "二级权限");
            ClsGlobal.Userdisc.Add(3, "三级权限");
            ClsGlobal.Userdisc.Add(4, "管理员");
            ClsGlobal.UserAuthority = 3;
            InitializeComponent();
        }
        public UserSetting(string iP, string database, string userID, string pWD)
        {

            StringBuilder StrB = new StringBuilder();
            StrB.Append("Data Source=" + this.iP);
            StrB.Append(" ;Initial Catalog=" + this.database);
            StrB.Append(" ;User ID=" + this.userID);
            StrB.Append(" ;Password=" + this.pWD);
            StrB.Append(" ;Charset=" + "utf8");
            StrB.Append(" ;Pooling=" + "true");
            mConnString = StrB.ToString();
            ClsGlobal.mProjectInfo = new Pro.DAL.mysql.ProjectInfo(mConnString);
            ClsGlobal.Userdisc.Add(1, "一级权限");
            ClsGlobal.Userdisc.Add(2, "二级权限");
            ClsGlobal.Userdisc.Add(3, "三级权限");
            ClsGlobal.Userdisc.Add(4, "管理员");
            ClsGlobal.UserAuthority = 3;
            InitializeComponent();
        }

        private void UserSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            //Environment.Exit(0);            //强制关闭
        }

        private void UserSetting_Load(object sender, EventArgs e)
        {

            if (ClsGlobal.UserAuthority == 1)
            {

                groupBox6.Visible = true;
            }
            else if (ClsGlobal.UserAuthority == 2)
            {

                groupBox6.Visible = true;
            }
            else if (ClsGlobal.UserAuthority == 3)
            {

                groupBox6.Visible = true;
            }
            else if (ClsGlobal.UserAuthority == 4)
            {

                groupBox6.Visible = true;

            }
            this.showData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int Authority = 0;
                if (txtUserName.Text.Trim() == "")
                {
                    MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                if (checkdeluser.Checked == false)
                {
                    if (tetPwd.Text.Trim() == "")
                    {
                        MessageBox.Show("密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.GetItemChecked(i))
                        {
                            Authority = i + 1;
                        }
                    }
                    if (Authority < 1 || Authority > 4)
                    {
                        MessageBox.Show("未选择分组!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                frmUserPwd frmPwd;

                if (checkAdduser.Checked == true)
                {
                    if (!ClsGlobal.mProjectInfo.ExistsUesr(txtUserName.Text.Trim().ToUpper()))
                    {
                        frmPwd = new frmUserPwd(PwdType.kinte, "新建用户", txtUserName.Text.Trim().ToUpper(), Authority.ToString());
                        if (frmPwd.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        int count = ClsGlobal.mProjectInfo.AddUesrInfo(txtUserName.Text.Trim().ToUpper(), this.tetPwd.Text.Trim(), Authority, ClsGlobal.Userdisc[Authority]);
                        if (count == 0)
                        {
                            MessageBox.Show("新建用户失败,请重新添加！");
                            return;
                        }
                        else
                        {

                            MessageBox.Show("新建用户成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    else
                    {
                        MessageBox.Show("此用户已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (checkupdateUser.Checked == true)
                {
                    if (ClsGlobal.mProjectInfo.ExistsUesr(txtUserName.Text.Trim().ToUpper()))
                    {
                        frmPwd = new frmUserPwd(PwdType.kinte, "修改用户", txtUserName.Text.Trim().ToUpper(), Authority.ToString());
                        if (frmPwd.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        int count = ClsGlobal.mProjectInfo.UpdateUesrInfo(txtUserName.Text.Trim().ToUpper(), this.tetPwd.Text.Trim(), Authority, ClsGlobal.Userdisc[Authority]);
                        if (count == 0)
                        {
                            MessageBox.Show("修改用户信息失败,请重新添加！");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("修改用户信息成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("此用户不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (checkdeluser.Checked == true)
                {
                    if (ClsGlobal.mProjectInfo.ExistsUesr(txtUserName.Text.Trim().ToUpper()))
                    {
                        frmPwd = new frmUserPwd(PwdType.kinte, "删除用户", txtUserName.Text.Trim().ToUpper(), Authority.ToString());
                        if (frmPwd.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        bool flag = ClsGlobal.mProjectInfo.DeleteUesrInfo(txtUserName.Text.Trim().ToUpper());
                        if (flag == false)
                        {
                            MessageBox.Show("删除用户信息失败!");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("删除用户信息成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("删除用户信息成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                this.showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作异常！原因：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.CurrentValue == CheckState.Checked) return;//取消选中就不用进行以下操作

            for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)

            {

                ((CheckedListBox)sender).SetItemChecked(i, false);//将所有选项设为不选中

            }

            e.NewValue = CheckState.Checked;//刷新
        }

        private void checkAdduser_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAdduser.Checked == true)
            {
                checkdeluser.Checked = false;
                checkupdateUser.Checked = false;
                tetPwd.Visible = true;
                label16.Visible = true;
            }
        }

        private void checkdeluser_CheckedChanged(object sender, EventArgs e)
        {
            if (checkdeluser.Checked == true)
            {
                checkAdduser.Checked = false;
                checkupdateUser.Checked = false;
                tetPwd.Visible = false;
                label16.Visible = false;
            }
        }

        private void checkupdateUser_CheckedChanged(object sender, EventArgs e)
        {
            if (checkupdateUser.Checked == true)
            {
                checkAdduser.Checked = false;
                checkdeluser.Checked = false;
                tetPwd.Visible = true;
                label16.Visible = true;
            }
        }

        private void showData()
        {
            try
            {
                DataTable dt = ClsGlobal.mProjectInfo.GetUserInfoList(1);
                if (dt != null)
                {

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
