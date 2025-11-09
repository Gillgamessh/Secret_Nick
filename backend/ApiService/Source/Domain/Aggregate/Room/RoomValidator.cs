using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    internal class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            #region Name

            RuleFor(room => room.Name)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("name")
                .OverridePropertyName("name");

            #endregion

            #region Description

            RuleFor(room => room.Description)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("description")
                .OverridePropertyName("description");

            #endregion

            #region GiftExchangeDate

            RuleFor(room => room.GiftExchangeDate)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");

            #endregion

            #region GiftMaximumBudget

            RuleFor(room => room.GiftMaximumBudget)
                .NotNull()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("giftMaximumBudget")
                .OverridePropertyName("giftMaximumBudget");

            #endregion

            NameValidation();
            DescriptionValidation();
            InvitationNoteValidation();
            GiftExchangeDateValidation();
            GiftMinimumBudgetValidation();
            GiftMaximumBudgetValidation();
            LimitValidation();
            UsersValidation();
        }

        private void NameValidation() =>
            RuleFor(room => room.Name)
                .MaximumLength(Room.NameCharLimit)
                .WithMessage($"Maximum length is {Room.NameCharLimit}.")
                .WithName("name")
                .OverridePropertyName("name");

        private void DescriptionValidation() =>
            RuleFor(room => room.Description)
                .MaximumLength(Room.DescriptionCharLimit)
                .WithMessage($"Maximum length is {Room.DescriptionCharLimit}.")
                .WithName("description")
                .OverridePropertyName("description");

        private void InvitationNoteValidation() =>
            RuleFor(room => room)
                .Must(room => room.InvitationNote.Length <= Room.InvitationNoteCharLimit)
                .WithMessage($"Maximum length is {Room.InvitationNoteCharLimit}.")
                .OverridePropertyName("invitationNote");

        private void GiftExchangeDateValidation() =>
            RuleFor(room => room.GiftExchangeDate)
                .Must(DateIsNotPast)
                .WithMessage("Timestamp must be not before today.")
                .WithName("giftExchangeDate")
                .OverridePropertyName("giftExchangeDate");

        private void GiftMinimumBudgetValidation() =>
            RuleFor(room => room.GiftMaximumBudget)
                .GreaterThanOrEqualTo(ulong.MinValue)
                .WithMessage("giftMaximumBudget must be greater or equal to 0.")
                .WithName("giftMaximumBudget")
                .OverridePropertyName("giftMaximumBudget");

        private void GiftMaximumBudgetValidation() =>
            RuleFor(room => room.GiftMaximumBudget)
                .LessThanOrEqualTo(Room.RoomMaximumBudget)
                .WithMessage($"giftMaximumBudget must be less or equal to {Room.RoomMaximumBudget}.")
                .WithName("giftMaximumBudget")
                .OverridePropertyName("giftMaximumBudget");

        private void LimitValidation() =>
            RuleFor(room => room)
                .SetValidator(new RoomLimitValidator())
                .WithName("limitsValidation")
                .OverridePropertyName("limitsValidation");

        private void UsersValidation() =>
            RuleForEach(room => room.Users).SetValidator(new UserValidator());

        private static bool DateIsNotPast(DateTime date) =>
            date >= DateTime.UtcNow.Date;
    }
}