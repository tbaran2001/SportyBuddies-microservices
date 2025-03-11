namespace ProfileManagement.API.Profiles.Features;

public class ProfileMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProfileReadModel, ProfileDto>()
            .Map(dest => dest.Id, src => src.ProfileId);
    }
}