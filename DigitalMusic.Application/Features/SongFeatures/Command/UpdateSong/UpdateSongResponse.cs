namespace DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong
{
    public sealed record UpdateSongResponse
    {
        public string Title { get; init; }
        public int Year { get; init; }
        public string Performer { get; init; }
        public string Genre { get; init; }
        public int Duration { get; init; }
        public Guid? AlbumId { get; init; }
    }
}
