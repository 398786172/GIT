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
    public class ClsACIRTestData
    {
     
        public enum eTestACIRState
        {
            Init=-1,
            StartTest = 1,          //正常ACIR测试
            TestAgain = 2,          //再测试
            StartTestAgain = 3,     //开始再测试
            TestOK = 4,            //测试结束
            StopTest = 7,           //停止测试
            ErrTest = 20          //测试异常
        }
        FrmSys mForm;
        private ClsSWControl SWControl;          //切换控制
        private ClsHIOKI365X HIOKI365X;       //内阻仪BT356x控制
        private ClsHIOKI4560 HIOKI4560;       //内阻仪BT4560控制
        private Thread ThreadTestAction;
        private int ChannelCount;             //测试通道数
        private readonly int Chindex;
        public List<int> lstACIRErrNo = new List<int>();      //内阻测量失败电池号,从0开始计算
        public ClsDataModel.C_CellRealData[] mAcirRealData;   // 电池实时数据
        public int TestCount=0;                            //测试计数
        public int MaxTestNum = 1;                       //测试最大次数 (正常只测一次,有问题需重测,直到最大测量次数)

        private eTestACIRState mTestACIRState;       //流程状态标识
        public eTestACIRState TestACIRState { get { return mTestACIRState; } set { mTestACIRState = value; } }

        private bool mStopTestAcir;
        public bool StopTestAcir { get { return mStopTestAcir; } set { mStopTestAcir = value; } }
        private ILogNet ACIRTestlogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\OCV\\OCVTestlog", GenerateMode.ByEveryDay);
       

        public ClsACIRTestData(ClsSWControl swControl, ClsHIOKI365X hioki365X,int channelcount, int chindex, FrmSys Fm, int DebugLog =0)
        {
            this.mForm = Fm;
            this.mTestACIRState = eTestACIRState.StopTest;
            this.SWControl = swControl;
            this.HIOKI365X = hioki365X;
            this.ChannelCount = channelcount;
            this.Chindex = chindex;
            this.mAcirRealData = new ClsDataModel.C_CellRealData[this.ChannelCount];
            if (DebugLog == 1)
            {
                ACIRTestlogNet.SetMessageDegree(ClsMessageDegree.DEBUG);
            }
            else
            {
                ACIRTestlogNet.SetMessageDegree(ClsMessageDegree.INFO);
            }
            ThreadTestAction = new Thread(new ThreadStart(TestAcir));
            ThreadTestAction.IsBackground = true;
            ThreadTestAction.Start();
        }
        public ClsACIRTestData(ClsSWControl swControl,ClsHIOKI4560 hioki4560, int channelcount, int chindex, FrmSys Fm, int DebugLog = 0)
        {
            this.Chindex = chindex;
            this.mForm = Fm;
            this.mTestACIRState = eTestACIRState.StopTest;
            this.SWControl = swControl;
            this.HIOKI4560 = hioki4560;
            this.ChannelCount = channelcount;
            this.mAcirRealData = new ClsDataModel.C_CellRealData[this.ChannelCount];
            if (DebugLog == 1)
            {
                ACIRTestlogNet.SetMessageDegree(ClsMessageDegree.DEBUG);
            }
            else
            {
                ACIRTestlogNet.SetMessageDegree(ClsMessageDegree.INFO);
            }
            ThreadTestAction = new Thread(new ThreadStart(TestAcir));
            ThreadTestAction.IsBackground = true;
            ThreadTestAction.Start();
        }
        private void TestAcir()
        {
            while (true)
            {
                try
                {
                    switch (this.mTestACIRState)
                    {
                        case eTestACIRState.StartTest:

                            double theIRSample = 0;
                            SWControl.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断

                            for (int Num = 0; Num < this.ChannelCount; Num++)
                            {
                                if (mStopTestAcir == true)
                                {
                                    this.SWControl.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断
                                    this.mTestACIRState = eTestACIRState.StopTest;
                                    break;
                                }
                                this.SWControl.ChannelAcirSwitchContr(2, Num + 1);     //内阻测量

                                Thread.Sleep(40);

                                this.HIOKI4560.ReadRData(out theIRSample);
                                //内阻数据
                                if (0 < theIRSample * 1000 && theIRSample * 1000 < ClsGlobal.ReTestLmt_ACIR)
                                {
                                    //内阻数据
                                    mAcirRealData[Num].ACIR_Now = Math.Round(theIRSample * 1000, 4);
                                }
                                else
                                {
                                    lstACIRErrNo.Add(Num);
                                    mAcirRealData[Num].ACIR_Now = 99999;
                                }

                                #region 显示数据
                                if (mForm.IsHandleCreated == true)
                                {
                                    mForm.Invoke(new EventHandler(delegate
                                    {
                                        mForm.dgvTest.Rows[this.Chindex + Num].Cells["Col_ACIR"].Value = mAcirRealData[Num].ACIR_Now.ToString("F4");
                                    }
                                    ));
                                }
                                #endregion
                            }
                            this.mTestACIRState = eTestACIRState.TestOK;
                            break;
                        case eTestACIRState.StartTestAgain:

                            #region 复测  仅把第一次测量失败的进行更新

                            int lstErrNo = lstACIRErrNo.Count;
                            SWControl.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断

                            for (int i = 0; i < lstErrNo; i++)
                            {
                                if (mStopTestAcir == true)
                                {
                                    this.SWControl.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断
                                    this.mTestACIRState = eTestACIRState.StopTest;
                                    break;
                                }

                                int item = lstACIRErrNo[i];
                                SWControl.ChannelAcirSwitchContr(2, item + 1);     //内阻测量

                                Thread.Sleep(100);
                                HIOKI4560.ReadRData(out theIRSample);
                                //内阻数据
                                if (0 < theIRSample * 1000 && theIRSample * 1000 < ClsGlobal.ReTestLmt_ACIR)
                                {
                                    //内阻数据

                                    mAcirRealData[item].ACIR_Now = Math.Round(theIRSample * 1000, 4);
                                }
                                else
                                {
                                    lstACIRErrNo.Add(item);
                                    mAcirRealData[item].ACIR_Now = 99999;
                                }

                                #region 显示数据
                                if (mForm.IsHandleCreated == true)
                                {
                                    mForm.Invoke(new EventHandler(delegate
                                    {
                                        mForm.dgvTest.Rows[this.Chindex + item].Cells["Col_ACIR"].Value = mAcirRealData[item].ACIR_Now.ToString("F4");
                                    }
                                    ));
                                }
                                #endregion
                            }
                            this.mTestACIRState = eTestACIRState.TestOK;
                            #endregion
                            break;
                        case eTestACIRState.TestAgain:
                            break;
                        case eTestACIRState.TestOK:

                            #region  测试结束，判断是否复测
                            SWControl.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断
                                                                         //测试次数判断
                            TestCount++;
                            //ACIR有异常测试数据,重测
                            if (lstACIRErrNo.Count > 0 && TestCount < MaxTestNum)
                            {
                                this.mTestACIRState = eTestACIRState.TestAgain;
                            }
                            else
                            {
                                this.mTestACIRState = eTestACIRState.TestOK;  //测试成功
                                lstACIRErrNo.Clear();////清空数据      
                                TestCount = 0;
                            }
                            SWControl.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断
                            if (lstACIRErrNo != null)
                            {
                                for (int i = 0; i < lstACIRErrNo.Count; i++)
                                {
                                    lstACIRErrNo.Add(lstACIRErrNo[i]);
                                }
                            }
                            #endregion

                            break;
                        case eTestACIRState.StopTest:
                            break;
                        case eTestACIRState.ErrTest:
                            break;
                        default:

                            break;
                    }

                }
                catch (Exception ex)
                {
                    //测试异常   
                    this.mTestACIRState = eTestACIRState.ErrTest;
                    ACIRTestlogNet.WriteWarn("ACIR测试异常",ex.ToString());
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }
           
        }

    }
}
