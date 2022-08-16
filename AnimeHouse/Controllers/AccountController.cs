using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using AnimeHouse.Data;
using Microsoft.AspNetCore.Identity;

namespace AnimeHouse.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AccountController(ILogger<AccountController> logger, UserManager<User> userManager,SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
			_roleManager = roleManager;
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
		public async  Task<IActionResult> Registration(UserRegistrationViewModel user)
		{
			if (ModelState.IsValid)
			{

				User user1 = new User { Email = user.Email, UserName = user.Nickname};
				_logger.LogInformation("User enter correct data");
				var result = await _userManager.CreateAsync(user1, user.Password);
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user1, "user");
					await _signInManager.SignInAsync(user1, false);
					_logger.LogInformation("User's data add to database");
					return RedirectToAction("Main", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
			}
             
			}
			else
			{
				_logger.LogInformation("User enter uncorrect data");
				return View(user);
			}
			return View(user);

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

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(UserLoginViewModel user)
		{
			if (ModelState.IsValid)
			{
				_logger.LogInformation("Uncorrect input data");
				var result =
					await _signInManager.PasswordSignInAsync(user.Nickname, user.Password, user.RememberMe, false);
				if (result.Succeeded)
				{
					_logger.LogInformation("User is login");
					return RedirectToAction("Main", "Home");
					
				}
				else
				{
					_logger.LogInformation("Неправильно введены данные");
					ModelState.AddModelError("", "Uncorrect nickname or password");
				}

			}
			return View(user);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			// удаляем аутентификационные куки
			await _signInManager.SignOutAsync();
			return RedirectToAction("Main", "Home");
		}


	}
}
