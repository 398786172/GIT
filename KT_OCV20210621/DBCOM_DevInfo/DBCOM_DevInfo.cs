using DevInfo.DAL;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevInfo
{
    /// <summary>
    /// 设备信息访问接口
    /// </summary>
    public class DBCOM_DevInfo
    {
        DAL.Channel_Info mChannel_Info;
        DAL.SET_Info mSET_Info;

        public DBCOM_DevInfo(string strSource)
        {
            StringBuilder StrB = new StringBuilder();
            StrB.Append("Data Source=" + strSource);
            StrB.Append(" ;Integrated Security=no");

            DbHelperSQLite.connectionString = StrB.ToString();

            mChannel_Info = new Channel_Info();
            mSET_Info = new SET_Info();
        }

        public void InitConnectionString(string str)
        {
            DbHelperSQLite.connectionString = str;
        }

        //------------设置信息----------------------

        //设置保存
        public void SaveSetInfo(Model.SET_Info model)
        {
            if (mSET_Info.Exists(model.SetName) == true)
            {
                mSET_Info.Update(model);
            }
            else
            {
                mSET_Info.Add(model);
            }
        }

        //删除工程
        public void DeleteSetInfo(Model.SET_Info model)
        {
            mSET_Info.Delete(model.SetName);
        }

        //获取信息列表
        public void GetSetInfoList(out List<Model.SET_Info> modelList)
        {
            List<Model.SET_Info> theList = new List<Model.SET_Info>();

            DataSet DS = mSET_Info.GetList("");

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                theList.Add(mSET_Info.DataRowToModel(DS.Tables[0].Rows[i]));
            }

            modelList = theList;
        }


        //------------通道测试信息----------------

        //删除所有数据
        public void DeleteAllData()
        {
            mChannel_Info.DeleteAll();
        }

        //初始化通道数据
        public void InitChannelData(int ChannelNum)
        {
            mChannel_Info.DeleteAll();

            for (int i = 0; i < ChannelNum; i++)
            {
                Model.Channel_Info theChannel_Info = new Model.Channel_Info();

                theChannel_Info.ChannelNo = i + 1;
                theChannel_Info.OCV_ErrCount = 0;
                theChannel_Info.Shell_ErrCount = 0;
                theChannel_Info.ACIR_ErrCount = 0;
                theChannel_Info.OCV_EN = true;
                theChannel_Info.Shell_EN = true;
                theChannel_Info.ACIR_EN = true;

                mChannel_Info.Add(theChannel_Info);
            }

        }

        //获得通道数据
        public void GetChannelData(out List<Model.Channel_Info> modelList)
        {
            List<Model.Channel_Info> theList = new List<Model.Channel_Info>();

            int count = mChannel_Info.GetRecordCount("");

            for (int i = 0; i < count; i++)
            {
                theList.Add(mChannel_Info.GetModel(i + 1));
            }

            modelList = theList;
        }

        public void GetChannelData(out DataSet DS)
        {
            DS = mChannel_Info.GetList("");
        }

        //写通道数据
        public void UpdateChannelData(List<Model.Channel_Info> modelList)
        {
            if (modelList == null) return;

            for (int i = 0; i < modelList.Count; i++)
            {
                mChannel_Info.Update(modelList[i]);
            }
        }

        //OCV--------------

        //增加错误累计次数, 并获得当前次数
        public void Add_OCVErrCount(int ChannelNo, out int NowCount)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            //累计次数+1
            theModel.OCV_ErrCount += 1;
            NowCount = (int)theModel.OCV_ErrCount;
            mChannel_Info.Update(theModel);
        }

        //设置错误累计次数
        public void Set_OCVErrCount(int ChannelNo, int Num)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            //次数
            theModel.OCV_ErrCount = Num;
            mChannel_Info.Update(theModel);
        }

        //获得错误累计次数
        public int Get_OCVErrCount(int ChannelNo)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);
            return (int)theModel.OCV_ErrCount;
        }


        //壳体电压------------

        //增加错误累计次数,并获得当前次数
        public void Add_ShellErrCount(int ChannelNo, out int NowCount)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            //累计次数+1
            theModel.Shell_ErrCount += 1;
            NowCount = (int)theModel.Shell_ErrCount;
            mChannel_Info.Update(theModel);
        }


        //设置错误累计次数
        public void Set_ShellErrCount(int ChannelNo, int Num)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            //次数
            theModel.Shell_ErrCount = Num;
            mChannel_Info.Update(theModel);
        }

        //获得错误累计次数
        public int Get_ShellErrCount(int ChannelNo)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            return (int)theModel.Shell_ErrCount;
        }

        //ACIR------------

        //增加错误累计次数,并获得当前次数
        public void Add_ACIRErrCount(int ChannelNo, out int NowCount)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            //累计次数+1
            theModel.ACIR_ErrCount += 1;
            NowCount = (int)theModel.ACIR_ErrCount;
            mChannel_Info.Update(theModel);
        }


        //设置错误累计次数     
        public void Set_ACIRErrCount(int ChannelNo, int Num)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);

            //次数
            theModel.ACIR_ErrCount = Num;
            mChannel_Info.Update(theModel);
        }

        //获得错误累计次数
        public int Get_ACIRErrCount(int ChannelNo)
        {
            Model.Channel_Info theModel;

            //获得原有累计次数
            theModel = mChannel_Info.GetModel(ChannelNo);
            return (int)theModel.ACIR_ErrCount;
        }

    }
}
