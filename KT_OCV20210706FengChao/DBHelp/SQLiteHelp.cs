using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Collections.Concurrent;

namespace OCV
{
    public class SQLiteHelp
    {
        #region ///变量定义

        //private SQLiteConnection mConn;

        //请勿直接绕过set为mDataSource赋值
        private string mDataSource;
        public string DataSource
        {
            get { return mDataSource; }
            set
            {
                sqliteFileLockers.TryAdd(value, new object());
                mDataSource = value;
            }
        }
        //SQLite写入存在库锁，需要同步
        internal static readonly ConcurrentDictionary<string, object> sqliteFileLockers = new ConcurrentDictionary<string, object>();

        private string mPassWord;
        public string PassWord
        {
            get
            {
                return mPassWord;
            }
            set
            {
                mPassWord = value;
            }
        }

        public static string ErrMsg = "";


        #endregion

        #region ///构造函数

        public SQLiteHelp()
        {
            DataSource = Environment.CurrentDirectory + "\\SDALTest.db";
            mPassWord = "";
        }

        public SQLiteHelp(string dataSource)
        {
            DataSource = dataSource;
            mPassWord = "";
        }

        public SQLiteHelp(string dataSource, string passWord)
        {
            DataSource = dataSource;
            mPassWord = passWord;
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
                using (var mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;

                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogRecord("Connected" + " " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 打开数据库连接并返回连接结果
        /// </summary>
        /// <param name="DataSource">DB文件路径</param>
        /// <returns>True：连接成功，False：连接失败</returns>
        public bool Connected(string dataSource)
        {
            DataSource = dataSource;

            return this.Connected();
        }

        /// <summary>
        /// 打开数据库连接并返回连接结果
        /// </summary>
        /// <param name="DataSource">DB文件路径</param>
        /// <param name="mPassWord">可选参数：DB文件密码</param>
        /// <returns>True：连接成功，False：连接失败</returns>
        public bool Connected(string dataSource, string passWord = "")
        {
            DataSource = dataSource;
            mPassWord = passWord;

            return this.Connected();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void DisConnected()
        {
            //全部迁移到自动释放连接

            //if (this.mConn != null)
            //{
            //    if (this.mConn.State != ConnectionState.Closed)
            //    {
            //        this.mConn.Close();
            //    } 
            //}
        }

        /// <summary>
        /// 打开数据库连接并返回该连接的实例对象
        /// </summary>
        /// <returns>null：连接失败</returns>
        public SQLiteConnection GetConnection()
        {
            try
            {
                SQLiteConnection mConn = new SQLiteConnection();
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                sb.DataSource = DataSource;
                sb.Password = mPassWord;
                sb.Version = 3;
                sb.DefaultTimeout = 3;

                mConn.ConnectionString = sb.ToString();
                mConn.Open();

                return mConn;
            }
            catch (Exception ex)
            {
                LogRecord("GetConnection1" + " " + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// 打开数据库连接并返回该连接的实例对象
        /// </summary>
        /// <param name="DataSource">DB文件路径</param>
        /// <param name="mPassWord">可选参数：DB文件密码</param>
        /// <returns>null：连接失败</returns>
        public SQLiteConnection GetConnection(string dataSource, string passWord = "")
        {
            try
            {
                DataSource = dataSource;
                mPassWord = passWord;

                SQLiteConnection mConn = new SQLiteConnection();
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                sb.DataSource = DataSource;
                sb.Password = mPassWord;
                sb.Version = 3;
                sb.DefaultTimeout = 3;

                mConn.ConnectionString = sb.ToString();
                mConn.Open();

                return mConn;
            }
            catch (Exception ex)
            {
                LogRecord("GetConnection2" + " " + ex.Message);

                return null;
            }
        }

        #endregion

        #region///数据表操作

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool IsTableExist(string tableName)
        {
            bool isTableExist = false;

            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;

                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();

                    mCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "';";

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

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        public bool CreateTable(string commandText)
        {
            bool flag = false;

            try
            {
                lock (sqliteFileLockers[mDataSource])
                {
                    using (SQLiteConnection mConn = new SQLiteConnection())
                    {
                        SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                        sb.DataSource = DataSource;
                        sb.Password = mPassWord;

                        sb.Version = 3;
                        sb.DefaultTimeout = 3;

                        mConn.ConnectionString = sb.ToString();
                        mConn.Open();

                        SQLiteCommand mCmd = mConn.CreateCommand();

                        mCmd.CommandText = commandText;
                        mCmd.ExecuteNonQuery();

                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogRecord("创建数据表" + " " + ex.Message);
            }

            return flag;
        }

        #endregion

        #region ///数据查询执行

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns></returns>
        public int ExeNonQuery(CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;

                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();

                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQuery1" + " " + commandText + " " + ex.Message);
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
        public int ExeNonQuery(SQLiteConnection mConn, CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SQLiteCommand mCmd = mConn.CreateCommand())
                {
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQuery2" + " " + commandText + " " + ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="mPassWord">可选参数：DB文件密码</param>
        /// <returns></returns>
        public int ExeNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            int lines = 0;

            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    mConn.ConnectionString = connectionString;
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQuery3" + " " + commandText + " " + ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="DataSource">DB文件路径</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="mPassWord">可选参数：DB文件密码</param>
        /// <returns></returns>
        public int ExeNonQuery(string DataSource, CommandType commandType, string commandText, string mPassWord = "")
        {
            int lines = 0;

            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;
                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();
                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;
                    lines = mCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQuery4" + " " + commandText + " " + ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句数组</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTrans(CommandType commandType, string[] commandText)
        {
            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;

                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();
                    SQLiteTransaction mTrans = mConn.BeginTransaction();

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
                LogRecord("ExeNonQueryUseTrans1" + " " + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句数组</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTrans(SQLiteConnection mConn, CommandType commandType, string[] commandText)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SQLiteCommand mCmd = mConn.CreateCommand())
                {
                    SQLiteTransaction mTrans = mConn.BeginTransaction();

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

                mConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryUseTrans2" + " " + ex.Message);

                mConn.Close();

                return false;
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTextList">SQL语句列表</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTrans(SQLiteConnection mConn, CommandType commandType, List<string> commandTextList)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SQLiteCommand mCmd = mConn.CreateCommand())
                {
                    SQLiteTransaction mTrans = mConn.BeginTransaction();

                    mCmd.Transaction = mTrans;
                    mCmd.CommandType = commandType;

                    foreach (string commandText in commandTextList)
                    {
                        if (commandText == null)
                        {
                            continue;
                        }

                        mCmd.CommandText = commandText;
                        mCmd.ExecuteNonQuery();
                    }

                    mTrans.Commit();
                    mTrans.Dispose();
                }

                mConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryUseTrans3" + " " + ex.Message);

                mConn.Close();

                return false;
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTextList">SQL语句列表</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTrans(string connectionString, CommandType commandType, List<string> commandTextList)
        {
            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    mConn.ConnectionString = connectionString;
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();
                    SQLiteTransaction mTrans = mConn.BeginTransaction();

                    mCmd.Transaction = mTrans;
                    mCmd.CommandType = commandType;

                    foreach (string commandText in commandTextList)
                    {
                        if (commandText == null)
                        {
                            continue;
                        }

                        mCmd.CommandText = commandText;
                        mCmd.ExecuteNonQuery();
                    }

                    mTrans.Commit();
                    mTrans.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryUseTrans4" + " " + ex.Message);

                return false;
            }
        }


        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns></returns>
        public int ExeNonQueryWithLocker(CommandType commandType, string commandText)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQuery(commandType, commandText);
            }
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int ExeNonQueryWithLocker(SQLiteConnection mConn, CommandType commandType, string commandText)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQuery(mConn, commandType, commandText);
            }
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="mPassWord">可选参数：DB文件密码</param>
        /// <returns></returns>
        public int ExeNonQueryWithLocker(string connectionString, CommandType commandType, string commandText)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQuery(connectionString, commandType, commandText);
            }
        }

        /// <summary>
        /// 对连接执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="DataSource">DB文件路径</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="mPassWord">可选参数：DB文件密码</param>
        /// <returns></returns>
        public int ExeNonQueryWithLocker(string DataSource, CommandType commandType, string commandText, string mPassWord = "")
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQuery(DataSource, commandType, commandText, mPassWord);
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句数组</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTransWithLocker(CommandType commandType, string[] commandText)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQueryUseTrans(commandType, commandText);
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句数组</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTransWithLocker(SQLiteConnection mConn, CommandType commandType, string[] commandText)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQueryUseTrans(mConn, commandType, commandText);
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTextList">SQL语句列表</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTransWithLocker(SQLiteConnection mConn, CommandType commandType, List<string> commandTextList)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQueryUseTrans(mConn, commandType, commandTextList);
            }
        }

        /// <summary>
        /// 对连接通过事务执行多条SQL语句
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTextList">SQL语句列表</param>
        /// <returns>True：SQL语句正常执行完成，False：SQL语句执行过程中出错</returns>
        public bool ExeNonQueryUseTransWithLocker(string connectionString, CommandType commandType, List<string> commandTextList)
        {
            lock (sqliteFileLockers[mDataSource])
            {
                return ExeNonQueryUseTrans(connectionString, commandType, commandTextList);
            }
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
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;

                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    SQLiteDataAdapter sda = new SQLiteDataAdapter(commandText, mConn);

                    DataSet mDset = new DataSet();

                    sda.Fill(mDset);

                    return mDset;
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryToDataSet1" + " " + commandText + " " + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// 对连接执行SQL语句并返回DataSet对象
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>DataSet对象</returns>
        public DataSet ExeNonQueryToDataSet(SQLiteConnection mConn, string commandText)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SQLiteDataAdapter sda = new SQLiteDataAdapter(commandText, mConn))
                {
                    DataSet mDset = new DataSet();

                    sda.Fill(mDset);

                    return mDset;
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeNonQueryToDataSet2" + " " + commandText + " " + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// 对连接执行查询，并返回查询结果集的第一行第一列，忽略其它行和列
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>结果集中第一行的第一列；如果结果集为空，则为空引用（在VB中为Nothing）。返回的最大字符数为 2033 个字符。</returns>
        public object ExeScalar(CommandType commandType, string commandText)
        {
            try
            {
                using (SQLiteConnection mConn = new SQLiteConnection())
                {
                    SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();

                    sb.DataSource = DataSource;
                    sb.Password = mPassWord;

                    sb.Version = 3;
                    sb.DefaultTimeout = 3;

                    mConn.ConnectionString = sb.ToString();
                    mConn.Open();

                    SQLiteCommand mCmd = mConn.CreateCommand();

                    mCmd.CommandType = commandType;
                    mCmd.CommandText = commandText;

                    return mCmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeScalar1" + " " + commandText + " " + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// 对连接执行查询，并返回查询结果集的第一行第一列，忽略其它行和列
        /// </summary>
        /// <param name="mConn">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句</param>
        /// <returns>结果集中第一行的第一列；如果结果集为空，则为空引用（在VB中为Nothing）。返回的最大字符数为 2033 个字符。</returns>
        public object ExeScalar(SQLiteConnection mConn, CommandType commandType, string commandText)
        {
            try
            {
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                using (SQLiteCommand mComm = mConn.CreateCommand())
                {
                    mComm.CommandType = commandType;
                    mComm.CommandText = commandText;

                    return mComm.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                LogRecord("ExeScalar2" + " " + commandText + " " + ex.Message);

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
            //每次调用DateTime.Now，都会得到新值，此处防止跨月时，月份为上月，日为01（虽然概率极低）
            var now = DateTime.Now;
            int nYear = now.Year, nMonth = now.Month, nDay = now.Day;

            try
            {
                //按年\月建文件夹，eg:2016\06 
                string path = Environment.CurrentDirectory + "\\SQLiteLogs\\" + nYear.ToString("0000") + "\\" + nMonth.ToString("00");
                //string path = @"\\SQLiteLogs\\" + nYear.ToString("0000") + "\\" + nMonth.ToString("00");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //按日期建文件
                path += "\\" + nDay.ToString("00") + ".log";

                using (FileStream fs = new FileStream(path, FileMode.Append))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    string tmpStr = now.ToLongTimeString() + "." + now.Millisecond.ToString("000") + " " + Message;
                    sw.WriteLine(tmpStr);
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
