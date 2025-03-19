using Ardalis.GuardClauses;

namespace ProfileManagement.API.Profiles.Features.Commands.UpdateProfile;

public record UpdateProfileCommand(Guid ProfileId, string Name, string Description, Gender Gender, DateTime BirthDate)
    : ICommand<UpdateProfileResult>;

public record UpdateProfileResult(Guid Id);

public record ProfileUpdatedDomainEvent(
    ProfileId Id,
    Name Name,
    Description Description,
    BirthDate BirthDate,
    Gender Gender)
    : IDomainEvent;

public record UpdateProfileRequestDto(string Name, string Description, Gender Gender, DateOnly BirthDate);

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
        RuleFor(x => x.ProfileId)
            .NotEmpty()
            .WithMessage("ProfileId is required.");
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Name is required and must not exceed 50 characters.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Description is required and must not exceed 500 characters.");
        RuleFor(x => x.Gender)
            .IsInEnum();
        RuleFor(x => x.BirthDate)
            .Must(birthDate => DateTime.Now.Year - birthDate.Year >= 18 && DateTime.Now.Year - birthDate.Year < 120)
            .WithMessage("Age must be over 18");
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

        profile.Update(Name.Of(command.Name), Description.Of(command.Description),
            BirthDate.Of(command.BirthDate), command.Gender);

        await unitOfWork.CommitChangesAsync();

        return new UpdateProfileResult(profile.Id);
    }
}