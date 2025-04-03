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
    // TODO: remove and use UOW
    Task<int> SaveChangesAsync();
    IEnumerable<TEntity> All(string? userId = null);
    Task<IEnumerable<TEntity>> AllAsync(string? userId = null);

    TEntity? Find(TKey id, string? userId = null);
    Task<TEntity?> FindAsync(TKey id, string? userId = null);
    
    void Add(TEntity entity);
    
    TEntity Update(TEntity entity);
    
    void Remove(TEntity entity, string? userId = null);
    void Remove(TKey id, string? userId = null);
    
    Task RemoveAsync(TKey id, string? userId = null);
    
    bool Exists(TKey id, string? userId = null);
    Task<bool> ExistsAsync(TKey id, string? userId = null);
}