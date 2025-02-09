namespace ProfileManagement.API.Sports.Models;

public record Sport : Aggregate<SportId>
{
    public static Sport Create(SportId id)
    {
        var sport = new Sport
        {
            Id = id,
        };

        return sport;
    }
}