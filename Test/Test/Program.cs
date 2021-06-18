using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.WriteLog("AAAA");
            Console.ReadKey();

        }
    }
}
