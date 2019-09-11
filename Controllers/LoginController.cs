using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Osiris_Movie_WebApp.Interface;
using Osiris_Movie_WebApp.Models;

namespace Osiris_Movie_WebApp.Controllers
{
    [Route("api/Login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
        }

        // POST: api/Login
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost("{UserLogin}")]
        [HttpPost]
        public IActionResult UserLogin([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = _login.UserLogin(login);
            if (user != null)
            {
                var tokenString = _login.GenerateWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
    }
}
