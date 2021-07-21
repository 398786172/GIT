using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class ClsTempControl
    {

        ClsIOControl modbusControl;

        public static TempModel CurrentTemp { get; set; } = new TempModel();
        public ClsTempControl()
        {
            //温度警报事件
            while (true)
            {
                if (ClsGlobal.mIOControl == null)
                {
                    System.Threading.Thread.Sleep(10);
                }
                else
                {
                    break;
                }
            }

            //Task taskOpenTemp = new Task(OpenTempGet);
            //taskOpenTemp.Start();
            modbusControl = ClsGlobal.mIOControl;
            //modbusControl.WriteSingleRegister(ClsTempAddr.CloseTemRead, 1); //关闭周期温度获取
            modbusControl.WriteSingleRegister(ClsTempAddr.OpenTemRead, 1); //开启周期温度获取   

            Task taskGetTemp = new Task(ReflashTemp);
            taskGetTemp.Start();
        }


        void OpenTempGet()
        {
            while (true)
            {
                try
                {
                    modbusControl = ClsGlobal.mIOControl;
                    //modbusControl.WriteSingleRegister(ClsTempAddr.CloseTemRead, 1); //关闭周期温度获取
                    modbusControl.WriteSingleRegister(ClsTempAddr.OpenTemRead, 1); //开启周期温度获取                       
                    System.Threading.Thread.Sleep(30 * 60 * 1000); //30分容刷新一起,保证不出问题
                }
                catch { }
            }
        }


        /// <summary>
        /// 待协议补充
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetTemp(int index)
        {
            double result = 0;
            switch (index)
            {
                case 1:
                    result = CurrentTemp.Temp1;
                    break;
                case 2:
                    result = CurrentTemp.Temp2;
                    break;
                case 3:
                    result = CurrentTemp.Temp3;
                    break;
                case 4:
                    result = CurrentTemp.Temp4;
                    break;
                case 5:
                    result = CurrentTemp.Temp5;
                    break;
                case 6:
                    result = CurrentTemp.Temp6;
                    break;
                case 7:
                    result = CurrentTemp.Temp7;
                    break;
                case 8:
                    result = CurrentTemp.Temp8;
                    break;
                case 9:
                    result = CurrentTemp.Temp9;
                    break;
            }
            return result;
        }

        void ReflashTemp()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    byte[] readData;
                    modbusControl.ReadRegister(ClsTempAddr.ObjectTemp1, 18, out readData);

                    TempModel result = new TempModel();
                    int dataCount = 3;

                    int index = 0;
                    //index = index + 2;
                    var value1 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value1 = value1 + ClsTempSetting.TempSetting.TempDevice1.AdjuctValue;
                    if (temp1.Count > dataCount)
                    {
                        temp1.Dequeue();

                    }
                    temp1.Enqueue(value1);
                    result.Temp1 = temp1.Average();


                    index = index + 2;
                    index = index + 2;
                    var value2 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value2 = value2 + ClsTempSetting.TempSetting.TempDevice2.AdjuctValue;
                    if (temp2.Count > dataCount)
                    {
                        temp2.Dequeue();

                    }
                    temp2.Enqueue(value2);
                    result.Temp2 = temp2.Average();


                    index = index + 2;
                    index = index + 2;
                    var value3 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value3 = value3 + ClsTempSetting.TempSetting.TempDevice3.AdjuctValue;
                    if (temp3.Count > dataCount)
                    {
                        temp3.Dequeue();

                    }
                    temp3.Enqueue(value3);
                    result.Temp3 = temp3.Average();

                    index = index + 2;
                    index = index + 2;
                    var value4 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value4 = value4 + ClsTempSetting.TempSetting.TempDevice4.AdjuctValue;
                    if (temp4.Count > dataCount)
                    {
                        temp4.Dequeue();

                    }
                    temp4.Enqueue(value4);
                    result.Temp4 = temp4.Average();

                    index = index + 2;
                    index = index + 2;
                    var value5 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value5 = value5 + ClsTempSetting.TempSetting.TempDevice5.AdjuctValue;
                    if (temp5.Count > dataCount)
                    {
                        temp5.Dequeue();

                    }
                    temp5.Enqueue(value5);
                    result.Temp5 = temp5.Average();


                    index = index + 2;
                    index = index + 2;
                    var value6 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value6 = value6 + ClsTempSetting.TempSetting.TempDevice6.AdjuctValue;
                    if (temp6.Count > dataCount)
                    {
                        temp6.Dequeue();

                    }
                    temp6.Enqueue(value6);
                    result.Temp6 = temp6.Average();



                    index = index + 2;
                    index = index + 2;
                    var value7 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;

                    value7 = value7 + ClsTempSetting.TempSetting.TempDevice7.AdjuctValue;

                    if (temp7.Count > dataCount)
                    {
                        temp7.Dequeue();

                    }

                    temp7.Enqueue(value7);

                    result.Temp7 = temp7.Average();



                    index = index + 2;
                    index = index + 2;
                    var value8 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;

                    value8 = value8 + ClsTempSetting.TempSetting.TempDevice8.AdjuctValue;

                    if (temp8.Count > dataCount)
                    {
                        temp8.Dequeue();

                    }

                    temp8.Enqueue(value8);

                    result.Temp8 = temp8.Average();


                    index = index + 2;
                    index = index + 2;
                    var value9 = (double)((UInt16)((readData[index] << 8) + readData[index + 1])) / 100;
                    value9 = value9 + ClsTempSetting.TempSetting.TempDevice9.AdjuctValue;

                    if (temp9.Count > dataCount)
                    {
                        temp9.Dequeue();

                    }
                    temp9.Enqueue(value9);
                    result.Temp9 = temp9.Average();



                    result.ScanTime = DateTime.Now;
                    lock (CurrentTemp)
                    {
                        CurrentTemp = result;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }


        Queue<double> temp1 = new Queue<double>();
        Queue<double> temp2 = new Queue<double>();
        Queue<double> temp3 = new Queue<double>();
        Queue<double> temp4 = new Queue<double>();
        Queue<double> temp5 = new Queue<double>();
        Queue<double> temp6 = new Queue<double>();
        Queue<double> temp7 = new Queue<double>();
        Queue<double> temp8 = new Queue<double>();
        Queue<double> temp9 = new Queue<double>();

        public TempModel GetNowTemp()
        {
            return CurrentTemp;
        }

        public double GetAvgNowTem()
        {
            double result = 0;
            var tModel = GetNowTemp();
            List<double> lst = new List<double>();
            if (!ClsTempSetting.TempSetting.TempDevice1.IsShield)
            {
                lst.Add(tModel.Temp1);
            }
            if (!ClsTempSetting.TempSetting.TempDevice2.IsShield)
            {
                lst.Add(tModel.Temp2);
            }
            if (!ClsTempSetting.TempSetting.TempDevice3.IsShield)
            {
                lst.Add(tModel.Temp3);
            }
            if (!ClsTempSetting.TempSetting.TempDevice4.IsShield)
            {
                lst.Add(tModel.Temp4);
            }
            if (!ClsTempSetting.TempSetting.TempDevice5.IsShield)
            {
                lst.Add(tModel.Temp5);
            }
            if (!ClsTempSetting.TempSetting.TempDevice6.IsShield)
            {
                lst.Add(tModel.Temp6);
            }
            if (!ClsTempSetting.TempSetting.TempDevice7.IsShield)
            {
                lst.Add(tModel.Temp7);
            }
            if (!ClsTempSetting.TempSetting.TempDevice8.IsShield)
            {
                lst.Add(tModel.Temp8);
            }
            if (!ClsTempSetting.TempSetting.TempDevice9.IsShield)
            {
                lst.Add(tModel.Temp9);
            }
            if (lst.Count > 3)
            {
                lst.Remove(lst.Max());
                lst.Remove(lst.Min());
                result = lst.Sum() / lst.Count();
            }
            else if (lst.Count == 0)
            {
                result = 0;
            }
            else
            {
                result = lst.Average();
            }

            return result;
        }


        public double GetNowTempSpan()
        {
            double result = 0;
            var tModel = GetNowTemp();
            List<double> lst = new List<double>();
            if (!ClsTempSetting.TempSetting.TempDevice1.IsShield)
            {
                lst.Add(tModel.Temp1);
            }
            if (!ClsTempSetting.TempSetting.TempDevice2.IsShield)
            {
                lst.Add(tModel.Temp2);
            }
            if (!ClsTempSetting.TempSetting.TempDevice3.IsShield)
            {
                lst.Add(tModel.Temp3);
            }
            if (!ClsTempSetting.TempSetting.TempDevice4.IsShield)
            {
                lst.Add(tModel.Temp4);
            }
            if (!ClsTempSetting.TempSetting.TempDevice5.IsShield)
            {
                lst.Add(tModel.Temp5);
            }
            if (!ClsTempSetting.TempSetting.TempDevice6.IsShield)
            {
                lst.Add(tModel.Temp6);
            }
            if (!ClsTempSetting.TempSetting.TempDevice7.IsShield)
            {
                lst.Add(tModel.Temp7);
            }
            if (!ClsTempSetting.TempSetting.TempDevice8.IsShield)
            {
                lst.Add(tModel.Temp8);
            }
            if (!ClsTempSetting.TempSetting.TempDevice9.IsShield)
            {
                lst.Add(tModel.Temp9);
            }
            if (lst.Count > 2)
            {
                result = lst.Max() - lst.Min();
            }
            else
            {
                result = 0;
            }

            return result;
        }

    }



    public class ClsTempAddr
    {
        /// <summary>
        /// 启动周期读取温度 ---启动 = 1 ,  下位机收到1清0
        /// </summary>
        public const byte OpenTemRead = 70;
        /// <summary>
        /// 停止周期读取温度 ---停止 = 1  , 下位机收到1清0
        /// </summary>
        public const byte CloseTemRead = 71;
        //  public const byte OpenTemRead = 0x0008;


        /// <summary>
        /// 请求设置传感器地址 ---请求 = 1 , 下位机收到1清0, 硬件上每次只能接入一个传感器
        /// </summary>
        public const byte SetSensorAddr = 73;
        /// <summary>
        /// 需要设置的通道 ---0, 1~5 ;
        /// </summary>
        public const byte SetSensorAddrPassageway = 74;
        /// <summary>
        /// 需要设置的地址值 ---地址只能是{1A,,2A,3A,4A,5A}之一
        /// </summary>
        public const byte SetSensorAddrValue = 75;
        //public const byte OpenTemRead = 0x000C;

        /// <summary>
        /// 读地址 ---启动 = 1 , 下位机收到1清0
        /// </summary>
        public const byte ReadAddr = 77;
        /// <summary>
        /// 需要读的通道 ---0, 1~5 ,
        /// </summary>
        public const byte ReadPassageway = 78;
        //public const byte OpenTemRead = 0x000F;

        /// <summary>
        /// 请求设置传感器发射率值 ---请求 = 1 , 下位机收到1清0, 硬件上每次只能接入一个传感器
        /// </summary>
        public const byte SetSendorRate = 80;
        /// <summary>
        /// 需要设置的通道 ---0, 1~5 ;
        /// </summary>
        public const byte SetSensorRatePassageway = 81;
        /// <summary>
        /// 需要设置的发射率值 ---0x0 ~0xFFFF
        /// </summary>
        public const byte SetSensorRateValue = 82;
        //public const byte OpenTemRead = 0x0013;

        /// <summary>
        /// 读发射率 ---启动 =1 ,  下位机收到1清0
        /// </summary>
        public const byte ReadRate = 84;
        /// <summary>
        /// 需要读的通道 ---0, 1~5 ,
        /// </summary>
        public const byte ReadRatePassageway = 85;
        //public const byte OpenTemRead = 0x0016;
        //public const byte OpenTemRead = 0x0017;
        //public const byte OpenTemRead = 0x0018;
        //public const byte OpenTemRead = 0x0019;

        /// <summary>
        /// 物体温度1 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp1 = 214;
        /// <summary>
        /// 环境温度1 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp1 = 215;
        /// <summary>
        /// 物体温度2 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp2 = 216;
        /// <summary>
        /// 环境温度2 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp2 = 217;
        /// <summary>
        /// 物体温度3 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp3 = 218;
        /// <summary>
        /// 环境温度3 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp3 = 219;
        /// <summary>
        /// 物体温度4 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp4 = 220;
        /// <summary>
        /// 环境温度4 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp4 = 221;
        /// <summary>
        /// 物体温度5 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp5 = 222;
        /// <summary>
        /// 环境温度5 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp5 = 223;
        /// <summary>
        /// 物体温度6 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp6 = 224;
        /// <summary>
        /// 环境温度6 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp6 = 225;
        /// <summary>
        /// 物体温度7 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp7 = 226;
        /// <summary>
        /// 环境温度7 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp7 = 227;
        /// <summary>
        /// 物体温度8 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp8 = 228;
        /// <summary>
        /// 环境温度8 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp8 = 229;
        /// <summary>
        /// 物体温度9 物体温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte ObjectTemp9 = 230;
        /// <summary>
        /// 环境温度9 环境温度 输出值=温度(分辨率0.01°)*100, 周期更新.
        /// </summary>
        public const byte EnvironmenTemp9 = 231;
    }


    public class TempModel
    {
        public double Temp1 { get; set; }
        public double Temp2 { get; set; }
        public double Temp3 { get; set; }
        public double Temp4 { get; set; }
        public double Temp5 { get; set; }
        public double Temp6 { get; set; }
        public double Temp7 { get; set; }
        public double Temp8 { get; set; }
        public double Temp9 { get; set; }

        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime ScanTime { get; set; }

    }


    public static class ClsTempSetting
    {
        static string savePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\TempSetting";
        static string filePath = "SysSetting.json";
        static TempSetting _TempSetting;

        static TempSetting LoadTempSetting()
        {
            string temPath = Path.Combine(savePath, filePath);
            if (File.Exists(temPath))
            {
                string strData = File.ReadAllText(temPath);
                TempSetting result = JsonConvert.DeserializeObject<TempSetting>(strData);
                return result;
            }
            else
            {
                TempSetting result = new TempSetting();
                result.TempDCRExpression = new TempDCRExpression();
                return result;
            }
        }

        static void SaveTempSetting()
        {
            string temPath = Path.Combine(savePath, filePath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string strData = JsonConvert.SerializeObject(_TempSetting);
            try
            {
                File.WriteAllText(temPath, strData);
            }
            catch (Exception ex)
            {
                string dd = ex.Message;
            }
        }

        public static TempSetting TempSetting
        {
            get
            {
                if (_TempSetting == null)
                {
                    _TempSetting = LoadTempSetting();
                }
                return _TempSetting;

            }
            set
            {
                _TempSetting = value;
                SaveTempSetting();
            }
        }

        public static float TempDCR(float t, float dcr)
        {
            float result = 0;
            float k1 = TempSetting.TempDCRExpression.K1;
            float k2 = TempSetting.TempDCRExpression.K2;
            float k3 = TempSetting.TempDCRExpression.K3;
            float tempY = TempSetting.TempDCRExpression.TemY;

            result = k1 * ((t - tempY) * (t - tempY)) + k2 * (t - tempY) * k3 + dcr;

            return result;
        }
    }

    public class TempSetting
    {
        public TempSetting()
        {
            TempDCRExpression = new TempDCRExpression();
            TempDevice1 = new TempDeviceSetting() { ViewIndex = 2 };
            TempDevice2 = new TempDeviceSetting() { ViewIndex = 8 };
            TempDevice3 = new TempDeviceSetting() { ViewIndex = 5 };
            TempDevice4 = new TempDeviceSetting() { ViewIndex = 1 };
            TempDevice5 = new TempDeviceSetting() { ViewIndex = 7 };
            TempDevice6 = new TempDeviceSetting() { ViewIndex = 4 };
            TempDevice7 = new TempDeviceSetting() { ViewIndex = 9 };
            TempDevice8 = new TempDeviceSetting() { ViewIndex = 6 };
            TempDevice9 = new TempDeviceSetting() { ViewIndex = 3 };
        }
        public float SafeTemp { get; set; }
        public float MaxWarningTemp { get; set; }
        public float MinWarningTemp { get; set; }
        public float TempSpan { get; set; }
        public TempDCRExpression TempDCRExpression { get; set; }

        public TempDeviceSetting TempDevice1 { get; set; }
        public TempDeviceSetting TempDevice2 { get; set; }
        public TempDeviceSetting TempDevice3 { get; set; }
        public TempDeviceSetting TempDevice4 { get; set; }
        public TempDeviceSetting TempDevice5 { get; set; }
        public TempDeviceSetting TempDevice6 { get; set; }
        public TempDeviceSetting TempDevice7 { get; set; }
        public TempDeviceSetting TempDevice8 { get; set; }
        public TempDeviceSetting TempDevice9 { get; set; }
    }

    /// <summary>
    /// 温度采样器
    /// </summary>
    public class TempDeviceSetting
    {
        /// <summary>
        /// 采样器序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 是否屏蔽
        /// </summary>
        public bool IsShield { get; set; }
        /// <summary>
        /// 补偿值
        /// </summary>
        public double AdjuctValue { get; set; }
        /// <summary>
        /// 探针显示位置
        /// </summary>
        public int ViewIndex { get; internal set; }
    }

    /// <summary>
    /// DCR温补值 计算方法 温补值 = K1 * (t - TemY)平方 + K2 * (t - TemY) + K3 + Y(DCR测试值)
    /// </summary>
    public class TempDCRExpression
    {
        public float K1 { get; set; }
        public float K2 { get; set; }
        public float K3 { get; set; }
        public float TemY { get; set; }
    }
}
