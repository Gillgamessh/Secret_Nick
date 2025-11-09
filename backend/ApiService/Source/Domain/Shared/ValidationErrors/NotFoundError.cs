using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors
{
    /// <summary>
    /// Represents a "Not Found" validation error.
    /// </summary>
    public class NotFoundError(IEnumerable<ValidationFailure> failures) : ValidationResult(failures);
}