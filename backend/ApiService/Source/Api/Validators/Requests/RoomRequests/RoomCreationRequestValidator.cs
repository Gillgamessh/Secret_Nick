using Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests;
using Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.Requests.RoomRequests
{
    /// <summary>
    /// Validator for Room creation request.
    /// </summary>
    public class RoomCreationRequestValidator : AbstractValidator<RoomCreationRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomCreationRequestValidator"/> class.
        /// </summary>
        public RoomCreationRequestValidator()
        {
            RuleFor(roomRequest => roomRequest.Room)
                .SetValidator(new RoomDtoValidator())
                .WithName("room")
                .OverridePropertyName("room");
        }
    }
}