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
        public Archive(bool isB = false)
        {
            InitializeComponent();
            GetAllDate(isB);
        }
        private void GetAllDate(bool isB)
        {
            using ApplicationConnect db = new();
            if (isB)
            {
                dataGrid1.ItemsSource = new List<DBArchive>(db.DBArchives);
            }
            else 
            {
                dataGrid1.ItemsSource = new List<DBArchive>(db.DBArchives.Where(s=>s.IsShow == 1));
            }
        }
    }
}
