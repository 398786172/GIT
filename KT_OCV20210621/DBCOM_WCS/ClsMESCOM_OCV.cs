using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace OCV
{
    //MES通讯接口
    class ClsMESCOM_OCV
    {
        OCV.WSBindTrayReference.QWmind_BindTrayWS WSBindTrayClient;
        OCV.WSOCVReference.QWmind_OCVWS WSOCVClient;
        JavaScriptSerializer JSSerializer;

        public ClsMESCOM_OCV()
        {
            WSBindTrayClient = new OCV.WSBindTrayReference.QWmind_BindTrayWS();
            WSOCVClient = new OCV.WSOCVReference.QWmind_OCVWS();
            JSSerializer = new JavaScriptSerializer();
        }


        #region 获取托盘绑定电池信息

        /// <summary>
        /// 获取托盘绑定电池信息
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <param name="listCell">电池信息列表</param>
        /// <returns>
        /// success:0 
        /// 没有获取到电池信息:1
        /// 电池条码重复:2
        /// 电池位置号重复:3
        ///</returns>
        public int GetCellInfo(string Pallet_ID, out List<ET_CELL> listCell)
        {
            try
            {
                listCell = null;

                //获取bindtray数据
                if (Pallet_ID == "")
                {
                    return 4;
                }
                string TrayNo = Pallet_ID;
                string JSONData = WSBindTrayClient.SelectBatteryInfoByTrayNoFromRea(TrayNo);
                //JSONData = "[{\"BindRealinfo_id\":123,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0041RC\",\"BindRealinfo_ChNo\":1,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":124,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0075RC\",\"BindRealinfo_ChNo\":2,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":125,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0024RC\",\"BindRealinfo_ChNo\":3,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":126,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0042RC\",\"BindRealinfo_ChNo\":4,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":127,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0076RC\",\"BindRealinfo_ChNo\":5,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":128,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0025RC\",\"BindRealinfo_ChNo\":6,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":129,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0051RC\",\"BindRealinfo_ChNo\":7,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":130,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0068RC\",\"BindRealinfo_ChNo\":8,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":131,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0055RC\",\"BindRealinfo_ChNo\":9,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":132,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0072RC\",\"BindRealinfo_ChNo\":10,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":133,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0015RC\",\"BindRealinfo_ChNo\":11,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":134,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0026RC\",\"BindRealinfo_ChNo\":12,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":135,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0074RC\",\"BindRealinfo_ChNo\":13,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":136,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0066RC\",\"BindRealinfo_ChNo\":25,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":137,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0038RC\",\"BindRealinfo_ChNo\":26,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":138,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0021RC\",\"BindRealinfo_ChNo\":27,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":139,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0030RC\",\"BindRealinfo_ChNo\":28,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":140,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0036RC\",\"BindRealinfo_ChNo\":29,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":141,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0040RC\",\"BindRealinfo_ChNo\":30,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":142,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0062RC\",\"BindRealinfo_ChNo\":31,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":143,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0011RC\",\"BindRealinfo_ChNo\":32,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":144,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0007RC\",\"BindRealinfo_ChNo\":33,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":145,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0009RC\",\"BindRealinfo_ChNo\":34,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":146,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0057RC\",\"BindRealinfo_ChNo\":35,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":147,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0003RC\",\"BindRealinfo_ChNo\":36,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":148,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0060RC\",\"BindRealinfo_ChNo\":37,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":149,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0032RC\",\"BindRealinfo_ChNo\":49,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":150,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0044RC\",\"BindRealinfo_ChNo\":50,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":151,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0043RC\",\"BindRealinfo_ChNo\":51,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":152,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0014RC\",\"BindRealinfo_ChNo\":52,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":153,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0067RC\",\"BindRealinfo_ChNo\":53,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":154,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0059RC\",\"BindRealinfo_ChNo\":54,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":155,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0086RC\",\"BindRealinfo_ChNo\":55,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":156,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0053RC\",\"BindRealinfo_ChNo\":56,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":157,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0039RC\",\"BindRealinfo_ChNo\":57,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":158,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0020RC\",\"BindRealinfo_ChNo\":58,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":159,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0023RC\",\"BindRealinfo_ChNo\":59,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":160,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0058RC\",\"BindRealinfo_ChNo\":60,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"},{\"BindRealinfo_id\":161,\"BindRealinfo_TrayNo\":\"LQC020004\",\"BindRealinfo_CellNo\":\"A01CBKFDHT7063HTH31O0010RC\",\"BindRealinfo_ChNo\":61,\"BindRealinfo_BindDate\":\"2017-10-21T10:57:19\",\"BindRealinfo_BindName\":\"1\"}]";

                //转换到ClsBindRealinfo[]            
                ClsBindRealinfo[] arrBindRealinfo = JSSerializer.Deserialize<ClsBindRealinfo[]>(JSONData);

                //放入OCV测试结构
                List<ET_CELL> lstCell = new List<ET_CELL>();

                if (arrBindRealinfo.Count() == 0)
                {
                    return 1;
                }

                int Chk;
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    ET_CELL etCell = new ET_CELL();
                    Chk = 0;
                    for (int j = 0; j < arrBindRealinfo.Count(); j++)
                    {
                        if (arrBindRealinfo[j].BindRealinfo_ChNo.Trim() == (i + 1).ToString())
                        {
                            etCell.Cell_ID = arrBindRealinfo[j].BindRealinfo_CellNo;
                            etCell.Pallet_ID = arrBindRealinfo[j].BindRealinfo_TrayNo;
                            etCell.Cell_Position = Convert.ToInt16(arrBindRealinfo[j].BindRealinfo_ChNo);
                            lstCell.Add(etCell);
                            Chk = 1;
                            break;
                        }
                    }

                    if (Chk == 0)
                    {
                        etCell.Cell_ID = "NullCellCode_" + (i + 1);   //空电池条码
                        etCell.Pallet_ID = TrayNo.Trim();
                        etCell.Cell_Position = (i + 1);
                        lstCell.Add(etCell);
                    }
                }

                listCell = lstCell;
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }

        /// <summary>
        /// 获得托盘电池条码绑定数据,单机使用
        /// </summary>
        /// <param name="Pallet_ID">托盘号</param>
        /// <param name="listCell">电池信息列表</param>
        /// <returns>
        /// success:0 
        /// 没有获取到电池信息:1
        /// 电池条码重复:2
        /// 电池位置号重复:3
        ///</returns>
        public int GetCellInfo_Offline(string Pallet_ID, out List<ET_CELL> listCell)
        {
            List<ET_CELL> list = new List<ET_CELL>();
            listCell = null;
            try
            {
                for (int i = 1; i < ClsGlobal.TrayType + 1; i++)
                {
                    ET_CELL etCell = new ET_CELL();
                    etCell.Cell_ID = "ID" + i;
                    etCell.Pallet_ID = Pallet_ID;
                    etCell.Cell_Position = i;
                    list.Add(etCell);
                }
                listCell = list;
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        #endregion

        #region OCV测试数据上传和读回

        //插入OCV测试数据
        public int UpdataOCVACIRData(int OCVType, string DeviceNo, char MeasureMode, List<ET_CELL> listCell)
        {
            List<ET_CELL> theList = listCell;

            //插入OCV数据
            try
            {
                List<OCV.WSOCVReference.CellOCVMeasureModel> lstCellOCVMeasureData =
                    new List<OCV.WSOCVReference.CellOCVMeasureModel>();
                OCV.WSOCVReference.OCVMeasureModel OCVMeasureData =
                    new OCV.WSOCVReference.OCVMeasureModel();

                //listCell转换到CellOCVMeasureData
                if (OCVType == 1)
                {
                    for (int i = 0; i < theList.Count; i++)
                    {
                        OCV.WSOCVReference.CellOCVMeasureModel theMeasureModel =
                            new OCV.WSOCVReference.CellOCVMeasureModel();

                        theMeasureModel.OCV1_ChNo = theList[i].Cell_Position;
                        theMeasureModel.OCV1_CellNo = theList[i].Cell_ID;
                        theMeasureModel.OCV1_Voltage_1 = decimal.Parse(theList[i].OCV_V1);
                        theMeasureModel.OCV1_Resistance_1 = decimal.Parse(theList[i].ACIR_R1); 

                        theMeasureModel.OCV1_Status_1 = theList[i].NGStatus;
                        theMeasureModel.OCV1_NGReason_1 = theList[i].NGReason;
                        theMeasureModel.OCV1_Time_1 = Convert.ToDateTime(theList[i].OCV_Write_Time);

                        lstCellOCVMeasureData.Add(theMeasureModel);
                    }

                    //赋值OCVMeasure
                    OCVMeasureData.OCV1_DevID_1 = DeviceNo;
                    OCVMeasureData.OCV1_MeasureMode_1 = MeasureMode;
                    OCVMeasureData.OCV1_TrayNo = theList[0].Pallet_ID;
                    OCVMeasureData.OCVMeasure = lstCellOCVMeasureData.ToArray();
                }

                //插入OCVMeasure
                OCV.WSOCVReference.Result theRes = WSOCVClient.InsertOCVMeasure(OCVMeasureData);

                if (theRes.Code != 1)
                {
                    throw new Exception(theRes.Msg);
                }
                
                return theList.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //OCV测试数据读回
        //JSON格式
        public void GetOCVACIRData(string TrayNo)
        {
            string JSONData = WSOCVClient.SelectOCVInfoByTrayNo(TrayNo);
                        
            //转换为类
        }

        #endregion
        

        #region 获取OCVNG数据

        //获取OCVNG数据
        public void GetOCVNGInfo(string TrayNo , out List< ClsTrayBattInfo> lstBattInfo)
        {
            lstBattInfo = new List<ClsTrayBattInfo>();
            try
            {
                WSOCVReference.TrayInfoResponse Response = WSOCVClient.SelectOCVNGModelInfo(TrayNo);

                if (Response.Result.Code != 1)
                {
                    throw new Exception("错误代码：" + Response.Result.Code + ",原因:" + Response.Result.Msg);
                }
                else
                {
                    //电池NG信息
                    ClsTrayBattInfo[] theArr = new ClsTrayBattInfo[Response.TrayInfoResult.Count()];

                    for (int i = 0; i < Response.TrayInfoResult.Count(); i++)
                    {
                        ClsTrayBattInfo theInfo = new ClsTrayBattInfo();

                        theInfo.Position = Response.TrayInfoResult[i].Position;
                        theInfo.BatteryCode = Response.TrayInfoResult[i].BatteryCode;
                        theInfo.NGInfo = Response.TrayInfoResult[i].NGInfo;
                        lstBattInfo.Add(theInfo);            
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        
        //获取OCVNG数据
        //用JSON
        public void GetOCVNGInfo2(string TrayNo, out List<ClsTrayBattInfo> lstBattInfo)
        {
            lstBattInfo = new List<ClsTrayBattInfo>();
            try
            {
                string strData = WSOCVClient.SelectOCVNGInfo(TrayNo);                              

                if (strData == "")
                {
                    throw new Exception("获取OCVNG数据出错");
                }
                else
                {
                    ClsTrayBattInfo[] theArr = JSSerializer.Deserialize<ClsTrayBattInfo[]>(strData);
                    List<ClsTrayBattInfo> theList = JSSerializer.Deserialize<ClsTrayBattInfo[]>(strData).ToList();

                    lstBattInfo = theList;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }    

        #endregion

        #region 获取化成NG数据

        //获取化成NG数据
        public void GetPreChgInfo(string TrayNo, out List<ClsTrayBattInfo> lstBattInfo)
        {
            lstBattInfo = new List<ClsTrayBattInfo>();
            try
            {
                WSOCVReference.TrayInfoResponse Response = WSOCVClient.SelectPreChgNGModelInfo(TrayNo);

                if (Response.Result.Code != 1)
                {
                    throw new Exception("错误代码：" + Response.Result.Code + ",原因:" + Response.Result.Msg);
                }
                else
                {
                    //电池NG信息
                    ClsTrayBattInfo[] theArr = new ClsTrayBattInfo[Response.TrayInfoResult.Count()];

                    for (int i = 0; i < Response.TrayInfoResult.Count(); i++)
                    {
                        ClsTrayBattInfo theInfo = new ClsTrayBattInfo();

                        theInfo.Position = Response.TrayInfoResult[i].Position;
                        theInfo.BatteryCode = Response.TrayInfoResult[i].BatteryCode;
                        theInfo.NGInfo = Response.TrayInfoResult[i].NGInfo;
                        lstBattInfo.Add(theInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  

        #endregion

        #region 更新电池与托盘绑定状态

        ////OCV后更新绑定状态
        //public void UpdateTrayNGResult(List<ClsTrayBattInfo> lstBattInfo)
        //{
        //    int count = lstBattInfo.Count();
        //    List<OCV.WSOCVReference.TrayInfoResult> lstNGInfo = new List<WSOCVReference.TrayInfoResult>();

        //    if (count == 0)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            OCV.WSOCVReference.TrayInfoResult theInfo = new OCV.WSOCVReference.TrayInfoResult();

        //            theInfo.Position = lstBattInfo[i].Position;
        //            theInfo.BatteryCode = lstBattInfo[i].BatteryCode;
        //            theInfo.NGInfo = lstBattInfo[i].NGInfo;

        //            lstNGInfo.Add(theInfo);
        //        }
        //        WSOCVClient.UpdateTrayNGResult(lstNGInfo.ToArray());
        //    }
        //}

        #endregion


    }

    //托盘绑定电池信息
    public class ClsBindRealinfo
    {
        public string BindRealinfo_id;
        public string BindRealinfo_TrayNo;
        public string BindRealinfo_CellNo;
        public string BindRealinfo_ChNo;
        public string BindRealinfo_BindDate;
        public string BindRealinfo_BindName;
    }

    //OCV1电池返回信息
    public class ClsOCVDataInfo
    {
        public int OCV1_id;
        public string OCV1_TrayNo;
        public string OCV1_CellNo;
        public int OCV1_ChNo;
        public string OCV1_DevID_1;
        public decimal OCV1_Resistance_1;
        public decimal OCV1_Voltage_1;
        public decimal OCV1__ShellPositive_1;
        public decimal OCV1__ShellNagtive_1;
        public char OCV1_MeasureMode_1;
        public string OCV1_NGReason_1;
        public System.DateTime OCV1_Time_1;
    }


    //电池NG信息
    public class ClsTrayBattInfo
    {
        public int Position;                //通道号
        public string BatteryCode;      //电池号
        public string NGInfo;           //1.正常 2 NG 3.返工 9.空
    }

}
