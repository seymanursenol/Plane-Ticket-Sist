using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.ApplicationContext;
using ng_server.Models;

namespace ng_server.Data.Abstract
{
    public interface IAuthRepository: IRepository<Users>
    {
        Task Register(UserRegistration model);
        Task<string> Login(UserLoginDTO model);
    }
}