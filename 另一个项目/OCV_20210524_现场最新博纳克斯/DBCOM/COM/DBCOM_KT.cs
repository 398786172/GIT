using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OCV;

namespace DB_KT.DAL
{
    //擎天数据库访问接口
    public class DBCOM_KT
    {
        //DAL.TrayList DAL_TrayList = new DAL.TrayList();
        DAL.FlowValue DAL_FlowValue; // = new DAL.FlowValue();
        DAL.BatInfo_Year DAL_BatInfo_Data;  // = new DAL.BatInfo_2019();
        DAL.BatInfo DAL_BatInfo_Flow;


        public DBCOM_KT(string iP, string database, string userID, string pWD , string BatInfoTable)
        {            
            StringBuilder StrB = new StringBuilder();
            string IP = iP;
            string Database = database;
            string UserID = userID;
            string PWD = pWD;
            StrB.Append("Data Source=" + IP);
            StrB.Append(" ;Initial Catalog=" + Database);
            StrB.Append(" ;User ID=" + UserID);
            StrB.Append(" ;Password=" + PWD);
            StrB.Append(" ;Integrated Security=no");

            DAL_FlowValue = new FlowValue(StrB.ToString());
            DAL_BatInfo_Data = new BatInfo_Year(StrB.ToString(), BatInfoTable);
            DAL_BatInfo_Flow = new BatInfo(StrB.ToString());

        }


        //电池信息----------------------------------------------------

