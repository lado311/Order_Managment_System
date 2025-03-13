using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagment.SharedLibrary.Extentions;
using ProductApi.Domain.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices<TAssembly>(this IServiceCollection services, IConfiguration configuration)
        {
            SharedServiceContainer.AddSharedServices<ProductDataContext, TAssembly>(services, configuration);
            services.AddScoped<IProduct, ProductRepository>();

            return services;
        }

        public static IApplicationBuilder AddInfrastructurePolicies(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            
            return app;
        }
    }
}
