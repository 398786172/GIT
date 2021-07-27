using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DB_KT.DAL;
using System.IO.Ports;
using System.Threading;
using OCV.OCVTest;

namespace OCV
{
    public partial class FrmSys : Form
    {

        //监控界面
        FrmMonitor mFrmMon;
        //OCV测试
        FrmOCV mFrmOCV;

        public FrmSys()
        {
            InitializeComponent();
        }

        private void FrmSys_Load(object sender, EventArgs e)
        {
            //进程已经存在时退出
            if (ClsGlobal.CheckOCVProcessOn())
            {
                this.Close();
            }

            try
            {

                ClsGlobal.ID_InitOK = false;

                //log文件
                ClsGlobal.sLogsOCVpath = Application.StartupPath.ToString() + "\\OCVLogs.txt";      //OCV log
                ClsGlobal.CreateLogsFile(ClsGlobal.sLogsOCVpath);

                string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");                             //Monitor log
                //ClsGlobal.sLogspath = Application.StartupPath.ToString() + "\\log\\Logs_" + TodayDate + ".txt";
                //ClsGlobal.sDebugInfoPath = Application.StartupPath.ToString() + "\\DebugInfo\\Debugs_" + TodayDate + ".txt";
                //ClsGlobal.sLogAlarmpath = Application.StartupPath.ToString() + "\\log\\AlarmLogs_" + TodayDate + ".txt";
                ClsGlobal.CreateLogsFile(ClsGlobal.sLogspath);
                ClsGlobal.CreateLogsFile(ClsGlobal.sLogAlarmpath);
                ClsGlobal.CreateDebugFile(ClsGlobal.sDebugInfoPath);

                //BH20200311
                ClsGlobal.CreateDebugFile(ClsGlobal.sDebugOCVSelectionPath);



                //运行模式
                ClsGlobal.OCV_RunMode = (eRunMode)int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Run_Mode", null));
                if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                {
                    ClsGlobal.MaxTestNum = 2;
                }
                else if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                {
                    ClsGlobal.MaxTestNum = 1;
                }

                //PASSWARD
                ClsGlobal.UserPwd = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "PASSWARD", "UserPwd", null);
                if (ClsGlobal.UserPwd == null)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "PASSWARD", "UserPwd", "");
                    ClsGlobal.UserPwd = "";
                }


