namespace Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension
{
    /// <summary>
    /// Static class to implement fluent like declarative OpenAPI description to the group
    /// </summary>
    public static class RouteHandlerBuilderExtensions
    {
        /// <summary>
        /// Declarative extension method that adds metadata to the endpoint. 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name">The name of the group (should be the same as in WithTags() method)</param>
        /// <param name="description">Description of the group</param>
        /// <returns>Returns reference to input <paramref name="builder"/>.</returns>
        public static RouteGroupBuilder WithTagDescription(
            this RouteGroupBuilder builder, string name, string description)
        {
            builder.WithMetadata(new SwaggerTagMetadata(name, description));
            return builder;
        }
    }
}