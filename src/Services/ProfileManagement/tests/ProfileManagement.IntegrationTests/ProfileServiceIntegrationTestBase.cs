namespace ProfileManagement.IntegrationTests;

public class ProfileServiceIntegrationTestBase :
    IntegrationTestBase<IntegrationTestWebAppFactory, Program, ApplicationDbContext, ApplicationReadDbContext>
{
    protected ProfileServiceIntegrationTestBase(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
}