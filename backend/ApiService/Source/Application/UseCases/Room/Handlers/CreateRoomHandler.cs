using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Builders;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Handlers
{
    /// <summary>
    /// Handler for Room creation's command.
    /// </summary>
    /// <param name="roomRepository">Implementation of <see cref="IRoomRepository"/> for operating with database.</param>
    public class CreateRoomHandler(IRoomRepository roomRepository)
        : IRequestHandler<CreateRoomCommand, Result<RoomAggregate, ValidationResult>>
    {
        ///<inheritdoc/>
        public async Task<Result<RoomAggregate, ValidationResult>> Handle(CreateRoomCommand request,
            CancellationToken cancellationToken)
        {
            var adminRequest = request.Admin;
            var roomRequest = request.Room;
            var roomBuilderResult = RoomBuilder.Init()
                .WithName(roomRequest.Name)
                .WithDescription(roomRequest.Description)
                .WithGiftExchangeDate(roomRequest.GiftExchangeDate)
                .WithGiftMaximumBudget(roomRequest.GiftMaximumBudget)
                .WithInvitationCode(Guid.NewGuid().ToString("N"))
                .InitialAddUser(userBuilder => userBuilder
                    .WithAuthCode(Guid.NewGuid().ToString("N"))
                    .WithIsAdmin(true)
                    .WithFirstName(adminRequest.FirstName)
                    .WithLastName(adminRequest.LastName)
                    .WithPhone(adminRequest.Phone)
                    .WithEmail(adminRequest.Email)
                    .WithDeliveryInfo(adminRequest.DeliveryInfo)
                    .WithWantSurprise(adminRequest.WantSurprise)
                    .WithInterests(adminRequest.Interests)
                    .WithWishes(adminRequest.Wishes))
                .InitialBuild();

            return roomBuilderResult.IsFailure
                ? roomBuilderResult
                : await roomRepository.AddAsync(roomBuilderResult.Value, cancellationToken);
        }
    }
}