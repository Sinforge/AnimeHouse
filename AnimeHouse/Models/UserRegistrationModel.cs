using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AnimeHouse.Models
{
	public class UserRegistrationModel
	{

		

		[Required]
		public string Email { get; set; }
		[Required]
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
