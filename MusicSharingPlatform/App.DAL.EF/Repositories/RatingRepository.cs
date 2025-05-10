using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using Rating = App.DAL.DTO.Rating;

namespace App.DAL.EF.Repositories;

public class RatingRepository : BaseRepository<DTO.Rating, Domain.Rating>, IRatingRepository
{
    private readonly RatingUOWMapper _iuowMapper = new RatingUOWMapper();
    public RatingRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RatingUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.Rating>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(r => r.Track)
            .Include(r => r.User)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<DTO.Rating?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(r => r.Track)
            .Include(r => r.User)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }

    public async Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId)
    {
        return await RepositoryDbSet
            .AnyAsync(r => r.TrackId == trackId && r.UserId == userId);
    }

    public async Task<IEnumerable<App.DAL.DTO.Rating>> GetAllByTrackIdAsync(Guid trackId)
    {
        var domainRatings = await RepositoryDbSet
            .Where(r => r.TrackId == trackId)
            .ToListAsync();

        return domainRatings.Select(r => IuowMapper.Map(r)!);
    }
}