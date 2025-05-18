using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;
using Base.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace App.Test.Unit;

public class DummyBllEntity : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
}

public class DummyDalEntity : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
}

public class BaseServiceTests
{
    private readonly BaseService<DummyBllEntity, DummyDalEntity, IRepository<DummyDalEntity, Guid>, Guid> _baseService;
    private readonly Mock<IRepository<DummyDalEntity, Guid>> _repoMock = new();
    private readonly Mock<IBLLMapper<DummyBllEntity, DummyDalEntity, Guid>> _mapperMock = new();

    public BaseServiceTests()
    {
        var uowMock = new Mock<IBaseUOW>();
        _baseService = new BaseService<DummyBllEntity, DummyDalEntity, IRepository<DummyDalEntity, Guid>, Guid>(
            uowMock.Object,
            _repoMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public void Add_CallsRepository()
    {
        var bll = new DummyBllEntity { Id = Guid.NewGuid() };
        var dal = new DummyDalEntity { Id = bll.Id };

        _mapperMock.Setup(m => m.Map(bll)).Returns(dal);

        _baseService.Add(bll);

        _repoMock.Verify(r => r.Add(dal), Times.Once);
    }

    [Fact]
    public void All_MapsAll()
    {
        var dalList = new List<DummyDalEntity> { new() { Id = Guid.NewGuid() } };
        _repoMock.Setup(r => r.All(null)).Returns(dalList);
        _mapperMock.Setup(m => m.Map(It.IsAny<DummyDalEntity>())).Returns<DummyDalEntity>(d => new DummyBllEntity { Id = d.Id });

        var result = _baseService.All().ToList();

        Assert.Single(result);
        Assert.Equal(dalList[0].Id, result[0].Id);
    }

    [Fact]
    public async Task AllAsync_MapsAll()
    {
        var dalList = new List<DummyDalEntity> { new() { Id = Guid.NewGuid() } };
        _repoMock.Setup(r => r.AllAsync(null)).ReturnsAsync(dalList);
        _mapperMock.Setup(m => m.Map(It.IsAny<DummyDalEntity>())).Returns<DummyDalEntity>(d => new DummyBllEntity { Id = d.Id });

        var result = (await _baseService.AllAsync()).ToList();

        Assert.Single(result);
        Assert.Equal(dalList[0].Id, result[0].Id);
    }

    [Fact]
    public void Find_MapsAndReturns()
    {
        var id = Guid.NewGuid();
        var dal = new DummyDalEntity { Id = id };
        var bll = new DummyBllEntity { Id = id };

        _repoMock.Setup(r => r.Find(id, null)).Returns(dal);
        _mapperMock.Setup(m => m.Map(dal)).Returns(bll);

        var result = _baseService.Find(id);

        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task FindAsync_MapsAndReturns()
    {
        var id = Guid.NewGuid();
        var dal = new DummyDalEntity { Id = id };
        var bll = new DummyBllEntity { Id = id };

        _repoMock.Setup(r => r.FindAsync(id, null)).ReturnsAsync(dal);
        _mapperMock.Setup(m => m.Map(dal)).Returns(bll);

        var result = await _baseService.FindAsync(id);

        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public void Update_MapsAndCallsRepo()
    {
        var bll = new DummyBllEntity { Id = Guid.NewGuid() };
        var dal = new DummyDalEntity { Id = bll.Id };

        _mapperMock.Setup(m => m.Map(bll)).Returns(dal);
        _repoMock.Setup(r => r.Update(dal)).Returns(dal);
        _mapperMock.Setup(m => m.Map(dal)).Returns(bll);

        var result = _baseService.Update(bll);

        Assert.Equal(bll.Id, result.Id);
    }

    [Fact]
    public void Remove_ByEntity_CallsRepo()
    {
        var id = Guid.NewGuid();
        var dal = new DummyDalEntity { Id = id };

        _repoMock.Setup(r => r.Find(id, null)).Returns(dal);

        _baseService.Remove(new DummyBllEntity { Id = id });

        _repoMock.Verify(r => r.Remove(dal, null), Times.Once);
    }

    [Fact]
    public void Remove_ById_CallsRepo()
    {
        var id = Guid.NewGuid();
        var dal = new DummyDalEntity { Id = id };

        _repoMock.Setup(r => r.Find(id, null)).Returns(dal);

        _baseService.Remove(id);

        _repoMock.Verify(r => r.Remove(dal, null), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_CallsRepo()
    {
        var id = Guid.NewGuid();
        var dal = new DummyDalEntity { Id = id };

        _repoMock.Setup(r => r.FindAsync(id, null)).ReturnsAsync(dal);

        await _baseService.RemoveAsync(id);

        _repoMock.Verify(r => r.RemoveAsync(id, null), Times.Once);
    }

    [Fact]
    public void Exists_ReturnsCorrect()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.Find(id, null)).Returns(new DummyDalEntity { Id = id });

        var exists = _baseService.Exists(id);

        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsCorrect()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.FindAsync(id, null)).ReturnsAsync(new DummyDalEntity { Id = id });

        var exists = await _baseService.ExistsAsync(id);

        Assert.True(exists);
    }
}
