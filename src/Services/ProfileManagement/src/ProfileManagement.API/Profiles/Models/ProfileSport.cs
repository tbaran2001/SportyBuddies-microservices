using BuildingBlocks.Core.Model;

namespace ProfileManagement.API.Profiles.Models;

public class ProfileSport : Entity
{
    public Guid ProfileId { get; private set; }
    public Guid SportId { get; private set; }

    internal ProfileSport(Guid profileId, Guid sportId)
    {
        ProfileId = profileId;
        SportId = sportId;
    }
}