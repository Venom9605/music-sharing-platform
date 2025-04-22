using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserSavedTracksRepository : BaseRepository<DTO.UserSavedTracks, Domain.UserSavedTracks>, IUserSavedTracksRepository
{
    private readonly UserSavedTracksMapper _mapper = new UserSavedTracksMapper();
    public UserSavedTracksRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserSavedTracksMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.UserSavedTracks>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(u => u.User)
            .Include(u => u.Track)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}