using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AnimeHouse.Models
{
	public class User
	{


		[BindRequired]
		public string Nickname { get; set; } = null!;

		[BindRequired]
		public string Email { get; set; } = null!;

		[BindRequired]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[BindRequired]
		[DataType(DataType.Password)]
		public string PasswordConfirm { get; set; } = null!;

	}
}
