using DevInfo;
using System.Collections.Generic;

namespace OCV
{
    //测试数据分析
    public class ClsTestAnalysis
    {
        DBCOM_DevInfo mDBCOM_DevInfo;
        List<DevInfo.Model.SET_Info> lstSetInfo;
        DevInfo.Model.SET_Info mSetInfo;

        //标志位,用于避免重复写数据库
        static bool[] OCVErrClearFlag = new bool[256];
        static bool[] ShellErrClearFlag = new bool[256];
        static bool[] ACIRErrClearFlag = new bool[256];

        public ClsTestAnalysis(DBCOM_DevInfo dbCom_DevInfo)
        {
            mDBCOM_DevInfo = dbCom_DevInfo;
            mDBCOM_DevInfo.GetSetInfoList(out lstSetInfo);
            mSetInfo = lstSetInfo[0];
        }

        public void RefreshSetInfo()
        {
            mDBCOM_DevInfo.GetSetInfoList(out lstSetInfo);
            mSetInfo = lstSetInfo[0];
        }

        //OCV数据是否有连续超限异常
        //mode=1,不考虑空电池的情况(NullCellCode)
        //mode=2,不管有没有电池,都进行计数
        public bool OCVAnalysis(List<ET_CELL> lstCell, int mode, out List<int> lstErrChannelNo)
        {
            List<int> theLstErrChannelNo = new List<int>();
            int NowCount;

            for (int i = 0; i < lstCell.Count; i++)
            {
                if (mSetInfo.OCV_SetEN == true &&
                    ((mode == 1 && lstCell[i].Cell_ID.Substring(0, 12) != "NullCellCode") ||
                    mode == 2))
                {
                    if (lstCell[i].OCV_Now > (double)mSetInfo.OCV_UCL || lstCell[i].OCV_Now < (double)mSetInfo.OCV_LCL)   //超限
                    {
                        mDBCOM_DevInfo.Add_OCVErrCount(lstCell[i].Cell_Position, out NowCount);
                        OCVErrClearFlag[i] = false;

                        if (NowCount >= mSetInfo.OCV_TestTimes)  //超测试次数
                        {
                            theLstErrChannelNo.Add(lstCell[i].Cell_Position);
                        }
                    }
                    else   //没超限
                    {
                        if (OCVErrClearFlag[i] == false)
                        {
                            mDBCOM_DevInfo.Set_OCVErrCount(lstCell[i].Cell_Position, 0);
                            OCVErrClearFlag[i] = true;
                        }
                    }
                }
            }

            if (theLstErrChannelNo.Count > 0)
            {
                lstErrChannelNo = theLstErrChannelNo;
                return true;
            }
            else
            {
                lstErrChannelNo = null;
                return false;
            }

        }

        //Shell数据是否有连续超限异常
        public bool ShellAnalysis(List<ET_CELL> lstCell, int mode, out List<int> lstErrChannelNo)
        {
            List<int> theLstErrChannelNo = new List<int>();
            int NowCount;

            for (int i = 0; i < lstCell.Count; i++)
            {
                if (mSetInfo.Shell_SetEN == true &&
                    ((mode == 1 && lstCell[i].Cell_ID.Substring(0, 12) != "NullCellCode") ||
                    mode == 2))
                {
                    if (lstCell[i].OCV_Shell_Now > (double)mSetInfo.Shell_UCL || lstCell[i].OCV_Shell_Now < (double)mSetInfo.Shell_LCL)   //超限
                    {
                        mDBCOM_DevInfo.Add_ShellErrCount(lstCell[i].Cell_Position, out NowCount);
                        ShellErrClearFlag[i] = false;

                        if (NowCount >= mSetInfo.Shell_TestTimes)  //超测试次数
                        {
                            theLstErrChannelNo.Add(lstCell[i].Cell_Position);
                        }
                    }
                    else   //没超限
                    {
                        if (ShellErrClearFlag[i] == false)
                        {
                            mDBCOM_DevInfo.Set_ShellErrCount(lstCell[i].Cell_Position, 0);
                            ShellErrClearFlag[i] = true;
                        }
                    }
                }
            }

            if (theLstErrChannelNo.Count > 0)
            {
                lstErrChannelNo = theLstErrChannelNo;
                return true;
            }
            else
            {
                lstErrChannelNo = null;
                return false;
            }

        }

        //ACIR数据是否有连续超限异常
        public bool ACIRAnalysis(List<ET_CELL> lstCell, int mode, out List<int> lstErrChannelNo)
        {
            List<int> theLstErrChannelNo = new List<int>();
            int NowCount;

            for (int i = 0; i < lstCell.Count; i++)
            {
                if (mSetInfo.ACIR_SetEN == true &&
                    ((mode == 1 && lstCell[i].Cell_ID.Substring(0, 12) != "NullCellCode") ||
                    mode == 2))
                {
                    if (lstCell[i].ACIR_Now > (double)mSetInfo.ACIR_UCL ||lstCell[i].ACIR_Now < (double)mSetInfo.ACIR_LCL)   //超限
                    {
                        mDBCOM_DevInfo.Add_ACIRErrCount(lstCell[i].Cell_Position, out NowCount);
                        ACIRErrClearFlag[i] = false;

                        if (NowCount >= mSetInfo.ACIR_TestTimes)  //超测试次数
                        {
                            theLstErrChannelNo.Add(lstCell[i].Cell_Position);
                        }
                    }
                    else   //没超限
                    {
                        if (ACIRErrClearFlag[i] == false)
                        {
                            mDBCOM_DevInfo.Set_ACIRErrCount(lstCell[i].Cell_Position, 0);
                            ACIRErrClearFlag[i] = true;
                        }
                    }
                }
            }

            if (theLstErrChannelNo.Count > 0)
            {
                lstErrChannelNo = theLstErrChannelNo;
                return true;
            }
            else
            {
                lstErrChannelNo = null;
                return false;
            }

        }



    }



}
