
var builder = WebApplication.CreateBuilder(args);

var assembly= typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();

app.MapCarter();

app.Run();