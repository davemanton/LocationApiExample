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

	    public async Task<TimeAndDistanceDto> GetTimeAndDistanceBetween(string origin, string destination, DistanceUnits units = DistanceUnits.Metric)
	    {
		    var uri = $"{GOOGLE_MAPS_API_URL}/distancematrix/json" +
		              $"?units={units}" +
		              $"&origins={FormatQueryParameter(origin)}" +
		              $"&destinations={FormatQueryParameter(destination)}" +
		              $"&key={_googleApiKey}";

		    return await Get<TimeAndDistanceDto>(uri);
	    }

	    private string FormatQueryParameter(string parameter)
	    {
		    return Regex.Replace(parameter, @"\s", "+");
	    }				
    }
}
