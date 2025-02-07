using BuildingBlocks.Core.Model;

namespace ProfileManagement.API.Profiles.Models;

public class Sport : Entity
{
    public static Sport Create(Guid id)
    {
        var sport = new Sport
        {
            Id = id,
        };

        return sport;
    }
}