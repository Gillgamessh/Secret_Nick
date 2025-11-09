using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Builders
{
    /// <summary>
    /// Builder for creating Room domain aggregate.
    /// </summary>
    public class RoomBuilder : BaseAggregateBuilder<RoomBuilder>, IAggregateBuilder<Room>
    {
        private DateTime? _closedOn;
        private string _invitationCode = null!;
        private uint _minUsersLimit = 3;
        private uint _maxUsersLimit = 20;
        private uint _maxWishesLimit = 5;
        private string _name = null!;
        private string _description = null!;
        private string _invitationNote =
            "Hey!\n\nJoin our Secret Nick and make this holiday season magical! 🎄\n\nYou‘ll get to surprise someone with a gift — and receive one too. 🎅✨\n\nLet the holiday fun begin! 🌟\n\n🎁 Join here:";
        private DateTime _giftExchangeDate;
        private ulong _giftMaximumBudget;
        private IList<User> _users { get; set; } = [];

        /// <summary>
        /// Initialization of builder.
        /// </summary>
        /// <returns>Returns new <see cref="RoomBuilder"/> object.</returns>
        public static RoomBuilder Init() => new();

        /// <summary>
        /// Set a Closed on filed.
        /// </summary>
        /// <param name="closedOn">Time when Room was closed.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithShouldBeClosedOn(DateTime? closedOn)
        {
            _closedOn = closedOn;
            return this;
        }

        /// <summary>
        /// Set an invitation code.
        /// </summary>
        /// <param name="invitationCode">Code for invitation link.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithInvitationCode(string invitationCode)
        {
            _invitationCode = invitationCode;
            return this;
        }

        /// <summary>
        /// Set a min User limit in Room for draw.
        /// </summary>
        /// <param name="minUsersLimit">Minimal limit of Users in Room for draft.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithMinUsersLimit(uint? minUsersLimit)
        {
            if (minUsersLimit is null)
            {
                return this;
            }

            _minUsersLimit = minUsersLimit.Value;
            return this;
        }

        /// <summary>
        /// Set a max User limit in Room.
        /// </summary>
        /// <param name="maxUsersLimit">Maximum amount of Users in Room.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithMaxUsersLimit(uint? maxUsersLimit)
        {
            if (maxUsersLimit is null)
            {
                return this;
            }

            _maxUsersLimit = maxUsersLimit.Value;
            return this;
        }

        /// <summary>
        /// Set a max wishes limit.
        /// </summary>
        /// <param name="maxWishesLimit">Maximum amount of wishes per User.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithMaxWishesLimit(uint? maxWishesLimit)
        {
            if (maxWishesLimit is null)
            {
                return this;
            }

            _maxWishesLimit = maxWishesLimit.Value;
            return this;
        }

        /// <summary>
        /// Set a name for Room.
        /// </summary>
        /// <param name="name">Room's name.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Set a description of the Room.
        /// </summary>
        /// <param name="description">Room's description.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        /// <summary>
        /// Set an invitation note of the Room.
        /// </summary>
        /// <param name="giftExchangeDate">Room's invitation note (attached to invitation).</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithInvitationNote(string? giftExchangeDate)
        {
            if (giftExchangeDate is null)
            {
                return this;
            }

            _invitationNote = giftExchangeDate;
            return this;
        }

        /// <summary>
        /// Set an exchange date of the Room.
        /// </summary>
        /// <param name="giftExchangeDate">Date for gifts to be exchanged.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithGiftExchangeDate(DateTime giftExchangeDate)
        {
            _giftExchangeDate = giftExchangeDate;
            return this;
        }

        /// <summary>
        /// Set a maximum budget of the Room.
        /// </summary>
        /// <param name="giftMaximumBudget">Maximum budget of the Room.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder WithGiftMaximumBudget(ulong giftMaximumBudget)
        {
            _giftMaximumBudget = giftMaximumBudget;
            return this;
        }

        /// <summary>
        /// Method to add a User using a UserBuilder.
        /// </summary>
        /// <param name="userBuilderConfiguration"><see cref="UserBuilder"/> delegate for adding a User.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder AddUser(Func<UserBuilder, UserBuilder> userBuilderConfiguration)
        {
            var userBuilder = new UserBuilder();
            var user = userBuilderConfiguration(userBuilder).Build();
            _users.Add(user);
            return this;
        }

        /// <summary>
        /// Method to add an initial User.
        /// </summary>
        /// <param name="userBuilderConfiguration"><see cref="UserBuilder"/> delegate for adding a User.</param>
        /// <returns>Returns reference to current object.</returns>
        public RoomBuilder InitialAddUser(Func<UserBuilder, UserBuilder> userBuilderConfiguration)
        {
            var userBuilder = new UserBuilder();
            var user = userBuilderConfiguration(userBuilder).InitialBuild();
            _users.Add(user);
            return this;
        }

        /// <summary>
        /// Create a Room.
        /// </summary>
        /// <returns>Returns built <see cref="Room"/> encapsulated in <see cref="Result"/>.</returns>
        public Result<Room, ValidationResult> Build()
        {
            return Room.Create(_id, _createdOn, _modifiedOn,
                _closedOn, _invitationCode, _name, _description,
                _invitationNote, _giftExchangeDate, _giftMaximumBudget, _users,
                _minUsersLimit, _maxUsersLimit, _maxWishesLimit);
        }

        /// <summary>
        /// Initial create for the Room.
        /// </summary>
        /// <returns>Returns built <see cref="Room"/> encapsulated in <see cref="Result"/>.</returns>
        public Result<Room, ValidationResult> InitialBuild()
        {
            return Room.InitialCreate(_closedOn, _invitationCode, _name, _description,
                _invitationNote, _giftExchangeDate, _giftMaximumBudget, _users,
                _minUsersLimit, _maxUsersLimit, _maxWishesLimit);
        }
    }
}