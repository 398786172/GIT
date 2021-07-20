using OCV;
using System;
using System.Data;
using System.Data.SQLite;
using System.Text;


namespace DevInfo.DAL
{
    /// <summary>
    /// 数据访问类:SET_Info
    /// </summary>
    public partial class SET_Info
    {
        public SET_Info()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SetName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SET_Info");
            strSql.Append(" where SetName=@SetName ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@SetName", DbType.String,2147483647)           };
            parameters[0].Value = SetName;

            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DevInfo.Model.SET_Info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SET_Info(");
            strSql.Append("SetName,OCV_SetEN,OCV_TestTimes,OCV_UCL,OCV_LCL,Shell_SetEN,Shell_TestTimes,Shell_UCL,Shell_LCL,ACIR_SetEN,ACIR_TestTimes,ACIR_UCL,ACIR_LCL)");
            strSql.Append(" values (");
            strSql.Append("@SetName,@OCV_SetEN,@OCV_TestTimes,@OCV_UCL,@OCV_LCL,@Shell_SetEN,@Shell_TestTimes,@Shell_UCL,@Shell_LCL,@ACIR_SetEN,@ACIR_TestTimes,@ACIR_UCL,@ACIR_LCL)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@SetName", DbType.String,2147483647),
                    new SQLiteParameter("@OCV_SetEN", DbType.Boolean,1),
                    new SQLiteParameter("@OCV_TestTimes", DbType.Int32,4),
                    new SQLiteParameter("@OCV_UCL", DbType.Decimal,8),
                    new SQLiteParameter("@OCV_LCL", DbType.Decimal,8),
                    new SQLiteParameter("@Shell_SetEN", DbType.Boolean,1),
                    new SQLiteParameter("@Shell_TestTimes", DbType.Int32,4),
                    new SQLiteParameter("@Shell_UCL", DbType.Decimal,8),
                    new SQLiteParameter("@Shell_LCL", DbType.Decimal,8),
                    new SQLiteParameter("@ACIR_SetEN", DbType.Boolean,1),
                    new SQLiteParameter("@ACIR_TestTimes", DbType.Int32,4),
                    new SQLiteParameter("@ACIR_UCL", DbType.Decimal,8),
                    new SQLiteParameter("@ACIR_LCL", DbType.Decimal,8)};
            parameters[0].Value = model.SetName;
            parameters[1].Value = model.OCV_SetEN;
            parameters[2].Value = model.OCV_TestTimes;
            parameters[3].Value = model.OCV_UCL;
            parameters[4].Value = model.OCV_LCL;
            parameters[5].Value = model.Shell_SetEN;
            parameters[6].Value = model.Shell_TestTimes;
            parameters[7].Value = model.Shell_UCL;
            parameters[8].Value = model.Shell_LCL;
            parameters[9].Value = model.ACIR_SetEN;
            parameters[10].Value = model.ACIR_TestTimes;
            parameters[11].Value = model.ACIR_UCL;
            parameters[12].Value = model.ACIR_LCL;

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
        public bool Update(DevInfo.Model.SET_Info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SET_Info set ");
            strSql.Append("OCV_SetEN=@OCV_SetEN,");
            strSql.Append("OCV_TestTimes=@OCV_TestTimes,");
            strSql.Append("OCV_UCL=@OCV_UCL,");
            strSql.Append("OCV_LCL=@OCV_LCL,");
            strSql.Append("Shell_SetEN=@Shell_SetEN,");
            strSql.Append("Shell_TestTimes=@Shell_TestTimes,");
            strSql.Append("Shell_UCL=@Shell_UCL,");
            strSql.Append("Shell_LCL=@Shell_LCL,");
            strSql.Append("ACIR_SetEN=@ACIR_SetEN,");
            strSql.Append("ACIR_TestTimes=@ACIR_TestTimes,");
            strSql.Append("ACIR_UCL=@ACIR_UCL,");
            strSql.Append("ACIR_LCL=@ACIR_LCL");
            strSql.Append(" where SetName=@SetName ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@OCV_SetEN", DbType.Boolean,1),
                    new SQLiteParameter("@OCV_TestTimes", DbType.Int32,4),
                    new SQLiteParameter("@OCV_UCL", DbType.Decimal,8),
                    new SQLiteParameter("@OCV_LCL", DbType.Decimal,8),
                    new SQLiteParameter("@Shell_SetEN", DbType.Boolean,1),
                    new SQLiteParameter("@Shell_TestTimes", DbType.Int32,4),
                    new SQLiteParameter("@Shell_UCL", DbType.Decimal,8),
                    new SQLiteParameter("@Shell_LCL", DbType.Decimal,8),
                    new SQLiteParameter("@ACIR_SetEN", DbType.Boolean,1),
                    new SQLiteParameter("@ACIR_TestTimes", DbType.Int32,4),
                    new SQLiteParameter("@ACIR_UCL", DbType.Decimal,8),
                    new SQLiteParameter("@ACIR_LCL", DbType.Decimal,8),
                    new SQLiteParameter("@SetName", DbType.String,2147483647)};
            parameters[0].Value = model.OCV_SetEN;
            parameters[1].Value = model.OCV_TestTimes;
            parameters[2].Value = model.OCV_UCL;
            parameters[3].Value = model.OCV_LCL;
            parameters[4].Value = model.Shell_SetEN;
            parameters[5].Value = model.Shell_TestTimes;
            parameters[6].Value = model.Shell_UCL;
            parameters[7].Value = model.Shell_LCL;
            parameters[8].Value = model.ACIR_SetEN;
            parameters[9].Value = model.ACIR_TestTimes;
            parameters[10].Value = model.ACIR_UCL;
            parameters[11].Value = model.ACIR_LCL;
            parameters[12].Value = model.SetName;

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
        public bool Delete(string SetName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SET_Info ");
            strSql.Append(" where SetName=@SetName ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@SetName", DbType.String,2147483647)           };
            parameters[0].Value = SetName;

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
        public bool DeleteList(string SetNamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SET_Info ");
            strSql.Append(" where SetName in (" + SetNamelist + ")  ");
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
        public DevInfo.Model.SET_Info GetModel(string SetName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SetName,OCV_SetEN,OCV_TestTimes,OCV_UCL,OCV_LCL,Shell_SetEN,Shell_TestTimes,Shell_UCL,Shell_LCL,ACIR_SetEN,ACIR_TestTimes,ACIR_UCL,ACIR_LCL from SET_Info ");
            strSql.Append(" where SetName=@SetName ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@SetName", DbType.String,2147483647)           };
            parameters[0].Value = SetName;

            DevInfo.Model.SET_Info model = new DevInfo.Model.SET_Info();
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
        public DevInfo.Model.SET_Info DataRowToModel(DataRow row)
        {
            DevInfo.Model.SET_Info model = new DevInfo.Model.SET_Info();
            if (row != null)
            {
                if (row["SetName"] != null)
                {
                    model.SetName = row["SetName"].ToString();
                }
                if (row["OCV_SetEN"] != null && row["OCV_SetEN"].ToString() != "")
                {
                    if ((row["OCV_SetEN"].ToString() == "1") || (row["OCV_SetEN"].ToString().ToLower() == "true"))
                    {
                        model.OCV_SetEN = true;
                    }
                    else
                    {
                        model.OCV_SetEN = false;
                    }
                }
                if (row["OCV_TestTimes"] != null && row["OCV_TestTimes"].ToString() != "")
                {
                    model.OCV_TestTimes = int.Parse(row["OCV_TestTimes"].ToString());
                }
                if (row["OCV_UCL"] != null && row["OCV_UCL"].ToString() != "")
                {
                    model.OCV_UCL = decimal.Parse(row["OCV_UCL"].ToString());
                }
                if (row["OCV_LCL"] != null && row["OCV_LCL"].ToString() != "")
                {
                    model.OCV_LCL = decimal.Parse(row["OCV_LCL"].ToString());
                }
                if (row["Shell_SetEN"] != null && row["Shell_SetEN"].ToString() != "")
                {
                    if ((row["Shell_SetEN"].ToString() == "1") || (row["Shell_SetEN"].ToString().ToLower() == "true"))
                    {
                        model.Shell_SetEN = true;
                    }
                    else
                    {
                        model.Shell_SetEN = false;
                    }
                }
                if (row["Shell_TestTimes"] != null && row["Shell_TestTimes"].ToString() != "")
                {
                    model.Shell_TestTimes = int.Parse(row["Shell_TestTimes"].ToString());
                }
                if (row["Shell_UCL"] != null && row["Shell_UCL"].ToString() != "")
                {
                    model.Shell_UCL = decimal.Parse(row["Shell_UCL"].ToString());
                }
                if (row["Shell_LCL"] != null && row["Shell_LCL"].ToString() != "")
                {
                    model.Shell_LCL = decimal.Parse(row["Shell_LCL"].ToString());
                }
                if (row["ACIR_SetEN"] != null && row["ACIR_SetEN"].ToString() != "")
                {
                    if ((row["ACIR_SetEN"].ToString() == "1") || (row["ACIR_SetEN"].ToString().ToLower() == "true"))
                    {
                        model.ACIR_SetEN = true;
                    }
                    else
                    {
                        model.ACIR_SetEN = false;
                    }
                }
                if (row["ACIR_TestTimes"] != null && row["ACIR_TestTimes"].ToString() != "")
                {
                    model.ACIR_TestTimes = int.Parse(row["ACIR_TestTimes"].ToString());
                }
                if (row["ACIR_UCL"] != null && row["ACIR_UCL"].ToString() != "")
                {
                    model.ACIR_UCL = decimal.Parse(row["ACIR_UCL"].ToString());
                }
                if (row["ACIR_LCL"] != null && row["ACIR_LCL"].ToString() != "")
                {
                    model.ACIR_LCL = decimal.Parse(row["ACIR_LCL"].ToString());
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
            strSql.Append("select SetName,OCV_SetEN,OCV_TestTimes,OCV_UCL,OCV_LCL,Shell_SetEN,Shell_TestTimes,Shell_UCL,Shell_LCL,ACIR_SetEN,ACIR_TestTimes,ACIR_UCL,ACIR_LCL ");
            strSql.Append(" FROM SET_Info ");
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
            strSql.Append("select count(1) FROM SET_Info ");
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
                strSql.Append("order by T.SetName desc");
            }
            strSql.Append(")AS Row, T.*  from SET_Info T ");
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
            parameters[0].Value = "SET_Info";
            parameters[1].Value = "SetName";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQLite.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}