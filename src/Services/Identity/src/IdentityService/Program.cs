using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;
using IdentityService.Data.Seed;
using IdentityService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

builder.AddInfrastructure();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseInfrastructure();

await SeedIdentityData.EnsureSeedData(app);

app.Run();