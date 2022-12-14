using AnimeHouse.Data;
using AnimeHouse.Models;
using AnimeHouse.ViewModels.AdminModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnimeHouse.ViewModels.AnimeModels;
using Microsoft.EntityFrameworkCore;

namespace AnimeHouse.Controllers
{
    //Admin can add new anime titles, change user roles, change anime description
    [Authorize(Roles = "admin, moderator")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _db;

        public AdminController(ILogger<AdminController> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        [Route("/Admin/CreateTitle")]
        public async Task<IActionResult> CreateTitle(AddAnimeTitleViewModel model)
        {
            //Create new catalog for anime data(img, videos)
            string path = Directory.GetCurrentDirectory() + @"\wwwroot\animes\" + model.Title;
            _logger.LogInformation($"Title was added and his path: {path}");
            Directory.CreateDirectory(path);

            Anime anime = new Anime() {
                CountEpisodes = model.CountEpisodes,
                ImgName = model.Img?.FileName,
                Description = model.Description,
                Path = @$"animes\{model.Title}\",
                ShortDescription = model.ShortDescription,
                TitleName = model.Title
            };
            _db.Animes.Add(anime);
            foreach (var cat in _db.Categories.Where(cat => model.Categories.Contains(cat.Name)))
            {
 
                cat.Animes.Add(anime);
                _db.Categories.Update(cat);

            }

            if (model.Img != null)
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
            await _db.SaveChangesAsync();


            _logger.LogInformation("New title was succesful added");
            return RedirectToAction("Main", "Home");

        }


        [HttpGet]
        [Route("/Admin/CreateTitle")]
        public IActionResult CreateTitle()
        {
            ViewData["CategoriesList"] = new List<string> 
            { "Games", "Detective", "Military", "Mystic", "Romance", "Supernatural", "Sport", "School", "Thriller",
                "Fantasy", "Horror", "Everyday life", "Music", "Magic", "Comedy"

            };
            return View();
        }

        //TODO: user list with roles

        [HttpGet]
        [Route("/Admin/UserList")]
        public async Task<IActionResult> UserList()
        {

            List<UserListForAdminViewModel> users = new List<UserListForAdminViewModel>();
            List<User> DbUsers = _db.Users.ToList();
            foreach (var dbUser in DbUsers)
            {
                users.Add(
                    new UserListForAdminViewModel {
                        UserRoles = await _userManager.GetRolesAsync(dbUser),
                        AllRoles = _roleManager.Roles.ToList(),
                        UserEmail = dbUser.Email,
                        UserId = dbUser.Id,
                        UserNickname = dbUser.UserName

                    });
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole([FromRoute] string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            IList<string> UserRoles = await _userManager.GetRolesAsync(user);
            return View(new UserListForAdminViewModel
            {
                UserRoles = UserRoles,
                AllRoles = _roleManager.Roles.ToList(),
                UserEmail = user.Email,
                UserId = id,
                UserNickname = user.UserName

            });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole([FromForm] string id, [FromForm] List<string> roles )
        {
            User user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                // Get all roles
                var allRoles = _roleManager.Roles.ToList();
                // Only added roles
                var addedRoles = roles.Except(userRoles);
                // Roles, which will be deleted
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("UserList");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAnime([FromForm] int animeId)
        {
            Anime animeForDelete = _db.Animes.FirstOrDefault(anime => anime.Id == animeId);
            if (animeForDelete != null)
            {
                _db.Animes.Remove(animeForDelete);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("CreateTitle");

        }

        [HttpPost]
        public async Task<IActionResult> ChangeAnime([FromForm] AddAnimeTitleViewModel model)
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

        [HttpPost]
        [Route("/Admin/DeleteComment")]
        public async Task<IActionResult> DeleteComment(string userName, int animeId, string text)
        {
            Comment? finded_comment = await _db.Comments.FirstOrDefaultAsync(c => c.UserName == userName && c.AnimeId == animeId && c.Text == text);
            if (finded_comment != null)
            {
                _db.Comments.Remove(finded_comment);
                _db.SaveChanges();
            }
            var allComments = from com in _db.Comments where com.AnimeId == animeId select com;
            return PartialView("CommentPartialView", allComments);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 409_715_200)]
        [RequestSizeLimit(409_715_200)]
        public async Task<IActionResult> AddEpisode(AddEpisodeViewModel model)
        {
            string FilePath = @"wwwroot\animes\" + model.Name + @"\episodes\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string episodeNumber = "1";
            List<string> filesDir1 = (from a in Directory.GetFiles(FilePath) select Path.GetFileNameWithoutExtension(a))
                .ToList();
            if (filesDir1.Count != 0)
            {
                episodeNumber = (filesDir1.Count + 1).ToString();
            }

            using (var fileStream = new FileStream(FilePath + episodeNumber + ".mp4", FileMode.Create))
            {
                await model.Episode.CopyToAsync(fileStream);
            }

            return StatusCode(200);
        }
    }
}
