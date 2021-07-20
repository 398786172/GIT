
using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace OCV
{
    //OCV ACIR测试
    public class ClsOCVContr0
    {
        FrmSys mForm;
        FrmManualTest ManualTest;
        public ClsDMM_Ag34970 mDmm;          //万用表控制
        Thread ThreadTestAction;

        public ClsProcess mProc;

        private bool mStopManual;
        private bool mManualTestFinish = false;
        public bool ManualTestFinish { get { return mManualTestFinish; } }

        private bool mStop;         //停止(没用到)
        private bool mTestFinish;   //单次测量完成标志
        public bool TestFinish { get { return mTestFinish; } }
        ResultData rResultData;
        NgResult V_NgResult =new NgResult() ;
        NgResult C_NgResult = new NgResult();
        public void StopAction()
        {
            mStop = true;
        }

        //测试参数
        //int ShowHalfVal = ClsGlobal.TrayType / 2;                     //界面更新用
        //构造函数
        public ClsOCVContr0(SerialPort Switch_SP, SerialPort RTester_SP, string Dmm_USBAddr, FrmSys f1)
        {
            mDmm = new ClsDMM_Ag34970(Dmm_USBAddr);
            mDmm.InitControl_IMM();
            mForm = f1;
            InitPara();
        }

        public void InitPara()
        {
            //ShowHalfVal = ClsGlobal.TrayType;                  //界面更新用     
            mForm.Invoke(new EventHandler(delegate
            {           
                //界面处理->表格数据清空
                this.mForm.dgvTest.Rows.Clear();
              
            }));
        }

        //自动测试流程
        public void StartTestAction()
        {
            try
            {
                mStop = false;
                mTestFinish = false;
                if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest || ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                {
                    ThreadTestAction = new Thread(new ParameterizedThreadStart(TestOCVShell));
                    ThreadTestAction.Start();
                }
            }
            catch (Exception ex)
            {
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                ClsGlobal.OCV_TestErrDetail = ex.Message;
            }
        }

        //手动测试电压
        public void StartManualTest_Volt_A(FrmManualTest fm)
        {
            ManualTest = fm;
            ThreadTestAction = new Thread(new ParameterizedThreadStart(ManualTestVolt));
            ThreadTestAction.Start("A");
        }

        //手动测试壳压
        public void StartManualTest_C_Volt_A(FrmManualTest fm)
        {
            ManualTest = fm;
            ThreadTestAction = new Thread(new ParameterizedThreadStart(ManualTestC_Volt));
            ThreadTestAction.Start("A");
        }

        //手动测试电压壳压
        public void StartManualTest(FrmManualTest fm)
        {
            ManualTest = fm;
            ThreadTestAction = new Thread(new ParameterizedThreadStart(ManualTestVoltShell));
            ThreadTestAction.Start("A");
        }

        //电池的NG状态输出
        private void NGStateOutput(ResultData mResultData, out NgResult mV_NgResult, out NgResult mC_NgResult)
        {
            mV_NgResult.NgType = '0';
            mV_NgResult.NgCode = "00";
            mV_NgResult.NgDescribe = "合格";
            mV_NgResult.NgState = "OK";

            mC_NgResult.NgType = '0';
            mC_NgResult.NgCode = "00";
            mC_NgResult.NgDescribe = "合格";
            mC_NgResult.NgState = "OK";

            //内阻
            if (ClsGlobal.TestType == 0)
            {
                //电压
                if ((mResultData.Volt * 1000) > ClsGlobal.UpLmt_V)
                {
                    mV_NgResult.NgType = '1';
                    mV_NgResult.NgCode = "B1";
                    mV_NgResult.NgState = "NG";
                    mV_NgResult.NgDescribe = "大于上限电压";
                    return;
                }
                if ((mResultData.Volt * 1000) < ClsGlobal.DownLmt_V)
                {
                    mV_NgResult.NgType = '1';
                    mV_NgResult.NgCode = "B2";
                    mV_NgResult.NgState = "NG";
                    mV_NgResult.NgDescribe = "小于下限电压";
                    return;
                }
            }
            else 
            {
                //电压
                if ((mResultData.Volt * 1000) > ClsGlobal.UpLmt_V)
                {
                    mV_NgResult.NgType = '1';
                    mV_NgResult.NgCode = "B1";
                    mV_NgResult.NgState = "NG";
                    mV_NgResult.NgDescribe = "大于上限电压"; 
                }
                if ((mResultData.Volt * 1000) < ClsGlobal.DownLmt_V)
                {
                    mV_NgResult.NgType = '1';
                    mV_NgResult.NgCode = "B2";
                    mV_NgResult.NgState = "NG";
                    mV_NgResult.NgDescribe = "小于下限电压";
                }

                //壳压
                if ((mResultData.Vshell * 1000) > ClsGlobal.UpLmt_SV)
                {
                    mC_NgResult.NgType = '2';
                    mC_NgResult.NgCode = "C1";
                    mC_NgResult.NgState = "NG";
                    mC_NgResult.NgDescribe = "大于上限壳压";
                }
                if ((mResultData.Vshell * 1000) < ClsGlobal.DownLmt_SV)
                {
                    mC_NgResult.NgType = '2';
                    mC_NgResult.NgCode = "C2";
                    mC_NgResult.NgState = "NG";
                    mC_NgResult.NgDescribe = "小于下限壳压";
                }
             
            }
        }

        //测试电池电压内阻
        private void TestOCVShell(object Para)
        {
            double theDMMVolt = 0;
            double theC_DMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            double[] arrC_DMMVolt = new double[ClsGlobal.TrayType]; 
            double dVal;
            double theVolt = 0;
            string sEndTestTime = "";            //测试开始时间

            try
            {
                mTestFinish = false;
                mStop = false;

                #region 测电压壳压
                //仪表初始化
                try
                {
                    Thread.Sleep(150);
                   
                    mDmm.InitControl_IMM();
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    throw new Exception("万用表出错," + ex.Message, ex);
                }

                int iStart = 1;             //电池起始位
                switch (ClsGlobal.TestType)   
                {
                    case 0:
                        #region 测电压
                       
                        if (ClsGlobal.OCV_TestState != eTestState.TestAgain)
                        {
                            #region 第一次数据测试

                            ClsGlobal.TestStartTime = System.DateTime.Now; 
                            #region 测试
                            //测iStart电压
                            for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                            {
                                if (mStop == true)
                                {
                                    mDmm.InitControl_IMM(); ;     //结束,通道全部关断   
                                    throw new Exception("测试被终止");
                                }

                                mDmm.ChannelSwitchContr_IMM(1, i);
                                Thread.Sleep(ClsGlobal.SWDelayTime);
                                mDmm.ReadVolt(out theDMMVolt);
                                arrDMMVolt[i - iStart] = theDMMVolt;
                            }
                            mDmm.InitControl_IMM();      //结束,通道全部关断   
                            ClsGlobal.TestEndTime = System.DateTime.Now;
                            sEndTestTime = ClsGlobal.TestEndTime.ToString("yyy-MM-dd HH:mm:ss");    //测试时间

                            #endregion

                            #endregion

                            #region 数据载入

                            for (int i = 0; i < ClsGlobal.TrayType; i++)
                            {
                               
                                theVolt = arrDMMVolt[i];
                                if (Math.Abs(theVolt) < 1e+6)
                                {
                                    ClsGlobal.G_dbl_VoltArr[i] = theVolt;
                                    //ClsGlobal.G_dbl_VoltArr[i] = theVolt * 1000;
                                }
                                else
                                {
                                    ClsGlobal.lstACIRErrNo.Add(i);
                                    ClsGlobal.G_dbl_VoltArr[i] = 99999;
                                }
                                //数据载入                                  
                                ClsGlobal.listETCELL[i].OCV_Now = ClsGlobal.G_dbl_VoltArr[i].ToString("F6");
                                ClsGlobal.listETCELL[i].OCV_Shell_Now = "0";
                                ClsGlobal.listETCELL[i].Rev_OCV = (ClsGlobal.G_dbl_VoltArr[i] +
                                (ClsGlobal.G_dbl_TempArr[i] * 1000 - ClsGlobal.TempBase) * ClsGlobal.TempParaModify).ToString("F6");    //计算电压修正值 
                                ClsGlobal.listETCELL[i].End_Write_Time = sEndTestTime;   
                            }

                            #endregion
                        }
                        else     //第二次测量,仅把第一次测量失败的进行更新  复测
                        {
                            #region 复测  第二次测量,仅把第一次测量失败的进行更新
                            if (Para.ToString() == "A")
                            {
                                iStart = ClsGlobal.StartA;
                            }
                           
                            int lstErrNo = ClsGlobal.lstACIRErrNo.Count;
                            mDmm.InitControl_IMM();   
                            #region 测试电压 20180614 WPL
                            for (int i = 0; i < lstErrNo; i++)
                            {
                                if (mStop == true)
                                {
                                    mDmm.InitControl_IMM(); ;     //结束,通道全部关断   
                                    throw new Exception("测试被终止");
                                }

                                int item = ClsGlobal.lstACIRErrNo[i];
                                mDmm.ChannelSwitchContr_IMM(1, item + iStart);
                                Thread.Sleep(ClsGlobal.SWDelayTime);
                                mDmm.ReadVolt(out theDMMVolt);
                                arrDMMVolt[item] = theDMMVolt;

                                if (Math.Abs(theDMMVolt) < 1e+6)
                                {
                                    ClsGlobal.G_dbl_VoltArr[item] = theDMMVolt;
                                    //ClsGlobal.G_dbl_VoltArr[item] = theDMMVolt * 1000;
                                }
                                else
                                {
                                    ClsGlobal.G_dbl_VoltArr[item] = 99999;
                                }
                                //数据载入                                  
                                ClsGlobal.listETCELL[item].OCV_Now = ClsGlobal.G_dbl_VoltArr[item].ToString("F6");
                                ClsGlobal.listETCELL[item].OCV_Shell_Now = "0";
                                ClsGlobal.listETCELL[item].Rev_OCV = (ClsGlobal.G_dbl_VoltArr[item] +
                                                    (ClsGlobal.G_dbl_TempArr[item]*1000 - ClsGlobal.TempBase) * ClsGlobal.TempParaModify).ToString("F6");    //计算电压修正值    
                            }
                            mDmm.InitControl_IMM();      //结束,通道全部关断   
                            #endregion
                            ClsGlobal.lstACIRErrNo.Clear();////清空数据 
                            #endregion
                        }
                        #endregion
                        break;
                    case 1:
                        #region 测电压壳压       
                        if (ClsGlobal.OCV_TestState != eTestState.TestAgain)
                        {
                            #region 第一次数据测试

                            ClsGlobal.TestStartTime = System.DateTime.Now;
                            #region 测试
                            //测iStart电压
                            for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                            {
                                if (mStop == true)
                                {
                                    mDmm.InitControl_IMM(); ;     //结束,通道全部关断   
                                    throw new Exception("测试被终止");
                                }

                                mDmm.ChannelSwitchContr_IMM(1, i);
                                Thread.Sleep(ClsGlobal.SWDelayTime);
                                mDmm.ReadVolt(out theDMMVolt);
                                arrDMMVolt[i - iStart] = theDMMVolt;
                            }

                            mDmm.InitControl_IMM();     //结束,通道全部关断 

                            //测iStart壳压
                            for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                            {
                                if (mStop == true)
                                {
                                    mDmm.InitControl_IMM();      //结束,通道全部关断   
                                    throw new Exception("测试被终止");
                                }

                                mDmm.ChannelSwitchContr_IMM(2, i);
                                Thread.Sleep(ClsGlobal.SWDelayTime);
                                mDmm.ReadVolt(out theC_DMMVolt);
                                arrC_DMMVolt[i - iStart] = theC_DMMVolt;
                            }

                            mDmm.InitControl_IMM();      //结束,通道全部关断   
                            ClsGlobal.TestEndTime = System.DateTime.Now;
                            sEndTestTime = ClsGlobal.TestEndTime.ToString("yyy-MM-dd HH:mm:ss");    //测试时间

                            #endregion

                            #endregion

                            #region 数据载入

                            for (int i = 0; i < ClsGlobal.TrayType; i++)
                            {

                                theVolt = arrDMMVolt[i];
                                if (Math.Abs(theVolt) < 1e+6)
                                {
                                    ClsGlobal.G_dbl_VoltArr[i] = theVolt;
                                    //ClsGlobal.G_dbl_VoltArr[i] = theVolt * 1000;
                                }
                                else
                                {
                                    ClsGlobal.lstACIRErrNo.Add(i);
                                    ClsGlobal.G_dbl_VoltArr[i] = 99999;
                                }
                                theVolt = arrC_DMMVolt[i];
                                if (Math.Abs(theVolt) < 1e+6)
                                {
                                    ClsGlobal.G_dbl_VshellArr[i] = theVolt * 1000;
                                }
                                else
                                {
                                    ClsGlobal.G_dbl_VshellArr[i] = 99999;
                                }

                                //数据载入                                  
                                ClsGlobal.listETCELL[i].OCV_Now = ClsGlobal.G_dbl_VoltArr[i].ToString("F6");
                                ClsGlobal.listETCELL[i].OCV_Shell_Now = ClsGlobal.G_dbl_VshellArr[i].ToString("F6");
                                ClsGlobal.listETCELL[i].Rev_OCV = (ClsGlobal.G_dbl_VoltArr[i] +
                                (ClsGlobal.G_dbl_TempArr[i]*1000 - ClsGlobal.TempBase) * ClsGlobal.TempParaModify).ToString("F6");    //计算电压修正值 
                                ClsGlobal.listETCELL[i].End_Write_Time = sEndTestTime;
                            }

                            #endregion
                        }
                        else     //第二次测量,仅把第一次测量失败的进行更新  复测
                        {
                            #region 复测  第二次测量,仅把第一次测量失败的进行更新
                          
                            int lstErrNo = ClsGlobal.lstACIRErrNo.Count;
                            mDmm.InitControl_IMM();      //结束,通道全部关断   

                            #region 测试电压 
                            for (int i = 0; i < lstErrNo; i++)
                            {
                                if (mStop == true)
                                {
                                    mDmm.InitControl_IMM();      //结束,通道全部关断  
                                    throw new Exception("测试被终止");
                                }

                                int item = ClsGlobal.lstACIRErrNo[i];
                                mDmm.ChannelSwitchContr_IMM(1, i);
                                Thread.Sleep(40);
                                mDmm.ReadVolt(out theDMMVolt);
                                arrDMMVolt[item] = theDMMVolt;

                                if (Math.Abs(theDMMVolt) < 1e+6)
                                {
                                    ClsGlobal.G_dbl_VoltArr[item] = theDMMVolt;
                                    //ClsGlobal.G_dbl_VoltArr[item] = theDMMVolt * 1000;
                                }
                                else
                                {
                                    ClsGlobal.G_dbl_VoltArr[item] = 99999;
                                }
                                //数据载入                                  
                                ClsGlobal.listETCELL[item].OCV_Now = ClsGlobal.G_dbl_VoltArr[item].ToString("F6");
                                ClsGlobal.listETCELL[item].Rev_OCV = (ClsGlobal.G_dbl_VoltArr[item] +
                                                    (ClsGlobal.G_dbl_TempArr[item] * 1000 - ClsGlobal.TempBase) * ClsGlobal.TempParaModify).ToString("F6");    //计算电压修正值    
                            }
                            #endregion

                            #region 测试壳压
                            mDmm.InitControl_IMM();      //结束,通道全部关断  
                            Thread.Sleep(60);

                            for (int i = 0; i < lstErrNo; i++)
                            {
                                if (mStop == true)
                                {
                                    mDmm.InitControl_IMM();      //结束,通道全部关断 
                                    throw new Exception("测试被终止");
                                }
                               

                                int item = ClsGlobal.lstACIRErrNo[i];                     
                                mDmm.ChannelSwitchContr_IMM(2, item + iStart);
                                Thread.Sleep(200);
                                mDmm.ReadVolt(out theC_DMMVolt);

                                if (Math.Abs(theC_DMMVolt) < 1e+6)
                                {
                                    ClsGlobal.G_dbl_VshellArr[item] = theC_DMMVolt ;
                                    //ClsGlobal.G_dbl_VshellArr[item] = theC_DMMVolt * 1000;
                                }
                                else
                                {
                                    ClsGlobal.G_dbl_VshellArr[item] = 99999;
                                }
                                //数据载入                                  
                                ClsGlobal.listETCELL[item].OCV_Shell_Now = ClsGlobal.G_dbl_VshellArr[item].ToString("F6");
                              
                            }
                            #endregion

                            ClsGlobal.lstACIRErrNo.Clear();////清空数据 
                            #endregion
                        }

                        #endregion
                        break;
                    default:
                        break;
                }
                #endregion

                #region 压降、K值计算
                //if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                //{
                //    for (int i = 0; i < ClsGlobal.TrayType; i++)
                //    {
                //        Random RD = new Random(i);
                //        ClsGlobal.listETCELL[i].Capacity = (2600 * RD.Next(8, 11) * 0.1).ToString();
                //        ClsGlobal.listETCELL[i].OCV_1 = (double.Parse(ClsGlobal.listETCELL[i].OCV_Now) + 10 * RD.Next(8, 11) * 0.1).ToString("F2");
                //    }
                //}

                //if (ClsGlobal.OCVType == 2)
                //{
                //    for (int i = 0; i < ClsGlobal.TrayType; i++)
                //    {
                //        if (ClsGlobal.listETCELL[i].HNGSt == "OK")
                //        {
                //            double VoltToComp = double.Parse(ClsGlobal.listETCELL[i].OCV_1) - double.Parse(ClsGlobal.listETCELL[i].OCV_Now);
                //            ClsGlobal.listETCELL[i].VoltDrop_Now = VoltToComp.ToString("F2");

                //            ClsGlobal.listETCELL[i].K_Now = Math.Round((VoltToComp / ClsGlobal.GetHours), 6);
                //            ClsGlobal.listETCELL[i].K_1_2 = ClsGlobal.listETCELL[i].K_Now;
                //        }
                //        else
                //        {
                //            ClsGlobal.listETCELL[i].VoltDrop_Now = "0";
                //        }
                //    }

                //}
                //else if (ClsGlobal.OCVType == 3)
                //{
                //    //--------------------------------------------------------
                //    for (int i = 0; i < ClsGlobal.TrayType; i++)
                //    {
                //        if (ClsGlobal.listETCELL[i].HNGSt == "OK")
                //        {
                //            double VoltToComp = double.Parse(ClsGlobal.listETCELL[i].OCV_2) - double.Parse(ClsGlobal.listETCELL[i].OCV_Now);
                //            ClsGlobal.listETCELL[i].VoltDrop_Now = VoltToComp.ToString("F2");
                //            ClsGlobal.listETCELL[i].K_Now = Math.Round((VoltToComp / ClsGlobal.GetHours), 6);
                //            ClsGlobal.listETCELL[i].K_2_3 = ClsGlobal.listETCELL[i].K_Now;
                //        }
                //        else
                //        {
                //            ClsGlobal.listETCELL[i].VoltDrop_Now = "0";
                //        }
                //    }
                //}

                #endregion

                #region NG判断
                //--------------------------------------------------------
                //NG判断
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (! ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") && ! ClsGlobal.listETCELL[i].Cell_ID.Contains("BYD"))
                    {
                        rResultData.Volt = ClsGlobal.G_dbl_VoltArr[i];
                        rResultData.Vshell = ClsGlobal.G_dbl_VshellArr[i];
                     
                        NGStateOutput(rResultData, out V_NgResult,out C_NgResult);
                        if (ClsGlobal.TestType == 0)
                        {
                            ClsGlobal.listETCELL[i].V_NGStatus = V_NgResult.NgType;
                            ClsGlobal.listETCELL[i].V_NGSt = V_NgResult.NgState;
                            ClsGlobal.listETCELL[i].V_NGReason = V_NgResult.NgCode;
                            ClsGlobal.listETCELL[i].V_NGRea = V_NgResult.NgDescribe;
                            ClsGlobal.listETCELL[i].OCV_Shell_Now = "";
                        }
                        else
                        {
                            ClsGlobal.listETCELL[i].V_NGStatus = V_NgResult.NgType;
                            ClsGlobal.listETCELL[i].V_NGSt = V_NgResult.NgState;
                            ClsGlobal.listETCELL[i].V_NGReason = V_NgResult.NgCode;
                            ClsGlobal.listETCELL[i].V_NGRea = V_NgResult.NgDescribe;
                            ClsGlobal.listETCELL[i].C_NGStatus = C_NgResult.NgType;
                            ClsGlobal.listETCELL[i].C_NGSt = C_NgResult.NgState;
                            ClsGlobal.listETCELL[i].C_NGReason = C_NgResult.NgCode;
                            ClsGlobal.listETCELL[i].C_NGRea = C_NgResult.NgDescribe;
                           
                        }
                    }
                    else
                    {

                        ClsGlobal.listETCELL[i].V_NGStatus = '3';
                        ClsGlobal.listETCELL[i].V_NGSt = "不合格";
                        ClsGlobal.listETCELL[i].V_NGReason = "D1";
                        ClsGlobal.listETCELL[i].V_NGRea = "条码为空";

                        ClsGlobal.listETCELL[i].C_NGStatus = '3';
                        ClsGlobal.listETCELL[i].C_NGSt = "不合格";
                        ClsGlobal.listETCELL[i].C_NGReason = "D1";
                        ClsGlobal.listETCELL[i].C_NGRea = "条码为空";
                    }
                }
                #endregion

                #region  界面更新

                RefreshOCVdata();
                #endregion

                #region  测试结束，判断是否复测
                //测试次数判断
                ClsGlobal.TestCount++;
                //ACIR有异常测试数据,重测
                if (ClsGlobal.lstACIRErrNo.Count > ClsGlobal.SetEnOCV && ClsGlobal.TestCount < ClsGlobal.MaxTestNum)
                {
                    ClsGlobal.OCV_TestState = eTestState.TestAgain;
                   
                    ClsGlobal.ResRangeMode = 1;                             //用大量程                  
                }
                else
                {
                    ClsGlobal.OCV_TestState = eTestState.TestOK;            //测试成功
                    ClsGlobal.lstACIRErrNo.Clear();////清空数据      
                    ClsGlobal.TestCount = 0;         
                }
                mTestFinish = true;     //该次测试完成    
                #endregion
            }
            catch (Exception ex)
            {
                //测试异常   
              
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                ClsGlobal.OCV_TestErrDetail = ex.ToString();
                //  MessageBox.Show(ex.Message.ToString());
            }
        }

        private void RefreshOCVdata()
        {
            int index = 0;
            //刷新界面
            mForm.Invoke(new EventHandler(delegate
            {
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") && !ClsGlobal.listETCELL[i].Cell_ID.Contains("BYD"))
                    {
                        index = this.mForm.dgvTest.Rows.Add();
                        mForm.dgvTest.Rows[index].Cells[0].Value = ClsGlobal.listETCELL[i].Cell_Position;
                        mForm.dgvTest.Rows[index].Cells[1].Value = ClsGlobal.listETCELL[i].Cell_ID;
                        mForm.dgvTest.Rows[index].Cells[2].Value = ClsGlobal.listETCELL[i].OCV_Now;
                        mForm.dgvTest.Rows[index].Cells[3].Value = ClsGlobal.listETCELL[i].V_NGReason;

                        mForm.dgvTest.Rows[index].Cells[4].Value = ClsGlobal.listETCELL[i].OCV_Shell_Now;
                        mForm.dgvTest.Rows[index].Cells[5].Value = ClsGlobal.listETCELL[i].C_NGReason;

                        mForm.dgvTest.Rows[index].Cells[6].Value = ClsGlobal.listETCELL[i].TMP;
                        mForm.dgvTest.Rows[index].Cells[7].Value = ClsGlobal.listETCELL[i].T_NGReason;

                        mForm.dgvTest.Rows[index].Cells[1].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[2].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[3].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[4].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[5].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[6].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[5].Style.ForeColor = Color.Black;
                        mForm.dgvTest.Rows[index].Cells[6].Style.ForeColor = Color.Black;

                        //电压异常
                        if (ClsGlobal.listETCELL[i].V_NGStatus == '1')
                        {
                            mForm.dgvTest.Rows[index].Cells[2].Style.ForeColor = Color.Red;  
                        }

                        //壳压异常 
                        if (ClsGlobal.listETCELL[i].C_NGStatus == '1')
                        {
                            mForm.dgvTest.Rows[index].Cells[4].Style.ForeColor = Color.Red;                    
                        }

                        //温度异常
                        if (ClsGlobal.listETCELL[i].T_NGStatus  == '4')
                        {
                            mForm.dgvTest.Rows[index].Cells[6].Style.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        index = this.mForm.dgvTest.Rows.Add();
                        mForm.dgvTest.Rows[index].Cells[0].Value = ClsGlobal.listETCELL[i].Cell_Position;
                        mForm.dgvTest.Rows[index].Cells[1].Value = ClsGlobal.listETCELL[i].Cell_ID;
                        mForm.dgvTest.Rows[index].Cells[1].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[2].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[3].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[4].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[5].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[5].Style.ForeColor = Color.Black;
                        mForm.dgvTest.Rows[index].Cells[6].Style.BackColor = Color.White;
                        mForm.dgvTest.Rows[index].Cells[6].Style.ForeColor = Color.Black;
                        mForm.dgvTest.Rows[index].Cells[7].Value = "D1";

                    }
                }
            }));
        }
  
        private void ManualTestVoltShell(object Para )
        {
            double theDMMVolt = 0;
            double theC_DMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            double[] arrC_DMMVolt = new double[ClsGlobal.TrayType];
            int theCHCount = ClsGlobal.TrayType;
          
            string theTestType = "";
            try
            {
                mManualTestFinish = false;
                mStopManual = false;

                #region 测电压/壳压
                int iStart = 1;             //电池起始位
                //(1) 测(电压,内阻)             
                if (Para.ToString() == "A")
                {
                    iStart = ClsGlobal.StartA;
                  
                }
                ClsGlobal.TestStartTime = System.DateTime.Now;

                #region 测试
                //测iStart电压
                for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                {
                    if (mStop == true)
                    {
                        mDmm.InitControl_IMM(); ;     //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }

                    mDmm.ChannelSwitchContr_IMM(1, i);
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    mDmm.ReadVolt(out theDMMVolt);
                    arrDMMVolt[i - iStart] = theDMMVolt;
                }

                mDmm.InitControl_IMM();     //结束,通道全部关断 

                //测iStart壳压
                for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                {
                    if (mStop == true)
                    {
                        mDmm.InitControl_IMM();      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }

                    mDmm.ChannelSwitchContr_IMM(2, i);
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    mDmm.ReadVolt(out theC_DMMVolt);
                    arrC_DMMVolt[i - iStart] = theC_DMMVolt;
                }
                mDmm.InitControl_IMM();      //结束,通道全部关断   
                #endregion
                ClsGlobal.TestEndTime = System.DateTime.Now;

                //数值显示

                if (ManualTest.IsHandleCreated == true)
                {

                    ManualTest.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < theCHCount; i++)
                        {
                            //arrDMMVolt[i] = arrDMMVolt[i] * 1000;
                            //arrC_DMMVolt[i] = arrC_DMMVolt[i] * 1000;

                            ManualTest.dgvManualTest.Rows[i].Cells[1].Value = arrDMMVolt[i].ToString("F6");   //刷新界面
                            ManualTest.dgvManualTest.Rows[i].Cells[2].Value = arrC_DMMVolt[i].ToString("F6");
                        }
                        ManualTest.dgvManualTest.Refresh();

                        TimeSpan Ts = ClsGlobal.TestEndTime - ClsGlobal.TestStartTime;
                        ManualTest.lblTestTime.Text = "测试时间(s):" + Ts.TotalSeconds.ToString("F1");
                    }));
                }
                #endregion

                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(theTestType +  ":" + ex.Message);
            }

        }
            //手动测试电压
        private void ManualTestVolt(object Para)
        {
            double theDMMVolt = 0;
          
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
          
            int theCHCount = ClsGlobal.TrayType;
            string theTestType;
            try
            {

                mManualTestFinish = false;
                mStopManual = false;

                #region 测电压

                int iStart = 1;
                int index = 1;
                //(1) 测(电压)             
                if (Para.ToString() == "A")
                {
                    iStart = ClsGlobal.StartA;     
                }
               
                ClsGlobal.TestStartTime = System.DateTime.Now;

                index = 1;
                theTestType = "测(电压)";
                //测iStart电压
                for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                {
                    if (mStop == true)
                    {
                        mDmm.InitControl_IMM();      //结束,通道全部关断 
                        throw new Exception("测试被终止");
                    }
                    mDmm.ChannelSwitchContr_IMM(1, i);
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    mDmm.ReadVolt(out theDMMVolt);
                    arrDMMVolt[i - iStart] = theDMMVolt;
                }

                mDmm.InitControl_IMM();      //结束,通道全部关断 
                ClsGlobal.TestEndTime = System.DateTime.Now;

                //数值显示
                if (ManualTest.IsHandleCreated == true)
                {
                    ManualTest.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < theCHCount; i++)
                        {
                            //arrDMMVolt[i] = arrDMMVolt[i] * 1000;

                            ManualTest.dgvManualTest.Rows[i].Cells[1].Value = arrDMMVolt[i].ToString("F6");   //刷新界面
                        }
                        ManualTest.dgvManualTest.Refresh();

                        TimeSpan Ts = ClsGlobal.TestEndTime - ClsGlobal.TestStartTime;
                        ManualTest.lblTestTime.Text = "测试时间(s):" + Ts.TotalSeconds.ToString("F6");
                    }));
                }
                #endregion

                mManualTestFinish = true;          
            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show( ex.Message);
            }

        }

        //手动测试电压
        private void ManualTestC_Volt(object Para)
        {
            double theDMMVolt = 0;

            double[] arrDMMVolt = new double[ClsGlobal.TrayType];

            int theCHCount = ClsGlobal.TrayType;
            string theTestType;
            try
            {

                mManualTestFinish = false;
                mStopManual = false;

                #region 测电压

                int iStart = 1;
                int index = 1;
                //(1) 测(电压)             
                if (Para.ToString() == "A")
                {
                    iStart = ClsGlobal.StartA;
                }

                ClsGlobal.TestStartTime = System.DateTime.Now;

                index = 1;
                theTestType = "测(电压)";
                //测iStart电压
                for (int i = iStart; i < ClsGlobal.TrayType + iStart; i++)
                {
                    if (mStop == true)
                    {
                        mDmm.InitControl_IMM();      //结束,通道全部关断 
                        throw new Exception("测试被终止");
                    }
                    mDmm.ChannelSwitchContr_IMM(2, i);
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    mDmm.ReadVolt(out theDMMVolt);
                    arrDMMVolt[i - iStart] = theDMMVolt;
                }

                mDmm.InitControl_IMM();      //结束,通道全部关断 
                ClsGlobal.TestEndTime = System.DateTime.Now;

                //数值显示
                if (ManualTest.IsHandleCreated == true)
                {
                    ManualTest.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < theCHCount; i++)
                        {
                            //arrDMMVolt[i] = arrDMMVolt[i] * 1000;

                            ManualTest.dgvManualTest.Rows[i].Cells[2].Value = arrDMMVolt[i].ToString("F6");   //刷新界面
                        }
                        ManualTest.dgvManualTest.Refresh();

                        TimeSpan Ts = ClsGlobal.TestEndTime - ClsGlobal.TestStartTime;
                        ManualTest.lblTestTime.Text = "测试时间(s):" + Ts.TotalSeconds.ToString("F6");
                    }));
                }
                #endregion

                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }

        }


        //停止手动测试
        public void StopManualTest()
        {
           
            mManualTestFinish = true;
            mStopManual = true;
        }

        //停止手动测试
        public void ChannelSwitchContr( int CH)
        {

            mManualTestFinish = true;
            mStopManual = true;
        }


        #region 没用到的函数


        /// <summary>
        /// OCV常规电压,单通道测试
        /// </summary>
        /// <param name="ChannelNo">通道号</param>
        /// <param name="VoltVal">电压值</param>
        public void VoltTest_Single(uint ChannelNo, out double VoltVal)
        {
            try
            {
                //OCVSW.ChannelSelSwitch32(ChannelNo);
                mDmm.ReadVolt(out VoltVal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// OCV带壳电压,单通道测试
        /// </summary>
        /// <param name="ChannelNo">通道号</param>
        /// <param name="ShellVal">壳电压值(对负极)</param>
        public void VoltTest_Shell_Single(uint ChannelNo, out double ShellVal)
        {
            try
            {
                //OCVSW.ChannelSelSwitch24(ChannelNo, 1);
                mDmm.ReadVolt(out ShellVal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// OCV带壳电压,单通道测试
        /// </summary>
        /// <param name="ChannelNo">通道号</param>
        /// <param name="VoltVal">电压值</param>
        /// <param name="ShellVal">壳电压值(对负极)</param>
        public void VoltTest_Shell_Single(uint ChannelNo, out double VoltVal, out double ShellVal)
        {
            try
            {
                //OCVSW.ChannelSelSwitch24(ChannelNo, 0);
                mDmm.ReadVolt(out VoltVal);
                //OCVSW.ChannelSelSwitch24(ChannelNo, 1);
                mDmm.ReadVolt(out ShellVal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

    //电芯OCV ID
    struct NgResult
    {
        public char NgType;           //NG类型 0、1、2、3、4
        public string NgState;          //NG状态  NG、OK
        public string NgCode;           //NG代码  00、B1、B2...
        public string NgDescribe;       //NG描述  "合格"、"小于最小压降"....
    }
    struct ResultData
    {
        public double Volt;
        public double Vshell;   //壳压
    }

}
