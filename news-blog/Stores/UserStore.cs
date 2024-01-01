using news_blog.Model;
using System;

namespace news_blog.Stores
{
    public class UserStore
    {
        public event Action? CurrentUserChanged;

        private User? _currentUser;

        public User? CurrentUser
        {
            get => _currentUser!;
            set
            {
                _currentUser = value;
                OnCurrentUserChanged();
            }
        }

        private void OnCurrentUserChanged()
        {
            CurrentUserChanged?.Invoke();
        }
    }
}
