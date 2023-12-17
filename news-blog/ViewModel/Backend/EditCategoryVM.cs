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
    public class EditCategoryVM : ViewModelBase
    {
        private readonly Category? category;

        public EditCategoryVM(Category category)
        {
            this.category = category;
            _title = category.Title;
        }

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

        private RelayCommand? saveCategory;
        public RelayCommand? SaveCategory
        {
            get
            {
                return saveCategory ?? new RelayCommand(obj =>
                {
                    var window = (Window)obj;

                    if (Title == null || Title == "")
                    {
                        MessageBox.Show("Неверная категория", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    var result = DataWorker.UpdateCategory(category!.Id, Title);
                    UpdateListViews.UpdateCategories();
                    window.Close();
                    MessageBox.Show(result, "News Blog - Информация", MessageBoxButton.OK);
                });
            }
        }
    }
}
