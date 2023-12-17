using news_blog.Command;
using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news_blog.ViewModel.Frontend
{
    public class RegisterPageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;

        private RelayCommand? openLoginPage;
        public RelayCommand? OpenLoginPage
        {
            get
            {
                return openLoginPage ?? new RelayCommand(obj =>
                {
                    _navigationStore.CurrentViewModel = new LoginPageVM(_navigationStore, _userStore);
                });
            }
        }

        public RegisterPageVM(NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
        }
    }
}
