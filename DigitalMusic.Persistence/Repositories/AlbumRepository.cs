using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using DigitalMusic.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalMusic.Persistence.Repositories
{
    public class AlbumRepository : BaseGuidRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(DataContext context) : base(context)
        {
        }
    }
}
