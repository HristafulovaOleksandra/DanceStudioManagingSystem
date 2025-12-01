using DanceStudio.Reviews.Domain.Interfaces;
using DanceStudio.Reviews.Infrastructure.Configuration;
using DanceStudio.Reviews.Infrastructure.Persistence;
using DanceStudio.Reviews.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DanceStudio.Reviews.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<MongoDbContext>();

            services.AddScoped<IReviewRepository, ReviewRepository>();

            return services;
        }
    }
}