                ClsGlobal.AdvUserPwd = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "PASSWARD", "AdvUserPwd", null);
                if (ClsGlobal.AdvUserPwd == null)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "PASSWARD", "AdvUserPwd", "");
                    ClsGlobal.AdvUserPwd = "";
                }

                //设备
                ClsGlobal.DeviceName = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DeviceName", null);
                ClsGlobal.DeviceNo = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DeviceNo", null);

                //OCV类型参数
                #region 改由工程管理配置 20200828 由ajone 屏蔽
                //ClsGlobal.OCVType = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "OCVType", null));
                #endregion

                ClsGlobal.OCVType_Sub1 = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TypeFlag", "0"));
                ClsGlobal.TrayType = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TrayType", null));

                ClsGlobal.BatteryType = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "BatteryType", null);

                #region  改用工程管理方式设定 20200827 由ajone屏蔽
                ////电压上下限
                //ClsGlobal.VolUpLmt = double.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "VolUpLmt", null));
                //ClsGlobal.VolDownLmt = double.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "VolDownLmt", null));

                ////内阻上下限
                //ClsGlobal.ACIRUpLmt = double.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "ACIRUpLmt", null));
                //ClsGlobal.ACIRDownLmt = double.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "ACIRDownLmt", null));
                #endregion

                #region 切换控制与IO控制

                try
                {
                    ClsGlobal.SW_Port = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "SW_Port", null);

                    ClsGlobal.SWCom.PortName = ClsGlobal.SW_Port;
                    ClsGlobal.SWCom.BaudRate = 115200;
                    ClsGlobal.SWCom.Parity = Parity.None;
                    ClsGlobal.SWCom.DataBits = 8;
                    ClsGlobal.SWCom.StopBits = StopBits.One;
                    ////设置超时读取时间 
                    ClsGlobal.SWCom.ReadTimeout = 500;
                    ClsGlobal.SWCom.DtrEnable = true;
                    ClsGlobal.SWCom.Encoding = Encoding.UTF8;

                    ClsGlobal.SWCom.Open();
                    if (ClsGlobal.SWCom.IsOpen)
                    {
                        ClsGlobal.SWCom.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("通道切换系统串口初始化失败: " + ex.Message.ToString() + "  请重新设置.");
                }

                //切换板电池点位对应关系
                ClsGlobal.mSwitchCH = GetSwitchChannel(ClsGlobal.mSwitchPath);

                //IO与通道切换控制
                ClsGlobal.mIOControl = new ClsIOControl(ClsGlobal.SWCom, 1);

                #endregion

                #region 内阻仪

                //内阻仪参数
                ClsGlobal.RT_Port = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "RT_Port", null);

                try
                {

                    ClsGlobal.RTCom = new SerialPort(ClsGlobal.RT_Port, 9600, Parity.None, 8, StopBits.One);
                    ClsGlobal.RTCom.ReadTimeout = 500;      //设置超时读取时间 
                    ClsGlobal.RTCom.DtrEnable = true;
                    //ClsGlobal.RTCom.Encoding = Encoding.UTF8;

                    ClsGlobal.RTCom.Open();
                    if (ClsGlobal.RTCom.IsOpen)
                    {
                        ClsGlobal.RTCom.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("内阻仪串口初始化失败: " + ex.Message.ToString() + "  请重新设置.");
                }

                ClsGlobal.RT_Speed = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "RT_Speed", null));


                #endregion

                #region 万用表

                ClsGlobal.DMT_USBAddr = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DMT_USBAddr", null);
                ClsGlobal.DMT_SerialPort_Com = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Port", null);
                ClsGlobal.DMT_SerialPort_Com_Speed = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Speed", null);
                ClsGlobal.DMT_Connection_Type = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Multimeter_Connection_Type", null);
                #endregion

                //内阻校准
                ClsGlobal.IRTrueValSetFlag = false;
                ClsGlobal.ArrIRAdjustPara = GetIRAdjust(ClsGlobal.mIRAdjustPath);

                //界面显示
                //监控界面
                mFrmMon = new FrmMonitor();

                //OCV测试
                mFrmOCV = new FrmOCV();

                rdoMonitor.Checked = true;

                mFrmOCV.TopLevel = false;
                mFrmOCV.Parent = panel3;
                mFrmOCV.WindowState = FormWindowState.Maximized;
                mFrmOCV.Dock = DockStyle.Fill;
                mFrmOCV.FormBorderStyle = FormBorderStyle.None;
                mFrmOCV.Show();
                mFrmOCV.ShowIcon = false;

                mFrmMon.TopLevel = false;
                mFrmMon.Parent = panel3;
                mFrmMon.WindowState = FormWindowState.Maximized;
                mFrmMon.Dock = DockStyle.Fill;
                mFrmMon.FormBorderStyle = FormBorderStyle.None;
                mFrmMon.Show();
                mFrmMon.ShowIcon = false;

                lblOCV_Num.Text = "OCV-T" + (ClsGlobal.OCVType + ClsGlobal.OCVType_Sub1);
                lblTrayType.Text = ClsGlobal.TrayType.ToString() + "通道";

                if (ClsGlobal.OCVType == 1)
                {
                    lblTestType.Text = "内阻/电压";
                    lblTestType.BackColor = Color.Moccasin;
                }
                else if (ClsGlobal.OCVType == 2)
                {
                    lblTestType.Text = "电压";
                    lblTestType.BackColor = Color.PeachPuff;
                }
                panel2.Left = this.Size.Width - panel2.Width - 10;

                switch (ClsGlobal.OCV_RunMode)
                {
                    case eRunMode.NormalTest:
                        this.lbRunMode.Text = "联网测试";
                        this.lbRunMode.BackColor = Color.LightGreen;
                        break;
                    case eRunMode.OfflineTest:
                        this.lbRunMode.Text = "单机测试";
                        this.lbRunMode.BackColor = Color.Lavender;
                        break;
                    case eRunMode.ACIRAdjust:
                        this.lbRunMode.Text = "校准模式";
                        this.lbRunMode.BackColor = Color.Orange;
                        break;
                    default:
                        break;
                }

                //BH20200613:界面显示电池型号，方便用户确认换型是否成功。
                //Thread Thread_BatteryTypeCkecking = new Thread(BatteryTypeCkecking);
                //Thread_BatteryTypeCkecking.Start();

                ClsGlobal.ID_InitOK = true;
                var temp = ClsGlobal.BuildClsTempControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ClsGlobal.ID_InitOK = false;
            }
        }

        /// <summary>
        /// 设备目前适用的电池型号检测显示线程BH20200613
        /// </summary>
        private void BatteryTypeCkecking()
        {
            while (true)
            {
                ushort Val = ClsGlobal.mIOControl.Get_M_PressBattType();

                if (Val == 1)
                {
                    ClsGlobal.OCV_BatteryType = "18650";
                    this.lblBatteryType.Text = ClsGlobal.OCV_BatteryType;
                }
                else if (Val == 2)
                {
                    ClsGlobal.OCV_BatteryType = "21700";
                    this.lblBatteryType.Text = ClsGlobal.OCV_BatteryType;
                }
                else
                {
                    ClsGlobal.OCV_BatteryType = "N/A";
                    this.lblBatteryType.Text = ClsGlobal.OCV_BatteryType;
                }

                //switch (ClsGlobal.OCV_BatteryType)
                //{
                //    case "18650":
                //        this.lblBatteryType.Text = ClsGlobal.OCV_BatteryType;
                //        break;
                //    case "21700":
                //        this.lblBatteryType.Text = ClsGlobal.OCV_BatteryType;
                //        break;
                //    default:
                //        this.lblBatteryType.Text = ClsGlobal.OCV_BatteryType;
                //        break;
                //}
                Thread.Sleep(2000);
            }
        }

        //获得内阻校准值
        public static string[] GetIRAdjust(string Path)
        {
            int CountNum;
            string[] ArrAdjust;
            string count = INIAPI.INIGetStringValue(Path, "ACIRAdjust", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                ArrAdjust = new string[CountNum];

                for (int i = 0; i < ArrAdjust.Count(); i++)
                {
                    ArrAdjust[i] = INIAPI.INIGetStringValue(Path, "ACIRAdjust", "CH" + (i + 1).ToString(), null);
                }
                return ArrAdjust;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return null;
            }
        }


        //获得电池通道切换映射表
        private string[] GetSwitchChannel(string SwitchPath)
        {
            int CountNum;
            string[] SWChannel;
            string count = INIAPI.INIGetStringValue(SwitchPath, "switch", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                SWChannel = new string[CountNum];

                for (int i = 0; i < SWChannel.Count(); i++)
                {
                    SWChannel[i] = INIAPI.INIGetStringValue(SwitchPath, "switch", "CH" + (i + 1).ToString(), null);
                }
                return SWChannel;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", SwitchPath);
                return null;
            }
        }


        private void 运行模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.UserInfo.UserType != UserType.Admin && ClsGlobal.UserInfo.UserType != UserType.Engineer)
            {
                MessageBox.Show("无权限进行操作,请使用管理员账号操作!");
                return;
            }
            FrmRunMode FrmSS = new FrmRunMode();
            FrmSS.Owner = this;
            FrmSS.ShowDialog();

        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.UserInfo.UserType != UserType.Admin)
            {
                MessageBox.Show("无权限进行操作,请使用管理员账号操作!");
                return;
            }

            FormSysSetting FrmSS = new FormSysSetting();
            FrmSS.Owner = this;
            FrmSS.ShowDialog();
        }


        private void 手动上传数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂时不提供此功能");
            return;

            frmUserPwd fuserpwd = new frmUserPwd(1);

            if (fuserpwd.ShowDialog() == DialogResult.OK)
            {
                FrmManualUpload F_ManualUpload = new FrmManualUpload();
                F_ManualUpload.Show();
            }
        }

        private void 校准设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.UserInfo.UserType != UserType.Admin && ClsGlobal.UserInfo.UserType != UserType.Engineer)
            {
                MessageBox.Show("无权限进行操作,请使用管理员账号操作!");
                return;
            }
            FrmOCVMasterSetting Frm = new FrmOCVMasterSetting();
            Frm.Show();
        }

        private void FrmSys_Resize(object sender, EventArgs e)
        {
            panel2.Left = this.Size.Width - panel2.Width - 10;
        }

        private void rdoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMonitor.Checked == true)
            {
                //FM.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                if (mFrmMon != null)
                {
                    mFrmMon.WindowState = FormWindowState.Maximized;
                    mFrmMon.Dock = DockStyle.Fill;
                    mFrmMon.BringToFront();
                }
            }
        }

        private void rdoOCVTest_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOCVTest.Checked == true)
            {
                if (mFrmMon != null)
                {
                    //FOCV.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    mFrmOCV.WindowState = FormWindowState.Maximized;
                    mFrmOCV.Dock = DockStyle.Fill;
                    mFrmOCV.BringToFront();
                }
            }
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0 //隐藏子窗体图标
                || e.Item.Text == "最小化(&N)"//隐藏最小化按钮
                || e.Item.Text == "还原(&R)"//隐藏还原按钮
                || e.Item.Text == "关闭(&C)")//隐藏最关闭按钮
            {
                e.Item.Visible = false;
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout FA = new FrmAbout();
            FA.StartPosition = FormStartPosition.CenterScreen;
            FA.ShowDialog();
        }

        private void FrmSys_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //Application.Exit();
                Environment.Exit(0);            //强制关闭
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmSys_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClsGlobal.bCloseFrm == false && mFrmMon != null && mFrmOCV != null)
            {
                if (MessageBox.Show("确认关闭程序?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void 校准值设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.UserInfo.UserType != UserType.Admin && ClsGlobal.UserInfo.UserType != UserType.Engineer)
            {
                MessageBox.Show("无权限进行操作,请使用管理员账号操作!");
                return;
            }
            FrmAdjSetting Frm = new FrmAdjSetting();
            Frm.Show();
        }







    }
}
