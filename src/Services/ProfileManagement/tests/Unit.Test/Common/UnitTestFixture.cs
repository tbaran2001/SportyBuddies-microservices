using ProfileManagement.API.Data;

namespace Unit.Test.Common
{
    [CollectionDefinition(nameof(UnitTestFixture))]
    public class FixtureCollection : ICollectionFixture<UnitTestFixture> { }

    public class UnitTestFixture : IDisposable
    {
        public ProfileDbContext DbContext { get; } = DbContextFactory.Create();

        public void Dispose()
        {
            DbContextFactory.Destroy(DbContext);
        }
    }
}
