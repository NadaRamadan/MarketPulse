using API_FEB.Data;
using Microsoft.EntityFrameworkCore;

namespace API_FEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔥 Configure Logging (Console + Debug)
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // ✅ Add Services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 🔥 Add CORS (Cross-Origin Resource Sharing)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // 🔥 Add Database Context with Error Handling
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("❌ Connection string 'DefaultConnection' is missing in appsettings.json!");
            }

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure())); // Prevents crashes if DB connection fails temporarily

            var app = builder.Build();

            // ✅ Apply Migrations Automatically (Error Handling Added)
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();  // Automatically apply migrations
                    Console.WriteLine("✅ Database migrated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Database migration failed: {ex.Message}");
                }
            }

            // ✅ Configure the Middleware Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // 🔥 Enable CORS
            app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
