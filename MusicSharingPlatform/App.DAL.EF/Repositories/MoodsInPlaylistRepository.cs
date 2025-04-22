using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoodsInPlaylistRepository : BaseRepository<DTO.MoodsInPlaylist, Domain.MoodsInPlaylist>, IMoodsInPlaylistRepository
{
    private readonly MoodsInPlaylistMapper _mapper = new MoodsInPlaylistMapper();
    public MoodsInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MoodsInPlaylistMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.MoodsInPlaylist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Playlist)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}