using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClsDeviceComm.LogNet;

namespace OCV
{
    public class ClsOCVTestData
    {
     
        public enum eTestOCVState
        {
            Init= 0,
            StartTestOCV = 1,          //正常OCV测试
            StartTestSV = 2,          //正常壳压测试
            StartTest = 3,            //OCV和壳压同步测试
            TestOK = 4,            //测试结束
            StopTest = 7,           //停止测试
            ErrTest = 20,           //测试异常
        }
        FrmSys mForm;
        private ClsDMM_Ag344X DMM_Ag344X;              //万用表
        private ClsSWControl SWControl;          //切换控制
     
        private Thread ThreadTestAction;
        private int ChannelCount;             //测试通道数
        private int CHindex;                  //测试通道数偏移量
        public ClsDataModel.C_CellRealData[] mOcvRealData;   // 电池实时数据
        private eTestOCVState mTestOCVState;       //流程状态标识
        public eTestOCVState TestOCVState { get { return mTestOCVState; } set { mTestOCVState = value; } }

        private bool mStopTestOCV;
        public bool StopTestOCV { get { return mStopTestOCV; } set { mStopTestOCV = value; } }
        private ILogNet OCVTestlogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\OCV\\OCVTestlog", GenerateMode.ByEveryDay);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swControl">切换版对象</param>
        /// <param name="dMM_Ag344X">万用表</param>
        /// <param name="channelcount">测试通道数</param>
        /// <param name="CHindex">通道偏移量</param>
        /// <param name="Fm">界面对象</param>
        /// <param name="DebugLog">是否启用调试日志</param>
        public ClsOCVTestData(ClsSWControl swControl, ClsDMM_Ag344X dMM_Ag344X,int channelcount, int chindex ,FrmSys Fm, int DebugLog =0)
        {
            this.CHindex = chindex;
            this.mForm = Fm;
            this.mTestOCVState = eTestOCVState.StopTest;
            this.SWControl = swControl;
            this.DMM_Ag344X = dMM_Ag344X;
            this.ChannelCount = channelcount;
            this.mOcvRealData = new ClsDataModel.C_CellRealData[this.ChannelCount];
            for (int i = 0; i < this.ChannelCount; i++)
            {
                this.mOcvRealData[i] = new ClsDataModel.C_CellRealData();
            }
         
            if (DebugLog == 1)
            {
                OCVTestlogNet.SetMessageDegree(ClsMessageDegree.DEBUG);
            }
            else
            {
                OCVTestlogNet.SetMessageDegree(ClsMessageDegree.INFO);
            }
            ThreadTestAction = new Thread(new ThreadStart(TestOCV));
            ThreadTestAction.IsBackground = true;
            ThreadTestAction.Start();
        }
   
        private void TestOCV()
        {
            while (true)
            {
                try
                {
                    double theDMMVolt = 0;
                    switch (this.mTestOCVState)
                    {
                        
                        case eTestOCVState.StartTestOCV:
                           
                            SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断

                            for (int Num = 0; Num < this.ChannelCount; Num++)
                            {
                                if (mStopTestOCV == true)
                                {
                                    this.SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断
                                    this.mTestOCVState = eTestOCVState.StopTest;
                                    break;
                                }
                                this.SWControl.ChannelVoltSwitch(1, Num + 1);     //电压测量
                                Thread.Sleep(40);
                                this.DMM_Ag344X.ReadVolt(out theDMMVolt);

                                //OCV数据

                                if (Math.Abs(theDMMVolt * 1000) < 1e+6)
                                {
                                    this.mOcvRealData[Num].OCV_Now = Math.Round(theDMMVolt * 1000, 4);
                                }
                                else
                                {
                                    this.mOcvRealData[Num].OCV_Now = 0;
                                }

                                #region 显示数据
                                if (mForm.IsHandleCreated == true)
                                {
                                    mForm.Invoke(new EventHandler(delegate
                                    {
                                        mForm.dgvTest.Rows[this.CHindex + Num].Cells["Col_OCV"].Value = this.mOcvRealData[Num].OCV_Now.ToString("F4");
                                    }
                                    ));
                                }
                                #endregion
                            }
                            this.mTestOCVState = eTestOCVState.TestOK;
                            break;
                        case eTestOCVState.StartTestSV:

                            SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断

                            for (int Num = 0; Num < this.ChannelCount; Num++)
                            {
                                if (mStopTestOCV == true)
                                {
                                    this.SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断
                                    this.mTestOCVState = eTestOCVState.StopTest;
                                    break;
                                }
                                this.SWControl.ChannelVoltSwitch(1, Num + 1);     //壳压测量
                                Thread.Sleep(40);
                                this.DMM_Ag344X.ReadVolt(out theDMMVolt);
                               
                                //OCV数据
                                if (Math.Abs(theDMMVolt * 1000) < 1e+6)
                                {
                                    this.mOcvRealData[Num].Negative_Shell = Math.Round(theDMMVolt * 1000, 4);
                                }
                                else
                                {
                                    this.mOcvRealData[Num].Negative_Shell = 0;
                                }

                                #region 显示数据
                                if (mForm.IsHandleCreated == true)
                                {
                                    mForm.Invoke(new EventHandler(delegate
                                    {
                                        mForm.dgvTest.Rows[this.CHindex + Num].Cells["Col_CaseOCV"].Value = this.mOcvRealData[Num].Negative_Shell.ToString("F4");
                                    }
                                    ));
                                }
                                #endregion
                            }
                            this.mTestOCVState = eTestOCVState.TestOK;
                            break;
                        case eTestOCVState.StartTest:
                        case eTestOCVState.TestOK:
                        case eTestOCVState.StopTest:
                        case eTestOCVState.ErrTest:
                        case eTestOCVState.Init:
                            break;
                        default:

                            break;
                    }

                    #region  测试结束，判断
                    #endregion
                }
                catch (Exception ex)
                {
                    //测试异常   
                    this.mTestOCVState = eTestOCVState.ErrTest;
                    OCVTestlogNet.WriteWarn("OCV测试异常",ex.ToString());
                }
                finally
                {
                    Thread.Sleep(200);
                }
            }
           
        }
    }
}
