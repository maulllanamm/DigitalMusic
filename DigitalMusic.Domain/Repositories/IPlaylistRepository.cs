using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Repositories;

public interface IPlaylistRepository
{
    public Task<List<Playlist>> GetAll();
    public Task<Playlist> GetById(Guid id);
    public Task<Playlist> Create(Playlist playlist);
    public Task<Playlist> Update(Playlist playlist);
    public Task<bool> Delete(Guid id);
}