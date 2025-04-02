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
