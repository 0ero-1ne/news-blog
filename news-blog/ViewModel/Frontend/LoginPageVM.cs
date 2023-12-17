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
    public class LoginPageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;

        private RelayCommand? openRegisterPage;
        public RelayCommand? OpenRegisterPage
        {
            get
            {
                return openRegisterPage ?? new RelayCommand(obj =>
                {
                    _navigationStore.CurrentViewModel = new RegisterPageVM(_navigationStore, _userStore);
                });
            }
        }

        public LoginPageVM(NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
        }
    }
}
