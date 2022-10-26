using AnimeHouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AnimeHouse.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}


		[HttpGet]
		[Route("")]
		public IActionResult Main()
		{

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

        [Route("/Anime/KomiSan")]
        [Authorize]
		public IActionResult AnimePage()
        {
			return View();
        }

        public IActionResult DevelopersPage()
        {
            return View("Index");
        }
	}
}