﻿using news_blog.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace news_blog.Model
{
    public static class DataWorker
    {

        // ----- Tags CRUD -----

        public static List<Tag> GetTags()
        {
            using (var db = new ApplicationContext())
            {
                return db.Tags.ToList();
            }
        }

        public static string CreateTag(string? Tag)
        {
            string result = "Не удалось создать новый тег";
            using (var db = new ApplicationContext())
            {
                bool isTagExists = false;

                foreach(var tag in GetTags())
                    if (tag.Title!.ToLower() == Tag!.ToLower())
                        isTagExists = true;

                if (!isTagExists && Tag != null)
                {
                    Tag tag = new() { Title = Tag };
                    db.Tags.Add(tag);
                    db.SaveChanges();
                    result = "Тег успешно создан";
                }
            }
            return result;
        }

        public static string UpdateTag(int TagId, string? TagTitle)
        {
            string result = "Не удалось обновить тег";
            using (var db = new ApplicationContext())
            {
                Tag tag = db.Tags.SingleOrDefault(tag => tag.Id == TagId)!;

                bool isTagExists = false;

                foreach (var t in GetTags())
                    if (t.Title!.ToLower() == TagTitle!.ToLower())
                        isTagExists = true;

                if (tag != null)
                {
                    if (!isTagExists && TagTitle != null)
                    {
                        tag.Title = TagTitle == null ? tag.Title : TagTitle;
                        db.SaveChanges();
                        result = "Тег успешно обновлен";
                    }
                }
            }
            return result;
        }

        public static string DeleteTag(int TagId)
        {
            string result = "Не удалось удалить тег";
            using (var db = new ApplicationContext())
            {
                Tag tag = db.Tags.SingleOrDefault(tag => tag.Id == TagId)!;
                if (tag != null)
                {
                    List<ArticleTag> articleTags = GetArticleTags();
                    foreach (var articleTag in articleTags)
                    {
                        if (articleTag.TagId == TagId) DeleteArticleTag(articleTag.Id);
                    }
                    db.Tags.Remove(tag);
                    db.SaveChanges();
                    result = "Тег успешно удалён";
                }
            }
            return result;
        }

        // ----- Categories CRUD -----

        public static List<Category> GetCategories()
        {
            using (var db = new ApplicationContext())
            {
                return db.Categories.ToList();
            }
        }

        public static string CreateCategory(string? Category)
        {
            string result = "Не удалось создать новую категорию";
            using (var db = new ApplicationContext())
            {
                bool isCategoryExists = false;

                foreach (var category in GetCategories())
                    if (category.Title!.ToLower() == Category!.ToLower())
                        isCategoryExists = true;

                if (!isCategoryExists && Category != null)
                {
                    Category category = new() { Title = Category };
                    db.Categories.Add(category);
                    db.SaveChanges();
                    result = "Категория успешно создана";
                }
            }
            return result;
        }

        public static string UpdateCategory(int CategoryId, string? CategoryTitle)
        {
            string result = "Не удалось обновить категорию";
            using (var db = new ApplicationContext())
            {
                Category category = db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;

                bool isCategoryExists = false;

                foreach (var c in GetCategories())
                    if (c.Title!.ToLower() == CategoryTitle!.ToLower())
                        isCategoryExists = true;

                if (category != null)
                {
                    if (!isCategoryExists && CategoryTitle != null)
                    {
                        category.Title = CategoryTitle == null ? category.Title : CategoryTitle;
                        db.SaveChanges();
                        result = "Категория успешно обновлена";
                    }
                }
            }
            return result;
        }

        public static string DeleteCategory(int CategoryId)
        {
            string result = "Не удалось удалить категорию";
            using (var db = new ApplicationContext())
            {
                Category category = db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;
                if (category != null)
                {
                    List<Article> articles = GetArticles();
                    List<Comment> comments = GetComments();
                    foreach (var article in articles)
                    {
                        if (article.CategoryId == CategoryId) article.CategoryId = 1;
                    }
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    result = "Категория успешно удалена";
                }
            }
            return result;
        }

        // ----- Users CRUD -----

        public static List<User> GetUsers()
        {
            using (var db = new ApplicationContext())
            {
                return db.Users.ToList();
            }
        }

        public static string CreateUser(string? Username, string? Password, bool IsAdmin)
        {
            string result = "Не удалось создать нового пользователя";
            using (var db = new ApplicationContext())
            {
                bool isUserExists = db.Users.Any(user => user.Username!.ToLower() == Username!.ToLower());
                if (!isUserExists)
                {
                    var passwordBytes = Encoding.UTF8.GetBytes(Password!);
                    var passwordHash = SHA256.HashData(passwordBytes);
                    User user = new() { Username = Username, Password = Convert.ToHexString(passwordHash), IsAdmin = IsAdmin ? 1 : 0 };
                    db.Users.Add(user);
                    db.SaveChanges();
                    result = "Пользователь успешно добавлен";
                }
            }
            return result;
        }

        public static string UpdateUser(int UserId, string? Username, string? Password, bool IsAdmin)
        {
            string result = "Не удалось обновить пользователя";
            using (var db = new ApplicationContext())
            {
                User user = db.Users.SingleOrDefault(user => user.Id == UserId)!;
                User isUserExists = db.Users.SingleOrDefault(user => user.Username == Username)!;
                if (user != null)
                {
                    if (isUserExists != null && isUserExists.Id != user.Id)
                    {
                        return result;
                    }

                    if (user.Password != Password!)
                    {
                        var passwordBytes = Encoding.UTF8.GetBytes(Password!);
                        var passwordHash = SHA256.HashData(passwordBytes);
                        user.Password = Convert.ToHexString(passwordHash);
                    }

                    user.Username = Username == null ? user.Username : Username;
                    user.IsAdmin = IsAdmin ? 1 : 0;
                    db.SaveChanges();
                    result = "Пользователь успешно обновлён";
                }
            }
            return result;
        }

        public static string DeleteUser(int UserId)
        {
            string result = "Не удалось удалить пользователя";
            using (var db = new ApplicationContext())
            {
                User user = db.Users.SingleOrDefault(user => user.Id == UserId)!;
                if (user != null)
                {
                    List<Article> articles = GetArticles();
                    foreach (var article in articles)
                    {
                        if (article.AuthorId == UserId) DeleteArticle(article.Id);
                    }
                    db.Users.Remove(user);
                    db.SaveChanges();
                    result = "Пользователь успешно удалён";
                }
            }
            return result;
        }

        // ----- Comments CRUD -----

        public static List<Comment> GetComments()
        {
            using (var db = new ApplicationContext())
            {
                return db.Comments.ToList();
            }
        }

        public static string CreateComment(int UserId, int ArticleId, string? Text)
        {
            string result = "Не удалось создать новый комментарий";
            using (var db = new ApplicationContext())
            {
                Comment comment = new() { UserId = UserId, ArticleId = ArticleId, Text = Text };
                db.Comments.Add(comment);
                db.SaveChanges();
                result = "Комментарий успешно добавлен";
            }
            return result;
        }

        public static string UpdateComment(int CommentId, int UserId, int ArticleId, string? Text)
        {
            string result = "Не удалось обновить комментарий";
            using (var db = new ApplicationContext())
            {
                Comment comment = db.Comments.SingleOrDefault(comment => comment.Id == CommentId)!;
                if (comment != null)
                {
                    comment.UserId = UserId;
                    comment.ArticleId = ArticleId;
                    comment.Text = Text == null ? comment.Text : Text;
                    db.SaveChanges();
                    result = "Комментарий успешно обновлён";
                }
            }
            return result;
        }

        public static string DeleteComment(int CommentId)
        {
            string result = "Не удалось удалить комментарий";
            using (var db = new ApplicationContext())
            {
                Comment comment = db.Comments.SingleOrDefault(comment => comment.Id == CommentId)!;
                if (comment != null)
                {
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    result = "Комментарий успешно удалён";
                }
            }
            return result;
        }

        // ----- ArticleTags CRUD -----

        public static List<ArticleTag> GetArticleTags()
        {
            using (var db = new ApplicationContext())
            {
                return db.ArticleTags.ToList();
            }
        }

        public static string CreateArticleTag(int ArticleId, int TagId)
        {
            string result = "Не удалось привязать тег к статье";
            using (var db = new ApplicationContext())
            {
                bool isArticleTagExists = db.ArticleTags.Any(articleTag => articleTag.ArticleId == ArticleId && articleTag.TagId == TagId);
                if (!isArticleTagExists)
                {
                    ArticleTag articleTag = new() { ArticleId = ArticleId, TagId = TagId };
                    db.ArticleTags.Add(articleTag);
                    db.SaveChanges();
                    result = "Тег успешно привязан к статье";
                }
            }
            return result;
        }

        public static string UpdateArticleTag(int ArticleTagId, int ArticleId, int TagId)
        {
            string result = "Не удалось обновить тег у статьи";
            using (var db = new ApplicationContext())
            {
                ArticleTag articleTag = db.ArticleTags.SingleOrDefault(articleTag => articleTag.Id == ArticleTagId)!;
                if (articleTag != null)
                {
                    articleTag.ArticleId = ArticleId;
                    articleTag.TagId = TagId;
                    db.SaveChanges();
                    result = "Тег успешно привязан к статье";
                }
            }
            return result;
        }

        public static string DeleteArticleTag(int ArticleTagId)
        {
            string result = "Не удалось удалить тег у статьи";
            using (var db = new ApplicationContext())
            {
                ArticleTag articleTag = db.ArticleTags.SingleOrDefault(articleTag => articleTag.Id == ArticleTagId)!;
                if (articleTag != null)
                {
                    db.ArticleTags.Remove(articleTag);
                    db.SaveChanges();
                    result = "Тег успешно удалён у статьи";
                }
            }
            return result;
        }

        // ----- Articles CRUD -----

        public static List<Article> GetArticles()
        {
            using (var db = new ApplicationContext())
            {
                return db.Articles.ToList();
            }
        }

        public static string CreateArticle(string? Title, string? ShortText, string? Text, int AuthorId, int CategoryId, string? ImagePath)
        {
            string result = "Не удалось создать новую статью";
            using (var db = new ApplicationContext())
            {
                bool isArticleExists = db.Articles.Any(article => article.Title!.ToLower() == Title!.ToLower());
                if (!isArticleExists)
                {
                    Article article = new()
                    {
                        Title = Title,
                        ShortText = ShortText,
                        Text = Text,
                        AuthorId = AuthorId,
                        CategoryId = CategoryId,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Rating = 0
                    };

                    article.Image = "article-" + article.Id;
                    db.Articles.Add(article);
                    db.SaveChanges();

                    article = GetArticles().Last();
                    int lastId = article.Id;
                    article.Image = "article-" + lastId.ToString();

                    var appPath = AppDomain.CurrentDomain.BaseDirectory;
                    var dir = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\";

                    if (File.Exists(dir + article.Image + ".png"))
                    {
                        File.Delete(dir + article.Image + ".png");
                    }

                    File.Copy(ImagePath!, dir + article.Image + ".png");

                    db.Articles.Where(a => a.Id == lastId).ToList().ForEach(i => i.Image = "article-" + lastId);
                    db.SaveChanges();
                    
                    result = "Статья успешно создана";
                }
            }
            return result;
        }

        public static string UpdateArticle
        (
            int ArticleId,
            string? Title,
            string? ShortText,
            string? Text,
            int AuthorId,
            int CategoryId,
            int Rating,
            string? ImagePath
        )
        {
            string result = "Не удалось обновить статью";
            using (var db = new ApplicationContext())
            {
                Article article = db.Articles.SingleOrDefault(article => article.Id == ArticleId)!;
                bool isArticleExists = false;

                foreach (var a in GetArticles())
                    if (a.Title!.ToLower() == Title!.ToLower() && ArticleId != a.Id)
                        isArticleExists = true;

                if (article != null)
                {
                    if (isArticleExists)
                    {
                        return result;
                    }

                    article.Title = Title;
                    article.ShortText = ShortText;
                    article.Text = Text;
                    article.AuthorId = AuthorId;
                    article.CategoryId = CategoryId;
                    article.Updated = DateTime.Now;
                    article.Rating = Rating;

                    var appPath = AppDomain.CurrentDomain.BaseDirectory;
                    var dir = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\";

                    if (ImagePath == article.Image)
                    {
                        db.SaveChanges();
                        result = "Статья успешно обновлена";
                    }
                    else if (!ImagePath!.Contains(article.Image + @".png"))
                    {
                        if (File.Exists(dir + article.Image + @".png"))
                        {
                            File.Delete(dir + article.Image + @".png");
                        }

                        File.Copy(ImagePath!, dir + article.Image + @".png");
                        db.SaveChanges();
                    }

                    db.SaveChanges();
                    result = "Статья успешно обновлена";
                }
            }
            return result;
        }

        public static string DeleteArticle(int ArticleId)
        {
            string result = "Не удалось удалить статью";
            using (var db = new ApplicationContext())
            {
                Article article = db.Articles.SingleOrDefault(article => article.Id == ArticleId)!;
                if (article != null)
                {
                    List<ArticleTag> articleTags = GetArticleTags();
                    List<Comment> comments = GetComments();
                    foreach (var tag in articleTags)
                    {
                        if (tag.ArticleId == ArticleId) DeleteArticleTag(tag.Id);
                    }
                    foreach (var comment in comments)
                    {
                        if (comment.ArticleId == ArticleId) DeleteComment(comment.Id);
                    }
                    db.Articles.Remove(article);
                    db.SaveChanges();

                    result = "Статья успешно удалена";
                }
            }
            return result;
        }

        // ----- Feedbacks Create

        public static string CreateFeedback(string? Message)
        {
            string result = "Не удалось отправить ваше предложение";

            using (var db = new ApplicationContext())
            {
                Feedback feedback = new()
                { 
                    Message = Message
                };
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                result = "Ваше сообщение успешно доставлено. Спасибо за вашу помощь в улучшении проекта!";
            }

            return result;
        }

        // ----- Ratings CRD -----

        public static IEnumerable<Rating> GetRatings()
        {
            using (var db = new ApplicationContext())
            {
                return db.Ratings.ToList();
            }
        }

        public static bool CreateRating(int ArticleId, int UserId)
        {
            using (var db = new ApplicationContext())
            {
                bool isRatingExists = db.Ratings.Any(rating => rating.ArticleId == ArticleId && rating.UserId == UserId);

                if (!isRatingExists)
                {
                    Rating rating = new()
                    {
                        ArticleId = ArticleId,
                        UserId = UserId
                    };

                    db.Ratings.Add(rating);
                    db.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public static bool DeleteRating(int ArticleId, int UserId)
        {
            using (var db = new ApplicationContext())
            {
                bool isRatingExists = db.Ratings.Any(rating => rating.ArticleId == ArticleId && rating.UserId == UserId);

                if (isRatingExists)
                {
                    Rating rating = db.Ratings.Single(rating => rating.ArticleId == ArticleId && rating.UserId == UserId);

                    db.Ratings.Remove(rating);
                    db.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        // ----- Get Articles By Templates -----
        public static IEnumerable<ArticleTemplate> GetArticlesByTemplate()
        {
            using (var db = new ApplicationContext())
            {
                List<ArticleTemplate> articles = new();
                
                var Articles = GetArticles();
                var ArticleTags = GetArticleTags();
                var appPath = AppDomain.CurrentDomain.BaseDirectory;

                Articles.ForEach(article =>
                {
                    var at = ArticleTags.Where(articleTag => articleTag.ArticleId == article.Id).Select(articleTag => articleTag.TagId);
                    List<string> t = GetTags().Where(tag => at.Contains(tag.Id)).Select(tag => tag.Title).ToList()!;

                    var a = new ArticleTemplate(
                        article.Id,
                        article.Title,
                        GetCategories().Single(category => category.Id == article.CategoryId).Title,
                        "Автор: " + GetUsers().Single(user => user.Id == article.AuthorId).Username,
                        appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\article-" + article.Id + ".png",
                        article.Rating,
                        t
                    );
                    articles.Add(a);
                });

                return articles;
            }
        }

        public static object GetArticlesById(int ArticleId)
        {
            using (var db = new ApplicationContext())
            {
                var article = GetArticles().Single(article => article.Id == ArticleId);
                var ArticleTags = GetArticleTags();
                var appPath = AppDomain.CurrentDomain.BaseDirectory;

                var tags = "";
                var at = ArticleTags.Where(articleTag => articleTag.ArticleId == article.Id).Select(articleTag => articleTag.TagId);
                var t = GetTags().Where(tag => at.Contains(tag.Id)).Select(tag => tag.Title).ToList();
                t.ForEach(tag => tags += tag + ", ");

                BitmapImage _image = new BitmapImage();
                _image.BeginInit();
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.UriSource = new Uri(appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\article-" + article.Id + ".png");
                _image.EndInit();

                var obj = new
                {
                    article.Id,
                    article.Title,
                    article.Text,
                    article.ShortText,
                    Created = "Дата создания: " + article.Created.ToString().Substring(0, article.Created.ToString().IndexOf(" ")),
                    Image = _image,
                    Author = "Автор: " + GetUsers().Single(user => user.Id == article.AuthorId).Username,
                    article.Rating,
                    Tags = tags.Substring(0, tags.LastIndexOf(",")),
                    Category = GetCategories().Single(category => category.Id == article.CategoryId).Title
                };

                return obj;
            }
        }

        public static List<object> GetArticleComments(int ArticleId)
        {
            using (var db = new ApplicationContext())
            {
                var Comments = GetComments().Where(comment => comment.ArticleId == ArticleId).ToList();

                List<object> comments = new();
                Comments.ForEach(comment =>
                {
                    var obj = new
                    {
                        comment.Text,
                        Author = comment.UserId == 0 ? "Гость" : GetUsers().Single(user => user.Id == comment.UserId).Username
                    };
                    comments.Add(obj);
                });

                return comments;
            }
        }

        public static void ClearUnusedImages()
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var dir = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images";

            List<string> files = Directory.GetFiles(dir).Select(f => Path.GetFileName(f)).ToList();
            List<string> articlesImages = GetArticles().Select(article => article.Image + ".png").ToList()!;
            
            foreach (var file in files)
            {
                if (file.Contains("article"))
                {
                    if (!articlesImages.Contains(file))
                    {
                        File.Delete(dir + @"\" + file);
                    }
                }
            }
        }
    }
}
