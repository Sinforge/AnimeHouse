using Microsoft.AspNetCore.Identity;

namespace AnimeHouse.ViewModels.AdminModels
{
    public class UserListForAdminViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }

        public string UserNickname { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public UserListForAdminViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
