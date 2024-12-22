using AutoMapper;
using Squash.WebAPI.Models;
using Squash.WebAPI.Models.DTOs.Url;
using Squash.WebAPI.Models.DTOs.User;

namespace Squash.WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configure User Mapping
            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();

            // Configure User Mapping
            CreateMap<Url, UrlReadDTO>();
            CreateMap<UrlCreateDTO, Url>();
            CreateMap<UrlUpdateDTO, Url>();
      
        }
    }
}
