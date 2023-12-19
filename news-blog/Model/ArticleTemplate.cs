using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace news_blog.Model
{
    public class ArticleTemplate
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Author { get; set; }
        public int Rating { get; set; }
        public List<string>? Tags { get; set; }
        public string? TagsString { get; set; }
        public BitmapImage? Image { get; set; }

        public ArticleTemplate
        (
            int id,
            string? title,
            string? category,
            string? author,
            string? image,
            int rating,
            List<string>? tags
        )
        {
            Id = id;
            Title = title;
            Category = category;
            Author = author;
            Rating = rating;
            Tags = tags;
            setBitmapImage(image);
            setTagsString();
        }

        private void setTagsString()
        {
            string tags = "";
            Tags!.ForEach(tag => tags += tag + ", ");
            tags = tags.Substring(0, tags.LastIndexOf(","));
            TagsString = tags;
        }

        private void setBitmapImage(string? image)
        {
            using (var fileStream = new FileStream(image!, FileMode.Open, FileAccess.Read))
            {
                BitmapImage _image = new BitmapImage();
                _image.BeginInit();
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.StreamSource = fileStream;
                _image.EndInit();
                Image = _image;
            }
        }
    }
}
