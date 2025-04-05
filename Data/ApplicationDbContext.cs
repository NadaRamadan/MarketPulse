using API_FEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API_FEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> // ✅ Use custom User model
    {
        // DbSets for User and Campaign
        public DbSet<Campaign> Campaigns { get; set; } // Add DbSet for Campaign

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ✅ Ensure Identity is properly configured

            // Configure the User entity (Fluent API)
            modelBuilder.Entity<User>(entity =>
            {
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

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()"); // Default value at creation

                entity.Property(u => u.UpdatedAt)
                      .HasDefaultValueSql("GETUTCDATE()"); // Default value at creation
            });

            // Configure the Campaign entity (Fluent API)
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100); // Ensure Name is required with max length

                entity.Property(c => c.Description)
                      .HasMaxLength(500); // Optional: Set max length for Description

                entity.Property(c => c.StartDate)
                      .IsRequired(); // Ensure StartDate is required

                entity.Property(c => c.EndDate)
                      .IsRequired(); // Ensure EndDate is required

                entity.Property(c => c.IsActive)
                      .HasDefaultValue(true); // Set default IsActive to true

                entity.HasIndex(c => c.Name)
                      .IsUnique(); // Optional: Ensure campaign names are unique
            });
        }

        // ✅ Automatically update UpdatedAt before saving changes
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<User>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
