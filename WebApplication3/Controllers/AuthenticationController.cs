using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Model;
using WebApplication3.Implementation;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationBusiness _authenticateBusiness;
     

        public AuthenticationController(AuthenticationBusiness authenticationBusiness)
        {
     
            _authenticateBusiness = authenticationBusiness;
        }
        
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {          
           var user = _authenticateBusiness.Authenticate(model.Email, model.Password, model.RememberMe);

            if (user == null)
            {
                return BadRequest(new { message = "Emailid or password is incorrect" });
            }
            return Ok(user);
        }
    }
}


    public class AuthenticateModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }


