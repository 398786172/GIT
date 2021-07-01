using OCV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using OCV.OCVLogs;
using System.Windows.Forms;
using OCV.Models;

namespace DB_OCV.DAL
{

    //擎天数据库访问接口
    //OCV数据库包括两种情况:本地服务器 以及擎天服务器
    public class DBCOM_OCV
    {

        BatData batData;
        public DBCOM_OCV(string iP, string database, string userID, string pWD)
        {
            string IP = iP;
            string Database = database;
            string UserID = userID;
            string PWD = pWD;
            //StringBuilder StrB = new StringBuilder();
            //StrB.Append("Data Source=" + IP);
            //StrB.Append(" ;Initial Catalog=" + Database);
            //StrB.Append(" ;User ID=" + UserID);
            //StrB.Append(" ;Password=" + PWD);
            //StrB.Append(" ;Charset=" + "utf8");
            //StrB.Append(" ;Pooling=" + "true");
            string info = "Password={3};Persist Security Info=True;User ID={2};Initial Catalog={1};Data Source={0}";
            string strConnect = string.Format(info, IP, database, userID, PWD);
            batData = new BatData(strConnect);
        }

        public int InsertOCVACIRData(int OCVType, List<ET_CELL> listCell, int isTrans)
        {
            bool _iResult;
            int InsertCount = 0;

            List<Model.BatInfo_Model> lstBatInfo = new List<Model.BatInfo_Model>();
            try
            {
                if (OCVType == 1)   //写OCV1表
                {
                    ReflectETCellToBatInfo(0, listCell, ref lstBatInfo, isTrans);

                    for (int i = 0; i < lstBatInfo.Count; i++)
                    {
                        _iResult = batData.Add("qt_db_ocv1", lstBatInfo[i]);

                        if (_iResult == true)
                        {
                            InsertCount++;
                        }
                    }
                }
                else if (OCVType == 2)   //写OCV2表
                {
                    ReflectETCellToBatInfo(1, listCell, ref lstBatInfo, isTrans);
                    for (int i = 0; i < lstBatInfo.Count; i++)
                    {
                        _iResult = batData.Add("qt_db_ocv2", lstBatInfo[i]);
                        if (_iResult == true)
                        {
                            InsertCount++;
                        }
                    }
                }
                else if (OCVType == 3)   //写OCV3表
                {
                    ReflectETCellToBatInfo(2, listCell, ref lstBatInfo, isTrans);
                    for (int i = 0; i < lstBatInfo.Count; i++)
                    {
                        _iResult = batData.Add("qt_db_ocv3", lstBatInfo[i]);
                        if (_iResult == true)
                        {
                            InsertCount++;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ClsLogs.SqllogNet.WriteWarn("InsertOCVACIRData_KVal_Local", ex.ToString());
                return -1;
            }
            return InsertCount;
        }
        /// <summary>
        /// 电池信息: 获得OCV2数据信息
        /// </summary>
        public int Get_BattInfo(int OCVType, string Pallet_ID, ref List<ET_CELL> listCell)
        {
            List<Model.BatInfo_Model> lstBatInfo = new List<Model.BatInfo_Model>();
            Model.BatInfo_Model mOCVdata = new Model.BatInfo_Model();
            //List<ET_CELL> list = new List<ET_CELL>();
            //listCell = listCell = ClsGlobal.listETCELL;
            //listCell = null;
            string TestEndTime;
            int ret = -1;
            try
            {
                if (OCVType == 3)
                {
                    ret = batData.GetModel_LatestTime(Pallet_ID, "qt_db_ocv2", out TestEndTime);
                    if (ret == 0)
                    {
                        lstBatInfo = batData.GetData(3, Pallet_ID, "qt_db_ocv2", TestEndTime);

                        if (lstBatInfo == null)
                        {
                            lstBatInfo = null;
                            return 2;
                        }
                        ReflectBatInfoToETCell_KVal(OCVType, lstBatInfo, ref listCell);
                        //listCell = list;
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }


                }
                return 0;
            }
            catch (Exception ex)
            {
                ClsLogs.SqllogNet.WriteWarn("InsertOCVACIRData_KVal_Local", ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 电池信息: 更新ET_CELL电池信息到BatInfo
        /// K值传入压降
        /// 自定义其中的isTrans的值
        /// </summary>
        /// <param name="OCVTable">表类型, 0: OCV1, 1: OCV2,2: OCV3,3: 总表</param>
        /// <param name="listCell">ET_CELL列表</param>
        /// <param name="mlstBatInfo">BatInfo_MES列表</param>
        /// <param name="isTrans">isTrans参数值</param>
        private void ReflectETCellToBatInfo(int OCVTable, List<ET_CELL> listCell, ref List<Model.BatInfo_Model> mlstBatInfo, int isTrans)
        {
            List<ET_CELL> theList = listCell;

            try
            {
                for (int i = 0; i < theList.Count; i++)
                {


                    Model.BatInfo_Model BatInfo = new Model.BatInfo_Model();
                    BatInfo.TestMode = ClsGlobal.IsAutoMode ? "自动" : "手动";
                    switch (OCVTable)
                    {
                        case 0:     //OCV1
                            if (!theList[i].Cell_ID.Contains("NullCellCode"))
                            {
                                //BatInfo.Eqp_ID = ClsGlobal.DeviceCode;
                                //BatInfo.PC_ID = SystemInformation.ComputerName;
                                BatInfo.Eqp_ID = "QT-" + ClsGlobal.DeviceCode;
                                BatInfo.PC_ID = ClsGlobal.PCID;
                                BatInfo.OPERATION = ClsGlobal.OPEATION_ID;
                                BatInfo.IS_TRANS = isTrans;
                                BatInfo.TRAY_ID = theList[i].Pallet_ID.Trim();
                                BatInfo.CELL_ID = theList[i].Cell_ID;
                                BatInfo.BATTERY_POS = theList[i].Cell_Position;
                                BatInfo.MODEL_NO = theList[i].MODEL_NO;
                                BatInfo.BATCH_NO = "";
                                BatInfo.TOTAL_NG_STATE = theList[i].NgState;
                                BatInfo.OCV_VOLTAGE = Math.Round(theList[i].OCV_Now, 4);
                                //BatInfo.ACIR = theList[i].ACIR_Now;

                                BatInfo.TEST_NG_CODE = theList[i].Test_NgResult.NgCode;
                                BatInfo.TEST_RESULT = theList[i].Test_NgResult.NgState;
                                BatInfo.TEST_RESULT_DESC = theList[i].Test_NgResult.NgDescribe;

                                BatInfo.POSTIVE_TEMP = theList[i].PostiveTMP;
                                BatInfo.NEGATIVE_TEMP = theList[i].NegativeTMP;

                                BatInfo.REV_OCV = Math.Round(theList[i].Rev_OCV, 4);
                                BatInfo.END_DATE_TIME = theList[i].End_Write_Time;
                                mlstBatInfo.Add(BatInfo);
                            }
                            break;
                        case 1:     //OCV2
                            if (!theList[i].Cell_ID.Contains("NullCellCode"))
                            {
                                BatInfo.Eqp_ID = "QT-" + ClsGlobal.DeviceCode;
                                BatInfo.PC_ID = ClsGlobal.PCID;
                                BatInfo.OPERATION = ClsGlobal.OPEATION_ID;
                                BatInfo.IS_TRANS = isTrans;
                                BatInfo.TRAY_ID = theList[i].Pallet_ID.Trim();
                                BatInfo.CELL_ID = theList[i].Cell_ID;
                                BatInfo.BATTERY_POS = theList[i].Cell_Position;
                                BatInfo.MODEL_NO = theList[i].MODEL_NO;
                                BatInfo.BATCH_NO = "";
                                BatInfo.TOTAL_NG_STATE = theList[i].NgState;
                                BatInfo.OCV_VOLTAGE = Math.Round(theList[i].OCV_Now, 4);
                                //BatInfo.ACIR = theList[i].ACIR_Now;
                                BatInfo.TEST_NG_CODE = theList[i].Test_NgResult.NgCode;
                                BatInfo.TEST_RESULT = theList[i].Test_NgResult.NgState;
                                BatInfo.TEST_RESULT_DESC = theList[i].Test_NgResult.NgDescribe;


                                BatInfo.PostiveSHELL_VOLTAGE = Math.Round(theList[i].OCV_ShellPostive_Now, 4);
                                BatInfo.PostiveSV_NG_CODE = theList[i].PostiveSV_NgResult.NgCode;
                                BatInfo.PostiveSV_RESULT = theList[i].PostiveSV_NgResult.NgCode;
                                BatInfo.PostiveSV_RESULT_DESC = theList[i].PostiveSV_NgResult.NgCode;

                                BatInfo.SHELL_VOLTAGE = Math.Round(theList[i].OCV_Shell_Now, 4);
                                BatInfo.SV_NG_CODE = theList[i].SV_NgResult.NgCode;
                                BatInfo.SV_RESULT = theList[i].SV_NgResult.NgCode;
                                BatInfo.SV_RESULT_DESC = theList[i].SV_NgResult.NgCode;


                                BatInfo.POSTIVE_TEMP = theList[i].PostiveTMP;
                                BatInfo.NEGATIVE_TEMP = theList[i].NegativeTMP;
                                BatInfo.REV_OCV = Math.Round(theList[i].Rev_OCV, 4); ;
                                BatInfo.END_DATE_TIME = theList[i].End_Write_Time;
                                mlstBatInfo.Add(BatInfo);
                            }
                            break;
                        case 2:     //OCV3
                            if (!theList[i].Cell_ID.Contains("NullCellCode"))
                            {
                                //BatInfo.Eqp_ID = ClsGlobal.DeviceCode;
                                //BatInfo.PC_ID = SystemInformation.ComputerName; 
                                BatInfo.Eqp_ID = "QT-" + ClsGlobal.DeviceCode;
                                BatInfo.PC_ID = ClsGlobal.PCID;
                                BatInfo.OPERATION = ClsGlobal.OPEATION_ID;
                                BatInfo.IS_TRANS = isTrans;
                                BatInfo.TRAY_ID = theList[i].Pallet_ID.Trim();
                                BatInfo.CELL_ID = theList[i].Cell_ID;
                                BatInfo.BATTERY_POS = theList[i].Cell_Position;
                                BatInfo.MODEL_NO = theList[i].MODEL_NO;
                                BatInfo.BATCH_NO = "";
                                BatInfo.TOTAL_NG_STATE = theList[i].NgState;
                                BatInfo.OCV_VOLTAGE = Math.Round(theList[i].OCV_Now, 4);
                                BatInfo.ACIR = Math.Round(theList[i].ACIR_Now, 4);
                                BatInfo.TEST_NG_CODE = theList[i].Test_NgResult.NgCode;
                                BatInfo.TEST_RESULT = theList[i].Test_NgResult.NgState;
                                BatInfo.TEST_RESULT_DESC = theList[i].Test_NgResult.NgDescribe;

                                BatInfo.PostiveSHELL_VOLTAGE = Math.Round(theList[i].OCV_ShellPostive_Now, 4);
                                BatInfo.PostiveSV_NG_CODE = theList[i].PostiveSV_NgResult.NgCode;
                                BatInfo.PostiveSV_RESULT = theList[i].PostiveSV_NgResult.NgState;
                                BatInfo.PostiveSV_RESULT_DESC = theList[i].PostiveSV_NgResult.NgDescribe;

                                BatInfo.SHELL_VOLTAGE = Math.Round(theList[i].OCV_Shell_Now, 4);
                                BatInfo.SV_NG_CODE = theList[i].SV_NgResult.NgCode;
                                BatInfo.SV_RESULT = theList[i].SV_NgResult.NgState;
                                BatInfo.SV_RESULT_DESC = theList[i].SV_NgResult.NgDescribe;


                                BatInfo.POSTIVE_TEMP = theList[i].PostiveTMP;
                                BatInfo.NEGATIVE_TEMP = theList[i].NegativeTMP;
                                BatInfo.V_DROP = theList[i].VoltDrop_Now;
                                BatInfo.V_DROP_RESULT = theList[i].VoltDrop_NowState.NgState;
                                BatInfo.K = theList[i].K_Now;
                                BatInfo.V_DROP_RANGE = theList[i].DROP_range;
                                BatInfo.V_DROP_RANGE_CODE = theList[i].ACIR_NgResult.NgCode;
                                //BatInfo.V_DROP_RESULT = theList[i].ACIR_NgResult.NgState;
                                BatInfo.V_DROP_RESULT_DESC = theList[i].ACIR_NgResult.NgDescribe;
                                BatInfo.ACIR_RANGE = theList[i].ACIR_range;
                                BatInfo.R_RANGE_NG_CODE = theList[i].ACIR_NgResult.NgCode;
                                BatInfo.R_RANGE_RESULT = theList[i].ACIR_NgResult.NgState;
                                BatInfo.R_RANGE_RESULT_DESC = theList[i].ACIR_NgResult.NgDescribe;
                                BatInfo.REV_OCV = Math.Round(theList[i].Rev_OCV, 4);
                                BatInfo.END_DATE_TIME = theList[i].End_Write_Time;
                                mlstBatInfo.Add(BatInfo);
                            }

                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 电池信息: 脱机时, 返回自动生成的电池信息列表,不在数据库获取
        /// </summary>
        public int Get_ETCell_Offline(string Pallet_ID, out List<ET_CELL> listCell)
        {
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            List<C_CellBindShow> cellList = CellBindBLL.Instance.GetModelList(" TrayCode= '" + ClsGlobal.TraycodeA + "'");
            if (cellList == null)
            {
                throw new Exception("没有绑定数据！");
            }
            try
            {
                for (int i = 1; i <= ClsGlobal.TrayType; i++)
                {
                    var query = (from p in cellList where p.Channel == i.ToString() select p).ToList();
                    if (query.Count >= 1)
                    {
                        ET_CELL etCell = new ET_CELL();
                        etCell.Cell_ID = query[0].CellCode;
                        etCell.Pallet_ID = Pallet_ID;
                        etCell.Cell_Position = i;
                        etCell.MODEL_NO = "001";
                        list.Add(etCell);
                    }
                }
                listCell = list;
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //debug
        public int test_ETCell_Offline(string Pallet_ID, out List<ET_CELL> listCell)
        {
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            try
            {
                for (int i = 1; i <= ClsGlobal.TrayType; i++)
                {
                    ET_CELL etCell = new ET_CELL();
                    etCell.Cell_ID = "ID" + i;
                    etCell.Pallet_ID = Pallet_ID;
                    etCell.Cell_Position = i;

                    etCell.MODEL_NO = "123";
                    etCell.OCV_Now = 1;
                    etCell.ACIR_Now = 2;
                    etCell.OCV_Shell_Now = 3;
                    etCell.VoltDrop_Now = 4;
                    etCell.ACIR_range = 6;

                    etCell.DROP_range = 5;
                    if (i % 2 == 0)
                    {
                        etCell.Test_NgResult.NgState = "NG0";
                        etCell.Test_NgResult.NgDescribe = "电压超下限";
                        etCell.DROP_NgResult.NgState = "NG";
                        etCell.DROP_NgResult.NgDescribe = "压降极差超下限";
                        etCell.SV_NgResult.NgState = "OK1";
                        etCell.SV_NgResult.NgDescribe = "合格1";
                        etCell.ACIR_NgResult.NgState = "NG2";
                        etCell.ACIR_NgResult.NgDescribe = "合格2";


                    }
                    else
                    {
                        etCell.Test_NgResult.NgState = "OK0";
                        etCell.Test_NgResult.NgDescribe = "HG";
                        etCell.DROP_NgResult.NgState = "OK";
                        etCell.DROP_NgResult.NgDescribe = "合格";
                        etCell.SV_NgResult.NgState = "NG1";
                        etCell.SV_NgResult.NgDescribe = "keya超下限";
                        etCell.ACIR_NgResult.NgState = "OK2";
                        etCell.ACIR_NgResult.NgDescribe = "A超下限";
                    }

                    etCell.PostiveTMP = 21;
                    etCell.NegativeTMP = 22.2;
                    etCell.Rev_OCV = 18;
                    //etCell.Capacity = ClsGlobal.listETCELL[i].Capacity.ToString();
                    etCell.Capacity = 0;
                    etCell.End_Write_Time = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    list.Add(etCell);
                }
                listCell = list;
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        ///电池信息: 更新BatInfo电池信息到ET_CELL  
        /// </summary>
        /// <param name="OCVType"></param>
        /// <param name="lstBatInfo"></param>
        /// <param name="listCell"></param>
        private void ReflectBatInfoToETCell_KVal(int OCVType, List<Model.BatInfo_Model> lstBatInfo, ref List<ET_CELL> listCell)
        {
            int channelCount = lstBatInfo.Count;
            List<ET_CELL> list = new List<ET_CELL>();

            ushort batNum = 0;

            if (lstBatInfo.Count == 0)
            {
                return;
            }
            if (OCVType == 3)
            {

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {

                    ET_CELL etCell = new ET_CELL();
                    etCell = ClsGlobal.listETCELL[i];
                    for (int j = 0; j < lstBatInfo.Count; j++)
                    {
                        if ((lstBatInfo[j].BATTERY_POS) == (i + 1) && !lstBatInfo[j].CELL_ID.Contains("NullCellCode"))
                        {
                            etCell.OCV_1or2 = (double)lstBatInfo[i].OCV_VOLTAGE;
                            etCell.TEST_DATE = lstBatInfo[i].END_DATE_TIME;
                            etCell.HNGSt = lstBatInfo[i].TEST_RESULT;
                            list.Add(etCell);
                            if (lstBatInfo[i].TEST_RESULT == "OK")
                            {
                                batNum++;
                            }
                        }
                    }
                }

            }
            ClsGlobal.BatNum = batNum;
            listCell = list;
        }

        public DataTable GetProcessList(string strOCV_type, string strBatteryType)
        {
            DataTable DT_Info = new DataTable();
            DT_Info = batData.GetProcessList(strOCV_type, strBatteryType);
            return DT_Info;
        }

        /// <summary>
        /// 获取工艺参数的实体对象
        /// </summary>
        public ProcessInfo DataRowToProModel(DataRow row)
        {
            return batData.DataRowToProModel(row);
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        public int GetUesrInfo(string UesrName, string UserPwd)
        {
            try
            {
                ClsLogs.UserinfologNet.WriteWarn("验证用户密码", "账号：" + UesrName);
                return batData.GetUesrInfo(UesrName, UserPwd);
            }
            catch (Exception ex)
            {
                ClsLogs.UserinfologNet.WriteWarn("验证用户异常", ex.Message);
                return 0;
            }

        }

        /// <summary>
        /// 验证用户
        /// </summary>
        public bool ExistsUesrInfo(string UesrName)
        {
            try
            {
                ClsLogs.UserinfologNet.WriteWarn("验证用户", "账号：" + UesrName);
                return batData.ExistsUesr(UesrName);
            }
            catch (Exception ex)
            {
                ClsLogs.UserinfologNet.WriteWarn("验证用户异常", ex.Message);
                return false;
            }

        }
    }

}
