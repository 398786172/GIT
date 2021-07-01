using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OCV
{
    /// <summary>
    /// QT接口控制
    /// </summary>
    public class ClsDBQingtianContr
    {
    
        //private static string ConnStr = "Data Source=127.0.0.1" + ";" +
        //                                    "Initial Catalog=BYD_DB_BL_OCV" + ";" +
        //                                    "User ID=sa" + ";" +
        //                                    "Password=123" + ";" +
        //                                    "Integrated Security=no";
        private static string ConnStr = "Data Source=" + ClsGlobal.Server_Local_IP + ";" +
                                  "Initial Catalog=" + ClsGlobal.Server_Local_DB + ";" +
                                  "User ID=" + ClsGlobal.Server_Local_id + ";" +
                                  "Password=" + ClsGlobal.Server_Local_Pwd + ";" +
                                  "Integrated Security=no";
        //public static bool bPing = false;
        static bool flag = false;    //写OCV总表标志
        public static string testTime;   
        //插入OCV ACIR数据     
        public static int InsertOCVACIRData(List<ET_CELL> listCell)
        {
            int _iResult;
            int InsertCount = 0;
            List<ET_CELL> theList = listCell;
            try
            {
                string SID;
                int re = 0;               
                if (1 == ClsGlobal.OCVType)   //写OCV1表
                {                    
                    for (int i = 0; i < theList.Count; i++)
                    {
                        SID = System.Guid.NewGuid().ToString();
                        _iResult = InsertRecord(SID, "MES2_JHW_OCV1", ClsGlobal.SITE, theList[i].Cell_ID.Trim(), theList[i].Pallet_ID.Trim(), theList[i].Cell_Position, "OCV1" , ClsGlobal.RESRCE,
                                                theList[i].MODEL_NO, theList[i].PROJECT_NO, theList[i].BATCH_NO, theList[i].OCV_V1, theList[i].ACIR_R1, theList[i].Capacity, "", "", "", "", "", "", "", "", theList[i].NGSt,
                                                theList[i].NGRea, ClsGlobal.USER_NO, theList[i].End_Write_Time, theList[i].End_Write_Time, ClsGlobal.IS_TRANS, "", ClsGlobal.OCVType);
                        if (1 == _iResult)
                        {
                            InsertCount++;
                        }
                    }
                }
               
                else if (2 == ClsGlobal.OCVType && re ==0)   //写OCV2表
                {
                    for (int i = 0; i < theList.Count; i++)
                    {
                        
                        SID = System.Guid.NewGuid().ToString();
                        _iResult = InsertRecord(SID, "MES2_JHW_OCV2", ClsGlobal.SITE, theList[i].Cell_ID.Trim(), theList[i].Pallet_ID.Trim(), theList[i].Cell_Position, "OCV2", ClsGlobal.RESRCE,
                                                theList[i].MODEL_NO, theList[i].PROJECT_NO, theList[i].BATCH_NO, theList[i].Volt1, theList[i].ACIR_1, theList[i].Capacity, theList[i].TEST_DATE, theList[i].OCV_V1, theList[i].ACIR_R1,
                                                theList[i].VoltToCompare, "", "", "", "", theList[i].NGSt, theList[i].NGRea, ClsGlobal.USER_NO, theList[i].End_Write_Time, theList[i].End_Write_Time, ClsGlobal.IS_TRANS, "", ClsGlobal.OCVType);
                        if (1 == _iResult)
                        {
                            InsertCount++;
                        }
                    } 

                }
                else if (3 == ClsGlobal.OCVType && re == 0)   //写OCV3表
                {

                    for (int i = 0; i < theList.Count; i++)
                    {
                        SID = System.Guid.NewGuid().ToString();
                        _iResult = InsertRecord(SID, "MES2_JHW_OCV3", ClsGlobal.SITE, theList[i].Cell_ID.Trim(), theList[i].Pallet_ID.Trim(), theList[i].Cell_Position, "OCV" + ClsGlobal.OCVType, ClsGlobal.RESRCE,
                                                theList[i].MODEL_NO, theList[i].PROJECT_NO, theList[i].BATCH_NO, theList[i].Volt1, theList[i].ACIR_1, theList[i].Capacity, theList[i].TEST_DATE, theList[i].Volt2, theList[i].ACIR_2,
                                                theList[i].VoltDrop1, theList[i].TEST_DATE2, theList[i].OCV_V1, theList[i].ACIR_R1, theList[i].VoltToCompare, theList[i].NGSt, theList[i].NGRea, ClsGlobal.USER_NO,
                                                theList[i].End_Write_Time, theList[i].End_Write_Time, ClsGlobal.IS_TRANS, "", ClsGlobal.OCVType);
                        if (1 == _iResult)
                        {
                            InsertCount++;
                        }
                    } 

                }
                for (int i = 0; i < theList.Count; i++)   //写当前测试表
                {
                    flag = true;
                    SID = System.Guid.NewGuid().ToString();
                    _iResult = InsertRecord(SID, "MES2_TOTAL_OCV", ClsGlobal.SITE, theList[i].Cell_ID.Trim(), theList[i].Pallet_ID.Trim(), theList[i].Cell_Position, "OCV" + ClsGlobal.OCVType, ClsGlobal.RESRCE,
                                            theList[i].MODEL_NO, theList[i].PROJECT_NO, theList[i].BATCH_NO, theList[i].OCV_V1, theList[i].ACIR_R1, theList[i].Capacity, theList[i].End_Write_Time, "", "",
                                            "", "", "", "", "", theList[i].NGSt, theList[i].NGRea, ClsGlobal.USER_NO, theList[i].End_Write_Time, theList[i].End_Write_Time, ClsGlobal.IS_TRANS, theList[i].VoltToCompare, ClsGlobal.OCVType);
                    if (1 == _iResult)
                    {
                        InsertCount++;
                    }
                }
                flag = false;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message,"WriteSql");
                return InsertCount;
                throw ex;
            }
            return InsertCount;
        }

        /// <summary>
        /// 插入OCV 以及 ACIR数据记录
        /// </summary>
        /// <param name="sTableName">表名</param>
        /// <param name="SITE">站点</param> 现场设置
        /// <param name="SFC">电池条码</param> 擎天数据库获取
        /// <param name="DeviceCode">托盘号</param>
        /// <param name="iBatteryPos">电池位置</param>
        /// <param name="OPERATION">工序</param> 现场设置, 根据当前托盘要做OCV1,2,3决定
        /// <param name="RESRCE">岗位条码</param> 现场设置
        /// <param name="MODEL_NO">型号</param> 电池型号，擎天数据库获取      
        /// <param name="PROJECT_NO">项目编号</param> 擎天数据库获取
        /// <param name="BATCH_NO">批号</param>  擎天数据库获取
        /// <param name="VOLTAGE">一次电压</param>
        /// <param name="RESISTANCE">一次内阻</param>
        /// <param name="CAPACITY">容量</param>
        /// <param name="TEST_DATE">一次测试时间</param>
        /// <param name="VOLTAGE2">二次电压</param>
        /// <param name="RESISTANCE2">二次内阻param>
        /// <param name="PRESSURE_DROP2">一二次压降</param>
        /// <param name="TEST_DATE2">二次测试时间</param>
        /// <param name="VOLTAGE3">三次电压</param>
        /// <param name="RESISTANCE3">三次内阻</param> 
        /// <param name="PRESSURE_DROP3">二三次压降param>
        /// <param name="TEST_RESULT">判断结果 OK：合格；NG:不合格</param>
        /// <param name="RESULT_DESC">判断异常描述</param>
        /// <param name="USER_NO">OCV作业员</param>
        /// <param name="END_DATE_TIME">作业时间</param>
        /// <param name="END_DATE_TIME_STR">作业时间</param>
        /// <param name="IS_TRANS">数据转移注记</param>
        /// <param name="PRESSURE_DROP">压降</param>
        /// <param name="OCV_NO">OCV号</param>
        /// <returns></returns>
        private static int InsertRecord(string SID ,           string sTableName,        string SITE,     string SFC,           string sDeviceCode,    int sBatteryPos,    string OPERATION,   string RESRCE,   string MODEL_NO,
                                        string PROJECT_NO,     string BATCH_NO,          string VOLTAGE,  string RESISTANCE,    string CAPACITY,       string TEST_DATE,   string VOLTAGE2,    string RESISTANCE2,
                                        string PRESSURE_DROP2, string TEST_DATE2,        string VOLTAGE3, string RESISTANCE3,   string PRESSURE_DROP3, string TEST_RESULT, string RESULT_DESC, string USER_NO,
                                        string END_DATE_TIME,  string END_DATE_TIME_STR, int IS_TRANS,    string PRESSURE_DROP, int OCV_NO)
        {
            int _iResult = 0;
            try
            {
               
                string strSql = "";
                if(1==ClsGlobal.OCVType && flag == false)   //写OCV1表
                {
                    strSql = "insert into " + sTableName + "(SID,SITE,SFC,DEVICE_CODE,BATTERY_POS,OPERATION,RESRCE,MODEL_NO,PROJECT_NO,BATCH_NO,VOLTAGE,RESISTANCE,CAPACITY,TEST_RESULT,RESULT_DESC,USER_NO,END_DATE_TIME,END_DATE_TIME_STR,IS_TRANS" + ")" +
                                    "values('" + SID.ToUpper() + "','" + SITE + "','" + SFC + "','" + sDeviceCode + "','" + sBatteryPos + "','" + OPERATION + "','" + RESRCE + "','" + MODEL_NO + "','" + PROJECT_NO + "','" + BATCH_NO +
                                    "','" + VOLTAGE + "','" + RESISTANCE + "','" + CAPACITY + "','" + TEST_RESULT + "','" + RESULT_DESC + "','" + USER_NO + "','" + END_DATE_TIME +
                                    "','" + END_DATE_TIME_STR + "','" + IS_TRANS + "')";
                    _iResult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                    
                }
                else if (2 == ClsGlobal.OCVType && flag == false)  //写OCV2表
                {
                    strSql = "insert into " + sTableName + "(SID,SITE,SFC,DEVICE_CODE,BATTERY_POS,OPERATION,RESRCE,MODEL_NO,PROJECT_NO,BATCH_NO,VOLTAGE,RESISTANCE,CAPACITY,TEST_DATE,VOLTAGE2,RESISTANCE2,PRESSURE_DROP2,TEST_RESULT,RESULT_DESC,USER_NO,END_DATE_TIME,END_DATE_TIME_STR,IS_TRANS" + ")" +
                                    "values('" + SID.ToUpper() + "','" + SITE + "','" + SFC + "','" + sDeviceCode + "','" + sBatteryPos + "','" + OPERATION + "','" + RESRCE + "','" + MODEL_NO + "','" + PROJECT_NO + "','" + BATCH_NO + "','" + VOLTAGE + "','" + RESISTANCE +
                                    "','" + CAPACITY + "','" + TEST_DATE + "','" + VOLTAGE2 + "','" + RESISTANCE2.ToString() + "','" + PRESSURE_DROP2 + "','" + TEST_RESULT + "','" + RESULT_DESC + "','" + USER_NO + "','" + END_DATE_TIME +
                                    "','" + END_DATE_TIME_STR + "','" + IS_TRANS + "')";
                    _iResult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                }
                else if (3 == ClsGlobal.OCVType && flag == false)   //写OCV3表
                {
                    strSql = "insert into " + sTableName + "(SID,SITE,SFC,DEVICE_CODE,BATTERY_POS,OPERATION,RESRCE,MODEL_NO,PROJECT_NO,BATCH_NO,VOLTAGE,RESISTANCE,CAPACITY,TEST_DATE,VOLTAGE2,RESISTANCE2,PRESSURE_DROP2,TEST_DATE2,VOLTAGE3,RESISTANCE3,PRESSURE_DROP3,TEST_RESULT,RESULT_DESC,USER_NO,END_DATE_TIME,END_DATE_TIME_STR,IS_TRANS" + ")" +                  
                                "values('" + SID.ToUpper() + "','" + SITE + "','" + SFC + "','" + sDeviceCode + "','" + sBatteryPos + "','" + OPERATION + "','" + RESRCE + "','" + MODEL_NO + "','" + PROJECT_NO + "','" + BATCH_NO + "','" + VOLTAGE + "','" + RESISTANCE +
                                    "','" + CAPACITY + "','" + TEST_DATE + "','" + VOLTAGE2 + "','" + RESISTANCE2 + "','" + PRESSURE_DROP2 + "','" + TEST_DATE2 + "','" + VOLTAGE3 + "','" + RESISTANCE3.ToString() +
                                    "','" + PRESSURE_DROP3 + "','" + TEST_RESULT + "','" + RESULT_DESC + "','" + USER_NO + "','" + END_DATE_TIME + "','" + END_DATE_TIME_STR + "','" + IS_TRANS + "')";
                    _iResult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                }

                else  //写当前测试表OCV总表 
                {
                    if (1 == ClsGlobal.OCVType)   
                    {
                        strSql = "insert into " + sTableName + "(SID,SITE,SFC,DEVICE_CODE,BATTERY_POS,OPERATION,RESRCE,MODEL_NO,PROJECT_NO,BATCH_NO,VOLTAGE,RESISTANCE,CAPACITY,TEST_RESULT,RESULT_DESC,USER_NO,END_DATE_TIME,END_DATE_TIME_STR,IS_TRANS,PRESSURE_DROP,OCV_NO" + ")" +
                                            "values('" + SID.ToUpper() + "','" + SITE + "','" + SFC + "','" + sDeviceCode + "','" + sBatteryPos + "','" + OPERATION + "','" + RESRCE + "','" + MODEL_NO + "','" + PROJECT_NO + "','" + BATCH_NO +
                                            "','" + VOLTAGE + "','" + RESISTANCE + "','" + CAPACITY + "','" + TEST_RESULT + "','" + RESULT_DESC + "','" + USER_NO + "','" + END_DATE_TIME +
                                            "','" + END_DATE_TIME_STR + "','" + IS_TRANS + "','" + null + "','" + OCV_NO + "')";
                    }
                    else
                    {
                        strSql = "insert into " + sTableName + "(SID,SITE,SFC,DEVICE_CODE,BATTERY_POS,OPERATION,RESRCE,MODEL_NO,PROJECT_NO,BATCH_NO,VOLTAGE,RESISTANCE,CAPACITY,TEST_RESULT,RESULT_DESC,USER_NO,END_DATE_TIME,END_DATE_TIME_STR,IS_TRANS,PRESSURE_DROP,OCV_NO" + ")" +
                                                                 "values('" + SID.ToUpper() + "','" + SITE + "','" + SFC + "','" + sDeviceCode + "','" + sBatteryPos + "','" + OPERATION + "','" + RESRCE + "','" + MODEL_NO + "','" + PROJECT_NO + "','" + BATCH_NO +
                                                                 "','" + VOLTAGE + "','" + RESISTANCE + "','" + CAPACITY + "','" + TEST_RESULT+ "','" + RESULT_DESC + "','" + USER_NO + "','" + END_DATE_TIME +
                                                                 "','" + END_DATE_TIME_STR + "','" + IS_TRANS + "','" + PRESSURE_DROP + "','" + OCV_NO + "')";
                    }
                       _iResult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                }

            }
            catch (Exception ex)
            {
                WriteLog("上传数据失败,托盘号为：" + sDeviceCode + "，测试时间为：" + END_DATE_TIME + "！","WriteSql");
                WriteLog(ex.Message,"WriteSql");               
            }
            return _iResult;
        }


        /// <summary>
        /// 返回托盘最新登录时间
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <returns>
        /// success:0 
        /// 没有获取到测试信息:1,2
        /// </returns>    
        public static int GetLoginTimeInfo(string Pallet_ID)
        {
            int channelCount = 0,num=0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string tableName="";
            string strSql;
            try
            {
                if (1 == ClsGlobal.OCVType)
                {
                    return 2; 
                }
                else if (2 == ClsGlobal.OCVType)
                {
                    tableName = "MES2_JHW_OCV1";
                    num = 1;

                }
                else if (3 == ClsGlobal.OCVType)
                {
                    tableName = "MES2_JHW_OCV2";
                    num = 2;
                }
                strSql = "select END_DATE_TIME_STR from " + tableName + " where DEVICE_CODE='" +
                    Pallet_ID.Trim() + "' order by END_DATE_TIME_STR DESC";
                ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, strSql, null);
                dt = ds.Tables[0];
                testTime = dt.Rows[0]["END_DATE_TIME_STR"].ToString();
                channelCount = dt.Rows.Count;
                if (channelCount == 0)
                {
                    return num;
                }
                return 0;
            }
            catch (Exception ex)
            {
                WriteLog("读取OCV数据失败，原因："+ex.ToString(), "RadSql_");
                return 3;
            }
            finally
            {
                ds = null;
                dt = null;
            }
        }

        /// <summary>
        /// 返回OCV1  OCV2测试数据
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <returns>
        /// success:0 
        /// 没有获取到测试信息:1,2
        /// </returns>
        public static int GetData(string Pallet_ID, out List<ET_CELL> listCell)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = ClsGlobal.listETCELL;
            string strSql;
            try
            {  
                int re = GetLoginTimeInfo(Pallet_ID);
                if (re == 0)
                {
                    if (2 == ClsGlobal.OCVType)
                    {
                        strSql = "select VOLTAGE,RESISTANCE, END_DATE_TIME_STR,BATTERY_POS,TEST_RESULT from " + "MES2_JHW_OCV1" + " where DEVICE_CODE='" +
                        Pallet_ID.Trim() + "' AND END_DATE_TIME_STR = '" + testTime + "' order by BATTERY_POS";
                        ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, strSql, null);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            WriteLog("没有OCV1测试数据,托盘号为：" + Pallet_ID, "RadSql_");
                            return 1;
                        }
                        for (int i = 0; i < 80; i++)
                        {
                            ET_CELL etCell = new ET_CELL();
                            etCell = ClsGlobal.listETCELL[i];
                            for (int j = 0; j < 80; j++)
                            {
                                if (Convert.ToInt16(dt.Rows[j]["BATTERY_POS"]) == (i + 1))
                                {
                                    etCell.Volt1 = (dt.Rows[j]["VOLTAGE"]).ToString();
                                    etCell.ACIR_1 = (dt.Rows[j]["RESISTANCE"]).ToString();
                                    etCell.TEST_DATE = (dt.Rows[j]["END_DATE_TIME_STR"]).ToString();   
                                    etCell.HNGSt = (dt.Rows[j]["TEST_RESULT"]).ToString();
                                    list.Add(etCell);
                                }
                            }
                        }                     
                        listCell = list;
                        return 0;
                    }
                    else if (3 == ClsGlobal.OCVType)
                    {
                        strSql = "select VOLTAGE,RESISTANCE, TEST_DATE,VOLTAGE2,RESISTANCE2,PRESSURE_DROP2,END_DATE_TIME_STR,BATTERY_POS,TEST_RESULT from " + "MES2_JHW_OCV2" + " where DEVICE_CODE='" +
                        Pallet_ID.Trim() + "' AND END_DATE_TIME_STR = '" + testTime + "' order by BATTERY_POS";
                        ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, strSql, null);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            WriteLog("没有OCV2测试数据,托盘号为：" + Pallet_ID, "RadSql_");
                            return 2;
                        }
                        for (int i = 0; i < 80; i++)
                        {
                            ET_CELL etCell = new ET_CELL();
                            etCell = ClsGlobal.listETCELL[i];
                            for (int j = 0; j < 80; j++)
                            {
                                if (Convert.ToInt16(dt.Rows[j]["BATTERY_POS"]) == (i + 1))
                                {

                                    etCell.Volt1 = (dt.Rows[j]["VOLTAGE"]).ToString();
                                    etCell.ACIR_1 = (dt.Rows[j]["RESISTANCE"]).ToString();
                                    etCell.TEST_DATE = (dt.Rows[j]["TEST_DATE"]).ToString();
                                    etCell.Volt2 = (dt.Rows[j]["VOLTAGE2"]).ToString();
                                    etCell.ACIR_2 = (dt.Rows[j]["RESISTANCE2"]).ToString();
                                    etCell.VoltDrop1 = (dt.Rows[j]["PRESSURE_DROP2"]).ToString();
                                    etCell.TEST_DATE2 = (dt.Rows[j]["END_DATE_TIME_STR"]).ToString();
                                    etCell.HNGSt = (dt.Rows[j]["TEST_RESULT"]).ToString();   
                                    list.Add(etCell);
                                }
                            }
                        }
                        listCell = list;
                        return 0;
                    }
                    return 0;
                }
                else if (re==2)
                {
                    WriteLog("测试OCV1，不获取数据,托盘号为：" + Pallet_ID, "RadSql_");
                    return 4;
                }  
                else if(re!=3)
                {
                    WriteLog("获取OCV" + re + "数据失败,托盘号为：" + Pallet_ID, "RadSql_");
                    return re;
                }
                else 
                {         
                    WriteLog("获取OCV登录时间出错!托盘号为：" + Pallet_ID, "RadSql_");
                    return re;
                   
                }
            }
            catch (Exception ex)
            {
                WriteLog("读取OCV数据失败，原因：" + ex.ToString(), "RadSql_");
                return 3;
            }
            finally
            {
                ds = null;
                dt = null;
            }
                 
        }
       
        #region 没有用到的函数
        ///////// <summary>
        ///////// 更新数据
        ///////// </summary>
        /////// <param name="sTableName">表名</param>
        /////// <param name="sBatCode">电池条码号</param>
        /////// <param name="DeviceCode">托盘号</param>
        /////// <param name="iBatteryPos">电池位置</param>
        /////// <param name="dOCVValue">电压值</param>
        /////// <param name="sLoginTime">托盘登录时间</param>
        /////// <param name="sOCVTimeValue">OCV测试时间</param>
        /////// <param name="dCapacity">容量值</param>
        /////// <param name="cNGStatus">NG状态</param>
        /////// <param name="sNGReason">NG原因</param>
        /////// <param name="dACIR_R1_Value">内阻值R1</param>    
        ///////// <returns></returns>
        ////public static int UpdataOCVACIRData(List<ET_CELL> listCell)
        ////{
        ////    int UpdateCount = 0;
        ////    List<ET_CELL> theList = listCell;
        ////    try
        ////    {
        ////        //OCV
        ////        string sOCV_V1 = "OCV" + ClsGlobal.OCVType.ToString() + "_V1";                      
        ////        //ACIR
        ////        string sACIR_R1 = "ACIR" + ClsGlobal.OCVType.ToString() + "_R1";             
        ////        string sACIR_Time = "ACIR" + ClsGlobal.OCVType.ToString() + "_Write_Time";

        ////        for (int i = 0; i < theList.Count; i++)
        ////        {
        ////            UpdateRecord_VOLT_ACIR(ClsGlobal.mOCVTable, theList[i].Pallet_ID.Trim(), theList[i].Cell_ID.Trim(),
        ////                 sOCV_V1, theList[i].OCV_V1,
        ////                 sOCV_V2, theList[i].OCV_V2,
        ////                 sOCV_Temp, theList[i].TMP,
        ////                 sOCV_Time, theList[i].OCV_Write_Time,
        ////                 sACIR_R1, theList[i].ACIR_R1,
        ////                 sACIR_R2, theList[i].ACIR_R2,
        ////                 sACIR_Temp, theList[i].TMP,
        ////                 sACIR_Time, theList[i].ACIR_Write_Time
        ////                 );
        ////            UpdateCount++;
        ////        }
        ////        return UpdateCount;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        //////更新记录
        ////private static int UpdateRecord_VOLT_ACIR(string sTableName, string sTray_ID, string sCell_ID,
        ////                                string sOCV_V1, double sOCV_V1_Value,       //OCV_V1
        ////                                string sOCV_V2, double sOCV_V2_Value,       //OCV_V2
        ////                                string sOCVTmp, double dOCVTempValue,       //OCV温度
        ////                                string sOCVTime, string sOCVTimeValue,      //OCV时间
        ////                                string sACIR_R1, double dACIR_R1_Value,     //ACIR_R1
        ////                                string sACIR_R2, double dACIR_R2_Value,     //ACIR_R2
        ////                                string sACIRTmp, double dACIRTempValue,     //ACIR温度
        ////                                string sACIRTime, string sACIRTimeValue     //ACIR时间                                         
        ////    )
        ////{
        ////    int _iResult = 0;
        ////    try
        ////    {
        ////        DateTime dtime = System.DateTime.Now;
        ////        string strSql = "Update " + sTableName + " set " +
        ////                        sOCV_V1 + "=" + sOCV_V1_Value.ToString("f1") +
        ////                        "," + sOCV_V2 + "=" + sOCV_V2_Value.ToString("f1") +
        ////                        "," + sOCVTmp + "=" + dOCVTempValue +
        ////                        "," + sOCVTime + "='" + sOCVTimeValue + "'" +
        ////                        "," + sACIR_R1 + "=" + dACIR_R1_Value.ToString("f2") +
        ////                        "," + sACIR_R2 + "=" + dACIR_R2_Value.ToString("f2") +
        ////                        "," + sACIRTmp + "=" + dACIRTempValue +
        ////                        "," + sACIRTime + "='" + sACIRTimeValue + "'" +
        ////                        " where Cell_ID='" + sCell_ID + "' and Pallet_ID='" + sTray_ID + "'";
        ////        _iResult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////    return _iResult;
        ////}

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="sBatCode">电池条码</param>
        ///// <param name="sOcvType">OCV类型</param>
        ///// <param name="docvalue">OCV值</param>
        ///// <param name="sTableName">表名</param>
        ///// <param name="sTimearr">测试时间</param>
        ///// <returns></returns>
        //private static int UpdateRecord(string sBatCode, string sOcvType, double docvalue,
        //                                    string sTableName, string sTimearr)
        //{
        //    int _iResult = 0;
        //    try
        //    {
        //        string strSql = "Update " + sTableName + " set WorkedTime='" + sTimearr +
        //                        "'," + sOcvType + "=" + docvalue + " where BatCode='" + sBatCode + "'";
        //        _iResult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        //PubClass.WriteLogToExe(ex.Message);
        //        ClsGlobal.WriteLog(ex.Message);
        //    }
        //    return _iResult;
        //}
        ///// <summary>
        ///// 是否存在相同的电池型号
        ///// </summary> 
        ///// <param name="sBatCode">电池代码</param>
        ///// <returns>返回True表示为真</returns>
        //private static bool bExistRecord(string sBatCode, string sTableName)
        //{
        //    bool _bExist = false;
        //    try
        //    {
        //        string strSql = "select * from " + sTableName + " where BatCode='" + sBatCode + "'";
        //        System.Data.DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, strSql, null);
        //        dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            _bExist = true;
        //        }
        //        dt = null;
        //        ds = null;
        //    }
        //    catch (Exception ex)
        //    {                
        //        ClsGlobal.WriteLog(ex.Message);
        //    }
        //    return _bExist;
        //}

        ////<summary>
        ////擎天取得数据
        ////</summary>
        //public static int QinTianGetData(string[] G_dbl_TimeArr)
        //{
        //    int _iResult = 0;
        //    try
        //    {
        //        if (ClsDBZhongDingContr.alCellID.Count <= 0)
        //        {
        //            ClsDBZhongDingContr.GetCellID(ClsGlobal.TrayCode);
        //        }

        //        if (DBattery_Position != null)
        //        {
        //            DBattery_Position.Clear();
        //        }

        //        ClsDBZhongDingContr.GetCellID_Position(ClsGlobal.TrayCode, out DBattery_Position);

        //        double _dOcvValue = 0.0;
        //        double _dOcvValue_shell = 0.0;
        //        string _sBatCode = "";
        //        int _itype = ClsGlobal.QTOCVType;
        //        string _sOcvType = null;
        //        switch (_itype)
        //        {
        //            case 1://OCV1
        //                _sOcvType = "OCV1";
        //                break;
        //            case 2: //OCV2
        //                _sOcvType = "OCV2";
        //                break;
        //            case 3://OCV3
        //                _sOcvType = "OCV3";
        //                break;
        //            case 4://OCV4
        //                _sOcvType = "OCV4";
        //                break;
        //            default:
        //                break;
        //        }
        //        int _ivolStartIndex = 0;

        //        string _sTableName = "OCV" + ClsGlobal.QTOCVType.ToString() + "Data";
        //        string _sTimearr = null;

        //        for (int i = 0; i < ClsDBZhongDingContr.alCellID.Count; i++)
        //        {
        //            _sBatCode = ClsDBZhongDingContr.alCellID[i].ToString().Trim();

        //            string temp = (ClsGlobal.G_dbl_DataArr[i] * 1000).ToString();
        //            _dOcvValue = Convert.ToDouble(temp.Substring(0, 7));

        //            if (_itype == 4)
        //            {
        //                temp = (ClsGlobal.G_dbl_VshellArr[i] * 1000).ToString();
        //                _dOcvValue_shell = Convert.ToDouble(temp.Substring(0, 7));
        //            }
        //            else
        //            {
        //                _dOcvValue_shell = 0;
        //            }

        //            //if (PubClass.sVoltage1.Length >= _ivolStartIndex + 7)
        //            //{
        //            //    _dOcvValue = Convert.ToDouble(PubClass.sVoltage1.Substring(_ivolStartIndex, 7));
        //            //}
        //            //else
        //            //{
        //            //    _dOcvValue = 0.0;
        //            //}
        //            ///////////////
        //            //if (PubClass.sVoltage2.Length >= _ivolStartIndex + 7)
        //            //{
        //            //    _dOcvValue_shell = Convert.ToDouble(PubClass.sVoltage2.Substring(_ivolStartIndex, 7));
        //            //}
        //            //else
        //            //{
        //            //    _dOcvValue_shell = 0.0;
        //            //}
        //            /////////////////
        //            //_ivolStartIndex = _ivolStartIndex + 7;


        //            //判断是否存在，如果存在，就更新数据，不存在，就插入数据
        //            if (G_dbl_TimeArr != null)
        //            {
        //                _sTimearr = G_dbl_TimeArr[i].ToString();
        //            }
        //            else
        //            {
        //                _sTimearr = System.DateTime.Now.ToString();
        //            }
        //            int _iBatChannel = 0;
        //            if (DBattery_Position.Keys.Contains(_sBatCode))
        //            {
        //                _iBatChannel = Convert.ToInt32(DBattery_Position[_sBatCode].ToString());
        //            }
        //            _iResult = InsertRecord(_sBatCode, _itype, _dOcvValue, _dOcvValue_shell, _sTableName, _sTimearr, _iBatChannel, ClsGlobal.TrayCode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //PubClass.WriteLogToExe(ex.Message);
        //        ClsGlobal.WriteLog(ex.Message);
        //        throw ex;
        //    }
        //    return _iResult;
        //}
        #endregion

        private static void WriteLog(string Mess,string mode)
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\log";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = path + "\\CommSql";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                using (FileStream fs = new FileStream(path + "\\" + mode + DateTime.Now.ToString("yyyyMMdd") + ".log", FileMode.Append))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString() + " " + Mess);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
