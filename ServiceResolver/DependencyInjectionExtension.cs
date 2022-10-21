
using Domain.Entity;
using FluentValidation;
using fluentValidatorExample.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace fluentValidatorExample.ServiceResolver
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ServiceResolver(this IServiceCollection services, IConfiguration configuration)
        {          
            services.AddValidatorsFromAssemblyContaining<HotelValidator>();
            services.AddDbContext<DbTestContext>(options =>
             options.UseSqlServer(configuration["AppCofig:HotelDbContext"]));
            return services;
          
        }
    }
}
