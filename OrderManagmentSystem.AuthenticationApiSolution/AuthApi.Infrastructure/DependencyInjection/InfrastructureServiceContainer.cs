using AuthApi.Application.Mapping;
using AuthApi.Domain.Interfaces;
using AuthApi.Infrastructure.Data;
using AuthApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagment.SharedLibrary.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSharedServices<UserDataContext, UserMappingProfile>(configuration);

            services.AddScoped<IUser, UserRepository>();
            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
        {
            app.UseSharedPolicies();
            return app;
        }
    }
}
