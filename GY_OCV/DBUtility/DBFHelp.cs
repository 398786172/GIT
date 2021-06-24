using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Odbc;

namespace DBUtility
{
    /// <summary>
    /// 作者:张景
    /// 时间:202004
    /// 说明:DBF操作的公共类
    /// </summary>
    public class DbfHelp
    {
        private string connectionString = string.Empty;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dirPath">DBF文件所在目录的路径(绝对路径)</param>
        public DbfHelp(string dirPath)
        {
            //下面这两个驱动有效,均需要DBF文件所在目录的路径
            //this.connectionString = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + dirPath+ ";Exclusive=No;";
            this.connectionString = @"Driver={Microsoft FoxPro VFP Driver (*.dbf)};SourceType=DBF;SourceDB=" + dirPath + ";Exclusive=No;";
        }

        /// <summary>
        /// 执行增删改操作，返回受影响的行数
        /// </summary>
        /// <param name="sql">要执行的增删改的SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql)
        {
            int affectedRows = 0;
            using (OdbcConnection connection = new OdbcConnection(this.connectionString))
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(sql, connection))
                    {
                        affectedRows += command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("DBF执行sql异常:" + ex.Message);
                }
            }
            return affectedRows;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlList">sql集合</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteTransactionNonQuery(List<string> sqlList)
        {
            int affectedRows = 0;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (OdbcTransaction transaction = connection.BeginTransaction())
                {
                    using (OdbcCommand command = new OdbcCommand())
                    {
                        try
                        {
                            command.Connection = connection;
                            command.Transaction = transaction;
                            for (int i = 0; i < sqlList.Count; i++)
                            {
                                command.CommandText = sqlList[i];
                                affectedRows += command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            return affectedRows; //返回受影响的行数
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); //事务回滚
                            throw new Exception("执行事务异常:" + ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一个查询语句，返回一个包含查询结果的DataTable
        /// </summary>
        /// <param name="sql">要执行的查询语句</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (OdbcConnection connection = new OdbcConnection(this.connectionString))
            {
                using (OdbcCommand cmd = new OdbcCommand(sql, connection))
                {
                    try
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DBF数据查询异常:" + ex.Message);
                        //dt = null;
                    }
                }
            }
            return dt;
        }

    }
}
