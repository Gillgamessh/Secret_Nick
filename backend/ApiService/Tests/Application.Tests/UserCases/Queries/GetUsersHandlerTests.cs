using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;

namespace Epam.ItMarathon.ApiService.Application.Tests.UserCases.Queries
{
    /// <summary>
    /// Unit tests for the <see cref="GetUsersHandler"/> class.
    /// </summary>
    public class GetUsersHandlerTests
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepositoryMock;
        private readonly GetUsersHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersHandlerTests"/> class with mocked dependencies.
        /// </summary>
        public GetUsersHandlerTests()
        {
            _userReadOnlyRepositoryMock = Substitute.For<IUserReadOnlyRepository>();
            _handler = new GetUsersHandler(_userReadOnlyRepositoryMock);
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when the user by provided UserCode is not found.
        /// </summary>
        [Fact]
        public async Task Handler_ShouldReturnFailure_WhenAuthUserNotFound()
        {
            // Arrange
            var query = new GetUsersQuery(string.Empty, null);
            _userReadOnlyRepositoryMock
                .GetByCodeAsync(query.UserCode, CancellationToken.None, includeRoom: true, includeWishes: true)
                .Returns(Result.Failure<Domain.Entities.User.User, ValidationResult>(
                    new NotFoundError([
                        new ValidationFailure("userCode", string.Empty)
                    ])));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("userCode"));
        }

        /// <summary>
        /// Tests that the handler returns all users in the room when UserId is null and a valid UserCode is provided.
        /// </summary>
        [Fact]
        public async Task Handler_ShouldReturnAllUsersInRoom_WhenUserIdIsNullAndValidUserCodeProvided()
        {
            // Arrange
            const int usersInRoomCount = 3;
            var query = new GetUsersQuery(string.Empty, null);
            var usersInRoom = DataFakers.UserFaker
                .RuleFor(user => user.RoomId, _ => 1UL)
                .Generate(usersInRoomCount);
            _userReadOnlyRepositoryMock
                .GetByCodeAsync(query.UserCode, CancellationToken.None, includeRoom: true, includeWishes: true)
                .Returns(usersInRoom.First());
            _userReadOnlyRepositoryMock
                .GetManyByRoomIdAsync(Arg.Any<ulong>(), CancellationToken.None)
                .Returns(usersInRoom);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(usersInRoomCount);
            Assert.Equal(usersInRoom, result.Value);
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when the user with provided UserId not found.
        /// </summary>
        [Fact]
        public async Task Handler_ShouldReturnFailure_WhenUserWithProvidedUserIdNotFound()
        {
            // Arrange
            var query = new GetUsersQuery(string.Empty, 2);
            var authUser = DataFakers.UserFaker
                .RuleFor(user => user.RoomId, _ => 1UL)
                .Generate();
            _userReadOnlyRepositoryMock
                .GetByCodeAsync(query.UserCode, CancellationToken.None, includeWishes: true)
                .Returns(authUser);
            _userReadOnlyRepositoryMock
                .GetByIdAsync(query.UserId!.Value, CancellationToken.None, includeWishes: true)
                .Returns(Result.Failure<Domain.Entities.User.User, ValidationResult>(
                    new NotFoundError([
                        new ValidationFailure("id", string.Empty)
                    ])));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("id"));
        }

        /// <summary>
        /// Tests that the handler returns a NotAuthorizedError when the auth user is unauthorized to read a user from a different room.
        /// </summary>
        [Fact]
        public async Task Handler_ShouldReturnFailure_WhenAuthUserUnauthorizedToReadUserFromDifferentRoom()
        {
            // Arrange
            var query = new GetUsersQuery(string.Empty, 2);
            var authUser = DataFakers.UserFaker
                .RuleFor(user => user.RoomId, _ => 1UL)
                .Generate();
            var requestedUser = DataFakers.UserFaker
                .RuleFor(user => user.RoomId, _ => 2UL)
                .Generate();
            _userReadOnlyRepositoryMock
                .GetByCodeAsync(query.UserCode, CancellationToken.None, includeRoom: true, includeWishes: true)
                .Returns(authUser);
            _userReadOnlyRepositoryMock
                .GetByIdAsync(query.UserId!.Value, CancellationToken.None, includeWishes: true)
                .Returns(requestedUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotAuthorizedError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("id"));
        }

        /// <summary>
        /// Tests that the handler returns both the auth user and requested user when the auth user is authorized to read the user with provided UserId.
        /// </summary>
        [Fact]
        public async Task Handler_ShouldReturnTwoUsers_WhenAuthUserAuthorizedToReadUserWithProvidedId()
        {
            // Arrange
            var query = new GetUsersQuery(string.Empty, 2);
            var authUser = DataFakers.UserFaker
                .RuleFor(user => user.RoomId, _ => 1UL)
                .Generate();
            var requestedUser = DataFakers.UserFaker
                .RuleFor(user => user.RoomId, _ => 1UL)
                .Generate();
            _userReadOnlyRepositoryMock
                .GetByCodeAsync(query.UserCode, CancellationToken.None, includeRoom: true, includeWishes: true)
                .Returns(authUser);
            _userReadOnlyRepositoryMock
                .GetByIdAsync(query.UserId!.Value, CancellationToken.None, includeWishes: true)
                .Returns(requestedUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(2);
            Assert.Equal([requestedUser, authUser], result.Value);
        }
    }
}