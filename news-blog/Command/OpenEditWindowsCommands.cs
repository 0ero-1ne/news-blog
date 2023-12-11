using news_blog.Model;
using news_blog.View.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.Command
{
    public class OpenEditWindowsCommands
    {
        private static void SetCenterPositionOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public static void OpenEditTagWindow(Tag tag)
        {
            EditTagWindow window = new(tag);
            SetCenterPositionOpen(window);
        }

        public static void OpenEditCategoryWindow(Category category)
        {
            EditCategoryWindow window = new(category);
            SetCenterPositionOpen(window);
        }

        public static void OpenEditUserWindow(User user)
        {
            EditUserWindow window = new(user);
            SetCenterPositionOpen(window);
        }

        public static void OpenEditArticleWindow(Article article)
        {
            EditArticleWindow window = new(article);
            SetCenterPositionOpen(window);
        }

        public static void OpenEditCommentWindow(Comment comment)
        {
            EditCommentWindow window = new(comment);
            SetCenterPositionOpen(window);
        }
    }
}
