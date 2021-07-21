using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DB_KT.DAL
{
    public partial class BatInfo
    {
        DbHelperSQLP DbHelperSQL;
        string m_TableName = "BatInfo";

        public BatInfo(string strConn)
        {
            if (strConn == null)
            {
                throw new Exception("输入参数错误");
            }

            DbHelperSQL = new DbHelperSQLP(strConn);
        }

        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DB_KT.Model.BatInfo model)
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
            if (model.FlowType != null)
            {
                strSql1.Append("FlowType,");
                strSql2.Append("" + model.FlowType + ",");
            }
            if (model.LotID != null)
            {
                strSql1.Append("LotID,");
                strSql2.Append("'" + model.LotID + "',");
            }
            if (model.WorkStation != null)
            {
                strSql1.Append("WorkStation,");
                strSql2.Append("" + model.WorkStation + ",");
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
            if (model.LastFlowEndTime != null)
            {
                strSql1.Append("LastFlowEndTime,");
                strSql2.Append("'" + model.LastFlowEndTime + "',");
            }
            if (model.DevAddr != null)
            {
                strSql1.Append("DevAddr,");
                strSql2.Append("'" + model.DevAddr + "',");
            }
            strSql.Append("insert into BatInfo(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
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
        /// 更新一条数据
        /// </summary>
        public bool Update(DB_KT.Model.BatInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BatInfo set ");
            if (model.BatCode != null)
            {
                strSql.Append("BatCode='" + model.BatCode + "',");
            }
            if (model.LoginTime != null)
            {
                strSql.Append("LoginTime='" + model.LoginTime + "',");
            }
            if (model.DeviceCode != null)
            {
                strSql.Append("DeviceCode='" + model.DeviceCode + "',");
            }
            else
            {
                strSql.Append("DeviceCode= null ,");
            }
            if (model.ProcessID != null)
            {
                strSql.Append("ProcessID=" + model.ProcessID + ",");
            }
            else
            {
                strSql.Append("ProcessID= null ,");
            }
            if (model.FlowID != null)
            {
                strSql.Append("FlowID=" + model.FlowID + ",");
            }
            else
            {
                strSql.Append("FlowID= null ,");
            }
            if (model.FlowType != null)
            {
                strSql.Append("FlowType=" + model.FlowType + ",");
            }
            else
            {
                strSql.Append("FlowType= null ,");
            }
            if (model.LotID != null)
            {
                strSql.Append("LotID='" + model.LotID + "',");
            }
            else
            {
                strSql.Append("LotID= null ,");
            }
            if (model.WorkStation != null)
            {
                strSql.Append("WorkStation=" + model.WorkStation + ",");
            }
            else
            {
                strSql.Append("WorkStation= null ,");
            }
            if (model.ColCode != null)
            {
                strSql.Append("ColCode='" + model.ColCode + "',");
            }
            else
            {
                strSql.Append("ColCode= null ,");
            }
            if (model.BatteryPos != null)
            {
                strSql.Append("BatteryPos=" + model.BatteryPos + ",");
            }
            else
            {
                strSql.Append("BatteryPos= null ,");
            }
            if (model.LastFlowEndTime != null)
            {
                strSql.Append("LastFlowEndTime='" + model.LastFlowEndTime + "',");
            }
            else
            {
                strSql.Append("LastFlowEndTime= null ,");
            }
            if (model.DevAddr != null)
            {
                strSql.Append("DevAddr='" + model.DevAddr + "',");
            }
            else
            {
                strSql.Append("DevAddr= null ,");
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BatInfo ");
            strSql.Append(" where ");
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
        /// 得到一个对象实体
        /// </summary>
        public DB_KT.Model.BatInfo GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" BatCode,LoginTime,DeviceCode,ProcessID,FlowID,FlowType,LotID,WorkStation,ColCode,BatteryPos,LastFlowEndTime,DevAddr ");
            strSql.Append(" from BatInfo ");
            strSql.Append(" where ");
            DB_KT.Model.BatInfo model = new DB_KT.Model.BatInfo();
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
        public DB_KT.Model.BatInfo DataRowToModel(DataRow row)
        {
            DB_KT.Model.BatInfo model = new DB_KT.Model.BatInfo();
            if (row != null)
            {
                if (row["BatCode"] != null)
                {
                    model.BatCode = row["BatCode"].ToString();
                }
                if (row["LoginTime"] != null && row["LoginTime"].ToString() != "")
                {
                    model.LoginTime = DateTime.Parse(row["LoginTime"].ToString());
                }
                if (row["DeviceCode"] != null)
                {
                    model.DeviceCode = row["DeviceCode"].ToString();
                }
                if (row["ProcessID"] != null && row["ProcessID"].ToString() != "")
                {
                    model.ProcessID = int.Parse(row["ProcessID"].ToString());
                }
                if (row["FlowID"] != null && row["FlowID"].ToString() != "")
                {
                    model.FlowID = int.Parse(row["FlowID"].ToString());
                }
                if (row["FlowType"] != null && row["FlowType"].ToString() != "")
                {
                    model.FlowType = int.Parse(row["FlowType"].ToString());
                }
                if (row["LotID"] != null)
                {
                    model.LotID = row["LotID"].ToString();
                }
                if (row["WorkStation"] != null && row["WorkStation"].ToString() != "")
                {
                    model.WorkStation = int.Parse(row["WorkStation"].ToString());
                }
                if (row["ColCode"] != null)
                {
                    model.ColCode = row["ColCode"].ToString();
                }
                if (row["BatteryPos"] != null && row["BatteryPos"].ToString() != "")
                {
                    model.BatteryPos = int.Parse(row["BatteryPos"].ToString());
                }
                if (row["LastFlowEndTime"] != null && row["LastFlowEndTime"].ToString() != "")
                {
                    model.LastFlowEndTime = DateTime.Parse(row["LastFlowEndTime"].ToString());
                }
                if (row["DevAddr"] != null)
                {
                    model.DevAddr = row["DevAddr"].ToString();
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
            strSql.Append("select BatCode,LoginTime,DeviceCode,ProcessID,FlowID,FlowType,LotID,WorkStation,ColCode,BatteryPos,LastFlowEndTime,DevAddr ");
            strSql.Append(" FROM BatInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" BatCode,LoginTime,DeviceCode,ProcessID,FlowID,FlowType,LotID,WorkStation,ColCode,BatteryPos,LastFlowEndTime,DevAddr ");
            strSql.Append(" FROM BatInfo ");
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
            strSql.Append("select count(1) FROM BatInfo ");
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
                strSql.Append("order by T. desc");
            }
            strSql.Append(")AS Row, T.*  from BatInfo T ");
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
        public DB_KT.Model.BatInfo GetModel_LatestTime(string DeviceCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append("*");
            strSql.Append(" from BatInfo ");
            strSql.Append(" where DeviceCode='" + DeviceCode + "' order by LoginTime desc;");
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
        /// 获得托盘相应的电池数据
        /// </summary>
        public List<Model.BatInfo> GetList_LatestTime(Model.BatInfo model)
        {
            List<Model.BatInfo> lstBatInfo = new List<Model.BatInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM BatInfo ");
            strSql.Append(" where DeviceCode = '" + model.DeviceCode + "' and flowID = " + model.FlowID + " and loginTime = '" + model.LoginTime + "'");
            strSql.Append(" order by batterypos;");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
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
        /// 更新电池的流程部分
        /// </summary>
        public bool UpdateList_BattInfo(List<Model.BatInfo> lstModel)
        {
            if (lstModel.Count == 0) return false;

            foreach (var item in lstModel)
            {
                Update(item);
            }

            return true;
        }

        #endregion  MethodEx
    }
}

