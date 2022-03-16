namespace ActiveForum.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await WebAssemblyHostBuilder
                .CreateDefault(args)
                .UseStartup()
                .Build()
                .RunAsync();

            return 0;
        }

        public static WebAssemblyHostBuilder UseStartup(this WebAssemblyHostBuilder builder)
        {
            var startup = new ActiveForum.Client.Startup();
            startup.Configure(builder);
            startup.ConfigureServices(builder.Services, builder.HostEnvironment);
            
            return builder;
        }
    }
}
