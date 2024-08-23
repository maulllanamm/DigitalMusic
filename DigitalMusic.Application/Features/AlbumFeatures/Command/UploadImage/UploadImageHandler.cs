using AutoMapper;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.UploadImage
{
    public sealed class UploadImageHandler : IRequestHandler<UploadImageRequest, string>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IFileUploadHelper _fileUploadHelper;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UploadImageHandler(IMapper mapper, IAlbumRepository albumRepository, IHttpContextAccessor httpContextAccessor, IFileUploadHelper fileUploadHelper)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<string> Handle(UploadImageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var allowedFileExtentions = new[] { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileUploadHelper.SaveFileAsync(request.ImageFile, allowedFileExtentions);
                var album = await _albumRepository.GetById(request.Id);
                album.cover = createdImageName;
                album.modified_date = DateTime.UtcNow;
                var res = await _albumRepository.Update(album);
                return createdImageName;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
