using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UserService.Infrastructure.SeedWork.JsonSerializers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonSerializer(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .TryAddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();

            return services;
        }
    }
}