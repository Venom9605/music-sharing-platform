using Base.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.Dal.EF;

public class BaseUOW<TDbContext> : IBaseUOW
    where TDbContext : DbContext
{
    protected readonly TDbContext UOWDbContext;

    public BaseUOW(TDbContext uowDbContext)
    {
        UOWDbContext = uowDbContext;
    }


    public async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }
}