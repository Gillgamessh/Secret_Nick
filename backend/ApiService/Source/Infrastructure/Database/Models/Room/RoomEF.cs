using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room
{
    internal class RoomEf : BaseModelEf
    {
        /// <summary>
        /// Time when room was drawn.
        /// </summary>
        public DateTime? ClosedOn { get; set; }

        /// <summary>
        /// Unique identifier of Room's owner.
        /// </summary>
        public ulong? AdminId { get; set; }

        /// <summary>
        /// Invitation code to the Room.
        /// </summary>
        public required string InvitationCode { get; set; }

        /// <summary>
        /// Minimal amount of Users in the Room for draw.
        /// </summary>
        public uint MinUsersLimit { get; set; } = 3;

        /// <summary>
        /// Maximal amount of Users in the Room.
        /// </summary>
        public uint MaxUsersLimit { get; set; } = 20;

        /// <summary>
        /// Minimal amount of wisher per User in the Room.
        /// </summary>
        public uint MaxWishesLimit { get; set; } = 3;

        /// <summary>
        /// Name of Room.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Description of the Room.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Note included to invite.
        /// </summary>
        public required string InvitationNote { get; set; }

        /// <summary>
        /// Date of gifts to be exchanged.
        /// </summary>
        public DateTime GiftExchangeDate { get; set; }

        /// <summary>
        /// Maximum budged per gift.
        /// </summary>
        public ulong GiftMaximumBudget { get; set; }

        /// <summary>
        /// Mapping property to admin of the Room.
        /// </summary>
        public UserEf? Admin { get; set; }

        /// <summary>
        /// Mapping property to Room's Users.
        /// </summary>
        public ICollection<UserEf> Users { get; set; } = null!;
    }
}