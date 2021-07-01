using System;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DevInfo.DAL
{
    /// <summary>
    /// 数据访问类:Channel_Info
    /// </summary>
    public partial class Channel_Info
    {
        public Channel_Info()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQLite.GetMaxID("ChannelNo", "Channel_Info");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ChannelNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Channel_Info");
            strSql.Append(" where ChannelNo=@ChannelNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4)           };
            parameters[0].Value = ChannelNo;

            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DevInfo.Model.Channel_Info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Channel_Info(");
            strSql.Append("ChannelNo,OCV_ErrCount,OCV_EN,Shell_ErrCount,Shell_EN,ACIR_ErrCount,ACIR_EN)");
            strSql.Append(" values (");
            strSql.Append("@ChannelNo,@OCV_ErrCount,@OCV_EN,@Shell_ErrCount,@Shell_EN,@ACIR_ErrCount,@ACIR_EN)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4),
                    new SQLiteParameter("@OCV_ErrCount", DbType.Int32,4),
                    new SQLiteParameter("@OCV_EN", DbType.Boolean,1),
                    new SQLiteParameter("@Shell_ErrCount", DbType.Int32,4),
                    new SQLiteParameter("@Shell_EN", DbType.Boolean,1),
                    new SQLiteParameter("@ACIR_ErrCount", DbType.Int32,4),
                    new SQLiteParameter("@ACIR_EN", DbType.Boolean,1)};
            parameters[0].Value = model.ChannelNo;
            parameters[1].Value = model.OCV_ErrCount;
            parameters[2].Value = model.OCV_EN;
            parameters[3].Value = model.Shell_ErrCount;
            parameters[4].Value = model.Shell_EN;
            parameters[5].Value = model.ACIR_ErrCount;
            parameters[6].Value = model.ACIR_EN;

            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Update(DevInfo.Model.Channel_Info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Channel_Info set ");
            strSql.Append("OCV_ErrCount=@OCV_ErrCount,");
            strSql.Append("OCV_EN=@OCV_EN,");
            strSql.Append("Shell_ErrCount=@Shell_ErrCount,");
            strSql.Append("Shell_EN=@Shell_EN,");
            strSql.Append("ACIR_ErrCount=@ACIR_ErrCount,");
            strSql.Append("ACIR_EN=@ACIR_EN");
            strSql.Append(" where ChannelNo=@ChannelNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@OCV_ErrCount", DbType.Int32,4),
                    new SQLiteParameter("@OCV_EN", DbType.Boolean,1),
                    new SQLiteParameter("@Shell_ErrCount", DbType.Int32,4),
                    new SQLiteParameter("@Shell_EN", DbType.Boolean,1),
                    new SQLiteParameter("@ACIR_ErrCount", DbType.Int32,4),
                    new SQLiteParameter("@ACIR_EN", DbType.Boolean,1),
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4)};
            parameters[0].Value = model.OCV_ErrCount;
            parameters[1].Value = model.OCV_EN;
            parameters[2].Value = model.Shell_ErrCount;
            parameters[3].Value = model.Shell_EN;
            parameters[4].Value = model.ACIR_ErrCount;
            parameters[5].Value = model.ACIR_EN;
            parameters[6].Value = model.ChannelNo;

            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ChannelNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Channel_Info ");
            strSql.Append(" where ChannelNo=@ChannelNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4)           };
            parameters[0].Value = ChannelNo;

            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string ChannelNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Channel_Info ");
            strSql.Append(" where ChannelNo in (" + ChannelNolist + ")  ");
            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString());
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
        public DevInfo.Model.Channel_Info GetModel(int ChannelNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ChannelNo,OCV_ErrCount,OCV_EN,Shell_ErrCount,Shell_EN,ACIR_ErrCount,ACIR_EN from Channel_Info ");
            strSql.Append(" where ChannelNo=@ChannelNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4)           };
            parameters[0].Value = ChannelNo;

            DevInfo.Model.Channel_Info model = new DevInfo.Model.Channel_Info();
            DataSet ds = DbHelperSQLite.Query(strSql.ToString(), parameters);
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
        public DevInfo.Model.Channel_Info DataRowToModel(DataRow row)
        {
            DevInfo.Model.Channel_Info model = new DevInfo.Model.Channel_Info();
            if (row != null)
            {
                if (row["ChannelNo"] != null && row["ChannelNo"].ToString() != "")
                {
                    model.ChannelNo = int.Parse(row["ChannelNo"].ToString());
                }
                if (row["OCV_ErrCount"] != null && row["OCV_ErrCount"].ToString() != "")
                {
                    model.OCV_ErrCount = int.Parse(row["OCV_ErrCount"].ToString());
                }
                if (row["OCV_EN"] != null && row["OCV_EN"].ToString() != "")
                {
                    if ((row["OCV_EN"].ToString() == "1") || (row["OCV_EN"].ToString().ToLower() == "true"))
                    {
                        model.OCV_EN = true;
                    }
                    else
                    {
                        model.OCV_EN = false;
                    }
                }
                if (row["Shell_ErrCount"] != null && row["Shell_ErrCount"].ToString() != "")
                {
                    model.Shell_ErrCount = int.Parse(row["Shell_ErrCount"].ToString());
                }
                if (row["Shell_EN"] != null && row["Shell_EN"].ToString() != "")
                {
                    if ((row["Shell_EN"].ToString() == "1") || (row["Shell_EN"].ToString().ToLower() == "true"))
                    {
                        model.Shell_EN = true;
                    }
                    else
                    {
                        model.Shell_EN = false;
                    }
                }
                if (row["ACIR_ErrCount"] != null && row["ACIR_ErrCount"].ToString() != "")
                {
                    model.ACIR_ErrCount = int.Parse(row["ACIR_ErrCount"].ToString());
                }
                if (row["ACIR_EN"] != null && row["ACIR_EN"].ToString() != "")
                {
                    if ((row["ACIR_EN"].ToString() == "1") || (row["ACIR_EN"].ToString().ToLower() == "true"))
                    {
                        model.ACIR_EN = true;
                    }
                    else
                    {
                        model.ACIR_EN = false;
                    }
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
            strSql.Append("select ChannelNo,OCV_ErrCount,OCV_EN,Shell_ErrCount,Shell_EN,ACIR_ErrCount,ACIR_EN ");
            strSql.Append(" FROM Channel_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQLite.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Channel_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQLite.GetSingle(strSql.ToString());
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
                strSql.Append("order by T.ChannelNo desc");
            }
            strSql.Append(")AS Row, T.*  from Channel_Info T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQLite.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@tblName", DbType.VarChar, 255),
					new SQLiteParameter("@fldName", DbType.VarChar, 255),
					new SQLiteParameter("@PageSize", DbType.Int32),
					new SQLiteParameter("@PageIndex", DbType.Int32),
					new SQLiteParameter("@IsReCount", DbType.bit),
					new SQLiteParameter("@OrderType", DbType.bit),
					new SQLiteParameter("@strWhere", DbType.VarChar,1000),
					};
			parameters[0].Value = "Channel_Info";
			parameters[1].Value = "ChannelNo";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQLite.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public bool DeleteAll()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Channel_Info");
            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion  ExtensionMethod
    }
}

