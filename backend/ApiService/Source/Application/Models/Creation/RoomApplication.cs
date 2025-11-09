namespace Epam.ItMarathon.ApiService.Application.Models.Creation
{
    /// <summary>
    /// Application layer model for the Room.
    /// </summary>
    public class RoomApplication
    {
        /// <summary>
        /// Name of the Room.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Description of the Room.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gift exchange date of the Room.
        /// </summary>
        public required DateTime GiftExchangeDate { get; set; }

        /// <summary>
        /// Gift maximum budget for the Room.
        /// </summary>
        public required ulong GiftMaximumBudget { get; set; }
    }
}