using Sport.API.Sports.Features.CreateSport;

namespace Sport.API.Sports.Models;

public record Sport : Aggregate<SportId>
{
    public Name Name { get; private set; }
    public Description Description { get; private set; }

    public static Sport Create(Name name, Description description)
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

    public static Sport CreateWithId(SportId id, Name name, Description description)
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