using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Exceptions;

namespace ProfileManagement.Domain.ValueObjects;

public class ProfileName : ValueObject
{
    public string Value { get; }
    private ProfileName(string value) => Value = value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ProfileName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");

        return new ProfileName(name);
    }

    private ProfileName()
    {
    }
}