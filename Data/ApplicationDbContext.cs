using API_FEB.Models;
using Microsoft.EntityFrameworkCore;

namespace API_FEB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Define your DbSets (Tables)
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the User entity (Fluent API)
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(u => u.Email)
                      .IsUnique(); // Ensure unique emails

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(20); // Ensure Role column size limit
            });
        }
    }
}
