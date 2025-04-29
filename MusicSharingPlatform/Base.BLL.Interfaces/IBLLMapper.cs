using Base.Interfaces;

namespace Base.BLL.Interfaces;

public interface IBLLMapper<TBLLEntity, TDalEntity> : IBLLMapper<TBLLEntity, TDalEntity, Guid>
    where TDalEntity : class, IBaseEntityId<Guid>
    where TBLLEntity : class, IBaseEntityId<Guid>
{
    
}

public interface IBLLMapper<TBLLEntity, TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IBaseEntityId<TKey>
    where TBLLEntity : class, IBaseEntityId<TKey>
{
    public TDalEntity? Map(TBLLEntity? entity);
    public TBLLEntity? Map(TDalEntity? entity);
}