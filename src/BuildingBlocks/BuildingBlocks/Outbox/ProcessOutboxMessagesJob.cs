using BuildingBlocks.Core.Event;
using BuildingBlocks.EFCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace BuildingBlocks.Outbox;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob<TDbContext>(
    TDbContext dbContext,
    IPublisher publisher,
    ILogger<ProcessOutboxMessagesJob<TDbContext>> logger)
    : IJob
    where TDbContext : BaseDbContext
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Beginning to process outbox messages");

        await using var transaction = await dbContext.Database.BeginTransactionAsync(context.CancellationToken);

        var outboxMessages = await dbContext.OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(10)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in outboxMessages)
        {
            Exception exception = null;

            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    JsonSerializerSettings)!;

                await publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception while processing outbox message {MessageId}", outboxMessage.Id);
                exception = ex;
            }

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            outboxMessage.Error = exception?.ToString();

            dbContext.OutboxMessages.Update(outboxMessage);
            await dbContext.SaveChangesAsync(context.CancellationToken);
        }

        await transaction.CommitAsync(context.CancellationToken);

        logger.LogInformation("Completed processing outbox messages");
    }
}