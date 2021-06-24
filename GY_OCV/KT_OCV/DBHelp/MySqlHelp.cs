using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace OCV
{
    //Mysql基础操作类
    public class MySqlHelp
    {
        //连接字符串
        private string connectString = "Database='wp';Data Source='localhost';User Id='root';Password='123456';charset='utf8';pooling=true";

        public MySqlHelp()
        {
            
        }

        public MySqlHelp(string connectString)
        {
            this.connectString = connectString;
        }

        public bool Connect()
        {
            bool returnFlag = false;

            try
            {
                MySqlConnection conn = new MySqlConnection(this.connectString);

                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    returnFlag = true;
                }
            }
            catch
            {

            }

            return returnFlag;
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(this.connectString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        int ret = cmd.ExecuteNonQuery();
                        return ret;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        connection.Close();
                        throw new Exception("Mssql执行sql异常:" + ex.Message+"\r\n"+sql);
                    }
                }
            }
        }

        /// <summary>
        /// 对连接执行查询，并返回查询结果集的第一行第一列，忽略其它行和列
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>结果集中第一行的第一列；如果结果集为空，则为空引用（在VB中为Nothing）。返回的最大字符数为 2033 个字符。</returns>
        public object ExecuteToScalar(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(this.connectString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();

                        return cmd.ExecuteScalar();
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
        /// 执行SQL语句，返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>DataTable结果集</returns>
        public DataTable ExecuteSqlToDatatable(string sql)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(this.connectString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
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
        /// 执行多条SQL语句，实现数据库事务
        /// </summary>
        /// <param name="sqlList">多条SQL语句</param>        
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlTransaction(List<String> sqlList)
        {
            int affectedRows = 0;

            using (MySqlConnection connection = new MySqlConnection(this.connectString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            for (int i = 0; i < sqlList.Count; i++)
                            {
                                cmd.CommandText = sqlList[i];
                                affectedRows += cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        return affectedRows; //返回受影响的行数
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); //事务回滚

                        string sqlString = "";

                        foreach (var s in sqlList)
                        {
                            sqlString += s;
                        }

                        throw new Exception("执行事务异常:" + ex.Message +"\r\n"+ sqlString);
                    }
                }
            }
        }
    }
}
