using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tweetbook.Seeds
{
    public class UsersSeed
    {
        public static async Task Seed(UserManager<IdentityUser> userManager)
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