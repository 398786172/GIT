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
using OCV.OCVTest;
using System.Threading.Tasks;

namespace OCV
{
    public partial class FrmMonitor : Form
    {
        XmlDocument xmlDoc = new XmlDocument();
        ClsProcess mProc;
        delegate void RefreshText(string Info);
        RefreshText RefreshTxt;             //刷新实时信息
        RefreshText RefreshTxtAlarm;        //刷新报警信息

        delegate void delegateViewLog(string mes);
        event delegateViewLog OnLogView;
        

        ushort manualAlarmRecord;    //保存报警记录
        ushort ocvAlarmRecord;

        int Val1;

        bool bVal;

        public System.Windows.Forms.TextBox TxtTrayCodeScan
        {
            get { return txtTrayCodeScan; }
        }

            

        public FrmMonitor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //处理流程
                mProc = new ClsProcess(InfoHandle, GetScanCode,this);
                mProc.Stop();

                manualAlarmRecord = 0;
                ocvAlarmRecord = 0;

                //界面刷新
                btn_Start.Enabled = true;
                btn_Pause.Enabled = false;
                btn_Stop.Enabled = false;

                lbl_Stop.BackColor = Color.Red;
                lbl_Run.BackColor = Color.LightGray;
                lbl_Pause.BackColor = Color.LightGray;

                tim_run_M.Interval = 200;
                tim_run_M.Start();

                tim_UI_M.Interval = 200;
                tim_UI_M.Start();

                tim_ClearMsn.Interval = 120000;
                tim_ClearMsn.Start();

                RefreshTxt = new RefreshText(mRefreshText);
                RefreshTxtAlarm = new RefreshText(mRefreshTextAlarm);

                ClsGlobal.WriteLog("程序打开", ClsGlobal.sLogspath);

                InitTempView();
                Thread.Sleep(1*1000);
                tabControl1.SelectedTab = tabPage_TrayView;

