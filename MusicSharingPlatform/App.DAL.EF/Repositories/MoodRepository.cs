using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoodRepository : BaseRepository<Mood>, IMoodRepository
{
    public MoodRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}