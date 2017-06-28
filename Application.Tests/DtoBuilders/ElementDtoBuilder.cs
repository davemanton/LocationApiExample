using System.Collections.Generic;
using ApiClient.Dtos;
using ApiClient.Enums;

namespace Application.Tests.DtoBuilders
{
	internal class ElementDtoBuilder
	{
		internal IList<ElementDto> Object { get; private set; }

		internal ElementDtoBuilder Build()
		{
			Object = new List<ElementDto>
			{
				new ElementDto
				{
					Distance = new TextValueDto
					{
						Text = "DistanceText",
						Value = 10000
					},
					Duration = new TextValueDto
					{
						Text = "DurationText",
						Value = 20000
					},
					Status = GoogleStatusCode.OK
				}
			};

			return this;
		}
	}
}