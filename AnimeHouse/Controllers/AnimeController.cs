using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using AnimeHouse.Data;
using AnimeHouse.ViewModels.AdminModels;
using AnimeHouse.ViewModels.AnimeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AnimeHouse.Controllers
{
    public class AnimeController : Controller
    {
        private readonly ILogger<AnimeController> _logger;
        private readonly ApplicationContext _db;
        private readonly UserManager<User> _userManager;
        public AnimeController(ILogger<AnimeController> logger, ApplicationContext db, UserManager<User> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager; 
        }

        [Route("Anime")]
        public IActionResult GetListAnime()
        {

            return View(_db.Animes.ToArray());

        }

        [HttpGet]        
        public IActionResult SearchAnime(string animeTitle)
        {

            if(animeTitle == null)
            {
                return View("GetListAnime");
            }
            string NormalizedTitle = animeTitle.ToLower();
            IEnumerable<Anime> finded_anime = from anime in _db.Animes where anime.TitleName.ToLower().Contains(NormalizedTitle) select anime;
            return View("GetListAnime", finded_anime);
        }

       


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckTitle(string Title)
        {
            _logger.LogInformation("Check Title");
            Anime? anime = null;
            await Task.Run(() => { anime = _db.Animes.FirstOrDefault(a => a.TitleName == Title); });
            if (anime == null)
            {
                _logger.LogInformation("Title is valid");
                return Json(true);
            }
            _logger.LogInformation("Title is invalid");
            return Json(false);

        }

        [HttpGet]
        [Route("/Anime/Episods")]
        public async Task<IActionResult> AnimePage([FromQuery] string title, [FromQuery] int episod)
        {
            Anime? founded_anime = null;
            await Task.Run(() => { founded_anime = _db.Animes.FirstOrDefault(a => a.TitleName == title); });
            if (founded_anime == null || episod > founded_anime?.CountEpisodes || episod < 0)
            {
                return NotFound();
            }
            return View(new AnimePageViewModel { SearchedAnime = founded_anime, Episod = episod });
        }


        [Authorize]
        public async Task<IActionResult> AddToFavorite(int animeId, string userId)
        {
            Anime? anime = await Task.Run(()=>_db.Animes.FirstOrDefault(a => a.Id == animeId));
            User? user = _db.Users.Include(u => u.Animes).FirstOrDefault(u => u.Id == userId);
            if (anime != null && user != null)
            {
                user.Animes.Add(anime);
                _db.SaveChanges();
                return Content("AnimePage");
            }
            else return NotFound();
            
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromFavorite(int animeId, string userId)
        {
            Anime? anime = await Task.Run(() => _db.Animes.FirstOrDefault(a => a.Id == animeId));
            User? user = _db.Users.Include(u => u.Animes).FirstOrDefault(u => u.Id == userId);
            if (anime != null && user != null)
            {
                user.Animes.Remove(anime);
                _db.SaveChanges();
                return RedirectToAction("AnimePage");
            }
            else return NotFound();

        }





        [Authorize]
        [HttpPost]
        [Route("/Anime/Episods")]
        public IActionResult LeaveComment([FromQuery] string userName, [FromQuery] int animeId, [FromQuery] string text)
        {
            Comment comment = new Comment { UserName = userName, AnimeId = animeId, Text = text.Replace('^', ' ') };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            var allComments = from com in _db.Comments where com.AnimeId == animeId select com;
            return PartialView("CommentPartialView", allComments);


        }


    }
}

