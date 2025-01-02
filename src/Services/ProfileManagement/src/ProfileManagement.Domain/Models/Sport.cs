using ProfileManagement.Domain.Common;

namespace ProfileManagement.Domain.Models;

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