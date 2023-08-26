using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using RegistrationClinik.Views;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

namespace RegistrationClinik.ViewModels
{
    public class BMainWindowViewModel : BaseViewModel
    {
        public BMainWindowViewModel()
        {
            GetAllDate();
            ArchiveCommand = new LambdaCommand(ArchiveCommandExcecute, CanArchiveCommandExcecuted);
            EditCommand = new LambdaCommand(EditCommandExcecute, CanEditCommandExcecuted);
            ShowArchiveWindowCommand = new LambdaCommand(ShowArchiveWindowCommandExcecute, CanShowArchiveWindowCommandExcecuted);
            GetWhiteCommand = new LambdaCommand(GetWhiteCommandExcecute, CanGetWhiteCommandExcecuted);
            SaveToExcelCommand = new LambdaCommand(SaveToExcelCommandExcecuted, CanSaveToExcelCommandExcecute);
        }

        private bool CanSaveToExcelCommandExcecute(object arg)
        {
            return true;
        }



        #region Props

        private ObservableCollection<ShowTableModel> clientCollection;
        public ObservableCollection<ShowTableModel> ClientCollection
        {
            get { return clientCollection; }
            set
            {
                Set(ref clientCollection, value);
            }
        }

        private ShowTableModel selectedClient = new();
        public ShowTableModel SelectedClient
        {
            get { return selectedClient; }
            set
            {
                Set(ref selectedClient, value);
            }
        }
        private string searchText = string.Empty;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                Set(ref searchText, value);
                if (string.IsNullOrEmpty(value))
                    GetAllDate();
                else
                    SearchByText(value);
            }
        }
        public bool IsChange { get; set; } = false;

        #endregion
        public ICommand ArchiveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ShowArchiveWindowCommand { get; set; }
        public ICommand GetWhiteCommand { get; set; }
        public ICommand SaveToExcelCommand { get; set; }

        private bool CanGetWhiteCommandExcecuted(object arg)
        {
            return true;
        }
        private void GetWhiteCommandExcecute(object obj)
        {
            try
            {
                using (ApplicationConnect db = new ApplicationConnect())
                {
                    var result = db.DBTables.FirstOrDefault(s => s.Id == selectedClient.Id);
                    if (result != null)
                    {
                        result.IsShow = 1;
                    }
                    db.SaveChanges();
                    MessageBox.Show("Успешно!");
                    GetAllDate();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ArchiveCommandExcecute(object obj)
        {
            try
            {
                using (ApplicationConnect db = new ApplicationConnect())
                {
                    db.DBArchives.Add(new DBArchive
                    {
                        IsShow = SelectedClient.IsShow,
                        Adres = SelectedClient.Adres,
                        Name = SelectedClient.Name,
                        Birday = SelectedClient.Birday,
                        Analiz = SelectedClient.Analiz,
                        LDoctor = SelectedClient.LDoctor,
                        Oplata = SelectedClient.Oplata,
                        RegistrationDate = SelectedClient.RegistrationDate,
                        TelNumber = SelectedClient.TelNumber,

                    });
                    db.Remove(db.DBTables.FirstOrDefault(s => s.Id == SelectedClient.Id));
                    db.SaveChanges();
                    GetAllDate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }
        private bool CanArchiveCommandExcecuted(object arg)
        {
            return true;
        }
        private bool CanUpdateCommandExcecuted(object arg)
        {
            return true;
        }
        private bool CanEditCommandExcecuted(object arg)
        {
            return true;
        }

        private void EditCommandExcecute(object obj)
        {
            if ((string)obj == "1")
                IsChange = false;
            else
                IsChange = true;
            new regClient(this).Show();
        }
        private bool CanShowArchiveWindowCommandExcecuted(object arg)
        {
            return true;
        }

        private void ShowArchiveWindowCommandExcecute(object obj)
        {
            new Archive(true).Show();
        }

        public void GetAllDate()
        {
            try
            {
                using (ApplicationConnect db = new ApplicationConnect())
                {
                    var result = db.DBTables.ToList();
                    ClientCollection = new ObservableCollection<ShowTableModel>();
                    for (int i = 0; i < result.Count; i++)
                        ClientCollection.Add(new ShowTableModel
                        {
                            Number = i + 1,
                            Name = result[i].Name,
                            Adres = result[i].Adres,
                            Analiz = result[i].Analiz,
                            Birday = result[i].Birday,
                            Bonus = result[i].Bonus,
                            Comments = result[i].Comments,
                            KajBro = result[i].KajBro,
                            PalataNumber = result[i].PalataNumber,
                            TelNumber = result[i].TelNumber,
                            Id = result[i].Id,
                            IsShow = result[i].IsShow,
                            LDoctor = result[i].LDoctor,
                            Ostatok = result[i].Ostatok,
                            Oplata = result[i].Oplata,
                            RegistrationDate = result[i].RegistrationDate,
                            BackColor = result[i].IsShow == 0 ? "Blue" : "Red"
                        });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
          
        }
        private void SearchByText(string value)
        {
            if (ClientCollection is null && ClientCollection == new ObservableCollection<ShowTableModel>())
                return;
            GetAllDate();
            ClientCollection = new ObservableCollection<ShowTableModel>(ClientCollection.Where(s => s.Name.Contains(value)));
        }
        private void SaveToExcelCommandExcecuted(object obj)
        {
            try
            {
                Excel.Application app = new();
                Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets.get_Item(1);

                ws.Cells[1, 1] = "№";
                ws.Cells[1, 2] = "Имя";
                ws.Cells[1, 3] = "Номер палаты";
                ws.Cells[1, 4] = "День рождение";
                ws.Cells[1, 5] = "Адрес";
                ws.Cells[1, 6] = "Номер телефона";
                ws.Cells[1, 7] = "Каж/бро";
                ws.Cells[1, 8] = "Остаток";
                ws.Cells[1, 9] = "Оплата";
                ws.Cells[1, 10] = "Доктор";
                ws.Cells[1, 11] = "Бонус";
                ws.Cells[1, 12] = "Анализ";
                ws.Cells[1, 13] = "Дополнительно";

                for (int i = 0; i < ClientCollection.Count; i++)
                {
                    ws.Cells[i + 2, 1] = clientCollection[i].Number;
                    ws.Cells[i + 2, 2] = clientCollection[i].Name;
                    ws.Cells[i + 2, 3] = clientCollection[i].PalataNumber;
                    ws.Cells[i + 2, 4] = clientCollection[i].Birday.Value.ToShortDateString();
                    ws.Cells[i + 2, 5] = clientCollection[i].Adres;
                    ws.Cells[i + 2, 6] = clientCollection[i].TelNumber;
                    ws.Cells[i + 2, 7] = clientCollection[i].KajBro;
                    ws.Cells[i + 2, 8] = clientCollection[i].Ostatok;
                    ws.Cells[i + 2, 9] = clientCollection[i].Oplata;
                    ws.Cells[i + 2, 10] = clientCollection[i].LDoctor;
                    ws.Cells[i + 2, 11] = clientCollection[i].Bonus;
                    ws.Cells[i + 2, 12] = clientCollection[i].Analiz;
                    ws.Cells[i + 2, 13] = clientCollection[i].Comments;
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
