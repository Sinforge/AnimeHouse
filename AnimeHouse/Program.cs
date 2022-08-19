using AnimeHouse.Data;
using AnimeHouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 5;
	options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
	options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
	options.Password.RequireDigit = false; // требуются ли цифры
})
	.AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddMvc();

var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
    {
		var userManager = services.GetService<UserManager<User>>();
		var roleManager = services.GetService<RoleManager<IdentityRole>>();
		await RoleInitializer.InitializeAsync(userManager, roleManager);
    }
	catch(Exception ex)
    {
		var _logger = services.GetService<ILogger<Program>>();
		_logger.LogError("An error occurred while seeding the database.");
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
