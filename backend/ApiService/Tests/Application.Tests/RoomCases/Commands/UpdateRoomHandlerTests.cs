using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Handlers;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;

namespace Epam.ItMarathon.ApiService.Application.Tests.RoomCases.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="UpdateRoomHandler"/> class.
    /// </summary>
    public class UpdateRoomHandlerTests
    {
        private readonly IRoomRepository _roomRepositoryMock;
        private readonly UpdateRoomHandler _handler;

        /// <summary>
        /// Test data for invalid <see cref="UpdateRoomCommand"/> instances.
        /// </summary>
        public static TheoryData<string, UpdateRoomCommand> InvalidCommands => new()
        {
            { "name", new UpdateRoomCommand(string.Empty, DataFakers.GeneralFaker.Random.String(41), null, null, null, null) },
            { "description", new UpdateRoomCommand(string.Empty, null, DataFakers.GeneralFaker.Random.String(201), null, null, null) },
            { "invitationNote", new UpdateRoomCommand(string.Empty, null, null, DataFakers.GeneralFaker.Random.String(1001), null, null) },
            { "giftExchangeDate", new UpdateRoomCommand(string.Empty, null, null, null, DataFakers.GeneralFaker.Date.Past(), null) },
            { "giftMaximumBudget", new UpdateRoomCommand(string.Empty, null, null, null, null, 100_001) },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoomHandlerTests"/> class with mocked dependencies.
        /// </summary>
        public UpdateRoomHandlerTests()
        {
            _roomRepositoryMock = Substitute.For<IRoomRepository>();
            _handler = new UpdateRoomHandler(_roomRepositoryMock);
        }

        /// <summary>
        /// Tests that the handler returns a BadRequestError when all request fields are empty.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenAllRequestFieldsAreEmpty()
        {
            // Arrange
            var command = new UpdateRoomCommand(string.Empty, null, null, null, null, null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals(string.Empty));
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when the room by provided UserCode is not found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomNotFound()
        {
            // Arrange
            var command = new UpdateRoomCommand(string.Empty, "name", null, null, null, null);
            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(Result.Failure<Room, ValidationResult>(new NotFoundError([
                    new ValidationFailure("code", string.Empty)
                ])));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("code"));
        }

        /// <summary>
        /// Tests that the handler returns a ForbiddenError when the user is not an admin.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsNotAdmin()
        {
            // Arrange
            var existingRoom = DataFakers.RoomFaker.Generate();
            var command = new UpdateRoomCommand(existingRoom.Users.First(user => !user.IsAdmin).AuthCode, "name", null,
                null, null, null);
            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ForbiddenError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("userCode"));
        }

        /// <summary>
        /// Tests that the handler returns a BadRequestError when any request field is invalid.
        /// </summary>
        /// <param name="propertyName">The name of the invalid property.</param>
        /// <param name="command">The <see cref="UpdateRoomCommand"/> instance with invalid fields.</param>
        [Theory]
        [MemberData(nameof(InvalidCommands))]
        public async Task Handle_ShouldReturnFailure_WhenRequestFieldsAreInvalid(string propertyName,
            UpdateRoomCommand command)
        {
            // Arrange
            var existingRoom = DataFakers.RoomFaker
                .RuleFor(room => room.Users, _ =>
                [
                    DataFakers.ValidUserBuilder
                        .WithAuthCode(string.Empty)
                        .WithIsAdmin(true)
                        .Build()
                ])
                .Generate();

            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Count.Should().Be(1);
            result.Error.Errors.First().PropertyName.Should().BeEquivalentTo(propertyName);
        }

        /// <summary>
        /// Tests that the handler returns a BadRequestError when the room is already closed.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomIsAlreadyClosed()
        {
            // Arrange
            var existingRoom = DataFakers.RoomFaker
                .RuleFor(room => room.ClosedOn, faker => faker.Date.Past())
                .RuleFor(room => room.Users, _ =>
                [
                    DataFakers.ValidUserBuilder
                        .WithAuthCode(string.Empty)
                        .WithIsAdmin(true)
                        .Build()
                ])
                .Generate();
            var command = new UpdateRoomCommand(
                existingRoom.Users.First(user => user.IsAdmin).AuthCode,
                "name", null, null, null, null);
            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("room.ClosedOn"));
        }

        /// <summary>
        /// Tests that the handler successfully updates and returns the room when the request is valid.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnUpdatedRoom_WhenRequestIsValid()
        {
            // Arrange
            var existingRoom = DataFakers.RoomFaker
                .RuleFor(room => room.Users, _ =>
                [
                    DataFakers.ValidUserBuilder
                        .WithAuthCode(string.Empty)
                        .WithIsAdmin(true)
                        .Build()
                ])
                .Generate();
            var command = new UpdateRoomCommand(
                existingRoom.Users.First(user => user.IsAdmin).AuthCode,
                DataFakers.GeneralFaker.Random.String(40),
                DataFakers.GeneralFaker.Random.String(200),
                DataFakers.GeneralFaker.Random.String(1000),
                DataFakers.GeneralFaker.Date.Soon(7, DateTime.UtcNow),
                100_000);
            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(existingRoom);
            _roomRepositoryMock
                .UpdateAsync(Arg.Any<Room>(), Arg.Any<CancellationToken>())
                .Returns(Result.Success());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be(command.Name);
            result.Value.Description.Should().Be(command.Description);
            result.Value.InvitationNote.Should().Be(command.InvitationNote);
            result.Value.GiftExchangeDate.Should().Be(command.GiftExchangeDate!.Value.Date);
            result.Value.GiftMaximumBudget.Should().Be(command.GiftMaximumBudget!.Value);
        }
    }
}