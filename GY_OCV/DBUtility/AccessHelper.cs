using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.IO;          //包含File
using System.Data;        //包含datatable
using System.Data.OleDb;  //包含OleDbConnection
using System.Windows.Forms;

namespace DBUtility
{
    public class AccessHelper
    {
        public static string myMdbPath;
        public static string[] myTableName = new string[2];
        public static ArrayList[] myMdbHead = new ArrayList[2];

        //普通的节点
        public struct Node
        {
            private string nodeType;
            public string NodeType//表的字段名
            {
                set { nodeType = value; }
                get { return nodeType; }
            }

            private string nodeValue;
            public string NodeValue//具体的值
            {
                set { nodeValue = value; }
                get { return nodeValue; }
            }
        }

        //数据库初始化
        public static bool MDBInit(string mdbPath, string[] tableName, ArrayList[] mdbHead, int tableNum)
        {
            //检测数据库是否存在，若不存在则创建数据库并创建数据表
            if (!File.Exists(mdbPath))
            {
                CreateMDBDataBase(mdbPath);//创建mdb
                for (int i = 0; i < tableNum; i++)
                {
                    CreateMDBTable(mdbPath, tableName[i], mdbHead[i]);//创建数据表
                }
            }
            myMdbPath = mdbPath;
            for (int i = 0; i < tableNum; i++)
            {
                myTableName[i] = tableName[i].Clone().ToString();
            }
            for (int i = 0; i < tableNum; i++)
            {
                myMdbHead[i] = (ArrayList)mdbHead[i].Clone();
            }
            return true;
        }

        //创建mdb
        public static bool CreateMDBDataBase(string mdbPath)
        {
            try
            {
                ADOX.CatalogClass cat = new ADOX.CatalogClass();
                cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";");
                cat = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //新建mdb的表  //mdbHead是一个ArrayList，存储的是table表中的具体列名。
        public static bool CreateMDBTable(string mdbPath, string tableName, ArrayList mdbHead)
        {
            try
            {
                ADOX.CatalogClass cat = new ADOX.CatalogClass();//=========================================//=====================================//
                string sAccessConnection
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath;
                ADODB.Connection cn = new ADODB.Connection();
                cn.Open(sAccessConnection, null, null, -1);
                cat.ActiveConnection = cn;

                //新建一个表
                ADOX.TableClass tbl = new ADOX.TableClass();
                tbl.ParentCatalog = cat;
                tbl.Name = tableName;

                int size = mdbHead.Count;
                for (int i = 0; i < size; i++)
                {
                    //增加一个文本字段
                    ADOX.ColumnClass col2 = new ADOX.ColumnClass();
                    col2.ParentCatalog = cat;
                    col2.Name = mdbHead[i].ToString();//列的名称
                    col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                    tbl.Columns.Append(col2, ADOX.DataTypeEnum.adVarWChar, 500);  //把列加入table
                }
                cat.Tables.Append(tbl);   //这句把表加入数据库(非常重要)
                tbl = null;
                cat = null;
                cn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 读取mdb指定表的全部数据
        public static DataTable ReadAllData(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=";//创建数据库时未设用户及密码，不用提交密码即可操作数据库
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();
                //建立SQL查询
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句
                odCommand.CommandText = "select * from " + tableName;
                //建立读取
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据
                int size = odrReader.FieldCount; //获取当前行中的列数
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i)); //获取每一列的名称，放入列结构中
                    dt.Columns.Add(dc);   //新建的table完成属性列的构建
                }
                while (odrReader.Read())  //逐条记录(也即逐行)地读取数据
                {
                    dr = dt.NewRow();  //一行数据
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString(); //逐列读取每单元格数据放到行结构中
                    }
                    dt.Rows.Add(dr);  //新建的table完成每条记录的存储
                }
                //关闭连接
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        // 读取指定表的若干列的数据
        public static DataTable ReadDataByColumns(string mdbPath, string tableName, string[] columns, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();
                //建立SQL查询
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句
                string strColumn = "";
                //if (columns[0] == null)
                //{
                //    strColumn = " * ";
                //}
                //else
                //{
                for (int i = 0; i < columns.Length; i++)
                {
                    strColumn += columns[i].ToString() + ",";
                }
                strColumn = strColumn.TrimEnd(',');
                //}

                odCommand.CommandText = "select " + strColumn + " from " + tableName;
                //建立读取
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }

                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        //读取指定列数据
        public static DataTable ReadDataByColumnsBound(string mdbPath, string tableName, string[] columns, Node bound, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=haoren";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();
                //建立SQL查询
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句
                string strColumn = "";
                if (columns[0] == null)
                {
                    strColumn = " * ";
                }
                else
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        strColumn += columns[i].ToString() + ",";
                    }
                    strColumn = strColumn.TrimEnd(',');
                }

