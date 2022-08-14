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

        /*[Route("Anime")]
        public async Task<IActionResult> Anime(ApplicationContext db)
        {
            

        }*/



        
                [HttpPost]
                [Route("/Anime/CreateTitle")]
                public  async Task<IActionResult> CreateTitle(AddAnimeTitleViewModel model)
                {
            string path = Directory.GetCurrentDirectory() + @"\wwwroot\animes\" + model.TitleEng;
            Console.WriteLine(path);       
            _logger.LogInformation($"Title was added and his path: {path}");
                    Directory.CreateDirectory(path);
                    Anime anime = new Anime { Description = model.Description, ImgPath = path, ShortDescription = model.ShortDescription, TitleName = model.TitleEng};
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

