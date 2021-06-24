using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Text.RegularExpressions;

using System.Net;
using System.IO;

using Newtonsoft.Json;
using SZB.DLL;
using OCV.OCVLogs;

namespace OCV
{
    public class ClsMes
    {
        #region 定义

        //构造
        public ClsMes()
        {

        }

        #region Mes 3.0内容

        public static DataAbstr DataAb = DataAbstr.GetInstance();  //实例化   20191010

        public static string resource_id = SZB.DLL.Comm.Constant.RS_NO;  //资源号   20191010


        #region 电池登录信息

        //上传设备状态发送信息
        public class ClsMesSendUploadData_S
        {
            public string cz_date;    //时间
            public string status;     //设备状态（A:工作，B:待机，C:故障，D:关机）
            public ClsMesSendData data;       //参数值
        }

        //上传设备报警发送信息
        public class ClsMesSendUploadData_W
        {
            public string cz_date;    //时间
            public ClsMesSendData data;       //参数值
        }

        public class ClsMesSendData
        {
            public string DATA12;   //代码
            public string DATA13;   //描述
        }

        //员工登录结果返回
        public class ClsMesRecvUserLogin
        {
            public string result;    //返回结果OK/NG
            public string message;   //信息描述
            public string UserName;   //员工姓名
        }
        //获取来料信息返回
        public class ClsMesRecvGetData
        {
            public string result;    //返回结果OK/NG
            public string message;   //信息描述
            public string shop_order;   //工单
            public string tech_no;   //型号
        }
        //上传过程数据结果返回
        public class ClsMesRecvUploadData_P
        {
            public string result;    //返回结果OK/NG
            public string message;   //信息描述
        }
        
        //托盘绑定电池类型结果返回
        public class ClsMesRecvGetBasket
        {
            public string Result;    //返回结果OK/NG
            public string Message;   //信息描述
            public string Tech_no;   //电池类型集
        }

        //上传设备状态结果返回
        public class ClsMesRecvUploadData_S
        {
            public string result;    //返回结果OK/NG
            public string message;   //信息描述
        }

        //上传设备报警信息结果返回
        public class ClsMesRecvUploadData_W
        {
            public string result;    //返回结果OK/NG
            public string message;   //信息描述
        }
        
        #endregion

        #endregion
        #endregion

        #region 执行Mes交互
        #region mes 3.0功能

