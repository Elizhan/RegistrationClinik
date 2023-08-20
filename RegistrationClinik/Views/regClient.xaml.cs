﻿using RegistrationClinik.ViewModels;
using System.Windows;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для regClient.xaml
    /// </summary>
    public partial class regClient : Window
    {
        public regClient(BMainWindowViewModel model)
        {
            InitializeComponent();
            DataContext = new RegWindowViewModel(model);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
