using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OCV
{
    public class SQLServerHelp
    {
        #region ///变量定义

        private string mSVS;
        public string SVS
        {
            get
            {
                return mSVS;
            }
            set
            {
                mSVS = value;
            }
        }

        private string mDB;
        public string DB
        {
            get
            {
                return mDB;
            }
            set
            {
                mDB = value;
            }
        }

        private string mUSER;
        public string USER
        {
            get
            {
                return mUSER;
            }
            set
            {
                mUSER = value;
            }
        }

        private string mPWD;
        public string PWD
        {
            get
            {
                return mPWD;
            }
            set
            {
                mPWD = value;
            }
        }

        private string mConnStr;
        public string ConnStr
        {
            get
            {
                return mConnStr;
            }
            set
            {
                mConnStr = value;
            }
        }

        public static string ErrMsg = "";

        #endregion

        #region ///构造函数

        public SQLServerHelp(string strConnect)
        {
            mConnStr = strConnect;
        }

        public SQLServerHelp()
        {
            mSVS = "local";
            mDB = "Kinte";
            mUSER = "sa";
            mPWD = "";

            mConnStr = "Server=" + mSVS + ";Database=" + mDB + ";UID=" + mUSER + ";PWD=" + mPWD + ";Connection Timeout=2";
        }

        public SQLServerHelp(string _SVS, string _DB, string _USER, string _PWD)
        {
            mSVS  = _SVS ;
            mDB  = _DB ;
            mUSER  = _USER;
            mPWD = _PWD;

            mConnStr = "Server=" + mSVS + ";Database=" + mDB + ";UID=" + mUSER + ";PWD=" + mPWD + ";Connection Timeout=2";
        }

        #endregion

        #region ///数据库连接

        /// <summary>
        /// 打开数据库连接并返回连接结果
        /// </summary>
        /// <returns>True：连接成功，False：连接失败</returns>
        public bool Connected()
        {
            try
            {
                using (SqlConnection mConn = new SqlConnection(mConnStr))
                {
                    mConn.Open();
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogRecord(ex.Message);
                
                return false;
            }
        }

        /// <summary>
        /// 打开数据库连接并返回连接结果
        /// </summary>
        /// <param name="mSVS">服务器名</param>
        /// <param name="mDB">数据库名</param>
        /// <param name="mUSER">用户名</param>
        /// <param name="mPWD">密码</param>
        /// <returns>True：连接成功，False：连接失败</returns>
        public bool Connected(string _SVS, string _DB, string _USER, string _PWD)
        {
            try
            {
                mSVS = _SVS;
                mDB = _DB;
                mUSER  = _USER;
                mPWD = _PWD;

                mConnStr = "Server=" + mSVS + ";Database=" + mDB + ";UID=" + mUSER + ";PWD=" + mPWD + ";Connection Timeout=2";

                using (SqlConnection mConn = new SqlConnection(mConnStr))
                {
                    mConn.Open();
                   
                    return true;
                }
            }
            catch(Exception ex)
            {
                LogRecord(ex.Message);
               
                return false;
            }
        }

        /// <summary>
        /// 打开数据库连接并返回连接结果
        /// </summary>
        /// <param name="_ConnStr"></param>
        /// <returns></returns>
        public bool Connected(string _ConnStr)
        {
            try
            {
                mConnStr = _ConnStr;

                using (SqlConnection mConn = new SqlConnection(mConnStr))
                {
                    mConn.Open();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogRecord(ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 打开数据库连接并返回该连接的实例对象
        /// </summary>
        /// <returns>null：连接失败</returns>
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(mConnStr);
                
                mConn.Open();
                
                return mConn;
            }
            catch (Exception ex)
            {
                LogRecord(ex.Message);
                
                return null;
            }
        }

        /// <summary>
        /// 打开数据库连接并返回该连接的实例对象
        /// </summary>
        /// <param name="mSVS">服务器名</param>
        /// <param name="mDB">数据库名</param>
        /// <param name="mUSER">用户名</param>
        /// <param name="mPWD">密码</param>
        /// <returns>null：连接失败</returns>
        public SqlConnection GetConnection(string _SVS, string _DB, string _USER, string _PWD)
        {
            try
            {
                mSVS = _SVS;
                mDB = _DB;
                mUSER  = _USER;
                mPWD = _PWD;

                mConnStr = "Server=" + mSVS + ";Database=" + mDB + ";UID=" + mUSER + ";PWD=" + mPWD + ";Connection Timeout=2";

                SqlConnection mConn = new SqlConnection(mConnStr);                
               
                mConn.Open();
                
                return mConn;
            }
            catch(Exception ex)
            {
                LogRecord(ex.Message);

                return null;
            }
        }

        #endregion

        #region///数据表操作

        public bool IsTableExist(SqlConnection mConn, string tableName)
        {
            bool isTableExist = false;

            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlCommand mCmd = mConn.CreateCommand())
                {
                    mCmd.CommandType = CommandType.Text;
                    mCmd.CommandText = "SELECT COUNT(*) FROM dbo.sysobjects WHERE xtype = 'U' AND name = '" + tableName + "';";

                    if (1 == Convert.ToInt32(mCmd.ExecuteScalar()))
                    {
                        isTableExist = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogRecord("数据表是否存在" + " " + ex.Message);
            }

            return isTableExist;
        }

        public bool CreateTable(SqlConnection mConn, string commandText)
        {
            bool flag = false;

            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlCommand mCmd = mConn.CreateCommand())
                {
                    mCmd.CommandType = CommandType.Text;
                    mCmd.CommandText = commandText;

                    mCmd.ExecuteNonQuery();

                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LogRecord("创建数据表" + " " + ex.Message);
            }

            return flag;
        }

        public bool IsFieldExist(SqlConnection mConn, string tableName, string fieldName, string formatStr, bool flag)
        {
            bool isFieldExist = false;

            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlCommand mCmd = mConn.CreateCommand())
                {
                    mCmd.CommandType = CommandType.Text;
                    mCmd.CommandText = "select count(*) from dbo.syscolumns where id=object_id('" + tableName + "') and name='" + fieldName + "';";

                    //检查表中是否存在指定字段
                    if (1 == Convert.ToInt32(mCmd.ExecuteScalar()))
                    {
                        isFieldExist = true;
                    }

                    //若不存在，则增加该字段
                    if (flag == true && isFieldExist == false)
                    {
                        if (formatStr == "")
                        {
                            LogRecord("数据表中新建字段的类型没有给定！");
                        }

                        mCmd.CommandText = "ALTER TABLE " + tableName + " ADD " + fieldName + " " + formatStr + ";";

                        mCmd.ExecuteNonQuery();

                        isFieldExist = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogRecord("数据表中字段是否存在" + " " + ex.Message);
            }

            return isFieldExist;
        }

        #endregion

        #region ///数据查询执行

        public int ExecuteSql(string commandText)
        {
            int lines = 0;

            try
            {
                using (SqlConnection mConn = new SqlConnection(mConnStr))
                {
                    mConn.Open();
                    SqlCommand mCmd = mConn.CreateCommand();
                    mCmd.CommandType = CommandType.Text;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);
            }
            return lines;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int ExeNonQuery(CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                using (SqlConnection mConn = new SqlConnection("Server=" + mSVS + ";Database=" + mDB + ";UID=" + mUSER + ";PWD=" + mPWD + ";Connection Timeout=2"))
                {
                    mConn.Open();
                    SqlCommand mCmd = mConn.CreateCommand();
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);
            }
            return lines;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="mSVS">服务器名</param>
        /// <param name="mDB">数据库名</param>
        /// <param name="mUSER">用户名</param>
        /// <param name="mPWD">密码</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int ExeNonQuery(string _SVS, string _DB, string _USER, string _PWD, CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                mSVS = _SVS;
                mDB = _DB;
                mUSER = _USER;
                mPWD = _PWD;

                using (SqlConnection mConn = new SqlConnection("Server=" + mSVS + ";Database=" + mDB + ";UID=" + mUSER + ";PWD=" + mPWD + ";Connection Timeout=2"))
                {
                    mConn.Open();

                    SqlCommand mCmd = mConn.CreateCommand();
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="_ConnStr"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExeNonQuery(string _ConnStr, CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                mConnStr = _ConnStr;

                using (SqlConnection mConn = new SqlConnection(mConnStr))
                {
                    mConn.Open();

                    SqlCommand mCmd = mConn.CreateCommand();
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int ExeNonQuery(SqlConnection mConn, CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlCommand mCmd = mConn.CreateCommand())
                {
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// 对连接使用事务执行多条SQL语句
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句数组</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTrans(SqlConnection mConn, CommandType commandType, string[] commandText)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlCommand mCmd = mConn.CreateCommand())
                {
                    SqlTransaction mTrans = mConn.BeginTransaction();

                    mCmd.Transaction = mTrans;
                    mCmd.CommandType = commandType;

                    for (int i = 0; i < commandText.Length; i++)
                    {
                        if (commandText[i] == null)
                        {
                            continue;
                        }  

                        mCmd.CommandText = commandText[i];
                        mCmd.ExecuteNonQuery();
                    }

                    mTrans.Commit();
                    mTrans.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogRecord(ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>DataTable结果集</returns>
        public DataTable ExecuteSqlToDatatable(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(mConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();

                        throw new Exception("Mssql执行sql异常(有结果集):" + ex.Message + "\r\n" + sql);
                    }
                }
            }
        }

        /// <summary>
        /// 对连接执行SQL语句并返回DataSet对象
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>DataSet对象</returns>
        public DataSet ExeNonQueryToDataSet(SqlConnection mConn, string commandText)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlDataAdapter sda = new SqlDataAdapter(commandText, mConn))
                {
                    DataSet mDset = new DataSet();

                    sda.Fill(mDset);

                    return mDset;
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// 对连接执行查询，并返回查询结果集的第一行第一列，忽略其它行和列
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>结果集中第一行的第一列；如果结果集为空，则为空引用（在 Visual Basic 中为 Nothing）。返回的最大字符数为 2033 个字符。</returns>
        public object ExeScalar(SqlConnection mConn, CommandType commandType, string commandText)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SqlCommand mCmd = mConn.CreateCommand())
                {
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    return mCmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                LogRecord(commandText + " " + ex.Message);

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
                //按年月建文件夹，eg:201606 
                string path = Environment.CurrentDirectory + "\\SQLServerLogs\\" + nYear.ToString("0000") + nMonth.ToString("00");
                
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

                ErrMsg = Message;
            }
            catch
            {
            }
        }

        #endregion
    }
}
