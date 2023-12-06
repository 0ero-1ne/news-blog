using news_blog.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace news_blog.Model
{
    public static class DataWorker
    {

        // ----- Tags CRUD -----

        public static string CreateTag(string? Tag)
        {
            string result = "Тег уже существует";
            using (var db = new ApplicationContext())
            {
                bool isTagExist = db.Tags.Any(tag => tag.Title == Tag);
                if (!isTagExist && Tag != null)
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
            string result = "Тег не обновлен";
            using (var db = new ApplicationContext())
            {
                Tag tag = db.Tags.SingleOrDefault(tag => tag.Id == TagId)!;
                Tag isTagExist = db.Tags.SingleOrDefault(tag => tag.Title == TagTitle)!;
                if (tag != null)
                {
                    if (isTagExist != null && isTagExist.Id != tag.Id)
                    {
                        return result;
                    }

                    tag.Title = TagTitle == null ? tag.Title : TagTitle;
                    db.SaveChanges();
                    result = "Тег успешно обновлен";
                }
            }
            return result;
        }

        public static string DeleteTag(int TagId)
        {
            string result = "Тег не существует";
            using (var db = new ApplicationContext())
            {
                Tag tag = db.Tags.SingleOrDefault(tag => tag.Id == TagId)!;
                if (tag != null)
                {
                    db.Tags.Remove(tag);
                    db.SaveChanges();
                    result = "Тег успешно удалён";
                }
            }
            return result;
        }

        // ----- Categories CRUD -----

        public static string CreateCategory(string? Category, int ParentCategoryId)
        {
            string result = "Категория уже существует";
            using (var db = new ApplicationContext())
            {
                bool isCategoryExists = db.Categories.Any(category => category.Title == Category);
                if (!isCategoryExists && Category != null)
                {
                    Category category = new() { Title = Category, ParentCategory = ParentCategoryId < 0 ? 0 : ParentCategoryId };
                    db.Categories.Add(category);
                    db.SaveChanges();
                    result = "Категория успешно создана";
                }
            }
            return result;
        }

        public static string UpdateCategory(int CategoryId, string? CategoryTitle, int ParentCategoryId)
        {
            string result = "Категория не обновлена";
            using (var db = new ApplicationContext())
            {
                Category category = db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;
                Category isCategoryExists = db.Categories.Single(category => category.Title == CategoryTitle)!;
                if (category != null)
                {
                    if (isCategoryExists != null && isCategoryExists.Id != category.Id)
                    {
                        return result;
                    }

                    category.Title = CategoryTitle == null ? category.Title : CategoryTitle;
                    category.ParentCategory = ParentCategoryId < 0 ? 0 : ParentCategoryId;
                    db.SaveChanges();
                    result = "Категория успешно обновлена";
                }
            }
            return result;
        }

        public static string DeleteCategory(int CategoryId)
        {
            string result = "Категория не существует";
            using (var db = new ApplicationContext())
            {
                Category category = db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;
                if (category != null)
                {
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    result = "Категория успешно удалена";
                }
            }
            return result;
        }

        // ----- Users CRUD -----

        public static string CreateUser(string? Username, string? Password, bool IsAdmin)
        {
            string result = "Не удалось создать пользователя";
            using (var db = new ApplicationContext())
            {
                bool isUserExists = db.Users.Any(user => user.Username == Username);
                if (!isUserExists)
                {
                    User user = new() { Username = Username, Password = Password, IsAdmin = IsAdmin ? 1 : 0 };
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

                    user.Username = Username == null ? user.Username : Username;
                    user.Password = Password == null ? user.Password : Password;
                    user.IsAdmin = IsAdmin ? 1 : 0;
                    db.SaveChanges();
                    result = "Пользователь успешно обновлён";
                }
            }
            return result;
        }

        public static string DeleteUser(int UserId)
        {
            string result = "Пользователя не существует";
            using (var db = new ApplicationContext())
            {
                User user = db.Users.SingleOrDefault(user => user.Id == UserId)!;
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    result = "Пользователь успешно удалён";
                }
            }
            return result;
        }

        // ----- Comments CRUD -----

        public static string CreateComment(int UserId, int ArticleId, string? Text)
        {
            string result = "Комментарий не добавлен";
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
            string result = "Комментарий не обновлён";
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
            string result = "Комментарий не удалён";
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

        public static string CreateArticleTag(int ArticleId, int TagId)
        {
            string result = "Не удалось привязать тег к статье";
            using (var db = new ApplicationContext())
            {
                ArticleTag articleTag = new() { ArticleId = ArticleId, TagId = TagId };
                db.ArticleTags.Add(articleTag);
                db.SaveChanges();
                result = "Тег успешно привязан к статье";
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
                    articleTag.ArticleId = ArticleTagId;
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

        public static string CreateArticle(string? Title, string? ShortText, string? Text, int AuthorId, int CategoryId)
        {
            string result = "Не удалось создать статью";
            using (ApplicationContext db = new ApplicationContext())
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
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article = db.Articles.SingleOrDefault(article => article.Id == ArticleId)!;
                Article isArticleExists = db.Articles.SingleOrDefault(article => article.Title!.ToLower() == Title!.ToLower())!;
                if (article != null)
                {
                    if (isArticleExists != null && isArticleExists.Id != article.Id)
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
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article = db.Articles.SingleOrDefault(article => article.Id == ArticleId)!;
                if (article != null)
                {
                    db.Articles.Remove(article);
                    db.SaveChanges();
                    result = "Статья успешно удалена";
                }
            }
            return result;
        }
    }
}
