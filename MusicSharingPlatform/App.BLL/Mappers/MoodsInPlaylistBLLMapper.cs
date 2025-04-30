using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class MoodsInPlaylistBLLMapper : IBLLMapper<App.BLL.DTO.MoodsInPlaylist, App.DAL.DTO.MoodsInPlaylist>
{
    public MoodsInPlaylist? Map(DTO.MoodsInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new MoodsInPlaylist()
        {
            Id = entity.Id,
            
            MoodId = entity.MoodId,
            Mood = entity.Mood != null ? new Mood
            {
                Id = entity.Mood.Id,
                Name = entity.Mood.Name
            } : null,
            
            PlaylistId = entity.PlaylistId,
            Playlist = entity.Playlist != null ? new Playlist
            {
                Id = entity.Playlist.Id,
                Name = entity.Playlist.Name
            } : null,
        };
        return res;
    }

    public DTO.MoodsInPlaylist? Map(MoodsInPlaylist? entity)
    {
        if (entity == null) return null;
        
        Console.WriteLine("Mapping from DAL DTO to BLL DTO: " + entity.Id);
        
        var res = new DTO.MoodsInPlaylist()
        {
            Id = entity.Id,
            
            MoodId = entity.MoodId,
            Mood = entity.Mood != null ? new DTO.Mood
            {
                Id = entity.Mood.Id,
                Name = entity.Mood.Name
            } : null,
            
            PlaylistId = entity.PlaylistId,
            Playlist = entity.Playlist != null ? new DTO.Playlist
            {
                Id = entity.Playlist.Id,
                Name = entity.Playlist.Name
            } : null,
        };
        
        Console.WriteLine(res.Mood?.Name);
        Console.WriteLine(res.Playlist?.Name);

        
        
        return res;
    }
}