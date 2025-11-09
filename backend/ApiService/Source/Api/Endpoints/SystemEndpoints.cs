using System.Reflection;
using Epam.ItMarathon.ApiService.Api.Dto.Responses.SystemResponses;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;

namespace Epam.ItMarathon.ApiService.Api.Endpoints
{
    /// <summary>
    /// Static class containing system endpoints.
    /// </summary>
    public static class SystemEndpoints
    {
        /// <summary>
        /// Static method to map system-related endpoints to DI container.
        /// </summary>
        /// <param name="application">The WebApplication instance.</param>
        /// <returns>Reference to input <paramref name="application"/>.</returns>
        public static WebApplication MapSystemEndpoints(this WebApplication application)
        {
            var root = application.MapGroup("/api/system")
                .WithTags("System")
                .WithTagDescription("System", "System wide endpoints")
                .WithOpenApi();

            _ = root.MapGet("/info", GetSystemInfo)
                .Produces<AppInfoResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get application system info.")
                .WithDescription("Returns system app info. Useful for health checks.");

            return application;
        }

        /// <summary>
        /// Returns application system info.
        /// </summary>
        /// <returns>Returns <seealso cref="IResult"/> depending on operation result.</returns> 
        public static IResult GetSystemInfo(IWebHostEnvironment environment)
        {
            return Results.Ok(new AppInfoResponse
            {
                DateTime = DateTime.UtcNow,
                Environment = environment.EnvironmentName,
                Build = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3)
            });
        }
    }
}