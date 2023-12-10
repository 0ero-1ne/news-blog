using news_blog.View.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.Command
{
    public static class OpenCreationWindowsCommands
    {
        private static void SetCenterPositionOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public static void OpenCreateArticleWindowMethod()
        {
            CreateArticleWindow window = new();
            SetCenterPositionOpen(window);
        }

        public static void OpenCreateCategoryWindowMethod()
        {
            CreateCategoryWindow window = new();
            SetCenterPositionOpen(window);
        }

        public static void OpenCreateTagWindowMethod()
        {
            CreateTagWindow window = new();
            SetCenterPositionOpen(window);
        }

        public static void OpenCreateArticleTagWindowMethod()
        {
            CreateArticleTagWindow window = new();
            SetCenterPositionOpen(window);
        }

        public static void OpenCreateCommentWindowMethod()
        {
            CreateCommentWindow window = new();
            SetCenterPositionOpen(window);
        }

        public static void OpenCreateUserWindowMethod()
        {
            CreateUserWindow window = new();
            SetCenterPositionOpen(window);
        }
    }
}
