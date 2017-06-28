using System.Collections.Generic;
using ApiClient.Dtos;

namespace Application.Tests.DtoBuilders
{
	internal class GooglePlacesNearbySearchDtoBuilder
	{
		internal GooglePlacesNearbySearchDto Object { get; private set; }

		internal GooglePlacesNearbySearchDtoBuilder Build()
		{
			var resultDtoBuilder = new ResultDtoBuilder().BuildNearbySearchResult();

			Object = new GooglePlacesNearbySearchDto
			{
				Html_Attributions = new List<object>(),
				Results = new List<ResultDto>
				{
					resultDtoBuilder.Object
				},
				Status = "OK"
			};

			return this;
		}
	}
}