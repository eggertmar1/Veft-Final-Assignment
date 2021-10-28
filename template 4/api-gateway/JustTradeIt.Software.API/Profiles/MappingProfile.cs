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
			CreateMap<Trade, TradeDto>()
			.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TradeStatus));
			CreateMap<Trade, TradeDetailsDto>()
			.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TradeStatus));
			CreateMap<Item, ItemDto>()
			.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));
			// CreateMap<Item, ItemDetailsDto>()
			// .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier));
			CreateMap<ItemImage, ImageDto>();
			CreateMap<ItemInputModel, Item>();
		}
	}
}