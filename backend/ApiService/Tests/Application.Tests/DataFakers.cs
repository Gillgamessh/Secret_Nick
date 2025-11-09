using Bogus;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Builders;

namespace Epam.ItMarathon.ApiService.Application.Tests
{
    /// <summary>
    /// Static class that contains fakers for generating test data.
    /// </summary>
    public static class DataFakers
    {
        /// <summary>
        /// General-purpose faker for generating random data.
        /// </summary>
        public static Faker GeneralFaker { get; } = new();

        /// <summary>
        /// Faker for generating <see cref="UserApplication"/> instances.
        /// </summary>
        public static Faker<RoomApplication> RoomApplicationFaker => new Faker<RoomApplication>()
            .RuleFor(room => room.Name, faker => faker.Lorem.Word())
            .RuleFor(room => room.Description, faker => faker.Lorem.Word())
            .RuleFor(room => room.GiftExchangeDate, faker => faker.Date.Soon())
            .RuleFor(room => room.GiftMaximumBudget, faker => (ulong)faker.Random.Int(0, 1000));

        /// <summary>
        /// Faker for generating <see cref="UserApplication"/> instances.
        /// </summary>
        public static Faker<UserApplication> UserApplicationFaker => new Faker<UserApplication>()
            .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
            .RuleFor(user => user.LastName, faker => faker.Name.LastName())
            .RuleFor(user => user.Phone, faker => faker.Phone.PhoneNumber("+380#########"))
            .RuleFor(user => user.DeliveryInfo, faker => faker.Address.StreetAddress())
            .RuleFor(user => user.WantSurprise, _ => true)
            .RuleFor(user => user.Interests, faker => faker.Lorem.Word())
            .RuleFor(user => user.Wishes, _ => []);

        /// <summary>
        /// Pre-configured builder for creating valid <see cref="Room"/> instances.
        /// </summary>
        public static RoomBuilder ValidRoomBuilder => RoomBuilder.Init()
            .WithId((ulong)GeneralFaker.UniqueIndex + 1)
            .WithInvitationCode(Guid.NewGuid().ToString())
            .WithName(GeneralFaker.Lorem.Word())
            .WithDescription(GeneralFaker.Lorem.Word())
            .WithGiftExchangeDate(GeneralFaker.Date.Soon())
            .WithMinUsersLimit(10)
            .AddUser(_ => ValidUserBuilder);

        /// <summary>
        /// Faker for generating <see cref="Room"/> instances.
        /// </summary>
        public static Faker<Room> RoomFaker =>
            new Faker<Room>().CustomInstantiator(_ => ValidRoomBuilder.Build().Value);

        /// <summary>
        /// Pre-configured builder for creating valid <see cref="User"/> instances.
        /// </summary>
        public static UserBuilder ValidUserBuilder => new UserBuilder()
            .WithId((ulong)GeneralFaker.UniqueIndex + 1)
            .WithAuthCode(Guid.NewGuid().ToString())
            .WithFirstName(GeneralFaker.Name.FirstName())
            .WithLastName(GeneralFaker.Name.LastName())
            .WithPhone(GeneralFaker.Phone.PhoneNumber("+380#########"))
            .WithEmail(GeneralFaker.Internet.Email())
            .WithDeliveryInfo(GeneralFaker.Address.StreetAddress())
            .WithWantSurprise(true)
            .WithInterests(GeneralFaker.Lorem.Word())
            .WithWishes([]);

        /// <summary>
        /// Faker for generating <see cref="User"/> instances.
        /// </summary>
        public static Faker<User> UserFaker => new Faker<User>().CustomInstantiator(_ => ValidUserBuilder.Build());

        /// <summary>
        /// Generates a TheoryData object containing a random string of the specified length.
        /// </summary>
        /// <param name="stringLength">Length of the string to generate.</param>
        public static TheoryData<string> GetRandomString(int stringLength) =>
            [GeneralFaker.Random.String(stringLength)];

        /// <summary>
        /// Generates a TheoryData object containing various invalid email formats.
        /// </summary>
        public static TheoryData<string> InvalidEmails =>
        [
            "not_valid_email.com", // Missing @ symbol
            "missingdomain@", // Missing domain name
            "@missingusername.com", // Missing username
            "username..dots@example.com", // Consecutive dots
            "username@-domain.com", // Domain starts with a hyphen
            "username@domain-.com", // Domain ends with a hyphen
            new string('a', 65) + "@example.com", // Username exceeds 64 characters
            "username@" + new string('a', 64) + ".com", // Domain exceeds 253 characters
            "user name@example.com", // Email with spaces
            "user\"name@example.com", // Double quotes not properly escaped
            "username@domain_with_underscore.com" // Invalid character in domain
        ];

        /// <summary>
        /// Generates a TheoryData object containing invalid wish list scenarios.
        /// </summary>
        public static TheoryData<bool, IEnumerable<(string?, string?)>> InvalidWishes => new()
        {
            { false, [] },
            { false, [("Same", null), ("Same", null)] },
            { true, [(GeneralFaker.Random.String(10), null)] },
        };

        /// <summary>
        /// Generates a TheoryData object containing various invalid phone number formats.
        /// </summary>
        public static TheoryData<string> InvalidPhoneNumbers =>
        [
            "380123456789", // Missing '+'
            "+381123456789", // Incorrect country code
            "+38012345678", // Too few digits
            "+3801234567890", // Too many digits
            "+38012345678a", // Contains a letter
            "+38012345@789", // Contains a special character
            "+380", // Only country code
            "+380123", // Partial digits
            "+380 123456789", // Contains a space
            "+380123 456 789", // Multiple spaces
            "+38-012-345-67-89", // Contains dashes
            "+380.123.456.789", // Contains dots
            "", // Empty string
            null
        ];

        /// <summary>
        /// Generates a TheoryData object containing various past dates for gift exchange.
        /// </summary>
        public static TheoryData<DateTime> InvalidGiftExchangeDates =>
        [
            DateTime.MinValue, // Empty/default value
            DateTime.Today.AddDays(-1), // Yesterday
            DateTime.Today.AddDays(-7), // A week ago 
            new(2000, 1, 1), // Arbitrary past date
        ];
    }
}