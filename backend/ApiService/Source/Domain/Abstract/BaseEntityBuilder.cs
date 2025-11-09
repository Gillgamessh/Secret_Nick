namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Base class for implementing a factory for domain entities.
    /// </summary>
    /// <typeparam name="TFactory">Factory type for fluent chaining.</typeparam>
    public abstract class BaseEntityBuilder<TFactory> where TFactory : BaseEntityBuilder<TFactory>
    {
        /// <summary>
        /// Value to store by unique identifier.
        /// </summary>
        protected ulong _id;

        /// <summary>
        /// Value to store date when was created.
        /// </summary>
        protected DateTime _createdOn;

        /// <summary>
        /// Value to store when entity was modified.
        /// </summary>
        protected DateTime _modifiedOn;

        /// <summary>
        /// (USED ONLY BY MAPPERS) Set a unique identifier for entity.
        /// </summary>
        /// <param name="id">Unique identifier of entity.</param>
        /// <returns>Reference to current factory.</returns>
        public TFactory WithId(ulong id)
        {
            _id = id;
            return (TFactory)this;
        }

        /// <summary>
        /// (USED ONLY BY MAPPERS) Set a creation date for entity.
        /// </summary>
        /// <param name="createdOn">Creation date of entity.</param>
        /// <returns>Reference to current factory.</returns>
        public TFactory WithCreatedOn(DateTime createdOn)
        {
            _createdOn = createdOn;
            return (TFactory)this;
        }

        /// <summary>
        /// Set a modification date.
        /// </summary>
        /// <param name="updatedOn">Date when entity was updated.</param>
        /// <returns>Reference to current factory.</returns>
        public TFactory WithModifiedOn(DateTime updatedOn)
        {
            _modifiedOn = updatedOn;
            return (TFactory)this;
        }
    }
}