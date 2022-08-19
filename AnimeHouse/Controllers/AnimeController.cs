using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using AnimeHouse.Data;
using AnimeHouse.ViewModels.AdminModels;
using AnimeHouse.ViewModels.AnimeModels;
using Microsoft.AspNetCore.Identity;

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

        [HttpPost]
        public async Task<IActionResult> ChangeAnime(AddAnimeTitleViewModel model)
        {

            Anime? anime = _db.Animes.FirstOrDefault(a => a.TitleName == model.Title);
            if (anime == null)
            {
                return NotFound();
            }
            if (anime.ImgName != null && model.Img != null)
            {
                System.IO.File.Delete($@"wwwroot\" + anime.Path + @$"\{anime.ImgName}");
                // (anime.Path + @$"\{anime.ImgName}");

            }
            if (model.Img != null)
            {
                string FilePath = $@"wwwroot\" + anime.Path + @$"\{model.Img.FileName}";
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    await model.Img.CopyToAsync(fileStream);
                }
                anime.ImgName = model.Img.FileName;
            }



            //Change variables of record
            anime.Description = model.Description ?? anime.Description;
            anime.ShortDescription = model.ShortDescription ?? anime.Description;
            anime.CountEpisodes = model.CountEpisodes ?? anime.CountEpisodes;

            anime.TitleName = model.Title ?? anime.TitleName;
            _db.SaveChanges();

            return View("DataSuccessfulAdd", "Home");


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
        public async Task<ActionResult> AnimePage([FromQuery] string title, [FromQuery] int episod)
        {
            Anime? founded_anime = null;
            await Task.Run(() => { founded_anime = _db.Animes.FirstOrDefault(a => a.TitleName == title); });
            if (founded_anime == null || episod > founded_anime?.CountEpisodes || episod < 0)
            {
                return NotFound();
            }
            return View(new AnimePageViewModel { SearchedAnime = founded_anime, Episod = episod });
        }


        public async Task<ActionResult> AddToFavorite(int animeId, string userId)
        {
            Anime? anime = await Task.Run(()=>_db.Animes.FirstOrDefault(a => a.Id == animeId));
            User? user = await _userManager.FindByIdAsync(userId);
            if (anime != null && user != null)
            {
                user.FavoriteAnimes.Add(anime);
                _db.SaveChanges();
                return Content("Anime was added to your list");
            }
            else return NotFound();
            
        }



    }
}

