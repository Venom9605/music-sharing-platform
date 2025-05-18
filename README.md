Marc Täll
232696IADB

# MusicApp


### TODO:

Styling

documentation
host
react crud 

## OPTIONAL TODO:
Fix files duplicating
Average rating
Too many ratings wont fit on page
Localization


~~~sh

dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext InitialCreate

dotnet ef database --project App.DAL.EF --startup-project WebApp update

dotnet ef database --project App.DAL.EF --startup-project WebApp drop


Artist Discriminator:

dotnet ef migrations add SetArtistDiscriminator --project App.DAL.EF --startup-project WebApp
dotnet ef database update --project App.DAL.EF --startup-project WebApp



~~~

~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name TrackController -actions -m Domain.Track -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ArtistInTrackController -actions -m Domain.ArtistInTrack -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ArtistRoleController -actions -m Domain.ArtistRole -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


dotnet aspnet-codegenerator controller -name LinkTypeController -actions -m Domain.LinkType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name MoodController -actions -m Domain.Mood -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name MoodsInPlaylistController -actions -m Domain.MoodsInPlaylist -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name MoodsInTrackController -actions -m Domain.MoodsInTrack -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name PlaylistController -actions -m Domain.Playlist -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name RatingController -actions -m Domain.Rating -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TagController -actions -m Domain.Tag -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TagsInPlaylistController -actions -m Domain.TagsInPlaylist -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TagsInTrackController -actions -m Domain.TagsInTrack -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TrackInPlaylistController -actions -m Domain.TrackInPlaylist -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TrackLinkController -actions -m Domain.TrackLink -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UserLinkController -actions -m Domain.UserLink -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UserSavedTrackController -actions -m Domain.UserSavedTracks -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f




~~~