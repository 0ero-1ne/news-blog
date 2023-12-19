using news_blog.Command;
using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace news_blog.ViewModel.Frontend
{
    public class MainPageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        private List<string> categoriesFilters = new();
        private List<string> tagsFilters = new();
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser!;

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


        private RelayCommand? openArticlePage;
        public RelayCommand OpenArticlePage
        {
            get
            {
                return openArticlePage ?? new RelayCommand(obj =>
                {
                    int articleId = (int)obj;
                    _navigationStore.CurrentViewModel = new ArticlePageVM(articleId, _navigationStore, _userStore, "Main");
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

        public MainPageVM(NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
            Articles = DataWorker.GetArticlesByTemplate();
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
            Articles = categoriesFilters.Count == 0 ? DataWorker.GetArticlesByTemplate() : DataWorker.GetArticlesByTemplate().Where(article => categoriesFilters.Contains(article.Category!));
            foreach (var tag in tagsFilters)
            {
                Articles = tagsFilters.Count == 0 ? Articles : Articles.Where(article => article.Tags!.Contains(tag));
            }
            NotifyPropertyChanged(nameof(Articles));
        }
    }
}
