using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FakeXiecheng.API.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
        // ShoppingCart
        public ShoppingCart ShoppingCart { get; set; }
        // Order
        public ICollection<Order> Orders { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
        //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        //public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    }
}
