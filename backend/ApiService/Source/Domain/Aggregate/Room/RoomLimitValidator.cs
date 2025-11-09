using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    internal class RoomLimitValidator : AbstractValidator<Room>
    {
        public RoomLimitValidator()
        {
            MaxUsersLimitValidation();
            MaxWishesLimitValidation();
        }

        private void MaxUsersLimitValidation() =>
            RuleFor(room => room)
                .Must(room => room.Users.Count <= room.MaxUsersLimit)
                .WithMessage(user => $"Room {user.Id} exceeds the max users limit.")
                .WithName("userLimit")
                .OverridePropertyName("userLimit");

        private void MaxWishesLimitValidation() =>
            RuleForEach(room => room.Users)
                .Must((room, user) => user.Wishes.Count() <= room.MaxWishesLimit)
                .WithMessage(user => $"User {user.Name} exceeds the max wishes limit.")
                .WithName("wishesLimit")
                .OverridePropertyName("wishesLimit");
    }
}