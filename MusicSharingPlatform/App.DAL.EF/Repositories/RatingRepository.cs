using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RatingRepository : BaseRepository<Rating>, IRatingRepository
{
    public RatingRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<Rating>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(r => r.Track)
            .Include(r => r.User)
            .ToListAsync();
    }
}