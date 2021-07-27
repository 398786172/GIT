using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OCV.OCVTest
{
    public class ClsCommWithMES
    {

        public HttpResult2 CheckTrayNoExist(string trayNo, string type, string projectName)
        {
            HttpResult2 result = null;
            var apiServer = System.Configuration.ConfigurationManager.AppSettings["CheckTrayAPI"];
            string url = $"{apiServer}/api/ocv/validateOcv?jsonData=";
            TrayCodeCheck tryCode = new TrayCodeCheck() { trayNo = trayNo, type = type, projectName = projectName, checkFifo = ClsSysSetting.SysSetting.CheckFIFO?"Y":"N" ,isFct = ClsGlobal.ISFCT?"Y":"N"};
            url = $"{url}{JsonConvert.SerializeObject(tryCode)}";
            string strHttpResult = HttpGet(url);
            if (string.IsNullOrEmpty(strHttpResult))
            {
                throw new Exception($"http请求:{url},返回值为空");
            }
            try
            {
                result = JsonConvert.DeserializeObject<HttpResult2>(strHttpResult);
            }
            catch
            {
                throw new Exception($"http请求:{url},返回值为:{strHttpResult},解析错误");
            }
            return result;
        }


        public HttpResult1 GetBattyCodesByTrayCode(string trayNo)
        {
            HttpResult1 result = null;

           var apiServer = System.Configuration.ConfigurationManager.AppSettings["GetBatCodeAPI"];
            string url = $"{apiServer}/api/ocv/cabinetInfoOcv?jsonData=";
            TrayCode tryCode = new TrayCode() { trayNo = trayNo };
            url = $"{url}{JsonConvert.SerializeObject(tryCode)}";
            string strHttpResult = HttpGet(url);
            if (string.IsNullOrEmpty(strHttpResult))
            {
                throw new Exception($"http请求:{url},返回值为空");
            }
            try
            {
                result = JsonConvert.DeserializeObject<HttpResult1>(strHttpResult);
            }
            catch
            {
                throw new Exception($"http请求:{url},返回值为:{strHttpResult},解析错误");
            }
            return result;
        }


        string mediaType = "application/json";
        public string HttpPost(string url, object data)
        {
            string result = "";
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 2);
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, mediaType);
            var httpResult = httpClient.PostAsync(url, content).Result;
            if (httpResult.IsSuccessStatusCode)
            {
                result = httpResult.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception($"http请求失败,请求代码:{httpResult.StatusCode}");
            }
            return result;
        }

        public string HttpGet(string url)
        {
            string result = "";
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 2);
            var httpResult = httpClient.GetAsync(url).Result;
            if (httpResult.IsSuccessStatusCode)
            {
                result = httpResult.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception($"http请求失败,请求代码:{httpResult.StatusCode}");
            }
            return result;
        }
    }

    public class HttpResult1
    {
        public bool status { get; set; }
        public string errMessage { get; set; }
        public List<BattyCode> result { get; set; }
    }

    public class HttpResult2
    {
        public bool status { get; set; }
        public string errMessage { get; set; }
        public string dateTime { get; set; }
        public string stepNo { get; set; }
    }

    public class TrayCode
    {
        public string trayNo { get; set; }
    }

    public class TrayCodeCheck
    {
        /// <summary>
        /// 托盘号
        /// </summary>
        public string trayNo { get; set; }
        /// <summary>
        /// 测试类型-固定值:OCV1.OCV2 二选一 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string projectName { get; set; }
        /// <summary>
        /// 是否校验FIFO出入站
        /// </summary>
        public string checkFifo { get; set; }
        /// <summary>
        /// 是否fct物料
        /// </summary>
        public string isFct { get; set; }
    }


    /// <summary>
    /// 电池码信息
    /// </summary>
    public class BattyCode
    {
        /// <summary>
        /// 电芯条码	
        /// </summary>
        public string SFC_NO { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CREATE_USER { get; set; }
        /// <summary>
        /// 是否当前
        /// </summary>
        public string IS_CURRENT { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CREATE_DATE { get; set; }
        /// <summary>
        /// 电芯状态
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 托盘编号	
        /// </summary>
        public string TRAY_NO { get; set; }
        /// <summary>
        /// 卡位号
        /// </summary>
        public int TARY_NUMBER { get; set; }

    }
}
