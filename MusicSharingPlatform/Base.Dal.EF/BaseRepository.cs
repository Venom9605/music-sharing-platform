using Base.DAL.Interfaces;
using Base.Domain;
using Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Dal.EF;

public class BaseRepository<TEntity>: BaseRepository<TEntity, Guid>, IRepository<TEntity> 
    where TEntity : BaseEntity
{
    public BaseRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}

public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
    protected DbContext RepositoryDbContext;
    protected DbSet<TEntity> RepositoryDbSet;
    
    public BaseRepository(DbContext repositoryDbContext)
    {
        RepositoryDbContext = repositoryDbContext;
        RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> GetQuery(string? userId)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        if (userId != null && typeof(IDomainUserId<string>).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((IDomainUserId<string>)e).UserId.Equals(userId));
        }

        return query;
    }
    
    // TODO: remove and use UOW
    public async Task<int> SaveChangesAsync()
    {
        return await RepositoryDbContext.SaveChangesAsync();
    }
    
    public virtual IEnumerable<TEntity> All(string? userId = null)
    {
        return GetQuery(userId)
            .ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .ToListAsync();
    }

    public virtual TEntity? Find(TKey id, string? userId)
    {
        var query = GetQuery(userId);

        return query.FirstOrDefault(e => e.Id.Equals(id));
    }

    public virtual async Task<TEntity?> FindAsync(TKey id, string? userId)
    {
        var query = GetQuery(userId);

        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public virtual void Add(TEntity entity)
    {
        RepositoryDbSet.Add(entity);
    }

    public virtual TEntity Update(TEntity entity)
    {
        return RepositoryDbSet.Update(entity).Entity;
    }

    public virtual void Remove(TEntity entity, string? userId)
    {
        Remove(entity.Id, userId);
    }

    public virtual void Remove(TKey id, string? userId)
    {
        var query = GetQuery(userId);
        
        var entity = query.FirstOrDefault(e => e.Id.Equals(id));
        
        if (entity != null)
        {
            RepositoryDbSet.Remove(entity);
        }
    }

    public virtual async Task RemoveAsync(TKey id, string? userId)
    {
        var query = GetQuery(userId);
        
        var entity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        
        if (entity != null)
        {
            RepositoryDbSet.Remove(entity);
        }
    }

    public virtual bool Exists(TKey id, string? userId = null)
    {
        var query = GetQuery(userId);
        
        return query.Any(e => e.Id.Equals(id));
    }

    public virtual async Task<bool> ExistsAsync(TKey id, string? userId = null)
    {
        var query = GetQuery(userId);
        return await query.AnyAsync(e => e.Id.Equals(id));
    }
}