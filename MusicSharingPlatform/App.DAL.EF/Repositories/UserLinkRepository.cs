using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserLinkRepository : BaseRepository<UserLink>, IUserLinkRepository
{
    public UserLinkRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<UserLink>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(u => u.User)
            .Include(u => u.LinkType)
            .ToListAsync();
    }
}