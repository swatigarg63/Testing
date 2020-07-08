using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    [Table("AspNetUserRoles")]
    public class UserRoles : IdentityUserRole<int>
    {
        public int Id { get; set; }
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
