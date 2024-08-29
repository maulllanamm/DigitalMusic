using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using DigitalMusic.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalMusic.Persistence.Repositories
{
    public class SongRepository : BaseGuidRepository<Song>, ISongRepository
    {
        public SongRepository(DataContext context) : base(context)
        {
        }
        
        public async Task<List<Song>> GetAll()
        {
            return await _context.Set<Song>().Where(x => x.is_deleted == false)
                .ToListAsync();
        }
    }
}
