using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.MassTransit;
using IdentityService.Data;
using IdentityService.Data.Seed;
using IdentityService.Extensions;
using IdentityService.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageBroker<ApplicationDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());

builder.AddInfrastructure();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.UseInfrastructure();

await SeedIdentityData.EnsureSeedData(app);

app.MapRegisterEndpoint();

app.Run();