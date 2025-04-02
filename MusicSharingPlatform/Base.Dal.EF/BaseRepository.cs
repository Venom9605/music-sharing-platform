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
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    
    protected DbContext RepositoryDbContext;
    protected DbSet<TEntity> RepositoryDbSet;
    
    public BaseRepository(DbContext repositoryDbContext)
    {
        RepositoryDbContext = repositoryDbContext;
        RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
    }

    protected IQueryable<TEntity> GetQuery(string? userId)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        if (userId != null && typeof(IDomainUserId<string>).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((IDomainUserId<string>)e).UserId.Equals(userId));
        }

        return query;
    }
    
    public IEnumerable<TEntity> All(string? userId = null)
    {
        return GetQuery(userId)
            .ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .ToListAsync();
    }

    public TEntity? Find(TKey id, string? userId)
    {
        var query = GetQuery(userId);

        return query.FirstOrDefault(e => e.Id.Equals(id));
    }

    public async Task<TEntity?> FindAsync(TKey id, string? userId)
    {
        var query = GetQuery(userId);

        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public void Add(TEntity entity)
    {
        RepositoryDbSet.Add(entity);
    }

    public TEntity Update(TEntity entity)
    {
        return RepositoryDbSet.Update(entity).Entity;
    }

    public void Remove(TEntity entity, string? userId)
    {
        throw new NotImplementedException();
    }

    public void Remove(TKey id, string? userId)
    {
        throw new NotImplementedException();
    }

    public void RemoveAsync(TKey id, string? userId)
    {
        throw new NotImplementedException();
    }
}