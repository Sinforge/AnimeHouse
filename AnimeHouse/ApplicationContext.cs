using Microsoft.EntityFrameworkCore;
using AnimeHouse.Models;
using AnimeHouse.Data;
namespace AnimeHouse
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=Vladik_1;database=animehouse;",
                new MySqlServerVersion(new Version(8, 0, 28))
            );
        }
    }
}
