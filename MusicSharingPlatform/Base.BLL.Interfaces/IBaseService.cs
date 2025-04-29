using Base.DAL.Interfaces;
using Base.Interfaces;

namespace Base.BLL.Interfaces;

public interface IBaseService<TEntity> : IBaseService<TEntity, Guid>, IBaseRepository<TEntity>
    where TEntity : IBaseEntityId<Guid>
{
}

public interface IBaseService<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
}