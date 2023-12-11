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
using System.Windows.Controls;

namespace news_blog.ViewModel
{
    public class AdminPanelVM : ViewModelBase
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

        private RelayCommand? openCreateArticleWindow;
        public RelayCommand OpenCreateArticleWindow
        {
            get
            {
                return openCreateArticleWindow ?? new RelayCommand(obj =>
                {
                    OpenCreationWindowsCommands.OpenCreateArticleWindowMethod();
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
                    OpenCreationWindowsCommands.OpenCreateCategoryWindowMethod();
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
                    OpenCreationWindowsCommands.OpenCreateTagWindowMethod();
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
                    OpenCreationWindowsCommands.OpenCreateUserWindowMethod();
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
                    OpenCreationWindowsCommands.OpenCreateArticleTagWindowMethod();
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
                    OpenCreationWindowsCommands.OpenCreateCommentWindowMethod();
                });
            }
        }

        private Article? _selectedArticle;
        public Article? SelectedArticle
        {
            get => _selectedArticle;
            set
            {
                _selectedArticle = value;
                NotifyPropertyChanged(nameof(SelectedArticle));
            }
        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                NotifyPropertyChanged(nameof(SelectedCategory));
            }
        }

        private Tag? _selectedTag;
        public Tag? SelectedTag
        {
            get => _selectedTag;
            set
            {
                _selectedTag = value;
                NotifyPropertyChanged(nameof(SelectedTag));
            }
        }

        private ArticleTag? _selectedArticleTag;
        public ArticleTag? SelectedArticleTag
        {
            get => _selectedArticleTag;
            set
            {
                _selectedArticleTag = value;
                NotifyPropertyChanged(nameof(SelectedArticleTag));
            }
        }

        private Comment? _selectedComment;
        public Comment? SelectedComment
        {
            get => _selectedComment;
            set
            {
                _selectedComment = value;
                NotifyPropertyChanged(nameof(SelectedComment));
            }
        }

        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                NotifyPropertyChanged(nameof(SelectedUser));
            }
        }

        private TabItem? _selectedTabItem;
        public TabItem? SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                NotifyPropertyChanged(nameof(SelectedTabItem));
            }
        }

        private RelayCommand? deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string result = "Ничего не выбрано";

                    switch (SelectedTabItem!.Name)
                    {
                        case "Articles":
                            if (SelectedArticle != null)
                            {
                                result = DataWorker.DeleteArticle(SelectedArticle.Id);
                                SelectedArticle = null;
                            }
                            break;
                        case "Categories":
                            if (SelectedCategory != null)
                            {
                                result = DataWorker.DeleteCategory(SelectedCategory.Id);
                                SelectedCategory = null;
                            }
                            break;
                        case "Tags":
                            if (SelectedTag != null)
                            {
                                result = DataWorker.DeleteTag(SelectedTag.Id);
                                SelectedTag = null;
                            }
                            break;
                        case "ArticleTags":
                            if (SelectedArticleTag != null)
                            {
                                result = DataWorker.DeleteArticleTag(SelectedArticleTag.Id);
                                SelectedArticleTag = null;
                            }
                            break;
                        case "Comments":
                            if (SelectedComment != null)
                            {
                                result = DataWorker.DeleteComment(SelectedComment.Id);
                                SelectedComment = null;
                            }
                            break;
                        case "Users":
                            if (SelectedUser != null)
                            {
                                result = DataWorker.DeleteUser(SelectedUser.Id);
                                SelectedUser = null;
                            }
                            break;
                        default:
                            break;
                    }
                    UpdateListViews.UpdateAllLists();
                    MessageBox.Show(result);
                });
            }
        }
    }
}
