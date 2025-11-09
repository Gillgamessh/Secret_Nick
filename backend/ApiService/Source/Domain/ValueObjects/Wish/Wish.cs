namespace Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish
{
    /// <summary>
    /// Record class which represents a Wish value-object.
    /// </summary>
    public sealed record Wish
    {
        internal const int NameCharLimit = 40;

        /// <summary>
        /// Name of the wish.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Link to the wish.
        /// </summary>
        public string? InfoLink { get; init; }

        private Wish(string? name, string? infoLink)
        {
            Name = name;
            InfoLink = infoLink;
        }

        internal static Wish Create(string? name, string? infoLink) => new(name, infoLink);
    }
}