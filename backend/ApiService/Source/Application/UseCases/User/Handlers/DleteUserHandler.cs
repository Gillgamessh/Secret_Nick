using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers
{
    /// <summary>
    /// Handler that processes delete-user requests for a Room.
    /// </summary>
    /// <param name="roomRepository">Repository used to read/update Room aggregates.</param>
    /// <param name="userRepository">Read-only repository used to read User entities.</param>
    public class DeleteUserHandler(IRoomRepository roomRepository, IUserReadOnlyRepository userRepository)
        : IRequestHandler<DeleteUsersRequest, Result<RoomAggregate, ValidationResult>>
    {
        ///<inheritdoc/>
        public async Task<Result<RoomAggregate, ValidationResult>> Handle(DeleteUsersRequest request,
            CancellationToken cancellationToken)
        {

            var roomResult = await roomRepository.GetByUserCodeAsync(request.UserCode, cancellationToken);
            if (roomResult.IsFailure)
            {
                return roomResult;
            }

            // repository returned success but Value is null — treat as not found.
            if (roomResult.Value is null)
            {
                return Result.Failure<RoomAggregate, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(request.UserCode), "Room with such user code not found.")
                ]));
            }

            var room = roomResult.Value;

            // Resolve auth user from the room (should exist because GetByUserCodeAsync matched by auth code)
            var authUser = room.Users.FirstOrDefault(u => u.AuthCode == request.UserCode);
            if (authUser is null)
            {
                // defensive: userCode wasn't found in returned room (shouldn't happen normally)
                return Result.Failure<RoomAggregate, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(request.UserCode), "User with such code not found.")
                ]));
            }

            // Check admin rights
            if (!authUser.IsAdmin)
            {
                return Result.Failure<RoomAggregate, ValidationResult>(new NotAuthorizedError([
                    new ValidationFailure(nameof(request.UserCode), "User with userCode is not administrator.")
                ]));
            }

            if (request.UserId is null)
            {
                return Result.Failure<RoomAggregate, ValidationResult>(new BadRequestError([
                    new ValidationFailure(nameof(request.UserId), "UserId must be provided.")
                ]));
            }

            // Prevent deleting yourself
            if (authUser.Id == request.UserId.Value)
            {
                return Result.Failure<RoomAggregate, ValidationResult>(new BadRequestError([
                    new ValidationFailure(nameof(request.UserId), "User with userCode and id is the same user.")
                ]));
            }

            // Check whether target user exists and belongs to a different room (explicitly)
            var targetUserResult = await userRepository.GetByIdAsync(request.UserId.Value, cancellationToken, includeRoom: true);
            if (targetUserResult.IsFailure)
            {
                return targetUserResult.ConvertFailure<RoomAggregate>();
            }

            if (targetUserResult.Value.RoomId != authUser.RoomId)
            {
                return Result.Failure<RoomAggregate, ValidationResult>(new NotAuthorizedError([
                    new ValidationFailure("id", "User with userCode and user with Id belong to different rooms.")
                ]));
            }
            var deleteResult = room.DeleteUser(request.UserId);
            if (deleteResult.IsFailure)
            {
                return deleteResult;
            }

            var updateResult = await roomRepository.UpdateAsync(room, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Result.Failure<RoomAggregate, ValidationResult>(new BadRequestError([
                  new ValidationFailure(string.Empty, updateResult.Error)]));
            }

            var updatedRoomResult = await roomRepository.GetByUserCodeAsync(request.UserCode, cancellationToken);
            return updatedRoomResult;

        }
    }
}