using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;

namespace Epam.ItMarathon.ApiService.Domain.Builders
{
    /// <summary>
    /// Builder for User entity.
    /// </summary>
    public class UserBuilder : BaseEntityBuilder<UserBuilder>
    {
        private ulong _roomId;
        private string _authCode = null!;
        private string _firstName = null!;
        private string _lastName = null!;
        private string _phone = null!;
        private string? _email;
        private string _deliveryInfo = null!;
        private ulong? _giftRecipientUserId;
        private bool _wantSurprise;
        private string? _interests;
        private bool _isAdmin;
        private IEnumerable<Wish> _wishes = null!;

        /// <summary>
        /// Set a Room's unique identifier for User.
        /// </summary>
        /// <param name="roomId">User's Room unique identifier.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithRoomId(ulong roomId)
        {
            _roomId = roomId;
            return this;
        }

        /// <summary>
        /// Set an auth security code of User.
        /// </summary>
        /// <param name="authCode">User's authorization code.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithAuthCode(string authCode)
        {
            _authCode = authCode;
            return this;
        }

        /// <summary>
        /// Set a User's first name.
        /// </summary>
        /// <param name="firstName">User's first name.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        /// <summary>
        /// Set a User's last name.
        /// </summary>
        /// <param name="lastName">User's last name.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        /// <summary>
        /// Set a phone number for User.
        /// </summary>
        /// <param name="phone">User's mobile phone.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        /// <summary>
        /// Set an email for a User.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithEmail(string? email)
        {
            _email = email;
            return this;
        }

        /// <summary>
        /// Set a delivery information for a User.
        /// </summary>
        /// <param name="deliveryInfo">Delivery info for receiving Gift by User.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithDeliveryInfo(string deliveryInfo)
        {
            _deliveryInfo = deliveryInfo;
            return this;
        }

        /// <summary>
        /// Set a unique identifier of User's which you want to gift.
        /// </summary>
        /// <param name="giftRecipientUserId">Unique identifier of the User, which current User will send a gift.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithGiftRecipientId(ulong? giftRecipientUserId)
        {
            _giftRecipientUserId = giftRecipientUserId;
            return this;
        }

        /// <summary>
        /// Set whether User wants surprise instead of selection from Wish List.
        /// </summary>
        /// <param name="wantSurprise">Indicates whether User wants surprise instead of selection from Wish List.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithWantSurprise(bool wantSurprise)
        {
            _wantSurprise = wantSurprise;
            return this;
        }

        /// <summary>
        /// Set User's interests in case when he/she wants surprise.
        /// </summary>
        /// <param name="interests">Interests of User in case when he/she wants surprise.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithInterests(string? interests)
        {
            _interests = interests;
            return this;
        }

        /// <summary>
        /// Set if User is admin.
        /// </summary>
        /// <param name="isAdmin">Whether User an admin or not.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithIsAdmin(bool isAdmin)
        {
            _isAdmin = isAdmin;
            return this;
        }

        /// <summary>
        /// Set list of desired gifts in case when User doesn't want surprise.
        /// </summary>
        /// <param name="wishes">List of desired gifts in case when User doesn't want surprise.</param>
        /// <returns>Returns reference to current object.</returns>
        public UserBuilder WithWishes(IEnumerable<(string?, string?)> wishes)
        {
            _wishes = wishes.Where(wish => !string.IsNullOrEmpty(wish.Item1) || !string.IsNullOrEmpty(wish.Item2))
                .Select(pair => Wish.Create(pair.Item1, pair.Item2)).ToList();
            return this;
        }

        internal User Build()
        {
            return User.Create(_id, _createdOn, _modifiedOn,
                _roomId, _authCode, _firstName, _lastName, _phone, _email,
                _deliveryInfo, _giftRecipientUserId, _wantSurprise, _interests, _isAdmin, _wishes);
        }

        internal User InitialBuild()
        {
            return User.InitialCreate(_roomId, _authCode, _firstName, _lastName, _phone, _email, _deliveryInfo,
                _wantSurprise, _interests, _isAdmin, _wishes
            );
        }
    }
}