namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    /// <summary>
    /// Patch request for the Room.
    /// </summary>
    public class RoomPatchRequest
    {
        /// <summary>
        /// Room's name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Room's description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Room's invitation note.
        /// </summary>
        public string? InvitationNote { get; set; }

        /// <summary>
        /// Room's gift exchange date.
        /// </summary>
        public DateTime? GiftExchangeDate { get; set; }

        /// <summary>
        /// Gift maximum budget.
        /// </summary>
        public ulong? GiftMaximumBudget { get; set; }
    }
}