namespace AnimeHouse.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string? TitleName { get; set; }
        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? Path { get; set; }

        public string? ImgName { get; set; }

        public int? CountEpisodes { get; set; }
        public ICollection<User> Users { get; set; }
        public Anime()
        {
            Users = new List<User>();
        }

    }
}
