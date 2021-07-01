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
        public short Plc_AutoManual { set; get; }    //是自动模式
        public short Plc_HaveTray { set; get; }    //是否有托盘
        public short Plc_RequestScan { set; get; }    //请求扫码
        public short Plc_ScanFinshReply { set; get; } //扫码完成应答
        public short Plc_RequestTest { set; get; }    //A请求测试
        public short Plc_TestFinshReply { set; get; }    //A测试完成应答
 
        public short Plc_InitReply { set; get; }       //初始化应答

        public short Plc_EmergencyStop { set; get; }    //急停
        public short Plc_AutoStepNO { set; get; }    //自动流程工步号
        public short Plc_ResetStepNO { set; get; }    //自动流程工步号

        /// <summary>
        /// 生产周期
        /// </summary>
        public short Plc_Period { get; set; }
        public short Plc_Alarm1 { set; get; }    //报警汇总1
        public short Plc_Alarm2 { set; get; }    //报警汇总2
        public short Plc_Error{ set; get; }    //报警汇总2
        public short Plc_Error2 { set; get; }    //报警汇总2

        //IO
        public short Plc_IO_CVRun { set; get; }          //正转
        public short Plc_IO_CVRunback { set; get; }      //反转

        public short Plc_IO_PosCylUp1 { set; get; }       //顶升上
        public short Plc_IO_PosCylDown1 { set; get; }     //顶升下

        public short Plc_IO_PosCylUp2 { set; get; }       //顶升上
        public short Plc_IO_PosCylDown2 { set; get; }     //顶升下

        public short Plc_IO_ProbeCylClose1 { set; get; }  //针床压合
        public short Plc_IO_ProbeCylCloseS1 { set; get; }  //针床压合
        public short Plc_IO_ProbeCylOpen1 { set; get; }   //针床关闭

        public short Plc_IO_ProbeCylClose2 { set; get; }  //针床压合
        public short Plc_IO_ProbeCylCloseS2 { set; get; }  //针床压合
        public short Plc_IO_ProbeCylOpen2 { set; get; }   //针床关闭

        //public short Plc_IO_BlockCylUp { set; get; }     //阻挡上
        //public short Plc_IO_BlockCylDown { set; get; }   //阻挡下
        public short Plc_IO_FrCVRequest { set; get; }    //线体请求进
        public short Plc_IO_FrOCVAllow { set; get; }     //OCV允许进
        public short Plc_IO_BhOCVReq { set; get; }       //OCV请求出
        public short Plc_IO_BhCVAllow { set; get; }      //线体允许出

        public short Plc_IO_TrayForSignal { set; get; }  //托盘入口
        public short Plc_IO_SlowSpeedSignal { set; get; }//减速信号
        public short Plc_IO_TrayInSignal { set; get; }   //托盘到位信号



    }

    public class BitAddressValue
    {
        public string Address { get; set; }

        public ushort Value { get; set; }
    }
}
