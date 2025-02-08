using BuildingBlocks.Core.Event;

namespace BuildingBlocks.Core.Model;

public abstract record Entity<T> : IEntity<T>
{
    public T Id { get; set; }
}