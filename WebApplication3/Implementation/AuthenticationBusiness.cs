using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Interfaces;
using WebApplication3.Model;

namespace WebApplication3.Implementation
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepository;

        public AuthenticationBusiness(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }



        public AppUser Authenticate(string email,string password,bool RememberMe)
        {           
            var user = _userManager.FindByEmailAsync(email.Trim());

            if (user.Result == null || user.Result.IsDeleted || !user.Result.IsActive)
            {
                _signInManager.SignOutAsync();
                return null;
            }

            var result = _signInManager.PasswordSignInAsync(email, password, RememberMe, false);
            if (result.Result.Succeeded)
            {              
               return user.Result;
            }
            else
            {
                _signInManager.SignOutAsync();
                return null;
            }
        }
    }
}
