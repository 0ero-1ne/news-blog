using System;

namespace news_blog.Model
{
    public class Article
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ShortText { get; set; }

        public string? Text { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public int Rating { get; set; }

        public string? Image { get; set; }
    }
}
