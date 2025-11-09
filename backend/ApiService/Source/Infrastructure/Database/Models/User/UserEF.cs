using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User
{
    internal class UserEf : BaseModelEf
    {
        /// <summary>
        /// Unique identifier of Room User belongs to.
        /// </summary>
        public ulong RoomId { get; set; }

        /// <summary>
        /// Authorization code of User.
        /// </summary>
        public required string AuthCode { get; set; }

        /// <summary>
        /// User's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Phone of User.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// User's email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Delivery info for receiving Gift by User.
        /// </summary>
        public required string DeliveryInfo { get; set; }

        /// <summary>
        /// Unique identifier of User which current User targets to gift.
        /// </summary>
        public ulong? GiftRecipientUserId { get; set; }

        /// <summary>
        /// Represents user's wish to have a surprise.
        /// </summary>
        public bool WantSurprise { get; set; }

        /// <summary>
        /// List of desired gifts in case when User doesn't want surprise.
        /// </summary>
        public string? Interests { get; set; }

        /// <summary>
        /// Mapping property to a Room which User belongs to.
        /// </summary>
        public RoomEf Room { get; set; } = default!;

        /// <summary>
        /// Mapping property to a Room where the User is admin, if he is.
        /// </summary>
        public RoomEf? AdminRoom { get; set; }

        /// <summary>
        /// Number of gifts that User want to receive.
        /// </summary>
        public ICollection<GiftEf> Wishes { get; set; } = default!;

        /// <summary>
        /// User that current user chose to give.
        /// </summary>
        public UserEf? GiftRecipientUser { get; set; }

        /// <summary>
        /// User from which the current User will receive the gift.
        /// </summary>
        public UserEf? GiftSenderUser { get; set; }
    }
}