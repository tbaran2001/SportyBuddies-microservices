using BuildingBlocks.Mongo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MongoDb;
using Testcontainers.MsSql;
using Xunit;

namespace BuildingBlocks.Test;

public abstract class IntegrationTestWebAppFactoryBase<TProgram, TDbContext, TReadDbContext>
    : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
    where TDbContext : DbContext
    where TReadDbContext : class
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    private readonly MongoDbContainer _readDbContainer = new MongoDbBuilder()
        .WithImage("mongo:latest")
        .WithPortBinding(27017, true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<TDbContext>));
            services.AddDbContext<TDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(_dbContainer.GetConnectionString());
            });

            services.RemoveAll<TReadDbContext>();
            services.RemoveAll<MongoOptions>();

            services.Configure<MongoOptions>(options =>
            {
                options.ConnectionString = _readDbContainer.GetConnectionString();
                options.DatabaseName = "TestDatabase";
            });
            services.AddSingleton<TReadDbContext>();
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _readDbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _readDbContainer.StopAsync();
    }
}