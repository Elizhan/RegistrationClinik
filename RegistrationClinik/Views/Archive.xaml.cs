﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для Archive.xaml
    /// </summary>
    public partial class Archive : Window
    {
        public Archive()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
