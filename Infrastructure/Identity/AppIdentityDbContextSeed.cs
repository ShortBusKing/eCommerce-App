using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Joseph (CEO)",
                    Email = "josephf@cabuds.com",
                    UserName = "josephf@cabuds.com",
                    Address = new Address
                    {
                        FirstName = "Joseph",
                        LastName = "Fascenda",
                        Street = "555 Main St",
                        City = "Ventura",
                        State = "CA",
                        Zipcode = "93003"
                    }
                };

                await userManager.CreateAsync(user, "password");
            }
        }
    }
}
