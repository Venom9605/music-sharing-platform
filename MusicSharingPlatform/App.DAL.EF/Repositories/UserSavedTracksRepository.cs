using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserSavedTracksRepository : BaseRepository<UserSavedTracks>, IUserSavedTracksRepository
{
    public UserSavedTracksRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<UserSavedTracks>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(u => u.User)
            .Include(u => u.Track)
            .ToListAsync();
    }
}