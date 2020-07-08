using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Interfaces;
using WebApplication3.Model;

namespace WebApplication3.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext context;

        public UserRepository(ApplicationDBContext context) 
        {
            this.context = context;
        }

        public AppUser GetUserDetail(string email)
        {
            return context.Users.AsNoTracking().FirstOrDefault(x => x.Email.Trim().ToLower() == email.Trim().ToLower());
        }
    }
}
