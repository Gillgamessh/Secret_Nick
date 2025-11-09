namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Basic aggregate abstraction.
    /// </summary>
    public abstract class BaseAggregate : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAggregate"/> class.
        /// </summary>
        protected BaseAggregate() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAggregate"/> class.
        /// </summary>
        /// <param name="id">Unique identifier to be set.</param>
        protected BaseAggregate(ulong id) : base(id)
        {
        }
    }
}