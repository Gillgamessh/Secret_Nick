namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.SystemResponses
{
    /// <summary>
    /// Information about the application.
    /// </summary>
    public record AppInfoResponse
    {
        /// <summary>
        /// The current date and time.
        /// </summary>
        /// <example>2025-08-16T12:34:56Z</example>
        public DateTime DateTime { get; init; }

        /// <summary>
        /// The current environment name.
        /// </summary>
        /// <example>Development</example>
        public required string Environment { get; init; }

        /// <summary>
        /// The current assembly version.
        /// </summary>
        /// <example>1.0.0</example>
        public required string Build { get; init; }
    }
}