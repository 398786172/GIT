using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCS.Common;
using DCS.Common.Interface;
using System.Windows.Controls;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace DCS.Model6500_COM
{
    [MultimeterDescription("Agilent DM6500", "串口通讯")]
    public class Mul_Model6500_COM : IMultimeter
    {
        private DictionaryEx dicEx;
        private IDriver driver = new ComDriver.ComDriver();
        public IDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }
        public Mul_Model6500_COM()
        {
            dicEx = this.GetConfig();
            //this.SetVoltageFunction();
        }
        public DictionaryEx GetConfig()
        {
            DictionaryEx dic = new DictionaryEx();
            //读取配置，初始化
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + this.GetType().Name + ".xml");
            if (!File.Exists(configFile))
            {
                MessageBox.Show(this.GetType().Name + "配置文件不存在,请检查路径：\r\n" + configFile);
                Environment.Exit(-1);
                return null;
            }
            XDocument doc = XDocument.Load(configFile);
            foreach (var element in doc.Element(this.GetType().Name).Elements())
            {
                dic[element.Name.ToString()] = element.Value;
            }
            return dic;
        }

        public string GetProductInfo()
        {
            return this.WriteString("*IDN?", true);            
        }

        public void Init(Dictionary<string, object> dic)
        {
            System.Windows.Controls.MenuItem menuItem = dic[CommonStr.MenuItem] as System.Windows.Controls.MenuItem;
            var subItem = new MenuItem();
            //subItem.Header = "2700/DM7276 COM设置";

            subItem.Header = "DM6500 COM设置"; //wjp,2021/4/1

            subItem.Click += (sender, e) =>
              {
                  frmSetting frm = new frmSetting(this);
                  frm.ShowDialog();
              };
            //插入菜单到倒数第二的位置
            menuItem.Items.Insert(menuItem.Items.Count - 1, subItem);
            //读取配置，初始化
            
            this.Driver.Init(dicEx);
        }

        public void ShowSetting()
        {
            frmSetting frm = new frmSetting(this);
            frm.ShowDialog();
        }
        public void Reset()
        {
            this.WriteString("*RST", false);
            System.Threading.Thread.Sleep(100);
            this.WriteString("*CLS", false);
        }

        public void SaveConfig(DictionaryEx dic)
        {
            dicEx = dic;//20190510 li 更新配置信息
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + this.GetType().Name + ".xml");
            if (File.Exists(configFile))
            {
                File.Delete(configFile);
            }
            string className = this.GetType().Name;
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement(className));
            XElement xElement = doc.Element(className);
            foreach (var key in dic.Keys)
            {
                XElement childElement = new XElement(key);
                childElement.Value = dic[key];
                xElement.Add(childElement);
            }
            doc.Save(configFile);
        }

        public bool SetCurrentFunction()
        {
            //this.WriteString("SYST:REM"); //wjp,2021/4/1  2700使用
            //this.WriteString(":CONF:CURR:DC");

            this.WriteString(":SENS:FUNC 'CURR:DC'");  //wjp,2021/4/1 DM6500 使用
            //this.WriteString(":SENS:FUNC \"CURR:DC\"");

            string cmd = "";
            string index = dicEx.ContainsKey("Speed") ? dicEx["Speed"]: "0";
            switch (index)
            {
                //case "0": cmd = "CURR:DC:NPLC 0.02"; break;
                //case "1": cmd = "CURR:DC:NPLC 0.2"; break;
                //case "2": cmd = "CURR:DC:NPLC 1"; break;
                //case "3": cmd = "CURR:DC:NPLC 10"; break;
                //case "4": cmd = "CURR:DC:NPLC 100"; break;
                case "0": cmd = ":SENS:CURR:NPLC 0.0002"; break;
                case "1": cmd = ":SENS:CURR:NPLC 0.001"; break;
                case "2": cmd = ":SENS:CURR:NPLC 0.005"; break;
                case "3": cmd = ":SENS:CURR:NPLC 0.01"; break;
                case "4": cmd = ":SENS:CURR:NPLC 10"; break;
                default:break;
            }
            this.WriteString(cmd);
            //滤波次数写100次
            //this.WriteString(":SENS:CURR:AVER:COUN 100");

            return true;
        }

        public bool SetVoltageFunction()
        {
            //this.WriteString("SYST:REM");  //wjp,2021/4/1  2700使用
            //this.WriteString(":CONF:VOLT:DC");


            this.WriteString(":SENS:FUNC 'VOLT:DC'");//wjp,2021/4/1 DM6500 使用
            //this.WriteString(":SENS:FUNC \"VOLT:DC\"");

            string cmd = "";
            string index = dicEx.ContainsKey("Speed") ? dicEx["Speed"]: "0";
            switch (index)
            {
                //case "0": cmd = "VOLT:DC:NPLC 0.002"; break;
                //case "1": cmd = "VOLT:DC:NPLC 0.02"; break;
                //case "2": cmd = "VOLT:DC:NPLC 0.11"; break;
                //case "3": cmd = "VOLT:DC:NPLC 0"; break;
                //case "4": cmd = "VOLT:DC:NPLC 12"; break;
                case "0": cmd = ":SENS:VOLT:NPLC 0.0002"; break;
                case "1": cmd = ":SENS:VOLT:NPLC 0.001"; break;
                case "2": cmd = ":SENS:VOLT:NPLC 0.005"; break;//0.005
                case "3": cmd = ":SENS:VOLT:NPLC 0.01"; break;
                case "4": cmd = ":SENS:VOLT:NPLC 10"; break;
                default: break;
            }
            this.WriteString(cmd);
            //Thread.Sleep(50);
            //this.WriteString(":SENS:VOLT:AVER:TCON REP");
            //////Thread.Sleep(50);
            //this.WriteString(":SENS:VOLT:AVER:COUN 1");
            //////Thread.Sleep(50);
            //this.WriteString(":SENS:VOLT:AVER ON");

            return true;
        }
        /// <summary>
        /// 串口的驱动只实现了WriteByte和ReadByte方法
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="needReadResponse"></param>
        /// <returns></returns>
        public string WriteString(string cmd="", bool needReadResponse=false)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd+"\r\n"));
                //this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd + "\n"));
            }
            if (needReadResponse)
            {
                Thread.Sleep(250);// Thread.Sleep(1000);
                byte[] result = this.Driver.ReadByte();
                if (result == null)
                {
                    return null;
                }
                return Encoding.ASCII.GetString(result);
            }
            else
            {
                return null;
            }            
        }

        public double? ReadValue2700()  //wjp,2021/4/1 原2700读值
        {
            WriteString(":READ?");
            List<byte> returnBytes = new List<byte>();
            string pattern = @"([+|-]\d+\.\d+E[+|-]\d+)";
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(10);
                byte[] result = this.Driver.ReadByte();
                if (result != null && result.Length > 0)
                {
                    returnBytes.AddRange(result);
                    //if (Encoding.ASCII.GetString(result).Contains((char)(17)))
                    //{
                        var response = Encoding.ASCII.GetString(returnBytes.ToArray());                       
                        Match match = Regex.Match(response, pattern);
                        if (match.Success)
                        {
                            double v;
                            if (double.TryParse(match.Groups[0].Value, out v))
                            {
                                if (dicEx.ContainsKey("Unit") && dicEx["Unit"] == "mV/mA")
                                {
                                    return v;//针对DM7276
                                }
                                else
                                {
                                    return (v * 1000);//将V转换为mv,针对2700                                   
                                }
                            }
                        }
                    //}
                }
            }
            return null;//时间到，返回-1标识没有读取到
        }

        public double? ReadValue() //wjp,2021/4/1  DM6500 读值
        {
            string RecString = "";
            DateTime t1 = DateTime.Now;
            RecString=WriteString(":READ?",true);
            DateTime t2= DateTime.Now;
            TimeSpan ts = t2 - t1;
            int count = ts.Seconds;
            if(RecString==null || RecString=="")
            {
                return null;
            }
            string RecString1 = RecString;
            //if (true)
            //{
            //    RecString = RecString1;
            //}
            //识别返回的有效部分
            RecString = this.VaildChar(RecString);

            //RecString = CharPosition();
            //if (RecString.Contains("\\"))
            //{
            //    RecString = RecString.Substring(0, RecString.IndexOf("\\"));
            //}
            //else
            //{
            //    if (RecString.Contains("+"))
            //    {
            //        RecString = RecString.Substring(0, RecString.LastIndexOf("+") + 3);
            //    }
            //    else if (RecString.Contains("-"))
            //    {
            //        string tempStr = RecString;

            //        //int pos=
            //        RecString = RecString.Substring(0, RecString.IndexOf("-") + 3);
            //    }
            //}


            Decimal dData = 0.0M;
            //RecString = "-3.827948E+00\n";  //debug  测试赋值
            string str1;
            string str2;
            if (RecString.Contains("E"))
            {

                if (RecString.Contains("\n"))
                {
                    str1 = RecString.Replace("\r\n", "%").Replace("\n", "%").Replace("\r", "%");
                    str2 = str1.Split('%')[0];
                    dData = Decimal.Parse(str2, System.Globalization.NumberStyles.Float);
                }
                else
                {
                    dData = Decimal.Parse(RecString, System.Globalization.NumberStyles.Float);
                }

                double v;
                if (double.TryParse(dData.ToString(), out v))
                {
                    if (dicEx.ContainsKey("Unit") && dicEx["Unit"] == "mV/mA")
                    {
                        return v;
                    }
                    else
                    {
                        return (v * 1000);                              
                    }
                }

            }
            return null;//时间到，返回-1标识没有读取到
        }

        /// <summary>
        /// 截取有效部分，读书的第一个有效部分
        /// </summary>
        /// <param name="charIn"></param>
        /// <param name="targetChar"></param>
        /// <returns></returns>
        private string VaildChar(string charIn)
        {
            try
            {
                string tempStr = charIn;
                string[] charCheck = new string[2] { "+", "-" };

                for (int i = 0; i < charCheck.Length; i++)
                {
                    int pos = tempStr.IndexOf(charCheck[i]);
                    if (pos == 0)
                    {
                        int pos1 = tempStr.Substring(1).IndexOf("+");
                        int pos2 = tempStr.Substring(1).IndexOf("-");
                        if ((pos1 > pos2 && pos2 != -1) || pos1 == -1)//找到下一个-号
                        {
                            pos = tempStr.Substring(1).IndexOf("-");
                        }
                        else if ((pos2 > pos1 && pos1 != -1) || pos2 == -1)//找到下一个+号
                        {
                            pos = tempStr.Substring(1).IndexOf("+");
                        }

                        tempStr = charIn.Substring(0, pos + 1 + 3);
                        //return tempStr;
                        break;
                    }
                    else if (pos > 0)
                    {
                        pos--;
                        tempStr = charIn.Substring(0, pos + 1 + 3);
                        //return tempStr;
                        break;
                    }
                }
                return tempStr;
            }
            catch (Exception)
            {
                return charIn;
                //throw;
            }
        }

    }
}
