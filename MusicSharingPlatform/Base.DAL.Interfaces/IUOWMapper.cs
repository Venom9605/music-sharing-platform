using Base.Interfaces;

namespace Base.DAL.Interfaces;

public interface IUOWMapper<TDalEntity, TDomainEntity> : IUOWMapper<TDalEntity, TDomainEntity, Guid>
    where TDalEntity : class, IBaseEntityId<Guid>
    where TDomainEntity : class, IBaseEntityId<Guid>
{
    
}

public interface IUOWMapper<TDalEntity, TDomainEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IBaseEntityId<TKey>
    where TDomainEntity : class, IBaseEntityId<TKey>
{
    public TDalEntity? Map(TDomainEntity? entity);
    public TDomainEntity? Map(TDalEntity? entity);
}