                Task taskReflashProcess = new Task(ReflashProcess);
                taskReflashProcess.Start();
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog(ex.Message);
                MessageBox.Show(ex.ToString());
            }

        }

       

        void InitTempView() {
            try
            {
                FormTempView FormTempView = new FormTempView( );
                FormTempView.TopLevel = false;
                FormTempView.Parent = tabPage_Temp;
                FormTempView.FormBorderStyle = FormBorderStyle.None;
                FormTempView.Dock = DockStyle.Fill;
                FormTempView.Show();
                FormTempView.BringToFront();

                FrmBateryTestView frmBateryTestView = new FrmBateryTestView();
                frmBateryTestView.TopLevel = false;
                frmBateryTestView.Parent = tabPage_TrayView;
                frmBateryTestView.FormBorderStyle = FormBorderStyle.None;
                frmBateryTestView.Dock = DockStyle.Fill;
                frmBateryTestView.Show();
                frmBateryTestView.BringToFront();
                OnLogView += frmBateryTestView.LogInfo;

                tabControl1.SelectedTab = tabPage_Temp;
                Thread.Sleep(1*1000); //留时间给界面内容加载
                tabControl1.SelectedTab = tabPage_TrayView;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"加载温度监控组件出错:{ex.Message}");
            }
        }

        string temTrayCodeValue = "";
        public string GetTrayCode()
        {
            return txtTrayCodeScan.Text;
        }

       

        public void SetTrayCode( string value)
        {
            temTrayCodeValue = value;
        }

        //信息记录处理
        private void InfoHandle(string info)
        {
            if (this.IsHandleCreated == true)
            {
                this.BeginInvoke(RefreshTxt, info);
                ClsGlobal.WriteLog(info, ClsGlobal.sLogspath);       //详细日志

                if (info.Contains("异常") || info.Contains("报警") || info.Contains("超时") || info.Contains("错误"))
                {
                    this.BeginInvoke(RefreshTxtAlarm, info);
                    ClsGlobal.WriteLog(info, ClsGlobal.sLogAlarmpath);  //报警日志
                }
            }
        }

        private void DebugInfoHandle(string info)
        {
            if (this.IsHandleCreated == true)
            {
                this.BeginInvoke(RefreshTxt, info);
                ClsGlobal.WriteLog(info, ClsGlobal.sLogspath);       //详细日志

                if (info.Contains("异常") || info.Contains("报警") || info.Contains("超时") || info.Contains("错误"))
                {
                    this.BeginInvoke(RefreshTxtAlarm, info);
                    ClsGlobal.WriteLog(info, ClsGlobal.sLogAlarmpath);  //报警日志
                }
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

        public void FrmStopRun()
        {
            tim_run_M.Stop();
            tim_UI_M.Stop();
            mProc.Stop();
            ClsGlobal.CloseProg = true;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                FrmStopRun();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        #region 信息界面

        //刷新信息界面
        private void mRefreshText(string info)
        {
            OnLogView?.Invoke(info);
            txtInfo.Text += System.DateTime.Now + ":  " + info + "\r\n";
            //txtInfo.Focus();
            txtInfo.Select(txtInfo.TextLength, 0);
            txtInfo.ScrollToCaret();
        }

        //刷新报警界面
        private void mRefreshTextAlarm(string info)
        {
            txtAlarmInfo.Text += System.DateTime.Now + ":  " + info + "\r\n";
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;

            //判断
            if (ClsGlobal.ID_InitOK == false)
            {
                MessageBox.Show("初始化失败，无法启动");
                return;
            }

            //启动线程
            mProc.StartAction();
            btn_Start.Enabled = false;
            btn_Pause.Enabled = true;
            btn_Stop.Enabled = true;

            lbl_Run.BackColor = Color.LightGreen;
            lbl_Stop.BackColor = Color.LightGray;
            lbl_Pause.BackColor = Color.LightGray;
            
            //初始化
            ClsGlobal.ArrAdjustACIR = new double[ClsGlobal.TrayType];

        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            btn_Pause.Enabled = false;
            Thread.Sleep(100);

            if (mProc.RunState != 0)
            {
                if (btn_Pause.Text == "暂停")
                {
                    mProc.Pause();
                    btn_Pause.Text = "继续";
                    //lbl_Pause.BackColor = Color.Yellow;
                    //lbl_Run.BackColor = Color.LightGray;
                    mRefreshText("程序暂停中...");
                }
                else if (btn_Pause.Text == "继续")
                {
                    mProc.Resume();
                    btn_Pause.Text = "暂停";
                    //lbl_Pause.BackColor = Color.LightGray;
                    //lbl_Run.BackColor = Color.LightGreen;
                    mRefreshText("程序继续运行...");
                }
            }
            Thread.Sleep(100);
            btn_Pause.Enabled = true;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            mProc.Stop();
            mProc.AlarmReset();
            btn_Start.Enabled = true;
            btn_Pause.Enabled = false;
            lbl_Stop.BackColor = Color.Red;
            lbl_Run.BackColor = Color.LightGray;
        }

        private void tim_run_M_Tick(object sender, EventArgs e)
        {

            if (ClsGlobal.Trans_State == eTransState.TrayIn && bVal == true)
            {
                txtTrayCodeScan.Focus();
                bVal = false;
            }
            else if (ClsGlobal.Trans_State == eTransState.Init
                || ClsGlobal.Trans_State == eTransState.Ready ||
                ClsGlobal.Trans_State == eTransState.TestWork)
            {
                txtTrayCodeScan.Text = "";
                bVal = true;
            }

        }

        private void btnClearAlarm_Click(object sender, EventArgs e)
        {
            ClsGlobal.mIOControl.Set_ReqClearAlarm(1);
        }
        
        private void btnClearInfoText_Click(object sender, EventArgs e)
        {
            txtInfo.Clear();
        }

        private void tim_UI_M_Tick(object sender, EventArgs e)
        {
            short Val;

            if (string.IsNullOrEmpty(temTrayCodeValue) && temTrayCodeValue!=txtTrayCodeScan.Text)
            {
                txtTrayCodeScan.Text = temTrayCodeValue;
            }

            if (mProc.RunState == 0)    //停止状态
            {
                try
                {
                    lbl_Run.BackColor = Color.LightGray;
                    lbl_Pause.BackColor = Color.LightGray;
                    lbl_Stop.BackColor = Color.Red;


                    btn_Start.Enabled = true;
                    btn_Pause.Enabled = false;
                    btn_Stop.Enabled = false;
                }
                catch (System.Exception err)
                {
                    ClsGlobal.WriteLog(err.Message);
                }

            }
            else if (mProc.RunState == 1)   //运行状态
            {
                try
                {
                    lbl_Run.BackColor = Color.LightGreen;
                    lbl_Pause.BackColor = Color.LightGray;
                    lbl_Stop.BackColor = Color.LightGray;

                    btn_Pause.Text = "暂停";
                    btn_Start.Enabled = false;
                    btn_Pause.Enabled = true;
                    btn_Stop.Enabled = true;
                }
                catch (System.Exception err)
                {
                    ClsGlobal.WriteLog(err.Message);
                }
                
            }
            else if (mProc.RunState == 2)    //暂停状态
            {
                try
                {
                    lbl_Run.BackColor = Color.LightGray;
                    lbl_Pause.BackColor = Color.Yellow;
                    lbl_Stop.BackColor = Color.LightGray;

                    btn_Start.Enabled = false;
                    btn_Pause.Enabled = true;
                    btn_Stop.Enabled = true;
                    btn_Pause.Text = "继续";
                }
                catch (System.Exception err)
                {
                    ClsGlobal.WriteLog(err.Message);
                }
                
            }
            else if (mProc.RunState == 3)    //报警状态
            {
                try
                {
                    lbl_Run.BackColor = Color.LightGray;
                    lbl_Pause.BackColor = Color.LightGray;
                    lbl_Stop.BackColor = Color.LightGray;

                    btn_Start.Enabled = false;
                    btn_Pause.Enabled = false;
                    btn_Stop.Enabled = true;
                }
                catch (System.Exception err)
                {
                    ClsGlobal.WriteLog(err.Message);
                }
            }

        }

        private void btnClearAlarmInfo_Click(object sender, EventArgs e)
        {
            txtAlarmInfo.Clear();
        }

        #endregion
              

        #region 日志界面

        private void btnShowDaily_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFILE = new OpenFileDialog();
            oFILE.InitialDirectory = Application.StartupPath + "\\log";
            oFILE.Filter = "文本文件|*.txt|所有文件|*.*";
            oFILE.RestoreDirectory = true;
            oFILE.FilterIndex = 1;
            if (oFILE.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(oFILE.FileName);
            }
        }

        #endregion


        private void panel2_Resize(object sender, EventArgs e)
        {
            btnClearInfoText.Left = panel2.Width - btnClearInfoText.Width - 20;
        }

        private void panel3_Resize(object sender, EventArgs e)
        {
            btnClearAlarmInfo.Left = panel3.Width - btnClearAlarmInfo.Width - 20;
        }

        private void tim_ClearMsn_Tick(object sender, EventArgs e)
        {
            if (txtInfo.Lines.Count() > 80)
            {
                txtInfo.Clear();
            }

            if (txtAlarmInfo.Lines.Count() > 40)
            {
                txtAlarmInfo.Clear();
            }
        }


        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnErrClear_Click(object sender, EventArgs e)
        {
            //0803 HWJ 将内容移到复位按钮
            //0626,zxz
            //try
            //{
            //    short sVal;
            //    mPLCContr.WriteDB(ClsDevAddr.PC_复位Reset, 1);
            //    Thread.Sleep(1000);
            //    mPLCContr.ReadDB(ClsDevAddr.PLC_复位应答, out sVal);
            //    if (sVal == 1)
            //    {
            //        mRefreshText("异常状态清零");
            //        mPLCContr.WriteDB(ClsDevAddr.PC_复位Reset, 0);
            //        manualAlarmRecord = 0;
            //        ocvAlarmRecord = 0;
            //        txtAlarmInfo.Clear();
            //    }
            //    //ClsGlobal.mAlarmFlag = false;
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
            //0626,zxz
            //0803 HWJ
        }

        private void tim_err_Tick(object sender, EventArgs e)
        {
            //0803 HWJ 移到线程mErrCheckThread
            //0626,zxz,检测PLC异常报警
            //try
            //{
            //    short AlarmTemp2;
            //    short OcvAlarm, ManualAlarm;
            //    mPLCContr.ReadDB(ClsDevAddr.PLC_设备状态, out AlarmTemp2);

            //    mPLCContr.ReadDB(ClsDevAddr.PLC_OCV异常, out OcvAlarm);
            //    mAlarmInfo.GetOCVAlarmInfo(OcvAlarm);
            //    mPLCContr.ReadDB(ClsDevAddr.PLC_Manual异常, out ManualAlarm);
            //    mAlarmInfo.GetManualAlarmInfo(ManualAlarm);

            //    if ((OcvAlarm != 0 || ManualAlarm != 0 || AlarmTemp2 == 8 || AlarmTemp2 == 9) && ClsGlobal.mAlarmFlag == false)
            //    //显示报警信息   
            //    {
            //        if (OcvAlarm != 0 && OcvAlarm!=ocvAlarmRecord)
            //        {
            //            for (int i = 0; i < mAlarmInfo.OCVAlarmValue.Length; i++)
            //            {
            //                if (mAlarmInfo.OCVAlarmValue[i] == 1 && mAlarmInfo.CheckBit16(ocvAlarmRecord, i) == 0)
            //                {
            //                    //mRefreshTextAlarm("异常:" + mAlarmInfo.OCVAlarm[i]);
            //                    InfoHandle("异常:" + mAlarmInfo.OCVAlarm[i]);
            //                    mAlarmInfo.SetAlarmBit(i, ref ocvAlarmRecord);
            //                }
            //            }
            //        }
            //        if (ManualAlarm != 0 && ManualAlarm!=manualAlarmRecord)
            //        {
            //            for (int i = 0; i < mAlarmInfo.ManualAlarmValue.Length; i++)
            //            {
            //                if (mAlarmInfo.ManualAlarmValue[i] == 1 && mAlarmInfo.CheckBit16(manualAlarmRecord,i)==0)
            //                {
            //                    InfoHandle("异常:" + mAlarmInfo.ManualAlarm[i]);
            //                    mAlarmInfo.SetAlarmBit(i, ref manualAlarmRecord);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (System.Exception err)
            //{
            //    InfoHandle("异常:" + err.Message);
            //}
            //0626,zxz
            //0803 HWJ
        }

        private void btnTrayCodeEnter_Click(object sender, EventArgs e)
        {

        }

        void ReflashProcess()
        {
            while (true)
            {
                if (this.IsHandleCreated)
                {
                    Invoke(new EventHandler((ojb, e) => {
                        label10.Text = $"当前登录账号[{ClsGlobal.UserInfo.UserCode}]";

                    }));
                    System.Threading.Thread.Sleep(5 * 1000);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ClsGlobal.NontUsingScaner = checkBox1.Checked;
        }

        private void chkFCT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFCT.Checked)
            {
                ClsGlobal.ISFCT = true;
            }
            else
            {
                ClsGlobal.ISFCT = false;
            }
        }
    }
}
