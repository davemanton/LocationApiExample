using System.Collections.Generic;

namespace ApiClient.Dtos
{
	public class OpeningHoursDto
	{
		public bool Open_Now { get; set; }
		public List<object> Weekday_Text { get; set; }
	}
}