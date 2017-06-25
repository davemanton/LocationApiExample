using System.Collections.Generic;
using ApiClient.Enums;

namespace ApiClient.Dtos
{
	public class TimeAndDistanceDto
	{
		public IList<string> Destination_Addresses { get; set; }
		public IList<string> Origin_Addresses { get; set; }
		public IList<Row> Rows { get; set; }
		public GoogleStatusCode Status { get; set; }
	}
}