using System.Linq;
using ApiClient.Dtos;
using AutoMapper;
using AutoMapper.Configuration;
using Model;

namespace Website.Config
{
	public static class Mapping
	{
		public static void Initialize()
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<TimeAndDistanceDto, TravelStatsSearchResults>()
					.ForMember(dest => dest.Origin, map => map.MapFrom(src => src.Origin_Addresses.FirstOrDefault()))
					.ForMember(dest => dest.Destination, map => map.MapFrom(src => src.Destination_Addresses.FirstOrDefault()))
					.ForMember(dest => dest.Distance, map => map.MapFrom(src => src.Rows.FirstOrDefault() != null
					                                                            && src.Rows.FirstOrDefault().Elements
						                                                            .FirstOrDefault() != null
						? src.Rows.FirstOrDefault().Elements.FirstOrDefault().Distance.Text
						: null))
					.ForMember(dest => dest.DistanceMetres, map => map.MapFrom(src => src.Rows.FirstOrDefault() != null
					                                                                  && src.Rows.FirstOrDefault().Elements
						                                                                  .FirstOrDefault() != null
						? src.Rows.FirstOrDefault().Elements.FirstOrDefault().Distance.Value
						: default(int)))
					.ForMember(dest => dest.TravelDuration, map => map.MapFrom(src => src.Rows.FirstOrDefault() != null
					                                                                  && src.Rows.FirstOrDefault().Elements
						                                                                  .FirstOrDefault() != null
						? src.Rows.FirstOrDefault().Elements.FirstOrDefault().Duration.Text
						: null))
					.ForMember(dest => dest.TravelSeconds, map => map.MapFrom(src => src.Rows.FirstOrDefault() != null
					                                                                 && src.Rows.FirstOrDefault().Elements
						                                                                 .FirstOrDefault() != null
						? src.Rows.FirstOrDefault().Elements.FirstOrDefault().Duration.Value
						: default(int)))
					;

			});
		}
	}
}