namespace ProfileManagement.Domain.Common;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}