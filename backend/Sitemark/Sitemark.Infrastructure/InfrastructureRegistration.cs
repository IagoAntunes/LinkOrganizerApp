using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitemark.Application.Services;
using Sitemark.Domain.Repositories;
using Sitemark.Infrastructure.Data;
using Sitemark.Infrastructure.Repositories;
using Sitemark.Infrastructure.Services;

namespace Sitemark.Infrastructure
{
    public static class InfrastructureRegistration
    {

        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();


            services.AddDbContext<SitemarkDbContext>(
                    options => options.UseSqlServer(
                    configuration.GetConnectionString("SitemarkConnectionString")
                )
            );

            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Sitemark")
                .AddEntityFrameworkStores<SitemarkDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });



            services.AddDataProtection();

            return services;
        }

    }
}
