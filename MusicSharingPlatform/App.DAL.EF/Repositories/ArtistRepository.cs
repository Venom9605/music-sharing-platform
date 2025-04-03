using App.DAL.Interfaces;
using Base.Dal.EF;
using Base.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistRepository : BaseRepository<Artist, string>, IArtistRepository
{
    public ArtistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
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

    public override async Task<IEnumerable<Artist>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .ToListAsync();
    }
}