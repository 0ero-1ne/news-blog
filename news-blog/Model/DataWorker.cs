using news_blog.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
                bool isArticleTagExists = db.ArticleTags.Any(articleTag => articleTag.Id == ArticleId && articleTag.TagId == TagId);
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

        public static string CreateArticle(string? Title, string? ShortText, string? Text, int AuthorId, int CategoryId)
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
                    db.Articles.Add(article);
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
            int Rating
        )
        {
            string result = "Не удалось обновить статью";
            using (var db = new ApplicationContext())
            {
                Article article = db.Articles.SingleOrDefault(article => article.Id == ArticleId)!;
                bool isArticleExists = false;

                foreach (var a in GetArticles())
                    if (a.Title!.ToLower() == Title!.ToLower())
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
    }
}
