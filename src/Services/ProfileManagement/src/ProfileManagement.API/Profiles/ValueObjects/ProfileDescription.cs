using BuildingBlocks.Core.Model;
using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Profiles.ValueObjects;

public class ProfileDescription : ValueObject
{
    public string Value { get; }
    private ProfileDescription(string value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents()
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