namespace ActiveForum.Database.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using FluentMigrator.Runner;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ActiveForumDbExtension
    {
        public static IServiceCollection AddActiveForumDatabase(
            this IServiceCollection services, 
            string connectionString)
        {
            services.AddDbContext<ActiveForumDbExtension.EmptyDbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddDbContext<ActiveForumDbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                {
                    builder.AddSqlServer()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(ActiveForumDbContext).Assembly).For.Migrations();
                })
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            
            return services;
        }         

        public static IWebHost UpdateActiveForumDatabase(
            this IWebHost webHost,
            bool createDatabaseIfNotExist = true)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                if (createDatabaseIfNotExist)
                {
                    var emptyDbContext = scope.ServiceProvider.GetService<ActiveForumDbExtension.EmptyDbContext>();
                    emptyDbContext.Database.EnsureCreated();
                }

                var migrationRunner = scope.ServiceProvider.GetService<IMigrationRunner>();
                migrationRunner.MigrateUp();
            }
             
            return webHost;
        }         

        private class EmptyDbContext : DbContext
        {
            public EmptyDbContext(DbContextOptions<EmptyDbContext> options)
                : base(options)
            {
            }
        }
    }
}
