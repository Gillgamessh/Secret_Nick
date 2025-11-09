namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models
{
    /// <summary>
    /// Base abstract class for all entity framework entities.
    /// </summary>
    internal abstract class BaseModelEf
    {
        /// <summary>
        /// Unique identifier of Entity.
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Date when entity was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Last modified date.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}