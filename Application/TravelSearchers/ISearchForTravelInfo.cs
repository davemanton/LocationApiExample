using System.Threading.Tasks;
using Model;

namespace Application.TravelSearchers
{
	public interface ISearchForTravelInfo
	{
		Task<TravelInfoSearchResult> Search(TravelStatsSearchParameters searchParameters);
	}
}