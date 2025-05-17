namespace App.DAL.EF.DataSeeding;

public static class InitialData
{
    public static readonly (string email, string displayName, string password, Guid? id)[] Users =
    [
        ("artist1@example.com", "Artist One", "Test.123", null),
        ("artist2@example.com", "Artist Two", "Test.123", null)
    ];
    
    public static readonly (string Name, Guid? Id)[] ArtistRoles =
    [
        ("Main", null),
        ("Producer", null),
        ("Featured", null)
    ];

    public static readonly (string Name, Guid? Id)[] LinkTypes =
    [
        ("Spotify", null),
        ("YouTube", null),
        ("SoundCloud", null)
    ];

    public static readonly (string Name, Guid? Id)[] Moods =
    [
        ("Happy", null),
        ("Sad", null),
        ("Energetic", null)
    ];

    public static readonly (string Name, Guid? Id)[] Tags =
    [
        ("Rock", null),
        ("Electronic", null),
        ("Chill", null)
    ];
}