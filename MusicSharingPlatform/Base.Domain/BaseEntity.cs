using Base.Interfaces;

namespace Base.Domain;

public abstract class BaseEntity : BaseEntity<Guid>
{
}

public abstract class BaseEntity<TKey> : IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}
