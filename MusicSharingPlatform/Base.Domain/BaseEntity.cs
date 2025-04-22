using System.ComponentModel.DataAnnotations;

using Base.Interfaces;

namespace Base.Domain;

public abstract class BaseEntity : BaseEntity<Guid>
{
}

public abstract class BaseEntity<TKey> : IBaseEntityId<TKey>, IDomainMeta
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;

    [MaxLength(64)]
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    
    [MaxLength(64)]
    public string? ChangedBy { get; set; } = default!;
    public DateTime? ChangedAt { get; set; }
}
