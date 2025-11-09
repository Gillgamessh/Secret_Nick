namespace Epam.ItMarathon.ApiService.Application.Models.Creation
{
    /// <summary>
    /// User's application layer model.
    /// </summary>
    public class UserApplication
    {
        /// <summary>
        /// User's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// User's phone number.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// User's email.
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
        /// List of desired gifts in case when User doesn't want surprise. Represented as a list of tuples.
        /// </summary>
        public IEnumerable<(string?, string?)> Wishes { get; set; } = [];
    }
}