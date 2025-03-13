using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagment.SharedLibrary.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.SharedLibrary.Extentions
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext,TAssembly>(this IServiceCollection services,
            IConfiguration configuration) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            });

            services.AddAutoMapper(typeof(TAssembly).Assembly);

            services.AddJWTAuthenticationScheme(configuration);
            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            //app.UseMiddleware<ListenToApiGateway>();

            return app;
        }
    }
}
