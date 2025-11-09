using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using UserEntity = Epam.ItMarathon.ApiService.Domain.Entities.User.User;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Queries
{
    /// <summary>
    /// Query for getting Users from Room.
    /// </summary>
    /// <param name="UserCode">User authorization code.</param>
    /// <param name="UserId">User's unique identifier.</param>
    public record GetUsersQuery(string UserCode, ulong? UserId)
        : IRequest<Result<List<UserEntity>, ValidationResult>>;
}