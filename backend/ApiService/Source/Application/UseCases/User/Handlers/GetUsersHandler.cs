using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;
using UserEntity = Epam.ItMarathon.ApiService.Domain.Entities.User.User;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers
{
    /// <summary>
    /// Handler for Users query.
    /// </summary>
    /// <param name="userRepository">Implementation of <see cref="IUserReadOnlyRepository"/> for operating with database.</param>
    public class GetUsersHandler(IUserReadOnlyRepository userRepository)
        : IRequestHandler<GetUsersQuery, Result<List<UserEntity>, ValidationResult>>
    {
        ///<inheritdoc/>
        public async Task<Result<List<UserEntity>, ValidationResult>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var authUserResult = await userRepository.GetByCodeAsync(request.UserCode, cancellationToken,
                includeRoom: true, includeWishes: true);
            if (authUserResult.IsFailure)
            {
                return authUserResult.ConvertFailure<List<UserEntity>>();
            }

            if (request.UserId is null)
            {
                // Get all users in room
                var roomId = authUserResult.Value.RoomId;
                var result = await userRepository.GetManyByRoomIdAsync(roomId, cancellationToken);
                return result;
            }

            // Otherwise, Get user by id
            var requestedUserResult = await userRepository.GetByIdAsync(request.UserId.Value, cancellationToken,
                includeRoom: false, includeWishes: true);
            if (requestedUserResult.IsFailure)
            {
                return requestedUserResult.ConvertFailure<List<UserEntity>>();
            }

            if (requestedUserResult.Value.RoomId != authUserResult.Value.RoomId)
            {
                return Result.Failure<List<UserEntity>, ValidationResult>(new NotAuthorizedError([
                    new ValidationFailure("id", "User with userCode and user with Id belongs to different rooms.")
                ]));
            }

            return new List<UserEntity> { requestedUserResult.Value, authUserResult.Value };
        }
    }
}