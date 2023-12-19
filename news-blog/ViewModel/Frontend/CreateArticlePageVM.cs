using Microsoft.Win32;
using news_blog.Command;
using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace news_blog.ViewModel.Frontend
{
    public class CreateArticlePageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;

        private string? _title;
        public string? Title
        {
            get => _title;
            set
            {
                _title = value!.Trim();
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private string? _shortText;
        public string? ShortText
        {
            get => _shortText;
            set
            {
                _shortText = value!.Trim();
                NotifyPropertyChanged(nameof(ShortText));
            }
        }

        private string? _text;
        public string? Text
        {
            get => _text;
            set
            {
                _text = value!.Trim();
                NotifyPropertyChanged(nameof(Text));
            }
        }

        private string? _category;
        public string? Category
        {
            get => _category;
            set
            {
                _category = value!.Trim();
                NotifyPropertyChanged(nameof(Category));
            }
        }

        private string? _tags;
        public string? Tags
        {
            get => _tags;
            set
            {
                _tags = value!.Trim();
                NotifyPropertyChanged(nameof(Tags));
            }
        }

        private string? _imagePath;
        public string? ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value!;
                NotifyPropertyChanged(nameof(ImagePath));
            }
        }

        private BitmapImage? _image;
        public BitmapImage? Image
        {
            get => _image;
            set
            {
                _image = value!;
                NotifyPropertyChanged(nameof(Image));
            }
        }

        private RelayCommand? pickImage;
        public RelayCommand? PickImage
        {
            get
            {
                return pickImage ?? new RelayCommand(obj =>
                {
                    OpenFileDialog ofd = new();
                    ofd.Title = "News Blog - Выберите изображение для статьи";
                    ofd.Filter = "Portable Network Graphic (*.png)|*.png";

                    ofd.ShowDialog();

                    if (ofd.FileName != "")
                    {
                        ImagePath = ofd.FileName;
                        BitmapImage _image = new BitmapImage();
                        _image.BeginInit();
                        _image.CacheOption = BitmapCacheOption.OnLoad;
                        _image.UriSource = new Uri(ImagePath);
                        _image.EndInit();
                        Image = _image;
                    }
                });
            }
        }

        private RelayCommand? clearFields;
        public RelayCommand? ClearFields
        {
            get
            {
                return clearFields ?? new RelayCommand(obj =>
                {
                    Title = "";
                    ShortText = "";
                    Text = "";
                    Category = "";
                    Tags = "";
                    var appPath = AppDomain.CurrentDomain.BaseDirectory;
                    ImagePath = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\image.png";
                    BitmapImage _image = new BitmapImage();
                    _image.BeginInit();
                    _image.CacheOption = BitmapCacheOption.OnLoad;
                    _image.UriSource = new Uri(ImagePath);
                    _image.EndInit();
                    Image = _image;
                });
            }
        }

        private RelayCommand? backPage;
        public RelayCommand? BackPage
        {
            get
            {
                return backPage ?? new RelayCommand(obj =>
                {
                    _navigationStore.CurrentViewModel = new ProfilePageVM(_navigationStore, _userStore);
                });
            }
        }

        private RelayCommand? createArticle;
        public RelayCommand? CreateArticle
        {
            get
            {
                return createArticle ?? new RelayCommand(obj =>
                {
                    var appPath = AppDomain.CurrentDomain.BaseDirectory;

                    if (Title == "" || Title == null)
                    {
                        MessageBox.Show("Заполните поле \"Заголовок\"", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (ShortText == "" || ShortText == null)
                    {
                        MessageBox.Show("Заполните поле \"Приветственный текст\"", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (Text == "" || Text == null)
                    {
                        MessageBox.Show("Заполните поле \"Текст статьи\"", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (Category == "" || Category == null)
                    {
                        MessageBox.Show("Заполните поле \"Категория\"", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    Tags = Regex.Replace(Tags!, @";+", ";");

                    if (Tags.Last() == ';')
                    {
                        Tags = Tags.Substring(0, Tags.LastIndexOf(";"));
                    }

                    if (Tags == "" || Tags == null)
                    {
                        MessageBox.Show("Заполните поле \"Теги\", разделяя каждый тег символом \";\"", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    if (ImagePath == null || ImagePath == appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\image.png")
                    {
                        MessageBox.Show("Выберите изображение для статьи", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    if (!Regex.IsMatch(Category, @"^[a-zA-Z0-9а-яА-Я#\-()+\s]+$"))
                    {
                        MessageBox.Show("Поле \"Категория\" имеет запрещённые символы", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }


                    DataWorker.CreateCategory(Category.First().ToString().ToUpper() + Category.ToLower().Substring(1));
                    Category category = DataWorker.GetCategories().Single(c => c.Title == Category.First().ToString().ToUpper() + Category.ToLower().Substring(1));

                    string result = DataWorker.CreateArticle(
                        Title,
                        ShortText,
                        Text,
                        CurrentUser.Id,
                        category.Id,
                        ImagePath
                    );

                    if (result == "Не удалось создать новую статью")
                    {
                        MessageBox.Show(result + ": попробуйте поменять заголовок", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    Article article = DataWorker.GetArticles().Single(a => a.Title == Title);

                    Tags.Split(';').ToList().ForEach(tag =>
                    {
                        tag = tag.Trim();
                        var prettyTag = tag.Substring(1).ToLower();
                        prettyTag = tag.First().ToString().ToUpper() + prettyTag;
                        DataWorker.CreateTag(prettyTag);
                        Tag newTag = DataWorker.GetTags().Single(c => c.Title == prettyTag);
                        DataWorker.CreateArticleTag(article.Id, newTag.Id);
                    });

                    _navigationStore.CurrentViewModel = new ProfilePageVM(_navigationStore, _userStore);
                    MessageBox.Show("Статья успешно создана", "News Blog - Информация", MessageBoxButton.OK);
                });
            }
        }

        public CreateArticlePageVM(NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            ImagePath = appPath.Substring(0, appPath.IndexOf("bin")) + @"\Images\image.png";
            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.UriSource = new Uri(ImagePath);
            _image.EndInit();
            Image = _image;
        }
    }
}
