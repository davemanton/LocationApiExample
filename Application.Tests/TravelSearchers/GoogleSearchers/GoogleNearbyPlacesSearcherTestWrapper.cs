using ApiClient.Dtos;
using ApiClient.Requests;
using Application.Tests.DtoBuilders;
using Application.TravelSearchers;
using Application.TravelSearchers.GoogleSearchers;
using Moq;
using Website.Config;

namespace Application.Tests.TravelSearchers.GoogleSearchers
{
	internal class GoogleNearbyPlacesSearcherTestWrapper
	{
		internal GoogleNearbyPlacesSearcherTestWrapper()
		{
			Mapping.Initialize();

			GoogleApiRequester = new Mock<IRequestFromGooglePlacesApi>();

			SearchLocation = "SearchLocation";

			TextSearchApiResponse = new GooglePlacesTextSearchDtoBuilder().Build().Object;
			NearbySearchApiResponse = new GooglePlacesNearbySearchDtoBuilder().Build().Object;
		}

		internal ISearchForNearbyPlaces GetClassUnderTest()
		{
			GoogleApiRequester.Setup(x => x.GetPlacesByTextSearch(It.IsAny<string>()))
				.ReturnsAsync(TextSearchApiResponse)
				.Verifiable();

			GoogleApiRequester.Setup(x => x.GetPlacesByNearbySearch(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()))
				.Callback((double lat, double lng, int rad) =>
				{
					LatitudeCallback = lat;
					LongitudeCallback = lng;
				})
				.ReturnsAsync(NearbySearchApiResponse)
				.Verifiable();

			return new GoogleNearbyPlacesSearcher(GoogleApiRequester.Object);
		}

		internal Mock<IRequestFromGooglePlacesApi> GoogleApiRequester { get; set; }
		internal string SearchLocation { get; set; }		
		internal GooglePlacesTextSearchDto TextSearchApiResponse { get; set; }
		internal GooglePlacesNearbySearchDto NearbySearchApiResponse { get; set; }	

		internal double LatitudeCallback { get; set; }
		internal double LongitudeCallback { get; set; }
	}
}