using Microsoft.AspNetCore.Mvc;

namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    /// <summary>
    /// Room read request.
    /// </summary>
    /// <param name="UserCode">Authorization code of the User.</param>
    /// <param name="RoomCode">Join code of the Room.</param>
    public record RoomReadingRequest(
        [FromQuery(Name = "userCode")] string? UserCode,
        [FromQuery(Name = "roomCode")] string? RoomCode);
}