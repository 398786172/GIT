using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using OCV;
using OCV.OCVLogs;
using MySql.Data.MySqlClient;


namespace DB_OCV.DAL
{
    /// <summary>
    /// 数据访问类:BatData
    /// </summary>
    public partial class BatData
    {
      
        private MySqlConnection conn = null;
        private string connStr = "Database='test';Data Source='localhost';User Id='root';Password='root';charset='utf8';pooling=true";
        public BatData()
        {
        }

        public BatData(string conStr)
        {
            connStr = conStr;
        }
        private void InitConnString()
        {
            conn.ConnectionString = connStr;
        }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BatCode, DateTime TestEndTime)
        {
            return false;
        }

       
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(string tableName, Model.BatInfo_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            if (model.Eqp_ID != null)
            {
                strSql1.Append("Eqp_ID,");
                strSql2.Append("'" + model.Eqp_ID + "',");
            }
            if (model.PC_ID != null)
            {
                strSql1.Append("PC_ID,");
                strSql2.Append("'" + model.PC_ID + "',");
            }
            if (model.OPERATION != null)
            {
                strSql1.Append("OPERATION,");
                strSql2.Append("'" + model.OPERATION + "',");
            }
            if (model.IS_TRANS != null)
            {
                strSql1.Append("IS_TRANS,");
                strSql2.Append("" + model.IS_TRANS + ",");
            }
            if (model.TRAY_ID != null)
            {
                strSql1.Append("TRAY_ID,");
                strSql2.Append("'" + model.TRAY_ID + "',");
            }
            if (model.CELL_ID != null)
            {
                strSql1.Append("CELL_ID,");
                strSql2.Append("'" + model.CELL_ID + "',");
            }
            if (model.BATTERY_POS != null)
            {
                strSql1.Append("BATTERY_POS,");
                strSql2.Append("" + model.BATTERY_POS + ",");
            }
            if (model.MODEL_NO != null)
            {
                strSql1.Append("MODEL_NO,");
                strSql2.Append("'" + model.MODEL_NO + "',");
            }
            if (model.BATCH_NO != null)
            {
                strSql1.Append("BATCH_NO,");
                strSql2.Append("'" + model.BATCH_NO + "',");
            }
            if (model.TOTAL_NG_STATE != null)
            {
                strSql1.Append("TOTAL_NG_STATE,");
                strSql2.Append("'" + model.TOTAL_NG_STATE + "',");
            }
            if (model.OCV_VOLTAGE != null)
            {
                strSql1.Append("OCV_VOLTAGE,");
                strSql2.Append("" + model.OCV_VOLTAGE + ",");
            }
            if (model.ACIR != null)
            {
                strSql1.Append("ACIR,");
                strSql2.Append("" + model.ACIR + ",");
            }
            if (model.TEST_NG_CODE != null)
            {
                strSql1.Append("TEST_NG_CODE,");
                strSql2.Append("'" + model.TEST_NG_CODE + "',");
            }
            if (model.TEST_RESULT != null)
            {
                strSql1.Append("TEST_RESULT,");
                strSql2.Append("'" + model.TEST_RESULT + "',");
            }
            if (model.TEST_RESULT_DESC != null)
            {
                strSql1.Append("TEST_RESULT_DESC,");
                strSql2.Append("'" + model.TEST_RESULT_DESC + "',");
            }

            if (model.PostiveSHELL_VOLTAGE != null)
            {
                strSql1.Append("PostiveSHELL_VOLTAGE,");
                strSql2.Append("" + model.PostiveSHELL_VOLTAGE + ",");
            }
            if (model.PostiveSV_NG_CODE != null)
            {
                strSql1.Append("PostiveSV_NG_CODE,");
                strSql2.Append("'" + model.PostiveSV_NG_CODE + "',");
            }
            if (model.PostiveSV_RESULT != null)
            {
                strSql1.Append("PostiveSV_RESULT,");
                strSql2.Append("'" + model.PostiveSV_RESULT + "',");
            }
            if (model.PostiveSV_RESULT_DESC != null)
            {
                strSql1.Append("PostiveSV_RESULT_DESC,");
                strSql2.Append("'" + model.PostiveSV_RESULT_DESC + "',");
            }

            if (model.SHELL_VOLTAGE != null)
            {
                strSql1.Append("SHELL_VOLTAGE,");
                strSql2.Append("" + model.SHELL_VOLTAGE + ",");
            }
            if (model.SV_NG_CODE != null)
            {
                strSql1.Append("SV_NG_CODE,");
                strSql2.Append("'" + model.SV_NG_CODE + "',");
            }
            if (model.SV_RESULT != null)
            {
                strSql1.Append("SV_RESULT,");
                strSql2.Append("'" + model.SV_RESULT + "',");
            }
            if (model.SV_RESULT_DESC != null)
            {
                strSql1.Append("SV_RESULT_DESC,");
                strSql2.Append("'" + model.SV_RESULT_DESC + "',");
            }
            if (model.POSTIVE_TEMP != null)
            {
                strSql1.Append("POSTIVE_TEMP,");
                strSql2.Append("" + model.POSTIVE_TEMP + ",");
            }
            if (model.NEGATIVE_TEMP != null)
            {
                strSql1.Append("NEGATIVE_TEMP,");
                strSql2.Append("" + model.NEGATIVE_TEMP + ",");
            }
            if (model.K != null)
            {
                strSql1.Append("K,");
                strSql2.Append("" + model.K + ",");
            }
            if (model.V_DROP != null)
            {
                strSql1.Append("V_DROP,");
                strSql2.Append("" + model.V_DROP + ",");
            }
            if (model.V_DROP_RANGE != null)
            {
                strSql1.Append("V_DROP_RANGE,");
                strSql2.Append("" + model.V_DROP_RANGE + ",");
            }
            if (model.V_DROP_RANGE_CODE != null)
            {
                strSql1.Append("V_DROP_RANGE_CODE,");
                strSql2.Append("'" + model.V_DROP_RANGE_CODE + "',");
            }
            if (model.V_DROP_RESULT != null)
            {
                strSql1.Append("V_DROP_RESULT,");
                strSql2.Append("'" + model.V_DROP_RESULT + "',");
            }
            if (model.V_DROP_RESULT_DESC != null)
            {
                strSql1.Append("V_DROP_RESULT_DESC,");
                strSql2.Append("'" + model.V_DROP_RESULT_DESC + "',");
            }
            if (model.ACIR_RANGE != null)
            {
                strSql1.Append("ACIR_RANGE,");
                strSql2.Append("" + model.ACIR_RANGE + ",");
            }
            if (model.R_RANGE_NG_CODE != null)
            {
                strSql1.Append("R_RANGE_NG_CODE,");
                strSql2.Append("'" + model.R_RANGE_NG_CODE + "',");
            }
            if (model.R_RANGE_RESULT != null)
            {
                strSql1.Append("R_RANGE_RESULT,");
                strSql2.Append("'" + model.R_RANGE_RESULT + "',");
            }
            if (model.R_RANGE_RESULT_DESC != null)
            {
                strSql1.Append("R_RANGE_RESULT_DESC,");
                strSql2.Append("'" + model.R_RANGE_RESULT_DESC + "',");
            }
            if (model.REV_OCV != null)
            {
                strSql1.Append("REV_OCV,");
                strSql2.Append("" + model.REV_OCV + ",");
            }
            if (model.CAPACITY != null)
            {
                strSql1.Append("CAPACITY,");
                strSql2.Append("" + model.CAPACITY + ",");
            }
            if (model.END_DATE_TIME != null)
            {
                strSql1.Append("END_DATE_TIME,");
                strSql2.Append("'" + model.END_DATE_TIME + "',");
            }
            if (model.TestMode!= null)
            {
                strSql1.Append("TestMode,");
                strSql2.Append("'" + model.TestMode + "',");
            }

            strSql.Append("insert into " + tableName + "(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            ClsLogs.SqllogNet.WriteDebug("ADD()", "增加一条数据:" + strSql.ToString());

            SQLServerHelp msh = new SQLServerHelp(this.connStr);
            int rows = msh.ExecuteSql(strSql.ToString());
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
        public bool Update(string tableName, DB_OCV.Model.BatInfo_Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  " + tableName + " set ");
            if (model.Eqp_ID != null)
            {
                strSql.Append("Eqp_ID='" + model.Eqp_ID + "',");
            }
            else
            {
                strSql.Append("Eqp_ID= null ,");
            }
            if (model.PC_ID != null)
            {
                strSql.Append("PC_ID='" + model.PC_ID + "',");
            }
            else
            {
                strSql.Append("PC_ID= null ,");
            }
            if (model.OPERATION != null)
            {
                strSql.Append("OPERATION='" + model.OPERATION + "',");
            }
            else
            {
                strSql.Append("OPERATION= null ,");
            }
            if (model.IS_TRANS != null)
            {
                strSql.Append("IS_TRANS=" + model.IS_TRANS + ",");
            }
            else
            {
                strSql.Append("IS_TRANS= null ,");
            }
            if (model.TRAY_ID != null)
            {
                strSql.Append("TRAY_ID='" + model.TRAY_ID + "',");
            }
            else
            {
                strSql.Append("TRAY_ID= null ,");
            }
            if (model.CELL_ID != null)
            {
                strSql.Append("CELL_ID='" + model.CELL_ID + "',");
            }
            else
            {
                strSql.Append("CELL_ID= null ,");
            }
            if (model.BATTERY_POS != null)
            {
                strSql.Append("BATTERY_POS=" + model.BATTERY_POS + ",");
            }
            else
            {
                strSql.Append("BATTERY_POS= null ,");
            }
            if (model.MODEL_NO != null)
            {
                strSql.Append("MODEL_NO='" + model.MODEL_NO + "',");
            }
            else
            {
                strSql.Append("MODEL_NO= null ,");
            }
            if (model.BATCH_NO != null)
            {
                strSql.Append("BATCH_NO='" + model.BATCH_NO + "',");
            }
            else
            {
                strSql.Append("BATCH_NO= null ,");
            }
            if (model.TOTAL_NG_STATE != null)
            {
                strSql.Append("TOTAL_NG_STATE='" + model.TOTAL_NG_STATE + "',");
            }
            else
            {
                strSql.Append("TOTAL_NG_STATE= null ,");
            }

            if (model.OCV_VOLTAGE != null)
            {
                strSql.Append("OCV_VOLTAGE=" + model.OCV_VOLTAGE + ",");
            }
            else
            {
                strSql.Append("OCV_VOLTAGE= null ,");
            }
            if (model.ACIR != null)
            {
                strSql.Append("ACIR=" + model.ACIR + ",");
            }
            else
            {
                strSql.Append("ACIR= null ,");
            }
            if (model.TEST_NG_CODE != null)
            {
                strSql.Append("TEST_NG_CODE='" + model.TEST_NG_CODE + "',");
            }
            else
            {
                strSql.Append("TEST_NG_CODE= null ,");
            }
            if (model.TEST_RESULT != null)
            {
                strSql.Append("TEST_RESULT='" + model.TEST_RESULT + "',");
            }
            else
            {
                strSql.Append("TEST_RESULT= null ,");
            }
            if (model.TEST_RESULT_DESC != null)
            {
                strSql.Append("TEST_RESULT_DESC='" + model.TEST_RESULT_DESC + "',");
            }
            else
            {
                strSql.Append("TEST_RESULT_DESC= null ,");
            }
            if (model.SHELL_VOLTAGE != null)
            {
                strSql.Append("SHELL_VOLTAGE=" + model.SHELL_VOLTAGE + ",");
            }
            else
            {
                strSql.Append("SHELL_VOLTAGE= null ,");
            }
            if (model.SV_NG_CODE != null)
            {
                strSql.Append("SV_NG_CODE='" + model.SV_NG_CODE + "',");
            }
            else
            {
                strSql.Append("SV_NG_CODE= null ,");
            }
            if (model.SV_RESULT != null)
            {
                strSql.Append("SV_RESULT='" + model.SV_RESULT + "',");
            }
            else
            {
                strSql.Append("SV_RESULT= null ,");
            }
            if (model.SV_RESULT_DESC != null)
            {
                strSql.Append("SV_RESULT_DESC='" + model.SV_RESULT_DESC + "',");
            }
            else
            {
                strSql.Append("SV_RESULT_DESC= null ,");
            }
            if (model.POSTIVE_TEMP != null)
            {
                strSql.Append("POSTIVE_TEMP=" + model.POSTIVE_TEMP + ",");
            }
            else
            {
                strSql.Append("POSTIVE_TEMP= null ,");
            }
            if (model.NEGATIVE_TEMP != null)
            {
                strSql.Append("NEGATIVE_TEMP=" + model.NEGATIVE_TEMP + ",");
            }
            else
            {
                strSql.Append("NEGATIVE_TEMP= null ,");
            }
            if (model.K != null)
            {
                strSql.Append("K=" + model.K + ",");
            }
            else
            {
                strSql.Append("K= null ,");
            }
            if (model.V_DROP != null)
            {
                strSql.Append("V_DROP=" + model.V_DROP + ",");
            }
            else
            {
                strSql.Append("V_DROP= null ,");
            }
            if (model.V_DROP_RANGE != null)
            {
                strSql.Append("V_DROP_RANGE=" + model.V_DROP_RANGE + ",");
            }
            else
            {
                strSql.Append("V_DROP_RANGE= null ,");
            }
            if (model.V_DROP_RANGE_CODE != null)
            {
                strSql.Append("V_DROP_RANGE_CODE='" + model.V_DROP_RANGE_CODE + "',");
            }
            else
            {
                strSql.Append("V_DROP_RANGE_CODE= null ,");
            }
            if (model.V_DROP_RESULT != null)
            {
                strSql.Append("V_DROP_RESULT='" + model.V_DROP_RESULT + "',");
            }
            else
            {
                strSql.Append("V_DROP_RESULT= null ,");
            }
            if (model.V_DROP_RESULT_DESC != null)
            {
                strSql.Append("V_DROP_RESULT_DESC='" + model.V_DROP_RESULT_DESC + "',");
            }
            else
            {
                strSql.Append("V_DROP_RESULT_DESC= null ,");
            }
            if (model.ACIR_RANGE != null)
            {
                strSql.Append("ACIR_RANGE=" + model.ACIR_RANGE + ",");
            }
            else
            {
                strSql.Append("ACIR_RANGE= null ,");
            }
            if (model.R_RANGE_NG_CODE != null)
            {
                strSql.Append("R_RANGE_NG_CODE='" + model.R_RANGE_NG_CODE + "',");
            }
            else
            {
                strSql.Append("R_RANGE_NG_CODE= null ,");
            }
            if (model.R_RANGE_RESULT != null)
            {
                strSql.Append("R_RANGE_RESULT='" + model.R_RANGE_RESULT + "',");
            }
            else
            {
                strSql.Append("R_RANGE_RESULT= null ,");
            }
            if (model.R_RANGE_RESULT_DESC != null)
            {
                strSql.Append("R_RANGE_RESULT_DESC='" + model.R_RANGE_RESULT_DESC + "',");
            }
            else
            {
                strSql.Append("R_RANGE_RESULT_DESC= null ,");
            }
            if (model.REV_OCV != null)
            {
                strSql.Append("REV_OCV=" + model.REV_OCV + ",");
            }
            else
            {
                strSql.Append("REV_OCV= null ,");
            }
            if (model.CAPACITY != null)
            {
                strSql.Append("CAPACITY=" + model.CAPACITY + ",");
            }
            else
            {
                strSql.Append("CAPACITY= null ,");
            }
            if (model.END_DATE_TIME != null)
            {
                strSql.Append("END_DATE_TIME='" + model.END_DATE_TIME + "',");
            }
            else
            {
                strSql.Append("END_DATE_TIME= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where  TRAY_ID='" + model.TRAY_ID + "' and CELL_ID='" + model.CELL_ID + "'");
            ClsLogs.SqllogNet.WriteDebug("Update()", "更新一条数据:" + strSql.ToString());

            SQLServerHelp msh = new SQLServerHelp(this.connStr);
            int rowsAffected = msh.ExecuteSql(strSql.ToString());

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
        /// 得到一个对象实体
        /// </summary>
        /// <param name="OCVType">OCV类型</param>
        /// <param name="DeviceCode">托盘号</param>
        /// <param name="OCVtable">表</param>
        /// <returns></returns>
        public List<Model.BatInfo_Model> GetData(int OCVType, string DeviceCode, string OCVtable, string TestEndTime)
        {
           
            List<Model.BatInfo_Model> lstOCVInfo = new List<Model.BatInfo_Model>();
            StringBuilder strSql = new StringBuilder();
           
            try
            {
                SQLServerHelp msh = new SQLServerHelp(this.connStr);
                strSql.Append("select *");
                strSql.Append(" FROM " + OCVtable);
                strSql.Append(" where TRAY_ID = '" + DeviceCode + "' and END_DATE_TIME = '" + TestEndTime + "' order by BATTERY_POS");

                DataTable dt = msh.ExecuteSqlToDatatable(strSql.ToString());
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstOCVInfo.Add(DataRowToModel(dt.Rows[i]));
                    }
                }

                return lstOCVInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DB_OCV.Model.BatInfo_Model DataRowToModel(DataRow row)
        {
            DB_OCV.Model.BatInfo_Model model = new DB_OCV.Model.BatInfo_Model();
            if (row != null)
            {
                if (row["Eqp_ID"] != null)
                {
                    model.Eqp_ID = row["Eqp_ID"].ToString();
                }
                if (row["PC_ID"] != null)
                {
                    model.PC_ID = row["PC_ID"].ToString();
                }
                if (row["OPERATION"] != null)
                {
                    model.OPERATION = row["OPERATION"].ToString();
                }
                if (row["IS_TRANS"] != null && row["IS_TRANS"].ToString() != "")
                {
                    model.IS_TRANS = double.Parse(row["IS_TRANS"].ToString());
                }
                if (row["TRAY_ID"] != null)
                {
                    model.TRAY_ID = row["TRAY_ID"].ToString();
                }
                if (row["CELL_ID"] != null)
                {
                    model.CELL_ID = row["CELL_ID"].ToString();
                }
                if (row["BATTERY_POS"] != null && row["BATTERY_POS"].ToString() != "")
                {
                    model.BATTERY_POS = int.Parse(row["BATTERY_POS"].ToString());
                }
                if (row["MODEL_NO"] != null)
                {
                    model.MODEL_NO = row["MODEL_NO"].ToString();
                }
                if (row["BATCH_NO"] != null)
                {
                    model.BATCH_NO = row["BATCH_NO"].ToString();
                }
                if (row["OCV_VOLTAGE"] != null && row["OCV_VOLTAGE"].ToString() != "")
                {
                    model.OCV_VOLTAGE = double.Parse(row["OCV_VOLTAGE"].ToString());
                }
                if (row["ACIR"] != null && row["ACIR"].ToString() != "")
                {
                    model.ACIR = double.Parse(row["ACIR"].ToString());
                }
                if (row["TEST_NG_CODE"] != null)
                {
                    model.TEST_NG_CODE = row["TEST_NG_CODE"].ToString();
                }
                if (row["TEST_RESULT"] != null)
                {
                    model.TEST_RESULT = row["TEST_RESULT"].ToString();
                }
                if (row["TEST_RESULT_DESC"] != null)
                {
                    model.TEST_RESULT_DESC = row["TEST_RESULT_DESC"].ToString();
                }
                if (row["SHELL_VOLTAGE"] != null && row["SHELL_VOLTAGE"].ToString() != "")
                {
                    model.SHELL_VOLTAGE = double.Parse(row["SHELL_VOLTAGE"].ToString());
                }
                if (row["SV_NG_CODE"] != null)
                {
                    model.SV_NG_CODE = row["SV_NG_CODE"].ToString();
                }
                if (row["SV_RESULT"] != null)
                {
                    model.SV_RESULT = row["SV_RESULT"].ToString();
                }
                if (row["SV_RESULT_DESC"] != null)
                {
                    model.SV_RESULT_DESC = row["SV_RESULT_DESC"].ToString();
                }
                if (row["POSTIVE_TEMP"] != null && row["POSTIVE_TEMP"].ToString() != "")
                {
                    model.POSTIVE_TEMP = double.Parse(row["POSTIVE_TEMP"].ToString());
                }
                if (row["NEGATIVE_TEMP"] != null && row["NEGATIVE_TEMP"].ToString() != "")
                {
                    model.NEGATIVE_TEMP = double.Parse(row["NEGATIVE_TEMP"].ToString());
                }
                if (row["K"] != null && row["K"].ToString() != "")
                {
                    model.K = double.Parse(row["K"].ToString());
                }
                if (row["V_DROP"] != null && row["V_DROP"].ToString() != "")
                {
                    model.V_DROP = double.Parse(row["V_DROP"].ToString());
                }
                if (row["V_DROP_RANGE"] != null && row["V_DROP_RANGE"].ToString() != "")
                {
                    model.V_DROP_RANGE = double.Parse(row["V_DROP_RANGE"].ToString());
                }
                if (row["V_DROP_RANGE_CODE"] != null)
                {
                    model.V_DROP_RANGE_CODE = row["V_DROP_RANGE_CODE"].ToString();
                }
                if (row["V_DROP_RESULT"] != null)
                {
                    model.V_DROP_RESULT = row["V_DROP_RESULT"].ToString();
                }
                if (row["V_DROP_RESULT_DESC"] != null)
                {
                    model.V_DROP_RESULT_DESC = row["V_DROP_RESULT_DESC"].ToString();
                }
                if (row["ACIR_RANGE"] != null && row["ACIR_RANGE"].ToString() != "")
                {
                    model.ACIR_RANGE = double.Parse(row["ACIR_RANGE"].ToString());
                }
                if (row["R_RANGE_NG_CODE"] != null)
                {
                    model.R_RANGE_NG_CODE = row["R_RANGE_NG_CODE"].ToString();
                }
                if (row["R_RANGE_RESULT"] != null)
                {
                    model.R_RANGE_RESULT = row["R_RANGE_RESULT"].ToString();
                }
                if (row["R_RANGE_RESULT_DESC"] != null)
                {
                    model.R_RANGE_RESULT_DESC = row["R_RANGE_RESULT_DESC"].ToString();
                }
                if (row["REV_OCV"] != null && row["REV_OCV"].ToString() != "")
                {
                    model.REV_OCV = double.Parse(row["REV_OCV"].ToString());
                }
                if (row["CAPACITY"] != null && row["CAPACITY"].ToString() != "")
                {
                    model.CAPACITY = double.Parse(row["CAPACITY"].ToString());
                }
                if (row["END_DATE_TIME"] != null)
                {
                    model.END_DATE_TIME = row["END_DATE_TIME"].ToString();
                }
            }
            return model;
        }

        #endregion  Method

        #region  MethodEx

        /// <summary>
        /// 得到最近登录的TrayList
        /// </summary>
        public int GetModel_LatestTime(string DeviceCode, string tableName, out string TestEndTime)
        {
            SQLServerHelp msh = new SQLServerHelp(this.connStr);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  END_DATE_TIME  ");
            strSql.Append(" from " + tableName);
            strSql.Append(" where TRAY_ID='" + DeviceCode + "' order by END_DATE_TIME desc ");
            DataTable dt = msh.ExecuteSqlToDatatable(strSql.ToString());

            if (dt.Rows.Count > 0)
            {
                TestEndTime = dt.Rows[0]["END_DATE_TIME"].ToString();

                return 0;
            }
            else
            {
                TestEndTime = "";
                return 1;
            }
        }

     
        /// <summary>
        /// 获取工艺参数获得数据列表
        /// </summary>
        public DataTable GetProcessList(string strOCV_type, string strBatteryType)
        {
            SQLServerHelp msh = new SQLServerHelp(this.connStr);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM ProcessInfo ");
            strSql.Append(" where OCV_type='" + strOCV_type.Trim() + "'and BatteryType='" + strBatteryType.Trim() + "'");
            DataTable dt = msh.ExecuteSqlToDatatable(strSql.ToString());
            return dt;
        }
        /// <summary>
        /// 获取工艺参数的实体对象
        /// </summary>
        public ProcessInfo DataRowToProModel(DataRow row)
        {
            ProcessInfo model = new ProcessInfo();
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
                if (row["UpLmt_SV"] != null && row["UpLmt_SV"].ToString() != "")
                {
                    model.UpLmt_SV = decimal.Parse(row["UpLmt_SV"].ToString());
                }
                if (row["DownLmt_SV"] != null && row["DownLmt_SV"].ToString() != "")
                {
                    model.DownLmt_SV = decimal.Parse(row["DownLmt_SV"].ToString());
                }
                if (row["UpLmt_ACIR"] != null && row["UpLmt_ACIR"].ToString() != "")
                {
                    model.UpLmt_ACIR = decimal.Parse(row["UpLmt_ACIR"].ToString());
                }
                if (row["DownLmt_ACIR"] != null && row["DownLmt_ACIR"].ToString() != "")
                {
                    model.DownLmt_ACIR = decimal.Parse(row["DownLmt_ACIR"].ToString());
                }

                if (row["UpLMT_ACIRRange"] != null && row["UpLMT_ACIRRange"].ToString() != "")
                {
                    model.UpLMT_ACIRRange = decimal.Parse(row["UpLMT_ACIRRange"].ToString());
                }
                if (row["DownLMT_ACIRRange"] != null && row["DownLMT_ACIRRange"].ToString() != "")
                {
                    model.DownLMT_ACIRRange = decimal.Parse(row["DownLMT_ACIRRange"].ToString());
                }
                if (row["IS_Enable_ACIRRange"] != null && row["IS_Enable_ACIRRange"].ToString() != "")
                {
                    model.IS_Enable_ACIRRange = row["IS_Enable_ACIRRange"].ToString();
                }

                if (row["ACIR_MinOrMedian"] != null && row["ACIR_MinOrMedian"].ToString() != "")
                {
                    model.ACIR_MinOrMedian = row["ACIR_MinOrMedian"].ToString();
                }

                if (row["MaxVoltDrop"] != null && row["MaxVoltDrop"].ToString() != "")
                {
                    model.MaxVoltDrop = decimal.Parse(row["MaxVoltDrop"].ToString());
                }
                if (row["VoltDrop"] != null && row["VoltDrop"].ToString() != "")
                {
                    model.VoltDrop = decimal.Parse(row["VoltDrop"].ToString());
                }

                if (row["MinVoltDrop"] != null && row["MinVoltDrop"].ToString() != "")
                {
                    model.MinVoltDrop = decimal.Parse(row["MinVoltDrop"].ToString());
                }

                if (row["ENVoltDrop"] != null && row["ENVoltDrop"].ToString() != "")
                {
                    model.ENVoltDrop = row["ENVoltDrop"].ToString();
                }
                if (row["Drop_MinOrMedian"] != null && row["Drop_MinOrMedian"].ToString() != "")
                {
                    model.Drop_MinOrMedian = row["Drop_MinOrMedian"].ToString();
                }

                if (row["IS_Enable_DropRange"] != null && row["IS_Enable_DropRange"].ToString() != "")
                {
                    model.IS_Enable_DropRange = row["IS_Enable_DropRange"].ToString();
                }
                if (row["DownLMT_DropRange"] != null && row["DownLMT_DropRange"].ToString() != "")
                {
                    model.DownLMT_DropRange = decimal.Parse(row["DownLMT_DropRange"].ToString());
                }
                if (row["UpLMT_DropRange"] != null && row["UpLMT_DropRange"].ToString() != "")
                {
                    model.UpLMT_DropRange = decimal.Parse(row["UpLMT_DropRange"].ToString());
                }

                //if (row["UPLmt_Time"] != null && row["UPLmt_Time"].ToString() != "")
                //{
                //    model.UPLmt_Time = decimal.Parse(row["UPLmt_Time"].ToString());
                //}
                //if (row["DownLmt_Time"] != null && row["DownLmt_Time"].ToString() != "")
                //{
                //    model.DownLmt_Time = decimal.Parse(row["DownLmt_Time"].ToString());
                //}
                //if (row["K"] != null && row["K"].ToString() != "")
                //{
                //    model.K = decimal.Parse(row["K"].ToString());
                //}
                if (row["TempBase"] != null && row["TempBase"].ToString() != "")
                {
                    model.TempBase = decimal.Parse(row["TempBase"].ToString());
                }
                if (row["TempPara"] != null && row["TempPara"].ToString() != "")
                {
                    model.TempPara = decimal.Parse(row["TempPara"].ToString());
                }
                //if (row["ISOLATION"] != null)
                //{
                //    model.ISOLATION = row["ISOLATION"].ToString();
                //}
                //if (row["L_Dispiacement"] != null && row["L_Dispiacement"].ToString() != "")
                //{
                //    model.L_Dispiacement = decimal.Parse(row["L_Dispiacement"].ToString());
                //}
                //if (row["R_Dispiacement"] != null && row["R_Dispiacement"].ToString() != "")
                //{
                //    model.R_Dispiacement = decimal.Parse(row["R_Dispiacement"].ToString());
                //}
            }
            return model;
        }



        /// <summary>
        /// 获取用户权限
        /// </summary>
        public int GetUesrInfo(string UesrName, string UserPwd)
        {

            try
            {
                SQLServerHelp msh = new SQLServerHelp(this.connStr);
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
                DataTable dt = msh.ExecuteSqlToDatatable(strSql.ToString());
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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsUesr(string UesrName)
        {
            SQLServerHelp msh = new SQLServerHelp(this.connStr);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where UserName='" + UesrName + "' ");
            DataTable dt = msh.ExecuteSqlToDatatable(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                int count = int.Parse(dt.Rows[0][0].ToString());
                if (count>0)
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

        #endregion  MethodEx
    }
}

