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
    public class RegisterPageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;

        private string? _username;
        public string? Username
        {
            get => _username;
            set
            {
                _username = value!;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value!;
                NotifyPropertyChanged(nameof(Password));
            }
        }

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

        private RelayCommand? registerUser;
        public RelayCommand? RegisterUser
        {
            get
            {
                return registerUser ?? new RelayCommand(obj =>
                {
                    if (Username == null || Username == "")
                    {
                        MessageBox.Show("Заполните имя пользователя", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    if (Password == null || Username == "")
                    {
                        MessageBox.Show("Заполните пароль", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    User user = DataWorker.GetUsers().SingleOrDefault(u => u.Username == Username)!;

                    if (user != null)
                    {
                        MessageBox.Show("Такой пользователь уже существует", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    if (Password.Length < 6)
                    {
                        MessageBox.Show("Пароль должен содержать минимум 6 символов", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    DataWorker.CreateUser(Username, Password, false);

                    _userStore.CurrentUser = DataWorker.GetUsers().Single(user => user.Username == Username);
                    _navigationStore.CurrentViewModel = new MainPageVM(_navigationStore, _userStore);
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
