using System.Collections.Generic;
using ApiClient.Dtos;

namespace Application.Tests.DtoBuilders
{
	internal class GooglePlacesTextSearchDtoBuilder
	{
		internal GooglePlacesTextSearchDto Object { get; private set; }

		internal GooglePlacesTextSearchDtoBuilder Build()
		{
			var resultDtoBuilder = new ResultDtoBuilder().BuildTextSearchResult();

			Object = new GooglePlacesTextSearchDto
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