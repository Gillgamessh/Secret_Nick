namespace Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension
{
    /// <summary>
    /// Metadata for the minimal api endpoint group. Adds description to the target group.
    /// </summary>
    /// <param name="Name">Target group.</param>
    /// <param name="Description">Description of the group.</param>
    public sealed record SwaggerTagMetadata(string Name, string Description);
}