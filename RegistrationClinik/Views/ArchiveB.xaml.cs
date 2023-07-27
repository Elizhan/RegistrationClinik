using System;
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
    /// Логика взаимодействия для ArchiveB.xaml
    /// </summary>
    public partial class ArchiveB : Window
    {
        public ArchiveB()
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
