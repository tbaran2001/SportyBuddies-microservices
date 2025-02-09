namespace ProfileManagement.API.Sports.ValueObjects;

public record SportId
{
    public Guid Value { get; }

    private SportId(Guid value) => Value = value;

    public static SportId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidSportIdException(value);

        return new SportId(value);
    }

    public static implicit operator Guid(SportId sportId) => sportId.Value;
}