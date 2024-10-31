using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Identity;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Data.Concrete;
using ng_server.Models;

namespace Business.Concrete
{
    #nullable disable
    public class AuthManager : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<Users> _userManager;

        public AuthManager(UserManager<Users> userManager, IAuthRepository authRepository, IdentityContext context,UserManager<Users> userManagr){
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async  Task<ILoginResponse> UserLogin(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token =  await _authRepository.Login(model);
            return new ILoginResponse()
            {
                Token = token,
                UserName= user.UserName
            };
        }

        private object GenerateJwtToken(Users user, IList<string> roles)
        {
            throw new NotImplementedException();
        }

        public async Task Register(UserRegistration model)
        {  
            await _authRepository.Register(model);
        }

    }

}