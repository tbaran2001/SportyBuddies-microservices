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

namespace ProfileManagement.API.Profiles.Features.Commands.AddProfileSport;

public record AddProfileSportCommand(Guid SportId) : ICommand;

public record ProfileSportAddedDomainEvent(Guid ProfileId, IEnumerable<Guid> SportIds) : IDomainEvent;

public class AddProfileSportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("profiles/sports/{sportId:guid}", async (Guid sportId, ISender sender) =>
            {
                var command = new AddProfileSportCommand(sportId);

                await sender.Send(command);

                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithName("AddProfileSport")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add a sport to a profile")
            .WithDescription("Add a sport to a profile");
    }
}

public class AddProfileSportCommandValidator : AbstractValidator<AddProfileSportCommand>
{
    public AddProfileSportCommandValidator()
    {
        RuleFor(x => x.SportId).NotEmpty();
    }
}

internal class AddProfileSportCommandHandler(
    IProfilesRepository profilesRepository,
    ICurrentUserProvider currentUserProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProfileSportCommand>
{
    public async Task<Unit> Handle(AddProfileSportCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        profile.AddSport(command.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}