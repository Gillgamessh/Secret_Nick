using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift
{
    internal class GiftEf : BaseModelEf
    {
        /// <summary>
        /// Unique identifier of User which gift belongs to.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Name of gift.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Link to gift.
        /// </summary>
        public string? InfoLink { get; set; }

        /// <summary>
        /// Mapping property to User.
        /// </summary>
        public UserEf? User { get; set; }
    }
}