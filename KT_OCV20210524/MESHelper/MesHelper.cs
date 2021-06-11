using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OCV.OCVLogs;
using Qwmind_TestReference.Model.Step;
using Qwmind_TestReference;
using static OCV.ClsWCSCOM;

namespace OCV.MESHelper
{
    public class MesHelper
    {
        #region//变量定义
        // private string url = "";
        private ResultStackedMaterialCheck resultStackedMaterialCheck = null;

        #endregion

        #region//构造函数

        // public MesHelper(string url)
        // {
        //     this.url = url;
        // }

        #endregion


        /// <summary>  
        /// POST请求OCV入栈校验与获取结果  
        /// </summary>  
        public ResultStackedMaterialCheck HttpPostStackedMaterialCheck(string url, StackedMaterialCheck postData)
        {
            string retString = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url ?? "http://127.0.0.1:8080/IEAM/userManagement");
            request.Method = "POST";
            string Data = JsonConvert.SerializeObject(postData);
            request.ContentType = "application/json";
            request.ContentLength = Data.Length;
            try
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(Data);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.Write("发送数据失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("发送数据失败!" + ex.ToString());
                return null;
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.Write("获取响应失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("获取响应失败!" + ex.ToString());
                return null;
            }
            resultStackedMaterialCheck = JsonConvert.DeserializeObject<ResultStackedMaterialCheck>(retString);
            return resultStackedMaterialCheck;
        }


        /// <summary>  
        /// POST请求OCV结果上传 
        /// </summary>  
        public ResultOCVResultUpLoad HttpPostOCVResultUpLoad(string url, OCVResultUpLoad postData)
        {
            string retString = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url ?? "http://127.0.0.1:8080/IEAM/userManagement/loginAPP");
            request.Method = "POST";
            string Data = JsonConvert.SerializeObject(postData);
            request.ContentType = "application/json";
            request.ContentLength = Data.Length;
            try
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(Data);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.Write("发送数据失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("发送数据失败!" + ex.ToString());
                return null;
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.Write("获取响应失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("获取响应失败!" + ex.ToString());
                return null;
            }
            return JsonConvert.DeserializeObject<ResultOCVResultUpLoad>(retString);
        }

        /// <summary>  
        /// POST请求OCV拆盘校验  
        /// </summary>  
        public ResultOCVTrayRemoveConfirm HttpPostOCVTrayRemoveConfirm(string url, OCVTrayRemoveConfirm postData)
        {
            string retString = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url ?? "http://127.0.0.1:8080/IEAM/userManagement/loginAPP");
            request.Method = "POST";
            string Data = JsonConvert.SerializeObject(postData);
            request.ContentType = "application/json";
            request.ContentLength = Data.Length;
            try
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(Data);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.Write("发送数据失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("发送数据失败!" + ex.ToString());
                return null;
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.Write("获取响应失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("获取响应失败!" + ex.ToString());
                return null;
            }
            return JsonConvert.DeserializeObject<ResultOCVTrayRemoveConfirm>(retString);
        }

        /// <summary>  
        /// POST请求OCV托盘解绑信息上传 
        /// </summary>  
        public ResultTrayUnBindMessageUpLoad HttpPostOCVTrayUnBindMessageUpLoad(string url, OCVTrayUnBindMessageUpLoad postData)
        {
            string retString = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url ?? "http://127.0.0.1:8080/IEAM/userManagement/loginAPP");
            request.Method = "POST";
            string Data = JsonConvert.SerializeObject(postData);
            request.ContentType = "application/json";
            request.ContentLength = Data.Length;
            try
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(Data);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.Write("发送数据失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("发送数据失败!" + ex.ToString());
                return null;
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.Write("获取响应失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("获取响应失败!" + ex.ToString());
                return null;
            }
            return JsonConvert.DeserializeObject<ResultTrayUnBindMessageUpLoad>(retString);
        }



        public T2 HttpPostm<T1, T2>(string url, T1 postData)
        {
            string retString = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url ?? "http://127.0.0.1:8080/IEAM/userManagement/loginAPP");
            request.Method = "POST";
            string Data = JsonConvert.SerializeObject(postData);
            request.ContentType = "application/json";
            request.ContentLength = Data.Length;
            try
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(Data);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.Write("发送数据失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("发送数据失败!" + ex.ToString());
                return default(T2);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                retString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.Write("获取响应失败!" + ex.ToString());
                ClsLogs.MESlogNet.WriteError("获取响应失败!" + ex.ToString());
                return default(T2);
            }
            return JsonConvert.DeserializeObject<T2>(retString);
        }

