using OCV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class CellBindBLL
    {
        private static readonly CellBindBLL _instance = new CellBindBLL();
        private CellBindBLL()
        {

        }

        public static CellBindBLL Instance
        {
            get { return _instance; }
        }
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TrayCode,BindTime,Channel,CellCode,StateFlag,MODEL_NO ");
            strSql.Append(" FROM CellBind ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = ClsGlobal.sqlCodeData.ExecuteDataSet(strSql.ToString(), null);
            return ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<C_CellBindShow> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<C_CellBindShow> DataTableToList(DataTable dt)
        {
            List<C_CellBindShow> modelList = new List<C_CellBindShow>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                C_CellBindShow model;
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
        /// 得到一个对象实体
        /// </summary>
        public C_CellBindShow DataRowToModel(DataRow row)
        {
            C_CellBindShow model = new C_CellBindShow();
            if (row != null)
            {
                model.TrayIndex = 0;
                if (row["TrayCode"] != null)
                {
                    model.TrayCode = row["TrayCode"].ToString();
                }
                if (row["BindTime"] != null && row["BindTime"].ToString() != "")
                {
                    model.BindTime = row["BindTime"].ToString();
                }
                if (row["Channel"] != null && row["Channel"].ToString() != "")
                {
                    model.Channel = row["Channel"].ToString();
                }
                if (row["CellCode"] != null)
                {
                    model.CellCode = row["CellCode"].ToString();
                }
                if (row["StateFlag"] != null && row["StateFlag"].ToString() != "")
                {
                    model.StateFlag = int.Parse(row["StateFlag"].ToString());
                }
                if (row["MODEL_NO"] != null)
                {
                    model.MODEL_NO = row["MODEL_NO"].ToString();
                }
            }
            return model;
        }
    }
}

