using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Base.DAL.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistRoleRepository : BaseRepository<DTO.ArtistRole, Domain.ArtistRole>, IArtistRoleRepository
{
    private readonly ArtistRoleMapper _mapper = new ArtistRoleMapper();
    public ArtistRoleRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ArtistRoleMapper())
    {
    }
}