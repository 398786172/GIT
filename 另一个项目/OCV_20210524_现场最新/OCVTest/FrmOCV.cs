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
using System.Runtime.InteropServices;
using OCV;
using DB_KT.DAL;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OCV.OCVTest;

namespace OCV
{
    public partial class FrmOCV : Form
    {
        #region  OCV测试

        DateTime DTStart;                       //测试启动时间
        DateTime DTStop;                        //测试结束时间   

        public bool ExportToServerFinish = false;
        public bool ExportToLocFinish = false;

        public System.Windows.Forms.DataGridView dgvTestData1;
        public System.Windows.Forms.DataGridView dgvTestData2;

        private eTestState CurrentOCVTestState = eTestState.Idle;   //当前OCVTestState

        public ClsOCVIRTest mOCVIRTest { get; set; }     //测试控制
        bool TimeStopCheck;                 //禁止timer重复进入
        int InitFlag = 0;                   //流程复位
        int mProcStep = 0;                  //流程步   

        #endregion

        public FrmOCV()
        {
            InitializeComponent();
            dgvTestData1.AutoGenerateColumns = false;
            dgvTestData2.AutoGenerateColumns = false;
            dgvTestData2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestData1.MultiSelect = false;
            dgvTestData2.MultiSelect = false;
        }

        //
        class ThreadMethodHelper
        {
            //线程输入参数
            public List<DB_KT.Model.BatInfo_Year> isBatinfoData;

            public DB_KT.Model.FlowValue isFlowValueData;
            ////函数返回值
            //public long returnVaule;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClsGlobal.ID_InitOK = false;

            //进程已经存在时退出
            if (ClsGlobal.CheckOCVProcessOn())
            {
                this.Close();
            }

            try
            {
                #region OCV参数

                //运行模式
                ClsGlobal.OCV_RunMode = (eRunMode)int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Run_Mode", null));

                //OCV类型参数
                #region 改由工程管理配置 20200828 由ajone 屏蔽
                //ClsGlobal.OCVType = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "OCVType", null));
                #endregion

                ClsGlobal.TrayType = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TrayType", null));

                ClsGlobal.ProbeBoardType = ClsGlobal.TrayType;

                ClsGlobal.EN_TestOCV = 1;
                ClsGlobal.EN_TestACIR = 1;
                #endregion

                #region 密码

                try
                {
                    ClsGlobal.Passward = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Passward", "");
                    if (ClsGlobal.Passward == "")
                    {
                        ClsGlobal.Passward = "12345";
                    }
                }
                catch
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Passward", "12345");
                    ClsGlobal.Passward = "12345";
                }

                #endregion

                #region 数据保存参数

                //数据储存
                //string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
                //ClsGlobal.sLogspath = Application.StartupPath.ToString() + "\\log\\Logs_" + TodayDate + ".txt";
                //ClsGlobal.sLogAlarmpath = Application.StartupPath.ToString() + "\\log\\AlarmLogs_" + TodayDate + ".txt";
                ClsGlobal.CreateLogsFile(ClsGlobal.sLogspath);
                ClsGlobal.CreateLogsFile(ClsGlobal.sLogAlarmpath);

                ClsGlobal.sLogEventPath = Application.StartupPath.ToString() + "\\EventLog.txt";
                ClsGlobal.CreateLogsFile(ClsGlobal.sLogEventPath);





                //数据上传文件夹
                if (!Directory.Exists(Application.StartupPath + "\\DZBQ"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\DZBQ");      //生成
                }

                ClsGlobal.mDataUploadFilePath = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DataUploadFilePath", null);
                if (ClsGlobal.mDataUploadFilePath == null)
                {
                    string path = Application.StartupPath + "\\DZBQ";
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DataUploadFilePath", path);
                    ClsGlobal.mDataUploadFilePath = path;
                }



                #endregion

                #region OCV测试 

                try
                {
                    if (ClsGlobal.OCVType == 1)         //设备类型:OCV-IR, 
                    {
                        mOCVIRTest = new ClsOCVIRTest(ClsGlobal.mIOControl, ClsGlobal.RTCom, ClsGlobal.RT_Speed, ClsGlobal.DMT_USBAddr, this);
                    }
                    else if (ClsGlobal.OCVType == 2)    //设备类型:OCV 
                    {
                        mOCVIRTest = new ClsOCVIRTest(ClsGlobal.mIOControl, ClsGlobal.DMT_USBAddr, this);
                    }
                    ClsGlobal.OCVIRTest = mOCVIRTest;
                }
                catch (Exception ex)
                {
                    throw new Exception("初始化失败:" + ex.Message.ToString());
                }

                #endregion

                #region 20200702 zxz 初始化通道映射表
                for (int i = 0; i < 40; i++)
                {
                    ChannelItem itm1 = new ChannelItem();
                    itm1.Channel = 1 + (i * 6);
                    itm1.RealChannel = 161 + (i * 2);
                    ClsGlobal.ChannelMapping.Add(itm1);
                    ChannelItem itm2 = new ChannelItem();
                    itm2.Channel = 2 + (i * 6);
                    itm2.RealChannel = 162 + (i * 2);
                    ClsGlobal.ChannelMapping.Add(itm2);
                }

                for (int i = 0; i < 40; i++)
                {
                    ChannelItem itm1 = new ChannelItem();
                    itm1.Channel = 3 + (i * 6);
                    itm1.RealChannel = 81 + (i * 2);
                    ClsGlobal.ChannelMapping.Add(itm1);
                    ChannelItem itm2 = new ChannelItem();
                    itm2.Channel = 4 + (i * 6);
                    itm2.RealChannel = 82 + (i * 2);
                    ClsGlobal.ChannelMapping.Add(itm2);
                }

                for (int i = 0; i < 40; i++)
                {
                    ChannelItem itm1 = new ChannelItem();
                    itm1.Channel = 5 + (i * 6);
                    itm1.RealChannel = 1 + (i * 2);
                    ClsGlobal.ChannelMapping.Add(itm1);
                    ChannelItem itm2 = new ChannelItem();
                    itm2.Channel = 6 + (i * 6);
                    itm2.RealChannel = 2 + (i * 2);
                    ClsGlobal.ChannelMapping.Add(itm2);
                }

                for (int i = 241; i <= 256; i++)
                {
                    ChannelItem itm = new ChannelItem();
                    itm.Channel = i;
                    itm.RealChannel = i;
                    ClsGlobal.ChannelMapping.Add(itm);
                }

                ClsGlobal.ChannelMapping.Sort();
                #endregion

                #region 界面初始化

                #region  改用工程管理方式设定 20200827 由ajone屏蔽
                ////电压上下限输出
                //txtVoltUpLmt.Text = ClsGlobal.VolUpLmt.ToString();
                //txtVoltDownLmt.Text = ClsGlobal.VolDownLmt.ToString();

                ////内阻上下限输出
                //txtACIRUpLmt.Text = ClsGlobal.ACIRUpLmt.ToString();
                //txtACIRDownLmt.Text = ClsGlobal.ACIRDownLmt.ToString();
                #endregion


                dgvManualTest.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10);

                //电池自动与手动测试界面
                int Rows = 0, Rows2 = 0;

                Rows = ClsGlobal.TrayType / 2;
                Rows2 = ClsGlobal.TrayType;

                dgvManualTest.Rows.Add(Rows2);
                dgvManualTest.Font = new Font("微软雅黑", 12);

                for (int i = 0; i < Rows2; i++)
                {
                    dgvManualTest.Rows[i].Cells[0].Value = i + 1;
                    dgvManualTest.Rows[i].Cells[1].Value = i / 6 + 1;          //列
                    dgvManualTest.Rows[i].Cells[2].Value = i % 43 + 1;          //行
                }

                grpTestManual.Enabled = false;      //多通道手动测试



