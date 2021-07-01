using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OCV
{
    /// <summary>
    /// ZD接口控制
    /// </summary>
    public class ClsDBZhongDingContr
    {
        private static string SqlConnectionString = "Data Source=" + ClsGlobal.Server_QT_IP + ";" +
                             "Initial Catalog=" + ClsGlobal.Server_QT_DB + ";" +
                             "User ID=" + ClsGlobal.Server_QT_id + ";" +
                             "Password=" + ClsGlobal.Server_QT_Pwd + ";" +
                             "Integrated Security=no";
        //用于测试
        //private static string SqlConnectionString = "Data Source=127.0.0.1" + ";" +
                                          //"Initial Catalog=BYD_DB_BL" + ";" +
                                          //"User ID=sa" + ";" +
                                          //"Password=123" + ";" +
                                          //"Integrated Security=no";


        public static ArrayList alCellID = new ArrayList();//保存电池编码信息
        static string tableName = "BatInfo";
        public static int iChannelCount = 0;//通道数
        public static bool bPing = false;
        public static DateTime LoginTime;           //当前托盘登录时间
        Thread ThreadTestAction;

         //获取电池信息线程
        public void sGetCellInfo(string Pallet_ID)
        {
            ThreadTestAction = new Thread(new ParameterizedThreadStart(mGetCellInfo));
            ThreadTestAction.Start(Pallet_ID);
        }
         //获取电池信息
        private void mGetCellInfo(object Para)
        {
            try
            {
                // ret = MESCOM.GetCellInfo(mProc.TrayCode, out ClsGlobal.listETCELL);
                ClsGlobal.ret = GetCellInfo(Para.ToString(), out ClsGlobal.listETCELL);

            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog(ex.Message);
                ClsGlobal.ret = 1;               
            }
            
        }

        /// <summary>
        /// 返回电池信息列表
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <param name="listCell">电池信息列表</param>
        /// <returns>
        /// success:0 
        /// 没有获取到电池信息:1
        /// 电池条码重复:2
        /// 电池位置号重复:3
        /// 电池数据数量不匹配:4
        /// </returns>
        private static int GetCellInfo(string Pallet_ID, out List<ET_CELL> listCell)
        {
            int channelCount = 0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            string strSql;
            string BatCode;
            string LotID;
            try
            {
               int re = GetLoginTimeInfo(Pallet_ID);
               if (LoginTime.ToString() != "" && re==0)
                { 
                     strSql = "SELECT BatCode, COUNT (*) as repeatNum FROM " + tableName +
                            " where DeviceCode='" + Pallet_ID + "' AND LoginTime = '" + LoginTime +
                            "' group by BatCode having COUNT (*)>1";    
              
                    ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
                    dt = ds.Tables[0];
                    int r = dt.Rows.Count;
                    if (r > 0)
                    {
                        WriteLog("电池条码有重复,托盘号为：" + Pallet_ID + "，登录时间为：" + LoginTime + "！");
                        return 2;
                    }

                    strSql = "SELECT BatteryPos , COUNT (*) as repeatNum FROM " + tableName +
                                " where DeviceCode='" + Pallet_ID + "' AND LoginTime = '" + LoginTime +
                                "' group by BatteryPos having COUNT (*)>1";
                    ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        WriteLog("电池位置号有重复,托盘号为：" + Pallet_ID + "，登录时间为：" + LoginTime + "！");
                        return 3;
                    }
                    strSql = "SELECT PROJECT_NO , COUNT (*) as repeatNum FROM " + tableName +
                               " where DeviceCode='" + Pallet_ID + "' AND LoginTime = '" + LoginTime +
                               "' group by PROJECT_NO having COUNT (*)>1";
                    ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count != 1)
                    {
                        WriteLog("电池项目编号不唯一,托盘号为：" + Pallet_ID + "，登录时间为：" + LoginTime + "！");
                        return 5;
                    }
                    //strSql = "SELECT LotID , COUNT (*) as repeatNum FROM " + tableName +
                    //        " where DeviceCode='" + Pallet_ID + "' AND LoginTime = '" + LoginTime +
                    //        "' group by LotID having COUNT (*)>1";
                    //ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
                    //dt = ds.Tables[0];
                    //if (dt.Rows.Count != 1)
                    //{
                    //    WriteLog("电池批次编唯一,托盘号为：" + Pallet_ID + "，登录时间为：" + LoginTime + "！");
                    //    return 6;
                    //}

                    strSql = "select BatCode,DeviceCode, BatteryPos,Capacity1 ,PROJECT_NO ,LotID ,FlowEndTime1 from " + tableName + " where DeviceCode='" +
                        Pallet_ID + "' AND LoginTime = '" +LoginTime + "' order by BatteryPos";                

                    ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
                    dt = ds.Tables[0];
                    channelCount = dt.Rows.Count;

                    if (channelCount != 80)
                    {
                        //return 4;   //暂时取消报警
                    }
                    else if (channelCount == 0)
                    {
                        WriteLog("没有获取到电池信息,托盘号为：" + Pallet_ID + "，登录时间为：" + LoginTime + "！");
                        return 1;
                    }
                    int Chk;
                    for (int i = 0; i < 80; i++)
                    {
                        ET_CELL etCell = new ET_CELL();                   
                        Chk = 0;
                        for (int j = 0; j < channelCount; j++)
                        {
                            if (Convert.ToInt16(dt.Rows[j]["BatteryPos"]) == (i + 1))
                            {
                            
                           
                                BatCode = ((string)dt.Rows[j]["BatCode"]).Trim();
                                etCell.Cell_ID = BatCode;
                                etCell.Pallet_ID = ((string)dt.Rows[j]["DeviceCode"]).Trim();
                                etCell.Cell_Position = Convert.ToInt16(dt.Rows[j]["BatteryPos"]);
                                etCell.Capacity = (dt.Rows[j]["Capacity1"]).ToString();
                                etCell.FlowEndTime = (dt.Rows[j]["FlowEndTime1"].ToString()).Trim(); ;
                                LotID = ((string)dt.Rows[j]["LotID"]).Trim();
                                etCell.BATCH_NO = LotID;
                                string[] strArray = BatCode.Split(new string[] { LotID }, StringSplitOptions.RemoveEmptyEntries);
                                etCell.MODEL_NO = strArray[0];
                                etCell.PROJECT_NO = ((string)dt.Rows[j]["PROJECT_NO"]).Trim();
                                list.Add(etCell);
                                Chk = 1;
                                break;
                            }
                        }
                        if (Chk == 0)
                        {
                            etCell.Cell_ID = "NullCellCode_" + (i + 1);   //空电池条码
                            etCell.Pallet_ID = ((string)dt.Rows[0]["DeviceCode"]).Trim();
                            etCell.Cell_Position = (i + 1);
                            list.Add(etCell);
                        }
                    }               
                    listCell = list;
                    return 0;
                }
                else
                {
                    return 7;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return 1;
            }
            finally
            {
                ds = null;
                dt = null;
            }
        }

        /// <summary>
        /// 返回托盘最新登录时间
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <returns>
        /// success:0 
        /// 没有获取到登录信息:1
        /// </returns>
        /// 
        private static int GetLoginTimeInfo(string Pallet_ID)
        {
            int channelCount = 0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();   
            string strSql;
            try
            {
                strSql = "select LoginTime from " + tableName + " where DeviceCode='" +
                         Pallet_ID.Trim() + "' order by LoginTime DESC";             
                ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
                dt = ds.Tables[0];        
                channelCount = dt.Rows.Count;
                if (channelCount == 0)
                {
                    return 1;
                }
                LoginTime = (DateTime)dt.Rows[0]["LoginTime"]; 
                return 0;
            }
            catch (Exception ex)
            {
                WriteLog("没有获取到托盘号为：" + Pallet_ID + "，登录时间.");
                return 1;
                throw ex;
            }
            finally
            {
                ds = null;
                dt = null;
            }
        }
       
        private static void WriteLog(string Mess)
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\log";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = path + "\\CommSql";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                using (FileStream fs = new FileStream(path + "\\" +"ReadSql_"+ DateTime.Now.ToString("yyyyMMdd") + ".log", FileMode.Append))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString() + " " + Mess);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
        #region old
        /// <summary>
        /// 返回电池信息列表
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <param name="listCell">电池信息列表</param>
        /// <returns>success:0, fail:1</returns>
        //public static int GetCellInfo_Offline(string Pallet_ID, out List<ET_CELL> listCell)
        //{
        //    List<ET_CELL> list = new List<ET_CELL>();
        //    listCell = null;
        //    try
        //    {
        //        for (int i = 1; i < ClsGlobal.TrayType + 1; i++)
        //        {
        //            ET_CELL etCell = new ET_CELL();
        //            etCell.Cell_ID = "ID" + i;
        //            etCell.Pallet_ID = Pallet_ID;
        //            etCell.Cell_Position = i;// -1;
        //            list.Add(etCell);
        //        }
        //        listCell = list;
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 1;
        //    }

        //}


        //public static int GetTrayCellType()
        //{
        //    //1:72 2:xx 3:xx
        //    if (ClsGlobal.TrayType == 72)
        //    {
        //        return 1;   //成泰只有一种
        //    }
        //    return 1;
        //}

        //public static int UpdataOCVACIRData(List<ET_CELL> listCell)
        //{
        //    int UpdateCount = 0;
        //    List<ET_CELL> theList = listCell;

        //    try
        //    {
        //        //OCV
        //        string sOCV_V1 = "OCV" + ClsGlobal.OCVType.ToString() + "_V1";
        //        string sOCV_V2 = "OCV" + ClsGlobal.OCVType.ToString() + "_V2";
        //        string sOCV_Temp = "OCV" + ClsGlobal.OCVType.ToString() + "_T";
        //        string sOCV_Time = "OCV" + ClsGlobal.OCVType.ToString() + "_Write_Time";
        //        //ACIR
        //        string sACIR_R1 = "ACIR" + ClsGlobal.OCVType.ToString() + "_R1";
        //        string sACIR_R2 = "ACIR" + ClsGlobal.OCVType.ToString() + "_R2";
        //        string sACIR_Temp = "ACIR" + ClsGlobal.OCVType.ToString() + "_T";
        //        string sACIR_Time = "ACIR" + ClsGlobal.OCVType.ToString() + "_Write_Time";

        //        for (int i = 0; i < theList.Count; i++)
        //        {
        //            UpdateRecord_VOLT_ACIR(ClsGlobal.mZDTableName, theList[i].Pallet_ID.Trim(), theList[i].Cell_ID.Trim(),
        //                 sOCV_V1, theList[i].OCV_V1,
        //                 sOCV_V2, theList[i].OCV_V2,
        //                 sOCV_Temp, theList[i].TMP,
        //                 sOCV_Time, theList[i].OCV_Write_Time,
        //                 sACIR_R1, theList[i].ACIR_R1,
        //                 sACIR_R2, theList[i].ACIR_R2,
        //                 sACIR_Temp, theList[i].TMP,
        //                 sACIR_Time, theList[i].ACIR_Write_Time
        //                 );
        //            UpdateCount++;
        //        }
        //        return UpdateCount;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        ///// <summary>
        ///// 更新OCV数据
        ///// </summary>
        ///// <param name="listCell">电池信息列表</param>
        ///// <returns>返回更新数据量</returns>
        //public static int UpdateOCVData(List<ET_CELL> listCell)
        //{
        //    int UpdateCount = 0;
        //    List<ET_CELL> theList = listCell;

        //    try
        //    {
        //        string sOCV_V1 = "OCV" + ClsGlobal.OCVType.ToString() + "_V1";
        //        string sOCV_V2 = "OCV" + ClsGlobal.OCVType.ToString() + "_V2";
        //        string sOCV_Temp = "OCV" + ClsGlobal.OCVType.ToString() + "_T";
        //        string sOCV_Time = "OCV" + ClsGlobal.OCVType.ToString() + "_Write_Time";

        //        for (int i = 0; i < theList.Count; i++)
        //        {
        //            UpdateRecord_VOLT(ClsGlobal.mZDTableName, theList[i].Pallet_ID, theList[i].Cell_ID,
        //                 sOCV_V1, theList[i].OCV_V1,
        //                 sOCV_V2, theList[i].OCV_V2,
        //                 sOCV_Temp, theList[i].TMP,
        //                 sOCV_Time, theList[i].OCV_Write_Time);
        //            UpdateCount++;
        //        }
        //        return UpdateCount;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        ///// <summary>
        ///// 更新ACIR数据
        ///// </summary>
        ///// <param name="listCell">电池信息列表</param>
        ///// <returns>返回更新数据量</returns>
        //public static int UpdateACIRData(List<ET_CELL> listCell)
        //{
        //    int UpdateCount = 0;
        //    List<ET_CELL> theList = listCell;

        //    try
        //    {
        //        string sACIR_R1 = "ACIR" + ClsGlobal.OCVType.ToString() + "_R1";
        //        string sACIR_R2 = "ACIR" + ClsGlobal.OCVType.ToString() + "_R2";
        //        string sACIR_Temp = "ACIR" + ClsGlobal.OCVType.ToString() + "_T";
        //        string sACIR_Time = "ACIR" + ClsGlobal.OCVType.ToString() + "_Write_Time";

        //        for (int i = 0; i < theList.Count; i++)
        //        {
        //            UpdateRecord_ACIR(ClsGlobal.mZDTableName, theList[i].Pallet_ID, theList[i].Cell_ID,
        //                 sACIR_R1, theList[i].ACIR_R1,
        //                 sACIR_R2, theList[i].ACIR_R2,
        //                 sACIR_Temp, theList[i].TMP,
        //                 sACIR_Time, theList[i].ACIR_Write_Time);
        //            UpdateCount++;
        //        }
        //        return UpdateCount;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}



        ////更新记录
        //private static int UpdateRecord_VOLT_ACIR(string sTableName, string sTray_ID, string sCell_ID,
        //                                string sOCV_V1, double sOCV_V1_Value,       //OCV_V1
        //                                string sOCV_V2, double sOCV_V2_Value,       //OCV_V2
        //                                string sOCVTmp, double dOCVTempValue,       //OCV温度
        //                                string sOCVTime, string sOCVTimeValue,      //OCV时间
        //                                string sACIR_R1, double dACIR_R1_Value,     //ACIR_R1
        //                                string sACIR_R2, double dACIR_R2_Value,     //ACIR_R2
        //                                string sACIRTmp, double dACIRTempValue,     //ACIR温度
        //                                string sACIRTime, string sACIRTimeValue     //ACIR时间                                         
        //    )
        //{
        //    int _iResult = 0;
        //    try
        //    {
        //        DateTime dtime = System.DateTime.Now;
        //        string strSql = "Update " + sTableName + " set " +
        //                        sOCV_V1 + "=" + sOCV_V1_Value.ToString("f1") +
        //                        "," + sOCV_V2 + "=" + sOCV_V2_Value.ToString("f1") +
        //                        "," + sOCVTmp + "=" + dOCVTempValue +
        //                        "," + sOCVTime + "='" + sOCVTimeValue + "'" +
        //                        "," + sACIR_R1 + "=" + dACIR_R1_Value.ToString("f2") +
        //                        "," + sACIR_R2 + "=" + dACIR_R2_Value.ToString("f2") +
        //                        "," + sACIRTmp + "=" + dACIRTempValue +
        //                        "," + sACIRTime + "='" + sACIRTimeValue + "'" +
        //                        " where Cell_ID='" + sCell_ID + "' and Pallet_ID='" + sTray_ID + "'";
        //        _iResult = SqlHelper.ExecuteNonQuery(SqlConnectionString, CommandType.Text, strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _iResult;



        //}

        ///// <summary>
        ///// 更新OCV数据记录 
        ///// </summary>
        //private static int UpdateRecord_VOLT(string sTableName, string sTray_ID, string sCell_ID,
        //                                string sOCV, double dOCVvalue,
        //                                string sOCV_shell, double dOCVValueShell,
        //                                 string sOCVTmp, double dOCVTempValue,
        //                                 string sOCVTime, string sOCVTimeValue
        //                                 )
        //{
        //    int _iResult = 0;
        //    try
        //    {
        //        DateTime dtime = System.DateTime.Now;
        //        string strSql = "Update " + sTableName + " set " +
        //                        sOCV + "=" + dOCVvalue.ToString("f1") +
        //                        "," + sOCV_shell + "=" + dOCVValueShell.ToString("f1") +
        //                        "," + sOCVTmp + "=" + dOCVTempValue +
        //                        "," + sOCVTime + "='" + sOCVTimeValue +
        //                        "' where Cell_ID='" + sCell_ID + "' and Pallet_ID='" + sTray_ID + "'";
        //        _iResult = SqlHelper.ExecuteNonQuery(SqlConnectionString, CommandType.Text, strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _iResult;
        //}


        ////更新ACIR数据记录
        //private static int UpdateRecord_ACIR(string sTableName, string sTray_ID, string sCell_ID,
        //                               string sACIR_R1, double dACIR_R1_Value,
        //                               string sACIR_R2, double dACIR_R2_Value,
        //                                string sACIRTmp, double dACIRTempValue,
        //                                string sACIRTime, string sACIRTimeValue
        //                                )
        //{
        //    int _iResult = 0;
        //    try
        //    {
        //        DateTime dtime = System.DateTime.Now;
        //        string strSql = "Update " + sTableName + " set " +
        //                        sACIR_R1 + "=" + dACIR_R1_Value.ToString("f2") +
        //                        "," + sACIR_R2 + "=" + dACIR_R2_Value.ToString("f2") +
        //                        "," + sACIRTmp + "=" + dACIRTempValue +
        //                        "," + sACIRTime + "='" + sACIRTimeValue +
        //                        "' where Cell_ID='" + sCell_ID + "' and Pallet_ID='" + sTray_ID + "'";
        //        _iResult = SqlHelper.ExecuteNonQuery(SqlConnectionString, CommandType.Text, strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _iResult;
        //}






        ////--------------------------原程序

        ///// <summary>
        ///// 返回通道的数量，取得电池编码集合
        ///// </summary>
        ///// <param name="sPallet_ID">托盘编码</param>
        ///// <returns>通道数</returns>
        //public static int GetCellID(string sPallet_ID)
        //{
        //    int _ichannelcount = 0;
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string strSql = "select Cell_ID from v_cell_ocv_dcir_info where Pallet_ID='" + sPallet_ID + "' order by Cell_Position";

        //        ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
        //        dt = ds.Tables[0];
        //        _ichannelcount = dt.Rows.Count;
        //        alCellID.Clear();
        //        if (_ichannelcount > 0)
        //        {
        //            for (int i = 0; i < _ichannelcount; i++)
        //            {
        //                alCellID.Add(dt.Rows[i][0]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        ds = null;
        //        dt = null;
        //    }
        //    return _ichannelcount;
        //}

        //public static int GetCellID_Position(string sPallet_ID, out Dictionary<string, int> dPosition)
        //{
        //    int _ichannelcount = 0;
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    dPosition = new Dictionary<string, int>();
        //    try
        //    {
        //        string strSql = "select Cell_ID,Cell_Position from v_cell_ocv_dcir_info  where Pallet_ID='" + sPallet_ID + "' order by Cell_Position";

        //        ds = SqlHelper.ExecuteDataset(SqlConnectionString, CommandType.Text, strSql, null);
        //        dt = ds.Tables[0];
        //        _ichannelcount = dt.Rows.Count;
        //        if (_ichannelcount > 0)
        //        {
        //            for (int i = 0; i < _ichannelcount; i++)
        //            {
        //                dPosition.Add(dt.Rows[i]["Cell_ID"].ToString().Trim(), Convert.ToInt32(dt.Rows[i]["Cell_Position"].ToString()));

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //PubClass.WriteLogToExe(ex.Message);
        //        ClsGlobal.WriteLog(ex.Message);
        //    }
        //    finally
        //    {
        //        ds = null;
        //        dt = null;
        //    }
        //    return _ichannelcount;
        //}
        #endregion
    }
}
        
