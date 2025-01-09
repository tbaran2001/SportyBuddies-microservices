using BuildingBlocks.Core.Model;
using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Profiles.ValueObjects;

public class ProfileName
{
    public string Value { get; }
    private ProfileName(string value) => Value = value;

    public static ProfileName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");

        return new ProfileName(name);
    }
}