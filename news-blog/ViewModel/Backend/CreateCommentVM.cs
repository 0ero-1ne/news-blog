﻿using news_blog.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using news_blog.View;
using news_blog.Model;

namespace news_blog.ViewModel.Backend
{
    public class CreateCommentVM : ViewModelBase
    {
        private List<User> _users = DataWorker.GetUsers();
        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                NotifyPropertyChanged(nameof(Users));
            }
        }

        private List<Article> _articles = DataWorker.GetArticles();
        public List<Article> Articles
        {
            get => _articles;
            set
            {
                _articles = value;
                NotifyPropertyChanged(nameof(Articles));
            }
        }

        private User? _user;
        public User? User
        {
            get => _user;
            set
            {
                _user = value!;
                NotifyPropertyChanged(nameof(User));
            }
        }

        public Article? _article;
        public Article? Article
        {
            get => _article;
            set
            {
                _article = value!;
                NotifyPropertyChanged(nameof(Article));
            }
        }

        private string? _text;
        public string Text
        {
            get => _text!;
            set
            {
                _text = value!.Trim();

                NotifyPropertyChanged(nameof(Text));
            }
        }

        private RelayCommand? saveComment;
        public RelayCommand SaveComment
        { 
            get
            {
                return saveComment ?? new RelayCommand(obj =>
                {
                    Window window = (Window)obj;
                    if (Text == "" || Text == null)
                    {
                        MessageBox.Show("Неверный текст");
                        return;
                    }
                    if (User == null)
                    {
                        MessageBox.Show("Неверный пользователь");
                        return;
                    }
                    if (Article == null)
                    {
                        MessageBox.Show("Неверная статья");
                        return;
                    }
                    string result = DataWorker.CreateComment(User.Id, Article.Id, Text);
                    window.Close();
                    MessageBox.Show(result);
                    UpdateListViews.UpdateComments();
                });
            }
        }
    }
}
