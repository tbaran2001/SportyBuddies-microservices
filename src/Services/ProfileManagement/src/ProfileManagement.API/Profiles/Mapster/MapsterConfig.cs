using Mapster;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.API.Profiles.Mapster;

public class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Name, string>.NewConfig()
            .MapWith(src => src.Value);

        TypeAdapterConfig<Description, string>.NewConfig()
            .MapWith(src => src.Value);
    }
}