using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Validators.Common;
using Epam.ItMarathon.ApiService.Domain.Shared;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Validators.CreationDtosValidators
{
    /// <summary>
    /// Validation for Room.
    /// </summary>
    public class RoomDtoValidator : AbstractValidator<RoomCreationDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomDtoValidator"/> class.
        /// </summary>
        public RoomDtoValidator()
        {
            #region GiftExchangeDate

            RuleFor(room => room.GiftExchangeDate)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");
            RuleFor(room => room.GiftExchangeDate)
                .Must(DateValidators.DateCorrectUtcIsoFormat)
                .WithMessage("Timestamp must be a valid UTC ISO 8601.")
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");

            #endregion
        }
    }
}