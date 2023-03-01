using Microsoft.AspNetCore.Mvc;

namespace RoadSafety.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View("map");
        }
    }
}
