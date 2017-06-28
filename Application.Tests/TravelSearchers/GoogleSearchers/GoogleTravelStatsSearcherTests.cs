using System.Linq;
using System.Threading.Tasks;
using ApiClient.Dtos;
using ApiClient.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;

namespace Application.Tests.TravelSearchers.GoogleSearchers
{
	[TestClass]
	public class GoogleTravelStatsSearcherTests
	{
		[TestMethod]
		public async Task Search_IfParametersIsNull_ReturnNull_DontCallApi()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(null);

			Assert.IsNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistanceUnit>()), Times.Never);
		}

		[TestMethod]
		public async Task Search_IfOriginIsNull_ReturnNull_DontCallApi()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper
			{
				SearchParameters =
				{
					Origin = null
				}
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNull(response);			

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistanceUnit>()), Times.Never);
		}

		[TestMethod]
		public async Task Search_IfDestinationIsNull_ReturnNull_DontCallApi()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper
			{
				SearchParameters =
				{
					Destination = null
				}
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistanceUnit>()), Times.Never);
		}

		[TestMethod]
		public async Task Search_IfUnitTypeIsNull_CallApiWithDefaultUnit()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper
			{
				SearchParameters =
				{
					UnitType = null
				}
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNotNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(wrapper.SearchParameters.Origin, wrapper.SearchParameters.Destination, It.IsAny<DistanceUnit>()), Times.Once);
		}

		[TestMethod]
		public async Task Search_IfUnitTypeIsImperial_CallApiWithImperialUnit()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper
			{
				SearchParameters =
				{
					UnitType = UnitType.Imperial
				}
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNotNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(wrapper.SearchParameters.Origin, wrapper.SearchParameters.Destination, DistanceUnit.Imperial), Times.Once);
		}

		[TestMethod]
		public async Task Search_IfUnitTypeIsMetric_CallApiWithMetricUnit()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper
			{
				SearchParameters =
				{
					UnitType = UnitType.Imperial
				}
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNotNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetTimeAndDistanceBetween(wrapper.SearchParameters.Origin, wrapper.SearchParameters.Destination, DistanceUnit.Imperial), Times.Once);
		}

		[TestMethod]
		public async Task Search_IfApiReturnsNull_ReturnNull()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			wrapper.GoogleApiRequester
				.Setup(x => x.GetTimeAndDistanceBetween(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DistanceUnit>()))
				.ReturnsAsync(null as TimeAndDistanceDto);

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNull(response);
		}

		[TestMethod]
		public async Task Search_ReturnsMappedObject()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);			

			Assert.AreEqual(wrapper.SearchApiResponse.Origin_Addresses.First(), response.Origin);
			Assert.AreEqual(wrapper.SearchApiResponse.Destination_Addresses.First(), response.Destination);
			Assert.AreEqual(wrapper.SearchApiResponse.Rows.First().Elements.First().Distance.Text, response.Distance);
			Assert.AreEqual(wrapper.SearchApiResponse.Rows.First().Elements.First().Distance.Value, response.DistanceMetres);
			Assert.AreEqual(wrapper.SearchApiResponse.Rows.First().Elements.First().Duration.Text, response.TravelDuration);
			Assert.AreEqual(wrapper.SearchApiResponse.Rows.First().Elements.First().Duration.Value, response.TravelSeconds);
		}

		[TestMethod]
		public async Task Search_IfOuterStatusIsNotOk_ReturnsNull()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper
			{
				SearchApiResponse =
				{
					Status = GoogleStatusCode.INVALID_REQUEST
				}
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNull(response);			
		}

		[TestMethod]
		public async Task Search_IfInnerStatusIsNotOk_ReturnsNull()
		{
			var wrapper = new GoogleTravelStatsSearcherTestWrapper();

			wrapper.SearchApiResponse.Rows.First().Elements.First().Status = GoogleStatusCode.REQUEST_DENIED;				

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.Search(wrapper.SearchParameters);

			Assert.IsNull(response);
		}
	}
}