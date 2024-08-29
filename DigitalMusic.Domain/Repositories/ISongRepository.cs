using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Repositories
{
    public interface ISongRepository
    {
        public Task<Song> Create(Song song);
    }
}
