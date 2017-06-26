using System.Collections.Generic;
using ApiClient.Dtos;
using ApiClient.Enums;

namespace ApplicationTests.DtoBuilders
{
	internal class TimeAndDistanceDtoBuilder
	{
		internal TimeAndDistanceDto Object { get; private set; }

		internal TimeAndDistanceDtoBuilder Build()
		{
			var rowBuilder = new RowDtoBuilder();

			Object = new TimeAndDistanceDto
			{
				Origin_Addresses = new List<string> { "Origin_Addresses" },
				Destination_Addresses = new List<string> { "Origin_Addresses" },
				Rows = rowBuilder.Build().Object,
				Status = GoogleStatusCode.OK
			};

			return this;
		}
	}
}