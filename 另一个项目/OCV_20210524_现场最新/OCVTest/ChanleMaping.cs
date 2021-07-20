using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class ChanleMaping
    {
        /// <summary>
        /// 行 1-43
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 列 1-6
        /// </summary>
        public int ColunmIndex { get; set; }
        /// <summary>
        /// 托盘通道号
        /// </summary>
        public int TrayCodeChanle { get; set; }
        /// <summary>
        /// 设备通道号
        /// </summary>
        public int DeviceChanle { get; set; }
        public double OCV { get; set; }

        public double ACIR { get; set; }

        public string CODE { get; set; }

        public string BatCode { get; set; }
    }

    public static class ChanleMapingSetting
    {
        static List<ChanleMaping> _ListBatTestData;

        /// <summary>
        /// 设备通道为索引
        /// </summary>
        public static Dictionary<int , ChanleMaping> DicDevIndexMaping
        {
            get
            {
                return _ListBatTestData.ToDictionary(a => a.DeviceChanle, b => b);
            }
        }

        /// <summary>
        /// 托盘通道为索引
        /// </summary>
        public static Dictionary<int,ChanleMaping> DicTrayIndexMaping
        {
            get
            {
                return _ListBatTestData.ToDictionary(a => a.TrayCodeChanle, b => b);
            }
        }

        public static List<ChanleMaping> ListBatTestData
        {
            get
            {
                if (_ListBatTestData == null)
                {
                    _ListBatTestData = CreateMaping();
                    _ListBatTestData = _ListBatTestData.OrderBy(a => a.TrayCodeChanle).ToList();
                }
                return _ListBatTestData;
            }
        }
        private static List<ChanleMaping> CreateMaping()
        {
            List<ChanleMaping> result = new List<ChanleMaping>();
            //for (int i = 0; i < ClsGlobal.BatInfoCount; i++)
            //{
            //    result.Add(new ChanleMaping() { TrayCodeChanle = i });
            //}
            #region 20200702 zxz 初始化通道映射表
            for (int i = 0; i < 40; i++)
            {
                ChanleMaping itm1 = new ChanleMaping();
                itm1.TrayCodeChanle = 1 + (i * 6);
                itm1.DeviceChanle = 161 + (i * 2);
                result.Add(itm1);
                ChanleMaping itm2 = new ChanleMaping();
                itm2.TrayCodeChanle = 2 + (i * 6);
                itm2.DeviceChanle = 162 + (i * 2);
                result.Add(itm2);
            }

            for (int i = 0; i < 40; i++)
            {
                ChanleMaping itm1 = new ChanleMaping();
                itm1.TrayCodeChanle = 3 + (i * 6);
                itm1.DeviceChanle = 81 + (i * 2);
                result.Add(itm1);
                ChanleMaping itm2 = new ChanleMaping();
                itm2.TrayCodeChanle = 4 + (i * 6);
                itm2.DeviceChanle = 82 + (i * 2);
                result.Add(itm2);
            }

            for (int i = 0; i < 40; i++)
            {
                ChanleMaping itm1 = new ChanleMaping();
                itm1.TrayCodeChanle = 5 + (i * 6);
                itm1.DeviceChanle = 1 + (i * 2);
                result.Add(itm1);
                ChanleMaping itm2 = new ChanleMaping();
                itm2.TrayCodeChanle = 6 + (i * 6);
                itm2.DeviceChanle = 2 + (i * 2);
                result.Add(itm2);
            }

            for (int i = 241; i <= 256; i++)
            {
                ChanleMaping itm = new ChanleMaping();
                itm.TrayCodeChanle = i;
                itm.DeviceChanle = i;
                result.Add(itm);
            }
            foreach (var m in result)
            {
                m.RowIndex = ((m.TrayCodeChanle - 1) / 6) + 1;
                if (m.TrayCodeChanle >= 253)
                {
                    m.ColunmIndex = ((m.TrayCodeChanle - 1) % 6) + 2;
                }
                else
                {
                    m.ColunmIndex = ((m.TrayCodeChanle - 1) % 6) + 1;
                }
            }

            #endregion
            return result;
        }
    }

}
