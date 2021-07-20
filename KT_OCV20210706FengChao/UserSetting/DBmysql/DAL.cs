
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using UserSetting;


namespace Pro.DAL.mysql
{
    /// <summary>
    /// 数据访问类:ProjectInfo
    /// </summary>
    public partial class ProjectInfo
    {
        private string connStr = "Database='test';Data Source='localhost';User Id='root';Password='root';charset='utf8';pooling=true";
        public ProjectInfo()
        { }
        public ProjectInfo(string conStr)
        {
            connStr = conStr;
        }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsUesr(string UesrName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where UserName='" + UesrName + "' ");
            DataSet ds = SqlHelper.ExecuteDataset(connStr,CommandType.Text, strSql.ToString());
            if (ds == null)
                return false;
            var dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                int count = int.Parse(dt.Rows[0][0].ToString());
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        public int GetUesrInfo(string UesrName, string UserPwd)
        {

            try
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
                DataSet ds = SqlHelper.ExecuteDataset(connStr, CommandType.Text, strSql.ToString());
                DataTable dt = ds.Tables[0];// = msh.ExecuteSqlToDatatable(strSql.ToString());
                if (dt.Rows.Count == 0)
                {
                    return 0;
                }
                if (dt.Rows.Count > 1)
                {
                    return 1;   //用户重复，默认最低权限
                }
                int UserAuthority = 0;
                if (dt.Rows[0]["UserAuthority"] != null)
                {
                    UserAuthority = int.Parse(dt.Rows[0]["UserAuthority"].ToString());
                }
                else
                {
                    UserAuthority = 6;  //无权限
                }
                return UserAuthority;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddUesrInfo(string UesrName, string UserPwd, int UserAuthority, string Userdisc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("UserName,UserPwd,UserAuthority,UserFlag,Userdisc)");
            strSql.Append(" values (");
            strSql.Append("'" + UesrName + "','" + UserPwd + "','" + UserAuthority + "','" + 1 + "','" + Userdisc + "')");
            int rows = SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, strSql.ToString());
            return rows;

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateUesrInfo(string UesrName, string UserPwd, int UserAuthority, string Userdisc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  UserInfo set ");
            strSql.Append("UserName= '" + UesrName + "', ");
            strSql.Append("UserPwd='" + UserPwd + "', ");
            strSql.Append("UserAuthority='" + UserAuthority + "', ");
            strSql.Append("UserFlag= 1  , ");
            strSql.Append("Userdisc='" + Userdisc + "' ");
            strSql.Append("  where UserName='" + UesrName + "' ");
            int rows = SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, strSql.ToString());
            return rows;

        }

        /// 删除一条数据
        /// </summary>
        public bool DeleteUesrInfo(string UesrName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
            strSql.Append(" where UserName='" + UesrName + "'");
            int rows = SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, strSql.ToString());
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
        public DataTable GetUserInfoList(int UserFlag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,Userdisc");
            strSql.Append(" FROM UserInfo ");
           // strSql.Append(" where UserFlag='" + UserFlag + "'");
            DataSet ds = SqlHelper.ExecuteDataset(connStr, CommandType.Text, strSql.ToString());
            //MySqlHelp msh = new MySqlHelp(this.connStr);
            //DataTable dt = msh.ExecuteSqlToDatatable(strSql.ToString());
            return ds.Tables[0];
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}


