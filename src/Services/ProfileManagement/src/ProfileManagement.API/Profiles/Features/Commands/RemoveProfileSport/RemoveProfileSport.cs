using Ardalis.GuardClauses;
using BuildingBlocks.Core.Event;
using BuildingBlocks.Core.Model;
using BuildingBlocks.CQRS;
using BuildingBlocks.Web;
using Carter;
using FluentValidation;
using MediatR;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Exceptions;

namespace ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;

public record RemoveProfileSportCommand(Guid SportId) : ICommand;

public record ProfileSportRemovedDomainEvent(Guid ProfileId, IEnumerable<Guid> SportIds) : IDomainEvent;

public class RemoveProfileSportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("profiles/sports/{sportId:guid}", async (Guid sportId, ISender sender) =>
            {
                var command = new RemoveProfileSportCommand(sportId);

                await sender.Send(command);

                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithName("RemoveProfileSport")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Remove a sport from a profile")
            .WithDescription("Remove a sport from a profile");
    }
}

public class RemoveProfileSportCommandValidator : AbstractValidator<RemoveProfileSportCommand>
{
    public RemoveProfileSportCommandValidator()
    {
        RuleFor(x => x.SportId).NotEmpty();
    }
}

internal class RemoveProfileSportCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveProfileSportCommand>
{
    public async Task<Unit> Handle(RemoveProfileSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        profile.RemoveSport(command.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}