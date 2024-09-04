using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls; 

namespace RTS
{
    public partial class MainWindow : FluentWindow
    {
        public MainWindow(object dataContext)
        {
            InitializeComponent();
           DataContext = dataContext;
           
        }
    }
}