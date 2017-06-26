using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Website.Controllers
{
    public class PlacesSearchController : Controller
    {
	    private readonly ISearchForTravelStats _travelStatsSearcher;

	    public PlacesSearchController(ISearchForTravelStats travelStatsSearcher)
	    {
		    _travelStatsSearcher = travelStatsSearcher;
	    }

	    public IActionResult Index()
        {
            return View();
        }

	    public async Task<IActionResult> SearchResults(TravelStatsSearchParameters searchParameters)
	    {
		    var searchResults = await _travelStatsSearcher.Search(searchParameters);	

		    return View(searchResults);
	    }

        public IActionResult Error()
        {
            return View();
        }
    }
}
