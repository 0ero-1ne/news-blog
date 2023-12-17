using news_blog.Command;
using news_blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.ViewModel.Backend
{
    public class EditTagVM : ViewModelBase
    {
        private Tag? tag;

        public EditTagVM(Tag tag)
        {
            this.tag = tag;
            _title = tag.Title;
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

        private RelayCommand? _saveTag;
        public RelayCommand? SaveTag
        {
            get
            {
                return _saveTag ?? new RelayCommand(obj =>
                {
                    Window window = (Window)obj;
                    if (Title == "" || Title == null)
                    {
                        MessageBox.Show("Неверный тег", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }
                    string result = DataWorker.UpdateTag(tag!.Id, Title);
                    UpdateListViews.UpdateTags();
                    window.Close();
                    MessageBox.Show(result, "News Blog - Информация", MessageBoxButton.OK);
                });
            }
        }
    }
}
