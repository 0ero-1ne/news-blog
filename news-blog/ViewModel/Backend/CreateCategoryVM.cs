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
    public class CreateCategoryVM : ViewModelBase
    {
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

        private RelayCommand? saveCategory;
        public RelayCommand? SaveCategory
        {
            get
            {
                return saveCategory ?? new RelayCommand(obj =>
                {
                    var window = (Window)obj;

                    if (Category == null || Category == "")
                    {
                        MessageBox.Show("Неверная категория", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    var result = DataWorker.CreateCategory(Category);
                    UpdateListViews.UpdateCategories();
                    window.Close();
                    MessageBox.Show(result);
                });
            }
        }
    }
}
