using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Osiris_Movie_WebApp.Interface;
using Osiris_Movie_WebApp.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Osiris_Movie_WebApp.Service
{
    public class LoginService : ILogin
    {
        private IConfiguration _config;

        public LoginService(IConfiguration config)
        {
            _config = config;
        }

        public UserModel UserLogin(UserModel login)
        {
            UserModel user = null;
            if (login.Username == "admin")
            {
                user = new UserModel { Username = "admin", Email = "admin" };
            }
            return user;
        }
         
        public object GenerateWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
