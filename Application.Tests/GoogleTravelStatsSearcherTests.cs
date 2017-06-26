using System.Threading.Tasks;
using ApiClient.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApplicationTests
{
	[TestClass]
	public class GoogleTravelStatsSearcherTests
	{
		[TestMethod]
		public async Task JustChecking_Works()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper();
			
			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNotNull(response);			

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistanceUnit>()), Times.Once);
		}
	}
}