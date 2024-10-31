using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Models;

namespace ng_server.Data.Concrete
{
    public class AuthRepository: Repository<Users, IdentityContext>, IAuthRepository
    {
        private readonly UserManager<Users> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Users> _signInManager;


        public AuthRepository(UserManager<Users> userManager, ICartRepository cartRepository, IdentityContext context, IConfiguration configuration, SignInManager<Users> signInManager){
            _userManager= userManager;
            _cartRepository = cartRepository;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task Register(UserRegistration model)
        {  
            var user = new Users
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if(result.Succeeded)
            {
                _cartRepository.InitializeCart(user.Id);
                await _context.SaveChangesAsync(); 
            }
            else
            {
                throw new Exception("Kullanıcı kaydı başarısız.");
            }
        }
        
        public async Task<string> Login(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                // Console.WriteLine("token:"+token); 
                return token; // Token burada oluşturuluyor
            }else
            {
                 throw new Exception("Kullanıcı girişi başarısız.");
            }
        }

        private async Task<string> GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}