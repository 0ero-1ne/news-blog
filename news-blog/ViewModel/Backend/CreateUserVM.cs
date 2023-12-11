using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using news_blog.Command;
using news_blog.Model;
using news_blog.View;

namespace news_blog.ViewModel.Backend
{
    public class CreateUserVM : ViewModelBase
    {
        private string? _username;
        public string? Username
        {
            get => _username;
            set
            {
                _username = value!.Trim();
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value!.Trim();
                NotifyPropertyChanged(nameof(Password));
            }
        }

        private string? _isAdmin;
        public string? IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value!.Trim();
                NotifyPropertyChanged(nameof(IsAdmin));
            }
        }

        private RelayCommand? saveUser;
        public RelayCommand SaveUser
        {
            get
            {
                return saveUser ?? new RelayCommand(obj =>
                {
                    var window = (Window)obj;

                    if (Username == "" || Username == null)
                    {
                        MessageBox.Show("Неверное имя пользователя");
                        return;
                    }

                    if (Password == "" || Password == null || Password!.Length < 6)
                    {
                        MessageBox.Show("Неверный пароль");
                        return;
                    }

                    var result = DataWorker.CreateUser(Username, Password, false);
                    window.Close();
                    MessageBox.Show(result);
                    UpdateListViews.UpdateUsers();
                });
            }
        }
    }
}
