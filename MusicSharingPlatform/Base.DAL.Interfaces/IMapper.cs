using Base.Interfaces;

namespace Base.DAL.Interfaces;

public interface IMapper<TDalEntity, TDomainEntity> : IMapper<TDalEntity, TDomainEntity, Guid>
    where TDalEntity : class, IBaseEntityId<Guid>
    where TDomainEntity : class, IBaseEntityId<Guid>
{
    
}

public interface IMapper<TDalEntity, TDomainEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IBaseEntityId<TKey>
    where TDomainEntity : class, IBaseEntityId<TKey>
{
    public TDalEntity? Map(TDomainEntity? entity);
    public TDomainEntity? Map(TDalEntity? entity);
}