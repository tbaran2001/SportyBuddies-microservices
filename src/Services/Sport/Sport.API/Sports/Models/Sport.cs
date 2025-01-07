using BuildingBlocks.Core.Model;
using Sport.API.Sports.Features.CreateSport;

namespace Sport.API.Sports.Models;

public class Sport : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    public static Sport Create(string name, string description)
    {
        var sport = new Sport
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };

        var domainEvent = new SportCreatedDomainEvent(sport.Id, sport.Name, sport.Description);
        sport.AddDomainEvent(domainEvent);

        return sport;
    }

    public static Sport CreateWithId(Guid id, string name, string description)
    {
        var sport = new Sport
        {
            Id = id,
            Name = name,
            Description = description
        };

        var domainEvent = new SportCreatedDomainEvent(sport.Id, sport.Name, sport.Description);
        sport.AddDomainEvent(domainEvent);

        return sport;
    }

    private Sport()
    {

    }
}