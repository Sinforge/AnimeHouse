using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using AnimeHouse.ViewModels.Attributes;
using AnimeHouse.Data;

namespace AnimeHouse.ViewModels.AdminModels
{
    public class AddAnimeTitleViewModel
    {
        [Required]
        //[Title(Lang.En)]
        [Display(Name = "English Title name")]
        [Remote(action: "CheckTitle", controller: "Anime", ErrorMessage = "This title was taked")]
        public string? Title { get; set; }



        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Count of episods")]
        public int? CountEpisodes
        {
            get; set;
        }

        [Display(Name = "Avatar of anime")]
        public IFormFile Img { get; set; }


    }
}
