using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Profiles.ValueObjects;

public class ProfileDescription
{
    public string Value { get; }
    private ProfileDescription(string value) => Value = value;

    public static ProfileDescription Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description cannot be empty");

        return new ProfileDescription(description);
    }

}