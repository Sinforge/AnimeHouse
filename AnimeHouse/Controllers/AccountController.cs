using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using AnimeHouse.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;

namespace AnimeHouse.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		public AccountController(ILogger<AccountController> logger, UserManager<User> userManager,SignInManager<User> signInManager)
        {
			_logger = logger;
			_userManager = userManager;
			_signInManager = signInManager;
        }
		[Route("Registration")]
		[HttpGet]
		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		[Route("Registration")]
		public async  Task<IActionResult> Registration(UserRegistrationModel user)
		{
			if (ModelState.IsValid)
			{

				User user1 = new User { Email = user.Email, UserName = user.Nickname};
				_logger.LogInformation("User enter correct data");
				var result = await _userManager.CreateAsync(user1, user.Password);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user1, false);
					_logger.LogInformation("User's data add to database");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
				return RedirectToAction("Login");
                
			}
			else
			{
				_logger.LogInformation("User enter uncorrect data");
				return View(user);
			}

		}
        [AcceptVerbs("Get","Post")]
		public async Task<IActionResult> CheckEmail(string Email)
        {
			_logger.LogInformation("Check Email");
			var user = await _userManager.FindByEmailAsync(Email);
			if (user == null)
            {
				_logger.LogInformation("Email is valid");
				return Json(true);
            }
			_logger.LogInformation("Email is invalid");
			return Json(false);

        }
        [HttpGet]
        [Route("Login")]
		public IActionResult Login()
        {
			return View();
        }

      
	}
}
