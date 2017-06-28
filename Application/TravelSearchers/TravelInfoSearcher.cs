using System.Threading.Tasks;
using Model;

namespace Application.TravelSearchers
{
	public class TravelInfoSearcher : ISearchForTravelInfo
	{
		private readonly ISearchForTravelStats _travelStatsSearcher;
		private readonly ISearchForNearbyPlaces _nearbyPlacesSearcher;

		public TravelInfoSearcher(
			ISearchForTravelStats travelStatsSearcher, 
			ISearchForNearbyPlaces nearbyPlacesSearcher)
		{
			_travelStatsSearcher = travelStatsSearcher;
			_nearbyPlacesSearcher = nearbyPlacesSearcher;
		}

		public async Task<TravelInfoSearchResult> Search(TravelStatsSearchParameters searchParameters)
		{
			if (searchParameters == null)
				return null;

			var nearbyPlacesSearchTask = _nearbyPlacesSearcher.SearchByLocationText(searchParameters.Destination);

			var travelStatsSearchTask = _travelStatsSearcher.Search(searchParameters);

			var travelInfo = new TravelInfoSearchResult
			{
				SearchParameters = searchParameters,
			};

			travelInfo.TravelStats = await travelStatsSearchTask;

			travelInfo.DestinationLandmarks = await nearbyPlacesSearchTask;

			return travelInfo;
		}
	}
}