using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OCV.PLCContr.Model;
using OCV.PLCContr.DAL;


namespace OCV.PLCContr.BLL
{
    /// <summary>
    /// 
    /// </summary>
   public partial class PLCInfoBLL
    {
        private PLCInfoDAL dal = null;
        private string filePath = Application.StartupPath + "\\Setting\\LocalSetting.db";

        public PLCInfoBLL()
        {
            string strConnect = "Data Source=" + filePath;
            dal = new PLCInfoDAL(strConnect);
        }

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PLCInfoModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PLCInfoModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PLCInfoModel GetModel(int id)
        {

            return dal.GetModel(id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PLCInfoModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PLCInfoModel> DataTableToList(DataTable dt)
        {
            List<PLCInfoModel> modelList = new List<PLCInfoModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PLCInfoModel model = new PLCInfoModel();
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion  BasicMethod
    }
}
