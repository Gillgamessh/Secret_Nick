using Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.Requests.RoomRequests
{
    /// <summary>
    /// Room reading request's validator.
    /// </summary>
    public class RoomReadingRequestValidator : AbstractValidator<RoomReadingRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomReadingRequestValidator"/> class.
        /// </summary>
        public RoomReadingRequestValidator()
        {
            RuleFor(request => request)
                .Must(request => string.IsNullOrEmpty(request.UserCode) != string.IsNullOrEmpty(request.RoomCode))
                .WithMessage("Only one code (either userCode or roomCode) should be provided")
                .WithName("code")
                .OverridePropertyName("code");
        }
    }
}