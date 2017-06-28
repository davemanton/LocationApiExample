using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiClient.Dtos;
using ApiClient.Enums;
using ApiClient.Settings;
using Microsoft.Extensions.Options;

namespace ApiClient.Requests
{
	public class GooglePlacesApiRequester : ApiRequesterBase, IRequestFromGooglePlacesApi
    {
	    private const string GOOGLE_MAPS_API_URL = "https://maps.googleapis.com/maps/api";

		private readonly string _googleApiKey;

	    public GooglePlacesApiRequester(IOptions<GoogleAppSettings> googleAppSettings)
	    {
			IRetrieveGoogleAppSettings appSettings = googleAppSettings.Value;

		    _googleApiKey = appSettings.GoogleApiKey;
	    }

	    public async Task<TimeAndDistanceDto> GetTimeAndDistanceBetween(string origin, string destination, DistanceUnit unit = DistanceUnit.Metric)
	    {
		    var uri = $"{GOOGLE_MAPS_API_URL}/distancematrix/json" +
		              $"?unit={unit}" +
		              $"&origins={FormatQueryParameter(origin)}" +
		              $"&destinations={FormatQueryParameter(destination)}" +
		              $"&key={_googleApiKey}";

		    return await Get<TimeAndDistanceDto>(uri);
	    }

	    public async Task<GooglePlacesTextSearchDto> GetPlacesByTextSearch(string queryString)
	    {
		    var uri = $"{GOOGLE_MAPS_API_URL}/place/textsearch/json" +
		              $"?query={queryString}" +
		              $"&key={_googleApiKey}";

		    return await Get<GooglePlacesTextSearchDto>(uri);
	    }

	    public async Task<GooglePlacesNearbySearchDto> GetPlacesByNearbySearch(double latitude, double longitude, int radius = 1000)
	    {
		    var uri = $"{GOOGLE_MAPS_API_URL}/place/nearbysearch/json" +
		              $"?location={latitude},{longitude}" +
		              $"&radius={radius}" +
		              $"&key={_googleApiKey}";

			return await Get<GooglePlacesNearbySearchDto>(uri);
	    }		

		private string FormatQueryParameter(string parameter)
	    {
		    return Regex.Replace(parameter, @"\s", "+");
	    }				
    }
}
