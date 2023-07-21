using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RegistrationClinik.ViewModels;
using RegistrationClinik.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Key = System.Windows.Input.Key;

namespace RegistrationClinik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVIewModel model;

        public MainWindow()
        {
            InitializeComponent();
            model = new MainWindowVIewModel();
            DataContext = model;
        } 

        private void showAddPage(object sender, RoutedEventArgs e)
        {
            MainWindowVIewModel.VisButton = false;
            new regClient(model).Show();
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

        private void Edit(object sender, RoutedEventArgs e)
        {
            MainWindowVIewModel.VisButton = true;
            object ob = ((Button)sender).CommandParameter;
            new regClient(model).Show();
        }
    }
}
