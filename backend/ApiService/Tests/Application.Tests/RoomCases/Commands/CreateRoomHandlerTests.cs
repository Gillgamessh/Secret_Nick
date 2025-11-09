using Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Handlers;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;

namespace Epam.ItMarathon.ApiService.Application.Tests.RoomCases.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="CreateRoomHandler"/> class.
    /// </summary>
    public class CreateRoomHandlerTests
    {
        private readonly IRoomRepository _roomRepositoryMock;
        private readonly CreateRoomHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoomHandlerTests"/> class with mocked dependencies.
        /// </summary>
        public CreateRoomHandlerTests()
        {
            _roomRepositoryMock = Substitute.For<IRoomRepository>();
            _handler = new CreateRoomHandler(_roomRepositoryMock);
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the room has an invalid name.
        /// </summary>
        /// <param name="name">Room's name to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(DataFakers.GetRandomString), 41, MemberType = typeof(DataFakers))]
        public async Task Handler_ShouldReturnFailure_WhenRoomHasInvalidName(string? name)
        {
            // Arrange
            var invalidRoom = DataFakers.RoomApplicationFaker
                .RuleFor(room => room.Name, _ => name)
                .Generate();
            var fakerUser = DataFakers.UserApplicationFaker.Generate();
            var command = new CreateRoomCommand(invalidRoom, fakerUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("name"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the room has an invalid description.
        /// </summary>
        /// <param name="description">Room's description to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(DataFakers.GetRandomString), 201, MemberType = typeof(DataFakers))]
        public async Task Handler_ShouldReturnFailure_WhenRoomHasInvalidDescription(string? description)
        {
            // Arrange
            var invalidRoom = DataFakers.RoomApplicationFaker
                .RuleFor(room => room.Description, _ => description)
                .Generate();
            var fakerUser = DataFakers.UserApplicationFaker.Generate();
            var command = new CreateRoomCommand(invalidRoom, fakerUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("description"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the room has an invalid gift exchange date.
        /// </summary>
        /// <param name="giftExchangeDate">Room's gift exchange date to test.</param>
        [Theory]
        [MemberData(nameof(DataFakers.InvalidGiftExchangeDates), MemberType = typeof(DataFakers))]
        public async Task Handler_ShouldReturnFailure_WhenRoomHasInvalidGiftExchangeDate(DateTime giftExchangeDate)
        {
            // Arrange
            var invalidRoom = DataFakers.RoomApplicationFaker
                .RuleFor(room => room.GiftExchangeDate, _ => giftExchangeDate)
                .Generate();
            var fakerUser = DataFakers.UserApplicationFaker.Generate();
            var command = new CreateRoomCommand(invalidRoom, fakerUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("giftExchangeDate"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the room's gift maximum budget exceeds limit.
        /// </summary>
        [Fact]
        public async Task Handler_ShouldReturnFailure_WhenRoomGiftMaximumBudgetExceedsLimit()
        {
            // Arrange
            var invalidRoom = DataFakers.RoomApplicationFaker
                .RuleFor(room => room.GiftMaximumBudget, _ => 100_001UL)
                .Generate();
            var fakerUser = DataFakers.UserApplicationFaker.Generate();
            var command = new CreateRoomCommand(invalidRoom, fakerUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("giftMaximumBudget"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid first name.
        /// </summary>
        /// <param name="firstName">User's first name to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(DataFakers.GetRandomString), 41, MemberType = typeof(DataFakers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidFirstName(string? firstName)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.FirstName, _ => firstName)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].firstName"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid last name.
        /// </summary>
        /// <param name="lastName">User's last name to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(DataFakers.GetRandomString), 41, MemberType = typeof(DataFakers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidLastName(string? lastName)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.LastName, _ => lastName)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].lastName"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid delivery info.
        /// </summary>
        /// <param name="deliveryInfo">User's delivery info to test.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [MemberData(nameof(DataFakers.GetRandomString), 501, MemberType = typeof(DataFakers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidDeliveryInfo(string? deliveryInfo)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.DeliveryInfo, _ => deliveryInfo)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].deliveryInfo"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid phone number.
        /// </summary>
        /// <param name="phoneNumber">User's phone number to test.</param>
        [Theory]
        [MemberData(nameof(DataFakers.InvalidPhoneNumbers), MemberType = typeof(DataFakers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidPhone(string? phoneNumber)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.Phone, _ => phoneNumber)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].phone"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid email.
        /// </summary>
        /// <param name="email">User's email to test.</param>
        [Theory]
        [MemberData(nameof(DataFakers.InvalidEmails), MemberType = typeof(DataFakers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidEmail(string email)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.Email, _ => email)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].email"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has an invalid wishes based on their wantSurprise preference.
        /// </summary>
        /// <param name="wantSurprise">User's want surprise preference.</param>
        /// <param name="wishList">User's wish list to test.</param>
        [Theory]
        [MemberData(nameof(DataFakers.InvalidWishes), MemberType = typeof(DataFakers))]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidWishes(bool wantSurprise,
            IEnumerable<(string?, string?)> wishList)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => wantSurprise)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, _ => wishList)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].wishList"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user's wishes exceed the limit.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserWishesExceedsLimit()
        {
            // Arrange
            const int wishesToGenerate = 6;
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => false)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, _ => Enumerable.Range(1, wishesToGenerate)
                    .Select<int, (string?, string?)>((_, index) => (index.ToString(), null)))
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("limitsValidation.wishesLimit[0]"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user has invalid interests based on their wantSurprise preference.
        /// </summary>
        /// <param name="wantSurprise">User's want surprise preference.</param>
        /// <param name="interests">User's interests to test.</param>
        [Theory]
        [InlineData(true, null)]
        [InlineData(false, "text")]
        public async Task Handle_ShouldReturnFailure_WhenUserHasInvalidInterests(bool wantSurprise, string? interests)
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker.Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => wantSurprise)
                .RuleFor(user => user.Interests, _ => interests)
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].interests"));
        }

        /// <summary>
        /// Tests that the handler successfully creates room when provided with valid room and user information.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldCreateAndReturnRoomSuccessfully_WhenProvidedValidRoomAndUserInfo()
        {
            // Arrange
            var fakeRoom = DataFakers.RoomApplicationFaker
                .RuleFor(room => room.Name, faker => faker.Random.String(40))
                .RuleFor(room => room.Description, faker => faker.Random.String(200))
                .RuleFor(room => room.GiftExchangeDate, faker => faker.Date.Soon())
                .RuleFor(room => room.GiftMaximumBudget, _ => 100_000UL)
                .Generate();
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.FirstName, faker => faker.Random.String(40))
                .RuleFor(user => user.LastName, faker => faker.Random.String(40))
                .RuleFor(user => user.Email, faker => faker.Internet.Email())
                .RuleFor(user => user.Phone, faker => faker.Phone.PhoneNumber("+380#########"))
                .RuleFor(user => user.DeliveryInfo, faker => faker.Random.String(500))
                .RuleFor(user => user.WantSurprise, _ => false)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, faker =>
                [
                    (faker.Random.String(10), null),
                    (faker.Random.String(10), faker.Internet.Url().Replace("http:", "https:"))
                ])
                .Generate();
            var request = new CreateRoomCommand(fakeRoom, invalidUser);
            _roomRepositoryMock
                .AddAsync(Arg.Any<Room>(), CancellationToken.None)
                .Returns(callInfo => callInfo.Arg<Room>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
    }
}