
using System;
using System.Data.SQLite;
using System.Windows.Forms;



namespace OCV

{

    public partial class FrmUpdatePwd : Form

    {
        //private SqLiteHelper sql;
        public FrmUpdatePwd()

        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //ClsGlobal.sql = new SqLiteHelper("OCVInfo.db");

            ////创建名为UserInfo的数据表

            //ClsGlobal.sql.CreateTable("UserInfo", new string[] { "UserName", "UserPwd", "UserAuthority", "SetTime" }, new string[] { "TEXT KEY NOT NULL  UNIQUE", "TEXT", "TEXT", "TEXT" });
            //ClsGlobal.sql.InsertValues("UserInfo", new string[] { "kinte", "123qweASD","4", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            //ClsGlobal.sql.CreateTable("UserGroup", new string[] { "Rank", "UserAuthority" }, new string[] { "TEXT KEY NOT NULL  UNIQUE", "TEXT  NOT NULL  UNIQUE" });
            ////插入两条数据

            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "一级权限", "1" });
            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "二级权限", "2" });
            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "三级权限", "3" });
            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "超级权限", "4" });
            //ClsGlobal.sql.CreateTable("SettingRecord", new string[] { "OperateValue", "Operate", "UserName","SetTime" }, new string[] { "TEXT", "TEXT", "TEXT", "TEXT" });

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Authority = "";

            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (tetPwd.Text.Trim() == "")
            {
                MessageBox.Show("密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    Authority = (i + 1).ToString();
                }
            }
            if (Authority == "")
            {
                MessageBox.Show("未选择分组!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmUserPwd frmPwd;
            //if (Authority=="3")
            //{
            //    frmPwd = new frmUserPwd(PwdType.kinte, "新建用户", txtUserName.Text.Trim(), Authority);
            //    if (frmPwd.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    } 
            //}
            //else
            //{
            //     frmPwd = new frmUserPwd(PwdType.ADVCMD, "新建用户", txtUserName.Text.Trim(), Authority);
            //    if (frmPwd.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            frmPwd = new frmUserPwd(PwdType.kinte, "新建用户", txtUserName.Text.Trim().ToUpper(), Authority);
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.InsertValues("UserInfo", new string[] { txtUserName.Text.Trim().ToUpper(), tetPwd.Text.Trim(), Authority, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            if (reader != null)
            {
                MessageBox.Show("添加成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("此用户已存在不允许新建!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string Authority = "";
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.ReadDataTable("UserInfo", txtUserName.Text.Trim().ToUpper());
            reader.Read();
            try
            {
                Authority = reader["UserAuthority"].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("此用户不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmUserPwd frmPwd;
            //if (Authority == "3")
            //{

            //    frmPwd = new frmUserPwd(PwdType.kinte,"删除用户", txtUserName.Text.Trim(), Authority);
            //    if (frmPwd.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    frmPwd = new frmUserPwd(PwdType.ADVCMD, "删除用户", txtUserName.Text.Trim(), Authority);
            //    if (frmPwd.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            frmPwd = new frmUserPwd(PwdType.kinte, "删除用户", txtUserName.Text.Trim().ToUpper(), Authority);
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            reader = ClsGlobal.sql.DeleteValuesAND("UserInfo", new string[] { "UserName" }, new string[] { txtUserName.Text.Trim().ToUpper() }, new string[] { "=" });
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("无此用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button7_Click(object sender, EventArgs e)
        {
            string Authority = "";
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (tetPwd.Text.Trim() == "")
            {
                MessageBox.Show("密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    Authority = (i + 1).ToString();
                }
            }
            if (Authority == "")
            {
                MessageBox.Show("未选择分组!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmUserPwd frmPwd;
            //if (Authority == "3")
            //{
            //    frmPwd = new frmUserPwd(PwdType.kinte, "修改用户", txtUserName.Text.Trim(), Authority);
            //    if (frmPwd.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    frmPwd = new frmUserPwd(PwdType.ADVCMD, "修改用户", txtUserName.Text.Trim(), Authority);
            //    if (frmPwd.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            frmPwd = new frmUserPwd(PwdType.kinte, "修改用户", txtUserName.Text.Trim().ToUpper(), Authority);
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("UserInfo", new string[] { "UserPwd", "UserAuthority" }, new string[] { tetPwd.Text.Trim(), Authority }, "UserName", txtUserName.Text.Trim().ToUpper(), "=");

            int count = reader.RecordsAffected;
            if (count > 0)
            {
                MessageBox.Show("修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("无此用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

