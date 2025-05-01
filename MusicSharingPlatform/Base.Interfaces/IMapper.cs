namespace Base.Interfaces;

public interface IMapper<TAppEntity, TBLLEntity> : IMapper<TAppEntity, TBLLEntity, Guid>
    where TAppEntity : class, IBaseEntityId<Guid>
    where TBLLEntity : class, IBaseEntityId<Guid>
{
}

public interface IMapper<TAppEntity, TBLLEntity, TKey>
    where TKey : IEquatable<TKey>
    where TAppEntity : class, IBaseEntityId<TKey>
    where TBLLEntity : class, IBaseEntityId<TKey>
{
    public TAppEntity? Map(TBLLEntity? entity);
    public TBLLEntity? Map(TAppEntity? entity);
}