using System.Threading.Tasks;
using ApiClient.Dtos;
using ApiClient.Enums;

namespace ApiClient.Requests
{
	public interface IRequestFromGooglePlacesApi
	{
		Task<TimeAndDistanceDto> GetTimeAndDistanceBetween(string origin, string destination, DistanceUnit unit = DistanceUnit.Metric);
		Task<GooglePlacesTextSearchDto> GetPlacesByTextSearch(string queryString);
		Task<GooglePlacesNearbySearchDto> GetPlacesByNearbySearch(double latitude, double longitude, int radius = 1000);
	}
}