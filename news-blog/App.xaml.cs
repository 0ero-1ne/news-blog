using Microsoft.EntityFrameworkCore;
using news_blog.Context;
using news_blog.Stores;
using news_blog.ViewModel.Frontend;
using news_blog.Model;
using System.Globalization;
using System.Windows;
using System.Linq;

namespace news_blog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;

        public App()
        {
            _navigationStore = new();
            _userStore = new();
            using (var db = new ApplicationContext())
            {
                db.Database.Migrate();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new MainPageVM(_navigationStore, _userStore);
            _userStore.CurrentUser = null;

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowVM(_navigationStore, _userStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
