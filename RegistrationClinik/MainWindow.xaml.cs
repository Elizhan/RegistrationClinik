using RegistrationClinik.Infras;
using RegistrationClinik.ViewModels;
using RegistrationClinik.Views;
using System.Windows;
using System.Windows.Input;
using Key = System.Windows.Input.Key;

namespace RegistrationClinik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVIewModel();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Add)
                new MainWindowB().Show();
        }
    }
}
