using news_blog.Command;
using news_blog.Model;
using news_blog.View;
using news_blog.View.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.ViewModel
{
    public class AdminPanelVM : INotifyPropertyChanged
    {
        private List<Article> _articles = DataWorker.GetArticles();
        public List<Article> Articles
        {
            get => _articles;
            set
            {
                _articles = value;
                NotifyPropertyChanged("Articles");
            }
        }

        private List<Tag> _tags = DataWorker.GetTags();
        public List<Tag> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                NotifyPropertyChanged("Tags");
            }
        }

        private List<ArticleTag> _articlesTags = DataWorker.GetArticleTags();
        public List<ArticleTag> ArticleTags
        {
            get => _articlesTags;
            set
            {
                _articlesTags = value;
                NotifyPropertyChanged("ArticleTags");
            }
        }

        private List<Category> _categories = DataWorker.GetCategories();
        public List<Category> Categories
        { 
            get => _categories;
            set
            {
                _categories = value;
                NotifyPropertyChanged("Categories");
            }
        }

        private List<Comment> _comments = DataWorker.GetComments();
        public List<Comment> Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                NotifyPropertyChanged("Comments");
            }
        }

        private List<User> _users = DataWorker.GetUsers();
        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                NotifyPropertyChanged("Users");
            }
        }

        private void SetCenterPositionOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void OpenCreateArticleWindowMethod()
        {
            CreateArticleWindow window = new();
            SetCenterPositionOpen(window);
        }

        private void OpenCreateCategoryWindowMethod()
        {
            CreateCategoryWindow window = new();
            SetCenterPositionOpen(window);
        }

        private void OpenCreateTagWindowMethod()
        {
            CreateTagWindow window = new();
            SetCenterPositionOpen(window);
        }

        private void OpenCreateArticleTagWindowMethod()
        {
            CreateArticleTagWindow window = new();
            SetCenterPositionOpen(window);
        }

        private void OpenCreateCommentWindowMethod()
        {
            CreateCommentWindow window = new();
            SetCenterPositionOpen(window);
        }

        private void OpenCreateUserWindowMethod()
        {
            CreateUserWindow window = new();
            SetCenterPositionOpen(window);
        }

        private RelayCommand? openCreateArticleWindow;
        public RelayCommand OpenCreateArticleWindow
        {
            get
            {
                return openCreateArticleWindow ?? new RelayCommand(obj =>
                {
                    OpenCreateArticleWindowMethod();
                });
            }
        }

        private RelayCommand? openCreateCategoryWindow;
        public RelayCommand OpenCreateCategoryWindow
        {
            get
            {
                return openCreateCategoryWindow ?? new RelayCommand(obj =>
                {
                    OpenCreateCategoryWindowMethod();
                });
            }
        }

        public RelayCommand? openCreateTagWindow;
        public RelayCommand OpenCreateTagWindow
        {
            get
            {
                return openCreateTagWindow ?? new RelayCommand(obj =>
                {
                    OpenCreateTagWindowMethod();
                });
            }
        }

        private RelayCommand? openCreateUserWindow;
        public RelayCommand OpenCreateUserWindow
        {
            get
            {
                return openCreateUserWindow ?? new RelayCommand(obj =>
                {
                    OpenCreateUserWindowMethod();
                });
            }
        }

        private RelayCommand? openCreateArticleTagWindow;
        public RelayCommand OpenCreateArticleTagWindow
        {
            get
            {
                return openCreateArticleTagWindow ?? new RelayCommand(obj =>
                {
                    OpenCreateArticleTagWindowMethod();
                });
            }
        }

        private RelayCommand? openCreateCommentWindow;
        public RelayCommand OpenCreateCommentWindow
        {
            get
            {
                return openCreateCommentWindow ?? new RelayCommand(obj =>
                {
                    OpenCreateCommentWindowMethod();
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
