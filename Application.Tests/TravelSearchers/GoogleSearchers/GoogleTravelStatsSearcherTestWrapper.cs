using ApiClient.Dtos;
using ApiClient.Enums;
using ApiClient.Requests;
using Application.Tests.DtoBuilders;
using Application.Tests.ModelBuilders;
using Application.TravelSearchers;
using Application.TravelSearchers.GoogleSearchers;
using Model;
using Moq;
using Website.Config;

namespace Application.Tests.TravelSearchers.GoogleSearchers
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