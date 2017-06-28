using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiClient.Dtos;
using ApiClient.Requests;
using AutoMapper;
using Model;

namespace Application.TravelSearchers.GoogleSearchers
{
	public class GoogleNearbyPlacesSearcher : ISearchForNearbyPlaces
	{
		private readonly IRequestFromGooglePlacesApi _googlePlacesRequester;

		public GoogleNearbyPlacesSearcher(IRequestFromGooglePlacesApi googlePlacesRequester)
		{
			_googlePlacesRequester = googlePlacesRequester;
		}

		public async Task<ICollection<Landmark>> SearchByLocationText(string location)
		{
			if (string.IsNullOrWhiteSpace(location))
				return null;

			var searchByTextDto = await _googlePlacesRequester.GetPlacesByTextSearch(location);

			var latitude = searchByTextDto?.Results?.FirstOrDefault()?.Geometry?.Location?.Lat;
			var longitude = searchByTextDto?.Results?.FirstOrDefault()?.Geometry?.Location?.Lng;

			if (!latitude.HasValue || !longitude.HasValue)
				return null;

			var nearByPlaces = await _googlePlacesRequester.GetPlacesByNearbySearch(latitude.Value, longitude.Value);

			return Mapper.Map<List<ResultDto>, List<Landmark>>(nearByPlaces?.Results);
		}
	}
}