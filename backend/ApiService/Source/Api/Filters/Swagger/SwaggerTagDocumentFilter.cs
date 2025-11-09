using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Epam.ItMarathon.ApiService.Api.Filters.Swagger
{
    /// <summary>
    /// Filter for the swagger documentation (post-handling). Handles custom metadata. <see cref="SwaggerTagMetadata"/>
    /// </summary>
    public class SwaggerTagDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Handling of the metadata. And Tags generating based/modification based on it.
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tagMetadata = context.ApiDescriptions
                .SelectMany(description => description.ActionDescriptor.EndpointMetadata
                    .OfType<SwaggerTagMetadata>())
                .GroupBy(tag => tag.Name)
                .Select(group => new OpenApiTag
                {
                    Name = group.Key,
                    Description = group.First().Description
                });

            swaggerDoc.Tags = swaggerDoc.Tags?.Concat(tagMetadata).ToList() ?? tagMetadata.ToList();
        }
    }
}