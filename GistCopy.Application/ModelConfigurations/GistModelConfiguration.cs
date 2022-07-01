using GistCopy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GistCopy.Application.ModelConfigurations;

public class GistModelConfiguration : IEntityTypeConfiguration<Gist>
{
    public void Configure(EntityTypeBuilder<Gist> builder)
    {
        builder.ToTable("Gists");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(256);
        builder.Property(x => x.Filename).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Text).IsRequired().HasMaxLength(3000);
        builder.Property(x => x.TimeCreated).IsRequired();

        builder.HasMany(x => x.Comments).WithOne(x => x.Gist);
    }
}