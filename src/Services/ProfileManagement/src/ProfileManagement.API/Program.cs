using ProfileManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseInfrastructure();

app.Run();