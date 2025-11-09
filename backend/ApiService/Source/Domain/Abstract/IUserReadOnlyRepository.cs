using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Readonly repository for <see cref="User"/> aggregate.
    /// </summary>
    public interface IUserReadOnlyRepository
    {
        /// <summary>
        /// Get User by unique User's code.
        /// </summary>
        /// <param name="userCode">Unique User's code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        /// <param name="includeRoom">Include dependent Room to response.</param>
        /// <param name="includeWishes">Include list of dependent wishes to response.</param>
        /// <returns>Returns <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<User, ValidationResult>> GetByCodeAsync(string userCode, CancellationToken cancellationToken,
            bool includeRoom = false, bool includeWishes = false);

        /// <summary>
        /// Get User by unique User's unique identifier.
        /// </summary>
        /// <param name="id">Unique User's unique identifier.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        /// <param name="includeRoom">Include dependent Room to response.</param>
        /// <param name="includeWishes">Include list of dependent wishes to response.</param>
        /// <returns>Returns <see cref="User"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<User, ValidationResult>> GetByIdAsync(ulong id, CancellationToken cancellationToken,
            bool includeRoom = false, bool includeWishes = false);

        /// <summary>
        /// Get all Users in Room by Room's id.
        /// </summary>
        /// <param name="roomId">Unique room id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        /// <returns>Returns list of <see cref="User"/> if found, otherwise <see cref="ValidationResult"/>.</returns>
        public Task<Result<List<User>, ValidationResult>> GetManyByRoomIdAsync(ulong roomId,
            CancellationToken cancellationToken);
    }
}