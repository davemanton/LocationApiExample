using System.Threading.Tasks;
using ApiClient.Requests;
using AutoMapper;
using Model;

namespace Application
{
	public interface ISearchForTravelStats
	{
		Task<TravelStatsSearchResults> Search(TravelStatsSearchParameters searchParameters);
	}

    public class GoogleTravelStatsSearcher : ISearchForTravelStats
    {
	    private readonly IRequestFromGooglePlacesApi _googlePlacesRequester;

	    public GoogleTravelStatsSearcher(IRequestFromGooglePlacesApi googlePlacesRequester)
	    {
		    _googlePlacesRequester = googlePlacesRequester;
	    }

	    public async Task<TravelStatsSearchResults> Search(TravelStatsSearchParameters searchParameters)
	    {
		    var result = await _googlePlacesRequester.GetTimeAndDistanceBetween(searchParameters.Origin, searchParameters.Destination);

		    var mapped = Mapper.Map<TravelStatsSearchResults>(result);

		    return mapped;
	    }
    }
}