        public int Get_PermissionFormMES(string TrayCode, String process_id)
        {
            try
            {
                StackedMaterialCheck stackMaterialCheck = new StackedMaterialCheck();
                stackMaterialCheck.equipment_id = ClsGlobal.DeviceCode;
                stackMaterialCheck.process_id = process_id;
                stackMaterialCheck.traycode = TrayCode;
                ResultStackedMaterialCheck ReceivedResult = HttpPostStackedMaterialCheck("http://127.0.0.1:8080/IEAM/userManagement", stackMaterialCheck);
                int OCVtype = 0;
                if (ReceivedResult.message == "请求成功")
                {
                    if (!(ReceivedResult == null))
                    {
                        var _JsonText = ReceivedResult.procedure;
                        ClsLogs.MESlogNet.WriteInfo("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息为：" + _JsonText);
                        if (_JsonText == "01")
                        {
                            OCVtype = 1;
                        }
                        else if (_JsonText == "02")
                        {
                            OCVtype = 2;
                        }
                        else if (_JsonText == "03")
                        {
                            OCVtype = 3;
                        }
                        else
                        {
                            throw new Exception("MES系统返回托盘工序信息异常，返回值为：" + _JsonText);
                        }
                        return OCVtype;
                    }
                    else
                    {
                        ClsLogs.MESlogNet.WriteWarn("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息为：空");
                        throw new Exception("MES系统返回托盘工序data数据为空");
                    }
                }
                else
                {
                    ClsLogs.MESlogNet.WriteWarn("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息异常：" + "异常信息为" + ReceivedResult.message);
                    throw new Exception("异常信息为" + ReceivedResult.message);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<ET_CELL> Get_BattInfoFormMES(string TrayCode)
        {
            List<ET_CELL> resultlist = new List<ET_CELL>();
            try
            {
                if (resultStackedMaterialCheck != null)
                {
                    ClsLogs.MESlogNet.WriteInfo("获取托盘电池信息", "获取到托盘" + TrayCode + "电池信息为：" + JsonConvert.SerializeObject(resultStackedMaterialCheck.data));
                    for (int i = 0; i < resultStackedMaterialCheck.data.Count; i++)
                    {
                        resultlist.Add(new ET_CELL()
                        {
                            Cell_ID = resultStackedMaterialCheck.data[i].bar_code,
                            Cell_Position = i + 1,// resultStackedMaterialCheck.data[i].location,
                            Pallet_ID = resultStackedMaterialCheck.traycode,
                            MODEL_NO= resultStackedMaterialCheck.data[i].marking_memo,
                        }
                        );
                    }
                    if (resultlist != null)
                    {
                        return resultlist;
                    }
                    else
                    {
                        ClsLogs.MESlogNet.WriteWarn("获取托盘电池信息", "获取到托盘" + TrayCode + "电池信息Data为空");
                        throw new Exception("MES系统返回电池信息data数据为空");
                    }
                }
                else
                {
                    StackedMaterialCheck stackMaterialCheck = new StackedMaterialCheck();
                    stackMaterialCheck.equipment_id = ClsGlobal.DeviceCode;
                    stackMaterialCheck.process_id = ClsGlobal.process_id;
                    stackMaterialCheck.traycode = TrayCode;
                    resultStackedMaterialCheck = HttpPostStackedMaterialCheck("http://127.0.0.1:8080/IEAM/userManagement", stackMaterialCheck);
                    for (int i = 0; i < resultStackedMaterialCheck.data.Count; i++)
                    {

                        resultlist.Add(new ET_CELL()
                        {
                            Cell_ID = resultStackedMaterialCheck.data[i].bar_code,
                            Cell_Position = i + 1,// resultStackedMaterialCheck.data[i].location, 
                            Pallet_ID = resultStackedMaterialCheck.traycode,
                            MODEL_NO = resultStackedMaterialCheck.data[i].marking_memo,
                        }
                        );
                    }
                    if (resultlist != null)
                    {
                        return resultlist;
                    }
                    else
                    {
                        ClsLogs.MESlogNet.WriteWarn("获取托盘电池信息", "获取到托盘" + TrayCode + "电池信息Data为空");
                        throw new Exception("MES系统返回电池信息data数据为空");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}




