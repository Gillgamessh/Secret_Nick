using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.ReadDtos
{
    /// <summary>
    /// User read DTO.
    /// </summary>
    public class UserReadDto
    {
        /// <summary>
        /// Unique identifier of the User.
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Date when User was created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date when User was modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Unique identifier of Room User belongs to.
        /// </summary>
        public ulong RoomId { get; set; }

        /// <summary>
        /// Indicates whether User is admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// First name of the User.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Last name of the User.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Auth security code of User.
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// User's phone number.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// User's email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Unique identifier of recipient User.
        /// </summary>
        public ulong? GiftToUserId { get; set; }

        /// <summary>
        /// Delivery info for receiving Gift by User.
        /// </summary>
        public string? DeliveryInfo { get; set; }

        /// <summary>
        /// Indicates whether User wants surprise instead of selection from Wish List.
        /// </summary>
        public bool? WantSurprise { get; set; }

        /// <summary>
        /// Interests of User in case when he/she wants surprise.
        /// </summary>
        public string? Interests { get; set; }

        /// <summary>
        /// List of desired gifts in case when User doesn't want surprise.
        /// </summary>
        public IEnumerable<WishDto>? WishList { get; set; }
    }
}