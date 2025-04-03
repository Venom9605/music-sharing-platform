using App.DAL.Interfaces;
using Base.Dal.EF;
using Base.DAL.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistRoleRepository : BaseRepository<ArtistRole>, IArtistRoleRepository
{
    public ArtistRoleRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}