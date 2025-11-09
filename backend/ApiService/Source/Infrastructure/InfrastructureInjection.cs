using System.Diagnostics.CodeAnalysis;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper;
using Epam.ItMarathon.ApiService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Epam.ItMarathon.ApiService.Infrastructure
{
    /// <summary>
    /// Infrastructure layer injection and setup static class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class InfrastructureInjection
    {
        /// <summary>
        /// Extension method for more fluent setup. This is where all required configuration for Infrastructure layer happens.
        /// </summary>
        public static void InjectInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseNpgsql(configuration.GetConnectionString("DbConnectionString"));
            });
            services.ConfigureMapper();
            services.ConfigureRepositories();
        }

        private static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new GiftMappingProfile());
                config.AddProfile(new UserMappingProfile());
                config.AddProfile(new RoomMappingProfile());
            });
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
        }
    }
}