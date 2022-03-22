using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VY.RebelsExam.Data.Contracts.Repositories;
using VY.RebelsExam.Data.Implementation.Repositories;

namespace VY.RebelsExam.Data.Implementation.Extensions
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRebelRepository, RebelRepository>(c => 
                                                    new RebelRepository(configuration["RebelRepository"])
                                                    );
            return services;
        }
    }
}
