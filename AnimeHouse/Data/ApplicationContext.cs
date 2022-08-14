using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnimeHouse.Models;

namespace AnimeHouse.Data
{

	public class ApplicationContext : IdentityDbContext<User>
	{
		public DbSet<Anime> Animes { get; set; } = null!;
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

	}
}

