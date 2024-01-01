using news_blog.ViewModel;
using System;

namespace news_blog.Stores
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel!;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
