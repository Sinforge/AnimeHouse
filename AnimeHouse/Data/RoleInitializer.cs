using AnimeHouse.Models;
using Microsoft.AspNetCore.Identity;
namespace AnimeHouse.Data
{
    public class RoleInitializer
    {
        private const string AdminEmail = "vlad.vlasov77@mail.ru";
        private const string AdminNickname = "SinForgeAdmin";
        private const string password = "SuperVlad_1234";
        private static readonly string[] Categories = new string[] { "Games", "Detective", "Military", "Mystic", "Romance", "Supernatural", "Sport", "School", "Thriller",
            "Fantasy", "Horror", "Everyday life", "Music", "Magic", "Comedy" };

    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext _db)
        {
            if(await roleManager.FindByIdAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));   
            }
            if(await roleManager.FindByIdAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if(await roleManager.FindByIdAsync("moderator") == null) {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            }

            if(await userManager.FindByNameAsync("SinForgeAdmin") == null)
            {
                User admin = new User {Email = AdminEmail, UserName = AdminNickname};
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            foreach (var category in Categories)
            {
                if (_db.Categories.FirstOrDefault(cat => cat.Name == category) == null)
                {
                    _db.Categories.Add(new Category { Name = category });
                }
            }

            _db.SaveChanges();
        }
    }
}
