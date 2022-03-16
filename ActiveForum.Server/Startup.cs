namespace ActiveForum.Server
{
    using System;
    using System.Linq;
    using System.Text;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Extensions;
    using ActiveForum.Server.SignalR.Hubs;
    using ActiveForum.Shared.Interfaces;
    using ActiveForum.Shared.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IApplicationInfoService, ApplicationInfoService<ActiveForum.Server.Startup>>();
            string connectionString = this.configuration.GetConnectionString("DefaultConnection");

           // string connectionString = "Server=.\\ACTIVEFORUM; Database=ActiveForum; User Id=sa; Password=_instance0; MultipleActiveResultSets=true";
            services.AddActiveForumDatabase(connectionString);

            // nowe
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ActiveForumDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                });

            // koniec
            ConfigureDataBasesServices(services);
            ConfigureAspMvcServices(services);
            services.AddSignalR();
            services.AddResponseCompression(options =>
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "appplication/octet-stream" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // nowe
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // koniec
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }

        private static void ConfigureAspMvcServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        private static void ConfigureDataBasesServices(IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
        }

/*        private string GetConnectionString(string connectionStringKey)
        {
            string connectionString = this.configuration.GetConnectionString(connectionStringKey) ?? string.Empty;

            return connectionString;
        }*/
    }
}
