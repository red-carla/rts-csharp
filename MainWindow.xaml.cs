using System.Text;
using System.Windows;


namespace RTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow(object dataContext)
        {
             InitializeComponent();
            DataContext = dataContext;
           
        }
    }
}