using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;
using UserEntity = Epam.ItMarathon.ApiService.Domain.Entities.User.User;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Handlers
{
    /// <summary>
    /// Handler for User creation in the Room.
    /// </summary>
    /// <param name="roomRepository">Implementation of <see cref="IRoomRepository"/> for operating with database.</param>
    /// <param name="userRepository">Implementation of <see cref="IUserReadOnlyRepository"/> for operating with database.</param>
    public class CreateUserInRoomHandler(IRoomRepository roomRepository, IUserReadOnlyRepository userRepository) :
        IRequestHandler<CreateUserInRoomRequest, IResult<UserEntity, ValidationResult>>
    {
        ///<inheritdoc/>
        public async Task<IResult<UserEntity, ValidationResult>> Handle(CreateUserInRoomRequest request,
            CancellationToken cancellationToken)
        {
            var roomCode = request.RoomCode;
            var user = request.User;
            var roomFindResult = await roomRepository.GetByRoomCodeAsync(roomCode, cancellationToken);
            if (roomFindResult.IsFailure)
            {
                return roomFindResult.ConvertFailure<UserEntity>();
            }

            var userCode = Guid.NewGuid().ToString("N");
            var roomResult = roomFindResult.Value.AddUser(userBuilder => userBuilder
                .WithAuthCode(userCode)
                .WithIsAdmin(false)
                .WithFirstName(user.FirstName)
                .WithLastName(user.LastName)
                .WithPhone(user.Phone)
                .WithEmail(user.Email)
                .WithDeliveryInfo(user.DeliveryInfo)
                .WithWantSurprise(user.WantSurprise)
                .WithInterests(user.Interests)
                .WithWishes(user.Wishes)
            );

            if (roomResult.IsFailure)
            {
                return roomResult.ConvertFailure<UserEntity>();
            }

            var roomUpdatingResult = await roomRepository.UpdateAsync(roomResult.Value, cancellationToken);
            if (roomUpdatingResult.IsFailure)
            {
                return Result.Failure<UserEntity, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(roomCode), roomUpdatingResult.Error)
                ]));
            }

            return await userRepository.GetByCodeAsync(userCode, cancellationToken);
        }
    }
}