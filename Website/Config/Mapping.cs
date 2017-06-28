using System.Collections.Generic;
using System.Linq;
using ApiClient.Dtos;
using AutoMapper;
using Model;

namespace Website.Config
{
	public static class Mapping
	{
		public static void Initialize()
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<PhotoDto, Photo>()
					.ForMember(dest => dest.PhotoReference, map => map.MapFrom(src => src.Photo_Reference))
					.ForMember(dest => dest.HtmlAttribution, map => map.MapFrom(src => src.Html_Attributions.First()));

				config.CreateMap<ResultDto, Landmark>()
					.ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
					.ForMember(dest => dest.Area, map => map.MapFrom(src => src.Vicinity))
					.ForMember(dest => dest.Rating, map => map.MapFrom(src => src.Rating))
					.ForPath(dest => dest.Photos, map => map.MapFrom(src => Mapper.Map<List<Photo>>(src.Photos)));

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