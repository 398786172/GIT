using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace OCV.OCVTest
{
    public class AutoClearLogs
    {
        private static Thread thClear = null;
        private static readonly AutoClearLogs _instance = new AutoClearLogs();
        private string _logPath;
        private string _dataPath;
        private AutoClearLogs()
        {
            _logPath = Application.StartupPath + "\\Logs";
            _dataPath = Application.StartupPath + "\\ExportStepData";
        }

        public static AutoClearLogs Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 开启清理工作
        /// </summary>
        public void StartClear()
        {
            if (thClear == null)
            {
                thClear = new Thread(ClearWork);
                thClear.IsBackground = true;
                thClear.Start();
            }
            else
            {
                if (thClear.IsAlive == false)
                {
                    thClear = new Thread(ClearWork);
                    thClear.IsBackground = true;
                    thClear.Start();
                }
            }
        }

        /// <summary>
        /// 停止清理工作
        /// </summary>
        public void StopClear()
        {
            if (thClear == null)
                return;
            if (thClear.IsAlive)
            {
                try
                {
                    thClear.Abort();
                    thClear = null;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 清理工作
        /// </summary>
        private void ClearWork()
        {
            while (true)
            {
                try
                {
                    string[] filesLog = Directory.GetFiles(_logPath, "*.txt", SearchOption.AllDirectories);
                    string[] filesData = Directory.GetFiles(_dataPath, "*.csv", SearchOption.AllDirectories);
                    string[] allFile = filesLog.Union(filesData).ToArray();
                    var list = StringToFileInfo(allFile).ToList();
                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            try
                            {
                                item.Delete();
                            }
                            catch
                            {
                            }
                        }
                    }

                }
                catch
                {
                }
                Thread.Sleep(15 * 60 * 1000);
            }
        }


        private IEnumerable<FileInfo> StringToFileInfo(string[] allFilePath)
        {
            foreach (var item in allFilePath)
            {
                FileInfo info = new FileInfo(item);
                if (info.LastWriteTime <= DateTime.Now.AddDays(-60))
                {
                    yield return info;
                }

            }
        }
    }
}
