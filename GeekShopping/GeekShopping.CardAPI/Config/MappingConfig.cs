using AutoMapper;
using GeekShopping.CardAPI.Data.ValueObjects;
using GeekShopping.CardAPI.Model;

namespace GeekShopping.CardAPI.Config
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<ProductVO, Product>().ReverseMap();
				config.CreateMap<CardVO, Card>().ReverseMap();
				config.CreateMap<CardHeaderVO, CardHeader>().ReverseMap();
				config.CreateMap<CardDetailsVO, CardDetail>().ReverseMap();
			});

			return mappingConfig;
		}
	}
}
