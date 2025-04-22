using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserLinkRepository : BaseRepository<DTO.UserLink, Domain.UserLink>, IUserLinkRepository
{
    private readonly UserLinkMapper _mapper = new UserLinkMapper();
    public UserLinkRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserLinkMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.UserLink>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(u => u.User)
            .Include(u => u.LinkType)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}