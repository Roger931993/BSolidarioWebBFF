using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebBFF.Cliente.Application.Common;
using WebBFF.Cliente.Application.Interfaces.Base;
using WebBFF.Cliente.Application.Mappings;

namespace WebBFF.Cliente.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApplicationMappingProfile));

            services.AddScoped<IErrorCatalogException, ErrorCatalogException>();

            services.AddMediatR(gfc => gfc.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));            

            return services;
        }
    }
}
