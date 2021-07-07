using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OCV
{
    public class GetConfigDetail
    {
        static ClsMESCOM mMESCOM;
        static List<clsMESConfig_M> thelstMESConfig_M;
  
        //获得工程信息
        public static int GetProjectInfo(int ProjectSetType, int OCVtype)
        {
            try
            {
                string mProjectPath;
                string BattType, CfgName="";
                int index=0;
                int ret;
                //变量加载----------------------------------------------------
                if (ProjectSetType == 1)
                {
                    //mProjectPath = ClsGlobal.mSettingPath;
                    //CfgName = "OCVSetting";
                    //ret = GetProjectInfo_Loc(ProjectSetType, OCVtype, mProjectPath, CfgName);

                    BattType = ClsGlobal.listETCELL[0].MODEL_NO;
                    ret = GetProjectInfo_Loc(ProjectSetType, OCVtype, BattType);
                    return ret;
                }
                else if (ProjectSetType == 2)
                {
                    bool flag=false;
                    //string mUrl = "http://10.5.80.52:8080/mes/service";
                    string mUrl = ClsGlobal.MESURL_OCV;
                    mMESCOM = new ClsMESCOM(mUrl);
                    //mMESCOM = new ClsMESCOM();

                    List<clsMESConfig_M> lstMESConfig_M = new List<clsMESConfig_M>();
                    clsMESConfig_Query MESConfig_Query = new clsMESConfig_Query();
                    BattType = ClsGlobal.listETCELL[0].MODEL_NO;    //电池型号


                    //**********************测试用**********************//
                   // mMODEL_NO = "2564A2";
                   // ClsGlobal.listETCELL[0].MODEL_NO = mMODEL_NO; 
                   // MESConfig_Query.param = "08_OCV" + OCVtype;               //工序号
                   // MESConfig_Query.project_no = "111111"; //项目编号

                    //**********************测试用**********************//

                    MESConfig_Query.param = "08_OCV" + OCVtype;               //工序号
                    MESConfig_Query.project_no = ClsGlobal.listETCELL[0].PROJECT_NO; //项目编号
                    MESConfig_Query.model_no = BattType;

                    lstMESConfig_M = mMESCOM.GetOCVConfigNames(MESConfig_Query);  //获取工艺名版本
                    thelstMESConfig_M = lstMESConfig_M;

                    if (thelstMESConfig_M != null)
                    {
                        ret = GetConfigName(ProjectSetType, OCVtype, BattType, out CfgName);
                        if (ret != 0)
                        {                 
                            for (int i = 0; i < thelstMESConfig_M.Count; i++)
                            {
                                ClsGlobal.MESConfig.Add(thelstMESConfig_M[i].revsion);
                            }
                            FrmProSelection FS = new FrmProSelection();
                            FS.StartPosition = FormStartPosition.CenterScreen;
                            FS.ShowDialog();
                            index = ClsGlobal.MESConfigIndex;
                            BattType = MESConfig_Query.model_no;
                            CfgName = thelstMESConfig_M[index].revsion;
                            flag = true;
                        }
                        else
                        {                        
                            for (int i = 0; i < thelstMESConfig_M.Count; i++)
                            {
                                if (CfgName == thelstMESConfig_M[i].revsion)
                                {
                                    flag = true;
                                    index = i;
                                    break;
                                }
                            }
                            if(flag==false)
                            {
                                for (int i = 0; i < thelstMESConfig_M.Count; i++)
                                {
                                    ClsGlobal.MESConfig.Add(thelstMESConfig_M[i].revsion);
                                }

                                FrmProSelection FS = new FrmProSelection();
                                FS.StartPosition = FormStartPosition.CenterScreen;
                                FS.ShowDialog();
                                index = ClsGlobal.MESConfigIndex;
                                flag = true;
                            }
                        }                    
                    }
                    else
                    {
                        return ret = 26;  //MES无对应工艺返回
                    }


                    if (flag==true)
                    {
                        if (OCVtype == 1)
                        {
                            clsMESConfigDetail_OCV1 ConfigDetail = new clsMESConfigDetail_OCV1();
                            clsMESConfig_M MESConfig_M = thelstMESConfig_M[index];
                            ConfigDetail = mMESCOM.GetConfigDetail_OCV1(MESConfig_M);
                             if (ConfigDetail != null)
                            {
                                // 电压上下限
                                ClsGlobal.UpLmt_V = ConfigDetail.OCV_MAX_VOLTAGE;
                                ClsGlobal.DownLmt_V = ConfigDetail.OCV_MIN_VOLTAGE;

                                ClsGlobal.IS_TEST_RESISTANCE = ConfigDetail.IS_TEST_RESISTANCE;  //是否测试内阻（Y或N）
                                if (ClsGlobal.IS_TEST_RESISTANCE == "Y")
                                {
                                    //ACIR上下限
                                    ClsGlobal.UpLmt_ACIR = ConfigDetail.OCV_MAX_RESISTANCE;
                                    ClsGlobal.DownLmt_ACIR = ConfigDetail.OCV_MIN_RESISTANCE;
                                }
                                else
                                {
                                    //ACIR上下限
                                    ClsGlobal.UpLmt_ACIR = 999999999;
                                    ClsGlobal.DownLmt_ACIR = -100;
                                }

                                ClsGlobal.IS_VERIFY_CAPACITY = ConfigDetail.IS_VERIFY_CAPACITY;  //是否验证容量（Y或N）
                                ClsGlobal.IS_GET_CAPACITY = ConfigDetail.IS_GET_CAPACITY;      //是否获取容量数据（Y或N） 
                                if (ClsGlobal.IS_VERIFY_CAPACITY == "Y" && ClsGlobal.IS_GET_CAPACITY == "Y")
                                {
                                    //电容上下限
                                    ClsGlobal.UpLmt_CAP = ConfigDetail.OCV_MAX_CAPACITY;
                                    ClsGlobal.DownLmt_CAP = ConfigDetail.OCV_MIN_CAPACITY;
                                }
                                else
                                {
                                    ClsGlobal.UpLmt_CAP = 9999999;
                                    ClsGlobal.DownLmt_CAP = -100;
                                }

                                //压降最大参数
                                ClsGlobal.MaxVoltDrop = 99999;
                                ClsGlobal.MinVoltDrop = -10;

                                ClsGlobal.IS_DATE_LIMIT = "N"; //是否时间限制(Y或N)

                                //时间上下限
                                ClsGlobal.UpLmt_time = 99999;
                                ClsGlobal.DownLmt_time = -10;
                                ClsGlobal.K_VALUE = -10;
                             
                                //电压预警值
                                ClsGlobal.VOLTAGE_ALARM_VALUE = ConfigDetail.VOLTAGE_ALARM_VALUE;
                                ClsGlobal.ISOLATION_CONDITION = ConfigDetail.ISOLATION_CONDITION; //隔离条件
                                WriteConfigDetail(OCVtype, BattType, CfgName);  //写本地文件    
                            }                                 
                            else
                            {
                                return ret = 28;  //MES无返回工艺信息

                            }
                        }
                        else
                        {
                            clsMESConfigDetail_OCV2_3 ConfigDetail = new clsMESConfigDetail_OCV2_3();
                            clsMESConfig_M MESConfig_M = thelstMESConfig_M[index];
                            ConfigDetail = mMESCOM.GetConfigDetail_OCV2_3(MESConfig_M);                           
                            if (ConfigDetail != null)
                            {
                                // 电压上下限
                                ClsGlobal.UpLmt_V = ConfigDetail.OCV_MAX_VOLTAGE;
                                ClsGlobal.DownLmt_V = ConfigDetail.OCV_MIN_VOLTAGE;      
                                //ACIR上下限
                                ClsGlobal.UpLmt_ACIR = ConfigDetail.OCV_MAX_RESISTANCE;
                                ClsGlobal.DownLmt_ACIR = ConfigDetail.OCV_MIN_RESISTANCE;
                                //电容上下限
                                ClsGlobal.UpLmt_CAP = ConfigDetail.OCV_MAX_CAPACITY;
                                ClsGlobal.DownLmt_CAP = ConfigDetail.OCV_MIN_CAPACITY;
                                //压降最大参数
                                ClsGlobal.MaxVoltDrop = ConfigDetail.OCV_MAX_PRESSUREDROP;
                                ClsGlobal.MinVoltDrop = ConfigDetail.OCV_MIN_PRESSUREDROP;                               
                                //时间上下限
                                ClsGlobal.UpLmt_time = ConfigDetail.OCV_INTERNAL_MAX;
                                ClsGlobal.DownLmt_time = ConfigDetail.OCV_INTERNAL_MIN;
                                ClsGlobal.K_VALUE = ConfigDetail.K_VALUE;
                                //电压预警值
                                ClsGlobal.VOLTAGE_ALARM_VALUE = ConfigDetail.VOLTAGE_ALARM_VALUE;
                                ClsGlobal.ISOLATION_CONDITION = ConfigDetail.ISOLATION_CONDITION; //隔离条件

                                ClsGlobal.IS_VERIFY_CAPACITY = ConfigDetail.IS_VERIFY_CAPACITY;  //是否验证容量（Y或N）
                                ClsGlobal.IS_GET_CAPACITY = ConfigDetail.IS_GET_CAPACITY;      //是否获取容量数据（Y或N） 
                                ClsGlobal.IS_DATE_LIMIT = ConfigDetail.IS_DATE_LIMIT; //是否时间限制(Y或N)
                                ClsGlobal.IS_TEST_RESISTANCE = ConfigDetail.IS_TEST_RESISTANCE;  //是否测试内阻（Y或N）

                                WriteConfigDetail(OCVtype, BattType, CfgName);  //写本地文件    


                                //根据限制条件重新加载判定条件
                                if (ClsGlobal.IS_TEST_RESISTANCE == "N")
                                {
                                    //ACIR上下限
                                    ClsGlobal.UpLmt_ACIR = 999999999;
                                    ClsGlobal.DownLmt_ACIR = -100;
                                }                             

                                if (ClsGlobal.IS_DATE_LIMIT == "N")
                                {
                                    //时间上下限
                                    ClsGlobal.UpLmt_time = 9999999;
                                    ClsGlobal.DownLmt_time = -100;
                                }
                             
                                if (ClsGlobal.IS_VERIFY_CAPACITY == "N"|| ClsGlobal.IS_GET_CAPACITY == "N")
                                {
                                    //电容上下限
                                    ClsGlobal.UpLmt_CAP = 9999999;
                                    ClsGlobal.DownLmt_CAP = -100;
                                }                            
                            }      
                        }
                        flag = false ;
                        return 0;
                    }
                    else
                    {
                        flag = false;
                        return ret = 27;  //MES返回工艺中没有默认的工艺
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                
                if (ex.Message == "无法连接到远程服务器")
                {
                    MessageBox.Show(ex.Message);
                }
                WriteLog("获取工艺参数失败，原因;" + ex.Message);
                return 25; // 获取工艺参数失败
            }
        }

        //本地工程信息
        private static int GetProjectInfo_Loc(int ProjectSetType,int OCVtype, string BattType)
        {
            string mProjectPath, mConfig;

            try
            {
                int ret = GetConfigName(ProjectSetType, OCVtype, BattType, out mConfig);
                mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype + "\\" + BattType + ".ini";
                if (ret != 0)
                {
                    return ret;
                }

                // 电压上下限
                ClsGlobal.UpLmt_V = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_V", null));
                ClsGlobal.DownLmt_V = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_V", null));
                ClsGlobal.IS_TEST_RESISTANCE = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_TEST_RESISTANCE", null);  //是否测试内阻（Y或N）
                if (ClsGlobal.IS_TEST_RESISTANCE == "Y")
                {
                    //ACIR上下限
                    ClsGlobal.UpLmt_ACIR = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_ACIR", null));
                    ClsGlobal.DownLmt_ACIR = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_ACIR", null));
                }
                else
                {
                    //ACIR上下限
                    ClsGlobal.UpLmt_ACIR = 9999999;
                    ClsGlobal.DownLmt_ACIR = -100;
                }

                ClsGlobal.IS_VERIFY_CAPACITY = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_VERIFY_CAPACITY", null);                
                ClsGlobal.IS_GET_CAPACITY = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_GET_CAPACITY", null);     //是否获取容量数据（Y或N） 
                if (ClsGlobal.IS_VERIFY_CAPACITY == "Y" && ClsGlobal.IS_GET_CAPACITY == "Y")
                {               
                    //电容上下限
                    ClsGlobal.UpLmt_CAP = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_CAP", null));
                    ClsGlobal.DownLmt_CAP = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_CAP", null));
                }
                else
                {
                    ClsGlobal.UpLmt_CAP = 9999999;
                    ClsGlobal.DownLmt_CAP = -100;
                }
                //压降最大参数
                ClsGlobal.MaxVoltDrop = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "MaxVoltDrop", null));
                ClsGlobal.MinVoltDrop = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "MinVoltDrop", null));

                ClsGlobal.IS_DATE_LIMIT = INIAPI.INIGetStringValue(mProjectPath, mConfig, "IS_DATE_LIMIT", null); //是否时间限制(Y或N)
                if (ClsGlobal.IS_DATE_LIMIT == "Y")
                {
                    //时间上下限
                    ClsGlobal.UpLmt_time = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "UpLmt_time", null));
                    ClsGlobal.DownLmt_time = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "DownLmt_time", null));
                }
                else
                {
                    //时间上下限
                    ClsGlobal.UpLmt_time = 9999999;
                    ClsGlobal.DownLmt_time = -100;
                }
               
                if (OCVtype==1)
                {
                    ClsGlobal.K_VALUE = -10;
                }
               else
                {
                    ClsGlobal.K_VALUE = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "K", null));
                }

                ClsGlobal.VOLTAGE_ALARM_VALUE = double.Parse(INIAPI.INIGetStringValue(mProjectPath, mConfig, "VOLTAGE_ALARM_VALUE", null));
                ClsGlobal.ISOLATION_CONDITION = INIAPI.INIGetStringValue(mProjectPath, mConfig, "ISOLATION_CONDITION", null);  //隔离条件
                return 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return 25; // 获取工艺参数失败
            }

        }

        //判断电池条码在本地是否有默认工程  适用于本地测试
        private static int JudgeEngExist(int OCVtype, string BattType)
        {
            string[] mFiles = Directory.GetFiles(ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype, "*.ini");
            List<String> list = new List<string>();
            for (int i = 0; i < mFiles.Length; i++)
            {
                string[] arr = mFiles[i].Split('.');
                list.Add(Path.GetFileName(arr[0]));
            }
            if (list.Count == 0)
            {
                return 20;  //无工程文件，请设置工程。
            }
            else
            {
                bool exists = (list).Contains(BattType);
                if (exists)
                {
                    return 0;
                }
                else
                {
                    return 21;  //此电池型号无对应工程，请设置工程。
                }
            }
        }

        //判断是否有默认的工艺，并且获取默认的工艺  适用于本地测试
        //取消ProName 20180922
        private static int GetConfigName(int Test_Mode, int OCVtype, string BattType, out string ConfigName)
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
                
                if (Test_Mode==3)
                {
                    int re = JudgeEngExist(OCVtype, BattType);
                    if (re!=0)
                    {
                        return re;
                    }
                    string mConfigPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + OCVtype + "\\" + BattType + ".ini";

                    Section = INIAPI.INIGetAllSectionNames(mConfigPath);
                    int id = Array.IndexOf(Section, ConfigName);
                    if (id == -1)
                    { 
                        ConfigName = "";
                        return 23; //无对应工艺
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                WriteLog("获取默认工艺失败，原因;" + ex.Message);
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

                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLmt_ACIR", ClsGlobal.UpLmt_ACIR.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLmt_ACIR", ClsGlobal.DownLmt_ACIR.ToString());

                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLmt_CAP", ClsGlobal.UpLmt_CAP.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLmt_CAP", ClsGlobal.DownLmt_CAP.ToString());

                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "MaxVoltDrop", ClsGlobal.MaxVoltDrop.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "MinVoltDrop", ClsGlobal.MinVoltDrop.ToString());

                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "UpLMT_time", ClsGlobal.UpLmt_time.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "DownLmt_time", ClsGlobal.DownLmt_time.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "K",  ClsGlobal.K_VALUE.ToString());

                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "VOLTAGE_ALARM_VALUE", ClsGlobal.VOLTAGE_ALARM_VALUE.ToString());
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "IS_TEST_RESISTANCE", ClsGlobal.IS_TEST_RESISTANCE);
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "IS_VERIFY_CAPACITY", ClsGlobal.IS_VERIFY_CAPACITY);
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "IS_GET_CAPACITY", ClsGlobal.IS_GET_CAPACITY);
                INIAPI.INIWriteValue(mProjectPath, ConfigDetail, "IS_DATE_LIMIT", ClsGlobal.IS_DATE_LIMIT);

            }
            catch (Exception ex)
            {
                WriteLog("写入默认默认工艺失败，原因;" + ex.Message);
            }
        }
   
        private static void WriteLog(string Mess)
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\log";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = path + "\\CommSql";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                using (FileStream fs = new FileStream(path + "\\" + "GetConfig_" + DateTime.Now.ToString("yyyyMMdd") + ".log", FileMode.Append))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString() + " " + Mess);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
        
        //检测电芯条码是否符合隔离条件
        public static void CheckIsolation()
        {
            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
             {

                bool re = CheckIsolationData(ClsGlobal.listETCELL[i].Cell_ID.Trim(), ClsGlobal.ISOLATION_CONDITION);
                if(re)
                {
                    ClsGlobal.listETCELL[i].ISOLATION= "1";
                }
                else
                {
                   ClsGlobal.listETCELL[i].ISOLATION = "0";
                }
             }
        }

        //检测电芯条码是否符合隔离条件
        //隔离条件(型号, (批号 / 起始流水号, 结束流水号)),条件之间用"|"分割
        private static bool CheckIsolationData(string strSFC, string condition)
        {
            try
            {
                if (string.IsNullOrEmpty(condition) || condition == "0")
                {
                    return false;
                }
                string[] strCondition = condition.Split('|');
                for (int i = 0; i < strCondition.Length; i++)
                {
                    if (string.IsNullOrEmpty(strCondition[i]) == true)
                    {
                        continue;
                    }
                    string[] strChildCondition = strCondition[i].Split(',');
                    if (strChildCondition.Length == 1)
                    {
                        if (strSFC.Contains(strChildCondition[0]))
                        {
                            return true;
                        }
                    }
                    else if (strChildCondition.Length == 2)
                    {
                        if (strSFC.Contains(strChildCondition[0]) && strSFC.Contains(strChildCondition[1]))
                        {
                            return true;
                        }
                    }
                    else if (strChildCondition.Length == 3)
                    {
                        int startNo = int.Parse(strChildCondition[1]);
                        int endNo = int.Parse(strChildCondition[2]);
                        int serialNo = int.Parse(strSFC.Substring(strSFC.Length - 5, 5));
                        bool w = strSFC.Contains(strChildCondition[0]) && serialNo >= startNo && serialNo <= endNo;
                        if (strSFC.Contains(strChildCondition[0]) && serialNo >= startNo && serialNo <= endNo)
                        {
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
