namespace BuildingBlocks.Core.Model;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}