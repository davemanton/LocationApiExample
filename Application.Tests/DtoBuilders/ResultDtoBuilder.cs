using System.Collections.Generic;
using ApiClient.Dtos;

namespace Application.Tests.DtoBuilders
{
	internal class ResultDtoBuilder
	{
		internal ResultDto Object { get; private set; }

		internal ResultDtoBuilder BuildTextSearchResult()
		{
			Object = new ResultDto
			{
				Geometry = new GeometryDto
				{
					Location = new LocationDto
					{
						Lat = 53.43432,
						Lng = -2.243
					}
				}
			};

			return this;
		}

		internal ResultDtoBuilder BuildNearbySearchResult()
		{
			Object = new ResultDto
			{
				Name = "Name",
				Vicinity = "Vicinity",
				Rating = 3.5,
				Photos	= new List<PhotoDto>
				{
					new PhotoDto
					{
						Photo_Reference = "Photo_Reference",
						Html_Attributions = new List<string>
						{
							"HtmlAttribution"
						}
					}
				}
			};

			return this;
		}
	}
}