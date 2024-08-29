using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong
{
    public sealed class UpdateSongHandler : IRequestHandler<UpdateSongRequest, UpdateSongResponse>
    {
        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        public UpdateSongHandler(IMapper mapper, ISongRepository songRepository, IAlbumRepository albumRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _albumRepository = albumRepository;
        }

        public async Task<UpdateSongResponse> Handle(UpdateSongRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newSong = _mapper.Map<Song>(request);
                var song = await _songRepository.GetById(request.Id);
                song.title = newSong.title;
                song.year = newSong.year;
                song.performer = newSong.performer;
                song.genre = newSong.genre;
                song.duration = newSong.duration;
                
                if (song.album_id != Guid.Empty)
                {
                    var album  = await _albumRepository.GetById(song.album_id.Value);
                    if (album is null)
                    {
                        throw new NotFoundException("Album id doesn't exist");
                    }
                }
                
                song.album_id = newSong.album_id;
                
                var res = await _songRepository.Update(song);
                
                return _mapper.Map<UpdateSongResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
