using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;

namespace Epam.ItMarathon.ApiService.Domain.Entities.User
{
    /// <summary>
    /// User base entity for a domain.
    /// </summary>
    public sealed class User : BaseEntity
    {
        internal static int FirstNameCharLimit = 40;
        internal static int LastNameCharLimit = 40;
        internal static int DeliveryInfoCharLimit = 500;
        internal static int InterestsCharLimit = 1000;

        /// <summary>
        /// User's Room unique identifier.
        /// </summary>
        public ulong RoomId { get; private set; }

        /// <summary>
        /// User's authorization code.
        /// </summary>
        public string AuthCode { get; private set; } = null!;

        /// <summary>
        /// User's first name.
        /// </summary>
        public string FirstName { get; private set; } = null!;

        /// <summary>
        /// User's last name.
        /// </summary>
        public string LastName { get; private set; } = null!;

        /// <summary>
        /// User's mobile phone.
        /// </summary>
        public string Phone { get; private set; } = null!;

        /// <summary>
        /// User's email.
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// Delivery info for receiving Gift by User.
        /// </summary>
        public required string DeliveryInfo { get; set; }

        /// <summary>
        /// Unique identifier of the User, which current User will send a gift.
        /// </summary>
        public ulong? GiftRecipientUserId { get; set; }

        /// <summary>
        /// Indicates whether User wants surprise instead of selection from Wish List.
        /// </summary>
        public bool WantSurprise { get; set; }

        /// <summary>
        /// Interests of User in case when he/she wants surprise.
        /// </summary>
        public string? Interests { get; set; }

        /// <summary>
        /// Whether User an admin or not.
        /// </summary>
        public bool IsAdmin { get; private set; }

        /// <summary>
        /// List of desired gifts in case when User doesn't want surprise.
        /// </summary>
        public required IEnumerable<Wish> Wishes { get; set; }

        private User()
        {
        }

        internal static User InitialCreate(ulong roomId, string authCode,
            string firstName, string lastName, string phone, string? email,
            string deliveryInfo, bool wantSurprise, string? interests, bool isAdmin, IEnumerable<Wish> wishes)
        {
            return new User
            {
                RoomId = roomId,
                AuthCode = authCode,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                DeliveryInfo = deliveryInfo,
                WantSurprise = wantSurprise,
                Interests = interests,
                IsAdmin = isAdmin,
                Wishes = wishes
            };
        }

        internal static User Create(ulong id, DateTime createdOn, DateTime modifiedOn,
            ulong roomId, string authCode,
            string firstName, string lastName, string phone, string? email,
            string deliveryInfo, ulong? giftRecipientUserId, bool wantSurprise, string? interests,
            bool isAdmin, IEnumerable<Wish> wishes)
        {
            var user = InitialCreate(roomId, authCode, firstName, lastName, phone, email, deliveryInfo,
                wantSurprise, interests, isAdmin, wishes);
            user.Id = id;
            user.ModifiedOn = modifiedOn;
            user.CreatedOn = createdOn;
            user.GiftRecipientUserId = giftRecipientUserId;

            return user;
        }

        /// <summary>
        /// Method to promote User to admin.
        /// </summary>
        public void PromoteToAdmin()
        {
            IsAdmin = true;
        }
    }
}