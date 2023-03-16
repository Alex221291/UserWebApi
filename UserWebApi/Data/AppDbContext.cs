using Microsoft.EntityFrameworkCore;
using UserWebApi.Models;

namespace UserWebApi.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
