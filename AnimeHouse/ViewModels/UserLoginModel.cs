using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AnimeHouse.Models
{
    public class UserLoginModel
    {
        [BindRequired]
        [EmailAddress]
        public string Email { get; set; }


        [BindRequired]
        public string Password { get; set; }
    }
}
