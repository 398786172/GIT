using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using OCV;


namespace DB_KT.DAL
{

    public partial class BatInfo_Year
    {
        DbHelperSQLP DbHelperSQL;
        string m_TableName = "BatInfo_Year";        //年表名

        //public BatInfo_Year(string strConn)
        //{
        //    if (strConn == null)
        //    {
        //        throw new Exception("输入参数错误");
        //    }

        //    DbHelperSQL = new DbHelperSQLP(strConn);
        //}

        public BatInfo_Year(string strConn, string tableName)
        {
            if (strConn == null || tableName == null)
            {
                throw new Exception("输入参数错误");
            }

            DbHelperSQL = new DbHelperSQLP(strConn);
            m_TableName = tableName.Trim();
        }

        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ProcessID", m_TableName);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BatCode, DateTime LoginTime, string DeviceCode, int ProcessID, string LotID, int BatteryPos, long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + m_TableName);
            strSql.Append(" where BatCode='" + BatCode + "' and LoginTime='" + LoginTime + "' and DeviceCode='" + DeviceCode + "' and ProcessID=" + ProcessID + " and LotID='" + LotID + "' and BatteryPos=" + BatteryPos + " and ID=" + ID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(DB_KT.Model.BatInfo_Year model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.BatCode != null)
            {
                strSql1.Append("BatCode,");
                strSql2.Append("'" + model.BatCode + "',");
            }
            if (model.LoginTime != null)
            {
                strSql1.Append("LoginTime,");
                strSql2.Append("'" + model.LoginTime + "',");
            }
            if (model.DeviceCode != null)
            {
                strSql1.Append("DeviceCode,");
                strSql2.Append("'" + model.DeviceCode + "',");
            }
            if (model.ProcessID != null)
            {
                strSql1.Append("ProcessID,");
                strSql2.Append("" + model.ProcessID + ",");
            }
            if (model.FlowID != null)
            {
                strSql1.Append("FlowID,");
                strSql2.Append("" + model.FlowID + ",");
            }
            if (model.LotID != null)
            {
                strSql1.Append("LotID,");
                strSql2.Append("'" + model.LotID + "',");
            }
            if (model.ColCode != null)
            {
                strSql1.Append("ColCode,");
                strSql2.Append("'" + model.ColCode + "',");
            }
            if (model.BatteryPos != null)
            {
                strSql1.Append("BatteryPos,");
                strSql2.Append("" + model.BatteryPos + ",");
            }
            if (model.GroupCode != null)
            {
                strSql1.Append("GroupCode,");
                strSql2.Append("'" + model.GroupCode + "',");
            }
            if (model.GradeName != null)
            {
                strSql1.Append("GradeName,");
                strSql2.Append("'" + model.GradeName + "',");
            }
            if (model.MatchName != null)
            {
                strSql1.Append("MatchName,");
                strSql2.Append("'" + model.MatchName + "',");
            }
            if (model.MatchFlag != null)
            {
                strSql1.Append("MatchFlag,");
                strSql2.Append("" + model.MatchFlag + ",");
            }
            if (model.Capacity1 != null)
            {
                strSql1.Append("Capacity1,");
                strSql2.Append("" + model.Capacity1 + ",");
            }
            if (model.Capacity2 != null)
            {
                strSql1.Append("Capacity2,");
                strSql2.Append("" + model.Capacity2 + ",");
            }
            if (model.Capacity3 != null)
            {
                strSql1.Append("Capacity3,");
                strSql2.Append("" + model.Capacity3 + ",");
            }
            if (model.Capacity4 != null)
            {
                strSql1.Append("Capacity4,");
                strSql2.Append("" + model.Capacity4 + ",");
            }
            if (model.Capacity5 != null)
            {
                strSql1.Append("Capacity5,");
                strSql2.Append("" + model.Capacity5 + ",");
            }
            if (model.Capacity6 != null)
            {
                strSql1.Append("Capacity6,");
                strSql2.Append("" + model.Capacity6 + ",");
            }
            if (model.Voltage1 != null)
            {
                strSql1.Append("Voltage1,");
                strSql2.Append("" + model.Voltage1 + ",");
            }
            if (model.Voltage2 != null)
            {
                strSql1.Append("Voltage2,");
                strSql2.Append("" + model.Voltage2 + ",");
            }
            if (model.Voltage3 != null)
            {
                strSql1.Append("Voltage3,");
                strSql2.Append("" + model.Voltage3 + ",");
            }
            if (model.Voltage4 != null)
            {
                strSql1.Append("Voltage4,");
                strSql2.Append("" + model.Voltage4 + ",");
            }
            if (model.Voltage5 != null)
            {
                strSql1.Append("Voltage5,");
                strSql2.Append("" + model.Voltage5 + ",");
            }
            if (model.Voltage6 != null)
            {
                strSql1.Append("Voltage6,");
                strSql2.Append("" + model.Voltage6 + ",");
            }
            if (model.Current1 != null)
            {
                strSql1.Append("Current1,");
                strSql2.Append("" + model.Current1 + ",");
            }
            if (model.Current2 != null)
            {
                strSql1.Append("Current2,");
                strSql2.Append("" + model.Current2 + ",");
            }
            if (model.OCV1 != null)
            {
                strSql1.Append("OCV1,");
                strSql2.Append("" + model.OCV1 + ",");
            }
            if (model.OCV2 != null)
            {
                strSql1.Append("OCV2,");
                strSql2.Append("" + model.OCV2 + ",");
            }
            if (model.OCV3 != null)
            {
                strSql1.Append("OCV3,");
                strSql2.Append("" + model.OCV3 + ",");
            }
            if (model.OCV4 != null)
            {
                strSql1.Append("OCV4,");
                strSql2.Append("" + model.OCV4 + ",");
            }
            if (model.OCV5 != null)
            {
                strSql1.Append("OCV5,");
                strSql2.Append("" + model.OCV5 + ",");
            }
            if (model.InR1 != null)
            {
                strSql1.Append("InR1,");
                strSql2.Append("" + model.InR1 + ",");
            }
            if (model.InR2 != null)
            {
                strSql1.Append("InR2,");
                strSql2.Append("" + model.InR2 + ",");
            }
            if (model.InR3 != null)
            {
                strSql1.Append("InR3,");
                strSql2.Append("" + model.InR3 + ",");
            }
            if (model.InR4 != null)
            {
                strSql1.Append("InR4,");
                strSql2.Append("" + model.InR4 + ",");
            }
            if (model.InR5 != null)
            {
                strSql1.Append("InR5,");
                strSql2.Append("" + model.InR5 + ",");
            }
            if (model.Delta_V1 != null)
            {
                strSql1.Append("Delta_V1,");
                strSql2.Append("" + model.Delta_V1 + ",");
            }
            if (model.Delta_V2 != null)
            {
                strSql1.Append("Delta_V2,");
                strSql2.Append("" + model.Delta_V2 + ",");
            }
            if (model.K_Value1 != null)
            {
                strSql1.Append("K_Value1,");
                strSql2.Append("" + model.K_Value1 + ",");
            }
            if (model.K_Value2 != null)
            {
                strSql1.Append("K_Value2,");
                strSql2.Append("" + model.K_Value2 + ",");
            }
            if (model.CRate != null)
            {
                strSql1.Append("CRate,");
                strSql2.Append("" + model.CRate + ",");
            }
            if (model.Temperature1 != null)
            {
                strSql1.Append("Temperature1,");
                strSql2.Append("" + model.Temperature1 + ",");
            }
            if (model.Temperature2 != null)
            {
                strSql1.Append("Temperature2,");
                strSql2.Append("" + model.Temperature2 + ",");
            }
            if (model.CapOnTem1 != null)
            {
                strSql1.Append("CapOnTem1,");
                strSql2.Append("" + model.CapOnTem1 + ",");
            }
            if (model.CapOnTem2 != null)
            {
                strSql1.Append("CapOnTem2,");
                strSql2.Append("" + model.CapOnTem2 + ",");
            }
            if (model.DevAddr1 != null)
            {
                strSql1.Append("DevAddr1,");
                strSql2.Append("'" + model.DevAddr1 + "',");
            }
            if (model.DevAddr2 != null)
            {
                strSql1.Append("DevAddr2,");
                strSql2.Append("'" + model.DevAddr2 + "',");
            }
            if (model.DevAddr3 != null)
            {
                strSql1.Append("DevAddr3,");
                strSql2.Append("'" + model.DevAddr3 + "',");
            }
            if (model.DevAddr4 != null)
            {
                strSql1.Append("DevAddr4,");
                strSql2.Append("'" + model.DevAddr4 + "',");
            }
            if (model.DevAddr5 != null)
            {
                strSql1.Append("DevAddr5,");
                strSql2.Append("'" + model.DevAddr5 + "',");
            }
            if (model.DevAddr6 != null)
            {
                strSql1.Append("DevAddr6,");
                strSql2.Append("'" + model.DevAddr6 + "',");
            }
            if (model.DevAddr7 != null)
            {
                strSql1.Append("DevAddr7,");
                strSql2.Append("'" + model.DevAddr7 + "',");
            }
            if (model.DevAddr8 != null)
            {
                strSql1.Append("DevAddr8,");
                strSql2.Append("'" + model.DevAddr8 + "',");
            }
            if (model.DevAddr9 != null)
            {
                strSql1.Append("DevAddr9,");
                strSql2.Append("'" + model.DevAddr9 + "',");
            }
            if (model.DevAddr10 != null)
            {
                strSql1.Append("DevAddr10,");
                strSql2.Append("'" + model.DevAddr10 + "',");
            }
            if (model.FlowEndTime1 != null)
            {
                strSql1.Append("FlowEndTime1,");
                strSql2.Append("'" + model.FlowEndTime1 + "',");
            }
            if (model.FlowEndTime2 != null)
            {
                strSql1.Append("FlowEndTime2,");
                strSql2.Append("'" + model.FlowEndTime2 + "',");
            }
            if (model.FlowEndTime3 != null)
            {
                strSql1.Append("FlowEndTime3,");
                strSql2.Append("'" + model.FlowEndTime3 + "',");
            }
            if (model.FlowEndTime4 != null)
            {
                strSql1.Append("FlowEndTime4,");
                strSql2.Append("'" + model.FlowEndTime4 + "',");
            }
            if (model.FlowEndTime5 != null)
            {
                strSql1.Append("FlowEndTime5,");
                strSql2.Append("'" + model.FlowEndTime5 + "',");
            }
            if (model.FlowEndTime6 != null)
            {
                strSql1.Append("FlowEndTime6,");
                strSql2.Append("'" + model.FlowEndTime6 + "',");
            }
            if (model.FlowEndTime7 != null)
            {
                strSql1.Append("FlowEndTime7,");
                strSql2.Append("'" + model.FlowEndTime7 + "',");
            }
            if (model.FlowEndTime8 != null)
            {
                strSql1.Append("FlowEndTime8,");
                strSql2.Append("'" + model.FlowEndTime8 + "',");
            }
            if (model.FlowEndTime9 != null)
            {
                strSql1.Append("FlowEndTime9,");
                strSql2.Append("'" + model.FlowEndTime9 + "',");
            }
            if (model.FlowEndTime10 != null)
            {
                strSql1.Append("FlowEndTime10,");
                strSql2.Append("'" + model.FlowEndTime10 + "',");
            }
            if (model.Thickness != null)
            {
                strSql1.Append("Thickness,");
                strSql2.Append("" + model.Thickness + ",");
            }
            if (model.Airtight != null)
            {
                strSql1.Append("Airtight,");
                strSql2.Append("" + (model.Airtight ? 1 : 0) + ",");
            }
            if (model.PackCode1 != null)
            {
                strSql1.Append("PackCode1,");
                strSql2.Append("'" + model.PackCode1 + "',");
            }
            if (model.PackCode2 != null)
            {
                strSql1.Append("PackCode2,");
                strSql2.Append("'" + model.PackCode2 + "',");
            }
            strSql.Append("insert into " + m_TableName + "(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DB_KT.Model.BatInfo_Year model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + m_TableName + " set ");
            if (model.FlowID != null)
            {
                strSql.Append("FlowID=" + model.FlowID + ",");
            }
            else
            {
                strSql.Append("FlowID= null ,");
            }
            if (model.ColCode != null)
            {
                strSql.Append("ColCode='" + model.ColCode + "',");
            }
            else
            {
                strSql.Append("ColCode= null ,");
            }
            if (model.GroupCode != null)
            {
                strSql.Append("GroupCode='" + model.GroupCode + "',");
            }
            else
            {
                strSql.Append("GroupCode= null ,");
            }
            if (model.GradeName != null)
            {
                strSql.Append("GradeName='" + model.GradeName + "',");
            }
            else
            {
                strSql.Append("GradeName= null ,");
            }
            if (model.MatchName != null)
            {
                strSql.Append("MatchName='" + model.MatchName + "',");
            }
            else
            {
                strSql.Append("MatchName= null ,");
            }
            if (model.MatchFlag != null)
            {
                strSql.Append("MatchFlag=" + model.MatchFlag + ",");
            }
            else
            {
                strSql.Append("MatchFlag= null ,");
            }
            if (model.Capacity1 != null)
            {
                strSql.Append("Capacity1=" + model.Capacity1 + ",");
            }
            else
            {
                strSql.Append("Capacity1= null ,");
            }
            if (model.Capacity2 != null)
            {
                strSql.Append("Capacity2=" + model.Capacity2 + ",");
            }
            else
            {
                strSql.Append("Capacity2= null ,");
            }
            if (model.Capacity3 != null)
            {
                strSql.Append("Capacity3=" + model.Capacity3 + ",");
            }
            else
            {
                strSql.Append("Capacity3= null ,");
            }
            if (model.Capacity4 != null)
            {
                strSql.Append("Capacity4=" + model.Capacity4 + ",");
            }
            else
            {
                strSql.Append("Capacity4= null ,");
            }
            if (model.Capacity5 != null)
            {
                strSql.Append("Capacity5=" + model.Capacity5 + ",");
            }
            else
            {
                strSql.Append("Capacity5= null ,");
            }
            if (model.Capacity6 != null)
            {
                strSql.Append("Capacity6=" + model.Capacity6 + ",");
            }
            else
            {
                strSql.Append("Capacity6= null ,");
            }
            if (model.Voltage1 != null)
            {
                strSql.Append("Voltage1=" + model.Voltage1 + ",");
            }
            else
            {
                strSql.Append("Voltage1= null ,");
            }
            if (model.Voltage2 != null)
            {
                strSql.Append("Voltage2=" + model.Voltage2 + ",");
            }
            else
            {
                strSql.Append("Voltage2= null ,");
            }
            if (model.Voltage3 != null)
            {
                strSql.Append("Voltage3=" + model.Voltage3 + ",");
            }
            else
            {
                strSql.Append("Voltage3= null ,");
            }
            if (model.Voltage4 != null)
            {
                strSql.Append("Voltage4=" + model.Voltage4 + ",");
            }
            else
            {
                strSql.Append("Voltage4= null ,");
            }
            if (model.Voltage5 != null)
            {
                strSql.Append("Voltage5=" + model.Voltage5 + ",");
            }
            else
            {
                strSql.Append("Voltage5= null ,");
            }
            if (model.Voltage6 != null)
            {
                strSql.Append("Voltage6=" + model.Voltage6 + ",");
            }
            else
            {
                strSql.Append("Voltage6= null ,");
            }
            if (model.Current1 != null)
            {
                strSql.Append("Current1=" + model.Current1 + ",");
            }
            else
            {
                strSql.Append("Current1= null ,");
            }
            if (model.Current2 != null)
            {
                strSql.Append("Current2=" + model.Current2 + ",");
            }
            else
            {
                strSql.Append("Current2= null ,");
            }
            if (model.OCV1 != null)
            {
                strSql.Append("OCV1=" + model.OCV1 + ",");
            }
            else
            {
                strSql.Append("OCV1= null ,");
            }
            if (model.OCV2 != null)
            {
                strSql.Append("OCV2=" + model.OCV2 + ",");
            }
            else
            {
                strSql.Append("OCV2= null ,");
            }
            if (model.OCV3 != null)
            {
                strSql.Append("OCV3=" + model.OCV3 + ",");
            }
            else
            {
                strSql.Append("OCV3= null ,");
            }
            if (model.OCV4 != null)
            {
                strSql.Append("OCV4=" + model.OCV4 + ",");
            }
            else
            {
                strSql.Append("OCV4= null ,");
            }
            if (model.OCV5 != null)
            {
                strSql.Append("OCV5=" + model.OCV5 + ",");
            }
            else
            {
                strSql.Append("OCV5= null ,");
            }
            if (model.InR1 != null)
            {
                strSql.Append("InR1=" + model.InR1 + ",");
            }
            else
            {
                strSql.Append("InR1= null ,");
            }
            if (model.InR2 != null)
            {
                strSql.Append("InR2=" + model.InR2 + ",");
            }
            else
            {
                strSql.Append("InR2= null ,");
            }
            if (model.InR3 != null)
            {
                strSql.Append("InR3=" + model.InR3 + ",");
            }
            else
            {
                strSql.Append("InR3= null ,");
            }
            if (model.InR4 != null)
            {
                strSql.Append("InR4=" + model.InR4 + ",");
            }
            else
            {
                strSql.Append("InR4= null ,");
            }
            if (model.InR5 != null)
            {
                strSql.Append("InR5=" + model.InR5 + ",");
            }
            else
            {
                strSql.Append("InR5= null ,");
            }
            if (model.Delta_V1 != null)
            {
                strSql.Append("Delta_V1=" + model.Delta_V1 + ",");
            }
            else
            {
                strSql.Append("Delta_V1= null ,");
            }
            if (model.Delta_V2 != null)
            {
                strSql.Append("Delta_V2=" + model.Delta_V2 + ",");
            }
            else
            {
                strSql.Append("Delta_V2= null ,");
            }
            if (model.K_Value1 != null)
            {
                strSql.Append("K_Value1=" + model.K_Value1 + ",");
            }
            else
            {
                strSql.Append("K_Value1= null ,");
            }
            if (model.K_Value2 != null)
            {
                strSql.Append("K_Value2=" + model.K_Value2 + ",");
            }
            else
            {
                strSql.Append("K_Value2= null ,");
            }
            if (model.CRate != null)
            {
                strSql.Append("CRate=" + model.CRate + ",");
            }
            else
            {
                strSql.Append("CRate= null ,");
            }
            if (model.Temperature1 != null)
            {
                strSql.Append("Temperature1=" + model.Temperature1 + ",");
            }
            else
            {
                strSql.Append("Temperature1= null ,");
            }
            if (model.Temperature2 != null)
            {
                strSql.Append("Temperature2=" + model.Temperature2 + ",");
            }
            else
            {
                strSql.Append("Temperature2= null ,");
            }
            if (model.CapOnTem1 != null)
            {
                strSql.Append("CapOnTem1=" + model.CapOnTem1 + ",");
            }
            else
            {
                strSql.Append("CapOnTem1= null ,");
            }
            if (model.CapOnTem2 != null)
            {
                strSql.Append("CapOnTem2=" + model.CapOnTem2 + ",");
            }
            else
            {
                strSql.Append("CapOnTem2= null ,");
            }
            if (model.DevAddr1 != null)
            {
                strSql.Append("DevAddr1='" + model.DevAddr1 + "',");
            }
            else
            {
                strSql.Append("DevAddr1= null ,");
            }
            if (model.DevAddr2 != null)
            {
                strSql.Append("DevAddr2='" + model.DevAddr2 + "',");
            }
            else
            {
                strSql.Append("DevAddr2= null ,");
            }
            if (model.DevAddr3 != null)
            {
                strSql.Append("DevAddr3='" + model.DevAddr3 + "',");
            }
            else
            {
                strSql.Append("DevAddr3= null ,");
            }
            if (model.DevAddr4 != null)
            {
                strSql.Append("DevAddr4='" + model.DevAddr4 + "',");
            }
            else
            {
                strSql.Append("DevAddr4= null ,");
            }
            if (model.DevAddr5 != null)
            {
                strSql.Append("DevAddr5='" + model.DevAddr5 + "',");
            }
            else
            {
                strSql.Append("DevAddr5= null ,");
            }
            if (model.DevAddr6 != null)
            {
                strSql.Append("DevAddr6='" + model.DevAddr6 + "',");
            }
            else
            {
                strSql.Append("DevAddr6= null ,");
            }
            if (model.DevAddr7 != null)
            {
                strSql.Append("DevAddr7='" + model.DevAddr7 + "',");
            }
            else
            {
                strSql.Append("DevAddr7= null ,");
            }
            if (model.DevAddr8 != null)
            {
                strSql.Append("DevAddr8='" + model.DevAddr8 + "',");
            }
            else
            {
                strSql.Append("DevAddr8= null ,");
            }
            if (model.DevAddr9 != null)
            {
                strSql.Append("DevAddr9='" + model.DevAddr9 + "',");
            }
            else
            {
                strSql.Append("DevAddr9= null ,");
            }
            if (model.DevAddr10 != null)
            {
                strSql.Append("DevAddr10='" + model.DevAddr10 + "',");
            }
            else
            {
                strSql.Append("DevAddr10= null ,");
            }
            if (model.FlowEndTime1 != null)
            {
                strSql.Append("FlowEndTime1='" + model.FlowEndTime1 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime1= null ,");
            }
            if (model.FlowEndTime2 != null)
            {
                strSql.Append("FlowEndTime2='" + model.FlowEndTime2 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime2= null ,");
            }
            if (model.FlowEndTime3 != null)
            {
                strSql.Append("FlowEndTime3='" + model.FlowEndTime3 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime3= null ,");
            }
            if (model.FlowEndTime4 != null)
            {
                strSql.Append("FlowEndTime4='" + model.FlowEndTime4 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime4= null ,");
            }
            if (model.FlowEndTime5 != null)
            {
                strSql.Append("FlowEndTime5='" + model.FlowEndTime5 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime5= null ,");
            }
            if (model.FlowEndTime6 != null)
            {
                strSql.Append("FlowEndTime6='" + model.FlowEndTime6 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime6= null ,");
            }
            if (model.FlowEndTime7 != null)
            {
                strSql.Append("FlowEndTime7='" + model.FlowEndTime7 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime7= null ,");
            }
            if (model.FlowEndTime8 != null)
            {
                strSql.Append("FlowEndTime8='" + model.FlowEndTime8 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime8= null ,");
            }
            if (model.FlowEndTime9 != null)
            {
                strSql.Append("FlowEndTime9='" + model.FlowEndTime9 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime9= null ,");
            }
            if (model.FlowEndTime10 != null)
            {
                strSql.Append("FlowEndTime10='" + model.FlowEndTime10 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime10= null ,");
            }
            if (model.Thickness != null)
            {
                strSql.Append("Thickness=" + model.Thickness + ",");
            }
            else
            {
                strSql.Append("Thickness= null ,");
            }
            if (model.Airtight != null)
            {
                strSql.Append("Airtight=" + (model.Airtight ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("Airtight= null ,");
            }
            if (model.PackCode1 != null)
            {
                strSql.Append("PackCode1='" + model.PackCode1 + "',");
            }
            else
            {
                strSql.Append("PackCode1= null ,");
            }
            if (model.PackCode2 != null)
            {
                strSql.Append("PackCode2='" + model.PackCode2 + "',");
            }
            else
            {
                strSql.Append("PackCode2= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据.OCV测试数据
        /// </summary>
        public bool UpdateOCVTest(DB_KT.Model.BatInfo_Year model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + m_TableName + " set ");

            if (model.GradeName != null)
            {
                strSql.Append("GradeName='" + model.GradeName + "',");
            }

            if (model.FlowID != null)
            {
                strSql.Append("FlowID=" + model.FlowID + ",");
            }

            if (model.OCV1 != null)
            {
                strSql.Append("OCV1=" + model.OCV1 + ",");
            }

            if (model.OCV2 != null)
            {
                strSql.Append("OCV2=" + model.OCV2 + ",");
            }

            if (model.OCV3 != null)
            {
                strSql.Append("OCV3=" + model.OCV3 + ",");
            }

            if (model.OCV4 != null)
            {
                strSql.Append("OCV4=" + model.OCV4 + ",");
            }

            if (model.OCV5 != null)
            {
                strSql.Append("OCV5=" + model.OCV5 + ",");
            }

            if (model.InR1 != null)
            {
                strSql.Append("InR1=" + model.InR1 + ",");
            }

            if (model.InR2 != null)
            {
                strSql.Append("InR2=" + model.InR2 + ",");
            }

            if (model.InR3 != null)
            {
                strSql.Append("InR3=" + model.InR3 + ",");
            }

            if (model.InR4 != null)
            {
                strSql.Append("InR4=" + model.InR4 + ",");
            }

            if (model.InR5 != null)
            {
                strSql.Append("InR5=" + model.InR5 + ",");
            }

            if (model.K_Value1 != null)//K值写入
            {
                strSql.Append("K_Value1=" + model.K_Value1 + ",");
            }

            if (model.K_Value2 != null)//k值写入
            {
                strSql.Append("K_Value2=" + model.K_Value2 + ",");
            }

            if (model.Delta_V1 != null)//k值写入
            {
                strSql.Append("Delta_V1=" + model.Delta_V1 + ",");
            }

            if (model.Delta_V2 != null)//k值写入
            {
                strSql.Append("Delta_V2=" + model.Delta_V2 + ",");
            }

            if (model.FlowEndTime1 != null)
            {
                strSql.Append("FlowEndTime1='" + model.FlowEndTime1 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime1= null ,");
            }
            if (model.FlowEndTime2 != null)
            {
                strSql.Append("FlowEndTime2='" + model.FlowEndTime2 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime2= null ,");
            }
            if (model.FlowEndTime3 != null)
            {
                strSql.Append("FlowEndTime3='" + model.FlowEndTime3 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime3= null ,");
            }
            if (model.FlowEndTime4 != null)
            {
                strSql.Append("FlowEndTime4='" + model.FlowEndTime4 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime4= null ,");
            }
            if (model.FlowEndTime5 != null)
            {
                strSql.Append("FlowEndTime5='" + model.FlowEndTime5 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime5= null ,");
            }
            if (model.FlowEndTime6 != null)
            {
                strSql.Append("FlowEndTime6='" + model.FlowEndTime6 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime6= null ,");
            }
            if (model.FlowEndTime7 != null)
            {
                strSql.Append("FlowEndTime7='" + model.FlowEndTime7 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime7= null ,");
            }
            if (model.FlowEndTime8 != null)
            {
                strSql.Append("FlowEndTime8='" + model.FlowEndTime8 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime8= null ,");
            }
            if (model.FlowEndTime9 != null)
            {
                strSql.Append("FlowEndTime9='" + model.FlowEndTime9 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime9= null ,");
            }
            if (model.FlowEndTime10 != null)
            {
                strSql.Append("FlowEndTime10='" + model.FlowEndTime10 + "',");
            }
            else
            {
                strSql.Append("FlowEndTime10= null ,");
            }
            //设备地址
            if (model.DevAddr1 != null)
            {
                strSql.Append("DevAddr1='" + model.DevAddr1 + "',");
            }
            else
            {
                strSql.Append("DevAddr1= null ,");
            }
            if (model.DevAddr2 != null)
            {
                strSql.Append("DevAddr2='" + model.DevAddr2 + "',");
            }
            else
            {
                strSql.Append("DevAddr2= null ,");
            }
            if (model.DevAddr3 != null)
            {
                strSql.Append("DevAddr3='" + model.DevAddr3 + "',");
            }
            else
            {
                strSql.Append("DevAddr3= null ,");
            }
            if (model.DevAddr4 != null)
            {
                strSql.Append("DevAddr4='" + model.DevAddr4 + "',");
            }
            else
            {
                strSql.Append("DevAddr4= null ,");
            }
            if (model.DevAddr5 != null)
            {
                strSql.Append("DevAddr5='" + model.DevAddr5 + "',");
            }
            else
            {
                strSql.Append("DevAddr5= null ,");
            }
            if (model.DevAddr6 != null)
            {
                strSql.Append("DevAddr6='" + model.DevAddr6 + "',");
            }
            else
            {
                strSql.Append("DevAddr6= null ,");
            }
            if (model.DevAddr7 != null)
            {
                strSql.Append("DevAddr7='" + model.DevAddr7 + "',");
            }
            else
            {
                strSql.Append("DevAddr7= null ,");
            }
            if (model.DevAddr8 != null)
            {
                strSql.Append("DevAddr8='" + model.DevAddr8 + "',");
            }
            else
            {
                strSql.Append("DevAddr8= null ,");
            }
            if (model.DevAddr9 != null)
            {
                strSql.Append("DevAddr9='" + model.DevAddr9 + "',");
            }
            else
            {
                strSql.Append("DevAddr9= null ,");
            }
            if (model.DevAddr10 != null)
            {
                strSql.Append("DevAddr10='" + model.DevAddr10 + "',");
            }
            else
            {
                strSql.Append("DevAddr10= null ,");
            }

            if (true)
            {

            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where BatCode='" + model.BatCode + "' and LoginTime='" + model.LoginTime + "' ");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateOCVGrade(string batCode,string loginTime, long gradeName,string sqlScale,out int rowsAffected)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + m_TableName + " set GradeName2='" + gradeName + "' where  BatCode='" + batCode + "' and LoginTime='" + loginTime + "' and " + sqlScale);
                 rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
               
                throw;
            }
           
        }

        public bool UpdateSingleOCVGrade(string batCode, string loginTime, long gradeName, out int rowsAffected)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + m_TableName + " set GradeName2='" + gradeName + "' where  BatCode='" + batCode + "' and LoginTime='" + loginTime + "';" );
                rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public bool ClearGrade(string DeviceCode, string loginTime)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + m_TableName + " set GradeName2='' where  DeviceCode='" + DeviceCode + "' and LoginTime='" + loginTime + "';");
                int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 查询表中是否存在某列，不存在则新增
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnType"></param>
        /// <returns></returns>
        public  bool CheckAndAddColumn(string tableName, string columnName, string columnType)
        {
            string sql;
            sql = "select * from syscolumns where id = object_id('" + tableName + "') and name = '" + columnName + "'";

            DataSet dt= DbHelperSQL.Query(sql);

            if (dt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {

                sql = "alter table " + tableName + " add " + columnName + " " + columnType;

                DbHelperSQL.ExecuteSql(sql.ToString());

                return true;
                
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + m_TableName + " ");
            strSql.Append(" where ID=" + ID + "");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string BatCode, DateTime LoginTime, string DeviceCode, int ProcessID, string LotID, int BatteryPos, long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + m_TableName + " ");
            strSql.Append(" where BatCode=@BatCode and LoginTime=@LoginTime and DeviceCode=@DeviceCode and ProcessID=@ProcessID and LotID=@LotID and BatteryPos=@BatteryPos and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatCode", SqlDbType.VarChar,-1),
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
					new SqlParameter("@DeviceCode", SqlDbType.VarChar,-1),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@LotID", SqlDbType.NVarChar,-1),
					new SqlParameter("@BatteryPos", SqlDbType.SmallInt),
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = BatCode;
            parameters[1].Value = LoginTime;
            parameters[2].Value = DeviceCode;
            parameters[3].Value = ProcessID;
            parameters[4].Value = LotID;
            parameters[5].Value = BatteryPos;
            parameters[6].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + m_TableName + " ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DB_KT.Model.BatInfo_Year GetModel(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" BatCode,LoginTime,DeviceCode,ProcessID,FlowID,LotID,ColCode,BatteryPos,GroupCode,GradeName,MatchName,MatchFlag,Capacity1,Capacity2,Capacity3,Capacity4,Capacity5,Capacity6,Voltage1,Voltage2,Voltage3,Voltage4,Voltage5,Voltage6,Current1,Current2,OCV1,OCV2,OCV3,OCV4,OCV5,InR1,InR2,InR3,InR4,InR5,Delta_V1,Delta_V2,K_Value1,K_Value2,CRate,Temperature1,Temperature2,CapOnTem1,CapOnTem2,DevAddr1,DevAddr2,DevAddr3,DevAddr4,DevAddr5,DevAddr6,DevAddr7,DevAddr8,DevAddr9,DevAddr10,FlowEndTime1,FlowEndTime2,FlowEndTime3,FlowEndTime4,FlowEndTime5,FlowEndTime6,FlowEndTime7,FlowEndTime8,FlowEndTime9,FlowEndTime10,Thickness,Airtight,PackCode1,PackCode2,ID ");
            strSql.Append(" from " + m_TableName + " ");
            strSql.Append(" where ID=" + ID + "");
            DB_KT.Model.BatInfo_Year model = new DB_KT.Model.BatInfo_Year();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DB_KT.Model.BatInfo_Year DataRowToModel(DataRow row)
        {
            DB_KT.Model.BatInfo_Year model = new DB_KT.Model.BatInfo_Year();
            if (row != null)
            {
                //ClsGlobal.WriteLog("BatCode=" + row["BatCode"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["BatCode"] != null)
                {
                    model.BatCode = row["BatCode"].ToString();
                }
                //ClsGlobal.WriteLog("LoginTime=" + row["LoginTime"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["LoginTime"] != null && row["LoginTime"].ToString() != "")
                {
                    model.LoginTime = DateTime.Parse(row["LoginTime"].ToString());

                    //model.LoginTime = DateTime.Parse(strDate);
                }
                //ClsGlobal.WriteLog("DeviceCode=" + row["DeviceCode"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DeviceCode"] != null)
                {
                    model.DeviceCode = row["DeviceCode"].ToString();
                }
                //ClsGlobal.WriteLog("ProcessID=" + row["ProcessID"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["ProcessID"] != null && row["ProcessID"].ToString() != "")
                {
                    model.ProcessID = int.Parse(row["ProcessID"].ToString());
                }
                //ClsGlobal.WriteLog("FlowID=" + row["FlowID"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowID"] != null && row["FlowID"].ToString() != "")
                {
                    model.FlowID = int.Parse(row["FlowID"].ToString());
                }
                //ClsGlobal.WriteLog("LotID=" + row["LotID"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["LotID"] != null)
                {
                    model.LotID = row["LotID"].ToString();
                }
                //ClsGlobal.WriteLog("ColCode=" + row["ColCode"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["ColCode"] != null)
                {
                    model.ColCode = row["ColCode"].ToString();
                }
                //ClsGlobal.WriteLog("BatteryPos=" + row["BatteryPos"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["BatteryPos"] != null && row["BatteryPos"].ToString() != "")
                {
                    model.BatteryPos = int.Parse(row["BatteryPos"].ToString());
                }
                //ClsGlobal.WriteLog("GroupCode=" + row["GroupCode"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["GroupCode"] != null)
                {
                    model.GroupCode = row["GroupCode"].ToString();
                }
                //ClsGlobal.WriteLog("MatchName=" + row["MatchName"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["GradeName"] != null)
                {
                    model.GradeName = row["GradeName"].ToString();
                }
                //ClsGlobal.WriteLog("DeviceCode=" + row["DeviceCode"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["MatchName"] != null)
                {
                    model.MatchName = row["MatchName"].ToString();
                }
                //ClsGlobal.WriteLog("MatchFlag=" + row["MatchFlag"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["MatchFlag"] != null && row["MatchFlag"].ToString() != "")
                {
                    model.MatchFlag = int.Parse(row["MatchFlag"].ToString());
                }
                //ClsGlobal.WriteLog("Capacity1=" + row["Capacity1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Capacity1"] != null && row["Capacity1"].ToString() != "")
                {
                    model.Capacity1 = decimal.Parse(row["Capacity1"].ToString());
                }
                //ClsGlobal.WriteLog("Capacity2=" + row["Capacity2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Capacity2"] != null && row["Capacity2"].ToString() != "")
                {
                    model.Capacity2 = decimal.Parse(row["Capacity2"].ToString());
                }
               // ClsGlobal.WriteLog("Capacity3=" + row["Capacity3"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Capacity3"] != null && row["Capacity3"].ToString() != "")
                {
                    model.Capacity3 = decimal.Parse(row["Capacity3"].ToString());
                }
                //ClsGlobal.WriteLog("Capacity4=" + row["Capacity4"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Capacity4"] != null && row["Capacity4"].ToString() != "")
                {
                    model.Capacity4 = decimal.Parse(row["Capacity4"].ToString());
                }
               //ClsGlobal.WriteLog("DeviceCCapacity5ode=" + row["Capacity5"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Capacity5"] != null && row["Capacity5"].ToString() != "")
                {
                    model.Capacity5 = decimal.Parse(row["Capacity5"].ToString());
                }
                //ClsGlobal.WriteLog("Capacity6=" + row["Capacity6"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Capacity6"] != null && row["Capacity6"].ToString() != "")
                {
                    model.Capacity6 = decimal.Parse(row["Capacity6"].ToString());
                }
                //ClsGlobal.WriteLog("Voltage1=" + row["Voltage1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Voltage1"] != null && row["Voltage1"].ToString() != "")
                {
                    model.Voltage1 = decimal.Parse(row["Voltage1"].ToString());
                }
                //ClsGlobal.WriteLog("Voltage2=" + row["Voltage2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Voltage2"] != null && row["Voltage2"].ToString() != "")
                {
                    model.Voltage2 = decimal.Parse(row["Voltage2"].ToString());
                }
               // ClsGlobal.WriteLog("Voltage3=" + row["Voltage3"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Voltage3"] != null && row["Voltage3"].ToString() != "")
                {
                    model.Voltage3 = decimal.Parse(row["Voltage3"].ToString());
                }
                //ClsGlobal.WriteLog("Voltage4=" + row["Voltage4"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Voltage4"] != null && row["Voltage4"].ToString() != "")
                {
                    model.Voltage4 = decimal.Parse(row["Voltage4"].ToString());
                }
                //ClsGlobal.WriteLog("Voltage5=" + row["Voltage5"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Voltage5"] != null && row["Voltage5"].ToString() != "")
                {
                    model.Voltage5 = decimal.Parse(row["Voltage5"].ToString());
                }
                //ClsGlobal.WriteLog("Voltage6=" + row["Voltage6"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Voltage6"] != null && row["Voltage6"].ToString() != "")
                {
                    model.Voltage6 = decimal.Parse(row["Voltage6"].ToString());
                }
                //ClsGlobal.WriteLog("Current1=" + row["Current1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Current1"] != null && row["Current1"].ToString() != "")
                {
                    model.Current1 = decimal.Parse(row["Current1"].ToString());
                }
                //ClsGlobal.WriteLog("Current2=" + row["Current2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Current2"] != null && row["Current2"].ToString() != "")
                {
                    model.Current2 = decimal.Parse(row["Current2"].ToString());
                }
                //ClsGlobal.WriteLog("OCV1=" + row["OCV1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["OCV1"] != null && row["OCV1"].ToString() != "")
                {
                    model.OCV1 = decimal.Parse(row["OCV1"].ToString());
                }
                //ClsGlobal.WriteLog("OCV2=" + row["OCV2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["OCV2"] != null && row["OCV2"].ToString() != "")
                {
                    model.OCV2 = decimal.Parse(row["OCV2"].ToString());
                }
                //ClsGlobal.WriteLog("OCV3=" + row["OCV3"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["OCV3"] != null && row["OCV3"].ToString() != "")
                {
                    model.OCV3 = decimal.Parse(row["OCV3"].ToString());
                }
                //ClsGlobal.WriteLog("OCV4=" + row["OCV4"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["OCV4"] != null && row["OCV4"].ToString() != "")
                {
                    model.OCV4 = decimal.Parse(row["OCV4"].ToString());
                }
                //ClsGlobal.WriteLog("OCV5=" + row["OCV5"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["OCV5"] != null && row["OCV5"].ToString() != "")
                {
                    model.OCV5 = decimal.Parse(row["OCV5"].ToString());
                }
                //ClsGlobal.WriteLog("InR1=" + row["InR1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["InR1"] != null && row["InR1"].ToString() != "")
                {
                    model.InR1 = decimal.Parse(row["InR1"].ToString());
                }
                //ClsGlobal.WriteLog("InR2=" + row["InR2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["InR2"] != null && row["InR2"].ToString() != "")
                {
                    model.InR2 = decimal.Parse(row["InR2"].ToString());
                }
                //ClsGlobal.WriteLog("InR3=" + row["InR3"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["InR3"] != null && row["InR3"].ToString() != "")
                {
                    model.InR3 = decimal.Parse(row["InR3"].ToString());
                }
                //ClsGlobal.WriteLog("InR4=" + row["InR4"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["InR4"] != null && row["InR4"].ToString() != "")
                {
                    model.InR4 = decimal.Parse(row["InR4"].ToString());
                }
                //ClsGlobal.WriteLog("InR5=" + row["InR5"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["InR5"] != null && row["InR5"].ToString() != "")
                {
                    model.InR5 = decimal.Parse(row["InR5"].ToString());
                }
                //ClsGlobal.WriteLog("Delta_V1=" + row["Delta_V1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Delta_V1"] != null && row["Delta_V1"].ToString() != "")
                {
                    string splitTemp = row["Delta_V1"].ToString();

                    if (splitTemp.IndexOf("E") >= 0)
                    {
                        model.Delta_V1 = 10000;
                    }
                    else
                    {
                        model.Delta_V1 = decimal.Parse(row["Delta_V1"].ToString());
                    }
                }
                //ClsGlobal.WriteLog("Delta_V2=" + row["Delta_V2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Delta_V2"] != null && row["Delta_V2"].ToString() != "")
                {
                    string splitTemp = row["Delta_V2"].ToString();

                    if (splitTemp.IndexOf("E") >= 0)
                    {
                        model.Delta_V2 = 10000;
                    }
                    else
                    {
                        model.Delta_V2 = decimal.Parse(row["Delta_V2"].ToString());
                    }
                }
                //ClsGlobal.WriteLog("K_Value1=" + row["K_Value1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                
                if (row["K_Value1"] != null && row["K_Value1"].ToString() != "")
                {
                    string splitTemp = row["K_Value1"].ToString();

                    if (splitTemp.IndexOf("E") >= 0)
                    {
                        model.K_Value1 = 10000;
                    }
                    else
                    {
                        model.K_Value1 = decimal.Parse(row["K_Value1"].ToString());
                    }
                    //model.K_Value1 = decimal.Parse(row["K_Value1"].ToString());
                }
                //ClsGlobal.WriteLog("K_Value2=" + row["K_Value2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["K_Value2"] != null && row["K_Value2"].ToString() != "")
                {
                    string splitTemp = row["K_Value2"].ToString();

                    if (splitTemp.IndexOf("E")>=0)
                    {
                        model.K_Value2 = 10000;
                    }
                    else
                    {
                        model.K_Value2 = decimal.Parse(row["K_Value2"].ToString());
                    }
                }
                //ClsGlobal.WriteLog("CRate=" + row["CRate"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["CRate"] != null && row["CRate"].ToString() != "")
                {
                    model.CRate = decimal.Parse(row["CRate"].ToString());
                }
                //ClsGlobal.WriteLog("Temperature1=" + row["Temperature1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Temperature1"] != null && row["Temperature1"].ToString() != "")
                {
                    model.Temperature1 = decimal.Parse(row["Temperature1"].ToString());
                }
                //ClsGlobal.WriteLog("Temperature2=" + row["Temperature2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Temperature2"] != null && row["Temperature2"].ToString() != "")
                {
                    model.Temperature2 = decimal.Parse(row["Temperature2"].ToString());
                }
                //ClsGlobal.WriteLog("CapOnTem1=" + row["CapOnTem1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["CapOnTem1"] != null && row["CapOnTem1"].ToString() != "")
                {
                    model.CapOnTem1 = decimal.Parse(row["CapOnTem1"].ToString());
                }
               //ClsGlobal.WriteLog("CapOnTem2=" + row["CapOnTem2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["CapOnTem2"] != null && row["CapOnTem2"].ToString() != "")
                {
                    model.CapOnTem2 = decimal.Parse(row["CapOnTem2"].ToString());
                }
                //ClsGlobal.WriteLog("DevAddr1=" + row["DevAddr1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr1"] != null)
                {
                    model.DevAddr1 = row["DevAddr1"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr2=" + row["DevAddr2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr2"] != null)
                {
                    model.DevAddr2 = row["DevAddr2"].ToString();
                }
               // ClsGlobal.WriteLog("DevAddr3=" + row["DevAddr3"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr3"] != null)
                {
                    model.DevAddr3 = row["DevAddr3"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr4=" + row["DevAddr4"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr4"] != null)
                {
                    model.DevAddr4 = row["DevAddr4"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr5=" + row["DevAddr5"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr5"] != null)
                {
                    model.DevAddr5 = row["DevAddr5"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr6=" + row["DevAddr6"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr6"] != null)
                {
                    model.DevAddr6 = row["DevAddr6"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr7=" + row["DevAddr7"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr7"] != null)
                {
                    model.DevAddr7 = row["DevAddr7"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr8=" + row["DevAddr8"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr8"] != null)
                {
                    model.DevAddr8 = row["DevAddr8"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr9=" + row["DevAddr9"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr9"] != null)
                {
                    model.DevAddr9 = row["DevAddr9"].ToString();
                }
                //ClsGlobal.WriteLog("DevAddr10=" + row["DevAddr10"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["DevAddr10"] != null)
                {
                    model.DevAddr10 = row["DevAddr10"].ToString();
                }
                //ClsGlobal.WriteLog("FlowEndTime1=" + row["FlowEndTime1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime1"] != null && row["FlowEndTime1"].ToString() != "")
                {
                    model.FlowEndTime1 = DateTime.Parse(row["FlowEndTime1"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime2=" + row["FlowEndTime2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime2"] != null && row["FlowEndTime2"].ToString() != "")
                {
                    model.FlowEndTime2 = DateTime.Parse(row["FlowEndTime2"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime3=" + row["FlowEndTime3"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime3"] != null && row["FlowEndTime3"].ToString() != "")
                {
                    model.FlowEndTime3 = DateTime.Parse(row["FlowEndTime3"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime4=" + row["FlowEndTime4"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime4"] != null && row["FlowEndTime4"].ToString() != "")
                {
                    model.FlowEndTime4 = DateTime.Parse(row["FlowEndTime4"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime5=" + row["FlowEndTime5"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime5"] != null && row["FlowEndTime5"].ToString() != "")
                {
                    model.FlowEndTime5 = DateTime.Parse(row["FlowEndTime5"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime6=" + row["FlowEndTime6"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime6"] != null && row["FlowEndTime6"].ToString() != "")
                {
                    model.FlowEndTime6 = DateTime.Parse(row["FlowEndTime6"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime7=" + row["FlowEndTime7"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime7"] != null && row["FlowEndTime7"].ToString() != "")
                {
                    model.FlowEndTime7 = DateTime.Parse(row["FlowEndTime7"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime8=" + row["FlowEndTime8"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime8"] != null && row["FlowEndTime8"].ToString() != "")
                {
                    model.FlowEndTime8 = DateTime.Parse(row["FlowEndTime8"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime9=" + row["FlowEndTime9"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime9"] != null && row["FlowEndTime9"].ToString() != "")
                {
                    model.FlowEndTime9 = DateTime.Parse(row["FlowEndTime9"].ToString());
                }
                //ClsGlobal.WriteLog("FlowEndTime10=" + row["FlowEndTime10"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["FlowEndTime10"] != null && row["FlowEndTime10"].ToString() != "")
                {
                    model.FlowEndTime10 = DateTime.Parse(row["FlowEndTime10"].ToString());
                }
                //ClsGlobal.WriteLog("Thickness=" + row["Thickness"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Thickness"] != null && row["Thickness"].ToString() != "")
                {
                    model.Thickness = decimal.Parse(row["Thickness"].ToString());
                }
                //ClsGlobal.WriteLog("Airtight=" + row["Airtight"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["Airtight"] != null && row["Airtight"].ToString() != "")
                {
                    //ClsGlobal.WriteLog("Airtight=" + row["Airtight"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                    if ((row["Airtight"].ToString() == "1") || (row["Airtight"].ToString().ToLower() == "true"))
                    {
                        model.Airtight = true;
                    }
                    else
                    {
                        model.Airtight = false;
                    }

                }
                //ClsGlobal.WriteLog("PackCode1=" + row["PackCode1"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["PackCode1"] != null)
                {
                    model.PackCode1 = row["PackCode1"].ToString();
                }
                //ClsGlobal.WriteLog("PackCode2=" + row["PackCode2"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["PackCode2"] != null)
                {
                    model.PackCode2 = row["PackCode2"].ToString();
                }
                //ClsGlobal.WriteLog("ID=" + row["ID"].ToString(), ClsGlobal.sDebugOCVSelectionPath);
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = long.Parse(row["ID"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BatCode,LoginTime,DeviceCode,ProcessID,FlowID,LotID,ColCode,BatteryPos,GroupCode,GradeName,MatchName,MatchFlag,Capacity1,Capacity2,Capacity3,Capacity4,Capacity5,Capacity6,Voltage1,Voltage2,Voltage3,Voltage4,Voltage5,Voltage6,Current1,Current2,OCV1,OCV2,OCV3,OCV4,OCV5,InR1,InR2,InR3,InR4,InR5,Delta_V1,Delta_V2,K_Value1,K_Value2,CRate,Temperature1,Temperature2,CapOnTem1,CapOnTem2,DevAddr1,DevAddr2,DevAddr3,DevAddr4,DevAddr5,DevAddr6,DevAddr7,DevAddr8,DevAddr9,DevAddr10,FlowEndTime1,FlowEndTime2,FlowEndTime3,FlowEndTime4,FlowEndTime5,FlowEndTime6,FlowEndTime7,FlowEndTime8,FlowEndTime9,FlowEndTime10,Thickness,Airtight,PackCode1,PackCode2,ID ");
            strSql.Append(" FROM " + m_TableName + " ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得分选结果名
        /// </summary>
        /// <returns></returns>
        public DataSet GetGradeName2(string istBatCode,string istLoginTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select GradeName2 from "+ m_TableName);

            strSql.Append( " where BatCode='" + istBatCode + "' and LoginTime='" + istLoginTime + "'");

            //ClsGlobal.WriteLog(strSql.ToString(), ClsGlobal.sDebugOCVSelectionPath);

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得AVG/Stdev数据列表 BH20200304
        /// </summary>
        public DataSet GetAvgStdev(List<Model.BatInfo_Year> lstModel, string colomn,string extraSql)
        {
            StringBuilder strSql = new StringBuilder();
            string deviceCode = "";
            string loginTime = "";
            foreach (var item in lstModel)
            {
                
                if (item.DeviceCode!="")
                {
                    deviceCode = item.DeviceCode;
                    loginTime = item.LoginTime.ToString();
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (extraSql!="")
            {
                strSql.Append("select AVG(" + colomn + "),stdev(" + colomn + ") from " + m_TableName + " where DeviceCode='" + deviceCode + "' and LoginTime='" + loginTime + "' and "+extraSql);
            }
            else
            {
                strSql.Append("select AVG(" + colomn + "),stdev(" + colomn + ") from " + m_TableName + " where DeviceCode='" + deviceCode + "' and LoginTime='" + loginTime + "'");
            }

            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取指定托盘的电池条码
        /// </summary>
        /// <param name="lstModel"></param>
        /// <param name="colomn"></param>
        /// <returns></returns>
        public DataSet GetBatCode(List<Model.BatInfo_Year> lstModel)
        {
            StringBuilder strSql = new StringBuilder();
            string deviceCode = "";
            string loginTime = "";
            foreach (var item in lstModel)
            {
                if (item.DeviceCode != "")
                {
                    deviceCode = item.DeviceCode;
                    loginTime = item.LoginTime.ToString();
                    break;
                }
                else
                {
                    continue;
                }
            }
            strSql.Append("select GradeName2,Batcode from " + m_TableName + " where DeviceCode='" + deviceCode + "' and LoginTime='" + loginTime + "'");

            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" BatCode,LoginTime,DeviceCode,ProcessID,FlowID,LotID,ColCode,BatteryPos,GroupCode,GradeName,MatchName,MatchFlag,Capacity1,Capacity2,Capacity3,Capacity4,Capacity5,Capacity6,Voltage1,Voltage2,Voltage3,Voltage4,Voltage5,Voltage6,Current1,Current2,OCV1,OCV2,OCV3,OCV4,OCV5,InR1,InR2,InR3,InR4,InR5,Delta_V1,Delta_V2,K_Value1,K_Value2,CRate,Temperature1,Temperature2,CapOnTem1,CapOnTem2,DevAddr1,DevAddr2,DevAddr3,DevAddr4,DevAddr5,DevAddr6,DevAddr7,DevAddr8,DevAddr9,DevAddr10,FlowEndTime1,FlowEndTime2,FlowEndTime3,FlowEndTime4,FlowEndTime5,FlowEndTime6,FlowEndTime7,FlowEndTime8,FlowEndTime9,FlowEndTime10,Thickness,Airtight,PackCode1,PackCode2,ID ");
            strSql.Append(" FROM " + m_TableName + " ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM " + m_TableName + " ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取指定语句的数据表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetRecord(string colomn,string deviceCode,string loginTime,string extraSql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select "+ colomn + " FROM " + m_TableName + " where DeviceCode='"+deviceCode+"' and LoginTime='"+loginTime+"' "+extraSql );
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //strSql = strSql.Append(strWhere);
  
                return DbHelperSQL.Query(strSql.ToString());
          
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from " + m_TableName + " T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        */

        #endregion  Method
        #region  MethodEx

        /// <summary>
        /// 得到最近登录的托盘电池
        /// </summary>
        public DB_KT.Model.BatInfo_Year GetModel_LatestTime(string DeviceCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append("*");
            strSql.Append(" from " + m_TableName + "");
            strSql.Append(" where DeviceCode='" + DeviceCode + "' order by LoginTime desc ");

            //ClsGlobal.WriteLog("查询SQL1=" + strSql.ToString(), ClsGlobal.sDebugOCVSelectionPath);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            //ClsGlobal.WriteLog("查询SQL1完成", ClsGlobal.sDebugOCVSelectionPath);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得托盘相应的电池数据数据
        /// </summary>
        public List<Model.BatInfo_Year> GetList_LatestTime(Model.BatInfo_Year model)
        {
            List<Model.BatInfo_Year> lstBatInfo = new List<Model.BatInfo_Year>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + m_TableName + " ");
            strSql.Append(" where DeviceCode = '" + model.DeviceCode + "' and loginTime = '" + model.LoginTime + "'");   //20191217 batInfo2019表里面的flowID没有被更新, 用了batinfo表
            //strSql.Append(" where loginTime = '" + model.LoginTime + "'");
            strSql.Append(" order by batterypos;");
            //ClsGlobal.WriteLog("查询SQL2="+ strSql.ToString(), ClsGlobal.sDebugOCVSelectionPath);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            //ClsGlobal.WriteLog("查询SQL2完成|ds.Tables[0].Rows.Count="+ ds.Tables[0].Rows.Count, ClsGlobal.sDebugOCVSelectionPath);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ClsGlobal.WriteLog("fck" + ds.Tables[0].Rows.Count, ClsGlobal.sDebugOCVSelectionPath);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    lstBatInfo.Add(DataRowToModel(ds.Tables[0].Rows[i]));
                }
            }
            else
            {
                return null;
            }

            return lstBatInfo;
        }

        /// <summary>
        /// 更新电池OCV流程数据结果
        /// </summary>
        public bool UpdateList_BattInfo(List<Model.BatInfo_Year> lstModel)
        {
            if (lstModel.Count == 0) return false;

            foreach (var item in lstModel)
            {
                UpdateOCVTest(item);    //单个更新
            }

            return true;
        }

        public bool UpdateList_BattOCVGrade(List<Model.BatInfo_Year> lstModel, long istGradeName, string istSqlScale,out int recode)
        {
            if (lstModel.Count == 0)
            {
                recode = 0;
                return false;

            }

            recode = 0;
            foreach (var item in lstModel)
            {
                long RealGradename = 0;
                #region
                ////按托盘更新
                //      if (DeviceCodeSave != item.DeviceCode)
                //      {
                //          DeviceCodeSave = item.DeviceCode;

                //UpdateOCVGrade(DeviceCodeSave, item.LoginTime.ToString(), istGradeName, sitSqlScale);
                //      }
                //      else
                //      {
                //          continue;
                //      }
                #endregion
                //按BatCode更新
                //先查询该条码的GradeName2值，加上目前的istGradeName作为新的GradeName
                DataSet dt = GetGradeName2(item.BatCode, item.LoginTime.ToString());

                //DataSet dt = GetGradeName2("1953", "2019-08-16 16:00:14.000");

                if (dt.Tables[0].Rows.Count > 0)
                {
                    if (long.TryParse(dt.Tables[0].Rows[0][0].ToString(), out long gradeName))//如果GradeName2原来有值
                    {

                        if (((FrmOCV.OCVSelection)gradeName & FrmOCV.OCVSelection.Bat_Not_Exist) != FrmOCV.OCVSelection.OK)
                        {
                            recode = recode + 1;
                            continue;
                        }

                        RealGradename = istGradeName + gradeName;
                    }
                    else
                    {
                        RealGradename = istGradeName;
                    }
                }

                UpdateOCVGrade(item.BatCode, item.LoginTime.ToString(), RealGradename, istSqlScale,out int mRecode);
                recode = recode + mRecode;

            }

            return true;
        }

        public bool OCVClearGrade(List<Model.BatInfo_Year> lstModel)
        {
            if (lstModel.Count == 0) return false;
            string DeviceCode = "";
            string LoginTime = "";
            foreach (var item in lstModel)
            {
                if (item.DeviceCode!="" && item .LoginTime.ToString()!="")
                {
                    DeviceCode = item.DeviceCode;
                    LoginTime = item.LoginTime.ToString();

                    return ClearGrade(DeviceCode, LoginTime);
                   
                }
            }
            return false;

        }

        #endregion  MethodEx
    }
}

