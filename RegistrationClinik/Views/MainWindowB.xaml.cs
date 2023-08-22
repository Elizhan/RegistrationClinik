using Microsoft.Office.Interop.Excel;
using RegistrationClinik.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using Key = System.Windows.Input.Key;

namespace RegistrationClinik.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowB.xaml
    /// </summary>
    public partial class MainWindowB
    {
        Microsoft.Office.Interop.Excel.Application excel;
        Microsoft.Office.Interop.Excel.Workbook workBook;
        Microsoft.Office.Interop.Excel.Worksheet workSheet;
        Microsoft.Office.Interop.Excel.Range cellRange;

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
            GenerateExcel1();
            convertExcel();
        }

        private void convertExcel()
        {
            workBook.SaveAs(System.IO.Path.Combine(@"Drive:\Folder(s)\", "Excel book Name"));
            workBook.Close();
            excel.Quit();
        }

        private void GenerateExcel1()
        {
            //try
            //{
            MessageBox.Show("sadads");
            excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DisplayAlerts = false;
            excel.Visible = false;
            workBook = excel.Workbooks.Add(Type.Missing);
            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
            workSheet.Name = "LearningExcel";
            //System.Data.DataTable tempDt = (System.Data.DataTable)DtIN;
            //datagrid1.ItemsSource = tempDt.DefaultView;
            workSheet.Cells.Font.Size = 11;
            int rowcount = 1;
            for (int i = 1; i <= datagrid1.Columns.Count; i++) //taking care of Headers.  
            {
                workSheet.Cells[1, i] = datagrid1.Columns[i - 1].Header;
            }
            foreach (System.Data.DataRow row in datagrid1.Items) //taking care of each Row  
            {
                rowcount += 1;
                for (int i = 0; i < datagrid1.Columns.Count; i++) //taking care of each column  
                {
                    workSheet.Cells[rowcount, i + 1] = row[i].ToString();
                }
            }
            cellRange = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[rowcount, datagrid1.Columns.Count]];
            cellRange.EntireColumn.AutoFit();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private void WhiteWindowClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
