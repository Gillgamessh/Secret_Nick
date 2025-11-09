using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Epam.ItMarathon.ApiService.Infrastructure.Repositories
{
    internal class UserReadOnlyRepository(AppDbContext context, IMapper mapper) : IUserReadOnlyRepository
    {
        /// <inheritdoc/>
        public async Task<Result<User, ValidationResult>> GetByCodeAsync(string userCode,
            CancellationToken cancellationToken, bool includeRoom = false, bool includeWishes = false)
        {
            var userQuery = context.Users.AsQueryable();
            if (includeRoom)
            {
                userQuery = userQuery.Include(user => user.Room);
            }

            if (includeWishes)
            {
                userQuery = userQuery.Include(user => user.Wishes);
            }

            var userEf = await userQuery.FirstOrDefaultAsync(user => user.AuthCode.Equals(userCode), cancellationToken);
            var result = userEf == null
                ? Result.Failure<User, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(userCode), "User with such code not found")
                ]))
                : mapper.Map<User>(userEf);
            return result;
        }

        /// <inheritdoc/>
        public async Task<Result<User, ValidationResult>> GetByIdAsync(ulong id, CancellationToken cancellationToken,
            bool includeRoom = false, bool includeWishes = false)
        {
            var userQuery = context.Users.AsQueryable();
            if (includeRoom)
            {
                userQuery = userQuery.Include(user => user.Room);
            }

            if (includeWishes)
            {
                userQuery = userQuery.Include(user => user.Wishes);
            }

            var userEf = await userQuery.FirstOrDefaultAsync(user => user.Id.Equals(id), cancellationToken);
            var result = userEf == null
                ? Result.Failure<User, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(id), "User with such id not found")
                ]))
                : mapper.Map<User>(userEf);
            return result;
        }

        /// <inheritdoc/>
        public async Task<Result<List<User>, ValidationResult>> GetManyByRoomIdAsync(ulong roomId,
            CancellationToken cancellationToken)
        {
            var usersEf = await context.Users
                .Include(user => user.Room)
                .Include(user => user.Wishes)
                .Where(user => user.RoomId == roomId)
                .ToListAsync(cancellationToken);

            var result = usersEf.Count == 0
                ? Result.Failure<List<User>, ValidationResult>(new NotFoundError([
                    new ValidationFailure("roomId", "Room with such id not found")
                ]))
                : mapper.Map<List<User>>(usersEf);
            return result;
        }
    }
}