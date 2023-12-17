using news_blog.Command;
using news_blog.Model;
using news_blog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace news_blog.ViewModel.Frontend
{
    public class FeedbackPageVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public User CurrentUser => _userStore.CurrentUser;

        private string? feedbackText = "Ваше предложение...";
        public string? FeedbackText
        {
            get => feedbackText;
            set
            {
                feedbackText = value!;
                NotifyPropertyChanged(nameof(FeedbackText));
            }
        }

        private RelayCommand? saveFeedback;
        public RelayCommand? SaveFeedback
        {
            get
            {
                return saveFeedback ?? new RelayCommand(obj =>
                {
                    if (FeedbackText == "" || FeedbackText == null)
                    {
                        MessageBox.Show("Заполните поле перед тем, как отправлять ваш отзыв", "News Blog - Информация", MessageBoxButton.OK);
                        return;
                    }

                    string result = DataWorker.CreateFeedback(FeedbackText);
                    MessageBox.Show(result, "News Blog - Информация", MessageBoxButton.OK);
                    FeedbackText = "Ваше предложение...";
                });
            }
        }

        public FeedbackPageVM(NavigationStore navigationStore, UserStore userStore)
        {
            _navigationStore = navigationStore;
            _userStore = userStore;
        }
    }
}
