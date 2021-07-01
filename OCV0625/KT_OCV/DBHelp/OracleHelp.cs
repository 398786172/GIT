/*
 * Oracle Data Adapter Library
 * 目的：Oracle数据库数据适配类，方便对Oracle数据库进行数据操作
 * 
 * 版本：V1.0
 * 作者：zxz
 * 日期：2019.10.28
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDTek.Oracle;
using System.Data;
using System.Threading;
using System.IO;

namespace OCV
{
    public class OracleHelp
    {
        #region 变量
        private string svrAddr;
        private string userName;
        private string pwd;
        private string connStr;
        OracleConnection oConn;
        #endregion

        #region 构造函数
        public OracleHelp(string addr,string uName,string upwd)
        {
            svrAddr = addr;
            userName = uName;
            pwd = upwd;
            connStr = string.Format("Host={0};Port=1521;Service Name=ORCL;User ID={1};Password={2}", svrAddr, userName, pwd);
            oConn = new OracleConnection(connStr);
        }
        public OracleHelp(string conn)
        {
            connStr = conn;
            oConn = new OracleConnection(connStr);
        }
        #endregion

        #region 数据库连接
        public bool Connected()
        {
            try
            {
                oConn.Open();
                Thread.Sleep(100);
                if (oConn.State == ConnectionState.Open)
                {
                    return true;
                }else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                LogRecord("Oracle Connect Error:" + ex.Message);
                return false;
            }
        }

        public void DisConnected()
        {
            try
            {
                if (oConn.State == ConnectionState.Open)
                {
                    oConn.Close();
                }
            }catch (Exception ex)
            {
                LogRecord("Oracle Connect Error:" + ex.Message);
            }
        }
        #endregion

        #region 数据库操作
        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <returns></returns>
        public int ExeNonQuery( string commandText)
        {
            int lines = -1;

            try
            {
                if (oConn!=null)
                {
                    if (oConn.State!=ConnectionState.Open)
                    {
                        Connected();
                    }
                    if (oConn.State==ConnectionState.Open)
                    {
                        OracleCommand cmd = new OracleCommand(commandText, oConn);
                        lines=cmd.ExecuteNonQuery();
                        oConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQuery error:" + ex.Message);
            }
            return lines;
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="SQLStringList">SQL语句数组</param>
        /// <returns></returns>
        public int ExeNonQueryUseTrans(List<String> SQLStringList)
        {
            int count=0;
            try
            {
                if (oConn != null)
                {
                    if (oConn.State != ConnectionState.Open)
                    {
                        Connected();
                    }
                    if (oConn.State == ConnectionState.Open)
                    {
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = oConn;
                        OracleTransaction tx = oConn.BeginTransaction();
                        cmd.Transaction = tx;
                        foreach (string strSql in SQLStringList)
                        {
                            if (!string.IsNullOrEmpty(strSql))
                            {
                                cmd.CommandText = strSql.ToUpper();
                                count += cmd.ExecuteNonQuery();
                            }
                        }
                        tx.Commit();
                        oConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryUseTrans error:" + " " + ex.Message);
            }
            return count;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回DataSet对象
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>DataSet对象</returns>
        public DataSet ExeNonQueryToDataSet(string commandText)
        {
            try
            {
                if (oConn != null)
                {
                    if (oConn.State != ConnectionState.Open)
                    {
                        Connected();
                    }
                    if (oConn.State == ConnectionState.Open)
                    {
                        OracleDataAdapter tOda = new OracleDataAdapter(commandText, oConn);
                        tOda.SelectCommand.CommandTimeout = 120;
                        DataSet ds = new DataSet();
                        tOda.Fill(ds);
                        return ds;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryToDataSet" + ex.Message);
                return null;
            }
        }
        #endregion

        #region ///日志记录

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="Message">错误内容</param>
        private void LogRecord(string Message)
        {
            int nYear, nMonth, nDay;

            nYear = DateTime.Now.Year;
            nMonth = DateTime.Now.Month;
            nDay = DateTime.Now.Day;

            try
            {
                //建立日志目录
                //string path = Environment.CurrentDirectory + "\\SQLiteLogs\\" + nYear.ToString("0000") + nMonth.ToString("00");
                string path = @"\\OracleLogs";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //按年月建文件夹，eg:201606 
                path = path + nYear.ToString("0000") + nMonth.ToString("00");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //按日期建文件
                path += "\\" + nDay.ToString("00") + ".log";

                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    string tmpStr = DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString("000") + " " + Message;
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                    sw.WriteLine(tmpStr);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
