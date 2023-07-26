﻿using RegistrationClinik.ViewModels;
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
            model = new BMainWindowViewModel();
            DataContext = model;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void showAddPage(object sender, RoutedEventArgs e)
        {
            BMainWindowViewModel.VisButtonB = false;
            new RegClientB(model).Show();
        }
    }
}