namespace AnimeHouse.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Anime> Animes { get; set; } = new List<Anime>();
        public override bool Equals(object? obj)
        {
            if (obj is Category person) return Name == person.Name;
            return false;
        }
        public override int GetHashCode() => Name.GetHashCode();
    }
}
