using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Interfaces
{
    public interface IUserRepository
    {
        AppUser GetUserDetail(string email);
    }
}
