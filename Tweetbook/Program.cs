using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tweetbook.Services;

namespace Tweetbook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();
                await SeedUsers(userManager);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            var admin = new IdentityUser
            {
                UserName = "admin@tweetbook.com",
                Email = "admin@tweetbook.com"
            };

            var poster = new IdentityUser
            {
                UserName = "poster@tweetbook.com",
                Email = "poster@tweetbook.com"
            };

            if (await userManager.FindByEmailAsync(admin.Email) != null)
            {
                return;
            }
            
            if ((await userManager.CreateAsync(admin, "Password123#")).Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if ((await userManager.CreateAsync(poster, "Password123#")).Succeeded)
            {
                await userManager.AddToRoleAsync(poster, "Poster");
            }
        }
    }
}