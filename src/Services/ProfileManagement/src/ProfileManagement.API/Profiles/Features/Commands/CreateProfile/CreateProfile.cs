using Ardalis.GuardClauses;
using BuildingBlocks.Core.Event;
using BuildingBlocks.Core.Model;
using BuildingBlocks.CQRS;
using FluentValidation;
using Mapster;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Dtos;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.API.Profiles.Features.Commands.CreateProfile;

public record CreateProfileCommand(Guid ProfileId, string Name, string Description) : ICommand<CreateProfileResult>;

public record CreateProfileResult(ProfileDto Profile);

public record ProfileCreatedDomainEvent(Guid ProfileId) : IDomainEvent;

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
            command.ProfileId,
            ProfileName.Create(command.Name),
            ProfileDescription.Create(command.Description));

        await profilesRepository.AddProfileAsync(profile);
        await unitOfWork.CommitChangesAsync();

        var profileDto = profile.Adapt<ProfileDto>();

        return new CreateProfileResult(profileDto);
    }
}