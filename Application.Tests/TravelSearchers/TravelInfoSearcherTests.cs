using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;

namespace Application.Tests.TravelSearchers
{
	[TestClass]
	public class TravelInfoSearcherTests
	{
		[TestMethod]
		public async Task Search_IfParametersAreNull_ReturnsNull_DoesntCallApis()
		{
			var wrapper = new TravelInfoSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(null);

			Assert.IsNull(response);

			wrapper.NearbyPlacesSearcher.Verify(x => x.SearchByLocationText(It.IsAny<string>()), Times.Never);
			wrapper.TravelStatsSearcher.Verify(x => x.Search(It.IsAny<TravelStatsSearchParameters>()), Times.Never);
		}

		[TestMethod]
		public async Task Search_CallsNearbySearcher_WithDestination()
		{
			var wrapper = new TravelInfoSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			// Prove is null before test
			Assert.IsNull(wrapper.DestinationCallback);

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.AreEqual(wrapper.SearchParameters.Destination, wrapper.DestinationCallback);			

			wrapper.NearbyPlacesSearcher.Verify(x => x.SearchByLocationText(wrapper.SearchParameters.Destination), Times.Once);
		}

		[TestMethod]
		public async Task Search_CallsStatsSearcher_WithParameters()
		{
			var wrapper = new TravelInfoSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			wrapper.TravelStatsSearcher.Verify(x => x.Search(wrapper.SearchParameters), Times.Once);
		}

		[TestMethod]
		public async Task Search_AssignsResultsOfApiCalls_ToResults()
		{
			var wrapper = new TravelInfoSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.AreEqual(wrapper.SearchParameters, response.SearchParameters);
			Assert.AreEqual(wrapper.StatsSearchResults, response.TravelStats);
			Assert.AreEqual(wrapper.NearbySearchResults, response.DestinationLandmarks);
		}
	}
}