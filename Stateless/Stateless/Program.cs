using System;
using S;

namespace Stateless
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Leave leave=new Leave()
            {
                FirSaveTime=DateTime.Now,
                Id=1,
                PassTime = DateTime.Now,
                Status=LeaveStatus.Draft,
                
            };
            LeaveManager leaveManager=new LeaveManager(leave);
            leaveManager.Execute(LeaveAction.Submit);
            Console.WriteLine(leave.Status.ToString());
            Console.ReadKey();

        }
    }
}
