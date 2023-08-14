using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {   
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(config=>
            {
                config.RegisterServicesFromAssemblies(assembly);
            });

            return services;
        }
    }
}
