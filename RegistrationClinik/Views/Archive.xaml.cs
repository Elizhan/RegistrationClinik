using Microsoft.Win32;
using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для Archive.xaml
    /// </summary>
    public partial class Archive : System.Windows.Window
    {
        private bool isB = true;

        private List<DBArchive> Collection = new List<DBArchive>();

        public Archive(bool _isB = true)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            isB = _isB;
            endDate.SelectedDate = DateTime.Now;
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
                Collection = db.DBArchives.Where(s => s.IsShow == 1).ToList();
            }
            dataGrid1.ItemsSource = new List<DBArchive>(Collection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Collection is null && Collection == new List<DBArchive>())
                return;
            if (startDate.SelectedDate is null && endDate.SelectedDate is null)
                dataGrid1.ItemsSource = new List<DBArchive>(Collection);
            else if(startDate.SelectedDate is null && endDate.SelectedDate is not null)
                dataGrid1.ItemsSource = new List<DBArchive>(Collection.Where(s=>s.RegistrationDate.Value.Date <= endDate.SelectedDate.Value.Date));
            else if (startDate.SelectedDate is not null && endDate.SelectedDate is null)
                dataGrid1.ItemsSource = new List<DBArchive>(Collection.Where(s => s.RegistrationDate.Value.Date >= startDate.SelectedDate.Value.Date));
            else dataGrid1.ItemsSource = new List<DBArchive>(Collection.Where(s => s.RegistrationDate.Value.Date >= startDate.SelectedDate.Value.Date && s.RegistrationDate.Value.Date <= endDate.SelectedDate.Value.Date));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = new List<DBArchive>(Collection);
        }

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                dataGrid1.ItemsSource = new List<DBArchive>(Collection);

            dataGrid1.ItemsSource = new List<DBArchive>(Collection.Where(s => s.Name.Contains(textBox1.Text)));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var col = new List<DBArchive>((IEnumerable<DBArchive>)dataGrid1.ItemsSource);
                Excel.Application app = new();
                Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets.get_Item(1);

                ws.Cells[1, 1] = "№";
                ws.Cells[1, 2] = "Имя";
                ws.Cells[1, 3] = "День рождение";
                ws.Cells[1, 4] = "Адрес";
                ws.Cells[1, 5] = "Номер телефона";
                ws.Cells[1, 6] = "Оплата";
                ws.Cells[1, 7] = "Доктор";
                ws.Cells[1, 8] = "Анализ";
                ws.Cells[1, 9] = "Номер палаты";
                ws.Cells[1, 10] = "Дата регистрации";

                for (int i = 0; i < col.Count; i++)
                {
                    ws.Cells[i + 2, 1] = col[i].Id;
                    ws.Cells[i + 2, 2] = col[i].Name;
                    ws.Cells[i + 2, 3] = col[i].Birday.Value.ToShortDateString();
                    ws.Cells[i + 2, 4] = col[i].Adres;
                    ws.Cells[i + 2, 5] = col[i].TelNumber;
                    ws.Cells[i + 2, 6] = col[i].Oplata;
                    ws.Cells[i + 2, 7] = col[i].LDoctor;
                    ws.Cells[i + 2, 8] = col[i].Analiz;
                    ws.Cells[i + 2, 9] = col[i].PalataNumber;
                    ws.Cells[i + 2, 10] = col[i].RegistrationDate.Value.ToShortDateString();
                }

                SaveFileDialog openFile = new SaveFileDialog();
                if (openFile.ShowDialog() == true)
                {
                    workbook.SaveAs(openFile.FileName);
                    MessageBox.Show("Экспортировано успешно!");
                    workbook.Close(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                    app.Quit();

                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    Marshal.ReleaseComObject(ws);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(app);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