                odCommand.CommandText = "select " + strColumn + " from " + tableName + " where " + bound.NodeType + " = '" + bound.NodeValue + "' ";
                //建立读取
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }

                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        //==================================================================
        //函数名：    GetTableNameList
        //作者：        LiYang
        //日期：        2015-6-26
        //功能：        获取ACCESS数据库中的表名
        //输入参数：    数据库路径
        //返回值：      List<string>
        //修改记录：
        //==================================================================
        public static List<string> GetTableNameList(string myMdbPath)
        {
            List<string> list = new List<string>();
            try
            {
                OleDbConnection Conn = new OleDbConnection();
                Conn.ConnectionString = "Provider = Microsoft.Jet.OleDb.4.0;Data Source=" + myMdbPath;
                Conn.Open();
                DataTable dt = Conn.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    if (row[3].ToString() == "TABLE")
                        list.Add(row[2].ToString());
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //==================================================================
        //函数名：    GetTableFieldNameList
        //作者：        LiYang
        //日期：        2015-6-26
        //功能：        获取ACCESS数据库指定表名中的字段
        //输入参数：    数据库路径，表名
        //返回值：      List<string>  属性列的名称
        //修改记录：
        //==================================================================
        public static List<string> GetTableFieldNameList(string myMdbPath, string TableName)
        {
            List<string> list = new List<string>();
            try
            {
                OleDbConnection Conn = new OleDbConnection();
                Conn.ConnectionString = "Provider = Microsoft.Jet.OleDb.4.0;Data Source=" + myMdbPath;
                Conn.Open();
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 * FROM [" + TableName + "]";//读取一个表中第一条记录，如select top 5 * from tablename，读取表中前5条记录
                    cmd.Connection = Conn;
                    OleDbDataReader dr = cmd.ExecuteReader(); //得到列头
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        list.Add(dr.GetName(i));//得到属性列名称
                    }
                }
                Conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //==================================================================
        //函数名：    ReadDataByColumnsLike
        //作者：        LiYang
        //日期：        2015-6-26
        //功能：        获取ACCESS数据库指定表名指定列的相似数据到缓存DataTable
        //输入参数：    数据库路径，表名，列名，相似结点
        //返回值：        DataTable
        //修改记录：
        //==================================================================
        public static DataTable ReadDataByColumnsLike(string mdbPath, string tableName, string[] columns, Node Like)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {
                OleDbConnection Conn = new OleDbConnection();
                Conn.ConnectionString = "Provider = Microsoft.Jet.OleDb.4.0;Data Source=" + myMdbPath;
                Conn.Open();
                OleDbCommand odCommand = Conn.CreateCommand();
                string strColumn = "";
                if (columns[0] == null)
                {
                    strColumn = " * ";
                }
                else
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        strColumn += columns[i].ToString() + ",";
                    }
                    strColumn = strColumn.TrimEnd(',');
                }

                odCommand.CommandText = "select " + strColumn + " from " + tableName + " where " + Like.NodeType + " LIKE '" + Like.NodeValue + "' ";
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                
                //查询并显示数据
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }

                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
                    }
                    dt.Rows.Add(dr);
                }

                //关闭连接
                odrReader.Close();
                Conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回某一表的所有字段名
        /// </summary>
        public static string[] GetTableColumn(string database_path, string varTableName)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider = Microsoft.Jet.OleDb.4.0;Data Source=" + database_path;
                conn.Open();
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, varTableName, null });
                int n = dt.Rows.Count;
                string[] strTable = new string[n];
                int m = dt.Columns.IndexOf("COLUMN_NAME");
                for (int i = 0; i < n; i++)
                {
                    DataRow m_DataRow = dt.Rows[i];
                    strTable[i] = m_DataRow.ItemArray.GetValue(m).ToString();
                }
                conn.Close();
                return strTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //添加一个字段
        public static void MakeNode(ref ArrayList arraylist, string nodetype, string nodevalue)
        {
            AccessClass.Node node = new AccessClass.Node();
            node.NodeType = nodetype;
            node.NodeValue = nodevalue;
            arraylist.Add(node);
        }

        //插入数据
        public static bool InsertRow(string mdbPath, string tableName, ArrayList insertArray, ref string errinfo)
        {
            try
            {
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=haoren";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();

                string str_col = "";
                int size_col = insertArray.Count;
                for (int i = 0; i < size_col; i++)
                {
                    Node vipNode = new Node();
                    try
                    {
                        vipNode = (Node)insertArray[i];
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    vipNode = (Node)insertArray[i];
                    str_col += vipNode.NodeType + ",";
                }
                str_col = str_col.TrimEnd(',');


                int size_row = insertArray.Count;
                string str_row = "";
                for (int i = 0; i < size_row; i++)
                {
                    Node vipNode = new Node();
                    vipNode = (Node)insertArray[i];
                    string v = vipNode.NodeValue.ToString();
                    v = DealString(v);
                    if (v == "")
                    {
                        str_row += "null" + ',';
                    }
                    else
                    {
                        str_row += "'" + v + "'" + ',';
                    }
                }
                str_row = str_row.TrimEnd(','); ;

                string sql = "insert into " + tableName + @" (" + str_col + ") values" + @"(" + str_row + ")";
                OleDbCommand odCommand = new OleDbCommand(sql, odcConnection);
                odCommand.ExecuteNonQuery();
                odcConnection.Close();
                return true;
            }
            catch (Exception err)
            {
                errinfo = err.Message;
                return false;
            }
        }

        //
        public static bool UpdateRow(string mdbPath, string tableName, Node keyNode, ArrayList insertArray, ref string errinfo)
        {
            try
            {
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=haoren";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();

                int size = insertArray.Count;
                string str = "";
                for (int i = 0; i < size; i++)
                {
                    Node node = new Node();
                    node = (Node)insertArray[i];
                    string v = node.NodeValue.ToString();
                    v = DealString(v);
                    str += node.NodeType + " = ";
                    if (v == "")
                    {
                        str += "null" + ',';
                    }
                    else
                    {
                        str += "'" + v + "'" + ',';
                    }

                }
                str = str.TrimEnd(',');

                string sql = "update " + tableName + " set " + str +
                    " where " + keyNode.NodeType + " = " + "'" + keyNode.NodeValue + "'";
                OleDbCommand odCommand = new OleDbCommand(sql, odcConnection);
                odCommand.ExecuteNonQuery();
                odcConnection.Close();
                return true;
            }
            catch (Exception err)
            {
                errinfo = err.Message;
                return false;
            }
        }

        //
        public static bool UpdateMDBNode(string mdbPath, string tableName, Node keyNode, Node saveNode, ref string errinfo)
        {
            try
            {
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=haoren";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();

                string sql = @"update " + tableName + " set " + saveNode.NodeType + " = '" + saveNode.NodeValue +
                    "' where " + keyNode.NodeType + " = " + "'" + keyNode.NodeValue + "'";
                OleDbCommand comm = new OleDbCommand(sql, odcConnection);
                comm.ExecuteNonQuery();
                odcConnection.Close();
                return true;
            }
            catch (Exception err)
            {
                errinfo = err.Message;
                return false;
            }
        }

        //删除一条记录
        public static bool DeleteRow(string mdbPath, string tableName, Node keyNode, ref string errinfo)
        {
            try
            {
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=haoren";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();
                string sql = "";
                if (keyNode.NodeType == null)
                {
                    sql = @"delete from " + tableName;
                }
                else
                {
                    sql = @"delete from " + tableName + " where " + keyNode.NodeType + " = " + "'" + keyNode.NodeValue + "' ";
                }

                OleDbCommand comm = new OleDbCommand(sql, odcConnection);
                comm.ExecuteNonQuery();
                odcConnection.Close();
                return true;
            }
            catch (Exception err)
            {
                errinfo = err.Message;
                return false;
            }
        }

        //
        public static bool ClearTable(string mdbPath, string tableName, ref string errinfo)
        {
            try
            {
                //1、建立连接
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbPath + ";Jet OLEDB:Database Password=haoren";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接
                odcConnection.Open();
                string sql = "";

                sql = @"delete * from " + tableName;

                OleDbCommand comm = new OleDbCommand(sql, odcConnection);
                comm.ExecuteNonQuery();
                odcConnection.Close();

                return true;
            }

            catch (Exception err)
            {
                errinfo = err.Message;

                return false;
            }
        }

        //处理数据，在把数据存到数据库前，先屏蔽那些危险字符！
        public static string DealString(string str)
        {
            str = str.Replace("<", "<");
            str = str.Replace(">", ">");
            str = str.Replace("\r", "<br>");
            str = str.Replace("\'", "’");
            str = str.Replace("\x0020", " ");

            return (str);
        }

        //恢复数据：把数据库中的数据，还原成未处理前的样子
        public static string UnDealString(string str)
        {
            str = str.Replace("<", "<");
            str = str.Replace(">", ">");
            str = str.Replace("<br>", "\r");
            str = str.Replace("’", "\'");
            str = str.Replace(" ", "\x0020");

            return (str);
        }
    }
}
