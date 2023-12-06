using Microsoft.EntityFrameworkCore;
using news_blog.Context;
using System.Windows;

namespace news_blog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
