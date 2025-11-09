using FluentValidation;

namespace Epam.ItMarathon.ApiService.Api.Filters.Validation
{
    /// <summary>
    /// Descriptor of validating argument.
    /// </summary>
    public class ValidationDescriptor
    {
        /// <summary>
        /// Position of the parameter.
        /// </summary>
        public required int ArgumentIndex { get; init; }

        /// <summary>
        /// Type of argument.
        /// </summary>
        public required Type ArgumentType { get; init; }

        /// <summary>
        /// Related validator for that type <see cref="IValidator"/> for proper validator implementing.
        /// </summary>
        public required IValidator Validator { get; init; }
    }
}