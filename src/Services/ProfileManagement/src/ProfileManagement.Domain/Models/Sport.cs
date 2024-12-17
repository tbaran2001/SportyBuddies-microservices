using ProfileManagement.Domain.Common;

namespace ProfileManagement.Domain.Models;

public class Sport : Entity
{
    public string Name { get; private set; } = default!;

    public static Sport Create(Guid id, string name)
    {
        var sport = new Sport
        {
            Id = id,
            Name = name
        };

        return sport;
    }
}