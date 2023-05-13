using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Users.Repository;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.SeedWork;
using UserService.Infrastructure.SeedWork.UnitOfWork;

namespace UserService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserServiceDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(UserService));

        var logQueryParams = configuration.GetSection("LogQueryParams")?.Get<bool>() ?? false;
        services.AddDbContext<UserServiceDbContext>(builder =>
        {
            builder.UseNpgsql(connectionString, optionsBuilder => { optionsBuilder.EnableRetryOnFailure(); });
            if (logQueryParams)
            {
                builder.EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<IUnitOfWork, UnitOfWork<UserServiceDbContext>>();
        services.AddScoped<IDatabaseSchemeUpdater, DatabaseSchemeUpdater<UserServiceDbContext>>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    
    
}