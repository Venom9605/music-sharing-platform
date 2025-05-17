using System.Net.Http.Json;
using App.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Test.Integration;

[Collection("Sequential")]
public class IdentityTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public IdentityTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task CanLogin_WithSeededUser()
    {
        // Arrange
        var loginInfo = new LoginInfo
        {
            Email = "artist1@example.com",
            Password = "Test.123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1.0/Account/Login?jwtExpiresInSeconds=3600&refreshTokenExpiresInSeconds=3600", loginInfo);

        // Assert
        response.EnsureSuccessStatusCode(); // throws if not 200-299

        var jwt = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(jwt);
        Assert.False(string.IsNullOrWhiteSpace(jwt.JWT));
        Assert.False(string.IsNullOrWhiteSpace(jwt.RefreshToken));
    }
    
    [Fact]
    public async Task TokenRefresh_WithValidToken_ReturnsNewJwt()
    {
        var login = new LoginInfo
        {
            Email = "artist1@example.com",
            Password = "Test.123"
        };

        var loginResponse = await _client.PostAsJsonAsync("/api/v1.0/Account/Login?jwtExpiresInSeconds=3600&refreshTokenExpiresInSeconds=3600", login);
        loginResponse.EnsureSuccessStatusCode();

        var jwt = await loginResponse.Content.ReadFromJsonAsync<JWTResponse>();

        var refreshPayload = new TokenRefreshInfo
        {
            JWT = jwt!.JWT,
            RefreshToken = jwt.RefreshToken
        };

        var refreshResponse = await _client.PostAsJsonAsync("/api/v1.0/Account/TokenRefresh?expiresInSeconds=3600", refreshPayload);
        refreshResponse.EnsureSuccessStatusCode();

        var newJwt = await refreshResponse.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.False(string.IsNullOrWhiteSpace(newJwt!.JWT));
        Assert.False(string.IsNullOrWhiteSpace(newJwt.RefreshToken));
        Assert.NotEqual(jwt.RefreshToken, newJwt.RefreshToken);
    }
    
    [Fact]
    public async Task Register_CreatesNewUserAndReturnsJwt()
    {
        var registrationData = new RegisterInfo
        {
            Email = "newuser@example.com",
            Password = "Test.123",
            DisplayName = "New User",
            Bio = "This is a new user.",
            ProfilePicture = "New Profile Picture URL",
        };

        var response = await _client.PostAsJsonAsync("/api/v1.0/Account/Register?expiresInSeconds=3600", registrationData);

        response.EnsureSuccessStatusCode();

        var jwt = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(jwt);
        Assert.False(string.IsNullOrWhiteSpace(jwt.JWT));
        Assert.False(string.IsNullOrWhiteSpace(jwt.RefreshToken));
    }
    
    [Fact]
    public async Task CanLogin_WithNewRegisteredUser()
    {
        // Arrange
        var loginInfo = new LoginInfo
        {
            Email = "newuser@example.com",
            Password = "Test.123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1.0/Account/Login?jwtExpiresInSeconds=3600&refreshTokenExpiresInSeconds=3600", loginInfo);

        // Assert
        response.EnsureSuccessStatusCode(); // throws if not 200-299

        var jwt = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(jwt);
        Assert.False(string.IsNullOrWhiteSpace(jwt.JWT));
        Assert.False(string.IsNullOrWhiteSpace(jwt.RefreshToken));
    }
    
    [Fact]
    public async Task Logout_DeletesRefreshToken()
    {
        var login = new LoginInfo
        {
            Email = "artist1@example.com",
            Password = "Test.123"
        };

        var loginResponse = await _client.PostAsJsonAsync("/api/v1.0/Account/Login?jwtExpiresInSeconds=3600&refreshTokenExpiresInSeconds=3600", login);
        loginResponse.EnsureSuccessStatusCode();

        var jwt = await loginResponse.Content.ReadFromJsonAsync<JWTResponse>();

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/Account/Logout")
        {
            Content = JsonContent.Create(new LogoutInfo { RefreshToken = jwt!.RefreshToken })
        };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.JWT);

        var logoutResponse = await _client.SendAsync(request);
        logoutResponse.EnsureSuccessStatusCode();
    }

}