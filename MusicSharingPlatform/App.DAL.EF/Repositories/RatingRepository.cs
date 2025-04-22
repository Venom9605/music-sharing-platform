using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RatingRepository : BaseRepository<DTO.Rating, Domain.Rating>, IRatingRepository
{
    private readonly RatingMapper _mapper = new RatingMapper();
    public RatingRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RatingMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.Rating>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(r => r.Track)
            .Include(r => r.User)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}