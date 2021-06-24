using OCV.OCVLogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.Tools
{
    public class FileHelper
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="SetReadOnly"></param>
        public static void SetFileReadAccess(string FileName, bool SetReadOnly)
        {

            try
            {
                FileInfo fInfo = new FileInfo(FileName);
                fInfo.IsReadOnly = SetReadOnly;
            }
            catch(Exception ex)
            {
                ClsLogs.OCVInfologNet.WriteFatal(ex.Message);
            }


        }
    }
}
