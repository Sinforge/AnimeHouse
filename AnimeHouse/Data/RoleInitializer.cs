using Microsoft.AspNetCore.Identity;
namespace AnimeHouse.Data
{
    public class RoleInitializer
    {
        private const string AdminEmail = "vlad.vlasov77@mail.ru";
        private const string AdminNickname = "SinForgeAdmin";
        private const string password = "SuperVlad_1234";

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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
        }
    }
}
