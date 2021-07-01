using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using OCV;
using DB_OCV.DAL.Local;
using DB_OCV.DAL;
using OCV.OCVLogs;

namespace OCV
{
    public partial class FrmUpload : Form
    {
        private int OCVTypeSelected;
        DataSet DS_TrayInfoLoc;
        string strDEVICE_CODE;
        string strDateTime;
        string strWhere = "";
        int OCVSendType = 1;
        string ErrMessage = "";
        string TrayCode = "";
        MES2_JHW_OCV1 MES2_JHW_OCV1_Local;
        MES2_JHW_OCV2 MES2_JHW_OCV2_Local;
        MES2_JHW_OCV3 MES2_JHW_OCV3_Local;

        public FrmUpload()
        {
            InitializeComponent();
        }

        private void FrmMonitor_Load(object sender, EventArgs e)
        {
            StringBuilder mConnString = new StringBuilder();
            //StringBuilder StrB = new StringBuilder();
            string IP = ClsGlobal.Server_Local_IP;
            string Database = ClsGlobal.Server_Local_DB;
            mConnString.Append("Data Source=" + IP);
            mConnString.Append(" ;Initial Catalog=" + Database);
            //StrB.Append(" ;Trusted_Connection=SSPI");
            //StrB.Append(" ;User ID=" + UserID);
            //StrB.Append(" ;Password=" + PWD);
            mConnString.Append(" ;Integrated Security=SSPI");

            MES2_JHW_OCV1_Local = new MES2_JHW_OCV1(mConnString.ToString());
            MES2_JHW_OCV2_Local = new MES2_JHW_OCV2(mConnString.ToString());
            MES2_JHW_OCV3_Local = new MES2_JHW_OCV3(mConnString.ToString());
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void DealMsn(string msn)
        {
            ShowMsn(msn);
            ClsLogs.MESlogNet.WriteWarn("手动上传MES", msn);
        }
        private void ShowMsn(string sloginfo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ShowMsn), sloginfo);
                return;
            }
            this.txtMes.Text += System.DateTime.Now + ": " + sloginfo + "\r\n";
            txtMes.Select(txtMes.TextLength, 0);
            txtMes.ScrollToCaret();
        }

        private void dgvTrayInfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a;
            try
            {
                a = dgvTrayInfo.CurrentRow.Index;
            }
            catch (Exception)
            {
                return;
            }

            strDEVICE_CODE = dgvTrayInfo.Rows[a].Cells["DEVICE_CODE"].Value.ToString();
            strDateTime = dgvTrayInfo.Rows[a].Cells["END_DATE_TIME_STR"].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtMes.Clear();
        }

        private void btnDateOK_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                DateTime selectedDate = this.dateTimePicker1.Value;
                //List<MES2_JHW_OCV1.TrayInfo> lstTrayInfo;

                //清0
                strDEVICE_CODE = "";
                strDateTime = "";
                strWhere = "";
                ////根据测试结束时间,获取本地数据库的数据
                //if (chkDate.Checked == true && selectedDate.ToString() != "")
                //{
                //    strWhere += "END_DATE_TIME Like '%" + selectedDate.ToString("yyyy-MM-dd") + "%' and ";
                //}
                //if (chkTrayCode.Checked == true && TexTrayCode.Text != "" && TexTrayCode.Text != null)
                //{
                //    strWhere += "DEVICE_CODE='" + TexTrayCode.Text.Trim() + "'and ";
                //}

                if (chkTrayCode.Checked == true && chkDate.Checked == false)
                {
                    strWhere += "DEVICE_CODE='" + TexTrayCode.Text.Trim() + "'and ";
                    strWhere +=  "END_DATE_TIME_STR IN (SELECT MAX(END_DATE_TIME_STR) FROM MES2_JHW_OCV1 GROUP BY DEVICE_CODE)";
                }
                if (chkTrayCode.Checked == false && chkDate.Checked == true)
                {
                    strWhere += "END_DATE_TIME Like '%" + selectedDate.ToString("yyyy-MM-dd") + "%' and ";
                    strWhere += "END_DATE_TIME_STR IN (SELECT MAX(END_DATE_TIME_STR) FROM MES2_JHW_OCV1 GROUP BY DEVICE_CODE)";
                }
                if (chkTrayCode.Checked == true && chkDate.Checked == true)
                {
                    strWhere += "DEVICE_CODE='" + TexTrayCode.Text.Trim() + "'and ";
                    strWhere += "END_DATE_TIME Like '%" + selectedDate.ToString("yyyy-MM-dd") + "%' and ";
                    strWhere += "END_DATE_TIME_STR IN (SELECT MAX(END_DATE_TIME_STR) FROM MES2_JHW_OCV1 GROUP BY DEVICE_CODE)";
                }
                if (strWhere == "")
                {
                    MessageBox.Show("查询方式错误或托盘条为空！");
                    return;
                }
                if (OCVTypeSelected == 1)
                {
                  //  strWhere += "END_DATE_TIME_STR IN (SELECT MAX(END_DATE_TIME_STR) FROM MES2_JHW_OCV1 GROUP BY DEVICE_CODE)";       //重复数据只显示一条  
                                                                                                          //GetList
                    DS_TrayInfoLoc = MES2_JHW_OCV1_Local.GetList_TrayInfo(strWhere);

                    List<MES2_JHW_OCV1.TrayInfo> lstTrayInfo = new List<MES2_JHW_OCV1.TrayInfo>();

                    //DataRowToModel
                     count = DS_TrayInfoLoc.Tables[0].Rows.Count;
                    if (DS_TrayInfoLoc.Tables[0].Rows != null)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            MES2_JHW_OCV1.TrayInfo TrayInfo = MES2_JHW_OCV1_Local.DataRowToModel_TrayInfo(DS_TrayInfoLoc.Tables[0].Rows[i]);
                            lstTrayInfo.Add(TrayInfo);
                        }
                    }
                    this.dgvTrayInfo.DataSource = lstTrayInfo;
                    labnum.Text = lstTrayInfo.Count.ToString();
                }
                else if (OCVTypeSelected == 2)
                {
                   // strWhere += "END_DATE_TIME_STR IN (SELECT MAX(END_DATE_TIME_STR) FROM MES2_JHW_OCV2 GROUP BY DEVICE_CODE)";
                    //GetList
                    DS_TrayInfoLoc = MES2_JHW_OCV2_Local.GetList_TrayInfo(strWhere);

                    List<MES2_JHW_OCV2.TrayInfo> lstTrayInfo = new List<MES2_JHW_OCV2.TrayInfo>();

                    //DataRowToModel
                     count = DS_TrayInfoLoc.Tables[0].Rows.Count;
                    if (DS_TrayInfoLoc.Tables[0].Rows != null)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            MES2_JHW_OCV2.TrayInfo TrayInfo = MES2_JHW_OCV2_Local.DataRowToModel_TrayInfo(DS_TrayInfoLoc.Tables[0].Rows[i]);
                            lstTrayInfo.Add(TrayInfo);
                        }
                    }
                    this.dgvTrayInfo.DataSource = lstTrayInfo;
                    labnum.Text = lstTrayInfo.Count.ToString();
                }
                else if (OCVTypeSelected == 3)
                {
                   // strWhere += "END_DATE_TIME_STR IN (SELECT MAX(END_DATE_TIME_STR) FROM MES2_JHW_OCV3 GROUP BY DEVICE_CODE)";
                    //GetList
                    DS_TrayInfoLoc = MES2_JHW_OCV3_Local.GetList_TrayInfo(strWhere);

                    List<MES2_JHW_OCV3.TrayInfo> lstTrayInfo = new List<MES2_JHW_OCV3.TrayInfo>();

                    //DataRowToModel
                     count = DS_TrayInfoLoc.Tables[0].Rows.Count;
                    if (DS_TrayInfoLoc.Tables[0].Rows != null)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            MES2_JHW_OCV3.TrayInfo TrayInfo = MES2_JHW_OCV3_Local.DataRowToModel_TrayInfo(DS_TrayInfoLoc.Tables[0].Rows[i]);
                            lstTrayInfo.Add(TrayInfo);
                        }

                    }
                  

                    this.dgvTrayInfo.DataSource = lstTrayInfo;
                    labnum.Text = lstTrayInfo.Count.ToString();
                }
                if (count == 0)
                {
                    UpdateMsg("查询完成，无对应数据！");
                }
            }
            catch (Exception  E)
            {
                UpdateMsg("查询完成，无对应数据！");
            }
        }

        private void UpdateMsg(string msg, bool isError)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(UpdateMsg), msg, isError);
                return;
            }
            this.txtMes.Focus();
            this.txtMes.AppendText(string.Format("[{0}]{1}\r\n", isError ? "错误" : "正常", msg));
        }
        private void UpdateMsg(string msg)
        {
            UpdateMsg(msg, false);
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            string strMsn = "";
            int DataCount = 0;
            RetVal ret = new RetVal();
            if ((strDEVICE_CODE == null || strDEVICE_CODE == "") ||
             (strDateTime == null || strDateTime == ""))
            {
                MessageBox.Show("没有选中数据或者选中数据为空");
                return;
            }
            if (MessageBox.Show("确定上传托盘:" + strDEVICE_CODE + "的数据吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            btn_Upload.Enabled = false;
            
            ret = ClsGlobal.mDBCOM_OCV_Local.SendOCVData_ToMes(OCVTypeSelected, strDEVICE_CODE, strDateTime,out DataCount, out ErrMessage);
            if (ret == RetVal.SUCCESS)
            {
                strMsn = "发送[" + TrayCode + "]OCV" + OCVSendType + "数据至MES成功," + "上传数量:" + DataCount;
                DealMsn(strMsn);
            }
            else
            {
                if (ret == RetVal.FAIL_SEND_OCV)
                {
                    strMsn = "发送[" + TrayCode + "]OCV" + OCVSendType + "数至MES失败！";
                }
                else if (ret == RetVal.FAIL_SEND_TotalOCV)
                {
                    strMsn = "发送[" + TrayCode + "]OCV" + OCVSendType + "数据至MES总表失败";
                }
                else if (ret == RetVal.FAIL_WORKSTATE_UPDATE)
                {
                    strMsn = "发送OCV" + OCVSendType + "数至MES时，更新上传状态标识失败,数据将重新上传";
                }
                DealMsn(strMsn);
            }
            Thread.Sleep(200);
           
        }

        private void rdoOCV1_CheckedChanged(object sender, EventArgs e)
        {
            OCVTypeSelected = 1;
            DS_TrayInfoLoc = null;
            
            strDEVICE_CODE = "";
            strDateTime = "";
            dgvTrayInfo.DataSource = null;
        }

        private void rdoOCV2_CheckedChanged(object sender, EventArgs e)
        {
            OCVTypeSelected = 2;
            DS_TrayInfoLoc = null;
            
            strDEVICE_CODE = "";
            strDateTime = "";
            dgvTrayInfo.DataSource = null;
        }

        private void rdoOCV3_CheckedChanged(object sender, EventArgs e)
        {
            OCVTypeSelected = 3;
            DS_TrayInfoLoc = null;
           
            strDEVICE_CODE = "";
            strDateTime = "";
            dgvTrayInfo.DataSource = null;
        }
    }
}
