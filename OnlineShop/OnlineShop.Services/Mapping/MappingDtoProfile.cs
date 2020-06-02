using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.Mapping
{
	public class MappingDtoProfile : Profile
	{
		public MappingDtoProfile()
		{
			CreateMap<Product, ProductInfoDto>().ReverseMap();
		}
	}
}
