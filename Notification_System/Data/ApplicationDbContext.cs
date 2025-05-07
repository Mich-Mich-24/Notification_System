using Microsoft.EntityFrameworkCore;
using Notification_System.Models;

namespace Notification_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email).IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber).IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.PushId).IsRequired(false);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter("[Email] IS NOT NULL");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique()
                .HasFilter("[PhoneNumber] IS NOT NULL");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PushId)
                .IsUnique()
                .HasFilter("[PushId] IS NOT NULL");
        }
    }
}