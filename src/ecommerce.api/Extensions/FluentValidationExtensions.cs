using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace ecommerce.api.Extensions
{
    public static partial class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services, string namespaceName)
        {
            var typesInNamespace = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(
                    type =>
                        type.Namespace is not null
                        && type.Namespace.StartsWith(namespaceName)
                        && !type.IsAbstract
                        && !type.IsInterface
                        && type.Name.EndsWith("Validator")
                );

            // Iterate through the types and register them with the service collection
            foreach (var type in typesInNamespace)
            {
                // Find the implemented interface IValidator<T>
                var validatorInterface = type.GetInterfaces()
                    .FirstOrDefault(
                        i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)
                    );

                // If the type implements IValidator<T>, register it with the service collection
                if (validatorInterface is not null)
                {
                    var genericArgument = validatorInterface.GetGenericArguments()[0];
                    var serviceType = typeof(IValidator<>).MakeGenericType(genericArgument);
                    services.AddScoped(serviceType, type);
                }
            }

            services.AddFluentValidationAutoValidation();

            return services;
        }

    }
}
