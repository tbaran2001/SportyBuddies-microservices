using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BuildingBlocks.Test;

public class IntegrationTestBase<TFactory, TProgram, TDbContext, TReadDbContext> : IClassFixture<TFactory>, IDisposable
    where TFactory : IntegrationTestWebAppFactoryBase<TProgram, TDbContext, TReadDbContext>
    where TProgram : class
    where TDbContext : DbContext
    where TReadDbContext : class
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly TDbContext DbContext;
    protected readonly TReadDbContext ReadDbContext;

    protected IntegrationTestBase(TFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<TDbContext>();
        ReadDbContext = _scope.ServiceProvider.GetRequiredService<TReadDbContext>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}