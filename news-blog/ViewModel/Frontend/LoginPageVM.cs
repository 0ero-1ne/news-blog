using news_blog.Command;
using news_blog.Model;
using news_blog.Stores;
using news_blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace news_blog.ViewModel.Frontend
{
    public class LoginPageVM : ViewModelBase
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

        private RelayCommand? loginUser;
        public RelayCommand? LoginUser
        {
            get
            {
                return loginUser ?? new RelayCommand(obj =>
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

                    if (user == null)
                    {
                        MessageBox.Show("Неверные имя пользователя или пароль", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    var passwordBytes = Encoding.UTF8.GetBytes(Password!);
                    var passwordHash = SHA256.HashData(passwordBytes);
                    var passwordHashString = Convert.ToHexString(passwordHash);

                    if (user.Password != passwordHashString)
                    {
                        MessageBox.Show("Неверные имя пользователя или пароль", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    _userStore.CurrentUser = user;
                    _navigationStore.CurrentViewModel = new MainPageVM(_navigationStore, _userStore);
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
