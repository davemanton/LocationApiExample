using Model;

namespace Application.Tests.ModelBuilders
{
	internal class TravelStatsSearchResultBuilder
	{
		internal TravelStatsSearchResults Object { get; private set; }

		internal TravelStatsSearchResultBuilder Build()
		{
			Object = new TravelStatsSearchResults
			{
				Destination = "Destination",
				Origin = "Origin",
				TravelDuration = "TravelDuration",
				Distance = "Distance",
				DistanceMetres = 10000,
				TravelSeconds = 20000
			};

			return this;
		}
	}
}