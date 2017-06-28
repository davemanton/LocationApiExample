using System.Threading.Tasks;
using Model;

namespace Application.TravelSearchers
{
	public interface ISearchForTravelStats
	{
		Task<TravelStatsSearchResults> Search(TravelStatsSearchParameters searchParameters);
	}
}