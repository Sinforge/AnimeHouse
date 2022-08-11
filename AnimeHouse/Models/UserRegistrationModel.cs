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
		public string Password { get; set; }

		[Required]
		public string PasswordConfirm { get; set; }

	}
}
