using news_blog.Command;
using news_blog.Model;
using news_blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.ViewModel.Backend
{
    public class CreateArticleVM : ViewModelBase
    {
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
                        MessageBox.Show("Неверный заголовок");
                        return;
                    }
                    if (ShortText == "" || ShortText == null)
                    {
                        MessageBox.Show("Неверный приветственный текст");
                        return;
                    }
                    if (Text == "" || Text == null)
                    {
                        MessageBox.Show("Неверный текст");
                        return;
                    }
                    if (Category == null)
                    {
                        MessageBox.Show("Неверная категория");
                        return;
                    }
                    if (Author == null)
                    {
                        MessageBox.Show("Неверный автор");
                        return;
                    }
                    string result = DataWorker.CreateArticle(Title, ShortText, Text, Author.Id, Category.Id);
                    window.Close();
                    MessageBox.Show(result);
                    UpdateArticles();
                });
            }
        }

        private void UpdateArticles()
        {
            AdminPanel.ArticlesList!.ItemsSource = null;
            AdminPanel.ArticlesList!.Items.Clear();
            AdminPanel.ArticlesList!.ItemsSource = DataWorker.GetArticles();
            AdminPanel.ArticlesList!.Items.Refresh();
        }
    }
}
