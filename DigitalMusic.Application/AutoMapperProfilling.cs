using AutoMapper;
using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Application.Features.AuthFeatures.RegisterFeatures;
using DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser;
using DigitalMusic.Application.Features.UserFeatures.Query.GetAll;
using DigitalMusic.Application.Features.UserFeatures.Query.GetById;
using DigitalMusic.Application.Features.UserFeatures.Query.GetByUsername;
using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application
{
    public class AutoMapperProfilling : Profile
    {
        public AutoMapperProfilling()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<UpdateUserRequestDTO, UpdateUserRequest>();
            
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserResponse>();

            CreateMap<GetAllUserRequest, User>();
            CreateMap<User, GetAllUserResponse>();            
            
            CreateMap<GetByIdUserRequest, User>();
            CreateMap<User, GetByIdUserResponse>();            
            
            CreateMap<GetByUsernameRequest, User>();
            CreateMap<User, GetByUsernameResponse>();

            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();

            CreateMap<LoginRequest, User>();
            CreateMap<User, LoginResponse>();
            
            CreateMap<CreateAlbumRequest, Album>();
            
            CreateMap<Album, GetByIdAlbumResponse>();
            
            CreateMap<Album, GetAllAlbumResponse>();
            
            CreateMap<UpdateAlbumRequest, Album>();
            CreateMap<Album, UpdateAlbumResponse>();
            
            
        }
    }
}
