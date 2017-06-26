using ApiClient.Dtos;
using ApiClient.Enums;
using ApiClient.Requests;
using Application;
using ApplicationTests.DtoBuilders;
using ApplicationTests.ModelBuilders;
using Model;
using Moq;
using Website.Config;

namespace ApplicationTests
{
	internal class GoogleTravelStatsSearcherTestWrapper
	{
		internal GoogleTravelStatsSearcherTestWrapper()
		{
			Mapping.Initialize();
						
			GoogleApiRequester = new Mock<IRequestFromGooglePlacesApi>();

			SearchParameters = new TravelStatsSearchParametersBuilder().Build().Object;

			SearchApiResponse = new TimeAndDistanceDtoBuilder().Build().Object;
		}

		internal ISearchForTravelStats GetClassUnderTest()
		{
			GoogleApiRequester.Setup(x => x.GetTimeAndDistanceBetween(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistanceUnit>()))
				.ReturnsAsync(SearchApiResponse)
				.Verifiable();

			return new GoogleTravelStatsSearcher(GoogleApiRequester.Object);
		}

		internal Mock<IRequestFromGooglePlacesApi> GoogleApiRequester { get; set; }
		internal TravelStatsSearchParameters SearchParameters { get; set; }
		internal TimeAndDistanceDto SearchApiResponse { get; set; }
	}
}