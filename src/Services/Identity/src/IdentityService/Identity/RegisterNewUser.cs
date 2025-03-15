using BuildingBlocks.CQRS;
using BuildingBlocks.Events.Identity;
using FluentValidation;
using IdentityService.Data;
using IdentityService.Models;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Identity;

public record RegisterNewUserCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string Name) : ICommand<RegisterNewUserResult>;

public record RegisterNewUserResult(Guid Id, string Name);

public record RegisterNewUserRequestDto(
    string Email,
    string Password,
    string ConfirmPassword,
    string Name);

public record RegisterNewUserResponseDto(Guid Id, string Name);

public static class RegisterNewUserEndpoint
{
    public static IEndpointRouteBuilder MapRegisterEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost($"/identity/register-user", async (
                RegisterNewUserRequestDto request, IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<RegisterNewUserCommand>();

                var result = await mediator.Send(command, cancellationToken);

                var response = result.Adapt<RegisterNewUserResponseDto>();

                return Results.Ok(response);
            })
            .WithName("RegisterUser")
            .Produces<RegisterNewUserResponseDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register User")
            .WithDescription("Register User");

        return builder;
    }
}

public class RegisterNewUserValidator : AbstractValidator<RegisterNewUserCommand>
{
    public RegisterNewUserValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter the password");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Please enter the confirmation password");

        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.Password != x.ConfirmPassword)
            {
                context.AddFailure(nameof(x.Password), "Passwords should match");
            }
        });

        RuleFor(x => x.Email).NotEmpty().WithMessage("Please enter the last email")
            .EmailAddress().WithMessage("A valid email is required");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter the name");
    }
}

internal class RegisterNewUserHandler(UserManager<ApplicationUser> userManager, IPublishEndpoint publishEndpoint, ApplicationDbContext dbContext)
    : ICommandHandler<RegisterNewUserCommand, RegisterNewUserResult>
{
    public async Task<RegisterNewUserResult> Handle(RegisterNewUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var applicationUser = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            PasswordHash = request.Password,
        };

        var identityResult = await userManager.CreateAsync(applicationUser, request.Password);
        var roleResult = await userManager.AddToRoleAsync(applicationUser, Constants.Role.User);

        if (identityResult.Succeeded == false)
            throw new Exception(string.Join(',', identityResult.Errors.Select(e => e.Description)));

        if (roleResult.Succeeded == false)
            throw new Exception(string.Join(',', roleResult.Errors.Select(e => e.Description)));

        var integrationEvent = new UserRegisteredIntegrationEvent
        {
            UserId = applicationUser.Id,
            Name = request.Name,
        };
        await publishEndpoint.Publish(integrationEvent, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterNewUserResult(applicationUser.Id, request.Name);
    }
}