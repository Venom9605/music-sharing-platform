using App.BLL.DTO;
using App.BLL.Services;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL.Interfaces;
using Moq;
using Xunit;

namespace App.Test.Unit;

public class TrackServiceTests
{
    private readonly TrackService _trackService;
    private readonly Mock<ITrackRepository> _trackRepoMock = new();
    private readonly Mock<IBLLMapper<App.BLL.DTO.Track, App.DAL.DTO.Track, Guid>> _mapperMock = new();

    public TrackServiceTests()
    {
        var uowMock = new Mock<IAppUOW>();
        uowMock.Setup(u => u.TrackRepository).Returns(_trackRepoMock.Object);

        _trackService = new TrackService(uowMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task IncrementPlayCountAsync_TrackExists_IncrementsAndReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var domainTrack = new Domain.Track { Id = id, TimesPlayed = 5 };
        _trackRepoMock.Setup(r => r.FindTrackedDomainAsync(id))
            .ReturnsAsync(domainTrack);

        // Act
        var result = await _trackService.IncrementPlayCountAsync(id);

        // Assert
        Assert.True(result);
        Assert.Equal(6, domainTrack.TimesPlayed);
    }

    [Fact]
    public async Task IncrementPlayCountAsync_TrackMissing_ReturnsFalse()
    {
        // Arrange
        _trackRepoMock.Setup(r => r.FindTrackedDomainAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Domain.Track?)null);

        // Act
        var result = await _trackService.IncrementPlayCountAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task GetRandomTrackFilteredAsync_ReturnsMappedTrack()
    {
        var trackDal = new App.DAL.DTO.Track { Id = Guid.NewGuid() };
        var trackBll = new App.BLL.DTO.Track { Id = trackDal.Id };

        _trackRepoMock.Setup(r => r.GetRandomTrackFilteredAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(trackDal);
        _mapperMock.Setup(m => m.Map(trackDal)).Returns(trackBll);

        var result = await _trackService.GetRandomTrackFilteredAsync([], []);

        Assert.NotNull(result);
        Assert.Equal(trackDal.Id, result!.Id);
    }

    [Fact]
    public async Task SearchTracksAsync_ReturnsMappedTrackList()
    {
        var dalList = new List<App.DAL.DTO.Track> { new() { Id = Guid.NewGuid() }, new() { Id = Guid.NewGuid() } };
        var bllList = dalList.Select(d => new App.BLL.DTO.Track { Id = d.Id }).ToList();

        _trackRepoMock.Setup(r => r.SearchTracksAsync(It.IsAny<string>())).ReturnsAsync(dalList);
        _mapperMock.Setup(m => m.Map(It.IsAny<App.DAL.DTO.Track>()))
            .Returns((App.DAL.DTO.Track d) => bllList.First(b => b.Id == d.Id));

        var result = await _trackService.SearchTracksAsync("rock");

        Assert.Equal(2, result.Count);
        Assert.All(result, r => Assert.Contains(bllList, b => b.Id == r.Id));
    }

    [Fact]
    public async Task UpdateTrackWithRelationsAsync_MapsAndCallsRepo()
    {
        var bllTrack = new App.BLL.DTO.Track { Id = Guid.NewGuid() };
        var dalTrack = new App.DAL.DTO.Track { Id = bllTrack.Id };

        _mapperMock.Setup(m => m.Map(bllTrack)).Returns(dalTrack);

        await _trackService.UpdateTrackWithRelationsAsync(bllTrack);

        _trackRepoMock.Verify(r => r.UpdateTrackWithRelationsAsync(dalTrack), Times.Once);
    }
}