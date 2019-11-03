using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IServiceProvider serviceProvider)
            : base(options)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureDbContext(serviceProvider);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Items> Items { get; set; }
    }

    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
    }

    public static class TenantBasedDbContextExtensions
    {
        public static IServiceCollection AddSharedDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Data:ApplicationDbContext:ConnectionString"];
            services.AddScoped(s => new MySqlConnection(connectionString));
            return services;
        }

        public static void ConfigureDbContext(this DbContextOptionsBuilder optionsBuilder,
           IServiceProvider serviceProvider)
        {
            //var environment = serviceProvider.GetRequiredService<IHostingEnvironment>();
            //if (environment.IsDevelopment())
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseMySql(serviceProvider.GetRequiredService<MySqlConnection>(),
                mySqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("WebApplication6");
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 4,
                        maxRetryDelay: TimeSpan.FromMilliseconds(2000),
                        errorNumbersToAdd: null);
                });
        }
    }
}
