using System.Collections.Generic;

namespace ApiClient.Dtos
{
	public class GooglePlacesTextSearchDto
	{
		public List<object> Html_Attributions { get; set; }
		public List<ResultDto> Results { get; set; }
		public string Status { get; set; }
	}
}