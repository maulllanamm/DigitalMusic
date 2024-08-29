using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Features.AlbumFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.SongFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.SongFeatures.Command.CreateSong
{
    public sealed class CreateSongHandler : IRequestHandler<CreateSongRequest, Guid>
    {
        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateSongHandler(IMapper mapper, ISongRepository songRepository, IHttpContextAccessor httpContextAccessor, IAlbumRepository albumRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _httpContextAccessor = httpContextAccessor;
            _albumRepository = albumRepository;
        }

        public async Task<Guid> Handle(CreateSongRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var song = _mapper.Map<Song>(request);
                if (song.album_id != Guid.Empty)
                {
                    var album  = await _albumRepository.GetById(song.album_id.Value);
                    if (album is null)
                    {
                        throw new NotFoundException("Album id doesn't exist");
                    }
                }
                var res = await _songRepository.Create(song);
                return res.id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
