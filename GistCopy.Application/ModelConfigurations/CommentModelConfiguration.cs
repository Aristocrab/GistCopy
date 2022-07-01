using GistCopy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GistCopy.Application.ModelConfigurations;

public class CommentModelConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Text).IsRequired().HasMaxLength(512);
        builder.Property(x => x.TimeCreated).IsRequired();
    }
}