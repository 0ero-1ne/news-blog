namespace news_blog.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public string? Text { get; set; }
    }
}
