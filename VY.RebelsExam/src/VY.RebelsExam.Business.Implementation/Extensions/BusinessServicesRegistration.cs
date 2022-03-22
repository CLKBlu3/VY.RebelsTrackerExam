using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VY.RebelsExam.Business.Contracts.Services;
using VY.RebelsExam.Business.Contracts.Validations;
using VY.RebelsExam.Business.Implementation.Mapping_Profiles;
using VY.RebelsExam.Business.Implementation.Services;
using VY.RebelsExam.Business.Implementation.Validations;
using VY.RebelsExam.Data.Implementation.Extensions;
using VY.RebelsExam.Dtos.Domain.V1;

namespace VY.RebelsExam.Business.Implementation.Extensions
{
    public static class BusinessServicesRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidation<RebelDto>, RebelValidation>();
            services.AddAutoMapper(typeof(RebelProfile));
            services.AddTransient<IRebelService, RebelService>();
            services.AddDataServices(configuration);
            return services;
        }
    }
}
