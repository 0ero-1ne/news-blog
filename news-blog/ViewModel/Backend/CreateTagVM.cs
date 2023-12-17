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
    public class CreateTagVM : ViewModelBase
    {
        private string? _tag;
        public string? Tag
        {
            get => _tag;
            set
            {
                _tag = value!.Trim();
                NotifyPropertyChanged(nameof(Tag));
            }
        }

        private RelayCommand? saveTag;
        public RelayCommand SaveTag
        {
            get
            {
                return saveTag ?? new RelayCommand(obj =>
                {
                    var window = (Window)obj;

                    if (Tag == null || Tag == "")
                    {
                        MessageBox.Show("Неверный тег", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    var result = DataWorker.CreateTag(Tag);
                    UpdateListViews.UpdateTags();
                    window.Close();
                    MessageBox.Show(result, "News Blog - Информация", MessageBoxButton.OK);
                });
            }
        }
    }
}
