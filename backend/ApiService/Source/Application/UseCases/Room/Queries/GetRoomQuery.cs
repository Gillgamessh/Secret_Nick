using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Queries
{
    /// <summary>
    /// Query to get a Room.
    /// </summary>
    /// <param name="UserCode">Authorization code of the User.</param>
    /// <param name="RoomCode">Join code of the Room.</param>
    public record GetRoomQuery(string? UserCode, string? RoomCode)
        : IRequest<Result<RoomAggregate, ValidationResult>>;
}