using GistCopy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GistCopy.Application.ModelConfigurations;

public class UserModelConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Username).IsRequired().HasMaxLength(32);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(64);
        builder.Property(x => x.TimeCreated).IsRequired();

        builder.HasMany(x => x.Gists).WithOne(x => x.User);
        builder.HasMany(x => x.Comments).WithOne(x => x.User);
    }
}