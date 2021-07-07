using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCV;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ClsHIOKI365X clsHioki365=new ClsHIOKI365X("COM6");
            clsHioki365.InitControl_IMM(2);
            double sVal;
            clsHioki365.ReadData(out  sVal);
            Console.WriteLine(sVal);
            Console.ReadLine();
        }
    }
}
