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
    public class CreateArticleTagVM : ViewModelBase
    {
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

        private List<Tag> _tags = DataWorker.GetTags();
        public List<Tag> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                NotifyPropertyChanged(nameof(Tags));
            }
        }

        private Article? _article;
        public Article? Article
        {
            get => _article;
            set
            {
                _article = value;
                NotifyPropertyChanged(nameof(Article));
            }
        }

        private Tag? _tag;
        public Tag? Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                NotifyPropertyChanged(nameof(Tag));
            }
        }

        private RelayCommand? _saveArticleTag;
        public RelayCommand? SaveArticleTag
        {
            get
            {
                return _saveArticleTag ?? new RelayCommand(obj =>
                {
                    Window window = (Window)obj;
                    if (Article == null)
                    {
                        MessageBox.Show("Неверная статья", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    if (Tag == null)
                    {
                        MessageBox.Show("Неверный тег", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    string result = DataWorker.CreateArticleTag(Article.Id, Tag.Id);
                    UpdateListViews.UpdateArticleTags();
                    window.Close();
                    MessageBox.Show(result);
                });
            }
        }
    }
}
