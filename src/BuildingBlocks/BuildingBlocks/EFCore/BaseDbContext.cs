using BuildingBlocks.Core.Model;
using BuildingBlocks.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore;

public abstract class BaseDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddInboxStateEntity();
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}