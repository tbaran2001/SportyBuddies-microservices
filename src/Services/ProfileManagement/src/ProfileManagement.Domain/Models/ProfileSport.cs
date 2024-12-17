using ProfileManagement.Domain.Common;

namespace ProfileManagement.Domain.Models;

public class ProfileSport : Entity
{
    public Guid ProfileId { get; private set; } = default!;
    public Guid SportId { get; private set; } = default!;

    internal ProfileSport(Guid profileId, Guid sportId)
    {
        ProfileId = profileId;
        SportId = sportId;
    }
}