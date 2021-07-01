using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;
using OCV.OCVLogs;

namespace OCV
{
    public class ClsGetConfigDetail
    {
        /// <summary>
        /// 获得工程信息
        /// </summary>
        /// <param name="ProjectSetType">1-》本地获取，2-》MES中获取</param>
        /// <param name="OCVtype">OCV型号</param>
        /// <returns></returns>
        public static int GetProjectInfo(int ProjectSetType, int OCVtype)
        {
            try
            {
                string BattType, CfgName = "";
                int index = 0;
                int ret;
                //变量加载----------------------------------------------------
                if (ProjectSetType == 1)
                {
                    //mProjectPath = ClsGlobal.mSettingPath;
                    //CfgName = "OCVSetting";
                    //ret = GetProjectInfo_Loc(ProjectSetType, OCVtype, mProjectPath, CfgName);
                    BattType = ClsGlobal.MODEL_NO;
                    ret = GetProjectInfo_Loc(ProjectSetType, OCVtype, BattType);
                    return ret;
                }
                else if (ProjectSetType == 2)
                {
                }
                else if (ProjectSetType == 3)
                {
                    #region 服务器获取工艺
                    DataTable DT_Info = new DataTable();
                    // DS_Info = ClsGlobal.mDBCOM_OCV_QT.GetProcessList("OCV" + OCVtype,ClsGlobal.MODEL_NO, ClsGlobal.Config);
                    DT_Info = ClsGlobal.mDBCOM_OCV_QT.GetProcessList("OCV" + OCVtype, ClsGlobal.MODEL_NO);
                    if (DT_Info.Rows.Count == 0)
                    {
                        return 31;
                    }
                    bool flag = false;
                    for (int i = 0; i < DT_Info.Rows.Count; i++)
                    {
                        ProcessInfo ModelInfo = ClsGlobal.mDBCOM_OCV_QT.DataRowToProModel(DT_Info.Rows[i]);

                        ClsGlobal.Config = ModelInfo.ProjectName;
                        // 电压上下限
                        ClsGlobal.UpLmt_V = (double)ModelInfo.UpLmt_V;
                        ClsGlobal.DownLmt_V = (double)ModelInfo.DownLmt_V; 
                        if (OCVtype == 1)
                        {

                        }
                        else if (ClsGlobal.TestType == 1)
                        {
                            // 壳压压上下限
                            ClsGlobal.UpLmt_SV = (double)ModelInfo.UpLmt_SV;
                            ClsGlobal.DownLmt_SV = (double)ModelInfo.DownLmt_SV;
                        }
                        else if (ClsGlobal.TestType == 2)
                        {
                            // 壳压压上下限
                            ClsGlobal.UpLmt_SV = (double)ModelInfo.UpLmt_SV;
                            ClsGlobal.DownLmt_SV = (double)ModelInfo.DownLmt_SV;
                            //ACIR上下限
                            ClsGlobal.UpLmt_ACIR = (double)ModelInfo.UpLmt_ACIR;
                            ClsGlobal.DownLmt_ACIR = (double)ModelInfo.DownLmt_ACIR;
                            //ACIR极差上下限
                            ClsGlobal.UpLMT_ACIRRange = (double)ModelInfo.UpLMT_ACIRRange;
                            ClsGlobal.DownLMT_ACIRRange = (double)ModelInfo.DownLMT_ACIRRange;
                            //acir极差参数
                            ClsGlobal.DownLMT_ACIRRange = (double)ModelInfo.DownLMT_ACIRRange;
                            ClsGlobal.UpLMT_ACIRRange = (double)ModelInfo.UpLMT_ACIRRange;
                            //是否启用ACIR极差限制(Y或N)
                            ClsGlobal.IS_Enable_ACIRRange =ModelInfo.IS_Enable_ACIRRange;
                          
                            //ACIR中值或最小值(Y或N)
                            ClsGlobal.ACIR_MinOrMedian = ModelInfo.ACIR_MinOrMedian;
                        }

                        if (OCVtype == 3)
                        {

                            //压降参数
                            ClsGlobal.MaxVoltDrop = (double)ModelInfo.MaxVoltDrop;
                            ClsGlobal.MinVoltDrop = (double)ModelInfo.MinVoltDrop;
                            ClsGlobal.VoltDrop = (double)ModelInfo.VoltDrop;
                            ClsGlobal.ENVoltDrop =ModelInfo.ENVoltDrop;

                            //Drop中值或最小值(Y或N)
                            ClsGlobal.Drop_MinOrMedian = ModelInfo.Drop_MinOrMedian;
                            //是否启用压降极差限制(Y或N)
                            ClsGlobal.IS_Enable_DropRange = ModelInfo.IS_Enable_DropRange;
                            //压降极差最大参数
                            ClsGlobal.DownLMT_DropRange = (double)ModelInfo.DownLMT_DropRange;
                            ClsGlobal.UpLMT_DropRange = (double)ModelInfo.UpLMT_DropRange;
                         
                        }
                        //ClsGlobal.IS_DATE_LIMIT = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_DATE_LIMIT", null); //是否时间限制(Y或N)
                        //if (ClsGlobal.IS_DATE_LIMIT == "Y")
                        //{
                        //    //时间上下限
                        //    ClsGlobal.UpLmt_time = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_time", null));
                        //    ClsGlobal.DownLmt_time = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_time", null));
                        //}
                        //else
                        //{
                        //    //时间上下限
                        //    ClsGlobal.UpLmt_time = 9999999;
                        //    ClsGlobal.DownLmt_time = -100;
                        //}


                        //温度修正值
                        ClsGlobal.TempBase = (double)ModelInfo.TempBase;
                        ClsGlobal.TempParaModify = (double)ModelInfo.TempPara;
                        WriteConfigDetail(OCVtype, ClsGlobal.MODEL_NO, ClsGlobal.Config);  //写本地文件  
                        flag = true;
                        break;
                    }
                    if (flag)
                    {
                        return 0;
                    }
                    else
                    {
                        return 32;
                    }
                    #endregion
                }
                return 0;
            }
            catch (Exception ex)
            {
                ClsLogs.ConfiglogNet.WriteWarn("GetConfigName_Loc", "获取工艺参数失败，原因;" + ex.Message);
                return 25; // 获取工艺参数失败
            }
        }

