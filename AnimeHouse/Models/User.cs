
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeHouse.Models
{
    public class User : IdentityUser
    {
        public ICollection<Anime> FavoriteAnimes {get;set;}
        public User()
        {
            FavoriteAnimes = new List<Anime>();
        }
        
    }


}
