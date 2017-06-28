using System.Collections.Generic;

namespace Model
{
	public class TravelInfoSearchResult
	{
		public TravelStatsSearchParameters SearchParameters { get; set; }
		public TravelStatsSearchResults TravelStats { get; set; }
		public ICollection<Landmark> DestinationLandmarks { get; set; }
	}
}