using BuildingBlocks.Authentication;
using MassTransit;

namespace ProfileManagement.Application.Profiles.Commands.AddSportToProfile;

public class AddSportToProfileCommandHandler(
    IProfilesRepository profilesRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddSportToProfileCommand>
{
    public async Task<Unit> Handle(AddSportToProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (profile == null)
            throw new ProfileNotFoundException(currentUser.Id);

        profile.AddSport(request.SportId);
        await unitOfWork.CommitChangesAsync();

        return Unit.Value;
    }
}