using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands
{
    /// <summary>
    /// Command for updating the Room.
    /// </summary>
    /// <param name="UserCode">Authorization code of User.</param>
    /// <param name="Name">Room's name.</param>
    /// <param name="Description">Room's description.</param>
    /// <param name="InvitationNote">Room's invitation note.</param>
    /// <param name="GiftExchangeDate">Room's gift exchange date.</param>
    /// <param name="GiftMaximumBudget">Room's maximum budget.</param>
    public record UpdateRoomCommand(
        string UserCode,
        string? Name,
        string? Description,
        string? InvitationNote,
        DateTime? GiftExchangeDate,
        ulong? GiftMaximumBudget) : IRequest<Result<RoomAggregate, ValidationResult>>;
}