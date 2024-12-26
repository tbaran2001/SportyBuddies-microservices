using Microsoft.AspNetCore.RateLimiting;
using YarpApiGateway.Identity;
using YarpApiGateway.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.Window = TimeSpan.FromSeconds(10);
        limiterOptions.PermitLimit = 5;
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.InitializeDatabaseAsync();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi();

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();