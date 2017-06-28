using System.Collections.Generic;
using Model;

namespace Application.Tests.ModelBuilders
{
	internal class LandmarksBuilder
	{
		internal ICollection<Landmark> Object { get; private set; }

		internal LandmarksBuilder Build(int noLandmarks)
		{
			Object = new List<Landmark>();

			for (var i = 1; i <= noLandmarks; i++)
			{
				Object.Add(new Landmark
				{
					Name = $"Name{i}",
					Area = $"Area{i}",
					Rating = i + i * 0.1,
					Photos = new List<Photo>
					{
						new Photo
						{
							PhotoReference = $"PhotoReference{i}",
							HtmlAttribution = $"HtmlAttribution{i}"
						}
					}
				});
			}

			return this;
		}
	}
}