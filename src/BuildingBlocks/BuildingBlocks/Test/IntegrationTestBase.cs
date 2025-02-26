using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BuildingBlocks.Test;

public class IntegrationTestBase<TFactory, TProgram, TDbContext> : IClassFixture<TFactory>, IDisposable
    where TFactory : BaseIntegrationTestWebAppFactory<TProgram, TDbContext>
    where TDbContext : DbContext
    where TProgram : class
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly TDbContext DbContext;

    protected IntegrationTestBase(TFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<TDbContext>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}