using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models;
using System.IO;
using AnimeHouse.ViewModels;
using AnimeHouse.Data;

namespace AnimeHouse.Controllers
{
    public class AnimeController : Controller
    {
        ILogger<AnimeController> _logger;
        private readonly ApplicationContext _db;
        private readonly DirectoryInfo _directory;
        public AnimeController(ILogger<AnimeController> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
            _directory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), @"\wwwroot\animes\"));
        }

        [Route("Anime")]
        public IActionResult GetListAnime()
        {
            
            return View(_db.Animes.ToArray());

        }

		[Route("Anime")]
		[HttpPost]
        public async Task<IActionResult> GetListAnime(AddAnimeTitleViewModel model) { 
	
            Anime? anime = _db.Animes.FirstOrDefault(a => a.TitleName == model.Title);
            if(anime == null)
			{
                return NotFound();
			}
            if(model.Img == null)
			{

                return RedirectToAction("GetListAnime");
			}
            if(anime.ImgName != null)
			{
                FileInfo fileInfo = new FileInfo(anime.Path + @$"\{anime.ImgName}");
                fileInfo.Delete();
			}
            string FilePath = anime.Path + @$"\{model.Img.FileName}";
            using (var fileStream = new FileStream(FilePath, FileMode.Create))
            {
               await model.Img.CopyToAsync(fileStream);
            }


            //Change variables of record
            anime.Description = model.Description??anime.Description;
            anime.ShortDescription = model.ShortDescription??anime.Description;
            anime.CountEpisodes = model.CountEpisodes??anime.CountEpisodes;
            anime.Path = model.Img.FileName;
            anime.TitleName = model.Title;
            _db.SaveChanges();
            
            return View();


        } 

         [HttpPost]
         [Route("/Anime/CreateTitle")]
          public  async Task<IActionResult> CreateTitle(AddAnimeTitleViewModel model)
          {
                //Create new catalog for anime data(img, videos)
                string path = Directory.GetCurrentDirectory() + @"\wwwroot\animes\" + model.Title;
                _logger.LogInformation($"Title was added and his path: {path}");
                Directory.CreateDirectory(path);
              
                Anime anime = new Anime() { CountEpisodes = model.CountEpisodes, ImgName = model.Img?.FileName ,Description = model.Description, Path = path, ShortDescription = model.ShortDescription, TitleName = model.Title};
                if(model.Img != null)
			{
                _logger.LogInformation("User add img for anime");
                using (var fileStream = new FileStream(path + @$"\{model.Img.FileName}", FileMode.Create))
                {
                    await model.Img.CopyToAsync(fileStream);
                }
            }
                else
			{
                _logger.LogInformation("User dont add img");
			}


                //Add to database
                _db.Animes.Add(anime);
                await _db.SaveChangesAsync();
                
            
                _logger.LogInformation("New title was succesful added");
                return RedirectToAction("Main", "Home");

          }


    [HttpGet]
    [Route("/Anime/CreateTitle")]
    public IActionResult CreateTitle()
        {
          return View();
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
    }
    }

