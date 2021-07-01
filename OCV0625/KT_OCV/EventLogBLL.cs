using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class EventLogBLL
    {
        private static readonly EventLogBLL _instance = new EventLogBLL();

        private EventLogBLL()
        {

        }
        public static EventLogBLL Instance
        {
            get { return _instance; }
        }

        public double DateTimeToDouble(DateTime dt)
        {
            try
            {
                string strTime = dt.ToString("yyyyMMddHHmmss");
                double value = double.Parse(strTime);
                return value;
            }
            catch
            {
                return double.MaxValue;
            }
        }

        public bool Add(List<OCVLog> list)
        {
            string txt = "('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8})";
            string strValues = "";
            for (int i = 0; i < list.Count; i++)
            {
                var m = list[i];
                if (i < list.Count - 1)
                    strValues += string.Format(txt, m.ID, m.TrayCode, m.CellCode, m.ChannelNo, m.PCName, m.ExpCode, m.Describe, m.ExpTime, m.TimeCut) + ",";
                else
                    strValues += string.Format(txt, m.ID, m.TrayCode, m.CellCode, m.ChannelNo, m.PCName, m.ExpCode, m.Describe, m.ExpTime, m.TimeCut);
            }
            string strSql = "insert into OCVLog values " + strValues;
            var result = ClsGlobal.sql_logDB.ExecuteQuery(strSql.ToString());
            var rows = result.RecordsAffected;
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
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OCVLog(");
            strSql.Append("ID,TrayCode,CellCode,ChannelNo,PCName,ExpCode,Describe,ExpTime,TimeCut)");
            strSql.Append(" values (");
            strSql.Append("@ID,@TrayCode,@CellCode,@ChannelNo,@PCName,@ExpCode,@Describe,@ExpTime,@TimeCut)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ID", DbType.String),
                    new SQLiteParameter("@TrayCode", DbType.String),
                    new SQLiteParameter("@CellCode", DbType.String),
                    new SQLiteParameter("@ChannelNo", DbType.Int32,4),
                    new SQLiteParameter("@PCName", DbType.String,50),
                    new SQLiteParameter("@ExpCode", DbType.String),
                    new SQLiteParameter("@Describe", DbType.String),
                    new SQLiteParameter("@ExpTime", DbType.String),
                    new SQLiteParameter("@TimeCut", DbType.Double,8)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TrayCode;
            parameters[2].Value = model.CellCode;
            parameters[3].Value = model.ChannelNo;
            parameters[4].Value = model.PCName;
            parameters[5].Value = model.ExpCode;
            parameters[6].Value = model.Describe;
            parameters[7].Value = model.ExpTime;
            parameters[8].Value = model.TimeCut;
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TrayCode,CellCode,ChannelNo,PCName,ExpCode,Describe,ExpTime,TimeCut ");
            strSql.Append(" FROM OCVLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ClsGlobal.sql_logDB.ExecuteDataSet(strSql.ToString(), null);
        }


        /// 得到一个对象实体
        /// </summary>
        public OCVLog DataRowToModel(DataRow row)
        {
            OCVLog model = new OCVLog();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["TrayCode"] != null)
                {
                    model.TrayCode = row["TrayCode"].ToString();
                }
                if (row["CellCode"] != null)
                {
                    model.CellCode = row["CellCode"].ToString();
                }
                if (row["ChannelNo"] != null && row["ChannelNo"].ToString() != "")
                {
                    model.ChannelNo = int.Parse(row["ChannelNo"].ToString());
                }
                if (row["PCName"] != null)
                {
                    model.PCName = row["PCName"].ToString();
                }
                if (row["ExpCode"] != null)
                {
                    model.ExpCode = row["ExpCode"].ToString();
                }
                if (row["Describe"] != null)
                {
                    model.Describe = row["Describe"].ToString();
                }
                if (row["ExpTime"] != null)
                {
                    model.ExpTime = row["ExpTime"].ToString();
                }
                if (row["TimeCut"] != null && row["TimeCut"].ToString() != "")
                {
                    model.TimeCut = double.Parse(row["TimeCut"].ToString());
                }
            }
            return model;
        }

        /// 得到一个对象实体
        /// </summary>
        public OCVLogExpNum DataRowToModel_ExpCount(DataRow row)
        {
            OCVLogExpNum model = new OCVLogExpNum();
            if (row != null)
            {

                if (row["TrayCode"] != null)
                {
                    model.TrayCode = row["TrayCode"].ToString();
                }

                if (row["num"] != null && row["num"].ToString() != "")
                {
                    model.ExpCount = int.Parse(row["num"].ToString());
                }

            }
            return model;
        }

        public List<ExpData> GetExpTrayExpNum()
        {
            List<ExpData> list = new List<ExpData>();
            try
            {

                DateTime minTime = DateTime.Now.AddDays(-ClsGlobal.Time_t);
                double cutTime = DateTimeToDouble(minTime);
                string txt = "select * from(select channelno,count(*) num from OCVLog  where TimeCut>{0} group by channelno ) t where t.num>={1}";
                //  string txt = "select * from(select traycode, count(*) num from OCVLog where TimeCut>{0} group by traycode) t where  t.num >= {1}";
                string strSql = string.Format(txt, cutTime, ClsGlobal.TrayExp_m);
                var ds = ClsGlobal.sql_logDB.ExecuteDataSet(strSql, null);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string channelno = ds.Tables[0].Rows[i]["channelno"].ToString();
                        int num = int.Parse(ds.Tables[0].Rows[i]["num"].ToString());
                        ExpData model = new ExpData() { ChannelNo = channelno, ExpCount = num };
                        list.Add(model);
                    }
                    return list;

                }
                else
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OCVLog> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OCVLog> DataTableToList(DataTable dt)
        {
            List<OCVLog> modelList = new List<OCVLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OCVLog model;
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

        public DataTable GetTable(string strChannels)
        {
            string txt = "select TrayCode 托盘, ChannelNo 通道,CellCode 电芯条码,ExpCode NG码,Describe 描述,ExpTime NG时间 from OCVLog where channelno in({0})";
            string strSql = string.Format(txt, strChannels);
            var ds = ClsGlobal.sql_logDB.ExecuteDataSet(strSql, null);
            if (ds != null)
                return ds.Tables[0];
            else
                return null;
        }
        public bool DeleteClear(string table)
        {
            string strSql = "";
            if (table == " OCVLog")
            {
                strSql = "delete from OCVLog";
            }
            else
            {
                strSql = "update ChannelExpCount set ExpCount=0";
            }
            var result = ClsGlobal.sql_logDB.ExecuteQuery(strSql);
            var rows = result.RecordsAffected;
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<OCVLogExpNum> DataTableToList_ExpCount(DataTable dt)
        {
            List<OCVLogExpNum> modelList = new List<OCVLogExpNum>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OCVLogExpNum model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel_ExpCount(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

    }
    public class ExpData
    {
        public string ChannelNo { get; set; }

        public int ExpCount { get; set; }
    }
}
