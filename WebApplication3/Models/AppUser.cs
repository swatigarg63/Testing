using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    [Table("AspNetUser")]
    public class AppUser : IdentityUser<int>
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string SMSPhoneNumber { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
