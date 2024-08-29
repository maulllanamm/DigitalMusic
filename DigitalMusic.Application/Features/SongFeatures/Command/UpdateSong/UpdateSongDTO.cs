namespace DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong
{
    public class UpdateSongDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Performer { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public Guid? AlbumId { get; set; }
    }
}
