using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tweetbook.Seeds
{
    public class RolesSeed
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Poster"))
            {
                await roleManager.CreateAsync(new IdentityRole("Poster"));
            }
        }
    }
}