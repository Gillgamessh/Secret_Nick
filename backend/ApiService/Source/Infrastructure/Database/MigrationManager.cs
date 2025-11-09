using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database
{
    /// <summary>
    /// Automatic migration applying while startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MigrationManager
    {
        /// <summary>
        /// Extension method to apply migration.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                appContext.Database.Migrate();
            }
            catch (Exception exception)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
                logger.LogError(exception, "An error occurred while migrating the database.");
                throw;
            }
        }
    }
}