using Microsoft.Win32;
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

        private string? _imagePath;
        public string? ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value!.Trim();
                NotifyPropertyChanged(nameof(ImagePath));
            }
        }

        private RelayCommand? _openDialog;
        public RelayCommand? OpenDialog
        {
            get
            {
                return _openDialog ?? new RelayCommand(obj =>
                {
                    OpenFileDialog ofd = new();
                    ofd.Title = "News Blog - Выберите изображение для статьи";
                    ofd.Filter = "Portable Network Graphic (*.png)|*.png";

                    ofd.ShowDialog();

                    if (ofd.FileName != "")
                    {
                        ImagePath = ofd.FileName;
                    }
                });
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
                    if (ImagePath == null || ImagePath == "")
                    {
                        MessageBox.Show("Выберите изображение для статьи", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    string result = DataWorker.CreateArticle(Title, ShortText, Text, Author.Id, Category.Id, ImagePath);
                    UpdateListViews.UpdateArticles();
                    window.Close();
                    MessageBox.Show(result, "News Blog - Информация", MessageBoxButton.OK);
                });
            }
        }
    }
}
