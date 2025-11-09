using FluentValidation;
using System.Reflection;

namespace Epam.ItMarathon.ApiService.Api.Filters.Validation
{
    /// <summary>
    /// Static class that connects logic for ValidationAttribute and ValidationDescriptor.
    /// Checks if the current method's context contains argument with provided attribute, and validates it.
    /// Factory patter is preferred due to its ability to access MethodInfo.
    /// </summary>
    public static class ValidationFactoryFilter
    {
        /// <summary>
        /// Retrieve the filter factory.
        /// </summary>
        /// <param name="context">Provides metadata about the endpoint being built when you’re creating filters dynamically.</param>
        /// <param name="next">Delegate of the next filter in pipeline.</param>
        /// <returns>Returns <see cref="EndpointFilterDelegate"/> from the context.</returns>
        public static EndpointFilterDelegate GetValidationFactory(EndpointFilterFactoryContext context,
            EndpointFilterDelegate next)
        {
            var validationDescriptors = GetValidators(context.MethodInfo, context.ApplicationServices);

            if (validationDescriptors.Any())
            {
                return invocationContext => Validate(validationDescriptors, invocationContext, next);
            }

            return next;
        }

        private static async ValueTask<object?> Validate(IEnumerable<ValidationDescriptor> validationDescriptors,
            EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
        {
            foreach (var descriptor in validationDescriptors)
            {
                var argument = invocationContext.Arguments[descriptor.ArgumentIndex];
                if (argument is null)
                {
                    continue;
                }

                var validationResult = await descriptor.Validator.ValidateAsync(
                    new ValidationContext<object>(argument)
                );

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
            }

            return await next.Invoke(invocationContext);
        }

        private static IEnumerable<ValidationDescriptor> GetValidators(MethodInfo methodInfo,
            IServiceProvider serviceProvider)
        {
            foreach (var item in methodInfo.GetParameters().Select((parameter, index) => new { parameter, index }))
            {
                if (item.parameter.GetCustomAttribute<ValidateAttribute>() == null)
                {
                    continue;
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(item.parameter.ParameterType);
                if (serviceProvider.GetService(validatorType) is IValidator validator)
                {
                    yield return new ValidationDescriptor
                    {
                        ArgumentIndex = item.index, ArgumentType = item.parameter.ParameterType,
                        Validator = validator
                    };
                }
            }
        }
    }
}