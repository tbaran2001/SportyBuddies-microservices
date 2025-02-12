namespace BuildingBlocks.Core.Model;

public interface IEntity
{
}

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}