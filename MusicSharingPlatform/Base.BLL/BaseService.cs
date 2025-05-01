using Base.BLL.Interfaces;
using Base.DAL.Interfaces;
using Base.Interfaces;

namespace Base.BLL;

public class BaseService<TBllEntity, TDalEntity, TDalRepository> : BaseService<TBllEntity, TDalEntity, TDalRepository, Guid>, IBaseService<TBllEntity>
    where TBllEntity : class, IBaseEntityId<Guid>
    where TDalEntity : class, IBaseEntityId<Guid>
    where TDalRepository : class, IBaseRepository<TDalEntity>
{
    public BaseService(IBaseUOW serviceUOW, TDalRepository serviceRepository, IBLLMapper<TBllEntity, TDalEntity, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}

public class BaseService<TBllEntity, TDalEntity, TDalRepository, TKey> : IBaseService<TBllEntity, TKey>
    where TBllEntity : class, IBaseEntityId<TKey>
    where TDalEntity : class, IBaseEntityId<TKey>
    where TDalRepository : class, IRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
{
    
    protected IBaseUOW ServiceUOW;
    protected TDalRepository ServiceRepository;
    protected IBLLMapper<TBllEntity, TDalEntity, TKey> BLLMapper;


    public BaseService(IBaseUOW serviceUOW, TDalRepository serviceRepository, IBLLMapper<TBllEntity, TDalEntity, TKey> bllMapper)
    {
        ServiceUOW = serviceUOW;
        ServiceRepository = serviceRepository;
        BLLMapper = bllMapper;
    }

    public virtual IEnumerable<TBllEntity> All(string? userId = null)
    {
        var entities = ServiceRepository.All(userId);
        return entities.Select(e => BLLMapper.Map(e)!).ToList();
    }

    public virtual async Task<IEnumerable<TBllEntity>> AllAsync(string? userId = null)
    {
        var entities = await ServiceRepository.AllAsync(userId);
        return entities.Select(e => BLLMapper.Map(e)!).ToList();
    }

    public virtual TBllEntity? Find(TKey id, string? userId = null)
    {
        var entity = ServiceRepository.Find(id, userId);
        return BLLMapper.Map(entity);
    }

    public virtual async Task<TBllEntity?> FindAsync(TKey id, string? userId = null)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        return BLLMapper.Map(entity);
    }

    public virtual void Add(TBllEntity entity)
    {
        var dalEntity = BLLMapper.Map(entity);
        ServiceRepository.Add(dalEntity!);
    }

    // TODO: add user id check to update
    public virtual TBllEntity Update(TBllEntity entity)
    {
        var dalEntity = BLLMapper.Map(entity);
        var updatedEntity = ServiceRepository.Update(dalEntity!);
        return BLLMapper.Map(updatedEntity)!;
    }

    public virtual void Remove(TBllEntity entity, string? userId = null)
    {
        Remove(entity.Id, userId);
    }

    public virtual void Remove(TKey id, string? userId = null)
    {
        var entity = ServiceRepository.Find(id, userId);
        
        if (entity != null)
        {
            ServiceRepository.Remove(entity, userId);
        }
    }

    public virtual async Task RemoveAsync(TKey id, string? userId = null)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        
        if (entity != null)
        {
            await ServiceRepository.RemoveAsync(id, userId);
        }

    }

    public virtual bool Exists(TKey id, string? userId = null)
    {
        var entity = ServiceRepository.Find(id, userId);
        return entity != null;
        
    }

    public virtual async Task<bool> ExistsAsync(TKey id, string? userId = null)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        return entity != null;
    }
}