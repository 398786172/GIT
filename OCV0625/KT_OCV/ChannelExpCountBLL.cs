using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class ChannelExpCountBLL
    {
        private static readonly ChannelExpCountBLL _instance=new ChannelExpCountBLL();
        private ChannelExpCountBLL()
        {
        }

        public static ChannelExpCountBLL Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellList"> battery test data list</param>
        /// <returns></returns>
        public List<ChannelExpCount> ChannelAlaysis(List<ET_CELL> cellList)
        {

            try
            {
                var query_ok = (from p in cellList
                                where p.NgState == "OK"
                                select new ChannelExpCount { ChannelNo = p.Cell_Position, ExpCount = 0 }).ToList();
                var query_NG = (from p in cellList
                                where p.NgState == "NG"
                                select new ChannelExpCount { ChannelNo = p.Cell_Position, ExpCount = 1 }).ToList();
                string strSql_ok = GetUpdateString_OK(query_ok);
                string strSql_NG = GetUpdateString_NG(query_NG);
                string allSql = strSql_ok + strSql_NG;
                var result = ClsGlobal.sql_logDB.ExecuteQuery(allSql);
                int count = result.RecordsAffected;
                if (count <= 0)
                {
                    ClsGlobal.sql_logDB.ExecuteQuery(allSql);
                }
                string strWhere = " and ExpCount >=" + ClsGlobal.ChannelExp_n;
                var resultList = GetModelList(strWhere);
                return resultList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private string GetUpdateString_OK(List<ChannelExpCount> list)
        {
            if (list.Count == 0)
                return "";
            string txt = "update ChannelExpCount set ExpCount=0 where channelno in({0});";
            string channel = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                {
                    channel += list[i].ChannelNo + ",";
                }
                else
                {
                    channel += list[i].ChannelNo;
                }
            }
            string strSql = string.Format(txt, channel);
            return strSql;
        }

        private string GetUpdateString_NG(List<ChannelExpCount> list)
        {
            if (list.Count == 0)
                return "";
            string txt = "update ChannelExpCount set ExpCount=(ExpCount+1) where channelno in({0});";
            string channel = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                {
                    channel += list[i].ChannelNo + ",";
                }
                else
                {
                    channel += list[i].ChannelNo;
                }
            }
            string strSql = string.Format(txt, channel);
            return strSql;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ChannelExpCount> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ChannelExpCount> DataTableToList(DataTable dt)
        {
            List<ChannelExpCount> modelList = new List<ChannelExpCount>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ChannelExpCount model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ChannelExpCount ");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@id", DbType.Int32,4)
            };
            parameters[0].Value = id;

            int rows = ClsGlobal.sql_logDB.ExecuteNonQuery(strSql.ToString(), parameters);
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
            strSql.Append("delete from ChannelExpCount ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = ClsGlobal.sql_logDB.ExecuteNonQuery(strSql.ToString());
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ChannelNo,ExpCount ");
            strSql.Append(" FROM ChannelExpCount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ClsGlobal.sql_logDB.ExecuteDataSet(strSql.ToString(), null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ChannelExpCount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ChannelExpCount(");
            strSql.Append("ChannelNo,ExpCount)");
            strSql.Append(" values (");
            strSql.Append("@ChannelNo,@ExpCount)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4),
                    new SQLiteParameter("@ExpCount", DbType.Int32,4)};
            parameters[0].Value = model.ChannelNo;
            parameters[1].Value = model.ExpCount;

            int count = ClsGlobal.sql_logDB.ExecuteNonQuery(strSql.ToString(), parameters);
            return count;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ChannelExpCount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ChannelExpCount set ");
            strSql.Append("ChannelNo=@ChannelNo,");
            strSql.Append("Count=@ExpCount");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4),
                    new SQLiteParameter("@ExpCount", DbType.Int32,4),
                    new SQLiteParameter("@id", DbType.Int32,8)};
            parameters[0].Value = model.ChannelNo;
            parameters[1].Value = model.ExpCount;
            parameters[2].Value = model.id;

            int rows = ClsGlobal.sql_logDB.ExecuteNonQuery(strSql.ToString(), parameters);
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
        public ChannelExpCount DataRowToModel(DataRow row)
        {
            ChannelExpCount model = new ChannelExpCount();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["ChannelNo"] != null && row["ChannelNo"].ToString() != "")
                {
                    model.ChannelNo = int.Parse(row["ChannelNo"].ToString());
                }
                if (row["ExpCount"] != null && row["ExpCount"].ToString() != "")
                {
                    model.ExpCount = int.Parse(row["ExpCount"].ToString());
                }
            }
            return model;
        }

    }
}
