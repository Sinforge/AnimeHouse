using System.Collections;
using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using AnimeHouse.Data;
using AnimeHouse.ViewModels.AdminModels;
using AnimeHouse.ViewModels.AnimeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            ViewData["CategoriesList"] = new List<string>
            { 
                "Games", "Detective", "Military", "Mystic", "Romance", "Supernatural", "Sport", "School", "Thriller",
                "Fantasy", "Horror", "Everyday life", "Music", "Magic", "Comedy"

            };

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
            IEnumerable<Anime> findedAnimes = from anime in _db.Animes where anime.TitleName.ToLower().Contains(NormalizedTitle) select anime;
            return PartialView("SearchedAnimePartialView", findedAnimes);
        }

        [Route("/Anime/FilterAnimes")]
        [HttpPost]
        public IActionResult FilterAnimes([FromForm] List<string> categories, [FromForm] string sortMethod)
        {
            IEnumerable<Anime> filteredAnimes = from anime in _db.Animes select anime;
            if (categories.Count == 0)
            {
                IEnumerable<Anime> allAnimes;
                if (sortMethod == null)
                {
                    allAnimes = from anime in _db.Animes select anime;

                }
                else if (sortMethod == "Alphabetically")
                { 
                    allAnimes = from anime in _db.Animes orderby anime.TitleName select anime;
                }
                else
                {
                    allAnimes = from anime in _db.Animes orderby anime.CountEpisodes select anime;

                }
                return PartialView("SearchedAnimePartialView", allAnimes);
            }

            foreach (var cat in _db.Categories.Include(c => c.Animes ).Where(c => categories.Contains(c.Name)))
            {
               filteredAnimes = filteredAnimes.Intersect(cat.Animes);
            }

            if (filteredAnimes?.Any() == true && sortMethod != null)
            {
                if (sortMethod == "Alphabetically")
                {
                    filteredAnimes = filteredAnimes.OrderBy(anime => anime.TitleName);

                }
                else
                {
                    filteredAnimes = filteredAnimes.OrderBy(anime => anime.CountEpisodes);
                }
            }

            return PartialView("SearchedAnimePartialView", filteredAnimes);
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
        [HttpPost]
        [Route("/Anime/AddToFavorite")]

        public async Task<ActionResult> AddToFavorite(int animeId, string userId)
        {
            Anime? anime = await Task.Run(()=>_db.Animes.FirstOrDefault(a => a.Id == animeId));
            User? user = _db.Users.Include(u => u.Animes).FirstOrDefault(u => u.Id == userId);
            if (anime != null && user != null)
            {
                anime.Likes++;
                user.Animes.Add(anime);
                _db.SaveChanges();
                return StatusCode(200);
            }
            return StatusCode(404);
        }

        [Authorize]
        [HttpPost]
        [Route("/Anime/DeleteFromFavorite")]
        public async Task<ActionResult> DeleteFromFavorite(string userId, int animeId)
        {
            Anime? anime = await Task.Run(() => _db.Animes.FirstOrDefault(a => a.Id == animeId));
            User? user = _db.Users.Include(u => u.Animes).FirstOrDefault(u => u.Id == userId);
            if (anime != null && user != null)
            {
                anime.Likes--;
                user.Animes.Remove(anime);
                _db.SaveChanges();
                return StatusCode(200);
            }
            return StatusCode(406);

        }





        [Authorize]
        [HttpPost]  
        [Route("/Anime/LeaveComment")]
        public IActionResult LeaveComment(string userName, int animeId, string text)
        {
            Comment comment = new Comment { UserName = userName, AnimeId = animeId, Text = text.Replace('^', ' ') };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            Console.WriteLine("Im here");
            var allComments = from com in _db.Comments where com.AnimeId == animeId select com;
            return PartialView("CommentPartialView", allComments);


        }


    }
}

