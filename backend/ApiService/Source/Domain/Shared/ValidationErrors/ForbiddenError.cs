using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors
{
    /// <summary>
    /// Represents a "Forbidden" validation error.
    /// </summary>
    public class ForbiddenError(IEnumerable<ValidationFailure> failures) : ValidationResult(failures);
}