                //运行模式
                if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                {
                    lblOCVRunMode.Text = "联网OCV测试";
                    lblOCVRunMode.ForeColor = Color.LightGreen;
                }
                //else if (ClsGlobal.OCV_RunMode == eRunMode.GoAhead)
                //{
                //    lblOCVRunMode.Text = "托盘直接排出";
                //    lblOCVRunMode.ForeColor = Color.Red;
                //}
                else if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                {
                    lblOCVRunMode.Text = "单机OCV测试";
                    lblOCVRunMode.ForeColor = Color.Yellow;
                }
                else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                {
                    lblOCVRunMode.Text = "内阻校准";
                    lblOCVRunMode.ForeColor = Color.GreenYellow;
                }

                grpManualSwitch.Enabled = false;
                grpManualIROCV.Enabled = false;
                grpManualOCV.Enabled = false;


                #endregion                              

                #region IO调试

                Thread thread_debug = new Thread(RefreshIODebug);
                thread_debug.Start();

                grpProbeBoard.Enabled = false;
                grpMsg.Enabled = false;

                #endregion

                #region 工程设定
                FrmProcessManager frmProcessManager = new FrmProcessManager();
                frmProcessManager.TopLevel = false;
                frmProcessManager.Parent = tab_SetPara;
                frmProcessManager.FormBorderStyle = FormBorderStyle.None;
                frmProcessManager.Dock = DockStyle.Fill;
                frmProcessManager.Show();
                frmProcessManager.BringToFront();
                #endregion

                #region 托盘显示
                //FrmBateryTestView frmBateryTestView = new FrmBateryTestView();
                //frmBateryTestView.TopLevel = false;
                //frmBateryTestView.Parent = tbp_TrayView;
                //frmBateryTestView.FormBorderStyle = FormBorderStyle.None;
                //frmBateryTestView.Dock = DockStyle.Fill;
                //frmBateryTestView.Show();
                //frmBateryTestView.BringToFront();
                #endregion

                //BH20200304 读取OCV分选设置 取消

                //ClsGlobal.WriteLog("开始读取OCV分选设置", ClsGlobal.sDebugOCVSelectionPath);

                //UpdateInfo();

                mProcStep = 0;
                tim_Proc_OCV.Interval = 200;
                tim_Proc_OCV.Start();        //流程运行
                tim_UI_OCV.Start();             //界面刷新

            }
            catch (Exception ex)
            {
                ClsGlobal.ID_InitOK = false;
                MessageBox.Show($"初始化frmOCV界面失败:{ex.Message}");
                ClsGlobal.WriteLogOCV(ex.Message);
            }
            finally
            {
            }
        }


        #region 多通道手动测试

