using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Application.Tests.ModelBuilders;
using Application.TravelSearchers;
using Model;
using Moq;

namespace Application.Tests.TravelSearchers
{
	internal class TravelInfoSearcherTestWrapper
	{
		internal TravelInfoSearcherTestWrapper()
		{
			TravelStatsSearcher = new Mock<ISearchForTravelStats>();
			NearbyPlacesSearcher = new Mock<ISearchForNearbyPlaces>();

			SearchParameters = new TravelStatsSearchParametersBuilder().Build().Object;
			StatsSearchResults = new TravelStatsSearchResultBuilder().Build().Object;
			NearbySearchResults = new LandmarksBuilder().Build(3).Object;
		}

		internal ISearchForTravelInfo GetClassUnderTest()
		{
			TravelStatsSearcher.Setup(x => x.Search(It.IsAny<TravelStatsSearchParameters>()))
				.ReturnsAsync(StatsSearchResults)
				.Verifiable();

			NearbyPlacesSearcher.Setup(x => x.SearchByLocationText(It.IsAny<string>()))
				.Callback((string search) =>
				{
					DestinationCallback = search;
				})
				.ReturnsAsync(NearbySearchResults)
				.Verifiable();

			return new TravelInfoSearcher(TravelStatsSearcher.Object, NearbyPlacesSearcher.Object);
		}

		internal Mock<ISearchForTravelStats> TravelStatsSearcher { get; set; }
		internal Mock<ISearchForNearbyPlaces> NearbyPlacesSearcher { get; set; }

		internal TravelStatsSearchParameters SearchParameters { get; set; }
		internal TravelStatsSearchResults StatsSearchResults { get; set; }
		internal ICollection<Landmark> NearbySearchResults { get; set; }

		internal string DestinationCallback { get; set; }
	}
}