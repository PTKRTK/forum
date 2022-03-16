namespace ActiveForum.Client
{
    using System;
    using System.Net.Http;
    using ActiveForum.Client.Auth;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Client.Repositories;
    using ActiveForum.Client.Repositories.ForumRepositories;
    using ActiveForum.Client.Repositories.UserConnectionsRepositories;
    using ActiveForum.Shared.Interfaces;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Interfaces.UserConnectionsInterfaces;
    using ActiveForum.Shared.Services;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IWebAssemblyHostEnvironment environment)
        {
            services.AddScoped<ISportsBoardRepository, SportsBoardRepository>();
            services.AddScoped<ISportsMenagmentRepository, SportsMenagmentRepository>();
            services.AddScoped<ITopicsBasicInfoRepository, TopicsBasicInfoRepository>();
            services.AddScoped<ITopicsMenagmentRepository, TopicsMenagmentRepository>();
            services.AddScoped<ITopicsCategoriesRepository, TopicsCategoriesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IPostRatingsRepository, PostRatingRepository>();
            services.AddScoped<ITopicRatingsRepository, TopicRatingsRepository>();
            services.AddScoped<IFriendsRepository, FriendsRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IVerificationRepository, VerificationRepository>();

            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddAuthorizationCore();
            services.AddScoped<JWTAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
            services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

            this.ConfigureHttpServices(services, environment);
        }

        public void Configure(WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // tymczasowe
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        }

        private void ConfigureHttpServices(
            IServiceCollection services,
            IWebAssemblyHostEnvironment environment)
        {
            services.AddScoped(provider => new HttpClient
            {
                BaseAddress = new Uri(environment.BaseAddress)
            });
             
            services.AddScoped<IHttpService, HttpService>();
        }
    }
}
