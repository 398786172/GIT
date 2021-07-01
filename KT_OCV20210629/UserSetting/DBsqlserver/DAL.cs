
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility.sql;//Please add references
using Pro.Model.sql;

namespace Pro.DAL.sql
{
    /// <summary>
    /// 数据访问类:ProjectInfo
    /// </summary>
    public partial class ProjectInfo
    {
        public ProjectInfo()
        { }
        public ProjectInfo(string connStr)
        {
            DbHelperSQL.connectionString = connStr;
        }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProcessInfo");
            strSql.Append(" where NO=@NO");
            SqlParameter[] parameters = {
                    new SqlParameter("@NO", SqlDbType.Int,4)
            };
            parameters[0].Value = NO;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.sql.ProjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProcessInfo(");
            strSql.Append("OCV_type,BatteryType,ProjectName,UpLmt_V,DownLmt_V,UpLmt_ACIR,DownLmt_ACIR,MaxVoltDrop,MinVoltDrop,UPLmt_Time,DownLmt_Time,K,TempBase,TempPara,ISOLATION,L_Dispiacement,R_Dispiacement)");
            strSql.Append(" values (");
            strSql.Append("@OCV_type,@BatteryType,@ProjectName,@UpLmt_V,@DownLmt_V,@UpLmt_ACIR,@DownLmt_ACIR,@MaxVoltDrop,@MinVoltDrop,@UPLmt_Time,@DownLmt_Time,@K,@TempBase,@TempPara,@ISOLATION,@L_Dispiacement,@R_Dispiacement)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@OCV_type", SqlDbType.NChar,10),
                    new SqlParameter("@BatteryType", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectName", SqlDbType.NChar,50),
                    new SqlParameter("@UpLmt_V", SqlDbType.Decimal,9),
                    new SqlParameter("@DownLmt_V", SqlDbType.Decimal,9),
                    new SqlParameter("@UpLmt_ACIR", SqlDbType.Decimal,9),
                    new SqlParameter("@DownLmt_ACIR", SqlDbType.Decimal,9),
                    new SqlParameter("@MaxVoltDrop", SqlDbType.Decimal,9),
                    new SqlParameter("@MinVoltDrop", SqlDbType.Decimal,9),
                    new SqlParameter("@UPLmt_Time", SqlDbType.Decimal,9),
                    new SqlParameter("@DownLmt_Time", SqlDbType.Decimal,9),
                    new SqlParameter("@K", SqlDbType.Decimal,9),
                    new SqlParameter("@TempBase", SqlDbType.Decimal,9),
                    new SqlParameter("@TempPara", SqlDbType.Decimal,9),
                    new SqlParameter("@ISOLATION", SqlDbType.NVarChar,50),
                    new SqlParameter("@L_Dispiacement", SqlDbType.Decimal,9),
                    new SqlParameter("@R_Dispiacement", SqlDbType.Decimal,9)};
            parameters[0].Value = model.OCV_type;
            parameters[1].Value = model.BatteryType;
            parameters[2].Value = model.ProjectName;
            parameters[3].Value = model.UpLmt_V;
            parameters[4].Value = model.DownLmt_V;
            parameters[5].Value = model.UpLmt_ACIR;
            parameters[6].Value = model.DownLmt_ACIR;
            parameters[7].Value = model.MaxVoltDrop;
            parameters[8].Value = model.MinVoltDrop;
            parameters[9].Value = model.UPLmt_Time;
            parameters[10].Value = model.DownLmt_Time;
            parameters[11].Value = model.K;
            parameters[12].Value = model.TempBase;
            parameters[13].Value = model.TempPara;
            parameters[14].Value = model.ISOLATION;
            parameters[15].Value = model.L_Dispiacement;
            parameters[16].Value = model.R_Dispiacement;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public int Update(Model.sql.ProjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProcessInfo set ");
            strSql.Append("OCV_type=@OCV_type,");
            strSql.Append("BatteryType=@BatteryType,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("UpLmt_V=@UpLmt_V,");
            strSql.Append("DownLmt_V=@DownLmt_V,");
            strSql.Append("UpLmt_ACIR=@UpLmt_ACIR,");
            strSql.Append("DownLmt_ACIR=@DownLmt_ACIR,");
            strSql.Append("MaxVoltDrop=@MaxVoltDrop,");
            strSql.Append("MinVoltDrop=@MinVoltDrop,");
            strSql.Append("UPLmt_Time=@UPLmt_Time,");
            strSql.Append("DownLmt_Time=@DownLmt_Time,");
            strSql.Append("K=@K,");
            strSql.Append("TempBase=@TempBase,");
            strSql.Append("TempPara=@TempPara,");
            strSql.Append("ISOLATION=@ISOLATION,");
            strSql.Append("L_Dispiacement=@L_Dispiacement,");
            strSql.Append("R_Dispiacement=@R_Dispiacement");
            strSql.Append(" where OCV_type=@OCV_type and BatteryType=@BatteryType and ProjectName=@ProjectName ");
            SqlParameter[] parameters = {
                    new SqlParameter("@OCV_type", SqlDbType.NChar,10),
                    new SqlParameter("@BatteryType", SqlDbType.NVarChar,50),
                    new SqlParameter("@ProjectName", SqlDbType.NChar,50),
                    new SqlParameter("@UpLmt_V", SqlDbType.Decimal,9),
                    new SqlParameter("@DownLmt_V", SqlDbType.Decimal,9),
                    new SqlParameter("@UpLmt_ACIR", SqlDbType.Decimal,9),
                    new SqlParameter("@DownLmt_ACIR", SqlDbType.Decimal,9),
                    new SqlParameter("@MaxVoltDrop", SqlDbType.Decimal,9),
                    new SqlParameter("@MinVoltDrop", SqlDbType.Decimal,9),
                    new SqlParameter("@UPLmt_Time", SqlDbType.Decimal,9),
                    new SqlParameter("@DownLmt_Time", SqlDbType.Decimal,9),
                    new SqlParameter("@K", SqlDbType.Decimal,9),
                    new SqlParameter("@TempBase", SqlDbType.Decimal,9),
                    new SqlParameter("@TempPara", SqlDbType.Decimal,9),
                    new SqlParameter("@ISOLATION", SqlDbType.NVarChar,50),
                    new SqlParameter("@L_Dispiacement", SqlDbType.Decimal,9),
                    new SqlParameter("@R_Dispiacement", SqlDbType.Decimal,9)};
            parameters[0].Value = model.OCV_type;
            parameters[1].Value = model.BatteryType;
            parameters[2].Value = model.ProjectName;
            parameters[3].Value = model.UpLmt_V;
            parameters[4].Value = model.DownLmt_V;
            parameters[5].Value = model.UpLmt_ACIR;
            parameters[6].Value = model.DownLmt_ACIR;
            parameters[7].Value = model.MaxVoltDrop;
            parameters[8].Value = model.MinVoltDrop;
            parameters[9].Value = model.UPLmt_Time;
            parameters[10].Value = model.DownLmt_Time;
            parameters[11].Value = model.K;
            parameters[12].Value = model.TempBase;
            parameters[13].Value = model.TempPara;
            parameters[14].Value = model.ISOLATION;
            parameters[15].Value = model.L_Dispiacement;
            parameters[16].Value = model.R_Dispiacement;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
         
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int NO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProcessInfo ");
            strSql.Append(" where NO=@NO");
            SqlParameter[] parameters = {
                    new SqlParameter("@NO", SqlDbType.Int,4)
            };
            parameters[0].Value = NO;

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

        /// 删除一条数据
        /// </summary>
        public bool Delete(string OCV_type, string BatteryType, string ProjectName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProcessInfo ");
            strSql.Append(" where OCV_type=@OCV_type and BatteryType=@BatteryType and ProjectName=@ProjectName ");
            SqlParameter[] parameters = {
                     new SqlParameter("@OCV_type", SqlDbType.NChar,10),
                     new SqlParameter("@BatteryType", SqlDbType.NVarChar,50),
                     new SqlParameter("@ProjectName", SqlDbType.NChar,50)  };
            parameters[0].Value = OCV_type;
            parameters[1].Value = BatteryType;
            parameters[2].Value = ProjectName;
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string OCV_type, string BatteryType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProcessInfo ");
            strSql.Append(" where OCV_type=@OCV_type and BatteryType=@BatteryType");
            SqlParameter[] parameters = {
                     new SqlParameter("@OCV_type", SqlDbType.NChar,10),
                     new SqlParameter("@BatteryType", SqlDbType.NVarChar,50) };
            parameters[0].Value = OCV_type;
            parameters[1].Value = BatteryType;

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
        public bool DeleteList(string NOlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProcessInfo ");
            strSql.Append(" where NO in (" + NOlist + ")  ");
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
        public Model.sql.ProjectInfo GetModel(int NO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OCV_type,BatteryType,ProjectName,UpLmt_V,DownLmt_V,UpLmt_ACIR,DownLmt_ACIR,MaxVoltDrop,MinVoltDrop,UPLmt_Time,DownLmt_Time,K,TempBase,TempPara,ISOLATION,L_Dispiacement,R_Dispiacement from ProcessInfo ");
            strSql.Append(" where NO=@NO");
            SqlParameter[] parameters = {
                    new SqlParameter("@NO", SqlDbType.Int,4)
            };
            parameters[0].Value = NO;

            Model.sql.ProjectInfo model = new Model.sql.ProjectInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
        public Model.sql.ProjectInfo DataRowToModel(DataRow row)
        {
            Model.sql.ProjectInfo model = new Model.sql.ProjectInfo();
            if (row != null)
            {
                if (row["OCV_type"] != null)
                {
                    model.OCV_type = row["OCV_type"].ToString();
                }
                if (row["BatteryType"] != null)
                {
                    model.BatteryType = row["BatteryType"].ToString();
                }
                if (row["ProjectName"] != null)
                {
                    model.ProjectName = row["ProjectName"].ToString();
                }
                if (row["UpLmt_V"] != null && row["UpLmt_V"].ToString() != "")
                {
                    model.UpLmt_V = decimal.Parse(row["UpLmt_V"].ToString());
                }
                if (row["DownLmt_V"] != null && row["DownLmt_V"].ToString() != "")
                {
                    model.DownLmt_V = decimal.Parse(row["DownLmt_V"].ToString());
                }
                if (row["UpLmt_ACIR"] != null && row["UpLmt_ACIR"].ToString() != "")
                {
                    model.UpLmt_ACIR = decimal.Parse(row["UpLmt_ACIR"].ToString());
                }
                if (row["DownLmt_ACIR"] != null && row["DownLmt_ACIR"].ToString() != "")
                {
                    model.DownLmt_ACIR = decimal.Parse(row["DownLmt_ACIR"].ToString());
                }
                if (row["MaxVoltDrop"] != null && row["MaxVoltDrop"].ToString() != "")
                {
                    model.MaxVoltDrop = decimal.Parse(row["MaxVoltDrop"].ToString());
                }
                if (row["MinVoltDrop"] != null && row["MinVoltDrop"].ToString() != "")
                {
                    model.MinVoltDrop = decimal.Parse(row["MinVoltDrop"].ToString());
                }
                if (row["UPLmt_Time"] != null && row["UPLmt_Time"].ToString() != "")
                {
                    model.UPLmt_Time = decimal.Parse(row["UPLmt_Time"].ToString());
                }
                if (row["DownLmt_Time"] != null && row["DownLmt_Time"].ToString() != "")
                {
                    model.DownLmt_Time = decimal.Parse(row["DownLmt_Time"].ToString());
                }
                if (row["K"] != null && row["K"].ToString() != "")
                {
                    model.K = decimal.Parse(row["K"].ToString());
                }
                if (row["TempBase"] != null && row["TempBase"].ToString() != "")
                {
                    model.TempBase = decimal.Parse(row["TempBase"].ToString());
                }
                if (row["TempPara"] != null && row["TempPara"].ToString() != "")
                {
                    model.TempPara = decimal.Parse(row["TempPara"].ToString());
                }
                if (row["ISOLATION"] != null)
                {
                    model.ISOLATION = row["ISOLATION"].ToString();
                }
                if (row["L_Dispiacement"] != null && row["L_Dispiacement"].ToString() != "")
                {
                    model.L_Dispiacement = decimal.Parse(row["L_Dispiacement"].ToString());
                }
                if (row["R_Dispiacement"] != null && row["R_Dispiacement"].ToString() != "")
                {
                    model.R_Dispiacement = decimal.Parse(row["R_Dispiacement"].ToString());
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
            strSql.Append("select OCV_type,BatteryType,ProjectName,UpLmt_V,DownLmt_V,UpLmt_ACIR,DownLmt_ACIR,MaxVoltDrop,MinVoltDrop,UPLmt_Time,DownLmt_Time,K,TempBase,TempPara,ISOLATION,L_Dispiacement,R_Dispiacement ");
            strSql.Append(" FROM ProcessInfo ");
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
            strSql.Append(" OCV_type,BatteryType,ProjectName,UpLmt_V,DownLmt_V,UpLmt_ACIR,DownLmt_ACIR,MaxVoltDrop,MinVoltDrop,UPLmt_Time,DownLmt_Time,K,TempBase,TempPara,ISOLATION,L_Dispiacement,R_Dispiacement ");
            strSql.Append(" FROM ProcessInfo ");
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
            strSql.Append("select count(1) FROM ProcessInfo ");
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
                strSql.Append("order by T.NO desc");
            }
            strSql.Append(")AS Row, T.*  from ProjectInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsUesr(string UesrName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where UserName='" + UesrName + "' ");
            return DbHelperSQL.Exists(strSql.ToString());
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        public int GetUesrInfo(string UesrName, string UserPwd)
        {
            string strWhere = "";
            strWhere += "UserName='" + UesrName.Trim().ToUpper() + "'and ";
            strWhere += "UserPwd='" + UserPwd.Trim() + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,UserPwd,UserAuthority");
            strSql.Append(" FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = new DataSet();
            ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count == 0)
            {
                return 0;
            }
            if (ds.Tables[0].Rows.Count > 1)
            {
                return 1;   //用户重复，默认最低权限
            }
            int UserAuthority = 0;
            if (ds.Tables[0].Rows[0]["UserAuthority"] != null)
            {
                UserAuthority = int.Parse(ds.Tables[0].Rows[0]["UserAuthority"].ToString());
            }
            else
            {
                UserAuthority = 6;  //无权限
            }
            return UserAuthority;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddUesrInfo(string UesrName, string UserPwd, int UserAuthority,string Userdisc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("UserName,UserPwd,UserAuthority,UserFlag,Userdisc)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@UserPwd,@UserAuthority,@UserFlag,@Userdisc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserPwd", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserAuthority", SqlDbType.Int,4),
                    new SqlParameter("@UserFlag", SqlDbType.Int, 4),
                    new SqlParameter("@Userdisc", SqlDbType.NVarChar,50)};
        parameters[0].Value = UesrName;
            parameters[1].Value = UserPwd;
            parameters[2].Value = UserAuthority;
            parameters[3].Value = 1;
            parameters[4].Value = Userdisc;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
            
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateUesrInfo(string UesrName, string UserPwd, int UserAuthority, string Userdisc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserPwd=@UserPwd,");
            strSql.Append("UserAuthority=@UserAuthority,");
            strSql.Append("UserFlag=@UserFlag,");
            strSql.Append("Userdisc=@Userdisc");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserPwd", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserAuthority", SqlDbType.Int,4),
                    new SqlParameter("@UserFlag", SqlDbType.Int, 4),
                     new SqlParameter("@Userdisc", SqlDbType.NVarChar,50)};
             parameters[0].Value = UesrName;
            parameters[1].Value = UserPwd;
            parameters[2].Value = UserAuthority;
            parameters[3].Value = 1;
            parameters[4].Value = Userdisc;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;

        }

        /// 删除一条数据
        /// </summary>
        public bool DeleteUesrInfo(string UesrName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
                     new SqlParameter("@UserName", SqlDbType.NChar,10) };        
            parameters[0].Value = UesrName;       
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetUserInfoList(int UserFlag)
        {
            DataSet ds = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,Userdisc");
            strSql.Append(" FROM UserInfo ");
            strSql.Append(" where UserFlag='" + UserFlag+"'");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}


