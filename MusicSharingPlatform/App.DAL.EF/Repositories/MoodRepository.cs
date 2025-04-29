using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoodRepository : BaseRepository<DTO.Mood, Domain.Mood>, IMoodRepository
{
    private readonly MoodUOWMapper _iuowMapper = new MoodUOWMapper();
    public MoodRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MoodUOWMapper())
    {
    }
}