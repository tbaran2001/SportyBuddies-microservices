using Ardalis.GuardClauses;
using BuildingBlocks.Core.Event;
using BuildingBlocks.Core.Model;
using BuildingBlocks.CQRS;
using BuildingBlocks.Web;
using Carter;
using FluentValidation;
using Humanizer;
using Mapster;
using MediatR;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Enums;
using ProfileManagement.API.Profiles.Exceptions;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.API.Profiles.Features.Commands.UpdateProfile;

public record UpdateProfileCommand(Guid ProfileId, string Name, string Description, Gender Gender, DateOnly DateOfBirth)
    : ICommand<UpdateProfileResult>;

public record UpdateProfileResult(Guid Id);

public record ProfileUpdatedDomainEvent(Guid ProfileId) : IDomainEvent;

public record UpdateProfileRequestDto(string Name, string Description, Gender Gender, DateOnly DateOfBirth);

public class UpdateProfileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("profiles/{profileId:guid}",
                async (Guid profileId, UpdateProfileRequestDto request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProfileCommand>() with { ProfileId = profileId };

                    await sender.Send(command);

                    return Results.NoContent();
                })
            .RequireAuthorization()
            .WithName(nameof(UpdateProfile))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary(nameof(UpdateProfile).Humanize())
            .WithDescription(nameof(UpdateProfile).Humanize());
    }
}

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x=>x.ProfileId)
            .NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);
        RuleFor(x => x.Gender)
            .IsInEnum();
        RuleFor(x => x.DateOfBirth)
            .Must(dateOfBirth => DateTime.Now.Year - dateOfBirth.Year >= 18);
    }
}

internal class UpdateProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProfileCommand, UpdateProfileResult>
{
    public async Task<UpdateProfileResult> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var profile = await profilesRepository.GetProfileByIdAsync(command.ProfileId);
        if (profile is null)
            throw new ProfileNotFoundException(command.ProfileId);

        profile.Update(ProfileName.Create(command.Name), ProfileDescription.Create(command.Description),
            command.DateOfBirth, command.Gender);

        await unitOfWork.CommitChangesAsync();

        return new UpdateProfileResult(profile.Id);
    }
}