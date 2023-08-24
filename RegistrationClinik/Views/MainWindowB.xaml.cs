
using RegistrationClinik.ViewModels;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Key = System.Windows.Input.Key;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections;

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
