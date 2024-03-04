using AutoMapper;

namespace GeekShopping.CardAPI.Config
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				//config.CreateMap<>();
			});

			return mappingConfig;
		}
	}
}
