namespace ProfileManagement.Application.Common;

public class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<ProfileName, string>.NewConfig()
            .MapWith(src => src.Value);

        TypeAdapterConfig<ProfileDescription, string>.NewConfig()
            .MapWith(src => src.Value);
    }
}