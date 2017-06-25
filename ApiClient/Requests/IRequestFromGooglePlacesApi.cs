using System.Threading.Tasks;
using ApiClient.Dtos;
using ApiClient.Enums;

namespace ApiClient.Requests
{
	public interface IRequestFromGooglePlacesApi
	{
		Task<TimeAndDistanceDto> GetTimeAndDistanceBetween(string origin, string destination, DistanceUnits units = DistanceUnits.Metric);
	}
}