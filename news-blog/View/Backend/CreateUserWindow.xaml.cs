using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using news_blog.Command;
using news_blog.Model;
using news_blog.ViewModel;
using news_blog.ViewModel.Backend;

namespace news_blog.View.Backend
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string? _username;
        public string? Username
        {
            get => _username;
            set
            {
                _username = value!.Trim();
            }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
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
                        MessageBox.Show("Неверное имя пользователя", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    if (Password == "" || Password == null || Password!.Length < 6)
                    {
                        MessageBox.Show("Неверный пароль", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    var result = DataWorker.CreateUser(Username, Password, false);
                    UpdateListViews.UpdateUsers();
                    window.Close();
                    MessageBox.Show(result);
                });
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        private void UsernameChanged(object sender, RoutedEventArgs e)
        {
            Username = ((TextBox)sender).Text.Trim();
        }
    }
}
