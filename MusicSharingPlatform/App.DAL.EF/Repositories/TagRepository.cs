using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagRepository : BaseRepository<DTO.Tag, Domain.Tag>, ITagRepository
{
    private readonly TagUOWMapper _iuowMapper = new TagUOWMapper();
    public TagRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TagUOWMapper())
    {
    }
}