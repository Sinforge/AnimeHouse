using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AnimeHouse.Models.Attributes;
namespace AnimeHouse.Models
{
	public class UserRegistrationViewModel
	{

		

		[Required]
        [Remote(action:"CheckEmail", controller:"Account", ErrorMessage = "This email was taked")]
		public string Email { get; set; }
		[Required]
        [Nickname(new string[] {"lox", "idiot", "nigger" }, ErrorMessage = "Uncorrect nickname")]
		public string Nickname { get; set; }

		[Required]
        [DataType(DataType.Password)]
		public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[Required]
        [DataType(DataType.Password)]
		public string PasswordConfirm { get; set; }

	}
}
