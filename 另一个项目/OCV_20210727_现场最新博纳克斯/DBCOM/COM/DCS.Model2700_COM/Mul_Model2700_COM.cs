using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using DCS.Model2700_COM.ComDriver;

namespace DCS.Model2700_COM
{
    //[MultimeterDescription("Agilent 2700/HIOKI DM7276", "串口通讯")]
    public class Mul_Model2700_COM
    {
        private Dictionary<string,string> dicEx;
        private IDriver driver = new ComDriver.ComDriver();
        public IDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }
        public Mul_Model2700_COM(string Com,string speed)
        {
            dicEx = new Dictionary<string, string>()
            {
                {"PortName",Com },
                {"BauRate","9600"},
                {"Parity","None" },
                { "DataBits","8"},
                { "StopBits","One"},
                { "Speed",speed},
                { "Unit","V/A"},
            };
            this.Driver.Init(dicEx);
        }

        public string GetProductInfo()
        {
            return this.WriteString("*IDN?", true);            
        }



        public void Reset()
        {
            this.WriteString("*RST", false);
            System.Threading.Thread.Sleep(100);
            this.WriteString("*CLS", false);
        }

        public bool SetCurrentFunction()
        {
            this.WriteString("SYST:REM");
            this.WriteString(":CONF:CURR:DC");
            string cmd = "";
            string index = dicEx.ContainsKey("Speed") ? dicEx["Speed"]: "0";
            switch (index)
            {
                case "0": cmd = "CURR:DC:NPLC 0.02"; break;
                case "1": cmd = "CURR:DC:NPLC 0.2"; break;
                case "2": cmd = "CURR:DC:NPLC 1"; break;
                case "3": cmd = "CURR:DC:NPLC 10"; break;
                case "4": cmd = "CURR:DC:NPLC 100"; break;
                default:break;
            }
            this.WriteString(cmd);
            return true;
        }

        public bool SetVoltageFunction()
        {
            this.WriteString("SYST:REM");
            this.WriteString(":CONF:VOLT:DC");
            string cmd = "";
            string index = dicEx.ContainsKey("Speed") ? dicEx["Speed"]: "0";
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
        public string WriteString(string cmd="", bool needReadResponse=false)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd+"\r\n"));
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

        public double? ReadValue()
        {
            string time1; string time2; string time3; string time4;
            int S;
            time1 = (DateTime.Now.ToString("HH:mm:ss:fff"));
            WriteString(":READ?");
            time2 = (DateTime.Now.ToString("HH:mm:ss:fff"));
            List<byte> returnBytes = new List<byte>();
            string pattern = @"([+|-]\d+\.\d+E[+|-]\d+)";
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(25);
                byte[] result = this.Driver.ReadByte();
                if (result != null && result.Length > 0)
                {
                    returnBytes.AddRange(result);
                    //if (Encoding.ASCII.GetString(result).Contains((char)(17)))
                    //{
                        var response = Encoding.ASCII.GetString(returnBytes.ToArray());                       
                        Match match = Regex.Match(response, pattern);
                    time3 = (DateTime.Now.ToString("HH:mm:ss:fff"));
                    S = i;
                    if (match.Success)
                        {
                            double v;
                            if (double.TryParse(match.Groups[0].Value, out v))
                            {
                                time4 = (DateTime.Now.ToString("HH:mm:ss:fff"));
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
          }
}
