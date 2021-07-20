using DevInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmChannelInfo : Form
    {
        DBCOM_DevInfo mDBCOM_DevInfo;
        DataSet DS;

        public FrmChannelInfo()
        {
            InitializeComponent();
        }

        private void FrmChannelInfo_Load(object sender, EventArgs e)
        {
            mDBCOM_DevInfo = new DBCOM_DevInfo(ClsGlobal.mDevInfoPath);

            mDBCOM_DevInfo.GetChannelData(out DS);

            dataGridView1.DataSource = DS.Tables[0];

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns[0].HeaderText = "序号";
            dataGridView1.Columns[1].HeaderText = "OCV次数";
            dataGridView1.Columns[2].HeaderText = "OCV使能";
            dataGridView1.Columns[3].HeaderText = "Shell次数";
            dataGridView1.Columns[4].HeaderText = "Shell使能";
            dataGridView1.Columns[5].HeaderText = "ACIR次数";
            dataGridView1.Columns[6].HeaderText = "ACIR使能";

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "修改异常统计数据");

            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            List<DevInfo.Model.Channel_Info> theList = new List<DevInfo.Model.Channel_Info>();

            //装载所有值到model中
            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                DevInfo.Model.Channel_Info theChannelInfo = new DevInfo.Model.Channel_Info();
                theChannelInfo.ChannelNo = i + 1;

                try
                {
                    theChannelInfo.OCV_ErrCount = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
                catch
                {
                    theChannelInfo.OCV_ErrCount = 0;
                }
                theChannelInfo.OCV_EN = bool.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());

                try
                {
                    theChannelInfo.Shell_ErrCount = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }
                catch
                {
                    theChannelInfo.Shell_ErrCount = 0;
                }
                theChannelInfo.Shell_EN = bool.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());

                try
                {
                    theChannelInfo.ACIR_ErrCount = int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                }
                catch
                {
                    theChannelInfo.ACIR_ErrCount = 0;
                }
                theChannelInfo.ACIR_EN = bool.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                theList.Add(theChannelInfo);
            }

            //update到数据库
            Thread theThread = new Thread(() => mDBCOM_DevInfo.UpdateChannelData(theList));
            theThread.Start();

            MessageBox.Show("修改成功");

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("输入出错");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "清除异常统计数据");

            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            List<DevInfo.Model.Channel_Info> theList = new List<DevInfo.Model.Channel_Info>();

            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                DevInfo.Model.Channel_Info theChannelInfo = new DevInfo.Model.Channel_Info();
                theChannelInfo.ChannelNo = i + 1;
                theChannelInfo.OCV_EN = true;
                theChannelInfo.OCV_ErrCount = 0;
                theChannelInfo.Shell_ErrCount = 0;
                theChannelInfo.Shell_EN = true;
                theChannelInfo.ACIR_ErrCount = 0;
                theChannelInfo.ACIR_EN = true;
                theList.Add(theChannelInfo);

            }

            //update到数据库
            Thread theThread = new Thread(() => mDBCOM_DevInfo.UpdateChannelData(theList));
            theThread.Start();
            Thread.Sleep(500);
            MessageBox.Show("清空成功");

            this.Close();
        }


    }
}
