using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Repositories
{
    public interface ISongRepository
    {
        public Task<List<Song>> GetAll();
        public Task<Song> GetById(Guid id);
        public Task<Song> Create(Song song);
        public Task<Song> Update(Song song);
        public Task<bool> Delete(Guid id);
    }
}
