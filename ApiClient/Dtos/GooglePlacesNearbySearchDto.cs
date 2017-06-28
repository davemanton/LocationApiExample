using System.Collections.Generic;

namespace ApiClient.Dtos
{
	public class GooglePlacesNearbySearchDto
	{
		public List<object> Html_Attributions { get; set; }
		public string Next_Page_Token { get; set; }
		public List<ResultDto> Results { get; set; }
		public string Status { get; set; }
	}
}