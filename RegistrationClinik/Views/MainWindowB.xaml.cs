
using RegistrationClinik.ViewModels;
using RegistrationClinik.Views;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Key = System.Windows.Input.Key;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.OleDb;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowB.xaml
    /// </summary>
    public partial class MainWindowB : System.Windows.Window
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
            {
                new MainWindow().Show();
                this.Close();
            }
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void SaveToExcel(object sender, RoutedEventArgs e)
        {

        }
        
        
        private void WhiteWindowClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
