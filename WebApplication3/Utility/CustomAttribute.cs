using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Logging;
using WebApplication3.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApplication3.Utility
{

    public class CustomAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        public string RoleName { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string[] roles = new string[] { };
             
                bool isAuthenticated = false;
                var identityUser = context.HttpContext.User.Identity;

                if (identityUser != null)
                {
                    if (identityUser.IsAuthenticated && !string.IsNullOrEmpty(RoleName))
                    {
                        roles = RoleName.Split(",");
                        var userClaims = (ClaimsIdentity)identityUser;

                        var userRole = userClaims.Claims.Where(x => roles.Contains(x.Value)).Select(a => a.Value).SingleOrDefault();

                        if ((userClaims == null || userRole == null))
                        {
                            isAuthenticated = false;
                        }
                    }
                    if (isAuthenticated)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}