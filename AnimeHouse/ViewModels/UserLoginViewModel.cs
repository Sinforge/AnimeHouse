using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AnimeHouse.Models
{
    public class UserLoginViewModel
    {
        [BindRequired]
        [Display(Name ="Nickname")]
        public string Nickname { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [BindRequired]
        public string Password { get; set; }

        [Display(Name ="Remember")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
