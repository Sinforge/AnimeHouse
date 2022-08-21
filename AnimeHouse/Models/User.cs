
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeHouse.Models
{
    public class User : IdentityUser
    {
        public List<Anime> Animes {get;set;} = new List<Anime>();

        
    }


}
