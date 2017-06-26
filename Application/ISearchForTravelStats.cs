using System.Threading.Tasks;
using Model;

namespace Application
{
	public interface ISearchForTravelStats
	{
		Task<TravelStatsSearchResults> Search(TravelStatsSearchParameters searchParameters);
	}
}