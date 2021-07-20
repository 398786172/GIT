using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace OCV
{
    //内阻仪HIOKI4560控制
    public class ClsHIOKI4560
    {
        private SerialPort SPort = new SerialPort();
        static byte[] m_TempBuffer = new byte[10240];           //接收数据缓存
        static int m_RecOffset = 0;                             //接收字节数
        int m_VSampleRateType = 0;                              //电压采样速率,SLOW->1 MED->2, FAST->3
        int m_ZSampleRateType = 0;                              //阻抗采样速率,SLOW->1 MED->2, FAST->3
        public string Freq
        {
            set { mFreq = value; }
        }
        private string mFreq = "1000";

        public int V_SampleRate
        {
            set { mV_SampleRate = value; }
        }
        private int mV_SampleRate = 3;

        public int Z_SampleRate
        {
            set { mZ_SampleRate = value; }
        }
        private int mZ_SampleRate = 2;  

        public InitBT4560 InitRTester
        {
            set { mInitRTester = value; }
        }
        private InitBT4560 mInitRTester = InitBT4560.RX;    
        public enum InitBT4560
        {
            RXV = 0,        
            ZVθ = 1,          
            RX = 2,          
            Zθ= 3,          
            V=4
        }

        public RANG mRang
        {
            set { nRANG = value; }
        }
        private RANG nRANG = RANG.R100mΩ;

        public enum RANG
        { 
            R100mΩ=0,
            R10mΩ = 1,
            R3mΩ = 2
        }

    

       public SerialPort SerialPort
        {
            get { return SPort; }
        }

      
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">元素：COMx (x=1,2,...)</param>
        public ClsHIOKI4560(string port)
        {
            // 关闭串口
            if (SPort.IsOpen == true)
            {
                SPort.Close();
            }
            if ((SPort != null) && (SPort.IsOpen == false))
            {
                //设置串口名 
                SPort.PortName = port.Trim();
                SPort.NewLine = "\r\n";
                //SPort.BaudRate = 38400;
                SPort.ReadTimeout = 60000;
                try
                {
                    SPort.Open();

                }
                catch (IOException ex)
                {
                    throw new Exception("表BT4560的COM口错误" + ex.Message);
                }     
            }

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">元素：COMx (x=1,2,...)</param>
        public ClsHIOKI4560(string port,string freq, RANG rANG , int mv_SampleRate, int mz_SampleRate, InitBT4560 initBT4560)
        {
            // 关闭串口
            if (SPort.IsOpen == true)
            {
                SPort.Close();
            }
            if ((SPort != null) && (SPort.IsOpen == false))
            {
                //设置串口名 
                SPort.PortName = port.Trim();
                SPort.NewLine = "\r\n";
                //SPort.BaudRate = 38400;
                SPort.ReadTimeout = 60000;
                try
                {
                    SPort.Open();

                }
                catch (IOException ex)
                {
                    throw new Exception("表BT4560的COM口错误" + ex.Message);
                }
                Freq = freq;
                mRang = rANG;
                V_SampleRate = mv_SampleRate;
                Z_SampleRate = mz_SampleRate;
                InitRTester = initBT4560;
            }

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SP">串口</param>
        public ClsHIOKI4560(SerialPort SP)
        {
            // 关闭串口
            if (SPort.IsOpen == true)
            {
                SPort.Close();
            }
            if ((SPort != null) && (SPort.IsOpen == false))
            {
                SPort = SP;
                SP.NewLine = "\r\n";
                // SPort.BaudRate = 38400;
                SP.ReadTimeout = 60000;

                try
                {
                    SPort.Open();

                }
                catch (IOException ex)
                {
                    throw new Exception("表BT4560的COM口错误" + ex);
                }
             
            }
        }

        /// <summary>
        /// 仪表重启
        /// </summary>
        public void Restart()
        {
            SPort.WriteLine("*RST");
        }

        public void InitControl_IMM()
        {
            switch (mInitRTester)
            {
                case InitBT4560.RXV:
                    InitControl_IMM_RV(mV_SampleRate, mZ_SampleRate);
                    break;
                case InitBT4560.ZVθ:
                    InitControl_IMM_ZV(mV_SampleRate, mZ_SampleRate);
                    break;
                case InitBT4560.RX:
                    InitControl_IMM_R(mZ_SampleRate);
                    break;
                case InitBT4560.Zθ:
                    InitControl_IMM_Z(mZ_SampleRate);
                    break;
                case InitBT4560.V:
                    InitControl_IMM_V(mV_SampleRate);
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 内阻仪初始化(内部触发方式)
        /// </summary>
        /// <param name="V_SampleRateType">电压采样速率类型: SLOW->1 MED->2, FAST->3</param>
        /// <param name="Z_SampleRateType">阻抗采样速率类型: SLOW->1 MED->2, FAST->3</param>
        public void InitControl_IMM_ZV(int V_SampleRateType, int Z_SampleRateType)
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }
            SPort.WriteLine("*CLS");
            //SPort.WriteLine("*IDN?");                  //不读取表信息
            string str = SPort.ReadExisting();
            SPort.WriteLine(":FUNC  ZV");               //RV/ZV/R/Z/V 
            switch (mFreq)
            {
                case "0.1":
                    SPort.WriteLine(":FREQ 0.1");
                    break;
                case "1":
                    SPort.WriteLine(":FREQ 1");
                    break;
                case "10":
                    SPort.WriteLine(":FREQ 10");
                    break;
                case "100":
                    SPort.WriteLine(":FREQ 100");
                    break;
                case "1000":
                    SPort.WriteLine(":FREQ 1000");
                    break;
                default:
                    SPort.WriteLine(":FREQ " + mFreq);
                    break;
            }
            SPort.WriteLine(":RES:RANG 10E-3");       //量程:10毫欧  

            switch (Z_SampleRateType)             //阻抗采样速率类型   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE Z, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE Z, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE Z, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE Z, MED");
                    break;
            }

            switch (V_SampleRateType)             //选择电压采样速率   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE V, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE V, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE V, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE V, MED");
                    break;
            }
            SPort.WriteLine(":MEAS:VAL 1");
            SPort.WriteLine(":TRIG:SOUR IMM");
            SPort.WriteLine(":AUT OFF");            //不自动量程

            SPort.WriteLine(":INIT:CONT ON");         //Read命令连续      
        }

        public void InitControl_IMM_RV(int V_SampleRateType, int Z_SampleRateType)
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }

            SPort.WriteLine("*CLS");
            //SPort.WriteLine("*IDN?");                  //不读取表信息
            string str = SPort.ReadExisting();
            SPort.WriteLine(":FUNC  RV");               //RV/ZV/R/Z/V 
            switch (mFreq)
            {
                case "0.1":
                    SPort.WriteLine(":FREQ 0.1");
                    break;
                case "1":
                    SPort.WriteLine(":FREQ 1");
                    break;
                case "10":
                    SPort.WriteLine(":FREQ 10");
                    break;
                case "100":
                    SPort.WriteLine(":FREQ 100");
                    break;
                case "1000":
                    SPort.WriteLine(":FREQ 1000");
                    break;
                default:
                    SPort.WriteLine(":FREQ " + mFreq);
                    break;
            }
            SPort.WriteLine(":MEAS:VAL 1");
            SPort.WriteLine(":TRIG:SOUR IMM");
            SPort.WriteLine(":AUT OFF");            //不自动量程

            switch (nRANG)
            {
                case RANG.R100mΩ:
                    SPort.WriteLine("RANG 100E-3");       //量程:100毫欧    
                    break;
                case RANG.R10mΩ:
                    // SPort.WriteLine(":RES:RANG 10E-3");       //量程:10毫欧  
                    SPort.WriteLine("RANG 10E-3");       //量程:10毫欧   
                    break;
                case RANG.R3mΩ:
                    SPort.WriteLine("RANG 1E-3");       //量程:3毫欧    
                    break;       
                default:
                    SPort.WriteLine("RANG 100E-3");       //量程:100毫欧    

                    break;
            }


            SPort.WriteLine(":INIT:CONT ON");         //Read命令连续               
            switch (Z_SampleRateType)             //阻抗采样速率类型   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE Z, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE Z, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE Z, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE Z, MED");
                    break;
            }

            switch (V_SampleRateType)             //选择电压采样速率   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE V, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE V, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE V, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE V, MED");
                    break;
            }

        }

        public void InitControl_IMM_V(int V_SampleRateType)
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }

            SPort.WriteLine("*CLS");
            string str = SPort.ReadExisting();
            SPort.WriteLine(":FUNC  V");               //RV/ZV/R/Z/V    只读取电压
            SPort.WriteLine(":MEAS:VAL 1");
            SPort.WriteLine(":TRIG:SOUR IMM");

            //SPort.WriteLine(":RES:RANG 1000E-3");    //量程:300毫欧 
            SPort.WriteLine(":RANG 1E-02");    //量程:300毫欧
            SPort.WriteLine(":AUT OFF");            //不自动量程

            SPort.WriteLine(":INIT:CONT ON");         //Read命令连续        

            switch (V_SampleRateType)             //选择电压采样速率   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE V, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE V, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE V, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE V, MED");
                    break;
            }
        }

        public void InitControl_IMM_R(int Z_SampleRateType)
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }

            SPort.WriteLine("*CLS");
            //SPort.WriteLine("*IDN?");                  //不读取表信息
            string str = SPort.ReadExisting();
            SPort.WriteLine(":FUNC R");               //RV/ZV/R/Z/V 
            switch (mFreq)
            {
                case "0.1":
                    SPort.WriteLine(":FREQ 0.1");
                    break;
                case "1":
                    SPort.WriteLine(":FREQ 1");
                    break;
                case "10":
                    SPort.WriteLine(":FREQ 10");
                    break;
                case "100":
                    SPort.WriteLine(":FREQ 100");
                    break;
                case "1000":
                    SPort.WriteLine(":FREQ 1000");
                    break;
                default:
                    SPort.WriteLine(":FREQ " + mFreq);
                    break;
            }
            SPort.WriteLine(":MEAS:VAL 1");
            SPort.WriteLine(":TRIG:SOUR IMM");
            SPort.WriteLine(":AUT OFF");            //不自动量程
            switch (nRANG)
            {
                case RANG.R100mΩ:
                    SPort.WriteLine("RANG 100E-3");       //量程:100毫欧    
                    break;
                case RANG.R10mΩ:
                    // SPort.WriteLine(":RES:RANG 10E-3");       //量程:10毫欧  
                    SPort.WriteLine("RANG 10E-3");       //量程:10毫欧   
                    break;
                case RANG.R3mΩ:
                    SPort.WriteLine("RANG 1E-3");       //量程:1毫欧    
                    break;
                default:
                    SPort.WriteLine("RANG 100E-3");       //量程:100毫欧    

                    break;
            }
          
            //SPort.WriteLine(":RES:RANG 10E-3");       //量程:10毫欧 


            SPort.WriteLine(":INIT:CONT ON");         //Read命令连续    

            switch (Z_SampleRateType)             //阻抗采样速率类型   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE Z, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE Z, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE Z, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE Z, MED");
                    break;
            }
            SPort.WriteLine("SAMP:RATE V, FAST");  //快   
        }

        public void InitControl_IMM_Z(int Z_SampleRateType)
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }

            SPort.WriteLine("*CLS");
            //SPort.WriteLine("*IDN?");                  //不读取表信息
            string str = SPort.ReadExisting();
            SPort.WriteLine(":FUNC Z");               //RV/ZV/R/Z/V 
            switch (mFreq)
            {
                case "0.1":
                    SPort.WriteLine(":FREQ 0.1");
                    break;
                case "1":
                    SPort.WriteLine(":FREQ 1");
                    break;
                case "10":
                    SPort.WriteLine(":FREQ 10");
                    break;
                case "100":
                    SPort.WriteLine(":FREQ 100");
                    break;
                case "1000":
                    SPort.WriteLine(":FREQ 1000");
                    break;
                default:
                    SPort.WriteLine(":FREQ " + mFreq);
                    break;
            }
            SPort.WriteLine(":MEAS:VAL 1");
            SPort.WriteLine(":TRIG:SOUR IMM");
            SPort.WriteLine(":AUT OFF");            //不自动量程
            SPort.WriteLine(":RES:RANG 10E-3");       //量程:10毫欧    
            SPort.WriteLine(":INIT:CONT ON");         //Read命令连续               
            switch (Z_SampleRateType)             //阻抗采样速率类型   
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE Z, SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE Z, MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE Z, FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE Z, MED");
                    break;
            }
            SPort.WriteLine("SAMP:RATE V, FAST");  //快   
        }

        /// <summary>
        /// 读内阻,电抗,电压 
        /// </summary>
        /// <param name="IMPVal">阻抗（Ω）</param>
        ///  <param name="Xval">电抗（Ω）</param>
        ///  <param name="Vval">电压（V）</param>
        ///  
        public void ReadData(out string IMPVal, out string Xval, out string Vval)
        {
            try
            {
                string strVal;
                string[] arrStrVal = new string[3];

                SPort.WriteLine(":MEAS:VAL 1");
                SPort.WriteLine(":READ?");
                //SPort.WriteLine(":FETC?");               
                Thread.Sleep(500);
                strVal = SPort.ReadLine();
                arrStrVal = strVal.Split(',');

                IMPVal = arrStrVal[0];
                Xval = arrStrVal[1];
                Vval = arrStrVal[1];

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读电压 
        /// </summary>
        ///  <param name="Vval">电压（V）</param>
        ///  
        public void VReadData(out string Vval)
        {
            try
            {
                string strVal;
                string[] arrStrVal = new string[3];
                //SPort.WriteLine(":TRIG:SOUR IMM");
                SPort.WriteLine(":MEAS:VAL 1");
                SPort.WriteLine(":READ?");
                //SPort.WriteLine(":FETC?");               
                Thread.Sleep(500);
                strVal = SPort.ReadLine();
                arrStrVal = strVal.Split(',');
                Vval = arrStrVal[2];            //读电压

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读内阻
        /// </summary>
        ///  <param name="Vval">内阻值</param>
        ///  
        public void ReadRData(out string IMPVal)
        {
            try
            {
                string strVal;
                string[] arrStrVal = new string[3];
                //SPort.WriteLine(":TRIG:SOUR IMM");
                SPort.WriteLine(":MEAS:VAL 1");
                SPort.WriteLine(":READ?");
                //SPort.WriteLine(":FETC?");               
                Thread.Sleep(500);
                strVal = SPort.ReadLine();
                arrStrVal = strVal.Split(',');
                IMPVal = arrStrVal[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 读内阻 
        /// </summary>
        ///  <param name="IMPVal">内阻值</param>
        ///  
        public void ReadRData(out double IMPVal)
        {
            try
            {
                string strVal;
                string strIMPVal;
                string[] arrStrVal = new string[3];
                //SPort.WriteLine(":TRIG:SOUR IMM");
                SPort.WriteLine(":MEAS:VAL 1");
                SPort.WriteLine(":READ?");
                //SPort.WriteLine(":FETC?");               
                // Thread.Sleep(500);
                strVal = SPort.ReadLine();
                arrStrVal = strVal.Split(',');
                if (arrStrVal[0].Trim().Length <= 0 || arrStrVal[0] == null || arrStrVal[0] == "")
                {
                    strIMPVal = "0";
                    throw new Exception("读值失败");
                }
                else
                {
                    strIMPVal = Convert.ToDouble(arrStrVal[0]).ToString();
                }
                IMPVal = double.Parse(strIMPVal);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 读取多次测量储存数据
        /// </summary>
        /// <param name="Val">数据字符串</param>
        public void ReadMultiData(out string Val)
        {
            try
            {
                string strVal = "";
                SPort.WriteLine(":MEM:DATA?");
                Thread.Sleep(500);                         //串口读取时间
                strVal = SPort.ReadExisting();
                Val = strVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 清除储存数据
        /// </summary>
        public void ClearData()
        {
            try
            {
                SPort.WriteLine(":MEM:ClEA");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  零位调整
        /// </summary>
        public void ZeroADJust(out string Zval)
        {
            try
            {
                string arrStrVal;
                SPort.WriteLine(":ADJ? SPOT");
                // SPort.WriteLine(":ADJ? ALL");        
                Thread.Sleep(13500);
                arrStrVal = SPort.ReadLine();
                Zval = arrStrVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 接收事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialGetData(object sender, SerialDataReceivedEventArgs e)
        {
            int i;
            i = SPort.BytesToRead;
            SPort.Read(m_TempBuffer, m_RecOffset, i);
            m_RecOffset += i;
        }

        /// <summary>
        /// 释放控制
        /// </summary>
        public void ReleaseControl()
        {
            SPort.WriteLine("SYST:LOC");
        }

        /// <summary>
        /// 内阻量程
        /// </summary>
        public void ResRange(string Range)
        {
            SPort.WriteLine("RES:RANG " + Range);
        }

        /// <summary>
        /// 电压量程
        /// </summary>
        public void VoltRange(string Range)
        {
            SPort.WriteLine("VOLT:RANG " + Range);
        }

        /// <summary>
        /// 自动量程
        public void AutoRange()
        {
            SPort.WriteLine(":AUT ON");
        }


    }
}
