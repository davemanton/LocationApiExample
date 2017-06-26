using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using ApiClient.Enums;
using ApiClient.Requests;
using AutoMapper;
using Model;

namespace Application
{
	public class GoogleTravelStatsSearcher : ISearchForTravelStats
    {
	    private readonly IRequestFromGooglePlacesApi _googlePlacesRequester;

	    public GoogleTravelStatsSearcher(IRequestFromGooglePlacesApi googlePlacesRequester)
	    {
		    _googlePlacesRequester = googlePlacesRequester;
	    }

	    public async Task<TravelStatsSearchResults> Search(TravelStatsSearchParameters searchParameters)
	    {
		    if (searchParameters?.Origin == null || searchParameters.Destination == null)
			    return null;

		    var searchResult = await _googlePlacesRequester.GetTimeAndDistanceBetween(searchParameters.Origin,
			    searchParameters.Destination, ConvertUnitType(searchParameters.UnitType));

		    var failedInnerStatus = searchResult?.Rows?.SelectMany(x => x.Elements)?.Any(x => x.Status != GoogleStatusCode.OK);

			if (searchResult?.Status != GoogleStatusCode.OK
				|| failedInnerStatus == null 
				|| failedInnerStatus.Value)
			    return null;

			var mappedResult = Mapper.Map<TravelStatsSearchResults>(searchResult);

		    return mappedResult;
	    }

	    private DistanceUnit ConvertUnitType(UnitType? unitType)
	    {
			return unitType == null || unitType == UnitType.Metric
				? DistanceUnit.Metric 
				: DistanceUnit.Imperial;
	    }
    }
}
