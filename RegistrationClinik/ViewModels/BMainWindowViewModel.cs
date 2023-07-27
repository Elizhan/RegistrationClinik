using RegistrationClinik.Infras;
using RegistrationClinik.Models;
using RegistrationClinik.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        private void ArchiveCommandExcecute(object obj)
        {
            using (ApplicationConnect db = new ApplicationConnect())
            {
                db.DBArchives.Add(new DBArchive
                {
                    IsShow = 0,
                    Adres = SelectedClient.Adres,
                    Name = SelectedClient.Name,
                    Birday = SelectedClient.Birday,
                    Analiz = SelectedClient.Analiz,
                    LDoctor = SelectedClient.LDoctor,
                    Oplata = SelectedClient.Oplata,
                    RegistrationDate = SelectedClient.RegistrationDate
                });
                db.Remove(db.DBTables.FirstOrDefault(s=>s.Id == SelectedClient.Id));
                db.SaveChanges();
                GetAllDate();
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
            new RegClientB(this).Show();
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
                        Avans = result[i].Avans,
                        Birday = result[i].Birday,
                        Id = result[i].Id,
                        IsShow = result[i].IsShow,
                        LDoctor = result[i].LDoctor,
                        Oplacheno = result[i].Oplacheno,
                        Oplata = result[i].Oplata,
                        Ostatok = result[i].Ostatok,
                        RegistrationDate = result[i].RegistrationDate,
                    });
            }
        }
        private void SearchByText(string value)
        {
            if (ClientCollection is null && ClientCollection == new ObservableCollection<ShowTableModel>())
                return;
            GetAllDate();
            ClientCollection = new ObservableCollection<ShowTableModel>(ClientCollection.Where(s => s.Name.Contains(value)));
        }
    }
}
