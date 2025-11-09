using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    /// <summary>
    /// Creating request for the Room.
    /// </summary>
    public class RoomCreationRequest
    {
        /// <summary>
        /// Room creation DTO.
        /// </summary>
        public required RoomCreationDto Room { get; set; }

        /// <summary>
        /// User creation DTO.
        /// </summary>
        public required UserCreationDto AdminUser { get; set; }
    }
}