using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Application.TravelSearchers
{
	public interface ISearchForNearbyPlaces
	{
		Task<ICollection<Landmark>> SearchByLocationText(string location);
	}
}