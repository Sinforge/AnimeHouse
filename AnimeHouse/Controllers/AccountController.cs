using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using AnimeHouse.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnimeHouse.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		public AccountController(ILogger<AccountController> logger)
        {
			_logger = logger;
        }
		[Route("Registration")]
		[HttpGet]
		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		[Route("Registration")]
		public IActionResult Registration(UserRegistrationModel user, [FromServices]ApplicationContext db)
		{
			if(ModelState.IsValid)
			{
				User user1 = new User { Email = user.Email, Nickname = user.Nickname, Password = user.Password};
				_logger.LogInformation("User enter correct data");
				db.Users.Add(user1);
				_logger.LogInformation("User's data add to database");
				db.SaveChanges();
				return RedirectToAction("Login");
			}
			else
			{
				string errors = "";
				_logger.LogInformation("User enter uncorrect data");
				foreach(var item in ModelState)
                {
					if(item.Value.ValidationState == ModelValidationState.Invalid)
                    {
						errors += $"Ошибки свойства: {item.Key}:\n";
						foreach (var error in item.Value.Errors)
                        {
							errors += $"{error.ErrorMessage.ToString()}\n";
                        } 
                    }
                }
				return View(errors);
			}

		}
        [HttpGet]
        [Route("Login")]
		public IActionResult Login()
        {
			return View();
        }
	}
}
