using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model
{
    public class SettingInfo
    {
        public string StrMultimeterName { get; set; }

        public string CSVSavePath { get; set; } = Application.StartupPath + "\\test.csv";
        public int ReadTime { get; set; } = 50;
    }
}
