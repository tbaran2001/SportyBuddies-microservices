using BuildingBlocks.EFCore;
using Microsoft.Extensions.Options;
using Quartz;

namespace BuildingBlocks.Outbox;

public class ProcessOutboxMessagesJobSetup<TDbContext> : IConfigureOptions<QuartzOptions>
    where TDbContext : BaseDbContext
{
    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessOutboxMessagesJob<TDbContext>);

        options
            .AddJob<ProcessOutboxMessagesJob<TDbContext>>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(2).RepeatForever()));
    }
}