using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace OCV
{
    public partial class FormUserManager : Form
    {
        public FormUserManager()
        {
            InitializeComponent();
            dgvUser.MultiSelect = false;
            dgvUser.AutoGenerateColumns = false;
        }
        string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SettingFile", "UserConfig.json");
        List<UserInfoView> lst = new List<UserInfoView>();
        bool IsCreate = false;

        private void FormUserManager_Load(object sender, EventArgs e)
        {
            cboUserType.Items.Add("管理员");
            cboUserType.Items.Add("工程师");
            cboUserType.Items.Add("操作员");
            LoadData();
            dgvUser.DataSource = lst;
            if (ClsGlobal.UserInfo.UserType != UserType.Admin)
            {
                this.Enabled = false;
            }
        }

        void LoadData()
        {
            try
            {
                lst.Clear();
                if (File.Exists(savePath))
                {
                    string jsonData = File.ReadAllText(savePath);
                    var lstUser = JsonConvert.DeserializeObject<List<UserInfo>>(jsonData);
                    foreach (var u in lstUser)
                    {
                        lst.Add(new UserInfoView(u));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载失败:{ex.Message}");
            }

        }
        void SaveData()
        {
            try
            {
                if (lst.Count <= 0)
                {
                    return;
                }
                List<UserInfo> lstUser = new List<UserInfo>();
                foreach (var u in lst)
                {
                    lstUser.Add(new UserInfo()
                    {
                        PassWord = u.PassWord,
                        UserCode = u.UserCode,
                        UserType = u.UserType
                    });
                }
                var dir = savePath.Replace("UserConfig.json", "");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string jsonData = JsonConvert.SerializeObject(lst);
                File.WriteAllText(savePath, jsonData);
                MessageBox.Show($"保存成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败:{ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsCreate)
            {
                if (string.IsNullOrEmpty(txtUserCode.Text))
                {
                    MessageBox.Show("工号不能为空!");
                    return;
                }
                if (string.IsNullOrEmpty(txtPassWord.Text))
                {
                    MessageBox.Show("密码不能为空!");
                    return;
                }
                lst.Add(new UserInfoView(new UserInfo()
                {
                    PassWord = txtPassWord.Text,
                    UserCode = txtUserCode.Text,
                    UserType = GetUserType(cboUserType.Text)
                }));
                dgvUser.DataSource = null;
                dgvUser.DataSource = lst;
                IsCreate = false;
                SaveData();
            }
            else
            {
                if (dgvUser.SelectedRows.Count == 0)
                {
                    return;
                }
                var index = dgvUser.SelectedRows[0].Index;
                dgvUser.DataSource = null;
                var model = lst[index];
                model.PassWord = txtPassWord.Text;
                model.UserCode = txtUserCode.Text;
                model.UserType = GetUserType(cboUserType.Text);
                dgvUser.DataSource = lst;
                SaveData();
            }
        }
        UserType GetUserType(string txt)
        {
            switch (txt)
            {
                case "管理员":
                    return UserType.Admin;
                case "工程师":
                    return UserType.Engineer;
                case "操作员":
                    return UserType.Operator;
                default:
                    return UserType.Operator;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            IsCreate = true;
            txtPassWord.Text = "";
            txtUserCode.Text = "";
            cboUserType.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除所选用户", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            if (dgvUser.SelectedRows.Count == 0)
            {
                return;
            }
            var model = dgvUser.SelectedRows[0].DataBoundItem as UserInfoView;
            dgvUser.DataSource = null;
            lst.Remove(model);
            dgvUser.DataSource = lst;
            SaveData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveData();

        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (lst.Count == 0)
            {
                return;
            }
            var model = lst[e.RowIndex];
            txtPassWord.Text = model.PassWord;
            txtUserCode.Text = model.UserCode;
            cboUserType.SelectedIndex = model.UserType == UserType.Admin ? 0 : model.UserType == UserType.Engineer ? 1 : 2;
        }
    }

    public class UserInfo
    {
        public string UserCode { get; set; }
        public string PassWord { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Admin = -1,
        Engineer = 0,
        Operator = 1
    }

    class UserInfoView : UserInfo
    {
        public UserInfoView(UserInfo info)
        {
            this.UserCode = info.UserCode;
            this.PassWord = info.PassWord;
            this.UserType = info.UserType;
        }
    }
}
