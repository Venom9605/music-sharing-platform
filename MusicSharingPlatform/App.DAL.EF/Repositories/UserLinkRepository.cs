using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserLinkRepository : BaseRepository<DTO.UserLink, Domain.UserLink>, IUserLinkRepository
{
    private readonly UserLinkUOWMapper _iuowMapper = new UserLinkUOWMapper();
    public UserLinkRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserLinkUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.UserLink>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(u => u.User)
            .Include(u => u.LinkType)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }
    
    public override async Task<DTO.UserLink?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(r => r.User)
            .Include(r => r.LinkType)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }
}