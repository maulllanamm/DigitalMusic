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
    }
}
