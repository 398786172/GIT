using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OCV
{
    class LogHelper
    {
        public static void LogInfo(string mes, string saveDir)
        {
            try
            {
                string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveDir);
                savePath = Path.Combine(savePath, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string fileName = $"Info_{DateTime.Now.Day.ToString("00")}.log";
                fileName = Path.Combine(savePath, fileName);
                var sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine(mes);
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
        public static void LogErr(string mes, string saveDir)
        {

            try
            {
                string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveDir);
                savePath = Path.Combine(savePath, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string fileName = $"Err_{DateTime.Now.Day.ToString("00")}.log";
                fileName = Path.Combine(savePath, fileName);
                var sw = new StreamWriter(fileName, true, Encoding.UTF8);
                sw.WriteLine(mes);
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
