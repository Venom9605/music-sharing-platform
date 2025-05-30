﻿using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using MoodsInTrack = App.DAL.DTO.MoodsInTrack;

namespace App.DAL.EF.Repositories;

public class MoodsInTrackRepository : BaseRepository<DTO.MoodsInTrack, Domain.MoodsInTrack>, IMoodsInTrackRepository
{
    private readonly MoodsInTrackUOWMapper _iuowMapper = new MoodsInTrackUOWMapper();
    public MoodsInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MoodsInTrackUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.MoodsInTrack>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Track)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<MoodsInTrack?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Track)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }
}