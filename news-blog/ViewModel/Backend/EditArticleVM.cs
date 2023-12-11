using news_blog.Command;
using news_blog.Model;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;

namespace news_blog.ViewModel.Backend
{
    public class EditArticleVM : ViewModelBase
    {
        private Article? _article;

        public EditArticleVM(Article? article)
        {
            _article = article;
            Title = _article!.Title!;
            ShortText = _article!.ShortText!;
            Text = _article!.Text!;
            Rating = _article!.Rating!.ToString();
            Author = Users.First(user => user.Id == _article.AuthorId);
            Category = Categories.First(category => category.Id == _article.CategoryId);
        }

        private List<Category> _categories = DataWorker.GetCategories();
        public List<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                NotifyPropertyChanged(nameof(Categories));
            }
        }

        private List<User> _users = DataWorker.GetUsers();
        public List<User> Users
        {
            get => _users!;
            set
            {
                _users = value;
                NotifyPropertyChanged(nameof(Users));
            }
        }

        private string? _title;
        public string Title
        {
            get => _title!;
            set
            {
                _title = value!.Trim();
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private string? _shortText;
        public string ShortText
        {
            get => _shortText!;
            set
            {
                _shortText = value!.Trim();
                NotifyPropertyChanged(nameof(ShortText));
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

        private User? _author;
        public User? Author
        {
            get => _author;
            set
            {
                _author = value;
                NotifyPropertyChanged(nameof(Author));
            }
        }

        private Category? _category;
        public Category Category
        {
            get => _category!;
            set
            {
                _category = value;
                NotifyPropertyChanged(nameof(Category));
            }
        }

        private string? _rating;
        public string? Rating
        {
            get => _rating;
            set
            {
                _rating = value!.Trim();
                NotifyPropertyChanged(nameof(Rating));
            }
        }


        private RelayCommand? saveArticle;
        public RelayCommand SaveArticle
        {
            get
            {
                return saveArticle ?? new RelayCommand(obj =>
                {
                    Window window = (Window)obj;
                    if (Title == "" || Title == null)
                    {
                        MessageBox.Show("Неверный заголовок", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (ShortText == "" || ShortText == null)
                    {
                        MessageBox.Show("Неверный приветственный текст", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (Text == "" || Text == null)
                    {
                        MessageBox.Show("Неверный текст", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (Category == null)
                    {
                        MessageBox.Show("Неверная категория", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (Author == null)
                    {
                        MessageBox.Show("Неверный автор", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (!Regex.IsMatch(Rating!, @"^0$|^[1-9][0-9]*$"))
                    {
                        MessageBox.Show("Неверный рейтинг", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    string result = DataWorker.UpdateArticle(_article!.Id, Title, ShortText, Text, Author.Id, Category.Id, Convert.ToInt32(Rating));
                    UpdateListViews.UpdateArticles();
                    window.Close();
                    MessageBox.Show(result);
                });
            }
        }
    }
}
