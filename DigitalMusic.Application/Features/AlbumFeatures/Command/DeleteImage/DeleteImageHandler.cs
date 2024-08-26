using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Application.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.DeleteImage;

public class DeleteImageHandler : IRequestHandler<DeleteImageRequest, bool>
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IFileUploadHelper _fileUploadHelper;
    private IRequestHandler<DeleteImageRequest, bool> _requestHandlerImplementation;

    public DeleteImageHandler(IAlbumRepository albumRepository, IFileUploadHelper fileUploadHelper)
    {
        _albumRepository = albumRepository;
        _fileUploadHelper = fileUploadHelper;
    }


    public async Task<bool> Handle(DeleteImageRequest request, CancellationToken cancellationToken)
    {

        var album = await _albumRepository.GetById(request.Id);
        if (album is null)
        {
            throw new NotFoundException("Album not found");
        }

        if (album.cover.IsNullOrEmpty())
        {
            var errors = new string[] { "This album don't have album" };
            throw new BadRequestException(errors);
        }

        _fileUploadHelper.DeleteFile(album.cover);
        album.cover = null;
        album.modified_date = DateTime.UtcNow;
        await _albumRepository.Update(album);
        
        return true;
    }
}