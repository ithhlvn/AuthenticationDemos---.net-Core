using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomAuthHandler.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private ICustomAuthManager authManager;

        public ValidateController(ICustomAuthManager authManager)
        {
            this.authManager = authManager;
        }

        // POST api/<ValidateController>
        [AllowAnonymous]
        [HttpPost("validate")]
        public void Post([FromBody] UserCred cred)
        {
           var token =   authManager.Authenticate(cred.UserName, cred.Password);
            if (string.IsNullOrEmpty(token))
                Unauthorized("Invalid UN / PWD");
            else
                Ok(token);
        }
    }

    public class UserCred 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