        //本地工程信息
        private static int GetProjectInfo_Loc(int ProjectSetType, int OCVtype, string BattType)
        {
            string mProjectPath, mConfig;

            try
            {
                int ret = GetConfigName_Loc(ProjectSetType, OCVtype, BattType, out mConfig);
                mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype + "\\" + BattType + ".ini";
                if (ret != 0)
                {
                    return ret;
                }
                ClsGlobal.Config = mConfig.Trim();
                // 电压上下限
                ClsGlobal.UpLmt_V = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_V", null));
                ClsGlobal.DownLmt_V = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_V", null));
                if (OCVtype==1)
                {

                }
                else if (ClsGlobal.TestType == 1)
                {
                    // 壳压压上下限
                    ClsGlobal.UpLmt_SV = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_SV", null));
                    ClsGlobal.DownLmt_SV = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_SV", null));
                }
                else if (ClsGlobal.TestType == 2)
                {
                    // 壳压压上下限
                    ClsGlobal.UpLmt_SV = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_SV", null));
                    ClsGlobal.DownLmt_SV = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_SV", null));
                    //ACIR上下限
                    ClsGlobal.UpLmt_ACIR = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_ACIR", null));
                    ClsGlobal.DownLmt_ACIR = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_ACIR", null));
                    //ACIR极差上下限
                    ClsGlobal.UpLMT_ACIRRange = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLMT_ACIRrange", null));
                    ClsGlobal.DownLMT_ACIRRange = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLMT_ACIRrange", null));

                    //是否启用ACIR极差限制(Y或N)
                    ClsGlobal.IS_Enable_ACIRRange = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_Enable_ACIRRange", null);
                    if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                    {
                        //acir极差参数
                        ClsGlobal.DownLMT_ACIRRange = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLMT_ACIRRange", null));
                        ClsGlobal.UpLMT_ACIRRange = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLMT_ACIRRange", null));
                    }
                    else
                    {
                        ClsGlobal.UpLMT_ACIRRange = 999999999999;
                        ClsGlobal.DownLMT_ACIRRange = -999999;
                    }
                    //ACIR中值或最小值(Y或N)
                    ClsGlobal.ACIR_MinOrMedian = INIAPI.INIGetStringValue(mProjectPath, mConfig, "ACIR_MinOrMedian", null);
                }

                if (OCVtype == 3)
                {

                    //压降参数
                    ClsGlobal.MaxVoltDrop = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "MaxVoltDrop", null));
                    ClsGlobal.MinVoltDrop = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "MinVoltDrop", null));
                    ClsGlobal.VoltDrop = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "VoltDrop", null));
                    ClsGlobal.ENVoltDrop = INIAPI.INIGetStringValue(mProjectPath, mConfig, "ENVoltDrop", null);
                  
                    //Drop中值或最小值(Y或N)
                    ClsGlobal.Drop_MinOrMedian = INIAPI.INIGetStringValue(mProjectPath, mConfig, "Drop_MedianOrMin", null);
                    //是否启用压降极差限制(Y或N)
                    ClsGlobal.IS_Enable_DropRange = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_Enable_DropRange", null); 
                   
                    if (ClsGlobal.IS_Enable_DropRange == "Y")
                    {
                        //压降极差最大参数
                        ClsGlobal.DownLMT_DropRange = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLMT_DropRange", null));
                        ClsGlobal.UpLMT_DropRange = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLMT_DropRange", null));
                    }
                    else
                    {
                        ClsGlobal.UpLMT_DropRange = 999999999999;
                        ClsGlobal.DownLMT_DropRange = -999999;
                    }

                }
                //ClsGlobal.IS_DATE_LIMIT = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_DATE_LIMIT", null); //是否时间限制(Y或N)
                //if (ClsGlobal.IS_DATE_LIMIT == "Y")
                //{
                //    //时间上下限
                //    ClsGlobal.UpLmt_time = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_time", null));
                //    ClsGlobal.DownLmt_time = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_time", null));
                //}
                //else
                //{
                //    //时间上下限
                //    ClsGlobal.UpLmt_time = 9999999;
                //    ClsGlobal.DownLmt_time = -100;
                //}


                //温度修正值
                ClsGlobal.TempBase = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "TempBase", null));
                ClsGlobal.TempParaModify = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "TempParaModify", null));
               
              

                //if (OCVtype == 1)
                //{
                //    ClsGlobal.K_VALUE = -10;
                //}
                //else
                //{
                //    ClsGlobal.K_VALUE = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "K", null));
                //}
                return 0;
            }
            catch (Exception ex)
            {
                ClsLogs.ConfiglogNet.WriteWarn("GetProjectInfo_Loc", "获取工艺参数失败，原因;" + ex.Message);
                return 25; // 获取工艺参数失败
            }

        }

        //判断是否有默认的工艺，并且获取默认的工艺  适用于本地测试
        //取消ProName 20180922
        private static int GetConfigName_Loc(int ProjectSetType, int OCVtype, string BattType, out string ConfigName)
        {
            string[] Section;
            string ProjectPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype + "\\Project.ini";
            ConfigName = null;
            try
            {
                //mProName = INIAPI.INIGetStringValue(ProjectPath, BattType, "ProName", null);
                ConfigName = INIAPI.INIGetStringValue(ProjectPath, BattType, "CfgName", null);

                if (ConfigName == "" || ConfigName == null)
                {
                    return 22;    //没有设置默认工艺
                }

                //if (ProjectSetType==1)
                //{
                //    int re = JudgeEngExist(OCVtype, BattType);
                //    if (re!=0)
                //    {
                //        return re;
                //    }
                //    string mConfigPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype + "\\" + BattType + ".ini";

                //    Section = INIAPI.INIGetAllSectionNames(mConfigPath);
                //    int id = Array.IndexOf(Section, ConfigName);
                //    if (id == -1)
                //    { 
                //        ConfigName = "";
                //        return 23; //无对应工艺
                //    }
                //}
                return 0;
            }
            catch (Exception ex)
            {
                return 24;   //没有工艺文件
            }
        }

        //保存本地配置文件
        private static void WriteConfigDetail(int OCVtype, string BattType, string ConfigDetail)
        {
            string mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype + "\\" + BattType + ".ini";
            //创建导出数据文件夹
            if (!Directory.Exists(ClsGlobal.mSettingProjectPath))
            {
                Directory.CreateDirectory(ClsGlobal.mSettingProjectPath);
            }
            //创建OCV型号数据文件夹      
            for (int i = 1; i < 4; i++)
            {
                string _sOCVProjectPath = "OCV" + i;
                _sOCVProjectPath = ClsGlobal.mSettingProjectPath + "\\" + _sOCVProjectPath;
                if (!Directory.Exists(_sOCVProjectPath))
                {
                    Directory.CreateDirectory(_sOCVProjectPath);
                }
            }

            try
            {
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLmt_V", ClsGlobal.UpLmt_V.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLmt_V", ClsGlobal.DownLmt_V.ToString());
                if (OCVtype==1)
                {

                }
                else if (ClsGlobal.TestType == 1)
                {
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLmt_SV", ClsGlobal.DownLmt_SV.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLmt_SV", ClsGlobal.UpLmt_SV.ToString());

                }
                else if (ClsGlobal.TestType == 2)
                {
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLmt_ACIR", ClsGlobal.UpLmt_ACIR.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLmt_ACIR", ClsGlobal.DownLmt_ACIR.ToString());


                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLMT_ACIRrange", ClsGlobal.UpLMT_ACIRRange.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLMT_ACIRrange", ClsGlobal.DownLMT_ACIRRange.ToString());

                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "ACIR_MedianOrMin", ClsGlobal.ACIR_MinOrMedian);
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "IS_Enable_ACIRRange", ClsGlobal.IS_Enable_ACIRRange);
                }

                if (OCVtype == 3)
                {
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "MaxVoltDrop", ClsGlobal.MaxVoltDrop.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "MinVoltDrop", ClsGlobal.MinVoltDrop.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "VoltDrop", ClsGlobal.VoltDrop.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "ENVoltDrop", ClsGlobal.ENVoltDrop);

                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLMT_DropRange", ClsGlobal.DownLMT_DropRange.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLMT_DropRange", ClsGlobal.UpLMT_DropRange.ToString());
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "Drop_MedianOrMin", ClsGlobal.Drop_MinOrMedian);
                    INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "IS_Enable_DropRange", ClsGlobal.IS_Enable_DropRange);

                }

                //2019 0227 新增温度工艺
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "TempBase", ClsGlobal.TempBase.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "TempParaModify", ClsGlobal.TempParaModify.ToString());

			
            }
            catch (Exception ex)
            {
               
                ClsLogs.ConfiglogNet.WriteWarn("WriteConfigDetail", "写入默认默认工艺失败，原因;" + ex.Message);
            }
        }

    }
}
