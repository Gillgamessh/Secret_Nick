using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Epam.ItMarathon.ApiService.Application
{
    /// <summary>
    /// Application layer injection and setup static class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ApplicationInjection
    {
        /// <summary>
        /// Extension method for more fluent setup. This is where all required configuration for Application layer happens.
        /// </summary>
        public static void InjectApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}