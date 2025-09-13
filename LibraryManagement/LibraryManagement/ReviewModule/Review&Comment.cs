namespace LibraryManagement.ReviewModule
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string User { get; set; } = "Гость";
        public int Rating { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public List<Comment> Comments { get; set; } = new();
    }

    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string User { get; set; } = "Гость";
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
