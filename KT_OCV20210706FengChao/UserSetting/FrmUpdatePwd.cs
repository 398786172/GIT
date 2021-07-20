
using System;
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
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)

        {


        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }
    }

}

