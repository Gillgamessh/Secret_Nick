namespace Epam.ItMarathon.ApiService.Api.Dto.BaseDtos
{
    /// <summary>
    /// Room base API's DTO.
    /// </summary>
    public class RoomBaseDto
    {
        /// <summary>
        /// Room's name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Room's description.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Planned date of Room draw.
        /// </summary>
        public required string GiftExchangeDate { get; set; }

        /// <summary>
        /// Gift maximum budget for the Room.
        /// </summary>
        public required ulong GiftMaximumBudget { get; set; }
    }
}