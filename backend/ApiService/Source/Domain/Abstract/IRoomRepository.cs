using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Repository for <see cref="Room"/> aggregate.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Add new Room to the repository.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> AddAsync(Room item, CancellationToken cancellationToken);

        /// <summary>
        /// Update existing room in the repository.
        /// </summary>
        /// <param name="roomToUpdate">Item to be updated.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        public Task<Result> UpdateAsync(Room roomToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Get Room by unique User code
        /// </summary>
        /// <param name="userCode">Unique user code</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> GetByUserCodeAsync(string userCode,
            CancellationToken cancellationToken);

        /// <summary>
        /// Get Room by unique room code
        /// </summary>
        /// <param name="roomCode">Unique Room code</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> that can be used to cancel operation.</param>
        /// <returns>Returns <see cref="Room"/> if found, otherwise <see cref="ValidationResult"/></returns>
        public Task<Result<Room, ValidationResult>> GetByRoomCodeAsync(string roomCode,
            CancellationToken cancellationToken);
    }
}