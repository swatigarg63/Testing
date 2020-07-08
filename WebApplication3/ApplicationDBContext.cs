using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.Models;

namespace WebApplication3
{
    public class ApplicationDBContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>,
                                                UserRoles, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        //public DbSet<AppRole> Roles { get; set; }
        //public DbSet<AppUser> Users { get; set; }
        public DbSet<SalesInfo> SalesInfo { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
