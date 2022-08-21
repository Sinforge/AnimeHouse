using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnimeHouse.Models;
using Microsoft.AspNetCore.Identity;

namespace AnimeHouse.Data
{

	public class ApplicationContext : IdentityDbContext<User>
	{
		public DbSet<Anime> Animes { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

     
		
    }

}

