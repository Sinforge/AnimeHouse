namespace AnimeHouse.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public string UserName { get; set; } = null!;
        public int AnimeId { get; set; }

        public string Text { get; set; } = null!;
    }
}
