using System.Net.Http.Headers;
using System.Net.Http.Json;
using App.DAL.EF;
using App.DTO.Identity;
using App.DTO.v1;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Test.Integration;

[Collection("Sequential")]
public class MainFlow : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public MainFlow(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    private async Task<string> LoginAsync(string email, string password)
    {
        var login = new LoginInfo
        {
            Email = email,
            Password = password
        };

        var response = await _client.PostAsJsonAsync("/api/v1.0/Account/Login?jwtExpiresInSeconds=3600&refreshTokenExpiresInSeconds=3600", login);
        response.EnsureSuccessStatusCode();
        var jwt = await response.Content.ReadFromJsonAsync<JWTResponse>();
        return jwt!.JWT;
    }
    
    private async Task<Guid> GetArtistRoleIdByName(string name)
    {
        var response = await _client.GetAsync("/api/v1.0/ArtistRole/GetAll");
        response.EnsureSuccessStatusCode();
        var roles = await response.Content.ReadFromJsonAsync<List<ArtistRole>>();
        return roles!.First(r => r.Name == name).Id;
    }
    
    [Fact]
    public async Task LoggedInUser_CanCreate_AndFetchTrack()
    {

        var jwt = await LoginAsync("artist1@example.com", "Test.123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);


        var artistRoleId = await GetArtistRoleIdByName("Producer");


        var createDto = new TrackCreate
        {
            Title = "Test Track",
            FilePath = "uploads/fake.mp3",
            CoverPath = "covers/fake.jpg",
            ArtistRoleId = artistRoleId,
            TagsInTracks = new List<TagInTrackCreate>(),
            MoodsInTracks = new List<MoodInTrackCreate>(),
            TrackLinks = new List<TrackLinkCreate>()
        };

        var createResponse = await _client.PostAsJsonAsync("/api/v1.0/Track/CreateTrack", createDto);
        createResponse.EnsureSuccessStatusCode();

        var createdTrack = await createResponse.Content.ReadFromJsonAsync<Track>();
        Assert.NotNull(createdTrack);
        Assert.Equal("Test Track", createdTrack!.Title);
        

        
        var getResponse = await _client.GetAsync("/api/v1.0/Track/GetTracks");
        getResponse.EnsureSuccessStatusCode();

        var tracks = await getResponse.Content.ReadFromJsonAsync<List<Track>>();
        Assert.Contains(tracks!, t => t.Title == "Test Track");
    }

    
    [Fact]
    public async Task Home_Shows_ArtistOfTheMonth()
    {
        await LoggedInUser_CanCreate_AndFetchTrack();

        // Act
        var response = await _client.GetAsync("/api/v1.0/Artist/GetMostPopular");

        // Assert
        response.EnsureSuccessStatusCode();
        var artist = await response.Content.ReadFromJsonAsync<Artist>();
        Assert.NotNull(artist);
        Assert.False(string.IsNullOrWhiteSpace(artist.DisplayName));
    }
    
    // create a track and find it in discover
    
    [Fact]
    public async Task User_CanFind_TrackInDiscover()
    {
        await LoggedInUser_CanCreate_AndFetchTrack();
        

        var body = new
        {
            tagIds = Array.Empty<Guid>(),
            moodIds = Array.Empty<Guid>()
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1.0/track/GetFilteredRandomTrack", body);

        // Assert
        response.EnsureSuccessStatusCode();
        var track = await response.Content.ReadFromJsonAsync<Track>();
        Assert.Equal("Test Track", track!.Title);
    }
    
    
    
    // create track again and in discover, save the song, leave a rating and also view artist profile.  check feedback by making request to own songs, view saved songs

    [Fact]
    public async Task User_CanSaveTrack_And_CanGiveFeedback_And_ViewPublicProfile()
    {
        var jwt = await LoginAsync("artist1@example.com", "Test.123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        
        await LoggedInUser_CanCreate_AndFetchTrack();
        
        var body = new
        {
            tagIds = Array.Empty<Guid>(),
            moodIds = Array.Empty<Guid>()
        };
        
        var response = await _client.PostAsJsonAsync("/api/v1.0/track/GetFilteredRandomTrack", body);
        response.EnsureSuccessStatusCode();
        
        var track = await response.Content.ReadFromJsonAsync<Track>();
        Assert.NotNull(track);
        Assert.NotEmpty(track.ArtistInTracks!);

        
        
        var userId = track.ArtistInTracks!.First().UserId;
        
        var profileResponse = await _client.GetAsync($"/api/v1.0/Artist/GetUserInfoById/{userId}");
        profileResponse.EnsureSuccessStatusCode();
        
        var artist = await profileResponse.Content.ReadFromJsonAsync<Artist>();
        Assert.NotNull(artist);
        Assert.Equal(track.ArtistInTracks!.First().ArtistDisplayName, artist!.DisplayName);
        
        
        
        
        var saveBody = new
        {
            trackId = track.Id
        };
        
        var saveResponse = await _client.PostAsJsonAsync($"/api/v1.0/UserSavedTracks/AddSavedTrack", saveBody);
        saveResponse.EnsureSuccessStatusCode();
        
        var savedTracksResponse = await _client.GetAsync($"/api/v1.0/UserSavedTracks/GetSavedTracks");
        savedTracksResponse.EnsureSuccessStatusCode();
        
        var savedTracks = await savedTracksResponse.Content.ReadFromJsonAsync<List<Track>>();
        Assert.NotNull(savedTracks);
        
        
        
        
        var feedbackBody = new
        {
            trackId = track.Id,
            score = 5,
            comment = "Great track!"
        };
        
        var feedbackResponse = await _client.PostAsJsonAsync($"/api/v1.0/Rating/Create", feedbackBody);
        feedbackResponse.EnsureSuccessStatusCode();
        
        var userTracksResponse = await _client.GetAsync($"/api/v1.0/Track/GetTracks");
        userTracksResponse.EnsureSuccessStatusCode();
        
        var userTracks = await userTracksResponse.Content.ReadFromJsonAsync<List<Track>>();
        var ratedTrack = userTracks!.First(t => t.Id == track.Id);
        var rating = ratedTrack.Rating!.First();
        
        Assert.Equal(5, rating.Score);
        Assert.Equal("Great track!", rating.Comment);
    }
    
    [Fact]
    public async Task LoggedInUser_CanView_TheirProfile()
    {
        // Arrange
        var jwt = await LoginAsync("artist1@example.com", "Test.123");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await _client.GetAsync("/api/v1.0/Artist/GetUserInfo");

        // Assert
        response.EnsureSuccessStatusCode();
        var artist = await response.Content.ReadFromJsonAsync<Artist>();
        Assert.Equal("Artist One", artist!.DisplayName);
    }

}