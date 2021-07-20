
using System;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace OCV
{
    /// <summary>
    /// SQLite 操作类
    /// </summary>
    public class SqLiteHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        public SQLiteConnection dbConnection;
        /// <summary>
        /// SQL命令定义
        /// </summary>
        private SQLiteCommand dbCommand;
        /// <summary>
        /// 数据读取定义
        /// </summary>
        private SQLiteDataReader dataReader;
        /// <summary>
        /// 数据库连接字符串定义
        /// </summary>
        private SQLiteConnectionStringBuilder dbConnectionstr;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接SQLite库字符串</param>
        public SqLiteHelper(string connectionString)
        {
            try
            {
                dbConnection = new SQLiteConnection();
                dbConnectionstr = new SQLiteConnectionStringBuilder();
                //dbConnectionstr.DataSource = connectionString;
                //dbConnectionstr.Password = "admin";      //设置密码，SQLite ADO.NET实现了数据库密码保护
                //dbConnection.ConnectionString = dbConnectionstr.ToString();
                dbConnection.ConnectionString = @"Data Source=" + connectionString + ".db3;Initial Catalog=DCIR_GD_Local;Integrated Security=True;Max Pool Size=10";
                dbConnection.Open();
            }
            catch (Exception e)
            {
                Log(e.ToString());
            }
        }



        /// <summary>
        /// Shortcut to ExecuteNonQuery with SqlStatement and object[] param values
        /// </summary>
        /// <param name="connectionString">SQLite Connection String</param>
        /// <param name="commandText">Sql Statement with embedded "@param" style parameters</param>
        /// <param name="paramList">object[] array of parameter values</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, params object[] paramList)
        {

            using (SQLiteCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = commandText;
                AttachParameters(cmd, commandText, paramList);
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
        }


        /// <summary>
        /// Parses parameter names from SQL Statement, assigns values from object array ,   /// and returns fully populated ParameterCollection.
        /// </summary>
        /// <param name="commandText">Sql Statement with "@param" style embedded parameters</param>
        /// <param name="paramList">object[] array of parameter values</param>
        /// <returns>SQLiteParameterCollection</returns>
        /// <remarks>Status experimental. Regex appears to be handling most issues. Note that parameter object array must be in same ///order as parameter names appear in SQL statement.</remarks>
        private SQLiteParameterCollection AttachParameters(SQLiteCommand cmd, string commandText, params object[] paramList)
        {
            if (paramList == null || paramList.Length == 0) return null;

            SQLiteParameterCollection coll = cmd.Parameters;
            string parmString = commandText.Substring(commandText.IndexOf("@"));
            // pre-process the string so always at least 1 space after a comma.
            parmString = parmString.Replace(",", " ,");
            // get the named parameters into a match collection
            string pattern = @"(@)\S*(.*?)\b";
            Regex ex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mc = ex.Matches(parmString);
            string[] paramNames = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                paramNames[i] = m.Value;
                i++;
            }

            // now let's type the parameters
            int j = 0;
            Type t = null;
            foreach (object o in paramList)
            {
                t = o.GetType();

                SQLiteParameter parm = new SQLiteParameter();
                switch (t.ToString())
                {

                    case ("DBNull"):
                    case ("Char"):
                    case ("SByte"):
                    case ("UInt16"):
                    case ("UInt32"):
                    case ("UInt64"):
                        throw new SystemException("Invalid data type");


                    case ("System.String"):
                        parm.DbType = DbType.String;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Byte[]"):
                        parm.DbType = DbType.Binary;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (byte[])paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Int32"):
                        parm.DbType = DbType.Int32;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (int)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.Boolean"):
                        parm.DbType = DbType.Boolean;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (bool)paramList[j];
                        coll.Add(parm);
                        break;

                    case ("System.DateTime"):
                        parm.DbType = DbType.DateTime;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDateTime(paramList[j]);
                        coll.Add(parm);
                        break;

                    case ("System.Double"):
                        parm.DbType = DbType.Double;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDouble(paramList[j]);
                        coll.Add(parm);
                        break;

                    case ("System.Decimal"):
                        parm.DbType = DbType.Decimal;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDecimal(paramList[j]);
                        break;

                    case ("System.Guid"):
                        parm.DbType = DbType.Guid;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (System.Guid)(paramList[j]);
                        break;

                    case ("System.Object"):

                        parm.DbType = DbType.Object;
                        parm.ParameterName = paramNames[j];
                        parm.Value = paramList[j];
                        coll.Add(parm);
                        break;

                    default:
                        throw new SystemException("Value is of unknown data type");

                } // end switch

                j++;
            }
            return coll;
        }


        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        public SQLiteDataReader ExecuteQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;       //设置SQL语句
                dataReader = dbCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                //  Log(e.Message);
                dataReader = null;
            }
            return dataReader;
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            //销毁Command
            if (dbCommand != null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;
            //销毁Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;
            //销毁Connection
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;
        }
        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;  //获取所有可用的字段
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 读取用户数据
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public SQLiteDataReader ReadDataTable(string tableName, string values)
        {
            string queryString = "SELECT * FROM " + tableName + " where UserName='" + values + "'";  //获取所有可用的字段
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>   
        /// <summary>
        /// 向指定数据表中插入数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的数值</param>
        public SQLiteDataReader InsertValues(string tableName, string[] values)
        {
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount;
            //当插入的数据长度不等于字段数目时引发异常
            if (values.Length != fieldCount)
            {
                throw new SQLiteException("values.Length!=fieldCount");
            }
            string queryString = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; i++)
            {
                queryString += ", " + "'" + values[i] + "'";
            }
            queryString += " )";
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...，默认“=”</param>
        public SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation)
        {
            // operation="=";  //默认
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key + operation + "'" + value + "'";
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...，默认“=”</param>
        public SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key1, string value1, string operation, string key2, string value2)
        {
            // operation="=";  //默认
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            //表中已经设置成int类型的不需要再次添加‘单引号’，而字符串类型的数据需要进行添加‘单引号’
            queryString += " WHERE " + key1 + operation + "'" + value1 + "'" + "OR " + key2 + operation + "'" + value2 + "'";
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public SQLiteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[0] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public SQLiteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 创建数据表
        /// </summary> +
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        public SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 插入字段
        /// </summary> +
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        public SQLiteDataReader Alter(string tableName, string[] colNames, string[] colTypes)
        {
            string queryString = "alter TABLE " + tableName + " add " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// Reads the table.
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">Table name.</param>
        /// <param name="items">Items.</param>
        /// <param name="colNames">Col names.</param>
        /// <param name="operations">Operations.</param>
        /// <param name="colValues">Col values.</param>
        public SQLiteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
        {
            string queryString = "SELECT " + items[0];
            for (int i = 1; i < items.Length; i++)
            {
                queryString += ", " + items[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
            }
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 本类log
        /// </summary>
        /// <param name="s"></param>
        static void Log(string s)
        {
            Console.WriteLine("class SqLiteHelper:::" + s);
        }
        public DataSet ConvertDataReaderToDataSet(string tableName)
        {
            DataSet dataSet = new DataSet();
            SQLiteDataReader reader = ReadFullTable(tableName);
            do
            {
                // Create new data table
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();
                if (schemaTable != null)
                {
                    // A query returning records was executed 
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table 
                        string columnName = (string)dataRow["ColumnName"]; //+ " // Add the column definition to the data table 
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dataSet.Tables.Add(dataTable);
                    // Fill the data table we just created
                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned
                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }

        public DataSet ExecuteDataSet(string commandText, object[] paramList)
        {

            using (SQLiteCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = commandText;
                if (paramList != null)
                {
                    AttachParameters(cmd, commandText, paramList);
                }
                DataSet ds = new DataSet();
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
                return ds;
            }
        }

    }
}
