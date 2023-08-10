using RegistrationClinik.ViewModels;
using RegistrationClinik.Views;
using System.Windows;
using System.Windows.Input;
using Key = System.Windows.Input.Key;

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

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Add)
                new MainWindow().Show();
        }
    }
}
