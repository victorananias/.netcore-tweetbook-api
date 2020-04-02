using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tweetbook.Seeds;

namespace Tweetbook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await SeedIdentity(host);

            host.Run();
        }

        private static async Task SeedIdentity(IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            
            var roleManager = serviceScope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceScope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            await RolesSeed.Seed(roleManager);
            await UsersSeed.Seed(userManager);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}