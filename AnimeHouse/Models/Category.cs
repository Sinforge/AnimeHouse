namespace AnimeHouse.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Anime> Animes { get; set; } = new List<Anime>();
    }
}
