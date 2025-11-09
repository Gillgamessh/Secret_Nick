using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Epam.ItMarathon.ApiService.Application.Tests.UserCases.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="CreateUserInRoomHandler"/> class.
    /// </summary>
    public class CreateUserInRoomHandlerTests
    {
        private readonly IRoomRepository _roomRepositoryMock;
        private readonly IUserReadOnlyRepository _userReadOnlyRepositoryMock;
        private readonly CreateUserInRoomHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserInRoomHandlerTests"/> class with mocked dependencies.
        /// </summary>
        public CreateUserInRoomHandlerTests()
        {
            _roomRepositoryMock = Substitute.For<IRoomRepository>();
            _userReadOnlyRepositoryMock = Substitute.For<IUserReadOnlyRepository>();
            _handler = new CreateUserInRoomHandler(_roomRepositoryMock, _userReadOnlyRepositoryMock);
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when the specified room is not found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomNotFound()
        {
            // Arrange
            var fakeUser = DataFakers.UserApplicationFaker.Generate();
            var request = new CreateUserInRoomRequest(fakeUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(new NotFoundError([
                    new ValidationFailure("code", string.Empty)
                ]));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("code"));
        }

        /// <summary>
        /// Tests that the handler returns a BadRequestError when the room is already closed.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomIsAlreadyClosed()
        {
            // Arrange
            var invalidUser = DataFakers.UserApplicationFaker.Generate();
            var existingRoom = DataFakers.RoomFaker
                .RuleFor(room => room.ClosedOn, faker => faker.Date.Past())
                .Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("room.ClosedOn"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.FirstName, _ => firstName)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].firstName"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.LastName, _ => lastName)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].lastName"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.DeliveryInfo, _ => deliveryInfo)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].deliveryInfo"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.Phone, _ => phoneNumber)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].phone"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.Email, _ => email)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].email"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => wantSurprise)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, _ => wishList)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].wishList"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the user's wishes exceed the limit.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserWishesExceedsLimit()
        {
            // Arrange
            const int wishesToGenerate = 6;
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => false)
                .RuleFor(user => user.Interests, _ => null)
                .RuleFor(user => user.Wishes, _ => Enumerable.Range(1, wishesToGenerate)
                    .Select<int, (string?, string?)>((_, index) => (index.ToString(), null)))
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"limitsValidation.wishesLimit[{existingRoom.Users.Count}]"));
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
            var invalidUser = DataFakers.UserApplicationFaker
                .RuleFor(user => user.WantSurprise, _ => wantSurprise)
                .RuleFor(user => user.Interests, _ => interests)
                .Generate();
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals($"Users[{existingRoom.Users.Count}].interests"));
        }

        /// <summary>
        /// Tests that the handler returns a ValidationResult error when the room's user limit is exceeded.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomUsersExceedsLimit()
        {
            // Arrange
            const int usersToGenerate = 20;
            var invalidUser = DataFakers.UserApplicationFaker.Generate();
            var existingRoom = DataFakers.RoomFaker.RuleFor(room => room.Users, _ =>
            {
                var existingUsers = new List<User>();
                for (var index = 0; index < usersToGenerate; index++)
                {
                    existingUsers.Add(DataFakers.ValidUserBuilder.Build());
                }

                return existingUsers;
            }).Generate();
            var request = new CreateUserInRoomRequest(invalidUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ValidationResult>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("limitsValidation.userLimit"));
        }

        /// <summary>
        /// Tests that the handler successfully creates a user when provided with valid user information.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldCreateUserSuccessfully_WhenProvidedValidUserInfo()
        {
            // Arrange
            var validUser = DataFakers.UserApplicationFaker
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
            var existingRoom = DataFakers.RoomFaker
                .RuleFor(room => room.Description, faker => faker.Lorem.Sentence(2))
                .RuleFor(room => room.Users, _ => [DataFakers.ValidUserBuilder.Build()])
                .Generate();
            var request = new CreateUserInRoomRequest(validUser, string.Empty);

            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            _userReadOnlyRepositoryMock
                .GetByCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom.Users.Last());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
    }
}