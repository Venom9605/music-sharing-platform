namespace Base.Interfaces;

public interface IDomainUserId : IDomainUserId<string>
{
}

public interface IDomainUserId<TKey> 
    where TKey : IEquatable<TKey>
{
    TKey UserId { get; set; }
}