using AutoMapper;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.InputModels;

namespace JustTradeIt.Software.API.Profiles
{
    public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			CreateMap<RegisterInputModel, User>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier));
		}
	}
}