using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using AnimeHouse.ViewModels.Attributes;
namespace AnimeHouse.ViewModels
{
    public class AddAnimeTitleViewModel
    {
        [Required]
        //[Title(Lang.En)]
        [Display(Name ="English Title name")]
        //[Remote(action: "CheckTitle", controller: "Anime", ErrorMessage = "This title was taked")]
        public string TitleEng { get; set; }

        [Required]
        [Display(Name = "Russian Title  name")]
        //[Title(Lang.Ru)]
        //[Remote(action: "CheckTitle", controller: "Anime", ErrorMessage = "This title was taked")]
        public string TitleRu { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        
    }
}
