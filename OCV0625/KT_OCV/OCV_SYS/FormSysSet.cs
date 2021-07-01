using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using OCV.OCVLogs;
using DevInfo;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace OCV
{
    public partial class FormSysSet : Form
    {
        public FormSysSet()
        {
            InitializeComponent();
        }
        private int BTCount;
        private bool Loadflag = false;
        DevInfo.Model.SET_Info mSET_Info = new DevInfo.Model.SET_Info();   //检测参数设置
        DBCOM_DevInfo mDBCOM_DevInfo;
        void FormSysSet_Load(object sender, System.EventArgs e)
        {
            Loadflag = false;
            List<DevInfo.Model.SET_Info> lstSetInfo;
            mDBCOM_DevInfo = new DBCOM_DevInfo(ClsGlobal.mDevInfoPath);
            //获取参数
            mDBCOM_DevInfo.GetSetInfoList(out lstSetInfo);
            if (lstSetInfo.Count == 0)
            {
                mSET_Info.SetName = "Set1";
                mSET_Info.OCV_SetEN = true;
                mSET_Info.OCV_UCL = 5000;
                mSET_Info.OCV_LCL = 0;
                mSET_Info.OCV_TestTimes = 5;

                mSET_Info.Shell_SetEN = true;
                mSET_Info.Shell_UCL = 5000;
                mSET_Info.Shell_LCL = 0;
                mSET_Info.Shell_TestTimes = 5;

                mSET_Info.ACIR_SetEN = true;
                mSET_Info.ACIR_UCL = 100;
                mSET_Info.ACIR_LCL = 0;
                mSET_Info.ACIR_TestTimes = 5;

                mDBCOM_DevInfo.SaveSetInfo(mSET_Info);
            }
            else
            {
                mSET_Info.SetName = lstSetInfo[0].SetName;
                mSET_Info.OCV_SetEN = lstSetInfo[0].OCV_SetEN;
                mSET_Info.OCV_UCL = lstSetInfo[0].OCV_UCL;
                mSET_Info.OCV_LCL = lstSetInfo[0].OCV_LCL;
                mSET_Info.OCV_TestTimes = lstSetInfo[0].OCV_TestTimes;

                mSET_Info.Shell_SetEN = lstSetInfo[0].Shell_SetEN;
                mSET_Info.Shell_UCL = lstSetInfo[0].Shell_UCL;
                mSET_Info.Shell_LCL = lstSetInfo[0].Shell_LCL;
                mSET_Info.Shell_TestTimes = lstSetInfo[0].Shell_TestTimes;

                mSET_Info.ACIR_SetEN = lstSetInfo[0].ACIR_SetEN; ;
                mSET_Info.ACIR_UCL = lstSetInfo[0].ACIR_UCL; ;
                mSET_Info.ACIR_LCL = lstSetInfo[0].ACIR_LCL;
                mSET_Info.ACIR_TestTimes = lstSetInfo[0].ACIR_TestTimes;
            }

            //测试设定
            txtOCVUCL.Text = mSET_Info.OCV_UCL.ToString();
            txtOCVLCL.Text = mSET_Info.OCV_LCL.ToString();
            txtOCVTestTimes.Text = mSET_Info.OCV_TestTimes.ToString();
            chkSetEnOCV.Checked = mSET_Info.OCV_SetEN;

            txtShellUCL.Text = mSET_Info.Shell_UCL.ToString();
            txtShellLCL.Text = mSET_Info.Shell_LCL.ToString();
            txtShellTestTimes.Text = mSET_Info.Shell_TestTimes.ToString();
            chkSetEnShell.Checked = mSET_Info.Shell_SetEN;

            txtACIRUCL.Text = mSET_Info.ACIR_UCL.ToString();
            txtACIRLCL.Text = mSET_Info.ACIR_LCL.ToString();
            txtACIRTestTimes.Text = mSET_Info.ACIR_TestTimes.ToString();
            chkSetEnACIR.Checked = mSET_Info.ACIR_SetEN;
            txtTime.Text = ClsGlobal.Time_t.ToString();
            txtTrayExpNum.Text = ClsGlobal.TrayExp_m.ToString();
            txtChannelExpNum.Text = ClsGlobal.ChannelExp_n.ToString();
            txtStartIndex.Text = ClsGlobal.StartIndex.ToString();
            txtBatTypeLen.Text = ClsGlobal.BatTypeLen.ToString();
            txtClassLen.Text = ClsGlobal.BatClassLen.ToString();
            txtOCV23BCValue.Text = ClsGlobal.OCV23BCValue.ToString();
            dtpOCVBCTime.Value = ClsGlobal.OCV3BCTime;
            for (int i = 1; i < 4; i++)
            {
                cmbOCVNum.Items.Add(i);
            }
            cmbOCVNum.Text = ClsGlobal.OCVType.ToString();

            if (ClsGlobal.IsLocalOCVType == 1)
            {
                rbLocalOCV.Checked = true;
            }
            else
            {
                rbRemote.Checked = true;
            }
            if (ClsGlobal.ExpEnable == 1)
            {
                chbExpEnable.Checked = true;
            }
            else
            {
                chbExpEnable.Checked = false;
            }

            if (ClsGlobal.TestType == 0)
            {
                rdoPNVolt.Checked = true;
            }
            if (ClsGlobal.TestType == 1)
            {
                rdoPNVoltandShell.Checked = true;
            }
            if (ClsGlobal.TestType == 2)
            {
                rdoPNVoltandACIR.Checked = true;
            }

            if (ClsGlobal.TestType == 3)
            {
                rdoPNVoltandACIR3.Checked = true;
            }
            //else if (ClsGlobal.TestType == 2)
            //{
            //    rdoTray1_74CH.Checked = true;
            //}
            cmbCOM_DMT.Items.Add(" ");
            for (int i = 1; i < 30; i++)
            {
                cmbCOM_DMT.Items.Add("COM" + i);
                cmbCOM_Tmp.Items.Add("COM" + i);
                cmbCOM_RT.Items.Add("COM" + i);
                cmbScan_Port.Items.Add("COM" + i);
            }

            cmbCOM_Tmp.Text = ClsGlobal.Temp_Port;
            cmbCOM_DMT.Text = ClsGlobal.DMT_Port;
            cmbCOM_RT.Text = ClsGlobal.RT_Port;
            cmbScan_Port.Text = ClsGlobal.Scan_Port;

            if (ClsGlobal.DMTComMode == 1)
            {
                rdoSerialCom.Checked = true;
                cmbCOM_DMT.Enabled = true;
            }
            else if (ClsGlobal.DMTComMode == 2)
            {
                rdoUSBCom.Checked = true;
                cmbCOM_DMT.Enabled = false;
            }
            //USB地址
            txtUSBAddr.Text = ClsGlobal.DMT_USBAddr;
            txtPLCIP.Text = ClsGlobal.PLCAddr;
            txtPLCPort.Text = ClsGlobal.PLCPort.ToString();
            this.cmbCOM_Count.Text = ClsGlobal.BTcount.ToString();
            for (int i = 0; i < ClsGlobal.BTcount; i++)
            {
                dgvBt4560.Rows[i].Cells[0].Value = i + 1;
                dgvBt4560.Rows[i].Cells[1].Value = ClsGlobal.RT4560_Port[i];
                dgvBt4560.Rows[i].Cells[2].Value = ClsGlobal.TestFreq[i];
                dgvBt4560.Rows[i].Cells[3].Value = ClsGlobal.InitBT4560[i].ToString();
                dgvBt4560.Rows[i].Cells[4].Value = ClsGlobal.BT4560RANG[i].ToString();
                dgvBt4560.Rows[i].Cells[5].Value = ClsGlobal.ChNo[i];
            }
            this.cmb_SwitchCount.Text = ClsGlobal.Switch_Count.ToString();
            for (int i = 0; i < ClsGlobal.Switch_Count; i++)
            {
                dgvcSwitch.Rows[i].Cells[0].Value = i + 1;
                dgvcSwitch.Rows[i].Cells[1].Value = ClsGlobal.Switch_Port[i];
                dgvcSwitch.Rows[i].Cells[2].Value = ClsGlobal.SwitchVersionStr[i];
                dgvcSwitch.Rows[i].Cells[3].Value = ClsGlobal.SwitchChNo[i];
            }
            txtMaxReTestTimes.Text = ClsGlobal.MaxTestNum.ToString();
            txtRetest_ACIR.Text = ClsGlobal.ReTestLmt_ACIR.ToString();

            txtOCVServer.Text = ClsGlobal.Server_OCV_IP;
            txtOCVDB.Text = ClsGlobal.Server_OCV_DB;
            txtOCVUID.Text = ClsGlobal.Server_OCV_id;
            txtOCVPWD.Text = ClsGlobal.Server_OCV_Pwd;

            txtQTServer.Text = ClsGlobal.Server_QT_IP;
            txtQTDB.Text = ClsGlobal.Server_QT_DB;
            txtQTUId.Text = ClsGlobal.Server_QT_id;
            txtQTPwd.Text = ClsGlobal.Server_QT_Pwd;

            txtOCVLocal.Text = ClsGlobal.Server_Local_IP;
            txtOCVLocalDB.Text = ClsGlobal.Server_Local_DB;
            txtOCVlocalUID.Text = ClsGlobal.Server_Local_id;
            txtOCVlocalPWD.Text = ClsGlobal.Server_Local_Pwd;
            txtReportPath.Text = ClsGlobal.ReportPath;
            if (ClsGlobal.TrayTypeFlag == 1)
            {
                rdoTray1_38CH.Checked = true;
                //rdoTray1_74CH.Checked = false;
            }
            else if (ClsGlobal.TrayTypeFlag == 2)
            {
                //rdoTray1_74CH.Checked = true;
                rdoTray1_38CH.Checked = false;
            }
            else if (ClsGlobal.TrayTypeFlag == 3)
            {
                rdoTray1_38CH.Checked = false;
            }
            if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
            {
                rb_Inte.Checked = (int)eRunMode.NormalTest == 1;
            }
            else if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
            {
                rb_Local.Checked = (int)eRunMode.OfflineTest == 3;
            }
            else if (ClsGlobal.OCV_RunMode == eRunMode.GoAhead)
            {
                rb_GoAhead.Checked = (int)eRunMode.GoAhead == 2;
            }

            if (ClsGlobal.EN_TestTemp == 0)
            {
                chkNoTemp.Checked = true;
            }
            else
            {
                chkNoTemp.Checked = false;
            }
            cmbDeviceNo.Text = ClsGlobal.DeviceNo;
            chkDebug.Checked = (ClsGlobal.DebugLog == 1);

            cmbTrayCodeLen.Text = ClsGlobal.TrayCodeLengh.ToString();

            //工程设定加载
            for (int i = 0; i < ClsGlobal.lstBatSet.Count; i++)
            {
                cmbCellCodeLen.Items.Add(ClsGlobal.lstBatSet[i].P_CellCodeLength);
            }
            if (cmbCellCodeLen.Items.Count > 0)
            {
                cmbCellCodeLen.SelectedIndex = 0;
            }
            textBox1.Text = ClsGlobal.SITE;
            texResrce.Text = ClsGlobal.RESRCE;
            txt_JT.Text = ClsGlobal.JT_NO;
            txtOPEATION_ID.Text = ClsGlobal.OPEATION_ID;
            tb_MESpcid.Text = ClsGlobal.PCID;
            tbTrayCode.Text = ClsGlobal.TrayCodeRegEx;
            tbCellCode.Text = ClsGlobal.CellCodeRegEx;
            timer_SysSet.Interval = 500;
            timer_SysSet.Start();
            Loadflag = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "参数设置", "设置OCV测试工序");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int Value = 0;
            if (int.TryParse(cmbOCVNum.Text, out Value))
            {
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { Value.ToString() }, "SetType", "OCVType", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.OCVType = Value;
                    ClsLogs.INIlogNet.WriteInfo("参数设置", "设置OCV测试工序：OCV" + Value + "成功");
                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", "OCV" );
                    ClsGlobal.SetParflag = true;
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("参数设置", "设置OCV测试工序：OCV" + Value + "失败");
                    MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("设置OCV测试工序错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "参数设置", "设置测量类型");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int Value = 0;
            if (rdoPNVolt.Checked == true)
            {
                Value = 0;
            }
            else if (rdoPNVoltandShell.Checked == true)
            {
                Value = 1;
            }
            if (rdoPNVoltandACIR.Checked == true)
            {
                Value = 2;
            }
            if (rdoPNVoltandACIR3.Checked == true)
            {
                Value = 3;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { Value.ToString() }, "SetType", "TestType", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                // ClsGlobal.TestType = Value;
                ClsLogs.INIlogNet.WriteInfo("参数设置", "设置测量类型:" + Value + "成功");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("参数设置", "设置测量类型:" + Value + "失败");
                MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "仪表端口");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            int count = 0;

            if (txtPLCIP.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { txtPLCIP.Text }, "Parameter", "PLC_PIAddr", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.PLCAddr = txtPLCIP.Text;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "PLCip设置:" + txtPLCIP.Text + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "PLCip设置:" + txtPLCIP.Text + "失败");
                    MessageBox.Show("PLCip设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "PLCip不能为空");
                MessageBox.Show("PLCip不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int Value = 0;
            if (int.TryParse(txtPLCPort.Text, out Value))
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { txtPLCPort.Text }, "Parameter", "PLC_Port", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.PLCPort = Value;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "PLC_Port设置:" + Value + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "PLC_Port设置:" + Value + "失败");
                    MessageBox.Show("PLC_Port设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("参数设置", "PLC_Port设置:" + Value + "失败");
                MessageBox.Show("PLC_Port设置参数类型错误！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnDebug_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "启用调试日志");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int Value = chkDebug.Checked ? 1 : 0;
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { Value.ToString() }, "SetType", "DebugLogFlag", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                //ClsGlobal.DebugLog = Value;
                ClsLogs.INIlogNet.WriteInfo("系统参数设置", "设置是否启用调试日志:" + Value + "成功");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设置是否启用调试日志:" + Value + "失败");
                MessageBox.Show("设置是否启用调试日志发生异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "系统参数设置", "设置测试通道数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int TrayTypeFlag = 0;
            if (rdoTray1_38CH.Checked == true)
            {
                TrayTypeFlag = 1;
            }
            //else if (rdoTray1_74CH.Checked == true)
            //{
            //    TrayTypeFlag = 2;
            //}
            string mes = "";
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { TrayTypeFlag.ToString() }, "SetType", "TrayBattType", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {

                //TrayBattType托盘类型 
                if (rdoTray1_38CH.Checked == true)
                {
                    //ClsGlobal.TrayTypeFlag = 2;
                    //ClsGlobal.StartA = 1;
                    //ClsGlobal.StartB = 81;
                    //ClsGlobal.TrayType = 80;
                    mes = "38通道";
                }
                ClsLogs.INIlogNet.WriteInfo("系统参数设置", "测试通道:" + mes + "成功");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设置测量类型:" + mes + "失败");
                MessageBox.Show("设置测量类型失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button9_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "运行模式设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int Value = 1;
            if (rb_Local.Checked == true)
            {
                Value = 3;
            }
            else if (rb_Inte.Checked == true)
            {
                Value = 1;
            }
            else if (rb_GoAhead.Checked == true)
            {
                Value = 2;
            }

            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { Value.ToString() }, "SetType", "Run_Mode", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                // ClsGlobal.OCV_RunMode = (eRunMode)int.Parse(Value.ToString());
                ClsLogs.INIlogNet.WriteInfo("系统参数设置", "运行模式设置:" + Value + "成功");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "运行模式设置:" + Value + "失败");
                MessageBox.Show("运行模式设置发生异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



        }
        private void button7_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "设备序号设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            if (cmbDeviceNo.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { cmbDeviceNo.Text }, "SetType", "DeviceNo", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.DeviceNo = cmbDeviceNo.Text;
                    ClsGlobal.SetParflag = true;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "设备线号设置:" + cmbDeviceNo.Text + "成功");
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设备线号设置:" + cmbDeviceNo.Text + "失败");
                    MessageBox.Show("设备线号设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "设备线号不能为空");
                MessageBox.Show("设备线号设置异常！", "设备线号不能为空", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "托盘条码长度设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            int Value;
            if (int.TryParse(cmbTrayCodeLen.Text, out Value))
            {
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { cmbTrayCodeLen.Text }, "SetType", "TrayCodeLen", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.TrayCodeLengh = Value;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "托盘条码长度设置:" + Value + "成功");
                    ClsGlobal.SetParflag = true;
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "托盘条码长度设置:" + Value + "失败");
                    MessageBox.Show("托盘条码设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "托盘条码长度数据类型异常");
                MessageBox.Show("托盘条码长度数据类型异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "电池条码长度设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            int Value, Value1, Value2;
            if (int.TryParse(cmbCellCodeLen.Text, out Value) && int.TryParse(txtKeyStart.Text, out Value1) && int.TryParse(txtModelLength.Text, out Value2))
            {
                ClsGlobal.sql.InsertValues("batSeting", new string[] { Value.ToString(), Value1.ToString(), Value2.ToString() });

                reader = ClsGlobal.sql.UpdateValues("batSeting", new string[] { "KeyStart", "ModelLenth" }, new string[] { Value1.ToString(), Value2.ToString() }, "CellCodeLen", Value.ToString(), "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.CellCodeLengh = Value;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "电池条码长度设置:" + Value + "成功");
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "电池型号起始位置设置:" + Value1 + "成功");
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "电池型号长度设置:" + Value1 + "成功");
                    ClsGlobal.SetParflag = true;
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "电池条码长度设置:" + Value + "失败");
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "电池型号起始位置设置:" + Value1 + "失败");
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "电池型号长度设置:" + Value1 + "失败");
                    MessageBox.Show("托盘条码设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "电池条码长度或起始位置或电池型号长度数据类型异常");
                MessageBox.Show("电池条码长度数据类型异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void btnChannelCountSet_Click(object sender, EventArgs e)
        {

            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "岗位条码设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            if (texResrce.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { texResrce.Text.Trim() }, "SetType", "DeviceCode", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.RESRCE = texResrce.Text.Trim();
                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "RESOURCE_ID", "resource_id", texResrce.Text.Trim());
                    ClsGlobal.SetParflag = true;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "岗位条码设置:" + texResrce.Text.Trim() + "成功");
                    MessageBox.Show("岗位条码设置成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "岗位条码设置:" + texResrce.Text + "失败");
                    MessageBox.Show("岗位条码设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "岗位条码不能为空");
                MessageBox.Show("岗位条码不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void timer_SysSet_Tick(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true)
            {
                this.tabSysSet.Enabled = false;
            }
            else
            {
                this.tabSysSet.Enabled = true;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "设置分容数据库参数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("SqlDBSet", new string[] { "ServerIP", "DBName", "UserName", "Password" }, new string[] { txtQTServer.Text.Trim(), txtQTDB.Text.Trim(), txtQTUId.Text.Trim(), txtQTPwd.Text.Trim() }, "DBType", "Server_QT", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                //ClsGlobal.Server_QT_IP = txtQTServer.Text.Trim(); ;
                //ClsGlobal.Server_QT_DB = txtQTDB.Text.Trim();
                //ClsGlobal.Server_QT_id= txtQTUId.Text.Trim();
                //ClsGlobal.Server_QT_Pwd = txtQTPwd.Text;
                ClsLogs.INIlogNet.WriteInfo("通信设置", "分容数据库设置成功");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "分容数据库设置成功");
                MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void rdoSerialCom_Click(object sender, EventArgs e)
        {
            cmbCOM_DMT.Enabled = true;
        }

        private void rdoUSBCom_Click(object sender, EventArgs e)
        {
            cmbCOM_DMT.Enabled = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "设置机台编号");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            if (texResrce.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { txt_JT.Text.Trim() }, "SetType", "JT_NO", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.JT_NO = txt_JT.Text.Trim(); 
                    ClsGlobal.SetParflag = true;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "设置机台编号:" + txt_JT.Text.Trim() + "成功");
                    MessageBox.Show("设置机台编号成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设置机台编号:" + txt_JT.Text.Trim() + "失败");
                    MessageBox.Show("设置机台编号设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设置机台编号");
                MessageBox.Show("设置机台编号！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void FormSysSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClsGlobal.SetParflag == true)
            {
                MessageBox.Show("设置过的参数重启生效", "提示", MessageBoxButtons.OK);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "设置OPEATION_ID");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            if (texResrce.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { txtOPEATION_ID.Text.Trim() }, "SetType", "OPEATION_ID", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.JT_NO = txt_JT.Text.Trim();
                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", txtOPEATION_ID.Text.Trim());
                    ClsGlobal.SetParflag = true;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "设置OPEATION_ID:" + txtOPEATION_ID.Text.Trim() + "成功");
                    MessageBox.Show("设置OPEATION_ID成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设置OPEATION_ID:" + txtOPEATION_ID.Text.Trim() + "失败");
                    MessageBox.Show("设置OPEATION_ID异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "设置txtOPEATION_ID");
                MessageBox.Show("设置OPEATION_ID！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "设置OCV本地数据库参数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("SqlDBSet", new string[] { "ServerIP", "DBName", "UserName", "Password" }, new string[] { txtOCVLocal.Text.Trim(), txtOCVLocalDB.Text.Trim(), txtOCVlocalUID.Text.Trim(), txtOCVlocalPWD.Text.Trim() }, "DBType", "Server_Local", "=");

            int count = reader.RecordsAffected;
            if (count > 0)
            {

                //ClsGlobal.Server_Local_IP = txtOCVLocal.Text.Trim(); ;
                //ClsGlobal.Server_Local_DB = txtOCVLocalDB.Text.Trim();
                //ClsGlobal.Server_Local_id = txtOCVlocalUID.Text.Trim();
                //ClsGlobal.Server_Local_Pwd = txtOCVlocalPWD.Text.Trim();
                ClsLogs.INIlogNet.WriteInfo("通信设置", "设置OCV本地数据库参数");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "设置OCV本地数据库参数");
                MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "设置OCV服务器数据库参数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("SqlDBSet", new string[] { "ServerIP", "DBName", "UserName", "Password" }, new string[] { txtOCVServer.Text.Trim(), txtOCVDB.Text.Trim(), txtOCVUID.Text.Trim(), txtOCVPWD.Text.Trim() }, "DBType", "Server_OCV_QT", "=");

            int count = reader.RecordsAffected;
            if (count > 0)
            {
                //ClsGlobal.Server_OCV_IP = txtOCVServer.Text.Trim(); ;
                //ClsGlobal.Server_OCV_DB  = txtOCVDB.Text.Trim();
                //ClsGlobal.Server_OCV_id = txtOCVUID.Text.Trim();
                //ClsGlobal.Server_OCV_Pwd  = txtOCVPWD.Text.Trim();
                ClsLogs.INIlogNet.WriteInfo("通信设置", "OCV设置数据库设置成功");
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "OCV设置数据库设置失败");
                MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "仪表端口");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            int count = 0;
            if (rdoSerialCom.Checked == true)
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { cmbCOM_DMT.Text }, "Parameter", "DMT_Port", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.DMT_Port = cmbCOM_DMT.Text;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "万用表端口设置:" + cmbCOM_DMT.Text + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "万用表端口设置:" + cmbCOM_DMT.Text + "失败");
                    MessageBox.Show("万用表端口设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { "1" }, "Parameter", "DMT_COM_Mode", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.DMTComMode = 1;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "万用表端通信模式:" + 1 + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "万用表端通信模式:" + 1 + "失败");
                    MessageBox.Show("万用表端通信模式异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rdoUSBCom.Checked == true)
            {

                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { txtUSBAddr.Text }, "Parameter", "DMT_USBAddr", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.DMT_USBAddr = txtUSBAddr.Text;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "万用表端口设置:" + txtUSBAddr.Text + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "万用表端口设置:" + txtUSBAddr.Text + "失败");
                    MessageBox.Show("万用表端口设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { "2" }, "Parameter", "DMT_COM_Mode", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.DMTComMode = 2;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "万用表端口设置:" + 2 + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "万用表端口设置:" + 2 + "失败");
                    MessageBox.Show("万用表端通信模式异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (cmbCOM_Tmp.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { cmbCOM_Tmp.Text }, "Parameter", "Temp_Port", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.Temp_Port = cmbCOM_Tmp.Text;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "温度采集卡端口设置:" + cmbCOM_Tmp.Text + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "温度采集卡端口设置:" + cmbCOM_Tmp.Text + "失败");
                    MessageBox.Show("温度采集卡端口设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "温度采集卡端口设置不能为空");
                MessageBox.Show("温度采集卡端口设置不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string val = "0";
            if (chkNoTemp.Checked == true)
            {
                val = "0";
            }
            else
            {
                val = "1";
            }

            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { val }, "SetType", "EN_TestTemp", "=");
            count = reader.RecordsAffected;
            if (count > 0)
            {
                //ClsGlobal.Temp_Port = cmbCOM_Tmp.Text;
                ClsLogs.INIlogNet.WriteInfo("测试设置", "温度采集功能设定: " + val + "成功");
                ClsGlobal.SetParflag = true;
            }
            else
            {
                ClsLogs.INIlogNet.WriteInfo("测试设置", "温度采集功能设定: " + val + "失败");
                MessageBox.Show("温度采集功能设定异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbCOM_RT.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { cmbCOM_RT.Text }, "Parameter", "RT_Port", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {

                    ClsLogs.INIlogNet.WriteInfo("通信设置", "Bt3562端口设置:" + cmbCOM_RT.Text + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "Bt3562端口设置:" + cmbCOM_RT.Text + "失败");
                    MessageBox.Show("Bt3562端口设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "Bt3562端口设置不能为空");
                MessageBox.Show("Bt3562端口设置不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dictionary<string, object> dicRuleParaObject = new Dictionary<string, object>();
            //BT5460参数
            dicRuleParaObject = new Dictionary<string, object>();
            dicRuleParaObject.Add("ComCount", this.cmbCOM_Count.Text);

            if (int.Parse(this.cmbCOM_Count.Text) > dgvBt4560.RowCount)
            {
                MessageBox.Show("端口参数数量与端口数量不同!");
                return;
            }
            for (int i = 0; i < int.Parse(this.cmbCOM_Count.Text); i++)
            {
                if (dgvBt4560[1, i].Value.ToString() == "")
                {
                    MessageBox.Show("端口不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvBt4560[2, i].Value.ToString() == "")
                {
                    MessageBox.Show("测试频率不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvBt4560[3, i].Value.ToString() == "")
                {
                    MessageBox.Show("测试方式不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvBt4560[4, i].Value.ToString() == "")
                {
                    MessageBox.Show("测试量程不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvBt4560[5, i].Value.ToString() == "")
                {
                    MessageBox.Show("测试通道不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dicRuleParaObject.Add("Port" + i, dgvBt4560[1, i].Value);
                dicRuleParaObject.Add("TestFreq" + i, dgvBt4560[2, i].Value);
                dicRuleParaObject.Add("TestType" + i, dgvBt4560[3, i].Value);
                dicRuleParaObject.Add("TestRang" + i, dgvBt4560[4, i].Value);
                dicRuleParaObject.Add("TestChNo" + i, dgvBt4560[5, i].Value);
            }
            //生成JSON字符串
            string jsonStr = JsonConvert.SerializeObject(dicRuleParaObject);

            if (jsonStr != "")
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { jsonStr }, "Parameter", "InitBT4560", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {

                    ClsLogs.INIlogNet.WriteInfo("通信设置", "Bt4560参数设置:" + jsonStr + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "Bt4560参数设置:" + jsonStr + "失败");
                    MessageBox.Show("Bt4560参数设置", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "Bt4560参数设置不能为空");
                MessageBox.Show("Bt4560参数设置不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbScan_Port.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { cmbScan_Port.Text }, "Parameter", "Scan_Port", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.Temp_Port = cmbCOM_Tmp.Text;
                    ClsLogs.INIlogNet.WriteInfo("通信设置", "扫码器端口设置:" + cmbScan_Port.Text + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "扫码器端口设置:" + cmbScan_Port.Text + "失败");
                    MessageBox.Show("扫码器端口设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "扫码器端口设置不能为空");
                MessageBox.Show("扫码器端口设置不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "仪表端口");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            int count = 0;

            Dictionary<string, object> dicParaObject = new Dictionary<string, object>();
            //参数
            dicParaObject = new Dictionary<string, object>();
            dicParaObject.Add("ComCount", this.cmb_SwitchCount.Text);

            if (int.Parse(this.cmb_SwitchCount.Text) > dgvcSwitch.RowCount)
            {
                MessageBox.Show("端口参数数量与端口数量不同!");
                return;
            }
            for (int i = 0; i < int.Parse(this.cmb_SwitchCount.Text); i++)
            {
                if (dgvcSwitch[1, i].Value.ToString() == "")
                {
                    MessageBox.Show("端口不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvcSwitch[2, i].Value.ToString() == "")
                {
                    MessageBox.Show("切换版版本不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvcSwitch[3, i].Value.ToString() == "")
                {
                    MessageBox.Show("测试通道不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dicParaObject.Add("Port" + i, dgvcSwitch[1, i].Value);
                dicParaObject.Add("SwitchVersion" + i, dgvcSwitch[2, i].Value);
                dicParaObject.Add("TestChNo" + i, dgvcSwitch[3, i].Value);
            }
            //生成JSON字符串
            string jsonStr = JsonConvert.SerializeObject(dicParaObject);

            if (jsonStr != "")
            {
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { jsonStr }, "Parameter", "Switch_Port", "=");
                count = reader.RecordsAffected;
                if (count > 0)
                {

                    ClsLogs.INIlogNet.WriteInfo("通信设置", "切换系统参数设置:" + jsonStr + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("通信设置", "切换系统参数设置:" + jsonStr + "失败");
                    MessageBox.Show("切换系统参数设置", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("通信设置", "切换系统参数设置不能为空");
                MessageBox.Show("切换系统参数设置不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "通信设置", "设置MES参数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (txtMesURL.Text != "")
            {
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("MESSet", new string[] { "Value" }, new string[] { txtMesURL.Text.Trim() }, "MESType", "MESURL_OCV", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.MESURL_OCV = txtMesURL.Text.Trim();
                    ClsGlobal.SetParflag = true;
                    ClsLogs.INIlogNet.WriteInfo("参数设置", "设置MES参数URL:" + txtMesURL.Text.Trim() + "成功");
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("参数设置", "设置MES参数URL:" + txtMesURL.Text + "失败");
                    MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("设置MES参数URL失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtSITE.Text != "")
            {
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("MESSet", new string[] { "Value" }, new string[] { txtSITE.Text.Trim() }, "MESType", "SITE", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.SITE = txtSITE.Text;

                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "SITE", "site", txtSITE.Text.Trim());
                    ClsLogs.INIlogNet.WriteInfo("参数设置", "设置MES参数SITE:" + txtSITE.Text.Trim() + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("参数设置", "设置MES参数SITE:" + txtSITE.Text + "失败");
                    MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("设置MES参数SITE失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtSITE.Text != "")
            {
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { txtSITE.Text.Trim() }, "SetType", "SITE", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.SITE = txtSITE.Text;

                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "SITE", "site", txtSITE.Text.Trim());
                    ClsLogs.INIlogNet.WriteInfo("参数设置", "设置参数SITE:" + txtSITE.Text.Trim() + "成功");
                    ClsGlobal.SetParflag = true;
                }

                else
                {
                    ClsLogs.INIlogNet.WriteFatal("参数设置", "设置参数SITE:" + txtSITE.Text + "失败");
                    MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("设置参数SITE失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbCellCodeLen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string theSel = cmbCellCodeLen.SelectedItem.ToString();

            if (theSel == "" || theSel == null)
            {

                return;
            }
            try
            {
                //读取电池条码参数
                DataSet BatdDs = ClsGlobal.sql.ConvertDataReaderToDataSet("batSeting");
                int Value = 0;
                for (int i = 0; i < BatdDs.Tables[0].Rows.Count; i++)
                {
                    if (int.TryParse(BatdDs.Tables[0].Rows[i][0].ToString(), out Value))
                    {
                        if (Value.ToString() == theSel)
                        {
                            if (int.TryParse(BatdDs.Tables[0].Rows[i][1].ToString(), out Value))
                            {
                                txtKeyStart.Text = Value.ToString();
                            }
                            if (int.TryParse(BatdDs.Tables[0].Rows[i][2].ToString(), out Value))
                            {
                                txtModelLength.Text = Value.ToString();
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void cmbCellCodeLen_TextChanged(object sender, EventArgs e)
        {
            txtModelLength.Text = "";
            txtKeyStart.Text = "";
        }

        private void btnSaveMaxTestSetInfo_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "复测参数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int Value = 0;
            if (int.TryParse(txtMaxReTestTimes.Text, out Value))
            {
                if (Value < 1 || Value > 3)
                {
                    MessageBox.Show("最大输入次数范围(1~3),请重新输入");
                    txtMaxReTestTimes.Text = "";
                    return;
                }


                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { Value.ToString() }, "SetType", "MaxTestNum", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    //ClsGlobal.MaxTestNum  = Value;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "最大复测次数设置:" + Value + "成功");
                    ClsGlobal.SetParflag = true;

                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "最大复测次数设置:" + Value + "失败");
                    MessageBox.Show("最大复测次数设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "最大复测次数数据类型异常");
                MessageBox.Show("最大复测次数数据类型异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.TryParse(txtRetest_ACIR.Text, out Value))
            {
                if (Value < 0.1 || Value > 1000)
                {
                    MessageBox.Show("内阻值输入范围（0.1~1000）,请重新输入");
                    txtRetest_ACIR.Text = "";
                    return;
                }
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { Value.ToString() }, "SetType", "ReTestLmt_ACIR", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.ReTestLmt_ACIR = Value;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "复测条件设置:" + Value + "成功");
                    ClsGlobal.SetParflag = true;
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "复测条件设置:" + Value + "失败");
                    MessageBox.Show("复测条件设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "复测条件数据类型异常");
                MessageBox.Show("复测条件数据类型异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClsGlobal.SetParflag = true;
            MessageBox.Show("复测参数设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveSetInfo_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "测试异常判断设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                mSET_Info.OCV_UCL = decimal.Parse(txtOCVUCL.Text);
                mSET_Info.OCV_LCL = decimal.Parse(txtOCVLCL.Text);
                mSET_Info.OCV_TestTimes = int.Parse(txtOCVTestTimes.Text);
                mSET_Info.OCV_SetEN = chkSetEnOCV.Checked;

                mSET_Info.Shell_UCL = decimal.Parse(txtShellUCL.Text);
                mSET_Info.Shell_LCL = decimal.Parse(txtShellLCL.Text);
                mSET_Info.Shell_TestTimes = int.Parse(txtShellTestTimes.Text);
                mSET_Info.Shell_SetEN = chkSetEnShell.Checked;

                mSET_Info.ACIR_UCL = decimal.Parse(txtACIRUCL.Text);
                mSET_Info.ACIR_LCL = decimal.Parse(txtACIRLCL.Text);
                mSET_Info.ACIR_TestTimes = int.Parse(txtACIRTestTimes.Text);
                mSET_Info.ACIR_SetEN = chkSetEnACIR.Checked;

                if (mSET_Info.OCV_UCL < mSET_Info.OCV_LCL ||
                    mSET_Info.Shell_UCL < mSET_Info.Shell_LCL ||
                    mSET_Info.ACIR_UCL < mSET_Info.ACIR_LCL)
                {
                    MessageBox.Show("设置值上限小于下限");
                    return;
                }

                if (mSET_Info.OCV_TestTimes < 0 ||
                    mSET_Info.Shell_TestTimes < 0 ||
                    mSET_Info.ACIR_TestTimes < 0)
                {
                    MessageBox.Show("次数不能小于0");
                    return;
                }

                mDBCOM_DevInfo.SaveSetInfo(mSET_Info);
                MessageBox.Show("保存成功");
            }
            catch
            {
                MessageBox.Show("参数输入有误,请更正");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "PCID设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            if (tb_MESpcid.Text != "")
            {
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { tb_MESpcid.Text.Trim() }, "SetType", "PCID", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.RESRCE = texResrce.Text.Trim();
                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "RESOURCE_ID", "resource_id", texResrce.Text.Trim());
                    ClsGlobal.SetParflag = true;
                    ClsLogs.INIlogNet.WriteInfo("系统参数设置", "PCID设置:" + tb_MESpcid.Text.Trim() + "成功");
                    MessageBox.Show("PCID设置成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("系统参数设置", "PCID设置:" + tb_MESpcid.Text + "失败");
                    MessageBox.Show("PCID设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "PCID不能为空");
                MessageBox.Show("PCID不能为空！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string trayCodeRegEx = this.tbTrayCode.Text;
            string cellCodeRegEx = this.tbCellCode.Text;

            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "系统参数设置", "条码规则设置");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { trayCodeRegEx.Trim() }, "SetType", "TrayCodeRegEx", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                ClsGlobal.SetParflag = true;
                ClsLogs.INIlogNet.WriteInfo("系统参数设置", "条码规则设置:" + trayCodeRegEx.Trim() + "成功");
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "条码规则设置:" + trayCodeRegEx + "失败");
                MessageBox.Show("条码规则设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { cellCodeRegEx.Trim() }, "SetType", "CellCodeRegEx", "=");
            count = reader.RecordsAffected;
            if (count > 0)
            {

                ClsGlobal.SetParflag = true;
                ClsLogs.INIlogNet.WriteInfo("系统参数设置", "条码规则设置:" + cellCodeRegEx.Trim() + "成功");
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("系统参数设置", "条码规则设置:" + cellCodeRegEx + "失败");
                MessageBox.Show("条码规则设置异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("条码规则设置成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbCOM_Count_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Loadflag == true)
                //{
                //    return;
                //}

                int Count = int.Parse(cmbCOM_Count.Text);
                int count = Count - dgvBt4560.RowCount;
                if (Count >= dgvBt4560.RowCount)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dgvBt4560.Rows.Add();
                    }

                }
                else
                {
                    for (int i = 0; i < Count; i++)
                    {
                        dgvBt4560.EndEdit();
                        dgvBt4560.Rows.RemoveAt(dgvBt4560.RowCount - 1);
                    }

                }
                for (int i = 0; i < dgvBt4560.RowCount; i++)
                {
                    dgvBt4560.Rows[i].Cells[0].Value = i + 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmb_SwitchCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Loadflag == true)
                //{
                //    return;
                //}

                int Count = int.Parse(cmb_SwitchCount.Text);
                int count = Count - dgvcSwitch.RowCount;
                if (Count >= dgvcSwitch.RowCount)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dgvcSwitch.Rows.Add();
                    }

                }
                else
                {
                    for (int i = 0; i < Count; i++)
                    {
                        dgvcSwitch.EndEdit();
                        dgvcSwitch.Rows.RemoveAt(dgvcSwitch.RowCount - 1);
                    }

                }
                for (int i = 0; i < dgvcSwitch.RowCount; i++)
                {
                    dgvcSwitch.Rows[i].Cells[0].Value = i + 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// OCV获取方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsLocalOCV_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "参数设置", "设置OCV测试工序");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int value = rbLocalOCV.Checked ? 1 : 0;
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { value.ToString() }, "SetType", "IsLocalOCVType", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                // ClsGlobal.OCVType = Value;
                ClsLogs.INIlogNet.WriteInfo("参数设置", "OCV类型获取方式设置成功");
                //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", "OCV" );
                ClsGlobal.SetParflag = true;
                MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("参数设置", "OCV类型获取方式设置失败");
                MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (rbLocalOCV.Checked)
            {
                int Value = 0;
                if (int.TryParse(cmbOCVNum.Text, out Value))
                {
                    ClsGlobal.OCVType = value;
                }
            }

        }


        private void btnConnectTest_local_Click(object sender, EventArgs e)
        {
            string info = "Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}";
            string mConnStr = string.Format(info, txtOCVPWD.Text, txtOCVUID.Text, txtOCVDB.Text, txtOCVServer.Text);
            using (SqlConnection mConn = new SqlConnection(mConnStr))
            {
                try
                {
                    mConn.Open();
                    MessageBox.Show("连接成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("连接失败:" + ex.Message);
                }
            }
        }

        private void btnExpSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckExp() == false)
                    return;
                int enableValue = chbExpEnable.Checked ? 1 : 0;
                string tTime = txtTime.Text;
                string txt1 = "update TestSeting set value='" + txtTime.Text + "' where SetType='Time_t';";
                string txt2 = "update TestSeting set value='" + txtChannelExpNum.Text + "' where SetType='ChannelExp_n';";
                string txt3 = "update TestSeting set value='" + txtTrayExpNum.Text + "' where SetType='TrayExp_m';";
                string txt4 = "update TestSeting set value='" + enableValue + "' where SetType='ExpEnable';";
                string strSql = txt1 + txt2 + txt3 + txt4;
                SQLiteDataReader reader = ClsGlobal.sql.UpdateValues(strSql);
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    MessageBox.Show("设置成功！");
                }
                else
                {
                    MessageBox.Show("设置失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败：" + ex.Message);
            }
        }
        private bool CheckExp()
        {
            int value = 0;
            if (string.IsNullOrEmpty(txtTime.Text))
            {
                MessageBox.Show("请输入时间值！");
                return false;
            }
            else
            {
                if (int.TryParse(txtTime.Text, out value) == false)
                {
                    MessageBox.Show("请输入整数！");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtChannelExpNum.Text))
            {
                MessageBox.Show("请输入通道次数！");
                return false;
            }
            else
            {
                if (int.TryParse(txtChannelExpNum.Text, out value) == false)
                {
                    MessageBox.Show("请输入整数！");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtTrayExpNum.Text))
            {
                MessageBox.Show("请输入托盘次数！");
                return false;
            }
            else
            {
                if (int.TryParse(txtChannelExpNum.Text, out value) == false)
                {
                    MessageBox.Show("请输入整数！");
                    return false;
                }
            }

            return true;
        }

        private void btnPathSave_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "参数设置", "设置OCV测试报告目录");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { txtReportPath.Text }, "SetType", "ReportPath", "=");
            int count = reader.RecordsAffected;
            if (count > 0)
            {
                // ClsGlobal.JT_NO = txt_JT.Text.Trim();
                //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", txtOPEATION_ID.Text.Trim());
                ClsGlobal.SetParflag = true;
                ClsLogs.INIlogNet.WriteInfo("参数设置", "设置ReportPath:" + txtReportPath.Text + "成功");
                MessageBox.Show("设置ReportPath成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ClsLogs.INIlogNet.WriteFatal("参数设置", "设置ReportPath:" + txtReportPath.Text + "失败");
                MessageBox.Show("设置ReportPath异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtPathSelect_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtReportPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (CheckBatteryClass() == false)
            {
                return;
            }
            try
            {
                string txt1 = "update TestSeting set value='" + txtStartIndex.Text + "' where SetType='StartIndex';";
                string txt2 = "update TestSeting set value='" + txtBatTypeLen.Text + "' where SetType='BatTypeLen';";
                string txt3 = "update TestSeting set value='" + txtClassLen.Text + "' where SetType='BatClassLen';";
                string strSql = txt1 + txt2 + txt3;
                SQLiteDataReader reader = ClsGlobal.sql.UpdateValues(strSql);
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    MessageBox.Show("设置成功！");
                }
                else
                {
                    MessageBox.Show("设置失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.Message);
            }
        }

        private bool CheckBatteryClass()
        {
            int value = 0;
            if (string.IsNullOrEmpty(txtStartIndex.Text))
            {
                MessageBox.Show("请输起始位置！");
                return false;
            }
            else
            {
                if (int.TryParse(txtStartIndex.Text, out value) == false)
                {
                    MessageBox.Show("请输入整数！");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtBatTypeLen.Text))
            {
                MessageBox.Show("请型号长度！");
                return false;
            }
            else
            {
                if (int.TryParse(txtBatTypeLen.Text, out value) == false)
                {
                    MessageBox.Show("请输入整数！");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtClassLen.Text))
            {
                MessageBox.Show("请输入区分长度！");
                return false;
            }
            else
            {
                if (int.TryParse(txtClassLen.Text, out value) == false)
                {
                    MessageBox.Show("请输入整数！");
                    return false;
                }
            }

            return true;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            ClsWCSCOM.Instance.StepTrayStaionChange(LocStatusType.Disable,"OCV");
        }

        private void btnFree_Click(object sender, EventArgs e)
        {
            ClsWCSCOM.Instance.StepTrayStaionChange(LocStatusType.Free, "OCV");
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "参数设置", "设置OCV测试工序");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            double Value = 0;
            if (double.TryParse(txtOCV23BCValue.Text, out Value))
            {
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { dtpOCVBCTime.Text }, "Parameter", "OCVBCTime", "=");
                reader = ClsGlobal.sql.UpdateValues("System", new string[] { "Value" }, new string[] { Value.ToString() }, "Parameter", "OCV23BCValue", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    // ClsGlobal.OCVType = Value;
                    ClsLogs.INIlogNet.WriteInfo("参数设置", @"OCV2\3开电压补偿值" + Value + "成功");
                    //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", "OCV" );
                    ClsGlobal.SetParflag = true;
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("参数设置", @"OCV2\3开电压补偿值" + Value + "失败");
                    MessageBox.Show("设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(@"OCV2\3开电压补偿值", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}

