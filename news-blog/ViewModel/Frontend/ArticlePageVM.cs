using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news_blog.ViewModel.Frontend
{
    class ArticlePageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;
        public object Article { get; set; }

        public List<object> Comments { get; set; }

        public ArticlePageVM(int ArticleId, NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
            Article = DataWorker.GetArticlesById(ArticleId);
            Comments = DataWorker.GetArticleComments(ArticleId);
        }
    }
}
