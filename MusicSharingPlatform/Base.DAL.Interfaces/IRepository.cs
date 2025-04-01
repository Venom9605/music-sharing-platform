using Base.Domain;

namespace Base.DAL.Interfaces;

public interface IRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : BaseEntity
{
}

public interface IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    IEnumerable<TEntity> All(string? userId);
    Task<IEnumerable<TEntity>> AllAsync(string? userId);

    TEntity Find(TKey id, string? userId);
    Task<TEntity> FindAsync(TKey id, string? userId);
    
    void Add(TEntity entity);
    
    TEntity Update(TEntity entity);
    
    void Remove(TEntity entity, string? userId);

    void Remove(TKey id, string? userId);
}