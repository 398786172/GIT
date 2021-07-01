using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Qwmind_TestReference;

using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using Qwmind_TestReference.Model.Step;
using OCV.OCVLogs;

namespace OCV
{
    //MES接口
    public class ClsWCSCOM
    {
        //string mUrl = "http://10.5.80.52:8080/mes/service";                       //地址
        string mUrl = "http://localhost:61063/WebService1.asmx/service";            //地址
        string method;                                                              //方法
        string param;                                                               //参数  
        JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
        Qwmind_TestReference_DLL WCS = new Qwmind_TestReference_DLL();
        private static readonly ClsWCSCOM _wcsInstance = new ClsWCSCOM();
        private ClsWCSCOM()
        {


        }

        public ClsWCSCOM(string url)
        {
            mUrl = url;
        }

        public static ClsWCSCOM Instance
        {
            get { return _wcsInstance; }
        }

        public qwmind_ReciveResult StepTrayStaionChange(LocStatusType status, string TrayCode)
        {
            step_tray_staion_change staion = new step_tray_staion_change();
            staion.messageId = "step.tray.staion.change";
            staion.locStatus = ((int)status).ToString();
            staion.stageLoc = "001-001-00" + ClsGlobal.JT_NO;
            staion.trayCode = TrayCode;
            qwmind_ReciveResult qwmind_result = WCS.Qwmind_StepTrayStationChange(staion);
            return qwmind_result;

        }


