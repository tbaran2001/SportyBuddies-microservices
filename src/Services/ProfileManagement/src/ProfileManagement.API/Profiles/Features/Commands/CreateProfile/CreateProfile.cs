using Ardalis.GuardClauses;

namespace ProfileManagement.API.Profiles.Features.Commands.CreateProfile;

public record CreateProfileCommand(Guid ProfileId, string Name, string Description) : ICommand<CreateProfileResult>;

public record CreateProfileResult(ProfileDto Profile);

public record ProfileCreatedDomainEvent(
    ProfileId ProfileId,
    Name Name,
    Description Description,
    BirthDate BirthDate,
    Gender Gender,
    Preferences Preferences) : IDomainEvent;

public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    public CreateProfileCommandValidator()
    {
        RuleFor(x => x.ProfileId).NotEmpty().WithMessage("ProfileId is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}

internal class CreateProfileCommandHandler(IProfilesRepository profilesRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProfileCommand, CreateProfileResult>
{
    public async Task<CreateProfileResult> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var profile = Profile.CreateSimple(
            ProfileId.Of(command.ProfileId),
            Name.Of(command.Name),
            Description.Of(command.Description));

        await profilesRepository.AddProfileAsync(profile);
        await unitOfWork.CommitChangesAsync();

        var profileDto = profile.Adapt<ProfileDto>();

        return new CreateProfileResult(profileDto);
    }
}