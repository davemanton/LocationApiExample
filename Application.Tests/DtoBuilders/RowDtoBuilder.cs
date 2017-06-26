using System.Collections.Generic;
using ApiClient.Dtos;

namespace ApplicationTests.DtoBuilders
{
	internal class RowDtoBuilder
	{
		internal IList<RowDto> Object { get; private set; }

		internal RowDtoBuilder Build()
		{
			var elementBuilder = new ElementDtoBuilder();

			Object = new List<RowDto>
			{
				new RowDto
				{
					Elements = elementBuilder.Build().Object					
				}
			};

			return this;
		}
	}
}