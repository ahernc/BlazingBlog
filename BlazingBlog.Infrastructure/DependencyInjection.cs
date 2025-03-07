using BlazingBlog.Application.Authentication;
using BlazingBlog.Domain.Articles;
using BlazingBlog.Domain.Users;
using BlazingBlog.Infrastructure.Authentication;
using BlazingBlog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingBlog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")
                ));

            AddAuthentication(services);

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Not sure if we need this
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();

            return services;
        }


        // To do: investigate what the out-of-the-box code would have given us here
        private static void AddAuthentication(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // This deviates from what is in the out-of-the-box code for a web application
            // with Identity. See the POC Web App for the standard code. Would it have made much difference?

            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddCascadingAuthenticationState();
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

        }
    }
}
