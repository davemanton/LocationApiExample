using System.Threading.Tasks;
using Application.TravelSearchers;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Website.Controllers
{
    public class PlacesSearchController : Controller
    {
	    private readonly ISearchForTravelInfo _travelInfoSearcher;

		public PlacesSearchController(ISearchForTravelInfo travelInfoSearcher)
		{
			_travelInfoSearcher = travelInfoSearcher;
		}

	    public IActionResult Index()
        {
            return View();
        }

	    public async Task<IActionResult> SearchResults(TravelStatsSearchParameters searchParameters)
	    {
		    var travelInfo = await _travelInfoSearcher.Search(searchParameters);

		    return View(travelInfo);
	    }

        public IActionResult Error()
        {
            return View();
        }
    }
}
