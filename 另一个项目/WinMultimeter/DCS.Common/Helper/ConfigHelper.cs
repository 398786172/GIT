using System;
using System.Collections.Generic;
using System.Configuration;
using DCS.Common.Model.Setting;
using System.IO;
using System.Xml.Serialization;
using Model;

namespace DCS.Common.Helper
{
    public class ConfigHelper
    {
 

        #region new
        public static SettingInfo LoadSetting()
        {
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Config.xml");
            if (!File.Exists(xmlPath))
            {
                return new SettingInfo();
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingInfo));
            using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
               return xmlSerializer.Deserialize(fs) as SettingInfo;
            }
        }
        public static void SaveSetting(SettingInfo settingInfo)
        {
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Config.xml");
            string xmlDir = Path.GetDirectoryName(xmlPath);
            if (!Directory.Exists(xmlDir))
            {
                Directory.CreateDirectory(xmlDir);
            }
            if (File.Exists(xmlPath))
            {
                File.Delete(xmlPath);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(settingInfo.GetType());
            using (FileStream fs = new FileStream(xmlPath, FileMode.Create, FileAccess.ReadWrite))
            {
                xmlSerializer.Serialize(fs, settingInfo);
            }

        }
        #endregion

    }
}
