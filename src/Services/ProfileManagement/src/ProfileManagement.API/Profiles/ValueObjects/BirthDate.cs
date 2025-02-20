namespace ProfileManagement.API.Profiles.ValueObjects;

public record BirthDate
{
    public DateOnly Value { get; }

    private BirthDate(DateOnly value) => Value = value;

    public static BirthDate Of(DateOnly value)
    {
        if (value == default)
            throw new InvalidBirthDateException(value);
        if (DateTime.Now.Year - value.Year < 18 || DateTime.Now.Year - value.Year >= 120)
            throw new InvalidBirthDateException(value);

        return new BirthDate(value);
    }

    public static implicit operator DateOnly(BirthDate birthDate) => birthDate.Value;
}