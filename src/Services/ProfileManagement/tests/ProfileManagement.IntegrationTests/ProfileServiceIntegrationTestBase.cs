namespace ProfileManagement.IntegrationTests;

public class ProfileServiceIntegrationTestBase :
    IntegrationTestBase<IntegrationTestWebAppFactory, Program, ApplicationDbContext>
{
    protected ProfileServiceIntegrationTestBase(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
}