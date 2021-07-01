using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCV.PLCContr.Model;
using OCV;

namespace OCV.PLCContr.DAL
{
    public class PLCInfoDAL
    {
        private string dbConnect = "";
        public PLCInfoDAL(string dbConnect)
        {
            this.dbConnect = dbConnect;
        }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PLCInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PLCInfo(");
            strSql.Append("ControlName,ControlType,PageName,SubPageName,RegisterAddress,ValueType,ByteName,LinkName,Memo,WriteEnable)");
            strSql.Append(" values (");
            strSql.Append("@ControlName,@ControlType,@PageName,@SubPageName,@RegisterAddress,@ValueType,@ByteName,@LinkName,@Memo,@WriteEnable)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ControlName", DbType.String),
                    new SQLiteParameter("@ControlType", DbType.String),
                    new SQLiteParameter("@PageName", DbType.String),
                    new SQLiteParameter("@SubPageName", DbType.String),
                    new SQLiteParameter("@RegisterAddress", DbType.String),
                    new SQLiteParameter("@ValueType", DbType.String),
                    new SQLiteParameter("@ByteName", DbType.String),
                    new SQLiteParameter("@LinkName", DbType.String),
                    new SQLiteParameter("@Memo", DbType.String),
                    new SQLiteParameter("@WriteEnable", DbType.Boolean,1)};
            parameters[0].Value = model.ControlName;
            parameters[1].Value = model.ControlType;
            parameters[2].Value = model.PageName;
            parameters[3].Value = model.SubPageName;
            parameters[4].Value = model.RegisterAddress;
            parameters[5].Value = model.ValueType;
            parameters[6].Value = model.ByteName;
            parameters[7].Value = model.LinkName;
            parameters[8].Value = model.Memo;
            parameters[9].Value = model.WriteEnable;

            int count = SQLiteHelper.ExecuteNonQuery(dbConnect, strSql.ToString(), CommandType.Text, parameters);
            return count;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PLCInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PLCInfo set ");
            strSql.Append("ControlName=@ControlName,");
            strSql.Append("ControlType=@ControlType,");
            strSql.Append("PageName=@PageName,");
            strSql.Append("SubPageName=@SubPageName,");
            strSql.Append("RegisterAddress=@RegisterAddress,");
            strSql.Append("ValueType=@ValueType,");
            strSql.Append("ByteName=@ByteName,");
            strSql.Append("LinkName=@LinkName,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("WriteEnable=@WriteEnable");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ControlName", DbType.String),
                    new SQLiteParameter("@ControlType", DbType.String),
                    new SQLiteParameter("@PageName", DbType.String),
                    new SQLiteParameter("@SubPageName", DbType.String),
                    new SQLiteParameter("@RegisterAddress", DbType.String),
                    new SQLiteParameter("@ValueType", DbType.String),
                    new SQLiteParameter("@ByteName", DbType.String),
                    new SQLiteParameter("@LinkName", DbType.String),
                    new SQLiteParameter("@Memo", DbType.String),
                    new SQLiteParameter("@WriteEnable", DbType.Boolean,1),
                    new SQLiteParameter("@id", DbType.Int32,8)};
            parameters[0].Value = model.ControlName;
            parameters[1].Value = model.ControlType;
            parameters[2].Value = model.PageName;
            parameters[3].Value = model.SubPageName;
            parameters[4].Value = model.RegisterAddress;
            parameters[5].Value = model.ValueType;
            parameters[6].Value = model.ByteName;
            parameters[7].Value = model.LinkName;
            parameters[8].Value = model.Memo;
            parameters[9].Value = model.WriteEnable;
            parameters[10].Value = model.id;

            int rows = SQLiteHelper.ExecuteNonQuery(dbConnect, strSql.ToString(), CommandType.Text, parameters);
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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PLCInfo ");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@id", DbType.Int32,4)
            };
            parameters[0].Value = id;
            int rows = SQLiteHelper.ExecuteNonQuery(dbConnect, strSql.ToString(), CommandType.Text, parameters);
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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PLCInfo ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = SQLiteHelper.ExecuteNonQuery(dbConnect, strSql.ToString(), CommandType.Text, null);
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
        public PLCInfoModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ControlName,ControlType,PageName,SubPageName,RegisterAddress,ValueType,ByteName,LinkName,Memo,WriteEnable from PLCInfo ");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@id", DbType.Int32,4)
            };
            parameters[0].Value = id;
            DataSet ds = SQLiteHelper.ExecuteDataSet(dbConnect, strSql.ToString(), CommandType.Text, parameters);
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
        public PLCInfoModel DataRowToModel(DataRow row)
        {
            PLCInfoModel model = new PLCInfoModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["ControlName"] != null)
                {
                    model.ControlName = row["ControlName"].ToString();
                }
                if (row["ControlType"] != null)
                {
                    model.ControlType = row["ControlType"].ToString();
                }
                if (row["PageName"] != null)
                {
                    model.PageName = row["PageName"].ToString();
                }
                if (row["SubPageName"] != null)
                {
                    model.SubPageName = row["SubPageName"].ToString();
                }
                if (row["RegisterAddress"] != null)
                {
                    model.RegisterAddress = row["RegisterAddress"].ToString();
                }
                if (row["ValueType"] != null)
                {
                    model.ValueType = row["ValueType"].ToString();
                }
                if (row["ByteName"] != null)
                {
                    model.ByteName = row["ByteName"].ToString();
                }
                if (row["LinkName"] != null)
                {
                    model.LinkName = row["LinkName"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["WriteEnable"] != null && row["WriteEnable"].ToString() != "")
                {
                    if ((row["WriteEnable"].ToString() == "1") || (row["WriteEnable"].ToString().ToLower() == "true"))
                    {
                        model.WriteEnable = true;
                    }
                    else
                    {
                        model.WriteEnable = false;
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
            strSql.Append("select id,ControlName,ControlType,PageName,SubPageName,RegisterAddress,ValueType,ByteName,LinkName,Memo,WriteEnable ");
            strSql.Append(" FROM PLCInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLiteHelper.ExecuteDataSet(dbConnect, strSql.ToString(), CommandType.Text);
        }

        #endregion  BasicMethod

    }
}
