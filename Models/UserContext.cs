using Microsoft.EntityFrameworkCore;

namespace MPBackends.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }
    }
}
