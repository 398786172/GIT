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

namespace DCS.ModelGDM906X_COM
{
    [MultimeterDescription("GDM906X", "串口通讯")]
    public class Mul_GDN906X_COM : IMultimeter
    {
        private DictionaryEx dicEx;
        private IDriver driver = new ComDriver.ComDriver();
        public IDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }
        public Mul_GDN906X_COM()
        {
            dicEx = this.GetConfig();
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
            this.Driver.Init(dicEx);
        }
        public void ShowSetting()
        {
            frmSetting frm = new frmSetting(this);
            frm.ShowDialog();
        }

        /// <summary>
        /// 复位
        /// </summary>
        public void Reset()
        {
            this.WriteString("*RST", false);
            System.Threading.Thread.Sleep(100);
            this.WriteString("*CLS", false);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="dic"></param>
        public void SaveConfig(DictionaryEx dic)
        {
            dicEx = dic;
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

        /// <summary>
        /// 设置电流档位
        /// </summary>
        /// <returns></returns>
        public bool SetCurrentFunction()
        {
            //  this.WriteString("SYST:REM");
            this.WriteString(":CONF:CURR:DC");
            string cmd = "";
            string index = dicEx.ContainsKey("Speed") ? dicEx["Speed"] : "0";
            switch (index)
            {
                case "0": cmd = "CURR:DC:NPLC 0.02"; break;
                case "1": cmd = "CURR:DC:NPLC 0.2"; break;
                case "2": cmd = "CURR:DC:NPLC 1"; break;
                case "3": cmd = "CURR:DC:NPLC 10"; break;
                case "4": cmd = "CURR:DC:NPLC 100"; break;
                default: break;
            }
            this.WriteString(cmd);
            return true;
        }

        /// <summary>
        /// 设置电压档位
        /// </summary>
        /// <returns></returns>
        public bool SetVoltageFunction()
        {
            // this.WriteString("SYST:REM");
            this.WriteString(":CONF:VOLT:DC");
            string cmd = "";
            string index = dicEx.ContainsKey("Speed") ? dicEx["Speed"] : "0";
            switch (index)
            {
                case "0": cmd = "VOLT:DC:NPLC 0.02"; break;
                case "1": cmd = "VOLT:DC:NPLC 0.2"; break;
                case "2": cmd = "VOLT:DC:NPLC 1"; break;
                case "3": cmd = "VOLT:DC:NPLC 10"; break;
                case "4": cmd = "VOLT:DC:NPLC 100"; break;
                default: break;
            }
            this.WriteString(cmd);
            return true;
        }

        /// <summary>
        /// 串口的驱动只实现了WriteByte和ReadByte方法
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="needReadResponse"></param>
        /// <returns></returns>
        public string WriteString(string cmd = "", bool needReadResponse = false)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd + "\r\n"));
                //this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd + "\n"));
            }
            if (needReadResponse)
            {
                Thread.Sleep(1000);
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

        /// <summary>
        /// 读值命令
        /// </summary>
        /// <returns></returns>
        public double? ReadValue()
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

                    var response = Encoding.ASCII.GetString(returnBytes.ToArray());
                    Match match = Regex.Match(response, pattern);
                    if (match.Success)
                    {
                        double v;
                        if (double.TryParse(match.Groups[0].Value, out v))
                        {
                            return (v * 1000);
                        }
                    }

                }
            }
            return null;//时间到，返回-1标识没有读取到
        }
    }
}
