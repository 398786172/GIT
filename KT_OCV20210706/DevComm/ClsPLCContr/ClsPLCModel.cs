using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class ClsPLCModel
    {
        //交互信号
        public short Plc_AutoManual { set; get; }       //是自动模式
        public short Plc_HaveTray { set; get; }         //是否有托盘
        public short Plc_RequestScan { set; get; }    //请求扫码
        public short Plc_ScanFinshReply { set; get; } //扫码完成应答
        public short Plc_RequestTest_A { set; get; }    //A请求测试
        public short Plc_TestFinshReply_A { set; get; } //A测试完成应答
        public short Plc_EmergencyStop { set; get; }    //急停
        public short Plc_InitReply { get; set; } //初始化应答
        public short Plc_StartReply { get; set; } //PLC启动应答
        public short Plc_CurrentPosition1 { get; set; } //PLC当前位置1
        public short Plc_CurrentPosition2 { get; set; } //PLC当前位置2
        /// <summary>
        /// 生产周期
        /// </summary>
        public short Plc_Period { get; set; }
        public short Plc_Alarm1 { set; get; }    //报警汇总1
        public short Plc_Alarm2 { set; get; }    //报警汇总2
        public short Plc_Error { set; get; }    //报警汇总2
        public short Plc_Error2 { set; get; }    //报警汇总2

        //IO

        public short Plc_IO_XAr { set; get; }          //X轴报警
        public short Plc_IO_PosCylUp1 { set; get; }          //后顶升伸
        public short Plc_IO_PosCylDown1 { set; get; }          //后顶升缩  
        public short Plc_IO_ProbeCylClose1 { set; get; }          //探针右压合伸
        public short Plc_IO_ProbeCylOpen1 { set; get; }          //探针右压合缩
        public short Plc_IO_YanwuUp { set; get; }          //烟雾传感器前
        public short Plc_IO_YanwuDown { set; get; }          //烟雾传感器后
        public short Plc_IO_Opendoor { set; get; }          //门开关
        public short Plc_IO_Stop { set; get; }          //急停
        public short Plc_IO_Alaguntong { set; get; }          //滚筒报警
        public short Plc_IO_jiansuguandian { set; get; }          //减速关电
        public short Plc_IO_jiantuopandaowei { set; get; }          //托盘到位检测
        public short Plc_IO_tuopanrudingqian { set; get; }          //托盘入定位销检测（前）
        public short Plc_IO_tuopanrudinghou { set; get; }          //托盘入定位销检测（后）
        public short Plc_IO_qiyajiance { set; get; }          //气压检测
        




        public short Plc_IO_CVRun { set; get; }          //正转
        public short Plc_IO_CVRunback { set; get; }      //反转
        public short Plc_IO_PosCylUp { set; get; }       //顶升上
        public short Plc_IO_PosCylDown { set; get; }     //顶升下
        public short Plc_IO_ProbeCylClose { set; get; }  //探针压合
        public short Plc_IO_ProbeCylOpen { set; get; }   //探针打开
        public short Plc_IO_BlockCylUp { set; get; }     //阻挡上
        public short Plc_IO_BlockCylDown { set; get; }   //阻挡下
        public short Plc_IO_FrCVRequest { set; get; }    //线体请求进
        public short Plc_IO_FrOCVAllow { set; get; }     //OCV允许进
        public short Plc_IO_BhOCVReq { set; get; }       //OCV请求出
        public short Plc_IO_BhCVAllow { set; get; }      //线体允许出

        public short Plc_IO_TrayForSignal { set; get; }  //托盘入口
        public short Plc_IO_SlowSpeedSignal { set; get; }//减速信号
        public short Plc_IO_TrayInSignal { set; get; }   //托盘到位信号
        public short Plc_IO_TrayTypeSignal { set; get; } //线体允许出

        public short Plc_IO_ZeroIng { set; get; }        //回零中
        public short Plc_IO_ZeroCompletion { set; get; } //回零完成
        public short Plc_IO_NegLimit { set; get; }       //负限位信号
        public short Plc_IO_HomeLimit { set; get; }      //获取原点信号
        public short Plc_IO_PosLimit { set; get; }       //正限位信号

        public short Plc_AutoStepNO { get; set; }  //PLC_自动流程工步

        /// <summary>
        /// PLC_初始流程工步
        /// </summary>
        public short Plc_ResetStepNO { get; set; }

    }

    public class BitAddressValue
    {
        public string Address { get; set; }

        public ushort Value { get; set; }
    }
}
