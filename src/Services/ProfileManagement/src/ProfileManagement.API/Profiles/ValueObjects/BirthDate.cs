namespace ProfileManagement.API.Profiles.ValueObjects;

public record BirthDate
{
    public DateTime Value { get; }

    private BirthDate(DateTime value) => Value = value;

    public static BirthDate Of(DateTime value)
    {
        if (value == default)
            throw new InvalidBirthDateException(value);
        if (DateTime.Now.Year - value.Year < 18 || DateTime.Now.Year - value.Year >= 120)
            throw new InvalidBirthDateException(value);

        return new BirthDate(value);
    }

    public static implicit operator DateTime(BirthDate birthDate) => birthDate.Value;
}