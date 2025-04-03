using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class LinkTypeRepository : BaseRepository<LinkType>, ILinkTypeRepository
{
    public LinkTypeRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}