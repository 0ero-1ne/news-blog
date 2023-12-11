using System;
using news_blog.ViewModel.Backend;
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
using news_blog.Model;

namespace news_blog.View.Backend
{
    /// <summary>
    /// Interaction logic for CreateTagWindow.xaml
    /// </summary>
    public partial class EditTagWindow : Window
    {
        public EditTagWindow(Tag tag)
        {
            InitializeComponent();
            DataContext = new EditTagVM(tag);
        }
    }
}
