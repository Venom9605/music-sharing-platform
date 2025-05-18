namespace App.DAL.EF.DataSeeding;

public static class InitialData
{
    public const string AdminEmail = "admin@example.com";
    public const string AdminPassword = "Admin.123";
    public const string AdminRoleName = "admin";
    
    public static readonly (string email, string displayName, string password, Guid? id, bool isAdmin)[] Users =
    [
        ("artist1@example.com", "Artist One", "Test.123", null, false),
        (AdminEmail, "Admin", AdminPassword, null, true)
    ];
    
    public static readonly (string Name, Guid? Id)[] ArtistRoles =
    [
        ("Producer", null),
        ("Performer", null),
        ("Composer", null),
        ("Lyricist", null),
        ("Mixing Engineer", null),
        ("Featuring Artist", null),
        ("Remixer", null),
        ("Sound Designer", null),
        ("Vocalist", null),
        ("Other", null)
    ];

    public static readonly (string Name, Guid? Id)[] LinkTypes =
    [
        ("Spotify", null),
        ("YouTube", null),
        ("SoundCloud", null),
        ("Apple Music", null),
        ("Deezer", null),
        ("Other", null)
    ];

    public static readonly (string Name, Guid? Id)[] Moods =
    [
        ("Uplifting", null),
        ("Sad", null),
        ("Energetic", null),
        ("Relaxing", null),
        ("Melancholic", null),
        ("Angry", null),
        ("Romantic", null),
        ("Nostalgic", null),
        ("Happy", null),
        ("Chill", null),
        ("Dark", null),
        ("Hopeful", null),
        ("Intense", null),
        ("Dreamy", null),
        ("Mysterious", null),
        ("Playful", null),
        ("Epic", null),
        ("Calm", null),
        ("Bittersweet", null),
        ("Surreal", null),
        ("Distorted", null),
        ("Industrial", null),
        ("Chaotic", null),
    ];

    public static readonly (string Name, Guid? Id)[] Tags =
    [
        ("Pop", null),
        ("Rock", null),
        ("Hip-Hop", null),
        ("Electronic", null),
        ("Jazz", null),
        ("Blues", null),
        ("Classical", null),
        ("Folk", null),
        ("Reggae", null),
        ("R&B", null),
        ("Country", null),
        ("Metal", null),
        ("Punk", null),
        ("Rap", null),
        ("Soul", null),
        
        ("House", null),
        ("Techno", null),
        ("Drum and Bass", null),
        ("Trance", null),
        ("Dubstep", null),
        ("EDM", null),
        ("Future Bass", null),
        
        ("Phonk", null),
        ("Breakcore", null),
        ("Hyperpop", null),
        ("Vaporwave", null),
        ("Synthwave", null),
        ("Lofi", null),
        ("Glitchcore", null),
        ("Weirdcore", null),
        ("Dreamcore", null),
        ("Trap Metal", null),
        ("Emo Rap", null),
        
        ("K-Pop", null),
        ("J-Pop", null),
        ("Latin", null),
        ("Afrobeats", null),
        ("Bossa Nova", null),
        ("Arabic", null),
        ("Indian", null),
        
        ("Indie Rock", null),
        ("Indie Pop", null),
        ("Shoegaze", null),
        ("Post-Rock", null),
        ("Math Rock", null),
        ("Grunge", null),
        ("Garage Rock", null),
        
        ("Hardcore", null),
        ("Death Metal", null),
        ("Black Metal", null),
        ("Screamo", null),
        
        ("Noise", null),
        ("Ambient", null),
        ("Field Recordings", null)
    ];
}