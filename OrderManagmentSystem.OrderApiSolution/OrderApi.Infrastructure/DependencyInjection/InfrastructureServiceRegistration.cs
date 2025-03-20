using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Mapping;
using OrderApi.Domain.Interfaces;
using OrderApi.Infrastructure.Data;
using OrderApi.Infrastructure.Repositories;
using OrderManagment.SharedLibrary.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //add services from shared library
            services.AddSharedServices<OrderDataContext,OrderMappingProfile>(configuration);

            services.AddScoped<IOrder, OrderRepository>();

            return services;
        }


        public static IApplicationBuilder UserInfrastructurePolicies(this IApplicationBuilder app)
        {
            app.UseSharedPolicies();
            return app;
        }

    }
}
