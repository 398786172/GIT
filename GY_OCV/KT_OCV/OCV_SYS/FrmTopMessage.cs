using OCV.OCV_SYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmTopMessage : Form
    {
        private List<ExpData> list = null;
        private string tableName = "";

        public FrmTopMessage(string msg)
        {
            InitializeComponent();
            txtInfo.Text = msg;
            btnClear.Visible = false;
            btnDetailed.Visible = false;

        }
        public FrmTopMessage(string msg, List<ExpData> list,string table)
        {
            InitializeComponent();
            txtInfo.Text = msg;
            this.list = list;
            this.tableName = table;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void FrmTopMessage_Load(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUserPwd fuserpwd = new frmUserPwd(PwdType.ADVCMD, "清空信息");
            //PubClass.sWinTextInfo = "用户密码确认";
            if (fuserpwd.ShowDialog() == DialogResult.OK)
            {
                bool result = EventLogBLL.Instance.DeleteClear(tableName);
                if (result)
                {
                    ClsGlobal.mPLCContr.WriteDB("W11", (ushort)0);
                    MessageBox.Show("清空成功！");
                }
                else
                {
                    MessageBox.Show("清空失败！");
                }
            }
            this.Show();
     
        }

        private void btnDetailed_Click(object sender, EventArgs e)
        {
            string txt = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                    txt += list[i].ChannelNo + ",";
                else
                    txt += list[i].ChannelNo;
            }
            var table = EventLogBLL.Instance.GetTable(txt);
            this.Hide();
            using (FrmExp frm = new FrmExp(table))
            {
                frm.ShowDialog();
            }
            this.Show();
        }
    }
}
