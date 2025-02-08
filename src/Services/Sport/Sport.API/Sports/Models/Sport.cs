using BuildingBlocks.Core.Model;
using Sport.API.Sports.Features.CreateSport;
using Sport.API.Sports.ValueObjects;

namespace Sport.API.Sports.Models;

public record Sport : Aggregate<SportId>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    public static Sport Create(string name, string description)
    {
        var sport = new Sport
        {
            Id = SportId.Of(Guid.NewGuid()),
            Name = name,
            Description = description
        };

        var domainEvent = new SportCreatedDomainEvent(sport.Id, sport.Name, sport.Description);
        sport.AddDomainEvent(domainEvent);

        return sport;
    }

    public static Sport CreateWithId(SportId id, string name, string description)
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