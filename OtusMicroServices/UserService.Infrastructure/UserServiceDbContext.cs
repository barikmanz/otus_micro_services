using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;

namespace UserService.Infrastructure;

public class UserServiceDbContext : DbContext
{
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        Assembly[] assembliesWithConfigurations =
        {
            GetType().Assembly
        };
        foreach (var assembly in assembliesWithConfigurations)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}