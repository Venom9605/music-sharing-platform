using Base.DAL.Interfaces;
using Base.Domain;
using Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Dal.EF;

public class BaseRepository<TDalEntity, TDomainEntity>: BaseRepository<TDalEntity, TDomainEntity, Guid>, IBaseRepository<TDalEntity> 
    where TDalEntity : class, IBaseEntityId<Guid>
    where TDomainEntity : BaseEntity
{
    public BaseRepository(DbContext repositoryDbContext, IUOWMapper<TDalEntity, TDomainEntity> iuowMapper) 
        : base(repositoryDbContext, iuowMapper)
    {
    }
}

public class BaseRepository<TDalEntity, TDomainEntity, TKey> : IRepository<TDalEntity, TKey>
    where TDalEntity : class, IBaseEntityId<TKey>
    where TDomainEntity : class, IBaseEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
    protected DbContext RepositoryDbContext;
    protected DbSet<TDomainEntity> RepositoryDbSet;
    protected IUOWMapper<TDalEntity, TDomainEntity, TKey> IuowMapper; 
    
    public BaseRepository(DbContext repositoryDbContext, IUOWMapper<TDalEntity, TDomainEntity, TKey> iuowMapper)
    {
        RepositoryDbContext = repositoryDbContext;
        IuowMapper = iuowMapper;
        RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
    }

    protected virtual IQueryable<TDomainEntity> GetQuery(string? userId)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        if (userId != null && typeof(IDomainUserId<string>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            query = query.Where(e => ((IDomainUserId<string>)e).UserId.Equals(userId));
        }

        return query;
    }
    
    
    public virtual IEnumerable<TDalEntity> All(string? userId = null)
    {
        return GetQuery(userId)
            .ToList()
            .Select(e => IuowMapper.Map(e)!);
    }

    public virtual async Task<IEnumerable<TDalEntity>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .ToListAsync())
            .Select(e => IuowMapper.Map(e)!);
    }

    public virtual TDalEntity? Find(TKey id, string? userId)
    {
        var query = GetQuery(userId);

        var res = query.FirstOrDefault(e => e.Id.Equals(id));

        return IuowMapper.Map(res);
    }

    public virtual async Task<TDalEntity?> FindAsync(TKey id, string? userId)
    {
        var query = GetQuery(userId);

        var res =  await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        
        return IuowMapper.Map(res);
    }

    public virtual void Add(TDalEntity entity)
    {
        RepositoryDbSet.Add(IuowMapper.Map(entity)!);
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return IuowMapper.Map(RepositoryDbSet.Update(IuowMapper.Map(entity)!).Entity)!;
    }

    public virtual void Remove(TDalEntity entity, string? userId)
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