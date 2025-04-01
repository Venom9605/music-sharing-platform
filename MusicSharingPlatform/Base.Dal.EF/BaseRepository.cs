using Base.DAL.Interfaces;
using Base.Domain;

namespace Base.Dal.EF;

public class BaseRepository<TEntity>: BaseRepository<TEntity, Guid>, IRepository<TEntity> 
    where TEntity : BaseEntity
{
}

public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    
    
    
    public IEnumerable<TEntity> All(string? userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> AllAsync(string? userId)
    {
        throw new NotImplementedException();
    }

    public TEntity Find(TKey id, string? userId)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindAsync(TKey id, string? userId)
    {
        throw new NotImplementedException();
    }

    public void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(TEntity entity, string? userId)
    {
        throw new NotImplementedException();
    }

    public void Remove(TKey id, string? userId)
    {
        throw new NotImplementedException();
    }
}