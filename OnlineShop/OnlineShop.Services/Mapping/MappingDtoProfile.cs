using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.OrderServices;

namespace OnlineShop.Services.Mapping
{
	public class MappingDtoProfile : Profile
	{
		public MappingDtoProfile()
		{
			CreateMap<Product, ProductInfoDto>().ReverseMap();
			CreateMap<Promotion, PromotionDto>().ReverseMap();
			CreateMap<OrderItem, ShoppingCartItemDto>().ReverseMap();
		}
	}
}
