using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Users;

namespace UserService.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(v => v.Value, v => new UserKey(v));

        builder.Property(u => u.Email).HasMaxLength(255);
        builder.Property(u => u.Phone).HasMaxLength(255);
        builder.Property(u => u.UserName).HasMaxLength(100);
        builder.Property(u => u.FirstName).HasMaxLength(150);
        builder.Property(u => u.LastName).HasMaxLength(150);
    }
}