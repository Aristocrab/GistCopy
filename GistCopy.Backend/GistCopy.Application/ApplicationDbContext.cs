using Microsoft.EntityFrameworkCore;
using GistCopy.Domain.Entities;

namespace GistCopy.Application;

public class ApplicationDbContext : DbContext
{
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Gist> Gists { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}