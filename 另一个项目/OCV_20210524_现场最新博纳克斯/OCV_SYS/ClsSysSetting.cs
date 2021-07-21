using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public static class ClsSysSetting
    {
        static string savePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\SettingFile\\SysSetting.json";
        static SysSetting _SysSetting;

        static SysSetting LoadSetting()
        {
            if (File.Exists(savePath))
            {
                string strData = File.ReadAllText(savePath);
                SysSetting result = JsonConvert.DeserializeObject<SysSetting>(strData);
                return result;
            }
            else
            {
                SysSetting result = new SysSetting();
                return result;
            }
        }

        static void SaveSetting()
        {
            string strData = JsonConvert.SerializeObject(_SysSetting);
            File.WriteAllText(savePath, strData);
        }

        public static SysSetting SysSetting
        {
            get
            {
                if (_SysSetting == null)
                {
                    _SysSetting = LoadSetting();
                }
                return _SysSetting;

            }
            set
            {
                _SysSetting = value;
                SaveSetting();
            }
        }
    }

    public class SysSetting
    {
        public string DetailDataSavePath { get; set; }
        public string EndDataSavePath { get; set; }
        public string BatCodeSavePath { get; set; }
        public string DeviceCode { get; set; }
        public string ScanCodeCOM { get; set; }

        public bool IsNGCheck { get; set; }
        public int NGCheckCount { get; set; }

        public bool CheckFIFO { get; set; }

        public bool LoginWithPassWord { get; set; }
        public bool PassPropcentWarning { get; set; }
        public double PassPropcent { get; set; }

    }

}
