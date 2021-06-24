using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OCV.OCVLogs;

using System.Data.SQLite;
using System.Collections;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace OCV.INI
{
    public class ClsIniParameter
    {
        public void IniParameter()
        {
            ClsGlobal.InitOK = false;

            //OCV系统参数
            ClsGlobal.sql = new SqLiteHelper("Setting");
            string logDB = Application.StartupPath + "\\EventLog\\EventLog";
            ClsGlobal.sql_logDB = new SqLiteHelper(logDB);
            string codeDataFile = Environment.CurrentDirectory + @"\CodeFile\CodeData";
            ClsGlobal.sqlCodeData = new SqLiteHelper(codeDataFile);
            int flag = 0;
            int Value = 0;

            #region 创建配置文件表，插入参数

            ClsGlobal.sql.InsertValues("TestSeting", new string[] { "TrayCodeRegEx", @"^(KZ|CQ|HZ)\d{6}-102$" });
            ClsGlobal.sql.InsertValues("TestSeting", new string[] { "CellCodeRegEx", @"^(CV)\w{17}$" });
            //电池条码
            ClsGlobal.sql.CreateTable("batSeting", new string[] { "CellCodeLen", "KeyStart", "ModelLenth" }, new string[] { "CHAR KEY NOT NULL  UNIQUE", "CHAR", "CHAR", "CHAR" });
            ClsGlobal.sql.InsertValues("batSeting", new string[] { "19", "10", "5" });
            #endregion

            #region 读取配置参数
            //读取设备参数
            DataSet SysdDs = ClsGlobal.sql.ConvertDataReaderToDataSet("System");
            if (SysdDs.Tables[0].Rows[0] != null)
            {
                for (int i = 0; i < SysdDs.Tables[0].Rows.Count; i++)
                {
                    if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "DeviceName")
                    {
                        //设备
                        ClsGlobal.DeviceName = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "DMT_COM_Mode")
                    {
                        if (int.TryParse(SysdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.DMTComMode = Value;

                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "DMT_COM_Mode数据类型异常,值为：" + SysdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "DMT_Port")
                    {

                        ClsGlobal.DMT_Port = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "DMT_USBAddr")
                    {

                        ClsGlobal.DMT_USBAddr = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "RT_Port")
                    {

                        ClsGlobal.RT_Port = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }

                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "InitBT4560")
                    {
                        string strJson = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                        //段字符串转化为字典
                        Dictionary<string, object> dicControlObject = new Dictionary<string, object>();
                        if (strJson != "")
                        {
                            dicControlObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(strJson);
                            ClsGlobal.BTcount = int.Parse(dicControlObject["ComCount"].ToString());

                            ClsGlobal.RT4560_Port = new string[ClsGlobal.BTcount];
                            ClsGlobal.TestFreq = new string[ClsGlobal.BTcount];
                            ClsGlobal.InitBT4560 = new ClsHIOKI4560.InitBT4560[ClsGlobal.BTcount];
                            ClsGlobal.BT4560RANG = new ClsHIOKI4560.RANG[ClsGlobal.BTcount];
                            ClsGlobal.ChNo = new string[ClsGlobal.BTcount];
                            for (int j = 0; j < ClsGlobal.BTcount; j++)
                            {

                                ClsGlobal.RT4560_Port[j] = dicControlObject["Port" + j].ToString();
                                ClsGlobal.TestFreq[j] = dicControlObject["TestFreq" + j].ToString();
                                ClsGlobal.InitBT4560[j] = dicControlObject["TestType" + j].ToString() == "RX" ? ClsHIOKI4560.InitBT4560.RX : ClsHIOKI4560.InitBT4560.Zθ;
                                string RANG = dicControlObject["TestRang" + j].ToString();
                                if (RANG == "R100mΩ")
                                {
                                    ClsGlobal.BT4560RANG[j] = ClsHIOKI4560.RANG.R100mΩ;
                                }
                                else if (RANG == "R10mΩ")
                                {
                                    ClsGlobal.BT4560RANG[j] = ClsHIOKI4560.RANG.R10mΩ;
                                }
                                else if (RANG == "R3mΩ")
                                {
                                    ClsGlobal.BT4560RANG[j] = ClsHIOKI4560.RANG.R3mΩ;
                                }
                                else
                                {
                                    ClsGlobal.BT4560RANG[j] = ClsHIOKI4560.RANG.R100mΩ;
                                }
                                ClsGlobal.ChNo[j] = dicControlObject["TestChNo" + j].ToString();
                            }
                        }

                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "Switch_Port")
                    {
                        string strJson = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                        //段字符串转化为字典
                        Dictionary<string, object> dicControlObject = new Dictionary<string, object>();
                        if (strJson != "")
                        {
                            dicControlObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(strJson);
                            ClsGlobal.Switch_Count = int.Parse(dicControlObject["ComCount"].ToString());
                            ClsGlobal.Switch_Port = new string[ClsGlobal.Switch_Count];
                            ClsGlobal.SwitchVersionStr = new string[ClsGlobal.Switch_Count];
                            ClsGlobal.SwitchChNo = new string[ClsGlobal.Switch_Count];
                            ClsGlobal.SwitchVersion = new int[ClsGlobal.Switch_Count];
                            for (int j = 0; j < ClsGlobal.Switch_Count; j++)
                            {

                                ClsGlobal.Switch_Port[j] = dicControlObject["Port" + j].ToString();
                                ClsGlobal.SwitchVersionStr[j] = dicControlObject["SwitchVersion" + j].ToString();
                                if (ClsGlobal.SwitchVersionStr[j] == "新版本")
                                {
                                    ClsGlobal.SwitchVersion[j] = 2;
                                }
                                else
                                {
                                    ClsGlobal.SwitchVersion[j] = 1;
                                }
                                ClsGlobal.SwitchChNo[j] = dicControlObject["TestChNo" + j].ToString(); ;
                            }
                        }
                    }

                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "OCVAddr")
                    {
                        if (int.TryParse(SysdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.OCVAddr = Value;

                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "OCVAddr数据类型异常,值为：" + SysdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "ACIRAddr")
                    {
                        if (int.TryParse(SysdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.ACIRAddr = Value;

                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ACIRAddr数据类型异常,值为：" + SysdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "PLC_PIAddr")
                    {
                        if (SysdDs.Tables[0].Rows[i]["Value"].ToString() != "")
                        {
                            ClsGlobal.PLCAddr = SysdDs.Tables[0].Rows[i]["Value"].ToString();

                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "PLC_PIAddr数据类型异常,值为：" + SysdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "PLC_Port")
                    {
                        if (int.TryParse(SysdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.PLCPort = Value;

                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "PLC_Port数据类型异常,值为：" + SysdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "Temp_Port")
                    {

                        ClsGlobal.Temp_Port = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "Scan_Port")
                    {

                        ClsGlobal.Scan_Port = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "Loca_Port")
                    {

                        ClsGlobal.Loca_Port = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "OCVBCTime")
                    {
                        string strTime = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                        DateTime myTime;
                        if (DateTime.TryParse(strTime, out myTime) == false)
                        {
                            myTime = DateTime.MaxValue.AddYears(-100);
                        }
                        ClsGlobal.OCV3BCTime = myTime;
                    }

                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "OCV23BCValue")
                    {
                        string strValue = SysdDs.Tables[0].Rows[i]["Value"].ToString();
                        double myValue;
                        if (double.TryParse(strValue, out myValue) == false)
                        {
                            myValue = 0;
                        }
                        ClsGlobal.OCV23BCValue = myValue;
                    }

                    else if (SysdDs.Tables[0].Rows[i]["Parameter"].ToString() == "LocalDays")
                    {
                        if (int.TryParse(SysdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.LocalDays = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "PLC_Port数据类型异常,值为：" + SysdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }


                }
            }
            //读数据库参数参数
            DataSet SqldDs = ClsGlobal.sql.ConvertDataReaderToDataSet("SqlDBSet");
            if (SqldDs.Tables[0].Rows[0] != null)
            {
                for (int i = 0; i < SqldDs.Tables[0].Rows.Count; i++)
                {
                    if (SqldDs.Tables[0].Rows[i]["DBType"].ToString() == "Server_QT")
                    {
                        ClsGlobal.Server_QT_IP = SqldDs.Tables[0].Rows[i]["ServerIP"].ToString();
                        ClsGlobal.Server_QT_id = SqldDs.Tables[0].Rows[i]["UserName"].ToString();
                        ClsGlobal.Server_QT_Pwd = SqldDs.Tables[0].Rows[i]["Password"].ToString();
                        ClsGlobal.Server_QT_DB = SqldDs.Tables[0].Rows[i]["DBName"].ToString();
                    }
                    else if (SqldDs.Tables[0].Rows[i]["DBType"].ToString() == "Server_Local")
                    {
                        ClsGlobal.Server_Local_IP = SqldDs.Tables[0].Rows[i]["ServerIP"].ToString();
                        ClsGlobal.Server_Local_id = SqldDs.Tables[0].Rows[i]["UserName"].ToString();
                        ClsGlobal.Server_Local_Pwd = SqldDs.Tables[0].Rows[i]["Password"].ToString();
                        ClsGlobal.Server_Local_DB = SqldDs.Tables[0].Rows[i]["DBName"].ToString();
                    }
                    else if (SqldDs.Tables[0].Rows[i]["DBType"].ToString() == "Server_OCV_QT")
                    {
                        ClsGlobal.Server_OCV_IP = SqldDs.Tables[0].Rows[i]["ServerIP"].ToString();
                        ClsGlobal.Server_OCV_id = SqldDs.Tables[0].Rows[i]["UserName"].ToString();
                        ClsGlobal.Server_OCV_Pwd = SqldDs.Tables[0].Rows[i]["Password"].ToString();
                        ClsGlobal.Server_OCV_DB = SqldDs.Tables[0].Rows[i]["DBName"].ToString();
                    }
                }
            }

            //读取MES参数
            DataSet MESdDs = ClsGlobal.sql.ConvertDataReaderToDataSet("MESSet");
            if (MESdDs.Tables[0].Rows[0] != null)
            {
                for (int i = 0; i < MESdDs.Tables[0].Rows.Count; i++)
                {
                    if (MESdDs.Tables[0].Rows[i]["MESType"].ToString() == "MESURL_bindtray")
                    {
                        ClsGlobal.MESURL_bindtray = MESdDs.Tables[0].Rows[i]["Value"].ToString();

                    }
                    else if (MESdDs.Tables[0].Rows[i]["MESType"].ToString() == "MESURL_OCV")
                    {
                        ClsGlobal.MESURL_OCV = MESdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (MESdDs.Tables[0].Rows[i]["MESType"].ToString() == "SITE")
                    {
                        ClsGlobal.MES_SITE = MESdDs.Tables[0].Rows[i]["Value"].ToString();

                    }
                }
            }

            //读取测试参数
            DataSet TestdDs = ClsGlobal.sql.ConvertDataReaderToDataSet("TestSeting");
            if (TestdDs.Tables[0].Rows[0] != null)
            {
                for (int i = 0; i < TestdDs.Tables[0].Rows.Count; i++)
                {
                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "OCVType")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.OCVType = Value;
                            ClsGlobal.OCVTypeShow = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "OCVType数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }

                    }

                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "IsLocalOCVType")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.IsLocalOCVType = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "OCVType数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }

                    }


                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "ExpEnable")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.ExpEnable = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ExpEnable数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }

                    }

                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "Time_t")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.Time_t = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "Time_t数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }

                    }
                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "ChannelExp_n")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.ChannelExp_n = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ExpEnable数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }

                    }
                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "TrayExp_m")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.TrayExp_m = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ExpEnable数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }
                    }

                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "StartIndex")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.StartIndex = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "StartIndex数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }
                    }
                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "BatTypeLen")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.BatTypeLen = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "StartIndex数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }
                    }

                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "BatClassLen")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.BatClassLen = Value;
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "StartIndex数据类型获取方式异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }
                    }
                    if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "ReportPath")
                    {
                        ClsGlobal.ReportPath = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                    }

                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "DeviceNo")
                    {
                        ClsGlobal.DeviceNo = TestdDs.Tables[0].Rows[i]["Value"].ToString();

                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "TrayBattType")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.TrayTypeFlag = Value;
                            if (ClsGlobal.TrayTypeFlag == 1)
                            {
                                ClsGlobal.TrayType = 38;
                                ClsGlobal.Lastboard = 3;
                            }
                            //else if (ClsGlobal.TrayTypeFlag == 2)
                            //{
                            //    ClsGlobal.TrayType = 74;
                            //    ClsGlobal.Lastboard = 3;   
                            //}
                            //else if (ClsGlobal.TrayTypeFlag == 3)
                            //{
                            //    ClsGlobal.TrayType = 40;
                            //    ClsGlobal.Lastboard = 2;
                            //}
                        }
                        else
                        {
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "TrayBattType数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            flag++;
                            //参数异常
                        }

                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "TestType")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.TestType = Value;
                            ClsGlobal.UIsettingTestType = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "TestType数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "Run_Mode")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.OCV_RunMode = (eRunMode)Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "Run_Mode数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "EN_TestTemp")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.EN_TestTemp = Value;

                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "EN_TestTemp数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "TrayCodeLen")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.TrayCodeLengh = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "TrayCodeLen数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }

                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "MaxTestNum")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.MaxTestNum = Value;
                            if (ClsGlobal.MaxTestNum > 3)
                            {
                                ClsGlobal.MaxTestNum = 3;
                            }
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "MaxTestNum数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "ReTestLmt_ACIR")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.ReTestLmt_ACIR = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ReTestLmt_ACIR数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "TempTestCH")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.EnCH = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ReTestLmt_ACIR数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "DeviceCode")
                    {
                        ClsGlobal.RESRCE = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                    }

                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "DebugLogFlag")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.DebugLog = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "DebugLogFlag数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "ProjectSetType")
                    {
                        if (int.TryParse(TestdDs.Tables[0].Rows[i]["Value"].ToString(), out Value))
                        {
                            ClsGlobal.ProjectSetType = Value;
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "ProjectSetType数据类型异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "JT_NO")
                    {
                        if (TestdDs.Tables[0].Rows[i]["Value"].ToString() != "")
                        {
                            ClsGlobal.JT_NO = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "机台编号异常,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }

                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "OPEATION_ID")
                    {
                        if (TestdDs.Tables[0].Rows[i]["Value"].ToString() != "")
                        {
                            ClsGlobal.OPEATION_ID = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                            //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", ClsGlobal.OPEATION_ID);
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "OPEATION_ID,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "SITE")
                    {
                        if (TestdDs.Tables[0].Rows[i]["Value"].ToString() != "")
                        {
                            ClsGlobal.SITE = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                            //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", ClsGlobal.OPEATION_ID);
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "OPEATION_ID,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "PCID")
                    {
                        if (TestdDs.Tables[0].Rows[i]["Value"].ToString() != "")
                        {
                            ClsGlobal.PCID = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                            //INIAPI.INIWriteValue(ClsGlobal.SZBPath, "OPEATION_ID", "opeation_id", ClsGlobal.OPEATION_ID);
                        }
                        else
                        {
                            flag++;
                            ClsLogs.INIlogNet.WriteFatal("参数异常", "PCID,值为：" + TestdDs.Tables[0].Rows[i]["Value"].ToString());
                            //参数异常
                        }
                    }

                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "TrayCodeRegEx")
                    {
                        ClsGlobal.TrayCodeRegEx = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                    else if (TestdDs.Tables[0].Rows[i]["SetType"].ToString() == "CellCodeRegEx")
                    {
                        ClsGlobal.CellCodeRegEx = TestdDs.Tables[0].Rows[i]["Value"].ToString();
                    }
                }
            }
            //读取电池条码参数
            DataSet BatdDs = ClsGlobal.sql.ConvertDataReaderToDataSet("batSeting");

            for (int i = 0; i < BatdDs.Tables[0].Rows.Count; i++)
            {
                struBatSet ProjectSet = new struBatSet();
                if (int.TryParse(BatdDs.Tables[0].Rows[i][0].ToString(), out Value))
                {
                    ProjectSet.P_CellCodeLength = Value;

                }
                else
                {
                    flag++;
                    ClsLogs.INIlogNet.WriteFatal("参数异常", "CellCodeLen数据类型异常,值为：" + TestdDs.Tables[0].Rows[i][0].ToString());
                    //参数异常
                }
                if (int.TryParse(BatdDs.Tables[0].Rows[i][1].ToString(), out Value))
                {
                    ProjectSet.P_KeyStart = Value;
                }
                else
                {
                    flag++;
                    ClsLogs.INIlogNet.WriteFatal("参数异常", "KeyStart数据类型异常,值为：" + TestdDs.Tables[0].Rows[i][1].ToString());
                    //参数异常
                }
                if (int.TryParse(BatdDs.Tables[0].Rows[i][2].ToString(), out Value))
                {
                    ProjectSet.P_ModelLenth = Value;
                }
                else
                {
                    flag++;
                    ClsLogs.INIlogNet.WriteFatal("参数异常", "KeyStart数据类型异常,值为：" + TestdDs.Tables[0].Rows[i][2].ToString());
                    //参数异常
                }
                //工程设定加载
                ClsGlobal.lstBatSet.Add(ProjectSet);
            }
            #endregion

            if (flag == 0)
            {
                ClsGlobal.InitOK = true;
            }
        }
    }
}
