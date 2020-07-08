using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3.Interfaces;
using WebApplication3.Model;

namespace WebApplication3.Implementation
{
    public class ClaimsFactory  : UserClaimsPrincipalFactory<AppUser> 
    {
        public ClaimsFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager,optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            user.FirstName = "SuperAdmin";
            user.LastName = "SuperAdmin";
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("firstname", user.FirstName));
            identity.AddClaim(new Claim("lastname", user.LastName));

            return identity;
        }
    }
}
