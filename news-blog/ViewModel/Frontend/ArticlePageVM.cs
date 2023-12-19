using news_blog.Command;
using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.ViewModel.Frontend
{
    class ArticlePageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser!;

        private string? _previousPage;
        public object Article { get; set; }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                _comment = value!.Trim();
                NotifyPropertyChanged(nameof(Comment));
            }
        }

        private string? _ratingImage;
        public string? RatingImage
        {
            get => _ratingImage;
            set
            {
                _ratingImage = value;
                NotifyPropertyChanged(nameof(RatingImage));
            }
        }

        private RelayCommand? saveComment;
        public RelayCommand SaveComment
        {
            get
            {
                return saveComment ?? new RelayCommand(obj =>
                {
                    int articleId = (int)obj;
                    int userId = CurrentUser == null ? 0 : CurrentUser.Id;
                    if (Comment == "" || Comment == null)
                    {
                        MessageBox.Show("Заполните поле \"Комментарий\"", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    DataWorker.CreateComment(userId, articleId, Comment);
                    _navigationStore.CurrentViewModel = new ArticlePageVM(articleId, _navigationStore, _userStore, _previousPage!);
                });
            }
        }

        private RelayCommand? backPage;
        public RelayCommand? BackPage
        {
            get
            {
                return backPage ?? new RelayCommand(obj =>
                {
                    if (_previousPage == "Profile")
                    {
                        _navigationStore.CurrentViewModel = new ProfilePageVM(_navigationStore, _userStore);
                        return;
                    }
                    if (_previousPage == "Main")
                    {
                        _navigationStore.CurrentViewModel = new MainPageVM(_navigationStore, _userStore);
                        return;
                    }
                });
            }
        }

        private RelayCommand? rateArticle;
        public RelayCommand? RateArticle
        {
            get
            {
                return rateArticle ?? new RelayCommand(obj =>
                {
                    if (CurrentUser == null)
                    {
                        MessageBox.Show("Авторизуйтесь, чтобы оценить статью", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    int ArticleId = (int)obj;

                    if (DataWorker.GetArticles().Single(article => article.Id == ArticleId).AuthorId == CurrentUser.Id)
                    {
                        MessageBox.Show("Вы не можете оценить самого себя", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    var appPath = AppDomain.CurrentDomain.BaseDirectory;

                    if (DataWorker.CreateRating(ArticleId, CurrentUser.Id))
                    {
                        Article a = DataWorker.GetArticles().Single(ar => ar.Id == ArticleId);
                        DataWorker.UpdateArticle(a.Id, a.Title, a.ShortText, a.Text, a.AuthorId, a.CategoryId, a.Rating + 1, a.Image);
                        _navigationStore.CurrentViewModel = new ArticlePageVM(ArticleId, _navigationStore, _userStore, _previousPage!);
                    }
                    else
                    {
                        DataWorker.DeleteRating(ArticleId, CurrentUser.Id);
                        Article a = DataWorker.GetArticles().Single(ar => ar.Id == ArticleId);
                        DataWorker.UpdateArticle(a.Id, a.Title, a.ShortText, a.Text, a.AuthorId, a.CategoryId, a.Rating - 1, a.Image);
                        _navigationStore.CurrentViewModel = new ArticlePageVM(ArticleId, _navigationStore, _userStore, _previousPage!);
                    }
                });
            }
        }

        public List<object> Comments { get; set; }

        public ArticlePageVM(int ArticleId, NavigationStore navigationStore, UserStore userStore, string previousPage)
        {
            _previousPage = previousPage;
            _navigationStore = navigationStore;
            _userStore = userStore;
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            if (CurrentUser != null)
            {
                bool rating = DataWorker.GetRatings().Any(rating => rating.UserId == _userStore.CurrentUser!.Id && rating.ArticleId == ArticleId);
                if (rating)
                {
                    RatingImage = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\heart_black.png";
                } else
                {
                    RatingImage = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\heart.png";
                }
            } else
            {
                RatingImage = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\heart.png";
            }
            Article = DataWorker.GetArticlesById(ArticleId);
            Comments = DataWorker.GetArticleComments(ArticleId);
        }
    }
}
