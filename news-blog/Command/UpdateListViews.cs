using news_blog.Model;
using news_blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news_blog.Command
{
    public static class UpdateListViews
    {
        public static void UpdateArticleTags()
        {
            AdminPanel.ArticleTagsList!.ItemsSource = null;
            AdminPanel.ArticleTagsList!.Items.Clear();
            AdminPanel.ArticleTagsList!.ItemsSource = DataWorker.GetArticleTags();
            AdminPanel.ArticleTagsList!.Items.Refresh();
        }

        public static void UpdateArticles()
        {
            AdminPanel.ArticlesList!.ItemsSource = null;
            AdminPanel.ArticlesList!.Items.Clear();
            AdminPanel.ArticlesList!.ItemsSource = DataWorker.GetArticles();
            AdminPanel.ArticlesList!.Items.Refresh();
        }

        public static void UpdateCategories()
        {
            AdminPanel.CategoriesList!.ItemsSource = null;
            AdminPanel.CategoriesList.Items.Clear();
            AdminPanel.CategoriesList.ItemsSource = DataWorker.GetCategories();
            AdminPanel.CategoriesList.Items.Refresh();
        }

        public static void UpdateComments()
        {
            AdminPanel.CommentsList!.ItemsSource = null;
            AdminPanel.CommentsList!.Items.Clear();
            AdminPanel.CommentsList!.ItemsSource = DataWorker.GetComments();
            AdminPanel.CommentsList?.Items.Refresh();
        }

        public static void UpdateTags()
        {
            AdminPanel.TagsList!.ItemsSource = null;
            AdminPanel.TagsList.Items.Clear();
            AdminPanel.TagsList.ItemsSource = DataWorker.GetTags();
            AdminPanel.TagsList.Items.Refresh();
        }

        public static void UpdateUsers()
        {
            AdminPanel.UsersList!.ItemsSource = null;
            AdminPanel.UsersList.Items.Clear();
            AdminPanel.UsersList.ItemsSource = DataWorker.GetUsers();
            AdminPanel.UsersList.Items.Refresh();
        }

        public static void UpdateAllLists()
        {
            UpdateArticles();
            UpdateComments();
            UpdateTags();
            UpdateUsers();
            UpdateArticleTags();
            UpdateCategories();

        }
    }
}
