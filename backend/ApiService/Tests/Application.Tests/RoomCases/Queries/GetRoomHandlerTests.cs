using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Handlers;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;

namespace Epam.ItMarathon.ApiService.Application.Tests.RoomCases.Queries
{
    /// <summary>
    /// Unit tests for the <see cref="GetRoomHandler"/> class.
    /// </summary>
    public class GetRoomHandlerTests
    {
        private readonly IRoomRepository _roomRepositoryMock;
        private readonly GetRoomHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoomHandlerTests"/> class with mocked dependencies.
        /// </summary>
        public GetRoomHandlerTests()
        {
            _roomRepositoryMock = Substitute.For<IRoomRepository>();
            _handler = new GetRoomHandler(_roomRepositoryMock);
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when a room by the specified user code is not found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomByUserCodeNotFound()
        {
            // Arrange
            var query = new GetRoomQuery(Guid.Empty.ToString(), null);
            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Failure<Room, ValidationResult>(new NotFoundError([
                    new ValidationFailure("code", string.Empty)
                ])));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("code"));
        }

        /// <summary>
        /// Tests that the handler returns an existing room when a room by the specified user code is found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnRoom_WhenRoomByUserCodeIsFound()
        {
            // Arrange
            var existingRoom = DataFakers.RoomFaker.Generate();
            var query = new GetRoomQuery(Guid.Empty.ToString(), null);
            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(existingRoom);
        }

        /// <summary>
        /// Tests that the handler returns a NotFoundError when a room with the specified room code is not found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomByRoomCodeNotFound()
        {
            // Arrange
            var query = new GetRoomQuery(null, Guid.Empty.ToString());
            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(Result.Failure<Room, ValidationResult>(new NotFoundError([
                    new ValidationFailure("code", string.Empty)
                ])));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("code"));
        }

        /// <summary>
        /// Tests that the handler returns an existing room when a room with the specified room code is found.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldReturnRoom_WhenRoomByRoomCodeIsFound()
        {
            // Arrange
            var existingRoom = DataFakers.RoomFaker.Generate();
            var query = new GetRoomQuery(null, Guid.Empty.ToString());
            _roomRepositoryMock
                .GetByRoomCodeAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(existingRoom);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(existingRoom);
        }
    }
}