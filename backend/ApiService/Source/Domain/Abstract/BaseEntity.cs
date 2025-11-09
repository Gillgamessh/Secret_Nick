namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Base domain entity abstract class. All domain entities inherit it, made to share logic.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identifier of entity.
        /// </summary>
        public ulong Id { get; protected set; }

        /// <summary>
        /// Date when the entity was created.
        /// </summary>
        public DateTime CreatedOn { get; protected set; }

        /// <summary>
        /// Date when the entity was modified last time. Same time as CreatedOn when entity created.
        /// </summary>
        public DateTime ModifiedOn { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        protected BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = CreatedOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        /// <param name="id">Unique identifier to be set.</param>
        protected BaseEntity(ulong id)
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = CreatedOn;
            Id = id;
        }
    }
}