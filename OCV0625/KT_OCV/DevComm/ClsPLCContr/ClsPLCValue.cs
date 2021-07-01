using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCV.DevComm.ClsPLCContr.ClsPLCModelCollection;

namespace OCV
{
    public class ClsPLCValue
    {
        public static bool connectSuccess = false;
        public static ClsPLCModelMotor PlcValue = new ClsPLCModelMotor();  //PLC设备状态
    }
}
