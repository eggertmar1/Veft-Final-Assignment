using System;
using AutoMapper;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.InputModels;

namespace JustTradeIt.Software.API.Profiles
{
    public class MappingProfile : Profile
    {
		/// <summary> Creates the mapping profiles </summary>
		public MappingProfile()
		{
			CreateMap<RegisterInputModel, User>();
            CreateMap<User, UserDto>()

                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier));
			CreateMap<Item, ItemDetailsDto>()
				.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier));
			CreateMap<Item, ItemDto>()
				.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier));
			CreateMap<ItemImage, ImageDto>();
			CreateMap<ItemInputModel, Item>();

			CreateMap<TradeInputModel, Trade>();
			CreateMap<Trade, TradeDto>()
				.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TradeStatus));
			CreateMap<Trade, TradeDetailsDto>()
				.ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.PublicIdentifier))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TradeStatus));
		}
	}
}