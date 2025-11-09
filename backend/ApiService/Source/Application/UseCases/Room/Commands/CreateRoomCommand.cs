using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands
{
    /// <summary>
    /// Command for creating a Room with an admin User.
    /// </summary>
    /// <param name="Room"><see cref="RoomApplication"/> entity.</param>
    /// <param name="Admin">Admin <see cref="UserApplication"/> entity.</param>
    public record CreateRoomCommand(RoomApplication Room, UserApplication Admin)
        : IRequest<Result<RoomAggregate, ValidationResult>>;
}