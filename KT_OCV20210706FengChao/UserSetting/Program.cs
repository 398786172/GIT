using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UserSetting
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


   
            if (args.Length == 0)
            {
                Application.Run(new UserSetting(""));
              //  Application.Run(new FormLoad());
            }
            else if(args.Length == 4)
            {

                Application.Run(new UserSetting(args[0], args[1], args[2], args[3]));
            }
            else
            {
                Application.Run(new UserSetting(args[0]));
            }

         
         
        }
    }
}
