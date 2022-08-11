using AnimeHouse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

		
	}
}