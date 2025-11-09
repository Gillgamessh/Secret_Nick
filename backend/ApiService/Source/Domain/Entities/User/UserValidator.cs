using Epam.ItMarathon.ApiService.Domain.Shared;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;
using FluentValidation;

namespace Epam.ItMarathon.ApiService.Domain.Entities.User
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            #region FirstName

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("firstName")
                .OverridePropertyName("firstName");

            #endregion

            #region LastName

            RuleFor(user => user.LastName)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("lastName")
                .OverridePropertyName("lastName");

            #endregion

            #region Phone

            RuleFor(user => user.Phone)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("phone")
                .OverridePropertyName("phone");

            #endregion

            #region DeliveryInfo

            RuleFor(user => user.DeliveryInfo)
                .NotEmpty()
                .WithMessage(ValidationConstants.RequiredMessage)
                .WithName("deliveryInfo")
                .OverridePropertyName("deliveryInfo");

            #endregion

            FirstNameValidation();
            LastNameValidation();
            DeliveryInfoValidation();
            InterestsValidation();
            EmailValidation();
            PhoneValidation();
            WishListValidation();
            WishDuplicateValidation();
        }

        private void FirstNameValidation() =>
            RuleFor(user => user.FirstName)
                .MaximumLength(User.FirstNameCharLimit)
                .WithMessage($"Maximum length is {User.FirstNameCharLimit}.")
                .WithName("firstName")
                .OverridePropertyName("firstName");

        private void LastNameValidation() =>
            RuleFor(user => user.LastName)
                .MaximumLength(User.LastNameCharLimit)
                .WithMessage($"Maximum length is {User.LastNameCharLimit}.")
                .WithName("lastName")
                .OverridePropertyName("lastName");

        private void DeliveryInfoValidation() =>
            RuleFor(user => user.DeliveryInfo)
                .MaximumLength(User.DeliveryInfoCharLimit)
                .WithMessage($"Maximum length is {User.DeliveryInfoCharLimit}.")
                .WithName("deliveryInfo")
                .OverridePropertyName("deliveryInfo");

        private void InterestsValidation()
        {
            RuleFor(user => user.Interests)
                .NotEmpty()
                .When(user => user.WantSurprise)
                .WithMessage("Interests should be provided if user does want surprise.")
                .WithName("interests")
                .OverridePropertyName("interests");
            RuleFor(user => user.Interests)
                .Empty()
                .When(user => !user.WantSurprise)
                .WithMessage("Interests should not be provided if user does not want surprise.")
                .WithName("interests")
                .OverridePropertyName("interests");
        }

        private void EmailValidation() =>
            RuleFor(user => user.Email)
                .Matches("^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9!#$%&'*+/=?^_{|}~-]+"
                         + @"(?:\.[a-zA-Z0-9!#$%&'*+/=?^_{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}"
                         + @"[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")
                .When(user => !string.IsNullOrEmpty(user.Email))
                .WithMessage("Email must be valid if provided.")
                .WithName("email")
                .OverridePropertyName("email");

        private void PhoneValidation() =>
            RuleFor(user => user.Phone)
                .Matches(@"^\+380\d{9}$")
                .WithMessage("Phone number must be a valid Ukrainian number in the format +380XXXXXXXXX")
                .WithName("phone")
                .OverridePropertyName("phone");

        private void WishListValidation()
        {
            RuleForEach(user => user.Wishes)
                .SetValidator(new WishValidator());
            RuleFor(user => user.Wishes)
                .NotEmpty()
                .When(user => !user.WantSurprise)
                .WithMessage("At least one wish should be provided if user does not want surprise.")
                .WithName("wishList")
                .OverridePropertyName("wishList");
            RuleFor(user => user.Wishes)
                .Empty()
                .When(user => user.WantSurprise)
                .WithMessage("Wishes should not be provided if user want surprise.")
                .WithName("wishList")
                .OverridePropertyName("wishList");
        }

        private void WishDuplicateValidation() =>
            RuleFor(user => user.Wishes)
                .Must(HaveNoDuplicates)
                .WithMessage("WishList should not contain duplicates.")
                .WithName("wishList")
                .OverridePropertyName("wishList");

        private bool HaveNoDuplicates(IEnumerable<Wish> entities)
        {
            if (entities == null || !entities.Any())
            {
                return true;
            }

            return entities
                .Select(wish => wish.GetHashCode())
                .GroupBy(hashCode => hashCode)
                .All(groups => groups.Count() == 1);
        }
    }
}