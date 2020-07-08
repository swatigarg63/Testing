using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    [Table("AspNetRoles")]
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
