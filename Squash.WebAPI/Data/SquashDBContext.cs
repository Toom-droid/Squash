using Microsoft.EntityFrameworkCore;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Data
{
    public class SquashDBContext(DbContextOptions<SquashDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>();
        }


    }
}
