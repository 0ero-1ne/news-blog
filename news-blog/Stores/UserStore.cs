using news_blog.Model;
using news_blog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
