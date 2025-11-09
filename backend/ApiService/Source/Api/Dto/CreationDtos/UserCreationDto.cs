namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    /// <summary>
    /// Creation DTO for User.
    /// </summary>
    public class UserCreationDto
    {
        /// <summary>
        /// First name of User.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Last name of User.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Phone number of User.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// Email of User.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Delivery info for receiving Gift by User.
        /// </summary>
        public required string DeliveryInfo { get; set; }

        /// <summary>
        /// Indicates whether User wants surprise instead of selection from Wish List.
        /// </summary>
        public required bool WantSurprise { get; set; } = true;

        /// <summary>
        /// Interests of User in case when he/she wants surprise.
        /// </summary>
        public string? Interests { get; set; }

        /// <summary>
        /// List of desired gifts in case when User doesn't want surprise.
        /// </summary>
        public IEnumerable<WishDto>? WishList { get; set; } = [];
    }
}