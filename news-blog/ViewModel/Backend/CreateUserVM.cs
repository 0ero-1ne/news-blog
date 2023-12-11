﻿using System;
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
    }
}
