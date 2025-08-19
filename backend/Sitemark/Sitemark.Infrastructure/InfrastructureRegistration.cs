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

            services.AddDataProtection();

            return services;
        }

    }
}
