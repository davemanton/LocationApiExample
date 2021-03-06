﻿using Model;

namespace Application.Tests.ModelBuilders
{
	internal class TravelStatsSearchParametersBuilder
	{
		internal TravelStatsSearchParameters Object { get; private set; }

		internal TravelStatsSearchParametersBuilder Build()
		{
			Object = new TravelStatsSearchParameters
			{
				Origin = "Origin",
				Destination = "Destination",
				UnitType = UnitType.Metric
			};

			return this;
		}
	}
}