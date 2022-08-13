using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AnimeHouse.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [Display(Name ="Nickname")]
        public string Nickname { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name ="Remember")]
        public bool RememberMe { get; set; }

    }
}
