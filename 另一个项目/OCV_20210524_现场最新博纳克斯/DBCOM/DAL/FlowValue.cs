using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DB_KT.DAL
{
	public partial class FlowValue
	{
        DbHelperSQLP DbHelperSQL;
        string m_TableName = "FlowValue";

        public FlowValue(string strConn)
        {
            if (strConn == null)
            {
                throw new Exception("输入参数错误");
            }

            DbHelperSQL = new DbHelperSQLP(strConn);
        }


        public FlowValue(string strConn, string tableName)
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
		/// 增加一条数据
		/// </summary>
		public int Add(DB_KT.Model.FlowValue model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ProcessID != null)
			{
				strSql1.Append("ProcessID,");
				strSql2.Append(""+model.ProcessID+",");
			}
			if (model.FlowName != null)
			{
				strSql1.Append("FlowName,");
				strSql2.Append("'"+model.FlowName+"',");
			}
			if (model.FlowType != null)
			{
				strSql1.Append("FlowType,");
				strSql2.Append(""+model.FlowType+",");
			}
			if (model.FlowData != null)
			{
				strSql1.Append("FlowData,");
				strSql2.Append("'"+model.FlowData+"',");
			}
			if (model.Station != null)
			{
				strSql1.Append("Station,");
				strSql2.Append(""+model.Station+",");
			}
			if (model.FlowStartTime != null)
			{
				strSql1.Append("FlowStartTime,");
				strSql2.Append("'"+model.FlowStartTime+"',");
			}
			if (model.Statis != null)
			{
				strSql1.Append("Statis,");
				strSql2.Append("'"+model.Statis+"',");
			}
			if (model.Selection != null)
			{
				strSql1.Append("Selection,");
				strSql2.Append("'"+model.Selection+"',");
			}
			if (model.Matching != null)
			{
				strSql1.Append("Matching,");
				strSql2.Append("'"+model.Matching+"',");
			}
			if (model.CapaCoeffi != null)
			{
				strSql1.Append("CapaCoeffi,");
				strSql2.Append("'"+model.CapaCoeffi+"',");
			}
			if (model.PLCSETStr != null)
			{
				strSql1.Append("PLCSETStr,");
				strSql2.Append("'"+model.PLCSETStr+"',");
			}
			strSql.Append("insert into FlowValue(");
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
				return Convert.ToInt32(obj);
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DB_KT.Model.FlowValue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FlowValue set ");
			if (model.ProcessID != null)
			{
				strSql.Append("ProcessID="+model.ProcessID+",");
			}
			if (model.FlowName != null)
			{
				strSql.Append("FlowName='"+model.FlowName+"',");
			}
			else
			{
				strSql.Append("FlowName= null ,");
			}
			if (model.FlowType != null)
			{
				strSql.Append("FlowType="+model.FlowType+",");
			}
			else
			{
				strSql.Append("FlowType= null ,");
			}
			if (model.FlowData != null)
			{
				strSql.Append("FlowData='"+model.FlowData+"',");
			}
			else
			{
				strSql.Append("FlowData= null ,");
			}
			if (model.Station != null)
			{
				strSql.Append("Station="+model.Station+",");
			}
			else
			{
				strSql.Append("Station= null ,");
			}
			if (model.FlowStartTime != null)
			{
				strSql.Append("FlowStartTime='"+model.FlowStartTime+"',");
			}
			else
			{
				strSql.Append("FlowStartTime= null ,");
			}
			if (model.Statis != null)
			{
				strSql.Append("Statis='"+model.Statis+"',");
			}
			else
			{
				strSql.Append("Statis= null ,");
			}
			if (model.Selection != null)
			{
				strSql.Append("Selection='"+model.Selection+"',");
			}
			else
			{
				strSql.Append("Selection= null ,");
			}
			if (model.Matching != null)
			{
				strSql.Append("Matching='"+model.Matching+"',");
			}
			else
			{
				strSql.Append("Matching= null ,");
			}
			if (model.CapaCoeffi != null)
			{
				strSql.Append("CapaCoeffi='"+model.CapaCoeffi+"',");
			}
			else
			{
				strSql.Append("CapaCoeffi= null ,");
			}
			if (model.PLCSETStr != null)
			{
				strSql.Append("PLCSETStr='"+model.PLCSETStr+"',");
			}
			else
			{
				strSql.Append("PLCSETStr= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where FlowID="+ model.FlowID+"");
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
		public bool Delete(int FlowID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from FlowValue ");
			strSql.Append(" where FlowID="+FlowID+"" );
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string FlowIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from FlowValue ");
			strSql.Append(" where FlowID in ("+FlowIDlist + ")  ");
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
		public DB_KT.Model.FlowValue GetModel(int FlowID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ProcessID,FlowID,FlowName,FlowType,FlowData,Station,FlowStartTime,Statis,Selection,Matching,CapaCoeffi,PLCSETStr ");
			strSql.Append(" from FlowValue ");
			strSql.Append(" where FlowID="+FlowID+"" );
			DB_KT.Model.FlowValue model=new DB_KT.Model.FlowValue();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
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
        public DB_KT.Model.FlowValue GetModel(int proID, int FlowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" ProcessID,FlowID,FlowName,FlowType,FlowData,Station,FlowStartTime,Statis,Selection,Matching,CapaCoeffi ");
            strSql.Append(" from FlowValue ");
            strSql.Append(" where FlowID=" + FlowID + " and ProcessID=" + proID);
            DB_KT.Model.FlowValue model = new DB_KT.Model.FlowValue();
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
		public DB_KT.Model.FlowValue DataRowToModel(DataRow row)
		{
			DB_KT.Model.FlowValue model=new DB_KT.Model.FlowValue();
			if (row != null)
			{
				if(row["ProcessID"]!=null && row["ProcessID"].ToString()!="")
				{
					model.ProcessID=int.Parse(row["ProcessID"].ToString());
				}
				if(row["FlowID"]!=null && row["FlowID"].ToString()!="")
				{
					model.FlowID=int.Parse(row["FlowID"].ToString());
				}
				if(row["FlowName"]!=null)
				{
					model.FlowName=row["FlowName"].ToString();
				}
				if(row["FlowType"]!=null && row["FlowType"].ToString()!="")
				{
					model.FlowType=int.Parse(row["FlowType"].ToString());
				}
				if(row["FlowData"]!=null)
				{
					model.FlowData=row["FlowData"].ToString();
				}
				if(row["Station"]!=null && row["Station"].ToString()!="")
				{
					model.Station=int.Parse(row["Station"].ToString());
				}
				if(row["FlowStartTime"]!=null)
				{
					model.FlowStartTime=row["FlowStartTime"].ToString();
				}
				if(row["Statis"]!=null)
				{
					model.Statis=row["Statis"].ToString();
				}
				if(row["Selection"]!=null)
				{
					model.Selection=row["Selection"].ToString();
				}
				if(row["Matching"]!=null)
				{
					model.Matching=row["Matching"].ToString();
				}
				if(row["CapaCoeffi"]!=null)
				{
					model.CapaCoeffi=row["CapaCoeffi"].ToString();
				}
				if(row["PLCSETStr"]!=null)
				{
					model.PLCSETStr=row["PLCSETStr"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ProcessID,FlowID,FlowName,FlowType,FlowData,Station,FlowStartTime,Statis,Selection,Matching,CapaCoeffi,PLCSETStr ");
			strSql.Append(" FROM FlowValue ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ProcessID,FlowID,FlowName,FlowType,FlowData,Station,FlowStartTime,Statis,Selection,Matching,CapaCoeffi,PLCSETStr ");
			strSql.Append(" FROM FlowValue ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM FlowValue ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.FlowID desc");
			}
			strSql.Append(")AS Row, T.*  from FlowValue T ");
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
        /// 获得工程的流程列表
        /// </summary>
        /// <param name="ProcessID">工程号</param>
        /// <returns>流程工步列表</returns>
        public List<DB_KT.Model.FlowValue> GetList_ProjFlow(int ProcessID)
        {
            List<Model.FlowValue> lstFlowValue = new List<Model.FlowValue>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM FlowValue ");
            strSql.Append(" where ProcessID = " + ProcessID);
            strSql.Append("order by FlowID ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstFlowValue.Add(DataRowToModel(ds.Tables[0].Rows[i]));
                }
            }
            else
            {
                return null;
            }

            return lstFlowValue;        
        }
        
        
        

        //public bool Exists(int currentFlowID, ref int nextFlowID)
        //{
        //    int processID = -1;
        //    string pstrSql = "select ProcessID from FlowValue where FlowID = " + currentFlowID;
        //    DataSet pds = DbHelperSQL.Query(pstrSql);
        //    if (pds.Tables[0].Rows.Count > 0)
        //    {
        //        processID = (int)pds.Tables[0].Rows[0].ItemArray[0];
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    string strSql = "select FlowID from FlowValue where ProcessID = " + processID + "order by FlowID";
        //    DataSet ds = DbHelperSQL.Query(strSql);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if ((int)ds.Tables[0].Rows[i].ItemArray[0] == currentFlowID)
        //            {
        //                if (i == ds.Tables[0].Rows.Count - 1)
        //                    return false;
        //                else
        //                {
        //                    nextFlowID = (int)ds.Tables[0].Rows[i + 1].ItemArray[0];
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //public bool Exists(int proID, int currentFlowID, ref int nextFlowID)
        //{
        //    string strSql = "select FlowID from FlowValue where ProcessID = " + proID + "order by FlowID";
        //    DataSet ds = DbHelperSQL.Query(strSql);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if ((int)ds.Tables[0].Rows[i].ItemArray[0] == currentFlowID)
        //            {
        //                if (i == ds.Tables[0].Rows.Count - 1)
        //                    return false;
        //                else
        //                {
        //                    nextFlowID = (int)ds.Tables[0].Rows[i + 1].ItemArray[0];
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        ////获取设备下一个工步号
        //public bool GetNextFlowID(int currFlowID, out int nextFlowID, out int nextFlowType)
        //{

        //    nextFlowID = -1;
        //    if (Exists(currFlowID, ref nextFlowID))
        //    {
        //        Model.FlowValue fv = GetModel(nextFlowID);
        //        nextFlowType = (int)fv.FlowType;
        //    }
        //    else
        //    {
        //        nextFlowID = -1;
        //        nextFlowType = -1;
        //        return false;
        //    }

        //    return true;
        //}

        ////获取设备下一个工步号
        //public bool GetNextFlowID(int ProID, int currFlowID, out int nextFlowID, out int nextFlowType)
        //{
        //    nextFlowID = -1;
        //    if (Exists(ProID, currFlowID, ref nextFlowID))
        //    {
        //        Model.FlowValue fv = GetModel(nextFlowID);
        //        nextFlowType = (int)fv.FlowType;
        //    }
        //    else
        //    {
        //        nextFlowID = -1;
        //        nextFlowType = -1;
        //        return false;
        //    }

        //    return true;
        //}

        ////获取设备上一个工步号
        //public bool GetLastFlowID(int currFlowID, out int LastFlowID, out int LastFlowType)
        //{
        //    int nextFlowID = -1;
        //    if (Exists(currFlowID, ref nextFlowID))
        //    {
        //        LastFlowID = currFlowID - 1;
        //        Model.FlowValue fv = GetModel(LastFlowID);
        //        LastFlowType = (int)fv.FlowType;
        //    }
        //    else
        //    {
        //        LastFlowID = -1;
        //        LastFlowType = -1;
        //        return false;
        //    }

        //    return true;
        //}

		#endregion  MethodEx
	}
}

