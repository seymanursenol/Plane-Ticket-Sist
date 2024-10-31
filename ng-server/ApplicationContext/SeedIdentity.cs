using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ng_server.ApplicationContext
{
    public class SeedIdentity
    {
        public static async Task Seed(UserManager<Users> userManager, RoleManager<IdentityRole> 
            roleManager,IConfiguration configuration)
        {
              var username = configuration["Data:AdminUser:username"];  
              var email = configuration["Data:AdminUser:email"];  
              var password = configuration["Data:AdminUser:password"];  
              var role = configuration["Data:AdminUser:role"];  

              if(await userManager.FindByNameAsync(username)==null)
              {
                  await roleManager.CreateAsync(new IdentityRole(role));

                  var user = new Users()
                  {
                      UserName = username,
                      Email = email,
                      Password = password,
                      EmailConfirmed = true
                  };
                  var result = await userManager.CreateAsync(user,password);
                  if(result.Succeeded)
                  {
                      await userManager.AddToRoleAsync(user,role);
                  }
              }
        }
    }
}