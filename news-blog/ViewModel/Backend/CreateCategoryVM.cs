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
                        return;

                    var result = DataWorker.CreateCategory(Category);
                    window.Close();
                    MessageBox.Show(result);
                    UpdateCategories();
                });
            }
        }

        private void UpdateCategories()
        {
            AdminPanel.CategoriesList!.ItemsSource = null;
            AdminPanel.CategoriesList.Items.Clear();
            AdminPanel.CategoriesList.ItemsSource = DataWorker.GetCategories();
            AdminPanel.CategoriesList.Items.Refresh();
        }
    }
}
