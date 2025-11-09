using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Common behavior for aggregate builders.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAggregateBuilder<TEntity> where TEntity : BaseAggregate
    {
        /// <summary>
        /// Build aggregate and validate.
        /// </summary>
        /// <returns>Result <see cref="Result"/> with aggregate or error.</returns>
        public Result<TEntity, ValidationResult> Build();

        /// <summary>
        /// Initial build aggregate and validate.
        /// </summary>
        /// <returns>Result <see cref="Result"/> with aggregate or error.</returns>
        public Result<TEntity, ValidationResult> InitialBuild();
    }
}