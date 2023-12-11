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
using news_blog.Model;
using news_blog.ViewModel.Backend;

namespace news_blog.View.Backend
{
    /// <summary>
    /// Interaction logic for CreateCommentWindow.xaml
    /// </summary>
    public partial class EditCommentWindow : Window
    {
        public EditCommentWindow(Comment comment)
        {
            InitializeComponent();
            DataContext = new EditCommentVM(comment);
        }
    }
}
