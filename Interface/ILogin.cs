using Osiris_Movie_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris_Movie_WebApp.Interface
{
   public interface ILogin
    {
        UserModel UserLogin(UserModel login);
        object GenerateWebToken(UserModel login);
    }
}
