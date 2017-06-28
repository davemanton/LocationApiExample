using System.Collections.Generic;

namespace ApiClient.Dtos
{
	public class ResultDto
	{
		public string Formatted_Address { get; set; }
		public GeometryDto Geometry { get; set; }
		public string Icon { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public List<PhotoDto> Photos { get; set; }
		public string Place_Id { get; set; }
		public string Reference { get; set; }
		public List<string> Types { get; set; }
		public string Scope { get; set; }
		public string Vicinity { get; set; }
		public double? Rating { get; set; }
		public OpeningHoursDto Opening_Hours { get; set; }

	}
}