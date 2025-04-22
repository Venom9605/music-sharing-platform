using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class LinkTypeRepository : BaseRepository<DTO.LinkType, Domain.LinkType>, ILinkTypeRepository
{
    private readonly LinkTypeMapper _mapper = new LinkTypeMapper();
    public LinkTypeRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new LinkTypeMapper())
    {
    }
}