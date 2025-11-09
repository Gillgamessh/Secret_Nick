using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors
{
    /// <summary>
    /// Represents a "BadRequest" validation error.
    /// </summary>
    public class BadRequestError(IEnumerable<ValidationFailure> failures) : ValidationResult(failures);
}