        /// <summary>
        /// 获得数据库电池_数据部分_信息
        /// </summary>
        public int GetList_BattInfo_Data(string DeviceCode, out List<Model.BatInfo_Year> lstBatInfo)
        {
            try
            {
                //没有获取到电池信息:1
                //电池条码重复:2
                //电池位置号重复:3
                //电池数据数量多于托盘容量:4
                //正常:0

                lstBatInfo = null;
                Model.BatInfo_Year theModel = DAL_BatInfo_Data.GetModel_LatestTime(DeviceCode);
                if (theModel == null)
                {
                    return 1;
                }
                
                lstBatInfo = DAL_BatInfo_Data.GetList_LatestTime(theModel);

                //ClsGlobal.WriteLog("lstBatInfo.Count"+ lstBatInfo.Count+ "|ClsGlobal.TrayType="+ ClsGlobal.TrayType, ClsGlobal.sDebugOCVSelectionPath);

                if (lstBatInfo.Count > ClsGlobal.TrayType)
                {
                    return 4;
                }
                else if (lstBatInfo == null)
                {
                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 获得数据库电池_流程部分_信息
        /// </summary>
        public int GetList_BattInfo_Flow(string DeviceCode, out List<Model.BatInfo> lstBatInfo)
        {
            try
            {
                lstBatInfo = null;
                Model.BatInfo theModel = new Model.BatInfo();

                theModel = DAL_BatInfo_Flow.GetModel_LatestTime(DeviceCode);
                if (theModel == null)
                {
                    return 1;
                }

                lstBatInfo = DAL_BatInfo_Flow.GetList_LatestTime(theModel);

                if (lstBatInfo.Count > ClsGlobal.TrayType)
                {
                    return 4;
                }
                else if (lstBatInfo == null)
                {
                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 电池信息: 脱机时, 返回自动生成的电池信息列表,不在数据库获取
        /// </summary>
        public int GetList_ETCell_Offline(string Pallet_ID, out List<ET_CELL> listCell)
        {
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            try
            {
                for (int i = 1; i < ClsGlobal.TrayType + 1; i++)
                {
                    ET_CELL etCell = new ET_CELL();
                    etCell.Cell_ID = "ID" + i;
                    etCell.Pallet_ID = Pallet_ID;
                    etCell.Cell_Position = i;
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
        /// 更新电池_数据部分_信息: 更新电池的电压,内阻参数, 设备号, 结束时间等
        /// </summary>
        public bool Update_BattInfo_Data(List<Model.BatInfo_Year> list)
        {
            try
            {
                return DAL_BatInfo_Data.UpdateList_BattInfo(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// BH20200304
        /// </summary>
        /// <param name="list"></param>
        /// <param name="istGradeName"></param>
        /// <param name="istSqlScale"></param>
        /// <returns></returns>
        public bool Update_BattInfo_OCVGrade(List<Model.BatInfo_Year> list,long  istGradeName,string istSqlScale,out int recode)
        {
            try
            {
                return DAL_BatInfo_Data.UpdateList_BattOCVGrade(list, istGradeName, istSqlScale,out recode);
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog("更新数据库等级出错"+ex.ToString(), ClsGlobal.sDebugOCVSelectionPath);
                throw ex;
            }
        }

        public bool Update_BattInfo_SingleOCVGrade(List<Model.BatInfo_Year> list, long istGradeName, string barCode, out int recode)
        {
            try
            {
                //return DAL_BatInfo_Data.UpdateList_BattOCVGrade(list, istGradeName, istSqlScale, out recode);
                string LoginTime = "";
                foreach (var item in list)
                {
                    if (item.BatCode== barCode)
                    {
                        LoginTime = item.LoginTime.ToString();
                        break;
                    }
                }

                return DAL_BatInfo_Data.UpdateSingleOCVGrade(barCode, LoginTime, istGradeName,  out recode);
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog("更新数据库等级出错" + ex.ToString(), ClsGlobal.sDebugOCVSelectionPath);
                throw ex;
            }
        }

        public bool ClearOCVGrade(List<Model.BatInfo_Year> list)
        {
            try
            {
                return DAL_BatInfo_Data.OCVClearGrade(list);
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog("清零数据库等级出错" + ex.ToString(), ClsGlobal.sDebugOCVSelectionPath);
                throw ex;
            }
        }
        /// <summary>
        /// 获取均值及方差
        /// </summary>
        /// <param name="list"></param>
        /// <param name="colomn"></param>
        /// <returns></returns>
        public DataSet getAvgAndStdev(List<Model.BatInfo_Year> list, string colomn,string extraSql)
        {
           
            try
            {
                return DAL_BatInfo_Data.GetAvgStdev(list,colomn,extraSql);
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog("获取均值和方差出错" + ex.ToString(), ClsGlobal.sDebugOCVSelectionPath);
                throw ex;
            }
        }

        public DataSet getRecode(List<Model.BatInfo_Year> list, string colomn,string extraSql)
        {
            string sqlStr = extraSql;
            string trayCode = "";
            string loginTime = "";

            foreach (var item in list)
            {
                if (item.DeviceCode!="")
                {
                    trayCode = item.DeviceCode;
                    loginTime = item.LoginTime.ToString();
                    break;
                }
                else
                {
                    continue;
                }
            }

           
            try
            {
                return DAL_BatInfo_Data.GetRecord(colomn, trayCode,loginTime,sqlStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取指定电池条码
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet getBatCode(List<Model.BatInfo_Year> list)
        {

            try
            {
                return DAL_BatInfo_Data.GetBatCode(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新电池_流程部分_信息
        /// </summary>
        public bool Update_BattInfo_Flow(List<Model.BatInfo> list)
        {
            try
            {
                return DAL_BatInfo_Flow.UpdateList_BattInfo(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //流程信息----------------------------------------------------   
        //获取电池所属的工程的流程列表
        public int GetList_ProjFlow(Model.BatInfo_Year BatInfo, out List<Model.FlowValue> lstProjFlow)
        {
            try
            {
                lstProjFlow = null;

                lstProjFlow = DAL_FlowValue.GetList_ProjFlow(BatInfo.ProcessID);
                if (lstProjFlow == null)
                {
                    return 1;
                }

                return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      
        public bool ExcuteCheckAddColumn(string tableName, string columnName, string columnType)
        {
            try
            {
                return DAL_BatInfo_Data.CheckAndAddColumn(tableName, columnName, columnType);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }



    }

}
