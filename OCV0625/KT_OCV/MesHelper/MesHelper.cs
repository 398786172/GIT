using Newtonsoft.Json;
using OCV.OCVLogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Qwmind_TestReference;

namespace OCV.MesHelper
{
    public class MesHelper
    {
        public T2 HttpPost<T1, T2>(string url, T1 postData)
        {
            string returnString = null;
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create(url ?? "http://127.0.0.1:8080/IEAM/userManagement/loginAPP");
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
                ClsLogs.MESlogNet.WriteError("发送MES数据失败!" + ex.ToString() + "原数据为:" + $"{Data}");
                return default(T2);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                returnString = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                ClsLogs.MESlogNet.WriteError("获取MES响应数据失败!" + ex.ToString() + "返回数据为:" + $"{returnString}");
                return default(T2);
            }

            return JsonConvert.DeserializeObject<T2>(returnString);
        }




        /// <summary>
        /// 获取托盘工序
        /// </summary>
        /// <param name="TrayCode"></param>
        /// <returns> OCV工序</returns>
        //public int Get_NowStepFormMES(string TrayCode, out string NowStep)
        //{
        //    try
        //    {

        //        step_get_nextstep get_cell_no = new step_get_nextstep();
        //        get_cell_no.messageId = "step.get.nextstep";
        //        get_cell_no.site = ClsGlobal.SITE;
        //        get_cell_no.device = ClsGlobal.DeviceCode;
        //        get_cell_no.lineId = ClsGlobal.DeviceNo;
        //        get_cell_no.operation = ClsGlobal.OPEATION_ID;
        //        get_cell_no.trayCode = TrayCode;
        //        qwmind_ReciveResult ReciveResult = WCS.Qwmind_StepGetNextStep(get_cell_no);
        //        int OCVtype = 0;
        //        if (ReciveResult.code == 200 || ReciveResult.code == 300)
        //        {

        //            List<ClsWCSCOM.clsCellData> lstCellChnoJson = new List<ClsWCSCOM.clsCellData>();
        //            ;
        //            if (ReciveResult.data != null)
        //            {
        //                string _JsonText = ReciveResult.data.ToString();
        //                ClsLogs.WCSlogNet.WriteInfo("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息为：" + _JsonText);
        //                NowStep = _JsonText;

        //                var workStatus = StepTrayStaionChange(LocStatusType.Working, TrayCode);
        //                if (workStatus.code != 200)
        //                {
        //                    throw new Exception("物流系统 StepTrayStaionChange方法返回异常，返回值为：" + workStatus.msg);
        //                }

        //                if (_JsonText == "OCV1")
        //                {
        //                    OCVtype = 1;
        //                }
        //                else if (_JsonText == "OCV2")
        //                {
        //                    OCVtype = 2;
        //                }
        //                else if (_JsonText == "OCV3")
        //                {
        //                    OCVtype = 3;
        //                }
        //                else
        //                {
        //                    throw new Exception("物流系统返回托盘工序信息异常，返回值为：" + _JsonText);
        //                }

        //                return OCVtype;
        //            }
        //            else
        //            {
        //                ClsLogs.WCSlogNet.WriteWarn("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息为：空");
        //                throw new Exception("物流系统返回托盘工序data数据为空");
        //            }
        //        }
        //        else
        //        {
        //            ClsLogs.WCSlogNet.WriteWarn("获取托盘工序",
        //                "获取到托盘" + TrayCode + "当前工步信息异常：" + "物流系统返回代码为：" + ReciveResult.code + "异常信息为" +
        //                ReciveResult.msg);
        //            throw new Exception("物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}



        ////向wcs上传ng结果
        //public bool UpLoadngData(string TrayCode, List<ET_CELL> listCell)
        //{
        //    try
        //    {
        //        step_data_upload_ng data_upload_ng = new step_data_upload_ng();
        //        data_upload_ng.messageId = "step.data.upload.ng";
        //        data_upload_ng.site = ClsGlobal.SITE;
        //        data_upload_ng.device = ClsGlobal.DeviceCode;
        //        data_upload_ng.lineId = ClsGlobal.DeviceNo;
        //        data_upload_ng.operation = ClsGlobal.OPEATION_ID;
        //        data_upload_ng.trayCode = TrayCode;

        //        for (int i = 0; i < listCell.Count; i++)
        //        {
        //            int ngflag = listCell[i].NgState == "NG" ? 1 : 0;
        //            CellChnoJson celldata = new CellChnoJson(listCell[i].Cell_ID, listCell[i].Cell_Position, ngflag);

        //            data_upload_ng.cellChnoJson.Add(celldata);
        //        }

        //        qwmind_ReciveResult ReciveResult = WCS.Qwmind_StepDataUploadNG(data_upload_ng);
        //        if (ReciveResult.code == 200)
        //        {
        //            ClsLogs.WCSlogNet.WriteInfo("上传NG数据",
        //                "上传托盘" + TrayCode + "NG数据成功：" + "物流系统返回信息为：" + ReciveResult.msg);

        //            return true;
        //        }
        //        else
        //        {
        //            ClsLogs.WCSlogNet.WriteWarn("上传NG数据",
        //                "上传托盘" + TrayCode + "NG数据异常：" + "物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
        //            throw new Exception("上传NG数据失败返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        }
    }



    

