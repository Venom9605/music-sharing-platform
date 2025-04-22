using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Base.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistRepository : BaseRepository<DTO.Artist, Domain.Artist, string>, IArtistRepository
{
    private readonly ArtistMapper _mapper = new ArtistMapper();
    public ArtistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ArtistMapper())
    {
    }

    protected override IQueryable<Artist> GetQuery(string? userId)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(userId))
        {
            query = query.Where(e => e.Id == userId);
        }

        return query;
    }

    public override async Task<IEnumerable<DTO.Artist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }

    public override async Task<DTO.Artist?> FindAsync(string id, string? userId)
    {
        var query = GetQuery(userId);

        var res = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _mapper.Map(res);
    }
}