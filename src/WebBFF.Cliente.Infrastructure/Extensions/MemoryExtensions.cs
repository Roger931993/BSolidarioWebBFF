using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using WebBFF.Cliente.Infrastructure.Caching.Memory;

namespace WebBFF.Cliente.Infrastructure.Extensions
{
    public static class MemoryExtensions
    {
        public static IServiceCollection AddMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar MemoryCache
            services.AddMemoryCache();
            // Registrar tus servicios
            services.AddSingleton<IMemoryCacheLocalService, MemoryCacheLocalService>();

            return services;
        }
    }
}
