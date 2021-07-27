using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.OCVTest
{

    public class ClsProcessSet
    {
        static string savePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\ProcessConfig\\processConfig.json";
        static List<ProcessModel> _LstProcess;

        static List<ProcessModel> LoadProcess()
        {
            if (File.Exists(savePath))
            {
                string strData = File.ReadAllText(savePath);
                List<ProcessModel> result = JsonConvert.DeserializeObject<List<ProcessModel>>(strData);
                return result;
            }
            else
            {
                List<ProcessModel> result = new List<ProcessModel>();
                return result;
            }
        }


        static void SaveProcess()
        {
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\ProcessConfig"))
            {
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\ProcessConfig");
            }
            string strData = JsonConvert.SerializeObject(_LstProcess);
            File.WriteAllText(savePath, strData);
        }

        /// <summary>
        /// 全部工程列表
        /// </summary>
        public static List<ProcessModel> LstProcess
        {
            get
            {
                if (_LstProcess == null)
                {
                    _LstProcess = LoadProcess();
                }
                return _LstProcess;
            }
            set
            {
                _LstProcess = value;
                SaveProcess();
            }
        }

       

        public static ProcessModel WorkingProcess
        {
            get
            {
                var result = LstProcess.Count == 0 ? null : LstProcess.Find(a => a.IsCurrent);
                return result;
            }
        }
    }


    /// <summary>
    /// 工程Model
    /// </summary>
    public class ProcessModel
    {
        /// <summary>
        /// 工程名
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 警报电压
        /// </summary>
        public double WarningV { get; set; }
        /// <summary>
        /// NG电压上限
        /// </summary>
        public double MaxV { get; set; }
        /// <summary>
        /// NG电压下限
        /// </summary>
        public double MinV { get; set; }
        /// <summary>
        /// NG内阻上限
        /// </summary>
        public double MaxIR { get; set; }
        /// <summary>
        /// NG内阻下限
        /// </summary>
        public double MinIR { get; set; }
        /// <summary>
        /// 静置时间小时
        /// </summary>
        public int SpanHourt { get; set; }
        /// <summary>
        /// 静置时间分
        /// </summary>
        public int SpanMinute { get; set; }
        /// <summary>
        /// OCV类型 0:OCV1 : 1:OCV2
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 是否当前工程
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}
