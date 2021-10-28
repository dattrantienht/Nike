using System.Linq;
using System.Threading.Tasks;
using Nike.Infrastructure.Identity;
using Nike.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Nike.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var assistantRole = new IdentityRole("Assistant");

            if (roleManager.Roles.All(r => r.Name != assistantRole.Name))
            {
                await roleManager.CreateAsync(assistantRole);
            }

            var adminUser = new ApplicationUser { UserName = "admin", Email = "admin@nike.com", Role = administratorRole };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                await userManager.CreateAsync(adminUser, "DeusTienDat_3k@");
                await userManager.AddToRolesAsync(adminUser, new[] { administratorRole.Name });
            }

            var assistantUser = new ApplicationUser { UserName = "assistant", Email = "assistant@nike.com", Role = assistantRole };

            if (userManager.Users.All(u => u.UserName != assistantUser.UserName))
            {
                await userManager.CreateAsync(assistantUser, "NonducorDuco_33#");
                await userManager.AddToRolesAsync(assistantUser, new[] { assistantRole.Name });
            }
        }

        public static async Task SeedSampleCityDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary

            if (!context.ProductCategories.Any())
            {
                context.ProductCategories.AddRange(
                new ProductCategory
                {
                    Name = "Jordan",
                    Products =
                        {
                        new Product{Name = "Air Jordan XXXVI SE Luka 'Global Game'"},
                        new Product{Name = "Jordan Delta 2"},
                        new Product{Name = "Jordan Jumpman 2021 PF"},
                        }
                },
                new ProductCategory
                {
                    Name = "Running",
                    Products =
                        {
                        new Product{Name = "Nike Air Zoom Alphafly NEXT%"},
                        new Product{Name = "Nike React Infinity Run Flyknit 2"},
                        new Product{Name = "Nike Revolution 5"},
                        }
                },
                new ProductCategory
                {
                    Name = "Basketball",
                    Products =
                        {
                        new Product{Name = "Zoom Freak 3"},
                        new Product{Name = "PG 5 EP"},
                        new Product{Name = "KD14 EP"},
                        }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
