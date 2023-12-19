using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using news_blog.Command;
using news_blog.View;
using System.ComponentModel;

namespace news_blog.ViewModel.Frontend
{
    public class ProfilePageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        private List<string> categoriesFilters = new();
        private List<string> tagsFilters = new();
    
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser!;

        public string? ProfileTitle => "Профиль: " + CurrentUser.Username;
        public string? Visibility { get; set; }

        public IEnumerable<ArticleTemplate> Articles { get; set; }

        private string? searchString = "";
        public string? SearchString
        {
            get => searchString;
            set
            {
                searchString = value!.Trim();
                NotifyPropertyChanged(nameof(SearchString));
            }
        }

        public List<CheckBox>? Categories { get; set; } = new();
        public List<CheckBox>? Tags { get; set; } = new();

        private RelayCommand? removeArticle;
        public RelayCommand? RemoveArticle
        {
            get
            {
                return removeArticle ?? new RelayCommand(obj =>
                {
                    MessageBoxResult mbr = MessageBox.Show("Вы действительно хотите удалить статью?", "News Blog - Информация", MessageBoxButton.YesNo);
                    if (mbr == MessageBoxResult.Yes)
                    {
                        int articleId = (int)obj;
                        DataWorker.DeleteArticle(articleId);
                        _navigationStore.CurrentViewModel = new ProfilePageVM(_navigationStore, _userStore);
                    }
                });
            }
        }

        private RelayCommand? openEditArticlePage;
        public RelayCommand? OpenEditArticlePage
        {
            get
            {
                return openEditArticlePage ?? new RelayCommand(obj =>
                {
                    int articleId = (int)obj;
                    _navigationStore.CurrentViewModel = new EditArticlePageVM(_navigationStore, _userStore, articleId);
                });
            }
        }

        private RelayCommand? logout;
        public RelayCommand? Logout
        {
            get
            {
                return logout ?? new RelayCommand(obj =>
                {
                    _userStore.CurrentUser = null;
                    _navigationStore.CurrentViewModel = new MainPageVM(_navigationStore, _userStore);
                });
            }
        }

        private RelayCommand? searchArticles;
        public RelayCommand? SearchArticles
        {
            get
            {
                return searchArticles ?? new RelayCommand(obj => {
                    Articles = Articles.Where(article => article.Title!.ToLower().Contains(SearchString!.ToLower()));
                    NotifyPropertyChanged(nameof(Articles));
                });
            }
        }

        private RelayCommand? openCreateArticlePage;
        public RelayCommand OpenCreateArticlePage
        {
            get
            {
                return openCreateArticlePage ?? new RelayCommand(obj =>
                {
                    _navigationStore.CurrentViewModel = new CreateArticlePageVM(_navigationStore, _userStore);
                });
            }
        }

        private RelayCommand? openArticlePage;
        public RelayCommand OpenArticlePage
        {
            get
            {
                return openArticlePage ?? new RelayCommand(obj =>
                {
                    int articleId = (int)obj;
                    _navigationStore.CurrentViewModel = new ArticlePageVM(articleId, _navigationStore, _userStore, "Profile");
                });
            }
        }
        
        private RelayCommand? openAdminPanel;
        public RelayCommand OpenAdminPanel
        {
            get
            {
                return openAdminPanel ?? new RelayCommand(obj =>
                {
                    if (CurrentUser.IsAdmin == 1)
                    {
                        AdminPanel adminPanel = new AdminPanel();
                        adminPanel.Closing += LoadMainPage!;
                        Application.Current.MainWindow.Hide();
                        adminPanel.Show();
                        return;
                    }
                });
            }
        }

        public ProfilePageVM(NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
            Articles = CurrentUser.IsAdmin != 1 ?
                DataWorker.GetArticlesByTemplate().Where(article => article.Author! == ("Автор: " + CurrentUser.Username!)) : 
                DataWorker.GetArticlesByTemplate();
            Visibility = CurrentUser.IsAdmin == 1 ? "Visible" : "Hidden";
            NotifyPropertyChanged(nameof(Visibility));
            CreateCategoriesCheckboxes();
            CreateTagsCheckboxes();
        }

        private void CreateCategoriesCheckboxes()
        {
            List<Category> categories = DataWorker.GetCategories();
            foreach (var category in categories)
            {
                var checkbox = new CheckBox()
                {
                    Content = category.Title,
                    Style = Application.Current.FindResource("CheckboxFilter") as Style
                };
                checkbox.Checked += FilterByCategory;
                checkbox.Unchecked += RemoveCategoryFilter;
                Categories!.Add(checkbox);
            }
        }

        private void CreateTagsCheckboxes()
        {
            List<Tag> tags = DataWorker.GetTags();
            foreach (var tag in tags)
            {
                var checkbox = new CheckBox()
                {
                    Content = tag.Title,
                    Style = Application.Current.FindResource("CheckboxFilter") as Style
                };
                checkbox.Checked += FilterByTag;
                checkbox.Unchecked += RemoveTagFilter;
                Tags!.Add(checkbox);
            }
        }

        private void FilterByCategory(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            categoriesFilters.Add(cb.Content.ToString()!);
            FilterArticles();
        }

        private void RemoveCategoryFilter(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            categoriesFilters.Remove(cb.Content.ToString()!);
            FilterArticles();
        }

        private void FilterByTag(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            tagsFilters.Add(cb.Content.ToString()!);
            FilterArticles();
        }

        private void RemoveTagFilter(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            tagsFilters.Remove(cb.Content.ToString()!);
            FilterArticles();
        }

        private void FilterArticles()
        {
            if (categoriesFilters.Count == 0)
            {
                Articles = CurrentUser.IsAdmin != 1 ?
                    DataWorker.GetArticlesByTemplate().Where(article => article.Author! == "Автор: " + CurrentUser.Username!) :
                    DataWorker.GetArticlesByTemplate();
            } else
            {
                Articles = CurrentUser.IsAdmin != 1 ?
                    DataWorker.GetArticlesByTemplate().Where(article => article.Author! == "Автор: " + CurrentUser.Username!) :
                    DataWorker.GetArticlesByTemplate()
                        .Where(article => categoriesFilters.Contains(article.Category!));
            }

            foreach (var tag in tagsFilters)
            {
                Articles = tagsFilters.Count == 0 ? Articles : Articles.Where(article => article.Tags!.Contains(tag));
            }
            NotifyPropertyChanged(nameof(Articles));
        }

        private void LoadMainPage(object sender, CancelEventArgs e)
        {
            _navigationStore.CurrentViewModel = new MainPageVM(_navigationStore, _userStore);
            Application.Current.MainWindow.Show();
        }
    }
}
