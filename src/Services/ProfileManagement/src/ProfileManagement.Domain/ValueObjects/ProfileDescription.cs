using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Exceptions;

namespace ProfileManagement.Domain.ValueObjects;

public class ProfileDescription : ValueObject
{
    public string Value { get; }
    private ProfileDescription(string value) => Value = value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ProfileDescription Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description cannot be empty");

        return new ProfileDescription(description);
    }

    private ProfileDescription()
    {
    }
}