        /// <summary>
        /// 电池信息: 从wcs获取
        /// </summary>
        /// <param name="TrayCode">托盘条码</param>
        /// <param name="listCell">电池信息列表</param>
        /// <returns></returns>
        public int Get_BattInfoFormWCS(string TrayCode, out List<ET_CELL> listCell)
        {
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            try
            {
                process_get_cellno get_cell_no = new process_get_cellno();
                get_cell_no.messageId = "process.get.cellno";
                get_cell_no.site = ClsGlobal.SITE;
                get_cell_no.device = ClsGlobal.DeviceCode;
                get_cell_no.lineId = ClsGlobal.DeviceNo;
                get_cell_no.operation = ClsGlobal.OPEATION_ID; ;
                get_cell_no.trayCode = TrayCode;
                qwmind_ReciveResult ReciveResult = WCS.Qwmind_ProcessGetCellno(get_cell_no);
                if (ReciveResult.code == 200)
                {
                    List<clsCellData> lstCellChnoJson = new List<clsCellData>(); ;
                    if (ReciveResult.data != null)
                    {
                        ClsLogs.WCSlogNet.WriteInfo("获取托盘电池信息", "获取到托盘" + TrayCode + "电池信息为：" + ReciveResult.data.ToString());
                        lstCellChnoJson = JSSerializer.Deserialize<List<clsCellData>>(ReciveResult.data.ToString());
                        // ClsLogs.SqllogNet.WriteWarn("66666");
                        //按38通道获取,空条码为nullCell开头?
                        ReflectBatInfoToETCell(TrayCode, lstCellChnoJson, out list);
                        //  ClsLogs.SqllogNet.WriteWarn("777777");
                        listCell = list;
                        return 0;
                    }
                    else
                    {
                        ClsLogs.WCSlogNet.WriteWarn("获取托盘电池信息", "获取到托盘" + TrayCode + "电池信息Data为空");
                        throw new Exception("物流系统返回电池信息data数据为空");
                    }
                }
                else
                {
                    ClsLogs.WCSlogNet.WriteWarn("获取托盘电池信息", "获取到托盘" + TrayCode + "电池信息异常：" + "物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
                    throw new Exception("物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //电池信息: 得到的lstCellData电池信息转换到ET_CELL
        private void ReflectBatInfoToETCell(string TrayCode, List<clsCellData> lstCellData, out List<ET_CELL> listCell)
        {
            int channelCount = lstCellData.Count;
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            int Chk;
            try
            {
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    ET_CELL etCell = new ET_CELL();
                    Chk = 0;
                    for (int j = 0; j < channelCount; j++)
                    {
                        if (lstCellData[j].CHNO == i + 1)
                        {
                            string CellCode = "";
                            if (ClsGlobal.CodeRegexCheck(lstCellData[j].CELLNO, ClsGlobal.CellCodeRegEx) == false)
                            {
                                CellCode = "error";
                            }
                            else
                            {
                                CellCode = lstCellData[j].CELLNO;
                            }
                            etCell.Cell_ID = CellCode;
                            etCell.Pallet_ID = TrayCode;
                            etCell.Cell_Position = lstCellData[j].CHNO;
                            for (int n = 0; n < ClsGlobal.lstBatSet.Count; n++)
                            {
                                try
                                {
                                    etCell.MODEL_NO = "GY";//lstCellData[j].CELLNO.Substring(ClsGlobal.lstBatSet[n].P_KeyStart - 1, ClsGlobal.lstBatSet[n].P_ModelLenth);
                                }
                                catch (Exception)
                                {
                                    etCell.MODEL_NO = "";
                                }
                            }
                            list.Add(etCell);
                            Chk = 1;
                            break;
                        }
                    }
                    if (Chk == 0)
                    {
                        etCell.Cell_ID = "NullCellCode_" + (i + 1);   //空电池条码
                        etCell.Pallet_ID = TrayCode;
                        etCell.Cell_Position = (i + 1);
                        list.Add(etCell);
                    }
                }
                listCell = list;


            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 校验工步dll接口方法
        /// </summary>
        /// <param name="tray">托盘类</param>
        /// <param name="operation">工站</param>
        /// <returns></returns>
        public qwmind_ReciveResult StepCheckin(string trayCode, string operation)
        {
            step_checkin checkin = new step_checkin();
            checkin.messageId = "step.checkin";
            checkin.trayCode = trayCode;
            checkin.operation = operation.ToUpper();
            qwmind_ReciveResult qwmind_result = WCS.Qwmind_StepCheckIn(checkin);
            return qwmind_result;
        }

        /// <summary>
        /// CheckOut dll接口方法
        /// </summary>
        /// <param name="deviceStation">工站</param>
        /// <param name="lineId">线体编号</param>
        /// <param name="trayNo">托盘号</param>
        /// <returns></returns>
        public qwmind_ReciveResult StepCheckOut(string operation, string trayNo)
        {
            step_checkout checkout = new step_checkout();
            checkout.messageId = "step.checkout";
            checkout.operation = operation;
            checkout.trayCode = trayNo;
            qwmind_ReciveResult qwmind_result = WCS.Qwmind_StepCheckOut(checkout);
            return qwmind_result;
        }

        /// <summary>
        /// 更改工步dll接口方法
        /// </summary>
        /// <param name="tray">托盘</param>
        /// <param name="isForce">0：是，1：否。正常流程应为1，若不想判断是否可以更改为0直接强制更改，强制更改时若上一工步未结束，则自动结束再更改</param>
        /// <param name="operation">工站</param>
        /// <returns></returns>
        public qwmind_ReciveResult StepChange(string trayCode, string isForce, string operation)
        {
            step_change_force change = new step_change_force();
            change.messageId = "step.change.force";
            change.trayCode = trayCode;
            change.operation = operation;
            change.lineId = "1";
            qwmind_ReciveResult qwmind_result = WCS.Qwmind_StepChangeForce(change);
            return qwmind_result;
        }




        /// <summary>
        /// 获取托盘工序
        /// </summary>
        /// <param name="TrayCode"></param>
        /// <returns> OCV工序</returns>
        public int Get_NowStepFormWCS(string TrayCode, out string NowStep)
        {
            try
            {

                step_get_nextstep get_cell_no = new step_get_nextstep();
                get_cell_no.messageId = "step.get.nextstep";
                get_cell_no.site = ClsGlobal.SITE;
                get_cell_no.device = ClsGlobal.DeviceCode;
                get_cell_no.lineId = ClsGlobal.DeviceNo;
                get_cell_no.operation = ClsGlobal.OPEATION_ID; ;
                get_cell_no.trayCode = TrayCode;
                qwmind_ReciveResult ReciveResult = WCS.Qwmind_StepGetNextStep(get_cell_no);
                int OCVtype = 0;
                if (ReciveResult.code == 200 || ReciveResult.code == 300)
                {

                    List<clsCellData> lstCellChnoJson = new List<clsCellData>(); ;
                    if (ReciveResult.data != null)
                    {
                        string _JsonText = ReciveResult.data.ToString();
                        ClsLogs.WCSlogNet.WriteInfo("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息为：" + _JsonText);
                        NowStep = _JsonText;

                        var workStatus = StepTrayStaionChange(LocStatusType.Working, TrayCode);
                        if (workStatus.code != 200)
                        {
                            throw new Exception("物流系统 StepTrayStaionChange方法返回异常，返回值为：" + workStatus.msg);
                        }
                        if (_JsonText == "OCV1")
                        {
                            OCVtype = 1;
                        }
                        else if (_JsonText == "OCV2")
                        {
                            OCVtype = 2;
                        }
                        else if (_JsonText == "OCV3")
                        {
                            OCVtype = 3;
                        }
                        else
                        {
                            throw new Exception("物流系统返回托盘工序信息异常，返回值为：" + _JsonText);
                        }
                        return OCVtype;
                    }
                    else
                    {
                        ClsLogs.WCSlogNet.WriteWarn("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息为：空");
                        throw new Exception("物流系统返回托盘工序data数据为空");
                    }
                }
                else
                {
                    ClsLogs.WCSlogNet.WriteWarn("获取托盘工序", "获取到托盘" + TrayCode + "当前工步信息异常：" + "物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
                    throw new Exception("物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        //向wcs上传ng结果
        public bool UpLoadngData(string TrayCode, List<ET_CELL> listCell)
        {
            try
            {
                step_data_upload_ng data_upload_ng = new step_data_upload_ng();
                data_upload_ng.messageId = "step.data.upload.ng";
                data_upload_ng.site = ClsGlobal.SITE;
                data_upload_ng.device = ClsGlobal.DeviceCode;
                data_upload_ng.lineId = ClsGlobal.DeviceNo;
                data_upload_ng.operation = ClsGlobal.OPEATION_ID;
                data_upload_ng.trayCode = TrayCode;

                for (int i = 0; i < listCell.Count; i++)
                {
                    int ngflag = listCell[i].NgState == "NG" ? 1 : 0;
                    CellChnoJson celldata = new CellChnoJson(listCell[i].Cell_ID, listCell[i].Cell_Position, ngflag);

                    data_upload_ng.cellChnoJson.Add(celldata);
                }
                qwmind_ReciveResult ReciveResult = WCS.Qwmind_StepDataUploadNG(data_upload_ng);
                if (ReciveResult.code == 200)
                {
                    ClsLogs.WCSlogNet.WriteInfo("上传NG数据", "上传托盘" + TrayCode + "NG数据成功：" + "物流系统返回信息为：" + ReciveResult.msg);

                    return true;
                }
                else
                {
                    ClsLogs.WCSlogNet.WriteWarn("上传NG数据", "上传托盘" + TrayCode + "NG数据异常：" + "物流系统返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
                    throw new Exception("上传NG数据失败返回代码为：" + ReciveResult.code + "异常信息为" + ReciveResult.msg);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //返回电池条码数据格式
        public class clsCellData
        {
            public int CHNO;
            public string CELLNO;
        }
    }

    public enum LocStatusType
    {
        Free = 0,
        Working = 1,
        Disable = 3,
    }
}