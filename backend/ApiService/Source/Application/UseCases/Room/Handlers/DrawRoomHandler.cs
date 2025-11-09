using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;
using UserEntity = Epam.ItMarathon.ApiService.Domain.Entities.User.User;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Handlers
{
    /// <summary>
    /// Handler for Room draw command.
    /// </summary>
    /// <param name="roomRepository">Implementation of <see cref="IRoomRepository"/> for operating with database.</param>
    public class DrawRoomHandler(IRoomRepository roomRepository)
        : IRequestHandler<DrawRoomCommand, Result<List<UserEntity>, ValidationResult>>
    {
        /// <inheritdoc/>
        public async Task<Result<List<UserEntity>, ValidationResult>> Handle(DrawRoomCommand request,
            CancellationToken cancellationToken)
        {
            // Get room by user.RoomId
            var roomResult = await roomRepository.GetByUserCodeAsync(request.UserCode, cancellationToken);
            if (roomResult.IsFailure)
            {
                return Result.Failure<List<UserEntity>, ValidationResult>(roomResult.Error);
            }

            // Get user by provided code and check user.IsAdmin
            var adminUser = roomResult.Value.Users.First(user => user.AuthCode.Equals(request.UserCode));
            if (!adminUser.IsAdmin)
            {
                return Result.Failure<List<UserEntity>, ValidationResult>(new ForbiddenError([
                    new ValidationFailure("userCode", "Only admin can draw the room.")
                ]));
            }

            // Draw room
            var drawResult = roomResult.Value.Draw();
            if (drawResult.IsFailure)
            {
                return Result.Failure<List<UserEntity>, ValidationResult>(drawResult.Error);
            }

            // Update room in DB
            var updatingResult = await roomRepository.UpdateAsync(drawResult.Value, cancellationToken);
            if (updatingResult.IsFailure)
            {
                return Result.Failure<List<UserEntity>, ValidationResult>(new BadRequestError([
                    new ValidationFailure(string.Empty, updatingResult.Error)
                ]));
            }

            return drawResult.Value.Users.ToList();
        }
    }
}