using Login.Application.IReopositories;
using Login.Infrastructure.Data;
using Login.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Login.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("DbConnect")
                ));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
