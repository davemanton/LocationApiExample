using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;

namespace Application.Tests.TravelSearchers.GoogleSearchers
{
	[TestClass]
	public class GoogleNearbyPlacesSearcherTests
	{
		[TestMethod]
		public async Task SearchByLocationText_IfLocationIsNull_ShouldReturnNull_AndNotCallApi()
		{
			var wrapper = new GoogleNearbyPlacesSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.SearchByLocationText(null);

			Assert.IsNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetPlacesByTextSearch(It.IsAny<string>()), Times.Never);
			wrapper.GoogleApiRequester.Verify(x => x.GetPlacesByNearbySearch(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()), Times.Never);
		}

		[TestMethod]
		public async Task SearchByLocationText_IfTextSearchReturnsNull_AndNotCallNearbySearch()
		{
			var wrapper = new GoogleNearbyPlacesSearcherTestWrapper
			{
				TextSearchApiResponse = null
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			ICollection<Landmark> response = new List<Landmark>();
			try
			{
				response = await classUnderTest.SearchByLocationText(wrapper.SearchLocation);				
			}
			catch (Exception ex)
			{
				Assert.Fail($"Probable null reference exception from text search result: {ex.Message}");
			}

			Assert.IsNull(response);

			wrapper.GoogleApiRequester.Verify(x => x.GetPlacesByTextSearch(It.IsAny<string>()), Times.Once);
			wrapper.GoogleApiRequester.Verify(
				x => x.GetPlacesByNearbySearch(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()), Times.Never);
		}

		[TestMethod]
		public async Task SearchByLocationText_IfTextSearchSuccessful_CallNearbySearchWithLatLng()
		{
			var wrapper = new GoogleNearbyPlacesSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			// Prove no value set prior to test
			Assert.AreEqual(default(double), wrapper.LatitudeCallback);
			Assert.AreEqual(default(double), wrapper.LongitudeCallback);

			var response = await classUnderTest.SearchByLocationText(wrapper.SearchLocation);

			var locationSetup = wrapper.TextSearchApiResponse.Results.First().Geometry.Location;

			Assert.AreEqual(locationSetup.Lat, wrapper.LatitudeCallback);
			Assert.AreEqual(locationSetup.Lng, wrapper.LongitudeCallback);

			wrapper.GoogleApiRequester.Verify(
				x => x.GetPlacesByNearbySearch(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()), Times.Once);
		}

		[TestMethod]
		public async Task SearchByLocationText_IfNearbySearchReturnsNull_ShouldReturnNull()
		{
			var wrapper = new GoogleNearbyPlacesSearcherTestWrapper
			{
				NearbySearchApiResponse = null
			};

			var classUnderTest = wrapper.GetClassUnderTest();

			// Prove cleared by api call
			ICollection<Landmark> response = new Collection<Landmark> { new Landmark() };
			try
			{
				response = await classUnderTest.SearchByLocationText(wrapper.SearchLocation);				
			}
			catch (Exception ex)
			{
				Assert.Fail($"Probable null reference exception from text search result: {ex.Message}");
			}

			Assert.IsFalse(response.Any());

			wrapper.GoogleApiRequester.Verify(
				x => x.GetPlacesByNearbySearch(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()), Times.Once);
		}

		[TestMethod]
		public async Task SearchByLocationText_MapsResult()
		{
			var wrapper = new GoogleNearbyPlacesSearcherTestWrapper();

			var classUnderTest = wrapper.GetClassUnderTest();

			var response = await classUnderTest.SearchByLocationText(wrapper.SearchLocation);

			var expectedResultDto = wrapper.NearbySearchApiResponse.Results.First();

			Assert.AreEqual(expectedResultDto.Name, response.First().Name);
			Assert.AreEqual(expectedResultDto.Vicinity, response.First().Area);
			Assert.AreEqual(expectedResultDto.Rating, response.First().Rating);

			var expectedPhotoDto = expectedResultDto.Photos.First();

			Assert.AreEqual(expectedPhotoDto.Photo_Reference, response.First().Photos.First().PhotoReference);
			Assert.AreEqual(expectedPhotoDto.Html_Attributions.First(), response.First().Photos.First().HtmlAttribution);			
		}
	}
}