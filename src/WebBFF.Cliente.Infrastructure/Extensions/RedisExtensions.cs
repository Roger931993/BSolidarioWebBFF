using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using WebBFF.Cliente.Infrastructure.Caching.Redis;

namespace WebBFF.Cliente.Infrastructure.Extensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Redis").GetValue<string>("ConnectionString");
            });
            services.AddTransient<IRedisCache, RedisCache>();

            return services;
        }
    }
}
