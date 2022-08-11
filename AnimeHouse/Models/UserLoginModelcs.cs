using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnimeHouse.Models
{
    public class UserLoginModelcs
    {
        [BindRequired]
        public string Email { get; set; }


        [BindRequired]
        public string Password { get; set; }
    }
}
