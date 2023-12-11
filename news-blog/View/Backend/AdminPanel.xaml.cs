using news_blog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using news_blog.ViewModel.Backend;
using news_blog.Command;
using news_blog.Context;
using news_blog.Model;

namespace news_blog.View
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public static ListView? TagsList;
        public static ListView? CategoriesList;
        public static ListView? UsersList;
        public static ListView? CommentsList;
        public static ListView? ArticlesList;
        public static ListView? ArticleTagsList;

        public AdminPanel()
        {
            InitializeComponent();
            DataContext = new AdminPanelVM();
            TagsList = TagsView;
            CategoriesList = CategoriesView;
            UsersList = UsersView;
            CommentsList = CommentsView;
            ArticlesList = ArticlesView;
            ArticleTagsList = ArticleTagsView;
        }
    }
}
