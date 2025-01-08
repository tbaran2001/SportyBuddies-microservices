using Ardalis.GuardClauses;
using BuildingBlocks.Core.Model;
using BuildingBlocks.CQRS;
using BuildingBlocks.Web;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Enums;
using ProfileManagement.API.Profiles.Exceptions;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.API.Profiles.Features.Commands.UpdateProfilePreferences;

public record UpdateProfilePreferencesCommand(int MinAge, int MaxAge, int MaxDistance, Gender PreferredGender)
    : ICommand;

public record UpdateProfilePreferencesRequestDto(int MinAge, int MaxAge, int MaxDistance, Gender PreferredGender);

public class UpdateProfilePreferencesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("profiles/preferences", async (UpdateProfilePreferencesRequestDto request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProfilePreferencesCommand>();

                await sender.Send(command);

                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithName("UpdateProfilePreferences")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update profile preferences")
            .WithDescription("Update profile preferences of the current user");
    }
}

public class UpdateProfilePreferencesCommandValidator : AbstractValidator<UpdateProfilePreferencesCommand>
{
    public UpdateProfilePreferencesCommandValidator()
    {
        RuleFor(x => x.MinAge)
            .InclusiveBetween(18, 100);
        RuleFor(x => x.MaxAge)
            .InclusiveBetween(18, 100);
        RuleFor(x => x.MaxDistance)
            .InclusiveBetween(1, 100);
        RuleFor(x => x.PreferredGender)
            .IsInEnum();
    }
}

internal class UpdateProfilePreferencesCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProfilePreferencesCommand>
{
    public async Task<Unit> Handle(UpdateProfilePreferencesCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        profile.UpdatePreferences(Preferences.Create(command.MinAge, command.MaxAge, command.MaxDistance,
            command.PreferredGender));
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}