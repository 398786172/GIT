using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCS.Common
{
    
   public class MultimeterDescriptionAttribute:Attribute
    {
        public string Name { get; set; }
        public string Driver { get; set; }
        public MultimeterDescriptionAttribute(string name,string driver)
        {
            this.Name = name;
            this.Driver = driver;
        }
    }
}