        private void chkEnMultiTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnMultiManual.Checked == true)
            {
                frmUserPwd fuserpwd = new frmUserPwd(1);

                if (fuserpwd.ShowDialog() == DialogResult.OK)
                {
                    //groupBox7.Enabled = true;
                    grpTestManual.Enabled = true;
                }
                else
                {
                    chkEnMultiManual.Checked = false;
                }
            }
            else
            {
                //groupBox7.Enabled = false;
                grpTestManual.Enabled = false;
                chkEnMultiManual.Checked = false;
            }
        }

        private void btnTestMulti_Click(object sender, EventArgs e)
        {
            ClearDataGrid();
            //DTStart = DateTime.Now;
            try
            {
                //grpTestM.Enabled = false;
                btnTestMulti.Enabled = false;
                btnTestMulti.Text = "测试中...";
                mOCVIRTest.StartManualTestAction();
            }
            catch
            {
                MessageBox.Show("读取电压/内阻值失败");
            }
        }


        private void btnStopTest_Click(object sender, EventArgs e)
        {
            mOCVIRTest.StopManualTest();
        }

        private void btnClearTest_Click(object sender, EventArgs e)
        {
            ClearDataGrid();
        }

        private void btnSaveTest_Click(object sender, EventArgs e)
        {
            SaveTest_csv();
        }

        private void SaveTest_csv()
        {
            try
            {
                string addr = Application.StartupPath + "\\TestFiles\\" + "DebugTest_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                using (StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default))
                {

                    SWR.WriteLine("序号, 列,行,OCV,ACIR");

                    for (int i = 0; i <= dgvManualTest.RowCount - 2; i++)
                    {
                        if (dgvManualTest[1, i].Value == null)
                        {
                            dgvManualTest[1, i].Value = "";
                        }
                        if (dgvManualTest[2, i].Value == null)
                        {
                            dgvManualTest[2, i].Value = "";
                        }
                        if (dgvManualTest[3, i].Value == null)
                        {
                            dgvManualTest[3, i].Value = "";
                        }
                        if (dgvManualTest[4, i].Value == null)
                        {
                            dgvManualTest[4, i].Value = "";
                        }

                        SWR.WriteLine(dgvManualTest[0, i].Value.ToString() + "," +
                        dgvManualTest[1, i].Value.ToString() + "," +
                        dgvManualTest[2, i].Value.ToString() + "," +
                        dgvManualTest[3, i].Value.ToString() + "," +
                        dgvManualTest[4, i].Value.ToString());
                    }

                    SWR.Close();
                }
                MessageBox.Show("数据保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据保存失败");
            }
        }

        private void SaveTest_xls()
        {
            ////数据保存到excel
            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //Int16 i, j;

            //xlApp = new Excel.ApplicationClass();
            //xlWorkBook = xlApp.Workbooks.Add(misValue);

            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //xlWorkSheet.Cells[1, 1] = "[OCVResult]";
            //xlWorkSheet.Cells[2, 1] = "[ProcessInfo]";
            //xlWorkSheet.Cells[3, 1] = "TrayNo =";
            //xlWorkSheet.Cells[3, 2] = ClsGlobal.TrayCode;
            //xlWorkSheet.Cells[4, 1] = DTStart.ToString("yyyy_MM_dd_HH:mm:ss");
            //xlWorkSheet.Cells[5, 1] = DTStop.ToString("yyyy_MM_dd_HH:mm:ss");
            //xlWorkSheet.Cells[6, 1] = "电池号";
            //xlWorkSheet.Cells[6, 2] = "电压数据";
            //xlWorkSheet.Cells[6, 3] = "壳体电压数据";
            //xlWorkSheet.Cells[6, 4] = "温度数据";

            //for (i = 0; i <= dataGridView2.RowCount - 2; i++)
            //{
            //    for (j = 0; j <= dataGridView2.ColumnCount - 1; j++)
            //    {
            //        if (dataGridView2[j, i].Value != null)
            //        {
            //            xlWorkSheet.Cells[i + 7, j + 1] = dataGridView2[j, i].Value.ToString();
            //        }
            //    }
            //}

            //string addr = Application.StartupPath + "\\TestFiles\\" + "DebugTest" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            //xlWorkBook.SaveAs(addr, Excel.XlFileFormat.xlWorkbookNormal,
            //    misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue,
            //    misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);
        }

        #endregion

        #region 手动测试

        private void chkEnManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnManual.Checked == true)
            {
                frmUserPwd fuserpwd = new frmUserPwd(1);

                if (fuserpwd.ShowDialog() == DialogResult.OK)
                {
                    grpManualSwitch.Enabled = true;

                    grpManualIROCV.Enabled = true;
                    grpManualOCV.Enabled = true;

                }
                else
                {
                    chkEnManual.Checked = false;
                }

            }
            else
            {
                grpManualSwitch.Enabled = false;
                grpManualIROCV.Enabled = false;
                grpManualOCV.Enabled = false;
                chkEnManual.Checked = false;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl_OCV.SelectedTab.Name == "tab_Debug")
            {
                if (chkEnDebug.Checked == true)
                {
                    ClsGlobal.EN_DebugFresh = true;
                }
            }
            else
            {
                ClsGlobal.EN_DebugFresh = false;
            }

            if (this.tabControl_OCV.SelectedTab.Name == "tab_SetPara")
            {

                #region  改用工程管理方式设定 20200827 由ajone屏蔽
                //电压上下限
                //txtVoltDownLmt.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "VolDownLmt", null);
                //txtVoltUpLmt.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "VolUpLmt", null);
                //txtVoltUpLmt.Text = ClsGlobal.VolUpLmt.ToString();
                //txtVoltDownLmt.Text = ClsGlobal.VolDownLmt.ToString();
                #endregion
            }

        }

        private int GetRealChannel(int ch)
        {
            int rCh = 0;
            foreach (ChannelItem itm in ClsGlobal.ChannelMapping)
            {
                if (itm.Channel == ch)
                {
                    rCh = itm.RealChannel;
                    break;
                }
            }
            return rCh;
        }

        private void btnSWChannel_Click(object sender, EventArgs e)
        {
            int err;

            uint result;
            if (uint.TryParse(txtChannel.Text, out result) == true)
            {
                int realChannel = GetRealChannel((int)result);  //20200702 zxz 增加通道映射


                if (realChannel != 0)
                {

                    if (radioB_V.Checked == true)  //wjp,2020/7/7
                    {
                        mOCVIRTest.SwitchDev.ChannelVoltIRSwitchContr(2, 1, realChannel);
                    }
                    else
                    {
                        mOCVIRTest.SwitchDev.ChannelVoltIRSwitchContr(2, 2, realChannel);
                    }
                }

            }
        }

        private void tab_ManualTest_Click(object sender, EventArgs e)
        {

        }

        private void btnTestVolt_Click(object sender, EventArgs e)
        {
            try
            {
                double Val;
                double Val2;
                mOCVIRTest.mRTester.ReadDataRV(out Val, out Val2);

                //txtVolt.Text = (Val * 1000).ToString("f1");
                //txtACIR.Text = (Val2 * 1000).ToString("f2");
                if (radioB_V.Checked == true)  //wjp,2020/7/7
                {
                    txtVolt.Text = (Val * 1000).ToString("f1");
                }

                else
                {
                    txtACIR.Text = (Val2 * 1000).ToString("f2");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("读取测试值失败:" + ex.Message.ToString());
            }

        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                mOCVIRTest.mRTester.InitControl_IMM(1, 2);
                //mOCVIRTest.mDmm.InitControl_IMM();
            }
            catch
            {
                MessageBox.Show("初始化失败");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mOCVIRTest.SwitchDev.ChannelVoltIRSwitchContr(1, 0, 0);
        }

        #endregion

        #region 工程设定(已弃用)  改用工程管理方式设定 20200827 由ajone屏蔽

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    double Val1, Val2;

        //    frmUserPwd fuserpwd = new frmUserPwd(1);
        //    if (fuserpwd.ShowDialog() != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        //电压上下限
        //        double.TryParse(txtVoltDownLmt.Text, out Val1);
        //        double.TryParse(txtVoltUpLmt.Text, out Val2);
        //        INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "VolDownLmt", Val1.ToString());
        //        INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "VolUpLmt", Val2.ToString());

        //        //刷新

        //        //电压上下限
        //        txtVoltUpLmt.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "VolUpLmt", null);
        //        txtVoltDownLmt.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "VolDownLmt", null);

        //        ClsGlobal.VolUpLmt = double.Parse(txtVoltUpLmt.Text);
        //        ClsGlobal.VolDownLmt = double.Parse(txtVoltDownLmt.Text);

        //        //内阻上下限
        //        txtACIRUpLmt.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "ACIRUpLmt", null);
        //        txtACIRDownLmt.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "OCVSetting", "ACIRDownLmt", null);

        //        ClsGlobal.ACIRUpLmt = double.Parse(txtACIRUpLmt.Text);
        //        ClsGlobal.ACIRDownLmt = double.Parse(txtACIRDownLmt.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        #endregion

        #region 设备调试

        private void chkEnDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnDebug.Checked == true)
            {
                frmUserPwd fuserpwd = new frmUserPwd(1);

                if (fuserpwd.ShowDialog() == DialogResult.OK)
                {

                    //grpProbeBoard.Enabled = true;
                    grpProbeBoard.Enabled = false; //wjp,2020/7/7 针床需在非调试模式下使用
                    grpMsg.Enabled = true;
                    ClsGlobal.mIOControl.Set_TestFinish();
                    Thread.Sleep(20);
                    ClsGlobal.mIOControl.Set_DebugIn();
                    ClsGlobal.EN_DebugFresh = true;


                }
                else
                {
                    ClsGlobal.EN_DebugFresh = false;
                    chkEnDebug.Checked = false;
                }

            }
            else
            {
                ClsGlobal.mIOControl.Set_DebugOut();
                ClsGlobal.EN_DebugFresh = false;
                //grpProbeBoard.Enabled = false;
                grpProbeBoard.Enabled = true;//wjp,2020/7/7

                grpMsg.Enabled = false;
                chkEnDebug.Checked = false;
                lblPBPosOpen.BackColor = Color.LightGray;
                lblPBPosPress.BackColor = Color.LightGray;
                lblType18650.BackColor = Color.LightGray;
                lblType21700.BackColor = Color.LightGray;
                lblTrayInPlace.BackColor = Color.LightGray;
            }
        }

        private void RefreshIODebug()
        {
            ushort Val;

            while (true)
            {

                if (ClsGlobal.EN_DebugAdvFresh == true || ClsGlobal.EN_DebugFresh == false)  //wjp,2020/7/8 
                {
                    continue;
                }

                if (ClsGlobal.CloseProg == true)
                {
                    break;
                }

                Thread.Sleep(500);

                if (this.IsHandleCreated == true)
                {
                    if (ClsGlobal.EN_DebugFresh == false) continue;
                    #region//wjp,2020/7/8 原有，注释
                    //Val = ClsGlobal.mIOControl.Get_M_Input1();
                    ////气压
                    //if ((Val & (1 << 0)) > 0)
                    //{
                    //    lblAirPress.BackColor = Color.LightGreen;
                    //}
                    //else
                    //{
                    //    lblAirPress.BackColor = Color.Red;
                    //}

                    ////紧急按钮
                    //if ((Val & (1 << 1)) > 0)
                    //{
                    //    lblEmergeBtn.BackColor = Color.Red;
                    //}
                    //else
                    //{
                    //    lblEmergeBtn.BackColor = Color.LightGray;
                    //}

                    ////启动按钮
                    //if ((Val & (1 << 2)) > 0)
                    //{
                    //    lblStartBtn.BackColor = Color.LightGreen;
                    //}
                    //else
                    //{
                    //    lblStartBtn.BackColor = Color.LightGray;
                    //}

                    ////烟雾报警
                    //if ((Val & (1 << 7)) > 0)
                    //{
                    //    lblSmokeAlarm.BackColor = Color.LightGreen;      //常闭
                    //}
                    //else
                    //{
                    //    lblSmokeAlarm.BackColor = Color.Red;            //断开, 报警
                    //}

                    ////托盘有无
                    //if ((Val & (1 << 8)) > 0)
                    //{
                    //    lblTrayInPlace.BackColor = Color.LightGreen;
                    //}
                    //else
                    //{
                    //    lblTrayInPlace.BackColor = Color.LightGray;
                    //}

                    ////探针压合
                    //if ((Val & (1 << 3)) > 0 && (Val & (1 << 4)) > 0)
                    //{
                    //    lblPBPosPress.BackColor = Color.LightGreen;
                    //    lblPBPosOpen.BackColor = Color.LightGray;
                    //}
                    //else if ((Val & (1 << 3)) == 0 && (Val & (1 << 4)) == 0)
                    //{
                    //    lblPBPosPress.BackColor = Color.LightGray;
                    //    lblPBPosOpen.BackColor = Color.LightGreen;
                    //}
                    //else
                    //{
                    //    lblPBPosPress.BackColor = Color.LightGray;
                    //    lblPBPosOpen.BackColor = Color.LightGray;
                    //}

                    ////if (ClsGlobal.EN_DebugFresh == false) continue;
                    ////if (mClsIOControl.Get_M_PBPress() == 1)
                    ////{
                    ////    lblPBPosPress.BackColor = Color.LightGreen;
                    ////    lblPBPosOpen.BackColor = Color.LightGray;
                    ////}
                    ////else
                    ////{
                    ////    lblPBPosPress.BackColor = Color.LightGray;
                    ////    lblPBPosOpen.BackColor = Color.LightGreen;
                    ////}

                    ////电池换型
                    //if (ClsGlobal.EN_DebugFresh == false) continue;

                    //Val = ClsGlobal.mIOControl.Get_M_PressBattType();

                    ////if (Val==1)
                    ////{
                    ////    ClsGlobal.OCV_BatteryType = "18650";

                    ////}
                    ////else if(Val==2)
                    ////{
                    ////    ClsGlobal.OCV_BatteryType = "21700";
                    ////}
                    ////else
                    ////{
                    ////    ClsGlobal.OCV_BatteryType = "N/A";
                    ////}

                    ////if (ClsGlobal.EN_DebugFresh == false) continue;
                    //if (Val == 1)
                    //{
                    //    lblType18650.BackColor = Color.LightGreen;
                    //    lblType21700.BackColor = Color.LightGray;
                    //}
                    //else if (Val == 2)
                    //{
                    //    lblType18650.BackColor = Color.LightGray;
                    //    lblType21700.BackColor = Color.LightGreen;
                    //}
                    //else
                    //{
                    //    lblType18650.BackColor = Color.LightGray;
                    //    lblType21700.BackColor = Color.LightGray;
                    //}

                    #endregion


                    #region//IO状态显示
                    //调试状态
                    Val = ClsGlobal.mIOControl.Get_Mode_Output();
                    if (Val > 0)
                    {
                        lbTestMode.BackColor = Color.LightGreen;
                        //lbTestMode.Text = "调试模式";
                    }
                    else
                    {
                        lbTestMode.BackColor = Color.LightGray;
                        //lbTestMode.Text = "非调试模式";
                    }

                    #endregion

                    #region//wjp,2020/7/8
                    Val = ClsGlobal.mIOControl.Get_M_Output1();
                    ////托盘夹紧动作 1: 夹紧  0:复位
                    if ((Val & (1 << 0)) > 0)
                    {
                        lbTrayClose.BackColor = Color.LightGreen;
                        lbTrayOpen.BackColor = Color.LightGray;
                    }
                    else
                    {
                        lbTrayClose.BackColor = Color.LightGray;
                        lbTrayOpen.BackColor = Color.LightGreen;
                    }
                    ////托盘下降动作 1: 下降  0:上升
                    if ((Val & (1 << 1)) > 0)
                    {
                        lbTrayDown.BackColor = Color.LightGreen;
                        lbTrayUp.BackColor = Color.LightGray;
                    }
                    else
                    {
                        lbTrayDown.BackColor = Color.LightGray;
                        lbTrayUp.BackColor = Color.LightGreen;
                    }
                    ////托盘前推动作 1: 前推  0:复位
                    if ((Val & (1 << 2)) > 0)
                    {
                        lbTrayIn.BackColor = Color.LightGreen;
                        lbTrayOut.BackColor = Color.LightGray;
                    }
                    else
                    {
                        lbTrayIn.BackColor = Color.LightGray;
                        lbTrayOut.BackColor = Color.LightGreen;
                    }
                    ////探针针板压合动作 1: 压合  0:复位
                    if ((Val & (1 << 3)) > 0)
                    {
                        lbPress.BackColor = Color.LightGreen;
                        lbOpen.BackColor = Color.LightGray;
                    }
                    else
                    {
                        lbPress.BackColor = Color.LightGray;
                        lbOpen.BackColor = Color.LightGreen;
                    }
                    ////双色灯(红灯) 1: 亮  0: 关
                    if ((Val & (1 << 4)) > 0)
                    {
                        lbRed.BackColor = Color.LightGreen;
                        //lbGreen.BackColor = Color.LightGray;
                    }
                    else
                    {
                        lbRed.BackColor = Color.LightGray;
                        //lbGreen.BackColor = Color.LightGreen;
                    }
                    ////双色灯(绿灯) 1: 亮  0: 关
                    if ((Val & (1 << 5)) > 0)
                    {
                        lbGreen.BackColor = Color.LightGreen;
                        //lbRed.BackColor = Color.LightGray;
                    }
                    else
                    {
                        lbGreen.BackColor = Color.LightGray;
                        //lbRed.BackColor = Color.LightGreen;
                    }
                    ////灯塔(红灯) 1: 亮  0: 关
                    if ((Val & (1 << 6)) > 0)
                    {
                        lbUpRed.BackColor = Color.LightGreen;

                    }
                    else
                    {
                        lbUpRed.BackColor = Color.LightGray;

                    }
                    /////灯塔(橙灯) 1: 亮  0: 关
                    if ((Val & (1 << 7)) > 0)
                    {
                        lbUpOrange.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbUpOrange.BackColor = Color.LightGray;
                    }
                    ////灯塔(绿灯) 1: 亮  0: 关
                    if ((Val & (1 << 8)) > 0)
                    {
                        lbUpGreen.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbUpGreen.BackColor = Color.LightGray;
                    }
                    ////灯塔(蜂鸣器) 1: 亮  0: 关
                    if ((Val & (1 << 9)) > 0)
                    {
                        lbBuzzer.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbBuzzer.BackColor = Color.LightGray;
                    }
                    #endregion

                    #region//wjp,2020/7/8
                    Val = ClsGlobal.mIOControl.Get_M_Input1();

                    ////托盘夹爪夹紧
                    if ((Val & (1 << 0)) > 0)
                    {
                        lbTray_Press.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbTray_Press.BackColor = Color.LightGray;
                    }
                    ////托盘夹爪复位 
                    if ((Val & (1 << 1)) > 0)
                    {
                        lbTray_Open.BackColor = Color.LightGreen;

                    }
                    else
                    {
                        lbTray_Open.BackColor = Color.LightGray;
                    }
                    ////托盘下降位 
                    if ((Val & (1 << 2)) > 0)
                    {
                        lbTray_Down.BackColor = Color.LightGreen;

                    }
                    else
                    {
                        lbTray_Down.BackColor = Color.LightGray;

                    }
                    ////托盘上升位
                    if ((Val & (1 << 3)) > 0)
                    {
                        lbTray_Up.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbTray_Up.BackColor = Color.LightGray;
                    }
                    ////托盘前推位
                    if ((Val & (1 << 4)) > 0)
                    {
                        lbTray_In.BackColor = Color.LightGreen;

                    }
                    else
                    {
                        lbTray_In.BackColor = Color.LightGray;

                    }
                    ////托盘后退位
                    if ((Val & (1 << 5)) > 0)
                    {
                        lbTray_Out.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbTray_Out.BackColor = Color.LightGray;
                    }
                    ////探针压合位
                    if ((Val & (1 << 6)) > 0)
                    {
                        lb_Press.BackColor = Color.LightGreen;

                    }
                    else
                    {
                        lb_Press.BackColor = Color.LightGray;

                    }
                    ///探针打开位
                    if ((Val & (1 << 7)) > 0)
                    {
                        lb_Open.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Open.BackColor = Color.LightGray;
                    }
                    ////急停按钮
                    if ((Val & (1 << 8)) > 0)
                    {
                        lb_Stop.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Stop.BackColor = Color.LightGray;
                    }
                    ////启动按钮
                    if ((Val & (1 << 9)) > 0)
                    {
                        lb_Run.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Run.BackColor = Color.LightGray;
                    }

                    ///托盘到位位置
                    if ((Val & (1 << 10)) > 0)
                    {
                        lb_Tray.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Tray.BackColor = Color.LightGray;
                    }
                    ////气压表
                    if ((Val & (1 << 11)) > 0)
                    {
                        lb_Baro.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Baro.BackColor = Color.LightGray;
                    }
                    ////烟感1
                    if ((Val & (1 << 12)) > 0)
                    {
                        lb_Smoke1.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Smoke1.BackColor = Color.LightGray;
                    }
                    ///烟感2
                    if ((Val & (1 << 13)) > 0)
                    {
                        lb_Smoke2.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_Smoke2.BackColor = Color.LightGray;
                    }
                    ////门开关
                    if ((Val & (1 << 14)) > 0)
                    {
                        lb_door.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lb_door.BackColor = Color.LightGray;
                    }

                    #endregion

                }

            }
        }

        private void btnDebugOpenPB_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TestFinish();
        }

        private void btnDebugPressPB_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_DebugPressPB();
        }

        int mToggleflag = 0;
        int mToggleflag2 = 0;
        private void btnRedLight_Click(object sender, EventArgs e)
        {
            if (mToggleflag == 0)
            {
                ClsGlobal.mIOControl.Set_DebugRedLight_On();
                mToggleflag = 1;
            }
            else
            {
                ClsGlobal.mIOControl.Set_DebugRedLight_Off();
                mToggleflag = 0;
            }
        }

        private void btnGreenLight_Click(object sender, EventArgs e)
        {
            if (mToggleflag2 == 0)
            {
                ClsGlobal.mIOControl.Set_DebugGreenLight_On();
                mToggleflag2 = 1;
            }
            else
            {
                ClsGlobal.mIOControl.Set_DebugGreenLight_Off();
                mToggleflag2 = 0;
            }
        }

        private void btnSUB_V_Click(object sender, EventArgs e)
        {
            int Num;

            if (int.TryParse(txtChannel.Text, out Num) == true && Num > 1)
            {
                txtChannel.Text = (Num - 1).ToString();
            }
            else
            {
                txtChannel.Text = "1";
            }
        }

        private void btnAdd_V_Click(object sender, EventArgs e)
        {
            int Num;

            if (int.TryParse(txtChannel.Text, out Num) == true && Num > 0)
            {
                txtChannel.Text = (Num + 1).ToString();
            }
            else
            {
                txtChannel.Text = "1";
            }
        }

        #endregion

        #region 校准

        private void tim_Adjust_Tick(object sender, EventArgs e)
        {
            if (ClsGlobal.OCV_TestState == eTestState.AdjustEnd)
            {
                //btnTestMultiACIR.Text = "测内阻";
                //txtAdjust.Text = "校准完成";
            }
        }

        private void 校准设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOCVMasterSetting Form1 = new FrmOCVMasterSetting();
            Form1.Show();
        }


        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsGlobal.CloseProg = true;
        }


        //输出本地文件
        private void ExportLocalData()
        {
            DataTable dtTemplate = new DataTable();                             //表模板
            try
            {
                if (ClsGlobal.listETCELL.Count == 0)
                {
                    return;
                }

                //取得开始时间和结束时间
                string _sdtStartTime = ClsGlobal.TestStartTime.ToString();       //开始时间
                string _sdtEndTime = ClsGlobal.TestEndTime.ToString();           //结束时间 
                string _sExcelPath = Application.StartupPath;

                //创建导出数据文件夹
                string _sFilePath = _sExcelPath + "\\" + "ExportOCVData";
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }
                //创建月份文件夹
                string _sYM = System.DateTime.Now.ToString("yyyyMM");
                _sFilePath = _sFilePath + "\\" + _sYM;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }
                //创建当天文件夹
                int _iDay = System.DateTime.Now.Day;
                _sFilePath = _sFilePath + "\\" + _iDay;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }

                //ExportDataToCSV(_sFilePath);
                ExportDataToCSV_withKVal(_sFilePath);


                ExportToLocFinish = true;

            }
            catch (Exception ex)
            {
                ClsGlobal.OCV_TestState = eTestState.ErrDataSaveLocal;
                ClsGlobal.ErrMsg = "ExportLocalData:" + ex.Message + ex.StackTrace;
                ExportToLocFinish = false;
                //ClsGlobal.WriteLogOCV(ClsGlobal.ErrMsg);
            }
            finally
            {

            }
        }

        //保存校准文件
        private void SaveAdjustData()
        {
            SetIRAdjust(ClsGlobal.mIRAdjustPath);
        }


        //保存内阻校准值
        public static void SetIRAdjust(string Path)
        {
            double[] ArrAdjust = new double[ClsGlobal.TrayType];

            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                ArrAdjust[i] = ClsGlobal.ArrIRTrueVal[i] - ClsGlobal.ArrAdjustACIR[i];
                INIAPI.INIWriteValue(Path, "ACIRAdjust", "CH" + (i + 1).ToString(), ArrAdjust[i].ToString("F2"));
                ClsGlobal.ArrIRAdjustPara[i] = ArrAdjust[i].ToString();
            }
        }


        //保存到本地excel
        private void ExportDataToExcel(string ExcelPath, string StartTime = "", string EndTime = "")
        {
            ////数据保存到excel
            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //xlApp = new Excel.ApplicationClass();
            //xlWorkBook = xlApp.Workbooks.Add(misValue);

            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //xlWorkSheet.Cells[1, 1] = "[OCVResult]";
            //xlWorkSheet.Cells[2, 1] = "[ProcessInfo]";
            //xlWorkSheet.Cells[3, 1] = "TrayNo =";
            //xlWorkSheet.Cells[3, 2] = ClsGlobal.TrayCode;
            //xlWorkSheet.Cells[4, 1] = "Start = " + StartTime;
            //xlWorkSheet.Cells[5, 1] = "End = " + EndTime;
            //xlWorkSheet.Cells[6, 1] = "MinVolt = " + ClsGlobal.VolDownLmt.ToString();    //txtDownLMT.Text;
            //xlWorkSheet.Cells[6, 2] = "MaxVolt = " + ClsGlobal.VolUpLmt.ToString();      //txtUpLMT.Text;
            //xlWorkSheet.Cells[7, 1] = "Num";
            //xlWorkSheet.Cells[7, 2] = "Cell ID";
            //xlWorkSheet.Cells[7, 3] = "OCV" + ClsGlobal.OCV_Num.ToString();
            //xlWorkSheet.Cells[7, 4] = "CASE OCV";
            //xlWorkSheet.Cells[7, 5] = "CODE";
            //xlWorkSheet.Cells[7, 6] = "TEMP";
            //xlWorkSheet.Cells[7, 7] = "修正值0CV";

            //int startRow = 8;
            //for (int i = 0; i < ClsGlobal.TrayType; i++)
            //{
            //    xlWorkSheet.Cells[i + startRow, 1] = i + 1;
            //    xlWorkSheet.Cells[i + startRow, 2] = ClsDBZhongDingContr.alCellID[i].ToString();
            //    xlWorkSheet.Cells[i + startRow, 3] = ClsGlobal.G_dbl_DataArr[i] * 1000;
            //    xlWorkSheet.Cells[i + startRow, 4] = ClsGlobal.G_dbl_VshellArr[i];
            //    xlWorkSheet.Cells[i + startRow, 5] = ClsGlobal.G_il_NGArr[i];
            //    xlWorkSheet.Cells[i + startRow, 6] = ClsGlobal.G_dbl_TempArr[i];
            //    xlWorkSheet.Cells[i + startRow, 7] = ClsGlobal.G_dbl_DataArr[i] * 1000 + (ClsGlobal.G_dbl_TempArr[i] - ClsGlobal.TempBase) *
            //        ClsGlobal.TempParaModify;
            //}

            ////地址
            //string addr = ExcelPath + "\\OCV" + ClsGlobal.OCV_Num.ToString() + "_" + ClsGlobal.TrayCode +
            //    "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

            //xlWorkBook.SaveAs(addr, Excel.XlFileFormat.xlWorkbookNormal,
            //    misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue,
            //    misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        //刷新UI界面
        private void tim_UI_OCV_Tick(object sender, EventArgs e)
        {
            string strInfo;

            //调试禁止
            if (ClsGlobal.Trans_State != eTransState.Stop)
            {
                chkEnDebug.Checked = false;
                chkEnManual.Checked = false;
                chkEnMultiManual.Checked = false;

                chkEnDebug.Enabled = false;
                chkEnManual.Enabled = false;
                chkEnMultiManual.Enabled = false;

            }
            else
            {
                chkEnDebug.Enabled = true;
                chkEnManual.Enabled = true;
                chkEnMultiManual.Enabled = true;
            }

            //手动测试////////////////////////////////////////////////////////////
            //if (mOCVIRTest.ManualTestFinish == true)
            {
                btnTestMulti.Text = "测试";
                grpTestM.Enabled = true;
                btnTestMulti.Enabled = true;
            }

            //自动测试/////////////////////////////////////////////////////////////
            if (ClsGlobal.Trans_RequestTest == 1)
            {
                txtScanCode.Text = ClsGlobal.TrayCode;
            }
            else if (ClsGlobal.Trans_State == eTransState.TrayTestFinish)
            {
                txtScanCode.Text = "";
            }

            //测试状态显示
            if (CurrentOCVTestState != ClsGlobal.OCV_TestState)
            {
                strInfo = "";
                switch (ClsGlobal.OCV_TestState)
                {
                    case eTestState.Idle:
                        strInfo = "OCV空闲";
                        break;
                    case eTestState.Standby:
                        strInfo = "OCV就绪";
                        break;
                    case eTestState.Testing:
                        strInfo = "OCV测试中";
                        break;
                    case eTestState.TestEnd:
                        strInfo = "OCV测试完成";
                        break;
                    case eTestState.AdjustEnd:
                        strInfo = "校准测试完成";
                        break;
                    case eTestState.ErrOCVTest:
                        strInfo = "OCV测试异常";
                        break;
                    case eTestState.ErrAdjust:
                        strInfo = "OCV校准异常";
                        break;
                    case eTestState.ErrOCVDataGetFail:
                        strInfo = "OCV获取电池数据失败";
                        break;
                    case eTestState.ErrOCVFlowErr:
                        strInfo = "OCV流程出错";
                        break;
                    case eTestState.ErrDataSaveKTFail:
                        strInfo = "保存到擎天数据库失败";
                        break;
                    case eTestState.ErrOther:
                        strInfo = "出现其他错误";
                        break;
                }
                ShowMsn(strInfo);
                //ClsGlobal.WriteLogOCV(strInfo);
                CurrentOCVTestState = ClsGlobal.OCV_TestState;
            }

        }

        //OCV自动测试控制
        private void tim_Proc_OCV_Tick(object sender, EventArgs e)
        {
            string strTemp;
            //DateTime LastOCVTime=DateTime.Now;//算k值用

            if (TimeStopCheck == true)
            {
                return;
            }
            try
            {
                TimeStopCheck = true;
                //初始化
                if (ClsGlobal.Trans_State == eTransState.Ready && InitFlag == 0)
                {
                    //ClsGlobal.OCV_AdjustWorkOn = false;
                    //ClsGlobal.IRTrueValSetFlag = false;
                    ClsGlobal.TestCount = 0;
                    ClsGlobal.AdjustCount = 0;
                    ClsGlobal.ArrAdjustACIR = new double[ClsGlobal.TrayType];
                    InitFlag = 1;
                    mProcStep = 2;
                }

                if (ClsGlobal.Trans_State == eTransState.Init)
                {
                    InitFlag = 0;
                }


                switch (mProcStep)
                {
                    case 0:
                        mProcStep = 1;
                        break;
                    case 1:   //置OCV空闲状态                        
                        if ((ClsGlobal.OCV_TestState == eTestState.TestEnd && ClsGlobal.Trans_TrayLoc == eTrayLoc.NotInPlace) ||
                            (ClsGlobal.OCV_TestState == eTestState.GetData) ||
                            (ClsGlobal.OCV_TestState == eTestState.Idle &&
                            (ClsGlobal.Trans_TrayLoc == eTrayLoc.InPlace || ClsGlobal.Trans_TrayLoc == eTrayLoc.Pressing)))
                        {
                            ClsGlobal.OCV_TestState = eTestState.Idle;             //OCV空闲   
                            ClsGlobal.ServerSaveFinish = false;
                            ClsGlobal.listETCELL = null;
                            mProcStep = 2;
                            break;
                        }
                        break;
                    case 2:  //有申请进托盘情况
                        if (ClsGlobal.Trans_TrayLoc == eTrayLoc.Pressing && ClsGlobal.Trans_RequestTest == 1)
                        {
                            //界面显示清空
                            dgvTestData1.DataSource = null;
                            dgvTestData2.DataSource = null;

                            ClsGlobal.OCV_TestState = eTestState.Standby;               //测试就绪
                            mProcStep = 3;
                        }
                        break;
                    case 3: //托盘电池数据获取
                        if (ClsGlobal.Trans_State == eTransState.TestWork && ClsGlobal.OCV_TestState == eTestState.Standby && ClsGlobal.Trans_RequestTest == 0)  //输送设备的请求已清0
                        {
                            ClsGlobal.OCV_TestState = eTestState.GetData;               //获取数据    
                            string msn;



                            mProcStep = 4;
                        }
                        break;
                    case 4:  //进行测试
                        ClsGlobal.TestStartTime = DateTime.Now;                 //开始时间
                        ClsGlobal.OCV_TestState = eTestState.Testing;
                        mOCVIRTest.StartTestAction();                           //测试电压内阻

                        mProcStep = 5;
                        break;
                    case 5:  //数据处理

                        //测试异常
                        if (ClsGlobal.OCV_TestState == eTestState.ErrOCVTest)
                        {
                            string msn = "测试出错,取消保存数据!";
                            ShowMsn(msn);
                            mProcStep = 0;
                        }

                        if (mOCVIRTest.TestFinish == true)
                        {
                            //根据运行模式进行处理

                            //单机测试------------------------------------------------------
                            if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                            {
                                if (ClsGlobal.OCV_TestState == eTestState.TestOK)
                                {
                                    string msn = "单机测试OK，不保存数据库数据!";
                                    ShowMsn(msn);
                                    ClsGlobal.OCV_TestState = eTestState.OfflineTestEnd;
                                    //结束时间
                                    ClsGlobal.TestEndTime = DateTime.Now;
                                    //保存数据到CSV
                                    Task taskSaveCSVData = new Task(ExportDataToCSVForLIWei);
                                    taskSaveCSVData.Start();
                                    //保存过压电池码
                                    Task taskSaveHighVolt1 = new Task(ExportHighVoltToCSVForLiWei);
                                    taskSaveHighVolt1.Start();
                                    ExportLocalData();                                      //保存本地后结束
                                    ExportToLocFinish = true;
                                    ExportToServerFinish = true;
                                    ClsGlobal.ServerSaveFinish = true;
                                    mProcStep = 8;
                                }
                                else if (ClsGlobal.OCV_TestState == eTestState.TestAgain)
                                {
                                    mProcStep = 2;                                          //再测量
                                }
                            }
                            //联机测试------------------------------------------------------
                            else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                            {
                                //mProcStep = 6;
                                if (ClsGlobal.OCV_TestState == eTestState.TestOK)           //测试OK
                                {

                                    mProcStep = 6;
                                }
                                else if (ClsGlobal.OCV_TestState == eTestState.TestAgain)   //再测定
                                {
                                    mProcStep = 2;
                                }

                            }
                            //校准测试------------------------------------------------------
                            else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                            {
                                if (ClsGlobal.OCV_TestState == eTestState.AdjustEnd)
                                {
                                    string msn = "保存校准数据!";
                                    //ClsGlobal.WriteLogOCV(msn);
                                    ShowMsn(msn);

                                    //计算并保存校准补偿值
                                    SaveAdjustData();

                                    ClsGlobal.IRTrueValSetFlag = false;
                                    ClsGlobal.AdjustCount = 0;

                                    mProcStep = 8;
                                }
                                else if (ClsGlobal.OCV_TestState == eTestState.AdjustAgain)  //再校准
                                {
                                    mProcStep = 2;
                                }
                                return;
                            }
                        }
                        break;
                    case 6: //更新数据, 测试结束

                        strTemp = "保存数据...";
                        ShowMsn(strTemp);

                        //结束时间
                        ClsGlobal.TestEndTime = DateTime.Now;

                        //联机测试下保存
                        ExportToLocFinish = true;
                        ExportToServerFinish = true;


                        //保存数据到本地   
                        //Thread threadExportData = new Thread(new ThreadStart(ExportLocalData));
                        //threadExportData.IsBackground = true;
                        //threadExportData.Start();

                        Task taskSaveCSVD = new Task(ExportDataToCSVForLIWei);
                        taskSaveCSVD.Start();

                        //保存过压电池码
                        Task taskSaveHighVolt = new Task(ExportHighVoltToCSVForLiWei);
                        taskSaveHighVolt.Start();


                        mProcStep = 7;

                        break;
                    case 7: //保存完成                        
                        if (ExportToLocFinish == true && ExportToServerFinish == true)
                        {
                            ClsGlobal.WriteLog("测试完成", ClsGlobal.sDebugOCVSelectionPath);

                            ClsGlobal.OCV_TestState = eTestState.TestEnd;               //测量完毕
                            ClsGlobal.ServerSaveFinish = true;
                            mProcStep = 8;
                        }
                        break;
                    case 8:
                        if (ClsGlobal.Trans_State == eTransState.TrayTestFinish)
                        {
                            string msn = "本次测试完成";

                            ClsGlobal.WriteLog(msn, ClsGlobal.sDebugOCVSelectionPath);

                            ShowMsn(msn);
                            ClsGlobal.WriteLogOCV(msn);
                            ClsGlobal.OCV_TestState = eTestState.Idle;
                            mProcStep = 2;
                            //mProcStep = 9;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                TimeStopCheck = false;
                mProcStep = 0;
                ClsGlobal.AdjustCount = 0;
                ClsGlobal.WriteLogOCV(ex.Message);
                ClsGlobal.ErrMsg = ex.Message + ex.StackTrace;
                this.txtLog.Text = System.DateTime.Now.ToString() + ":" + ClsGlobal.ErrMsg + "\r\n" + this.txtLog.Text;
                ClsGlobal.OCV_TestState = eTestState.ErrOther;
            }
            finally
            {
                TimeStopCheck = false;
            }
        }


        public bool AskNGCheck(List<string> NGCodes)
        {
            string batcodes = "";
            foreach (var b in NGCodes)
            {
                if (string.IsNullOrEmpty(b))
                {
                    batcodes = b;
                }
                else
                {
                    batcodes = $"{batcodes},{b}";
                }
            }
            string mes = $"以下电池码NG\r\n{batcodes}\r\n是否复测?";
            if (MessageBox.Show(mes, "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        /// <summary>
        /// 判断一个数奇偶性，true：奇数,false：偶数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool IsOdd(int num)
        {
            return (num & 1) == 1;
        }



        //保存到本地CSV
        private void ExportDataToCSV_withKVal(string CSVPath)
        {
            try
            {
                string addr = CSVPath + "\\OCV" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1).ToString() + "_" + ClsGlobal.TrayCode +
                    "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                List<double> listVoltData = new List<double>();

                for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                {
                    listVoltData.Add(ClsGlobal.listETCELL[i].OCV_V1);
                }

                StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default);
                SWR.WriteLine("[OCVResult]");
                SWR.WriteLine("[ProcessInfo]");
                SWR.WriteLine("TrayNo=" + ClsGlobal.TrayCode);
                if (ClsGlobal.listETCELL[0].LastTest_EndTime != null)
                {
                    SWR.WriteLine("LastTestEnd=" + ClsGlobal.listETCELL[0].LastTest_EndTime);
                }
                else
                {
                    SWR.WriteLine("LastTestEnd=");
                }

                SWR.WriteLine("Start=" + ClsGlobal.TestStartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                SWR.WriteLine("End=" + ClsGlobal.TestEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                SWR.WriteLine("MinVolt=" + listVoltData.Min() + " " + "MaxVolt=" + listVoltData.Max());

                SWR.WriteLine("PosNo, Cell ID, OCV" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1).ToString() +
                ", ACIR" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1).ToString() + ", K Value, CODE,TEMP");    //Rev_OCV

                if (ClsGlobal.listETCELL == null)
                {
                    return;
                }

                //电压内阻
                for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                {
                    SWR.WriteLine(
                        ClsGlobal.listETCELL[i].Cell_Position.ToString() + "," +
                        ClsGlobal.listETCELL[i].Cell_ID.ToString() + "," +
                        ClsGlobal.listETCELL[i].OCV_V1.ToString("f1") + "," +
                        ClsGlobal.listETCELL[i].ACIR.ToString("f2") + " ," +
                        ClsGlobal.listETCELL[i].KVal.ToString("f2") + " ," +
                        ClsGlobal.listETCELL[i].CODE.ToString() // + "," +

                        //(ClsGlobal.listETCELL[i].TMP).ToString()
                        //+ "," +
                        //(ClsGlobal.listETCELL[i].OCV_V1 + (ClsGlobal.G_dbl_TempArr[i] - ClsGlobal.TempBase) *
                        //ClsGlobal.TempParaModify).ToString("f1")
                        );
                }

                SWR.Close();
                SWR = null;
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLogOCV("ExportDataToCSV:" + ex.Message);
                ClsGlobal.WriteLogOCV("ExportDataToCSV:" + ex.StackTrace);
                throw ex;
            }
        }

        //保存到本地CSV
        private void ExportDataToCSV(string CSVPath)
        {
            try
            {
                string addr = CSVPath + "\\OCV" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1).ToString() + "_" + ClsGlobal.TrayCode +
                    "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                List<double> listVoltData = new List<double>();

                for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                {
                    listVoltData.Add(ClsGlobal.listETCELL[i].OCV_V1);
                }

                StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default);
                SWR.WriteLine("[OCVResult]");
                SWR.WriteLine("[ProcessInfo]");
                SWR.WriteLine("TrayNo=" + ClsGlobal.TrayCode);
                SWR.WriteLine("Start=" + ClsGlobal.TestStartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                SWR.WriteLine("End=" + ClsGlobal.TestEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                SWR.WriteLine("MinVolt=" + listVoltData.Min() + " " + "MaxVolt=" + listVoltData.Max());
                SWR.WriteLine("PosNo, Cell ID, OCV" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1).ToString() +
                ",CASE OCV,ACIR" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1).ToString() + ",CODE,TEMP"/*,Rev_OCV"*/);

                if (ClsGlobal.listETCELL == null)
                {
                    return;
                }

                //电压内阻
                for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                {
                    SWR.WriteLine(
                        ClsGlobal.listETCELL[i].Cell_Position.ToString() + "," +
                        ClsGlobal.listETCELL[i].Cell_ID.ToString() + "," +
                        (ClsGlobal.listETCELL[i].OCV_V1).ToString("f1") + ", ," +
                        (ClsGlobal.listETCELL[i].ACIR).ToString("f2") + " ," +
                        (ClsGlobal.listETCELL[i].CODE).ToString() // + "," +

                        //(ClsGlobal.listETCELL[i].TMP).ToString()
                        //+ "," +
                        //(ClsGlobal.listETCELL[i].OCV_V1 + (ClsGlobal.G_dbl_TempArr[i] - ClsGlobal.TempBase) *
                        //ClsGlobal.TempParaModify).ToString("f1")
                        );
                }

                SWR.Close();
                SWR = null;
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLogOCV("ExportDataToCSV:" + ex.Message);
                ClsGlobal.WriteLogOCV("ExportDataToCSV:" + ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// liwei结果数据导出
        /// </summary>
        private void ExportDataToCSVForLIWei()
        {
            string fileName = $"{ClsGlobal.TrayCode}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            string secDir = ClsProcessSet.WorkingProcess.Type == 0 ? "OCV1" : "OCV2";
            fileName = $"{secDir}-{fileName}";
            string saveDir = $"{ClsSysSetting.SysSetting.EndDataSavePath}\\{secDir}";
            string saveBackupFile = $"D:\\OCV测试数据\\{secDir}\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00");
            string backupFileName = Path.Combine(saveBackupFile, fileName);
            string title = "卡位号,托盘号,电芯条码,电压,内阻,时间,DCR,温度,设备编号,操作员工";
            string strData = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}";
            #region 改用Excel格式保存
            //try
            //{


            //    if (!Directory.Exists(saveDir))
            //    {
            //        Directory.CreateDirectory(saveDir);
            //    }
            //    fileName = Path.Combine(saveDir, fileName);
            //    StreamWriter SWR = new StreamWriter(fileName, false, Encoding.UTF8);
            //    SWR.WriteLine(title);
            //    foreach (var d in ClsGlobal.listETCELL)
            //    {
            //        string srd = string.Format(strData,
            //             d.Cell_Position + 1,
            //             d.Pallet_ID,
            //             d.Cell_ID,
            //             d.OCV_V1,
            //             d.ACIR,
            //             d.StartTime,
            //             "",
            //             d.TMP,
            //             ClsSysSetting.SysSetting.DeviceCode,
            //             ClsGlobal.UserInfo.UserCode
            //            );
            //        SWR.WriteLine(srd);
            //    }
            //    SWR.Close();
            //    if (!Directory.Exists(saveBackupFile))
            //    {
            //        Directory.CreateDirectory(saveBackupFile);
            //    }
            //    File.Copy(fileName,backupFileName);
            //}
            //catch (Exception ex)
            //{

            //}
            #endregion
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks workbooks;
                if (excelApp == null)
                {
                    throw new Exception("创建Excel文件失败,本机未安装Excel软件.");
                }
                workbooks = excelApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                string[] arrTitle = title.Split(',');
                for (int i = 0; i < arrTitle.Length; i++)
                {
                    worksheet.Cells[1, i + 1] = arrTitle[i];
                }
                int rows = 2;
                foreach (var d in ClsGlobal.listETCELL)
                {
                    int colunm = 1;
                    worksheet.Cells[rows, colunm] = d.Cell_Position + 1;
                    colunm++;
                    worksheet.Cells[rows, colunm] = d.Pallet_ID;
                    colunm++;
                    worksheet.Cells[rows, colunm] = d.Cell_ID;
                    colunm++;
                    worksheet.Cells[rows, colunm] = d.Cell_ID.StartsWith("000") ? 0 : d.OCV_V1;
                    colunm++;
                    worksheet.Cells[rows, colunm] = d.Cell_ID.StartsWith("000") ? 0 : d.ACIR;
                    colunm++;
                    worksheet.Cells[rows, colunm] = d.StartTime;
                    colunm++;
                    worksheet.Cells[rows, colunm] = "";
                    colunm++;
                    worksheet.Cells[rows, colunm] = d.TMP.ToString("0.0");
                    colunm++;
                    worksheet.Cells[rows, colunm] = ClsSysSetting.SysSetting.DeviceCode;
                    colunm++;
                    worksheet.Cells[rows, colunm] = ClsGlobal.UserInfo.UserCode;
                    colunm++;
                    rows++;
                }
                worksheet.Columns.EntireColumn.AutoFit();
                if (!Directory.Exists(saveBackupFile))
                {
                    Directory.CreateDirectory(saveBackupFile);
                }
                if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                {
                    fileName = backupFileName;
                }
                else
                {
                    fileName = Path.Combine(saveDir, fileName);
                }
                workbook.SaveAs(fileName);
                KillExcel(excelApp);
                if (ClsGlobal.OCV_RunMode != eRunMode.OfflineTest)
                {
                    File.Copy(fileName, backupFileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存Excel失败:{ex.Message}");
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public void KillExcel(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            IntPtr t = new IntPtr(excelApp.Hwnd); //得到这个句柄，具体作用是得到这块内存入口 
            int k = 0;
            GetWindowThreadProcessId(t, out k); //得到本进程唯一标志k
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); //得到对进程k的引用
            p.Kill(); //关闭进程k
        }

        private void ExportHighVoltToCSVForLiWei()
        {
            string saveDir = @"D:\HIGHVOLT";
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            if (ClsProcessSet.WorkingProcess == null)
            {
                return;
            }
            var lst = ClsGlobal.listETCELL.Where(a => a.OCV_V1 > ClsProcessSet.WorkingProcess.WarningV).ToList();
            foreach (var d in lst)
            {
                string fileName = $"{d.Cell_ID}.csv";
                fileName = Path.Combine(saveDir, fileName);
                File.WriteAllText(fileName, d.Cell_ID);
            }
        }
        private void ShowMsn(string sloginfo)
        {
            this.txtLog.Text += System.DateTime.Now + ": " + sloginfo + "\r\n";
            txtLog.Select(txtLog.TextLength, 0);
            txtLog.ScrollToCaret();
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {

            ClearDataGrid();
            DTStart = DateTime.Now;

            try
            {
                mOCVIRTest.StartManualTestAction();
            }
            catch
            {
                MessageBox.Show("读取电压值失败");
            }


            DTStop = DateTime.Now;
        }

        private void ClearDataGrid()
        {
            dgvManualTest.Rows.Clear();
            int Val = 0;
            Val = ClsGlobal.TrayType;
            dgvManualTest.Rows.Add(Val);
            for (int i = 0; i < Val; i++)
            {
                dgvManualTest.Rows[i].Cells[0].Value = i + 1;
                dgvManualTest.Rows[i].Cells[1].Value = i / 16 + 1;          //列
                dgvManualTest.Rows[i].Cells[2].Value = i % 16 + 1;          //行
            }
        }

        private void ClearDataGridACIR()
        {
            int Val = 0;
            Val = ClsGlobal.TrayType;
            for (int i = 0; i < Val; i++)
            {
                dgvManualTest.Rows[i].Cells[3].Value = null;
            }
        }

        private void btnTestVolt_DMM_Click(object sender, EventArgs e)
        {
            try
            {
                double Val;
                mOCVIRTest.mDmm.ReadVolt(out Val);
                txtVolt_DMM.Text = (Val * 1000).ToString("F1");
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取测试值失败:" + ex.Message.ToString());
            }
        }

        private void btnInit_DMM_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 判断输入是否为数字
        /// </summary>
        /// <param name="oInput"></param>
        /// <returns></returns>
        private bool IsNumberic(string oInput)
        {
            try
            {
                double var = Convert.ToDouble(oInput);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }





        #region
        private void btTrayClose_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TrayClose();
        }
        private void btTrayOpen_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TrayOpen();
        }
        private void btTrayDown_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TrayDown();
        }
        #endregion

        private void btTrayUp_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TrayUp();
        }

        private void btTrayIn_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TrayIn();
        }

        private void btTrayOut_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_TrayOut();
        }

        private void btPress_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_PBPress();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_PBOpen();
        }

        int mRedflag;
        private void btUpRed_Click(object sender, EventArgs e)
        {
            if (mRedflag == 0)
            {
                ClsGlobal.mIOControl.Set_TowerRedLight_On();
                mRedflag = 1;
            }
            else
            {
                ClsGlobal.mIOControl.Set_TowerRedLight_Off();
                mRedflag = 0;
            }
        }
        int mOrangeflag;
        private void btUpOrange_Click(object sender, EventArgs e)
        {
            if (mOrangeflag == 0)
            {
                ClsGlobal.mIOControl.Set_TowerOrangeLight_On();
                mOrangeflag = 1;
            }
            else
            {
                ClsGlobal.mIOControl.Set_TowerOrangeLight_Off();
                mOrangeflag = 0;
            }
        }
        int mGreenflag;
        private void btUpGreen_Click(object sender, EventArgs e)
        {
            if (mGreenflag == 0)
            {
                ClsGlobal.mIOControl.Set_TowerGreenLight_On();
                mGreenflag = 1;
            }
            else
            {
                ClsGlobal.mIOControl.Set_TowerGreenLight_Off();
                mGreenflag = 0;
            }
        }
        int mBuzzerflag;
        private void btUpBuzzer_Click(object sender, EventArgs e)
        {
            if (mBuzzerflag == 0)
            {
                ClsGlobal.mIOControl.Set_TowerBusser_On();
                mBuzzerflag = 1;
            }
            else
            {
                ClsGlobal.mIOControl.Set_TowerBusser_Off();
                mBuzzerflag = 0;
            }
        }

        private void lbTrayDown_Click(object sender, EventArgs e)
        {

        }

        private void label74_Click(object sender, EventArgs e)
        {

        }



        private void dgvTestData2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmBateryTestView frmBateryTestView = new FrmBateryTestView();
            frmBateryTestView.Show();
        }
    }

}