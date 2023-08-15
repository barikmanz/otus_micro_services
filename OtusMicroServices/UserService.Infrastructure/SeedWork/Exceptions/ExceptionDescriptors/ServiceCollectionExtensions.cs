using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExceptionDescriptors(this IServiceCollection services)
        {
            services
                .AddSingleton<IExceptionDescriptor, ValidationExceptionDescriptor>()
                .AddSingleton<IExceptionDescriptor, AccessForbiddenExceptionDescriptor>()
                .AddSingleton<IExceptionDescriptor, NotFoundExceptionDescriptor>()
                .AddSingleton<IExceptionDescriptor, UpdateConcurrencyDescriptor>()
                .AddSingleton<IExceptionDescriptor, UpdateConflictDescriptor>()
                .AddSingleton<IExceptionDescriptor, InternalInvalidOperationDescriptor>()
                .AddSingleton<IExceptionDescriptor, AccessValidationExceptionDescriptor>()
                .AddSingleton<IExceptionDescriptor, InternalExceptionDescriptor>();

            return services;
        }
    }
}