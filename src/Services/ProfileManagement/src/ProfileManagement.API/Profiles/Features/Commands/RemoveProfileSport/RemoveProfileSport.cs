using Ardalis.GuardClauses;
using BuildingBlocks.Core.Event;
using BuildingBlocks.Core.Model;
using BuildingBlocks.CQRS;
using BuildingBlocks.Web;
using Carter;
using FluentValidation;
using Humanizer;
using MediatR;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Exceptions;

namespace ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;

public record RemoveProfileSportCommand(Guid ProfileId, Guid SportId) : ICommand;

public record ProfileSportRemovedDomainEvent(Guid ProfileId, IEnumerable<Guid> SportIds) : IDomainEvent;

public class RemoveProfileSportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("profiles/{profileId:guid}/sports/{sportId:guid}",
                async (Guid profileId, Guid sportId, ISender sender) =>
                {
                    var command = new RemoveProfileSportCommand(profileId, sportId);

                    await sender.Send(command);

                    return Results.NoContent();
                })
            .RequireAuthorization()
            .WithName(nameof(RemoveProfileSport))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(RemoveProfileSport).Humanize())
            .WithDescription(nameof(RemoveProfileSport).Humanize());
    }
}

public class RemoveProfileSportCommandValidator : AbstractValidator<RemoveProfileSportCommand>
{
    public RemoveProfileSportCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty();
        RuleFor(x => x.SportId).NotEmpty();
    }
}

internal class RemoveProfileSportCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveProfileSportCommand>
{
    public async Task<Unit> Handle(RemoveProfileSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(command.ProfileId);
        if (profile == null)
            throw new ProfileNotFoundException(command.ProfileId);

        profile.RemoveSport(command.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}