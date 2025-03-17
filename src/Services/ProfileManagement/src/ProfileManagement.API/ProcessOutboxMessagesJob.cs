using BuildingBlocks.Outbox;
using Newtonsoft.Json;
using Quartz;

namespace ProfileManagement.API;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob(
    ApplicationDbContext dbContext,
    IPublisher publisher,
    ILogger<ProcessOutboxMessagesJob> logger)
    : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Beginning to process outbox messages");

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        var outboxMessages = await GetOutboxMessagesAsync();

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
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "Exception while processing outbox message {MessageId}",
                    outboxMessage.Id);

                exception = caughtException;
            }

            await UpdateOutboxMessageAsync(outboxMessage, exception);
        }

        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        logger.LogInformation("Completed processing outbox messages");
    }

    private async Task<List<OutboxMessage>> GetOutboxMessagesAsync()
    {
        return await dbContext.Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(10)
            .ToListAsync();
    }

    private async Task UpdateOutboxMessageAsync(OutboxMessage outboxMessage, Exception exception)
    {
        outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        outboxMessage.Error = exception?.ToString();

        dbContext.Set<OutboxMessage>().Update(outboxMessage);
        await dbContext.SaveChangesAsync();
    }
}