using RegistrationClinik.ViewModels;
using RegistrationClinik.Views;
using System.Windows;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowB.xaml
    /// </summary>
    public partial class MainWindowB : Window
    {
        public BMainWindowViewModel model;
        public MainWindowB()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            model = new BMainWindowViewModel();
            DataContext = model;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
