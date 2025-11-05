using System.Threading;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;
using Xunit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Epam.ItMarathon.ApiService.Application.Tests.UserCases.Commands
{
    public class DeleteUserInRoomHandlerTests
    {
        private readonly IRoomRepository _roomRepositoryMock;
        private readonly IUserReadOnlyRepository _userReadOnlyRepositoryMock;
        private readonly DeleteUserHandler _handler;

        public DeleteUserInRoomHandlerTests()
        {
            _roomRepositoryMock = Substitute.For<IRoomRepository>();
            _userReadOnlyRepositoryMock = Substitute.For<IUserReadOnlyRepository>();
            _handler = new DeleteUserHandler(_roomRepositoryMock, _userReadOnlyRepositoryMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoomNotFound()
        {
            var request = new DeleteUsersRequest("some-code", 2);

            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new NotFoundError(new[] { new ValidationFailure("code", "Room with such code not found") }));

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals("code"));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenAuthUserNotFoundInRoom()
        {
            // room contains users but none has matching AuthCode
            var existingRoom = DataFakers.RoomFaker.Generate();
            var request = new DeleteUsersRequest("missing-code", 2);

            _roomRepositoryMock.GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(existingRoom);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals(nameof(request.UserCode)));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserWithUserCodeIsNotAdmin()
        {
            var baseRoom = DataFakers.RoomFaker.Generate();

            var authUser = DataFakers.ValidUserBuilder
                .WithAuthCode("auth-code")
                .WithIsAdmin(false)
                .WithRoomId(baseRoom.Id)
                .Build();

            var room = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => baseRoom.Id)
                .RuleFor(r => r.Users, _ => new List<User>(baseRoom.Users) { authUser })
                .Generate();

            var request = new DeleteUsersRequest(authUser.AuthCode, 2);

            _roomRepositoryMock.GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(room);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotAuthorizedError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals(nameof(request.UserCode)));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIdIsNull()
        {
            var baseRoom = DataFakers.RoomFaker.Generate();

            var admin = DataFakers.ValidUserBuilder
                .WithAuthCode("admin-code")
                .WithIsAdmin(true)
                .WithRoomId(baseRoom.Id)
                .Build();

            var room = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => baseRoom.Id)
                .RuleFor(r => r.Users, _ => new List<User> { admin })
                .Generate();

            var request = new DeleteUsersRequest(admin.AuthCode, null);

            _roomRepositoryMock.GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(room);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals(nameof(request.UserId)));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTargetUserNotFoundInDatabase()
        {
            var baseRoom = DataFakers.RoomFaker.Generate();

            var admin = DataFakers.ValidUserBuilder
                .WithAuthCode("admin-code")
                .WithIsAdmin(true)
                .WithRoomId(baseRoom.Id)
                .Build();

            var room = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => baseRoom.Id)
                .RuleFor(r => r.Users, _ => new List<User> { admin })
                .Generate();

            var missingUserId = 999UL;
            var request = new DeleteUsersRequest(admin.AuthCode, missingUserId);

            _roomRepositoryMock.GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(room);

            _userReadOnlyRepositoryMock
                .GetByIdAsync(Arg.Any<ulong>(), Arg.Any<CancellationToken>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(new NotFoundError(new[] { new ValidationFailure("id", "User with such id not found") }));

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals("id"));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUsersBelongToDifferentRooms()
        {
            var baseRoom = DataFakers.RoomFaker.Generate();

            var admin = DataFakers.ValidUserBuilder
                .WithAuthCode("admin-auth")
                .WithIsAdmin(true)
                .WithRoomId(baseRoom.Id)
                .WithId(1)
                .Build();

            var room = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => baseRoom.Id)
                .RuleFor(r => r.Users, _ => new List<User> { admin })
                .Generate();

            var otherUser = DataFakers.ValidUserBuilder
                .WithId(42)
                .WithRoomId(baseRoom.Id + 1)
                .Build();

            var request = new DeleteUsersRequest(admin.AuthCode, otherUser.Id);

            _roomRepositoryMock.GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(room);

            _userReadOnlyRepositoryMock
                .GetByIdAsync(otherUser.Id, Arg.Any<CancellationToken>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(otherUser);

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotAuthorizedError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals("id"));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenAuthUserAttemptsToDeleteSelf()
        {
            var baseRoom = DataFakers.RoomFaker.Generate();

            var admin = DataFakers.ValidUserBuilder
                .WithAuthCode("admin-code")
                .WithIsAdmin(true)
                .WithId(11)
                .WithRoomId(baseRoom.Id)
                .Build();

            var room = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => baseRoom.Id)
                .RuleFor(r => r.Users, _ => new List<User> { admin })
                .Generate();

            var request = new DeleteUsersRequest(admin.AuthCode, admin.Id);

            // ensure UpdateAsync won't fail (not used in this branch, but keep consistent)
            _roomRepositoryMock.GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(room);
            _roomRepositoryMock.UpdateAsync(Arg.Any<Domain.Aggregate.Room.Room>(), Arg.Any<CancellationToken>())
                .Returns(Result.Success());

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(e => e.PropertyName.Equals(nameof(request.UserId)));
        }
        [Fact]
        public async Task Handle_ShouldDeleteUserSuccessfully_WhenAllChecksPass()
        {
            const ulong roomId = 100UL;

            var admin = DataFakers.ValidUserBuilder
                .WithAuthCode("admin-auth")
                .WithIsAdmin(true)
                .WithId(1)
                .WithRoomId(roomId)
                .Build();

            var target = DataFakers.ValidUserBuilder
                .WithAuthCode("target-auth")
                .WithId(2)
                .WithRoomId(roomId)
                .Build();

            var room = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => roomId)
                .RuleFor(r => r.Users, _ => new List<User> { admin, target })
                .Generate();

            var updatedRoom = DataFakers.RoomFaker
                .RuleFor(r => r.Id, _ => roomId)
                .RuleFor(r => r.Users, _ => new List<User> { admin })
                .Generate();

            var request = new DeleteUsersRequest(admin.AuthCode, target.Id);

            _roomRepositoryMock
                .GetByUserCodeAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(room, updatedRoom);

            _userReadOnlyRepositoryMock
                .GetByIdAsync(target.Id, Arg.Any<CancellationToken>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(target);

            _roomRepositoryMock
                .UpdateAsync(Arg.Any<Domain.Aggregate.Room.Room>(), Arg.Any<CancellationToken>())
                .Returns(Result.Success());

            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Users.Should().HaveCount(1);
            result.Value.Users.Should().Contain(u => u.Id == admin.Id);
        }
    }
}