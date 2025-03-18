using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BuildingBlocks.Outbox;

public static class Extensions
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        //services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();

        return services;
    }
}