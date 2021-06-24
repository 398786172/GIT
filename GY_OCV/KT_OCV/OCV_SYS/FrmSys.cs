using System;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClsDeviceComm.BasicFramework;
using CCWin;
using ClsDeviceComm.LogNet;
using OCV.OCVLogs;
using DB_OCV.DAL;
using OCV.INI;
using System.Collections.Generic;
using OCV.OCVTest;
using System.IO;
using System.Reflection;
using OCV.OCV_SYS;
using OCV.Models;

namespace OCV
{
    public partial class FrmSys : CCSkinMain  //Form
    {

        public FrmSys()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            AutoClearLogs.Instance.StartClear();
            //string path = Application.StartupPath + "\\KT_OCV";
            DateTime time = File.GetLastWriteTime(this.GetType().Assembly.Location);
            string text = "OCV测试系统(Ver{0}) © Kinte(广州擎天实业有限公司)";
            string info = string.Format(text, time.ToString("yyyyMMddHHMMss"));
            this.Text = info;
            rbOCV2.Checked = true;
            int width = SystemInformation.WorkingArea.Width;
            int height = SystemInformation.WorkingArea.Height;
            this.Width = width + 10;
            this.Height = height + 10;
        }
        //ClsProcess mProcA;

        delegate void RefreshText(string Info);
        RefreshText RefreshTxtAlarmA;        //刷新报警信息     
        RefreshText RefreshTxtMESinfo;        //刷新mes信息
        private ShowErrPopupMsg MyShowErrPopupMsg;
        private delegate void ShowErrPopupMsg(string msg, Color color); //委托，显示提示信息
        ClsIniParameter IniParameter = null;
        SerialPort SPort = null;
        private ClsProcess mProc;
        private ClsSWControl[] SWControl;          //切换控制
        private ClsDMM_Ag344X DMM_Ag344X;      //万用表
        private ClsHIOKI365X[] HIOKI365X;       //内阻仪BT356x控制 
        private ClsHIOKI4560[] HIOKI4560;       //内阻仪BT4560控制
        private int next = 0;
        private Dictionary<int, string> dicDesc1;
        private Dictionary<int, string> dicDesc2;
        //private ClsOCVContr OCVContr;
        //private ClsOCVTestData OCVTestData;
        private void FrmSys_Load(object sender, EventArgs e)
        {
            try
            {
                dicDesc1 = GetErrorDesc1();
                dicDesc2 = GetErrorDesc2();
                //debug
                //ClsGlobal.mDBCOM_OCV_QT.test_ETCell_Offline("123", out ClsGlobal.listETCELL);
                //ClsGlobal.mDBCOM_OCV_QT.InsertOCVACIRData(1, ClsGlobal.listETCELL, 1);
                ClsGlobal.SetParflag = false;
                ClsGlobal.sysOK = false;


                //进程已经存在时退出
                if (ClsGlobal.CheckProcessOn("KT_OCV"))
                {
                    MessageBox.Show("OCV程序已经运行,请勿重复启动");
                    this.Close();
                }




                #region 初始化日志
                ClsLogs.LogNetINI();
                #endregion

                #region 加载参数
                IniParameter = new ClsIniParameter();
                IniParameter.IniParameter();
                #endregion

                #region 初始化日志
                ClsLogs.LogNet();
                #endregion

                this.sscMain.SplitterWidth = 10;
                ClsGlobal.USER_NO = "";     //作业员
                ClsGlobal.ProjectSetType = 1;
                ClsGlobal.UserAuthority = 3;

                //擎天数据库的OCV数据连接
                ClsGlobal.mDBCOM_OCV_QT = new DBCOM_OCV(ClsGlobal.Server_OCV_IP, ClsGlobal.Server_OCV_DB, ClsGlobal.Server_OCV_id, ClsGlobal.Server_OCV_Pwd);

                // ClsGlobal.mDBCOM_OCV_BYD = new DBCOM_OCV(ClsGlobal.Server_QT_IP, ClsGlobal.Server_QT_DB, ClsGlobal.Server_QT_id, ClsGlobal.Server_QT_Pwd);
                //物流系统接口
                ClsGlobal.WCSCOM = ClsWCSCOM.Instance;

                #region PLC交互流程控制
                ClsGlobal.mPLCContr = new ClsPLCContr(ClsGlobal.PLCPort, ClsGlobal.PLCAddr);
                #endregion

                #region 界面初始化  
                this.WindowState = FormWindowState.Maximized;
                if (ClsGlobal.ProjectSetType == 1)
                {
                    if (ClsGlobal.UserAuthority < 2)
                    {
                        数据补传ToolStripMenuItem.Visible = false;
                        工程设置ToolStripMenuItem1.Visible = true;
                    }
                    if (ClsGlobal.UserAuthority > 1)
                    {
                        数据补传ToolStripMenuItem.Visible = false;
                        工程设置ToolStripMenuItem1.Visible = true;
                    }
                }
                else
                {
                    工程设置ToolStripMenuItem1.Visible = false;
                    数据补传ToolStripMenuItem.Visible = true;
                }
                //txt_FlowTimeA.Text = "";
                txtTraycodeA.Text = "";

                RefreshTxtAlarmA = new RefreshText(mRefreshTextAlarmA);

                MyShowErrPopupMsg += ShowPopupMessage;
                tssOperatorId.Text = "OCV" + ClsGlobal.OCVType;
                tssTestCH.Text = ClsGlobal.TrayType.ToString() + "通道";

                //ClsGlobal.DeviceCode = ClsGlobal.SITE + "-" + ClsGlobal.DeviceNo + "-" + "OCV" + ClsGlobal.OCVType + "-" + ClsGlobal.JT_NO;
                ClsGlobal.DeviceCode = "OCV" + ClsGlobal.OCVTypeShow + "-" + ClsGlobal.JT_NO;
                tssMachineId.Text = ClsGlobal.DeviceCode;

                if (ClsGlobal.TestType == 0)
                {
                    tssTestType.Text = "电压";
                }
                else if (ClsGlobal.TestType == 1)
                {
                    tssTestType.Text = "电压+负极壳压";
                }
                else if (ClsGlobal.TestType == 2)
                {
                    tssTestType.Text = "电压+负极壳压+ACIR";
                }
                else if (ClsGlobal.TestType == 3)
                {
                    tssTestType.Text = "电压+负极壳压+正极壳压+ACIR";
                }


                int showTrayType = ClsGlobal.TrayType / 1;
                dgvTest.Rows.Add(showTrayType);
                for (int i = 0; i < showTrayType; i++)
                {
                    dgvTest.Rows[i].Cells["Col_Num"].Value = (i + 1).ToString();
                    //dgvTest.Rows[i].Cells["Col_Num1"].Value = (i + 1+ showTrayType).ToString();
                }
                //显示运行模式 20180110 li
                switch (ClsGlobal.OCV_RunMode)
                {
                    case eRunMode.NormalTest:
                        this.tssRunMode.Text = "联网测试";
                        break;
                    case eRunMode.GoAhead:
                        this.tssRunMode.ForeColor = Color.Red;
                        this.tssRunMode.Text = "排出模式";
                        break;
                    case eRunMode.OfflineTest:
                        this.tssRunMode.Text = "单机测试";
                        this.tssRunMode.ForeColor = Color.Red;
                        break;
                    case eRunMode.ACIRAdjust:
                        this.tssRunMode.Text = "校准模式";
                        this.tssRunMode.ForeColor = Color.Red;
                        break;
                    default:
                        break;
                }
                #endregion
                #region 内阻仪参数
                if (ClsGlobal.TestType == 2|| ClsGlobal.TestType == 3)
                {
                    try
                    {

                        //获取内阻校准参数
                        ClsGlobal.mIRAdjustVal = ClsGlobal.GetAdjustVal_ACIR(ClsGlobal.mIRAdjustPath);
                        this.HIOKI4560 = new ClsHIOKI4560[ClsGlobal.BTcount];
                        for (int i = 0; i < ClsGlobal.BTcount; i++)
                        {
                            this.HIOKI4560[i] = new ClsHIOKI4560(ClsGlobal.RT4560_Port[i], ClsGlobal.TestFreq[i].Remove(ClsGlobal.TestFreq[i].Length - 2), ClsGlobal.BT4560RANG[i], 3, 2, ClsGlobal.InitBT4560[i]);
                            this.HIOKI4560[i].InitControl_IMM();
                        }
                    }
                    catch (Exception ex)
                    {
                        InfoHandleA(ex.Message);
                        throw ex;
                    }
                }

                #endregion

                #region 万用表参数

                try
                {
                    //万用表初始化
                    if (ClsGlobal.DMTComMode == 1)
                    {
                        this.DMM_Ag344X = new ClsDMM_Ag344X(ClsGlobal.DMT_Port, ClsGlobal.DMTComMode);
                        this.DMM_Ag344X.InitControl_IMM();
                    }
                    else
                    {
                        this.DMM_Ag344X = new ClsDMM_Ag344X(ClsGlobal.DMT_USBAddr, ClsGlobal.DMTComMode);
                        this.DMM_Ag344X.InitControl_IMM();
                    }
                }
                catch (Exception ex)
                {
                    InfoHandleA(ex.Message);
                    //throw ex;
                }


                #endregion

              

                #region 切换控制箱
                try
                {
                    this.SWControl = new ClsSWControl[ClsGlobal.Switch_Count];
                    try
                    {
                        //切换控制初始化
                        try
                        {
                            SPort = new SerialPort(ClsGlobal.Switch_Port[0], 115200, Parity.None, 8, StopBits.One);
                            //SwitchCom.ReadTimeout = 500;    //设置超时读取时间 
                            //SwitchCom.DtrEnable = true;
                            SPort.Close();
                            if (SPort.IsOpen == false)
                            {
                                SPort.Open();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("万用表串口初始化失败");

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("打开通道切换系统串口失败", ex);
                    }
                    for (int i = 0; i < ClsGlobal.Switch_Count; i++)
                    {
                        if (ClsGlobal.SwitchVersion[i] == 1)
                        {
                            this.SWControl[i] = new ClsSWControl(SPort, 1, 1);
                        }
                        else
                        {
                            this.SWControl[i] = new ClsSWControl(SPort, (byte)(i + 1), 2);
                        }
                        //切换板电池点位对应关系
                        //ClsGlobal.mSwitchCH = ClsGlobal.GetSwitchChannel(ClsGlobal.mSwitchPath);
                    }
                }
                catch (Exception ex)
                {
                    InfoHandleA(ex.Message);
                }
                #endregion
                if (ClsGlobal.TestType == 2|| ClsGlobal.TestType == 3)
                {
                    ClsGlobal.OCVTestContr = new ClsOCVContr(this.SWControl, this.DMM_Ag344X, this.HIOKI4560, this);
                    ClsGlobal.OCVTestContr.mInfoSend = new InfoSend(InfoHandleA);
                }
                else
                {
                    ClsGlobal.OCVTestContr = new ClsOCVContr(this.SWControl, this.DMM_Ag344X, this);
                    ClsGlobal.OCVTestContr.mInfoSend = new InfoSend(InfoHandleA);
                }

                #region 温度测试
                ClsGlobal.mTempAdjustVal_P = ClsGlobal.GetAdjustVal_Temp(ClsGlobal.mTempAdjustPath, "P");
                ClsGlobal.mTempAdjustVal_N = ClsGlobal.GetAdjustVal_Temp(ClsGlobal.mTempAdjustPath, "N");

                ClsGlobal.mSwitchCHTemp_P = ClsGlobal.GetSwitchChannel_Temp(ClsGlobal.mSwitchTempPath, "P");
                ClsGlobal.mSwitchCHTemp_N = ClsGlobal.GetSwitchChannel_Temp(ClsGlobal.mSwitchTempPath, "N");

                try
                {
                    ClsGlobal.TempCom = new SerialPort(ClsGlobal.Temp_Port, 115200, Parity.None, 8, StopBits.One);
                    ClsGlobal.TempCom.ReadTimeout = 500;           //设置超时读取时间 
                    if (ClsGlobal.TempCom.IsOpen)
                    {
                        ClsGlobal.TempCom.Close();
                    }
                }
                catch (Exception ex)
                {
                    InfoHandleA(ex.Message);
                    MessageBox.Show("温度串口初始化失败");

                }
                try
                {
                    ClsGlobal.TempContr = new ClsTempContrT2(ClsGlobal.TempCom, 1);
                }
                catch (Exception ex)
                {
                    InfoHandleA(ex.Message);
                }
                #endregion

                #region 扫码枪
                try
                {
                    ClsGlobal.CodeScan.SetPortProperty(ClsGlobal.Scan_Port, 9600, Parity.None, 8, StopBits.One);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("扫码枪初始化失败");
                }

                #endregion

                #region 流程控制
                //处理流程
                mProc = new ClsProcess("A", InfoHandleA, GetScanCode, this);
                mProc.Stop();
                #endregion

                #region 数据库
                //擎天数据库的OCV数据连接
                // ClsGlobal.mDBCOM_OCV_QT = new DBCOM_OCV(ClsGlobal.Server_OCV_IP, ClsGlobal.Server_OCV_DB, ClsGlobal.Server_OCV_id, ClsGlobal.Server_OCV_Pwd);
                //   ClsGlobal.mDBCOM_ProjectInfo = new ProjectInfo(ClsGlobal.Server_OCV_IP, ClsGlobal.Server_OCV_DB, ClsGlobal.Server_OCV_id, ClsGlobal.Server_OCV_Pwd);
                //本地OCV数据连接
                //ClsGlobal.mDBCOM_OCV_Local = new DBCOM_OCV(ClsGlobal.Server_Local_IP, ClsGlobal.Server_Local_DB);
                //现场装SQL SERVER时,权限不足已用混合密码
                //mDBCOM_OCV_Local = new DBCOM_OCV(ClsGlobal.Server_Local_IP, ClsGlobal.Server_Local_DB, ClsGlobal.Server_Local_id, ClsGlobal.Server_Local_Pwd);   

                //擎天数据库接口 (化成)
                //ClsGlobal.mDBCOM_SVSQT = new DBCOM_SVS_QT(ClsGlobal.Server_QT_IP, ClsGlobal.Server_QT_DB, ClsGlobal.Server_QT_id, ClsGlobal.Server_QT_Pwd);


                #endregion

                #region 测试流程控制
                InfoHandleA("程序启动");

                #endregion

                tim_UI.Start();
                Btn_A_RUN.Enabled = true;

                if (ClsGlobal.InitOK == false)
                {
                    InfoHandleA("设备参数异常！");
                    return;
                }
                ClsGlobal.sysOK = true;

            }
            catch (Exception ex)
            {
                string mes = "";
                if (ex.Message.ToString().Contains("ReturnCode"))
                {
                    mes = "初始化异常PLC连接失败：" + ex.Message.ToString();
                }
                else
                {
                    mes = "初始化异常：" + ex.Message;
                }
                mRefreshTextAlarmA(mes);
                ClsGlobal.InitOK = false;
                ClsGlobal.sysOK = false;
                Btn_A_RUN.Enabled = false;
            }
            dgvTest.Columns["Col_TEMP_P"].Visible = false;
        }


        private void tim_UI_Tick(object sender, EventArgs e)
        {
            if (ClsGlobal.SetParflag == true)
            {
                Btn_A_RUN.Enabled = false;
                Btn_A_stop.Enabled = false;
                this.Invoke(MyShowErrPopupMsg, "有参数需要更新，请重启软件", Color.Red);
                InfoHandleA("有参数需要更新，请重启软件！");
                tim_UI.Stop();
                return;
            }


            tssOperatorId.Text = "OCV" + ClsGlobal.OCVType;

            if (ClsGlobal.TestType == 0)
            {
                tssTestType.Text = "电压";
            }
            else if (ClsGlobal.TestType == 1)
            {
                tssTestType.Text = "电压+负极壳压";
            }
            else if (ClsGlobal.TestType == 2)
            {
                tssTestType.Text = "电压+负极壳压+ACIR";
            }
            else if (ClsGlobal.TestType == 3)
            {
                tssTestType.Text = "电压+负极壳压+正极壳压+ACIR";
            }

            //ClsGlobal.DeviceCode = ClsGlobal.SITE + "-" + ClsGlobal.DeviceNo + "-" + "OCV" + ClsGlobal.OCVType + "-" + ClsGlobal.JT_NO;
            //labDevice.Text = ClsGlobal.DeviceCode;
            lblFrCVRequest.LanternBackground = ClsPLCValue.PlcValue.Plc_IO_FrCVRequest == 1 ? Color.Green : Color.Gray;
            lblFrOCVAllow.LanternBackground = ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow == 1 ? Color.Green : Color.Gray;
            lblBhOCVReq.LanternBackground = ClsPLCValue.PlcValue.Plc_IO_BhOCVReq == 1 ? Color.Green : Color.Gray;
            lblBhCVAllow.LanternBackground = ClsPLCValue.PlcValue.Plc_IO_BhCVAllow == 1 ? Color.Green : Color.Gray;
            lblHaveGoods.LanternBackground = ClsPLCValue.PlcValue.Plc_HaveTray == 1 ? Color.Green : Color.Gray;
            if (ClsPLCValue.PlcValue.Plc_AutoManual == -1)
            {
                lblAuto.LanternBackground = Color.Gray;
                lblManual.LanternBackground = Color.Gray;
            }
            else if (ClsPLCValue.PlcValue.Plc_AutoManual == 1)
            {
                lblAuto.LanternBackground = Color.Green;
                if (next > 8)
                {
                    next = 0;
                    btnRunMode.SwitchStatus = true;
                    gbManual.Visible = false;
                }
                next++;
                lblManual.LanternBackground = Color.Gray;
            }
            else if (ClsPLCValue.PlcValue.Plc_AutoManual == 2)
            {
                lblAuto.LanternBackground = Color.Gray;
                lblManual.LanternBackground = Color.Green;
                if (next > 8)
                {
                    next = 0;
                    btnRunMode.SwitchStatus = false;
                }
                next++;
            }

            txtPlc_AutoStepNO.Text = ClsPLCValue.PlcValue.Plc_AutoStepNO.ToString();
            txtPlc_ResetStepNO.Text = ClsPLCValue.PlcValue.Plc_ResetStepNO.ToString();
            txtPeriod.Text = ClsPLCValue.PlcValue.Plc_Period.ToString();
            if (ClsGlobal.InitOK != true || ClsGlobal.sysOK != true)
            {
                return;
            }
            if (mProc.RunState == 0)         //停止状态
            {
                Btn_A_RUN.Text = "启动";
                Btn_A_RUN.Enabled = true;
                Btn_A_stop.Enabled = false;
                Btn_A_RUN.BaseColor = Color.DodgerBlue;
                Btn_A_stop.BaseColor = Color.DodgerBlue;
            }
            else if (mProc.RunState == 1)   //运行状态
            {
                Btn_A_RUN.BaseColor = Color.Green;
                Btn_A_stop.BaseColor = Color.DodgerBlue;
                Btn_A_RUN.Enabled = false;
                Btn_A_stop.Enabled = true;
                Btn_A_RUN.Text = "运行中...";

            }
            else if (mProc.RunState == 2)    //暂停状态
            {
                Btn_A_RUN.BaseColor = Color.Yellow;
                Btn_A_stop.BaseColor = Color.DodgerBlue;
                Btn_A_RUN.Enabled = true;
                Btn_A_stop.Enabled = true;

                Btn_A_RUN.Text = "继续";

            }
            else if (mProc.RunState == 3)    //报警状态
            {

                Btn_A_RUN.BaseColor = Color.DodgerBlue;
                Btn_A_stop.BaseColor = Color.Red;
                Btn_A_RUN.Enabled = false;
                Btn_A_stop.Enabled = true;
            }
            //   ClsPLCValue.
            SetPLCError1(ClsPLCValue.PlcValue.Plc_Error, ClsPLCValue.PlcValue.Plc_Error2);
            //txt_FlowTimeA.Text = ClsGlobal.FlowTimeA;     
            txtTraycodeA.Text = ClsGlobal.TraycodeA;
            if (txtInfoA.Lines.Count() > 100)
            {
                txtInfoA.Clear();
            }


        }
        //信息记录处理 A单元
        private void InfoHandleA(string info)
        {
            if (this.IsHandleCreated == true)
            {
                this.BeginInvoke(RefreshTxtAlarmA, info);
                if ((!info.Contains("异常")) && (!info.Contains("报警")) && (!info.Contains("超时")) && (!info.Contains("错误")))
                {

                    ClsLogs.OCVInfologNet.WriteInfo("A", info);
                }
                if (info.Contains("异常") || info.Contains("报警") || info.Contains("超时") || info.Contains("错误"))
                {
                    ClsLogs.OCVInfologNet.WriteWarn("A", info);

                }
            }
        }
        //刷新报警界面
        private void mRefreshTextAlarmA(string info)
        {
            txtInfoA.Text += System.DateTime.Now.ToString("HH:mm:ss") + ":  " + info + "\r\n";
            txtInfoA.Select(txtInfoA.TextLength, 0);
            txtInfoA.ScrollToCaret();
        }

        private void ShowPopupMessage(string Message, Color mColor)
        {
            FormPopup popup = new FormPopup(Message, mColor);
            popup.Show();


        }
        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true)
            {
                MessageBox.Show("测试状态无法设置系统参数");
                return;
            }
            frmUserPwd fuserpwd = new frmUserPwd(PwdType.USER, "系统设置");
            //PubClass.sWinTextInfo = "用户密码确认";
            if (fuserpwd.ShowDialog() == DialogResult.OK)
            {
                FormSysSet FrmSS = new FormSysSet();
                FrmSS.Owner = this;
                FrmSS.ShowDialog();
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
        //手工输入条码
        private bool GetScanCode(out string Code)
        {
            FrmCode FC = new FrmCode();
            FC.Location = new Point(50, 50);
            FC.ShowDialog();
            if (FC.OK == true)
            {
                Code = FC.TrayCode;
                return true;
            }
            else
            {
                Code = null;
                return false;
            }
        }

        private void FrmSys_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Environment.Exit(0);            //强制关闭
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmSys_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClsGlobal.sysOK == false)
            {
                return;
            }

            if (MessageBox.Show("确认关闭程序?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {


            }
            else
            {
                e.Cancel = true;
            }

        }

        private void 数据补传ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true)
            {
                MessageBox.Show("测试状态无法补传数据！");
                return;
            }
            frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "数据补传");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FrmManualUpload frmUpload = new FrmManualUpload();
            frmUpload.ShowDialog();
        }

        private void 关于ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmAbout FA = new FrmAbout();
            FA.StartPosition = FormStartPosition.CenterScreen;
            FA.ShowDialog();
        }

        private void 工程设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //if (ClsGlobal.IsAWorking == true)
            //{
            //    MessageBox.Show("测试状态无法修改工艺参数！");
            //    return;
            //}
            FormPorcessSet FE = new FormPorcessSet();
            FE.StartPosition = FormStartPosition.CenterScreen;
            FE.ShowDialog();
        }

        private void 型号设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true)
            {
                MessageBox.Show("测试状态无法设电池型号参数");
                return;
            }
            //FrmBatSetting FB = new FrmBatSetting();
            //FB.StartPosition = FormStartPosition.CenterScreen;
            //FB.ShowDialog();
        }

        private void Btn_A_RUN_Click(object sender, EventArgs e)
        {
            Btn_A_RUN.Enabled = false;
            //判断
            if (ClsGlobal.InitOK == false)
            {
                MessageBox.Show("初始化失败，无法启动");
                return;
            }

            if (ClsPLCValue.PlcValue.Plc_InitReply == 0)
            {
                MessageBox.Show("设备未初始化完成，无法启动");
                return;
            }

            if (Btn_A_RUN.Text == "启动")
            {
                //启动线程
                ClsGlobal.IsAutoMode = true;
                mProc.StartAction();
                Btn_A_RUN.Enabled = false;
                Btn_A_stop.Enabled = true;
                ClsGlobal.IsAWorking = true;

                //初始化
                ClsGlobal.ArrAdjustACIR = new double[160];

            }
            else
            {
                mProc.Resume();
            }

        }

        private void Btn_A_stop_Click(object sender, EventArgs e)
        {
            mProc.Stop();
            ClsGlobal.IsAWorking = false;

            //  Btn_A_RUN.BackColor = Color.Red;
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {

            mProc.ResetPlc_Alarm();
            ClsGlobal.mPLCContr.WriteDB("W11", (ushort)0);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            mProc.Reset();
        }

        private void 手动测试ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true)
            {
                MessageBox.Show("测试状态无法进行手动测试！");
                return;
            }
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "手动测试");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FrmManualTest ManualTest = new FrmManualTest();
            ManualTest.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ForNGexplain FN = new ForNGexplain();
            FN.StartPosition = FormStartPosition.CenterScreen;
            FN.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ClsDeviceComm.LogNet.FormLogNetView LogNet = new ClsDeviceComm.LogNet.FormLogNetView();
            LogNet.StartPosition = FormStartPosition.CenterScreen;
            LogNet.ShowDialog();
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD, "权限管理");

                if (frmPwd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //System.Diagnostics.Process.Start(".\\UserSetting\\UserSetting.exe");
                //StringBuilder StrB = new StringBuilder();
                //StrB.Append("Data Source=" + ClsGlobal.Server_OCV_IP);
                //StrB.Append(" ;Initial Catalog=" + ClsGlobal.Server_OCV_DB);
                //StrB.Append(" ;User ID=" + ClsGlobal.Server_OCV_id);
                //StrB.Append(" ;Password=" + ClsGlobal.Server_OCV_Pwd);
                //StrB.Append(" ;Charset=" + "utf8");
                //StrB.Append(" ;Pooling=" + "true");
                string info = "Password={3};Persist Security Info=True;User ID={2};Initial Catalog={1};Data Source={0}";
                string strConnect = string.Format(info, ClsGlobal.Server_OCV_IP, ClsGlobal.Server_OCV_DB, ClsGlobal.Server_OCV_id, ClsGlobal.Server_OCV_Pwd);
                UserSetting.UserSetting frm = new UserSetting.UserSetting(strConnect);
                frm.ShowDialog();
              //  System.Diagnostics.Process.Start("UserSetting.exe", StrB.ToString());

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message);
            }
        }

        private void 手动校准toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true)
            {
                MessageBox.Show("测试状态无法进行手动测试！");
                return;
            }
            frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "手动测试");

            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FrmManualAdjust ManualAdjust = new FrmManualAdjust();
            ManualAdjust.ShowDialog();
        }

        private void 通道异常统计toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "查看通道异常");

            //if (frmPwd.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            FrmChannelInfo ChannelInfo = new FrmChannelInfo();
            ChannelInfo.ShowDialog();
        }

        private void IOToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormIO frm = new FormIO();
            frm.ShowDialog();
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            mProc.InitPlc();
        }

        private void txtPlc_ResetStepNO_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRunMode_Click(object sender, EventArgs e)
        {
            next = 0;
            if (btnRunMode.SwitchStatus == true)
            {
                mProc.Plc_AutoManual(1);
                ClsGlobal.IsAutoMode = true;
                gbManual.Visible = false;

            }
            else
            {
                mProc.Plc_AutoManual(2);
               // gbManual.Visible = true;
            }
        }

        private void btnRunMode_OnSwitchChanged(object arg1, bool arg2)
        {

        }
        public void SetValueToUI(int rowIndex, int colIndex, double value, int num)
        {
            Action act = delegate
            {

                dgvTest.Rows[rowIndex].Cells[colIndex].Value = Math.Round(value, num).ToString();
            };
            if (this.InvokeRequired)
            {
                Invoke(act);
            }
            else
            {
                act();
            }
        }

        object objlock = new object();
        public void SetValueToUI(int rowIndex, string name, double value, int num)
        {
            Action act = delegate
            {
                lock(objlock)
                {
                    dgvTest.Rows[rowIndex].Cells[name].Value = Math.Round(value, num).ToString();
                }
            };
            if (this.InvokeRequired)
            {
                Invoke(act);
            }
            else
            {
                act();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void SetPLCError1(short value1, short value2)
        {
            listBox1.Items.Clear();
            string msg = Convert.ToString(value1, 2);
            string realInfo = msg.PadLeft(16, '0');
            char[] arrAlarm = realInfo.ToCharArray();
            List<char> list1 = arrAlarm.ToList();
            list1.Reverse();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].ToString() == "1")
                {
                    if (dicDesc1.Keys.Contains(i))
                    {
                        string info = dicDesc1[i];
                        listBox1.Items.Add(info);
                    }
                }
            }

            string msg2 = Convert.ToString(value2, 2);

            string realInfo2 = msg2.PadLeft(16, '0');
            char[] arrAlarm2 = realInfo2.ToCharArray();
            List<char> list2 = arrAlarm2.ToList();
            list2.Reverse();
            for (int i = 0; i < list2.Count; i++)
            {
                if (list2[i].ToString() == "1")
                {
                    if (dicDesc2.Keys.Contains(i))
                    {
                        string info = dicDesc2[i];
                        listBox1.Items.Add(info);
                    }
                }
            }

        }




        private Dictionary<int, string> GetErrorDesc1()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "急停按钮");
            dic.Add(2, "气缸警报");
            dic.Add(7, "气源压力不足");
            dic.Add(8, "门感应");
            dic.Add(9, "流道有电池请手动排出");
            dic.Add(10, "顶升时，定位销未到位");
            dic.Add(11, "顶升气缸上异常");
            dic.Add(12, "顶升气缸下异常");
            dic.Add(13, "探针压缩气缸伸异常");
            dic.Add(14, "探针压缩气缸缩异常");
            return dic;

        }

        private Dictionary<int, string> GetErrorDesc2()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, "探针缩未到位不允许下降");
            dic.Add(1, "进料超时");
            dic.Add(2, "出料超时");

            return dic;

        }

        private void dgvTest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rbOCV2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOCV2.Checked)
            {
                chbVol.Checked = true;
                chbACIR.Checked = false;
                chbNShellVol.Checked = false;
                chbPShellVol.Checked = false;

                chbVol.Enabled = false;
                chbACIR.Enabled = false;
                chbNShellVol.Enabled = false;
                chbPShellVol.Enabled = false;
            }
        }

        private void rbOCV3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOCV3.Checked)
            {
                chbVol.Checked = true;
                chbACIR.Checked = true;
                chbNShellVol.Checked = true;
                chbPShellVol.Checked = false;

                chbVol.Enabled = false;
                chbACIR.Enabled = false;
                chbNShellVol.Enabled = false;
                chbPShellVol.Enabled = true;
            }
        }

        /// <summary>
        ///  manual test 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualStart_Click(object sender, EventArgs e)
        {
          //List<C_CellBindShow> cellList = CellBindBLL.Instance.GetModelList(" TrayCode= '12312'");
            if (ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 == 0|| ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1==0)
            {
                MessageBox.Show("顶升到位并压合后才能测试！");
                return;
            }
            try
            {
                SetUIEnable(false);
                ClsGlobal.OCVTestContr.InitPara();
                ClsGlobal.IsAutoMode = false;
                Action manualAct = null;
                if (rbOCV2.Checked)
                {
                    ClsGlobal.OCVType = 2;
                    ClsGlobal.TestType = 0;
                    manualAct = ClsGlobal.OCVTestContr.FuncManualTestProcess(mProc, "OCV2");
                }
                if (rbOCV3.Checked)
                {
                    ClsGlobal.OCVType = 3;
                    if (chbNShellVol.Checked && chbPShellVol.Checked)
                        ClsGlobal.TestType = 3;
                    else
                        ClsGlobal.TestType = 2;
                    manualAct = ClsGlobal.OCVTestContr.FuncManualTestProcess(mProc, "OCV3");
                }
                manualAct.BeginInvoke(OnComplete_Manual, manualAct);
            }
            catch (Exception ex)
            {
                MessageBox.Show("测试异常！" + ex.Message);
                SetUIEnable(true);
            }
        }
        private void OnComplete_Manual(IAsyncResult iar)
        {
            try
            {
                Action act = iar.AsyncState as Action;
                act.EndInvoke(iar);
                SetUIEnable(true);
            }
            catch
            {
                SetUIEnable(true);
            }

        }

        private void SetUIEnable(bool value)
        {
            Action act = delegate
              {
                  btnManualStart.Enabled = value;
              };
            if (this.InvokeRequired)
            {
                this.Invoke(act);
            }
            else
            {
                act();
            }
        }

        private void 手动输入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManualInput frm = new FrmManualInput())
            {
                frm.ShowDialog();
            }
        }
    }

}
