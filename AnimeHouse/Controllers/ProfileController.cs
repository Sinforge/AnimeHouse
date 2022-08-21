using AnimeHouse.Data;
using AnimeHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeHouse.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ApplicationContext _db;
        
        
        
        
        public ProfileController (ApplicationContext db, ILogger<ProfileController> logger)
        {
            _db = db;
            _logger = logger;
        }




        [Route("/Profile")]
        public IActionResult Services()
        {
            return View();
        }







        [Route("/Profile/FavoriteAnimes")]
        public async Task<IActionResult> FavoriteAnimeList(string UserId)
        {
            User? user = await _db.Users.Include(u => u.Animes).FirstOrDefaultAsync(u => u.Id == UserId);
            if(user == null)
            {
                _logger.LogError($"User with id: {UserId} not found ");
                return NotFound();
            }
            return View(user.Animes.ToList());

        }
    }
}
