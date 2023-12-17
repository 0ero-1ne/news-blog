using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news_blog.Model
{
    public class ArticleTemplate
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Author { get; set; }
        public int Rating { get; set; }
        public List<String>? Tags { get; set; }
        public string? TagsString { get; set; }
        public string? Image { get; set; }

        public ArticleTemplate
        (
            int id,
            string? title,
            string? category,
            string? author,
            string? image,
            int rating,
            List<String>? tags
        )
        {
            Id = id;
            Title = title;
            Category = category;
            Author = author;
            Rating = rating;
            Tags = tags;
            Image = image;
            setTagsString();
        }

        private void setTagsString()
        {
            string tags = "";
            Tags!.ForEach(tag => tags += tag + ", ");
            tags = tags.Substring(0, tags.LastIndexOf(","));
            TagsString = tags;
        }
    }
}
