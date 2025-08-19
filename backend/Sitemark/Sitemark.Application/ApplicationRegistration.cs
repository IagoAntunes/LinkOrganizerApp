using Sitemark.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Sitemark.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services
        )
        {
            return services;
        }

    }
}
