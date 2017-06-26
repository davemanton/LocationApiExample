using Model;

namespace ApplicationTests.ModelBuilders
{
	internal class TravelStatsSearchParametersBuilder
	{
		internal TravelStatsSearchParameters Object { get; private set; }

		internal TravelStatsSearchParametersBuilder Build()
		{
			Object = new TravelStatsSearchParameters
			{
				Origin = "Origin",
				Destination = "Destination"
			};

			return this;
		}
	}
}