using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerCollection.Models;
using SneakerCollection.Services;
using System.Diagnostics;


namespace SneakerCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISneakerService _sneakerService;

        public HomeController(ILogger<HomeController> logger, ISneakerService sneakerService)
        {
            _logger = logger;
            _sneakerService = sneakerService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedSneakers = _sneakerService.GetFeaturedSneakers(),
                RecentSneakers = _sneakerService.GetRecentSneakers(6),
                TotalSneakers = _sneakerService.GetAllSneakers().Count()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // Nouveau ViewModel pour la page d'accueil
    public class HomeViewModel
    {
        public IEnumerable<Sneaker> FeaturedSneakers { get; set; } = new List<Sneaker>();
        public IEnumerable<Sneaker> RecentSneakers { get; set; } = new List<Sneaker>();
        public int TotalSneakers { get; set; }
    }
}
