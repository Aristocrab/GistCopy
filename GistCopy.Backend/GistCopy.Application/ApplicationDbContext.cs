using Microsoft.EntityFrameworkCore;
using GistCopy.Domain.Entities;

namespace GistCopy.Application;

public class ApplicationDbContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Gist> Gists { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}