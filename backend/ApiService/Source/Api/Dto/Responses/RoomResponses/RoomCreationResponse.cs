using Epam.ItMarathon.ApiService.Api.Dto.ReadDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses
{
    /// <summary>
    /// Room creation response.
    /// </summary>
    public class RoomCreationResponse
    {
        /// <summary>
        /// Room reading DTO.
        /// </summary>
        public required RoomReadDto Room { get; set; }

        /// <summary>
        /// Admin user authorization code.
        /// </summary>
        public required string UserCode { get; set; }
    }
}