namespace ProfileManagement.IntegrationTests;

public class ProfileServiceIntegrationTestBase :
    IntegrationTestBase<ProfileServiceIntegrationTestWebAppFactory, Program, ApplicationDbContext,
        ApplicationReadDbContext>
{
    protected ProfileServiceIntegrationTestBase(ProfileServiceIntegrationTestWebAppFactory factory) : base(factory)
    {
    }
}