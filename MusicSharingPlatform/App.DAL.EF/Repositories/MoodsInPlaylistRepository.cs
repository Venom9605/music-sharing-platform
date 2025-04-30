using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using MoodsInPlaylist = App.DAL.DTO.MoodsInPlaylist;

namespace App.DAL.EF.Repositories;

public class MoodsInPlaylistRepository : BaseRepository<DTO.MoodsInPlaylist, Domain.MoodsInPlaylist>, IMoodsInPlaylistRepository
{
    private readonly MoodsInPlaylistUOWMapper _iuowMapper = new MoodsInPlaylistUOWMapper();
    public MoodsInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MoodsInPlaylistUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.MoodsInPlaylist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Playlist)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<MoodsInPlaylist?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Playlist)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);

    }
}