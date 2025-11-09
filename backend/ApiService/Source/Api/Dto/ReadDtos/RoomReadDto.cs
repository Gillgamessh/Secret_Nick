using Epam.ItMarathon.ApiService.Api.Dto.BaseDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.ReadDtos
{
    /// <summary>
    /// DTO for reading a Room.
    /// </summary>
    public class RoomReadDto : RoomBaseDto
    {
        /// <summary>
        /// Unique identifier of the Room store.
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Date Room was created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date Room was modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Date when Room was drawn.
        /// </summary>
        public DateTime? ClosedOn { get; set; }

        /// <summary>
        /// Unique identifier of admin User.
        /// </summary>
        public ulong AdminId { get; set; }

        /// <summary>
        /// Code for invitation link.
        /// </summary>
        public required string InvitationCode { get; set; }

        /// <summary>
        /// Invitation text.
        /// </summary>
        public required string InvitationNote { get; set; }

        /// <summary>
        /// Indicates whether there can be no more Users in Room.
        /// </summary>
        public bool IsFull { get; set; }
    }
}