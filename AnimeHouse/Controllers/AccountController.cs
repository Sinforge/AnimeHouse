using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;

namespace AnimeHouse.Controllers
{
	public class AccountController : Controller
	{
		[Route("Registration")]
		[HttpGet]
		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		[Route("Registration")]
		public IActionResult Registration([FromForm]User user)
		{
			if(ModelState.IsValid)
			{
				/*Добавляем пользователя в базу данных*/
				return Content("Aboba");
			}
			else
			{
				return View();
			}

		}
	}
}
