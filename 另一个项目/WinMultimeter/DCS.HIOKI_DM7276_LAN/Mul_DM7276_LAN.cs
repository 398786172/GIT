using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCS.Common;
using DCS.Common.Interface;
using System.IO;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Threading;
using System.Text.RegularExpressions;
using DCS.HIOKI_LANDriver;
using DCS.HIOKI_DM7276_LAN;

namespace DCS.HIOKI_DM7276_LAN
{
    [MultimeterDescription("HIOKI DM7276","LAN")]
    public class Mul_DM7276_LAN : IMultimeter
    {
        private DictionaryEx dicEx;
        private IDriver driver=new LANDriver();
        public IDriver Driver
        {
            get { return driver; }
        }
        public Mul_DM7276_LAN()
        {
            dicEx = this.GetConfig();
        }
        public DictionaryEx GetConfig()
        {
            DictionaryEx dic = new DictionaryEx();
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + this.GetType().Name + ".xml");
            if (!File.Exists(configFile))
            {
                throw new Exception(this.GetType().Name + "配置文件不存在");
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
            subItem.Header = "HIOKI DM7276 LAN设置";
            subItem.Click += (sender, e) =>
            {
                frmSetting frm = new frmSetting(this);
                //frmSetting frm = new frmSetting(this);
                frm.ShowDialog();
            };
            //插入菜单到倒数第二的位置
            menuItem.Items.Insert(menuItem.Items.Count - 1, subItem);
            //读取配置，初始化

            this.Driver.Init(dicEx);
        }

        public double? ReadValue()
        {
            WriteString(":READ?");
            List<byte> returnBytes = new List<byte>();
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(10);
                byte[] result = this.Driver.ReadByte();
                if (result != null && result.Length > 0)
                {
                    returnBytes.AddRange(result);
                    if (Encoding.ASCII.GetString(result).Contains((char)(17)))
                    {
                        var response = Encoding.ASCII.GetString(returnBytes.ToArray());
                        string pattern = @"([+|-]\d+\.\d+E[+|-]\d+)";
                        Match match = Regex.Match(response, pattern);
                        if (match.Success)
                        {
                            double v;
                            if (double.TryParse(match.Groups[0].Value, out v))
                            {
                                return v;//DM7276返回的是mv，不需要乘以1000
                            }
                        }
                    }
                }
            }
            return null;//时间到，返回-1标识没有读取到
        }

        public void Reset()
        {
            this.WriteString("*RST", false);
            System.Threading.Thread.Sleep(100);
            this.WriteString("*CLS", false);
        }

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

        public bool SetCurrentFunction()
        {
            throw new NotSupportedException("HIOKI DM7276无量测电流功能！");
        }

        public bool SetVoltageFunction()
        {
            this.WriteString("SYST:REM");
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

        public string WriteString(string cmd="", bool needReadResponse=false)
        {

            if (!string.IsNullOrEmpty(cmd))
            {
                this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd + "\r\n"));
            }
            if (needReadResponse)
            {
                Thread.Sleep(500);
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
    }
}
