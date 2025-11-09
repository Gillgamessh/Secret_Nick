using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using UserEntity = Epam.ItMarathon.ApiService.Domain.Entities.User.User;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands
{
    /// <summary>
    /// Command for drawing the Room.
    /// </summary>
    /// <param name="UserCode">User's authorization code.</param>
    public record DrawRoomCommand(string UserCode)
        : IRequest<Result<List<UserEntity>, ValidationResult>>;
}