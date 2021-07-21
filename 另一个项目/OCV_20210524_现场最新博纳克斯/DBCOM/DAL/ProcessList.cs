using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace DB_KT.DAL
{
	public partial class ProcessList
	{
        DbHelperSQLP DbHelperSQL;
        string m_TableName = "ProcessList";

        public ProcessList(string strConn)
        {
            if (strConn == null)
            {
                throw new Exception("输入参数错误");
            }

            DbHelperSQL = new DbHelperSQLP(strConn);
        }

        public ProcessList(string strConn, string tableName)
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
		public int Add(DB_KT.Model.ProcessList model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ProcessName != null)
			{
				strSql1.Append("ProcessName,");
				strSql2.Append("'"+model.ProcessName+"',");
			}
			if (model.BatteryType != null)
			{
				strSql1.Append("BatteryType,");
				strSql2.Append("'"+model.BatteryType+"',");
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
			if (model.ProcessType != null)
			{
				strSql1.Append("ProcessType,");
				strSql2.Append("'"+model.ProcessType+"',");
			}
			strSql.Append("insert into ProcessList(");
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
		public bool Update(DB_KT.Model.ProcessList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ProcessList set ");
			if (model.ProcessName != null)
			{
				strSql.Append("ProcessName='"+model.ProcessName+"',");
			}
			else
			{
				strSql.Append("ProcessName= null ,");
			}
			if (model.BatteryType != null)
			{
				strSql.Append("BatteryType='"+model.BatteryType+"',");
			}
			else
			{
				strSql.Append("BatteryType= null ,");
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
			if (model.ProcessType != null)
			{
				strSql.Append("ProcessType='"+model.ProcessType+"',");
			}
			else
			{
				strSql.Append("ProcessType= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ProcessID="+ model.ProcessID+"");
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
		public bool Delete(int ProcessID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ProcessList ");
			strSql.Append(" where ProcessID="+ProcessID+"" );
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string ProcessIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ProcessList ");
			strSql.Append(" where ProcessID in ("+ProcessIDlist + ")  ");
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
		public DB_KT.Model.ProcessList GetModel(int ProcessID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ProcessID,ProcessName,BatteryType,Statis,Selection,Matching,ProcessType ");
			strSql.Append(" from ProcessList ");
			strSql.Append(" where ProcessID="+ProcessID+"" );
			DB_KT.Model.ProcessList model=new DB_KT.Model.ProcessList();
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
		public DB_KT.Model.ProcessList DataRowToModel(DataRow row)
		{
			DB_KT.Model.ProcessList model=new DB_KT.Model.ProcessList();
			if (row != null)
			{
				if(row["ProcessID"]!=null && row["ProcessID"].ToString()!="")
				{
					model.ProcessID=int.Parse(row["ProcessID"].ToString());
				}
				if(row["ProcessName"]!=null)
				{
					model.ProcessName=row["ProcessName"].ToString();
				}
				if(row["BatteryType"]!=null)
				{
					model.BatteryType=row["BatteryType"].ToString();
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
				if(row["ProcessType"]!=null)
				{
					model.ProcessType=row["ProcessType"].ToString();
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
			strSql.Append("select ProcessID,ProcessName,BatteryType,Statis,Selection,Matching,ProcessType ");
			strSql.Append(" FROM ProcessList ");
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
			strSql.Append(" ProcessID,ProcessName,BatteryType,Statis,Selection,Matching,ProcessType ");
			strSql.Append(" FROM ProcessList ");
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
			strSql.Append("select count(1) FROM ProcessList ");
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
				strSql.Append("order by T.ProcessID desc");
			}
			strSql.Append(")AS Row, T.*  from ProcessList T ");
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

		#endregion  MethodEx
	}
}

