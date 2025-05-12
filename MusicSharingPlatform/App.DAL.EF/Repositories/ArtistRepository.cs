using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Base.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistRepository : BaseRepository<DTO.Artist, Domain.Artist, string>, IArtistRepository
{
    private readonly ArtistUOWMapper _iuowMapper = new ArtistUOWMapper();
    public ArtistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ArtistUOWMapper())
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
            .Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<DTO.Artist?> FindAsync(string id, string? userId)
    {
        var query = GetQuery(userId);

        var res = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(res);
    }
    
    public async Task<DTO.Artist?> FindByNormalizedUserNameAsync(string normalizedUserName)
    {
        var res = await RepositoryDbSet
            .FirstOrDefaultAsync(a => a.NormalizedUserName == normalizedUserName);
        
        return _iuowMapper.Map(res);
    }

    public void CustomMethodTest()
    {
        Console.WriteLine("Custom test Artist method called.");
    }
}