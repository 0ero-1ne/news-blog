using news_blog.ViewModel.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace news_blog.View.Backend
{
    /// <summary>
    /// Interaction logic for CreateArticleTagWindow.xaml
    /// </summary>
    public partial class CreateArticleTagWindow : Window
    {
        public CreateArticleTagWindow()
        {
            InitializeComponent();
            DataContext = new CreateArticleTagVM();
        }
    }
}
