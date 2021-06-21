using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyContext context = new MyContext();
            //var empList = context.Persons.OrderBy(c => c.FirstName).ToList();
            //Console.WriteLine(empList.Count);

            Person people = new Person()
            {
                FirstName = "Hello",
                LastName = "World"
            };
            context.Persons.Add(people);
            context.SaveChanges();
            Console.ReadLine();
        }
    }
}
