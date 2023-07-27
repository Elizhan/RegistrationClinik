using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для Archive.xaml
    /// </summary>
    public partial class Archive : Window
    {
        private bool isB = false;

        private List<DBArchive> Collection = new List<DBArchive>();

        public Archive(bool _isB = false)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            isB = _isB;
            GetAllDate();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void GetAllDate()
        {
            using ApplicationConnect db = new();
            if (isB)
            {

                Collection = db.DBArchives.ToList();
            }
            else 
            {
                Collection = db.DBArchives.Where(s=>s.IsShow == 1).ToList();
            }
            dataGrid1.ItemsSource = new List<DBArchive>(Collection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Collection is null && Collection == new List<DBArchive>())
                return;
            dataGrid1.ItemsSource = new List<DBArchive>(Collection.Where(s=>s.RegistrationDate.Value.Date >= startDate.SelectedDate.Value.Date && s.RegistrationDate.Value.Date <= endDate.SelectedDate.Value.Date));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = new List<DBArchive>(Collection);
        }

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                dataGrid1.ItemsSource = new List<DBArchive>(Collection);

            dataGrid1.ItemsSource = new List<DBArchive>(Collection.Where(s=>s.Name.Contains(textBox1.Text)));
        }
    }
}
