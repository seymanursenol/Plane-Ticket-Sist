using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.ApplicationContext;
using ng_server.Models;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task Register(UserRegistration model);
        Task<ILoginResponse> UserLogin(UserLoginDTO model);
    }
}