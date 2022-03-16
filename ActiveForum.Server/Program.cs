namespace ActiveForum.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await CreateHostBuilder(args)
                .Build()
                .UpdateActiveForumDatabase(createDatabaseIfNotExist: true)
                .RunAsync();

            return 0;
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder(args);

            webHostBuilder.UseStartup<ActiveForum.Server.Startup>();
            return webHostBuilder;
        }
    }
}
