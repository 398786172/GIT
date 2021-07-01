using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClsDeviceComm;
using ClsDeviceComm.LogNet;
using System.Windows.Forms;

namespace OCV.OCVLogs
{
    public class ClsLogs
    {

        /// <summary>
        /// 日志
        /// </summary>
        public static ILogNet MESlogNet = null;
        public static ILogNet PLClogNet = null;
        public static ILogNet OCVInfologNet = null;
        public static ILogNet OCVTestlogNet = null;
        public static ILogNet SendDatalogNet = null;
        public static ILogNet SqllogNet = null;
        public static ILogNet INIlogNet = null;
        public static ILogNet ConfiglogNet = null;
        public static ILogNet UserinfologNet = null;
        public static ILogNet MesUpLoadStatus = null;
        public static ILogNet WCSlogNet = null;
        public static void LogNet()
        {
            ClsMessageDegree Degree;

            if (ClsGlobal.DebugLog == 1)
            {
                 Degree = ClsMessageDegree.DEBUG; 
            }
            else
            {
                Degree = ClsMessageDegree.INFO;
            }
            ///ClsMessageDegree.DEBUG  所有等级存储
            ///ClsMessageDegree.INFO  除DEBUG外，都存储
            ///ClsMessageDegree.WARN  除DEBUG和INFO外，都存储
            ///ClsMessageDegree.ERROR  只存储ERROR和FATAL
            ///ClsMessageDegree.FATAL  只存储FATAL
            ///ClsMessageDegree.None  不存储任何等级
            MESlogNet  = new LogNetDateTime(Application.StartupPath + "\\Logs\\MESlog", GenerateMode.ByEveryDay);
            MESlogNet.SetMessageDegree(Degree);

            PLClogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\PLClog", GenerateMode.ByEveryDay);
            PLClogNet.SetMessageDegree(Degree);

            OCVInfologNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\OCV\\OCVInflog", GenerateMode.ByEveryDay);
            OCVInfologNet.SetMessageDegree(Degree);

            OCVTestlogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\OCV\\OCVTestlog", GenerateMode.ByEveryDay);
            OCVTestlogNet.SetMessageDegree(Degree);

            SendDatalogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\SendDatalog", GenerateMode.ByEveryDay);
            SendDatalogNet.SetMessageDegree(Degree);

            SqllogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\Sqllog", GenerateMode.ByEveryDay);
            SqllogNet.SetMessageDegree(Degree);

            INIlogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\INIloglog", GenerateMode.ByEveryDay);
            INIlogNet.SetMessageDegree(Degree);

            ConfiglogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\Configlog", GenerateMode.ByEveryDay);
            ConfiglogNet.SetMessageDegree(Degree);

            UserinfologNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\UserInfolog", GenerateMode.ByEveryDay);
            UserinfologNet.SetMessageDegree(Degree);

            MesUpLoadStatus = new LogNetDateTime(Application.StartupPath + "\\Logs\\MesUpLoadStatuslog", GenerateMode.ByEveryDay);
            MesUpLoadStatus.SetMessageDegree(Degree);

            WCSlogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\WCSlogNet", GenerateMode.ByEveryDay);
            WCSlogNet.SetMessageDegree(Degree);

        }

        public static void LogNetINI()
        {
            INIlogNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\INIloglog", GenerateMode.ByEveryDay);
            INIlogNet.SetMessageDegree(ClsMessageDegree.DEBUG);
        }
    }
}
