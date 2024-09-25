using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using DigitalMusic.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalMusic.Persistence.Repositories
{
    public class PlaylistRepository : BaseGuidRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(DataContext context) : base(context)
        {
        }
    }
}
