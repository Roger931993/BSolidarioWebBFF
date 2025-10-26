using WebBFF.Cliente.Infrastructure.Extensions;
using WebBFF.Cliente.Infrastructure.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebBFF.Cliente.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(InfrastructureMappingProfile).Assembly);
            services.AddMemoryCache(config);
            services.AddRedis(config);            
            services.AddInternalServices(config);

            return services;
        }
    }
}