        #region 员工资质验证
        /// <summary>
        /// 员工资质验证
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public ClsMesRecvUserLogin MesUserLogin(string jsonString)
        {
            ClsMesRecvUserLogin rtn = new ClsMesRecvUserLogin();
            //Encoding encoding = Encoding.GetEncoding("utf-8");
            try
            {
                string ReceiveStr = DataAb.UserLogin(jsonString);
                if (ReceiveStr != "")
                {
                    rtn = JsonConvert.DeserializeObject<ClsMesRecvUserLogin>(ReceiveStr);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return rtn;
                throw new Exception(string.Format("执行MesUserLogin失败:{0}", rtn) + ex.Message);
            }
        }
        #endregion

        #region 获取来料信息
        /// <summary>
        /// 员工资质验证查询
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public ClsMesRecvGetData MesGetData(string jsonString)
        {
            ClsMesRecvGetData rtn = new ClsMesRecvGetData();
            //Encoding encoding = Encoding.GetEncoding("utf-8");
            try
            {
                string ReceiveStr = DataAb.GetData(jsonString);
                if (ReceiveStr != "")
                {
                    rtn = JsonConvert.DeserializeObject<ClsMesRecvGetData>(ReceiveStr);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return rtn;
                throw new Exception(string.Format("执行MesGetData失败:{0}", rtn) + ex.Message);
            }
        }
        #endregion


        #region 托盘绑定电池类型
        /// <summary>
        /// 托盘绑定电池类型
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public ClsMesRecvGetBasket MesGetBasket(string jsonString)
        {
            ClsMesRecvGetBasket rtn = new ClsMesRecvGetBasket();
            //Encoding encoding = Encoding.GetEncoding("utf-8");
            try
            {
                string ReceiveStr = DataAb.GetBasket(jsonString);
                if (ReceiveStr != "")
                {
                    rtn = JsonConvert.DeserializeObject<ClsMesRecvGetBasket>(ReceiveStr);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return rtn;
                throw new Exception(string.Format("执行MesGetBasket失败:{0}", rtn) + ex.Message);
            }
        }
        #endregion

        #region 上传设备状态
        /// <summary>
        /// 上传设备状态
        /// </summary>
        /// <param name="sendStatus">发送信息</param>
        /// <returns></returns>
        public ClsMesRecvUploadData_S MesSendStatus(ClsMesSendUploadData_S sendStatus)
        {
            ClsMesRecvUploadData_S rtn = new ClsMesRecvUploadData_S();
            try
            {
                string data = JsonConvert.SerializeObject(sendStatus.data);
                string ReceiveStr = DataAb.UploadData_S(resource_id, sendStatus.cz_date, sendStatus.status, data);
                if (ReceiveStr != "")
                {
                    rtn = JsonConvert.DeserializeObject<ClsMesRecvUploadData_S>(ReceiveStr);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return rtn;
                throw new Exception(string.Format("执行MesSendStatus失败:{0}", rtn) + ex.Message);
            }
        }
        #endregion

      
        public void MesUpLoadStatus(string status,string mStatusCode,string mStatusDesc)
        {
            string message = "";
            try
            {
              
                string statusCode = mStatusCode.Trim().ToUpper();
                string statusDesc = mStatusDesc.Trim().ToUpper();
                ClsMesSendUploadData_S sendS = new ClsMesSendUploadData_S();
                ClsMesSendData sd = new ClsMesSendData();
                sd.DATA12 = statusCode;
                sd.DATA13 = statusDesc;
                sendS.data = sd;
                sendS.cz_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sendS.status = status.Trim().ToUpper();
                ClsMes.ClsMesRecvUploadData_S uploadStatus = MesSendStatus(sendS);
                if (uploadStatus.result != "OK")
                {
                    if (uploadStatus.result == null && uploadStatus.message == null)
                    {
                        message = String.Format("MES上传设备状态：{0}{1}返回结果和信息描述为空，请检查MES通信是否正常！", statusCode, statusDesc);
                    }
                    else
                    {
                        message = String.Format("MES上传设备状态：{0}{1}失败：{2}，信息描述：{3}", sd.DATA12, sd.DATA13, uploadStatus.result, uploadStatus.message);
                    }
                }
                else
                {
                    message = String.Format("MES上传设备状态：{0}{1}成功：{2}，信息描述：{3}", sd.DATA12, sd.DATA13, uploadStatus.result, uploadStatus.message);
                }
                ClsLogs.MesUpLoadStatus.WriteInfo(message);
            }
            catch (Exception ex)
            {
                message = "MES上传设备状态出错：" + ex.Message;
                ClsLogs.MesUpLoadStatus.WriteInfo(message);
            }
        }


        #region 上传设备报警信息
        /// <summary>
        /// 上传设备报警信息
        /// </summary>
        /// <param name="sendStatus">发送信息</param>
        /// <returns></returns>
        public ClsMesRecvUploadData_W MesSendWarning(ClsMesSendUploadData_W sendStatus)
        {
            ClsMesRecvUploadData_W rtn = new ClsMesRecvUploadData_W();
            try
            {
                string data = JsonConvert.SerializeObject(sendStatus.data);
                string ReceiveStr = DataAb.UploadData_W(resource_id, sendStatus.cz_date, data);
                if (ReceiveStr != "")
                {
                    rtn = JsonConvert.DeserializeObject<ClsMesRecvUploadData_W>(ReceiveStr);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return rtn;
                throw new Exception(string.Format("执行MesSendWarning失败:{0}", rtn) + ex.Message);
            }
        }

        public void MesUpLoadWarning(string mWarningCode, string mWarningDesc)
        {
            string message = "";
            try
            {
                ClsMes mes = new ClsMes();
                string warningCode = mWarningCode.Trim().ToUpper();
                string warningDesc = mWarningDesc;
                ClsMes.ClsMesSendUploadData_W sendW = new ClsMes.ClsMesSendUploadData_W();
                ClsMes.ClsMesSendData sd = new ClsMes.ClsMesSendData();
                sd.DATA12 = warningCode;
                sd.DATA13 = warningDesc;
                sendW.data = sd;
                sendW.cz_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ClsMes.ClsMesRecvUploadData_W uploadWarning = mes.MesSendWarning(sendW);
                if (uploadWarning.result != "OK")
                {
                    if (uploadWarning.result == null && uploadWarning.message == null)
                    {
                        message = String.Format("MES上传设备报警信息：{0}{1}返回结果和信息描述为空，请检查MES通信是否正常！", warningCode, warningDesc);
                    }
                    else
                    {
                        message = String.Format("MES上传设备报警信息：{0}{1}失败：{2}，信息描述：{3}", sd.DATA12, sd.DATA13, uploadWarning.result, uploadWarning.message);
                    }
                }
                else
                {
                    message = String.Format("MES上传设备报警信息：{0}{1}成功：{2}，信息描述：{3}", sd.DATA12, sd.DATA13, uploadWarning.result, uploadWarning.message);
                }
                ClsLogs.MesUpLoadStatus.WriteInfo(message);
            }
            catch (Exception ex)
            {
                message = "MES上传设备报警信息出错：" + ex.Message;
                ClsLogs.MesUpLoadStatus.WriteInfo(message);
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
